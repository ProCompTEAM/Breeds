using AutoMapper;
using BusinessLogic.Providers.Interfaces;
using BusinessLogic.Services.Interfaces;
using Common.Dtos;
using Common.Dtos.Interfaces;
using Common.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class BreedsService : IBreedsService
    {
        private HttpClient httpClient = new HttpClient();

        private readonly IDatabaseProvider databaseProvider;

        private readonly IMapper _mapper;

        public BreedsService(IDatabaseProvider _databaseProvider, IMapper mapper)
        {
            databaseProvider = _databaseProvider;

            _mapper = mapper;
        }

        public ResultDto InitBreeds()
        {
            if (databaseProvider.Count<DogBreed>() != 0)
            {
                return GetResultErrorDto("Call DELETE /breeds for clearing all breeds.");
            }

            LoadBreedsByRequest();

            return GetResultSuccessDto();
        }

        public BreedsListDto GetAllBreeds()
        {
            BreedsListDto breedsList = new BreedsListDto();

            List<DogBreed> dogBreeds = databaseProvider.GetAll<DogBreed>();

            breedsList.Breeds = _mapper.Map<List<BreedDto>>(dogBreeds);

            return breedsList;
        }

        public ResultDto RemoveAllBreeds()
        {
            if (databaseProvider.Count<DogBreed>() == 0)
            {
                return GetResultErrorDto("There are no breeds in db. Call /init endpoint for loading them.");
            }

            databaseProvider.ClearTable<DogBreed>();
            databaseProvider.ClearTable<ChildDogBreed>();

            databaseProvider.Commit();

            return GetResultSuccessDto();
        }

        public IDto GetBreedByName(string breedName)
        {
            if (databaseProvider.Count<DogBreed>() == 0)
            {
                return GetResultErrorDto("There are no breeds in db. Call /init endpoint for loading them.");
            }

            DogBreed dogBreed = GetDogBreedByName(breedName);

            if (dogBreed == null)
            {
                ChildDogBreed childDogBreed = GetChildDogBreedByName(breedName);

                if (childDogBreed == null)
                {
                    return GetResultErrorDto($"Breed with name {breedName} was not found");
                }

                return _mapper.Map<ChildBreedDto>(childDogBreed);
            }

            return _mapper.Map<BreedDto>(dogBreed);
        }

        public IDto GetBreedImages(string breedName, int imagesLimit)
        {
            if (databaseProvider.Count<DogBreed>() == 0)
            {
                return GetResultErrorDto("There are no breeds in db. Call /init endpoint for loading them.");
            }

            IDto dogBreed = GetBreedByName(breedName);

            if (dogBreed is ResultDto)
            {
                return dogBreed;
            }

            BreedImagesDto<IDto> breedImagesDto = new BreedImagesDto<IDto>();

            breedImagesDto.Breed = dogBreed;
            breedImagesDto.Images = LoadBreedImages(breedName, imagesLimit);

            return breedImagesDto;
        }


        private void LoadBreedsByRequest()
        {
            Task<IncomingBreedDto> httpRequestTask = httpClient.GetFromJsonAsync<IncomingBreedDto>("https://dog.ceo/api/breeds/list/all");

            httpRequestTask.Wait();

            IncomingBreedDto breedsList = httpRequestTask.Result;

            foreach (KeyValuePair<string, List<string>> entry in breedsList.Message)
            {
                databaseProvider.Create(GetDogBreedModel(entry.Key, entry.Value));
            }

            databaseProvider.Commit();
        }

        private List<string> LoadBreedImages(string breedName, int imagesLimit)
        {
            Task<IncomingBreedImagesDto> httpRequestTask = httpClient.GetFromJsonAsync<IncomingBreedImagesDto>($"https://dog.ceo/api/breed/{breedName}/images/random/{imagesLimit}");

            httpRequestTask.Wait();

            IncomingBreedImagesDto breedsList = httpRequestTask.Result;

            return breedsList.Message;
        }

        private DogBreed GetDogBreedModel(string breedName, List<string> childs)
        {
            DogBreed dogBreed = new DogBreed
            {
                Name = breedName,
                Childs = new List<ChildDogBreed>()
            };

            childs.ForEach(child =>
            {
                dogBreed.Childs.Add(new ChildDogBreed
                {
                    Name = child
                });
            });

            return dogBreed;
        }

        private ResultDto GetResultSuccessDto()
        {
            return new ResultDto
            {
                Success = true
            };
        }

        private ResultDto GetResultErrorDto(string errorMessage)
        {
            return new ResultDto
            {
                Success = false,
                Error = new ErrorDto { Message = errorMessage }
            };
        }

        private DogBreed GetDogBreedByName(string breedName)
        {
            return databaseProvider.SingleOrDefault<DogBreed>(breed => breed.Name == breedName);
        }

        private ChildDogBreed GetChildDogBreedByName(string breedName)
        {
            return databaseProvider.SingleOrDefault<ChildDogBreed>(breed => breed.Name == breedName);
        }
    }
}
