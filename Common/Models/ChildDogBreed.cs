using Common.Constants;
using Common.Models.Attributes;
using Common.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class ChildDogBreed : BaseEntity
    {
        [Required, Unicode(DatabaseConstants.DefaultStringLength)]
        public string Name { get; set; }
    }
}
