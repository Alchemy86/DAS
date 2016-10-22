using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class ErrorReport
    {
        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }

    }
}
