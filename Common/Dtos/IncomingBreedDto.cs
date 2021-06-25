using Common.Dtos.Interfaces;
using System.Collections.Generic;

namespace Common.Dtos
{
    public class IncomingBreedDto : IDto
    {
        public Dictionary<string, List<string>> Message { get; set; }

        public bool Success { get; set; }
    }
}
