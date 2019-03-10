using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETB310_TennantWebsite.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace ETB310_TennantWebsite.MailKit
{
    /// <summary>
    /// SendMailSimple använder sig av MailKit och ett Googlekonto med sänkt säkerhet. 
    /// installera MailKit med Nuget package manager. skriv: 
    ///     Install-Package MailKit
    /// 
    /// I stället för Oath2 så räcker det här med användarnamn och lösenord
    /// I Googlekontot du använder så måste du först godkänna anslutning med sänkt säkerhet. 
    /// Gå till https://www.google.com/settings/security/lesssecureapps & välj "Turn On"
    /// </summary>
    public class SendMailSimple
    {

        public static void SendServiceCase(RegistrationConfirmationViewModel serviceCase)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("THN-AB", "thn.ab.service@gmail.com"));
            message.To.Add(new MailboxAddress("Serviceärende", "anders.andreen@hv.se"));
            message.Subject = "Serviceärende från webbplatsen";
            message.Body = new TextPart("plain")
            {
                Text = $"namn: {serviceCase.Name}\nlägenhetsnummer: {serviceCase.FlatNr}\n"
                    +$"E-Post: {serviceCase.ContactEmail}\n\n"
                    +$"Meddelande:\n{serviceCase.Message}"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);
                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("thn.ab.service@gmail.com", "ETB310Femskagg");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}