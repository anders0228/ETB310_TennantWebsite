using ETB310_TennantWebsite.MailKit;
using ETB310_TennantWebsite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace ETB310_TennantWebsite.Controllers
{
    public class ServiceCaseController : Controller
    {
        public ActionResult Index()
        {
            var vm = new ServiceCaseViewModel();
            return View("RegisterServiceCase", vm);
        }

        [HttpPost]
        public ActionResult EditServiceCase(ServiceCaseViewModel vm)
        {
            //ModelState.AddModelError("edit","");
            return View("RegisterServiceCase",vm);
        }
        [HttpPost]
        public ActionResult RegisterServiceCase(ServiceCaseViewModel vm)
        {
            // Kolla att alla inmatade fält är ok och returnera formuläret om nåt är fel
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // Mappa data från viewmodel till webbservicens klasser 
            var post = new ServiceReference1.ServiceCasePost()
            {
                Message = vm.NewPostMessage,
                Name = vm.Name,
                ContactEmail = vm.ContactEmail
            };
            //  skapa ett ServiceCase
            Int32.TryParse(vm.FlatNr, out int flatNr);
            var serviceCase = new ServiceReference1.ServiceCase
            {
                Name = vm.Name,
                FlatNr = flatNr,
                ContactEmail = vm.ContactEmail,
            };

            // skapa en lista för eventuella registreringsfel
            var errorLog = new List<ErrorLogItem>();

            try
            {
                // Anslut till webbservicen, lägg till meddelandetexten som ett inlägg och registrera ärendet
                var service = new ServiceReference1.Service1Client();
                serviceCase = service.CreateCase(serviceCase);
                serviceCase.Posts = new ServiceReference1.ServiceCasePost[1] 
                {
                    service.AddPost(serviceCase.CaseNr, post)
                };

                // Konrollera att registreringen gick bra
                ValidateEqual(vm.Name, serviceCase.Name, errorLog);
                ValidateEqual(vm.FlatNr, serviceCase.FlatNr.ToString(), errorLog);
                ValidateEqual(vm.ContactEmail, serviceCase.ContactEmail, errorLog);
                ValidateEqual(vm.NewPostMessage, serviceCase.Posts?[0].Message ?? "", errorLog);
            }
            catch (CommunicationException ex)
            {
                // Ifall webbservicen inte hittades e.d.
                errorLog.Add(new ErrorLogItem("CommunicationException", ex.Message));
                // TODO: Mejla alla uppkomna fel till en admin-webbadress
                Debug.WriteLine("ERROR: " + ex.Message);
            }

            //Kolla att det inte blev några fel
            if (errorLog.Count() == 0 
                && (serviceCase.Errors == null || serviceCase.Errors.Count() == 0)
                && (serviceCase.Posts.LastOrDefault().Errors == null || serviceCase.Posts.LastOrDefault().Errors.Count()==0))
            {
                // Mappa till resultat-vymodellen
                var vmResult = new RegistrationConfirmationViewModel
                {
                    CaseNr = serviceCase.CaseNr.ToString(),
                    Name = serviceCase.Name,
                    FlatNr = serviceCase.FlatNr.ToString(),
                    ContactEmail = serviceCase?.ContactEmail ?? "".ToString(),
                    Message = serviceCase.Posts[0].Message,
                };
                //SendMailSimple.SendRegistrationConfirmation(vmResult);
                return View("RegistrationConfirmation", vmResult);
            }

            // Ifall registreringen misslyckades:
            vm.TryCount++;
            // För att vm.TryCount++ ska appliceras så måste vi rensa ModelState-cashen med ModelState.Clear()
            // läs här för förklaring:
            // https://blogs.msdn.microsoft.com/simonince/2010/05/05/asp-net-mvcs-html-helpers-render-the-wrong-value/
            ModelState.Clear();
            return View("RegistrationError", vm);
        }

        private bool ValidateEqual(string value1, string value2, List<ErrorLogItem> errorLog)
        {
            if (value1 != value2)
            {
                var error = new ErrorLogItem("notEqual", "'" + value1 + "' IS NOT EQUAL TO '" + value2 + "'");
                errorLog.Add(error);
                Debug.WriteLine("ERROR: " + error.Description);
                return false;
            }
            return true;
        }
    }
}