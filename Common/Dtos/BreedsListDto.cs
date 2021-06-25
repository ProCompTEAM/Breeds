using Common.Dtos.Interfaces;
using System.Collections.Generic;

namespace Common.Dtos
{
    public class BreedsListDto : IDto
    {
        public List<BreedDto> Breeds { get; set; }

        public bool Success { get; } = true;
    }
}
