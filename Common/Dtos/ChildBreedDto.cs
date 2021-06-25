using Common.Dtos.Interfaces;

namespace Common.Dtos
{
    public class ChildBreedDto : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
