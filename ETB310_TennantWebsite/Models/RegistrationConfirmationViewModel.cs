using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETB310_TennantWebsite.Models
{
    public class RegistrationConfirmationViewModel
    {
        public string CaseNr { get; set; }
        public string FlatNr { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
       public string Message { get; set; }
    }
}