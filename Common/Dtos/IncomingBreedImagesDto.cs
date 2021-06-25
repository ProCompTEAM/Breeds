using Common.Dtos.Interfaces;
using System.Collections.Generic;

namespace Common.Dtos
{
    public class IncomingBreedImagesDto : IDto
    {
        public List<string> Message { get; set; }

        public bool Success { get; set; }
    }
}
