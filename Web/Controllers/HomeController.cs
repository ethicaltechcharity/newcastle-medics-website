using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.DataModels;
using Web.Helpers.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICaptchaHelper captchaHelper;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ICaptchaHelper captchaHelper, ApplicationDbContext dbContext)
        {
            this.captchaHelper = captchaHelper;
            this.dbContext = dbContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play()
        {
            return View();
        }

        public ActionResult Committee()
        {
            return View();
        }

        public ActionResult Contact()
        {
            var model = new ContactViewModel
            {
                Categories = BuildCategoriesList()
            };

            ViewBag.Success = false;

            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel viewModel)
        {
            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool IsCaptchaValid = (captchaHelper.Validate(EncodedResponse) == "true");

            if (!IsCaptchaValid || !ModelState.IsValid)
            {
                ViewBag.Success = false;
                viewModel.Categories = BuildCategoriesList();
                return View(viewModel);
            }

            var dataModel = new ContactDataModel
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Category = viewModel.SelectedCategory,
                Message = viewModel.Message,
                DateTime = DateTime.Now
            };

            dbContext.ContactSubmissions.Add(dataModel);
            dbContext.SaveChanges();

            ModelState.Clear();
            ViewBag.Success = true;

            return View(new ContactViewModel
            {
                Categories = BuildCategoriesList()
            });
        }

        private List<SelectListItem> BuildCategoriesList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Fixture", Value = "fixture" },
                new SelectListItem { Text = "Umpiring", Value = "umpiring" },
                new SelectListItem { Text = "Training", Value = "training" },
                new SelectListItem { Text = "Website", Value = "website" },
                new SelectListItem { Text = "Other", Value = "other" }
            };
        }
    }
}