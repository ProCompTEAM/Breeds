using Common.Dtos.Interfaces;
using System.Collections.Generic;

namespace Common.Dtos
{
    public class BreedImagesDto<T> : IDto
    {
        public T Breed;

        public List<string> Images;
    }
}
