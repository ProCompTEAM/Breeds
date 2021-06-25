using Common.Dtos;
using Common.Dtos.Interfaces;

namespace BusinessLogic.Services.Interfaces
{
    public interface IBreedsService
    {
        ResultDto InitBreeds();

        BreedsListDto GetAllBreeds();

        ResultDto RemoveAllBreeds();

        IDto GetBreedByName(string breedName);

        IDto GetBreedImages(string breedName, int imagesLimit);
    }
}
