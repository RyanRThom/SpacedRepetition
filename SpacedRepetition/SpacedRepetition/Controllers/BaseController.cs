using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using SpacedRepetition.Models;
using SpacedRepetition.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SpacedRepetition.Controllers
{
    public class BaseController : Controller
    {
        public static ApplicationUser GetCurrentUser()
        {
            return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        public async Task SendEmail(string firstName, string email, string subject, string plainTextContent, string htmlContent) //pass in email, subject, text
        {
            var apiKey = WebConfigurationManager.AppSettings["SendGridEnvironmentalKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("thomrya000@stu.oasd.org", "The HuYu Team");
            var to = new EmailAddress(email, firstName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}