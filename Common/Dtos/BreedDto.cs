using Common.Dtos.Interfaces;
using System.Collections.Generic;

namespace Common.Dtos
{
    public class BreedDto : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ChildBreedDto> Childs { get; set; }
    }
}
