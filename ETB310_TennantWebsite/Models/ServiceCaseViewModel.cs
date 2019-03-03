using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETB310_TennantWebsite.Models
{
    public class ServiceCaseViewModel
    {
        [Required(ErrorMessage = "Du måste skriva ett lägenhetsnumer.")]
        [StringLength(4,ErrorMessage ="Lägenhetsnumret måste var fyra siffror.", MinimumLength =4)]
        [RegularExpression(@"^\d{4}",ErrorMessage ="lägenhetsnumret måste var fyra siffror")]
        public string FlatNr { get; set; } // optional 

        public string Name { get; set; } // optional 

        [Required(ErrorMessage = "Du måste skriva en emailadres.")]
        //ett @ måste finas och minst en .
        public string ContactEmail { get; set; } // obligatory 

        public List<ServiceCasePostViewModel> Posts { get; set; }

        [Required(ErrorMessage = "Du måste skriva vad felet på lägenheten är.")]
        public string NewPostMessage { get; set; } // obligatory 
    }
}