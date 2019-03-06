using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETB310_TennantWebsite.Models
{
    public class ServiceCasePostViewModel
    {
        public DateTime Date { get; set; } // sätts automatiskt av webbservicen vid sparning 

        public string Message { get; set; } // obligatoriskt 
    }
}