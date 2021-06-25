using Common.Dtos.Interfaces;

namespace Common.Dtos
{
    public class ErrorDto : IDto
    {
        public string Message { get; set; }
    }
}
