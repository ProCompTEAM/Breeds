using Common.Constants;
using Common.Models.Attributes;
using Common.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class DogBreed : BaseEntity
    {
        [Required, Unicode(DatabaseConstants.DefaultStringLength)]
        public string Name { get; set; }

        [Required]
        public virtual ICollection<ChildDogBreed> Childs { get; set; }
    }
}
