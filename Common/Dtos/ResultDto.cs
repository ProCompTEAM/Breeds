using Common.Dtos.Interfaces;

namespace Common.Dtos
{
    public class ResultDto : IDto
    {
        public bool Success { get; set; }

        public ErrorDto Error { get; set; } = null;
    }
}
