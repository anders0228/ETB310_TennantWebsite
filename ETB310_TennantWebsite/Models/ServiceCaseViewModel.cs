using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETB310_TennantWebsite.Models
{
    public class ServiceCaseViewModel
    {
        [Required(ErrorMessage = "Du måste ange ett lägenhetsnumer.")]
        [StringLength(4,ErrorMessage ="Lägenhetsnumret måste var fyra siffror.", MinimumLength =4)]
        [RegularExpression(@"^\d{4}",ErrorMessage ="lägenhetsnumret måste var fyra siffror")]
        public string FlatNr { get; set; } // required 

        [Required(ErrorMessage = "Du måste ange ett namn.")]
        [StringLength(30,ErrorMessage ="Namn måste vara minst 2 tecken och inte längre än 30 tecken", MinimumLength =2)]
        public string Name { get; set; } // required 
        
        [Required(ErrorMessage = "Du måste skriva en emailadres.")]
        [StringLength(40, ErrorMessage = "Epostadressen måste vara minst 6 tecken och inte längre än 40 tecken.", MinimumLength = 6)]
        [RegularExpression(@"[\w\.-_]+@([\w\.-_]+\.)+[a-z]+", ErrorMessage ="Detta är inte en giltig e-postadress")]
        public string ContactEmail { get; set; } // required 

        [Required(ErrorMessage = "Du måste beskriva ditt ärende.")]
        [StringLength(2000, ErrorMessage = "Beskrivningen får inte vara längre än 2000 tecken.")]
        public string NewPostMessage { get; set; } // required 

        public List<ServiceCasePostViewModel> Posts { get; set; }
        public int TryCount { get; set; }
    }
}
