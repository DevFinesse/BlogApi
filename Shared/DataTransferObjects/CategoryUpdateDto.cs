using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CategoryUpdateDto
    {

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string? Name { get; set; }   
    }
}
