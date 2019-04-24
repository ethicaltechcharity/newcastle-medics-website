using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Helpers.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly IMemberHelper memberHelper;
        private readonly IPlayerHelper playerHelper;
        private readonly ApplicationDbContext dbContext;

        public MemberController(IMemberHelper memberHelper, IPlayerHelper playerHelper,
            ApplicationDbContext dbContext)
        {
            this.memberHelper = memberHelper;
            this.playerHelper = playerHelper;
            this.dbContext = dbContext;
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var model = memberHelper.BuildMemberIndexViewModel(new MemberIndexViewModel
            {
                PageNumber = pageNumber
            });

            return View(model);
        }

        [HttpPost]
        public PartialViewResult Index(MemberIndexViewModel model)
        {
            var responseModel = memberHelper.BuildMemberIndexViewModel(model);

            return PartialView("_ListPartial", responseModel);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Submitted = false;

            return View(memberHelper.BuildMemberRegistrationViewModel(null, false));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(MemberRegistrationViewModel model)
        {
            ViewBag.Submitted = false;

            if (!ModelState.IsValid)
            {
                return View(memberHelper.BuildMemberRegistrationViewModel(model, false));
            }

            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(model.SelectedDay)
                || string.IsNullOrWhiteSpace(model.SelectedMonth)
                || string.IsNullOrWhiteSpace(model.SelectedYear))
            {
                ModelState.AddModelError
                (
                    "DateOfBirth",
                    "You must provide your date of birth."
                );
                return View(memberHelper.BuildMemberRegistrationViewModel(model, false));
            }

            if (!playerHelper.ParseDate(
                model.SelectedDay,
                model.SelectedMonth,
                model.SelectedYear,
                out var dateOfBirth))
            {
                ModelState.AddModelError("DateOfBirth", "Invalid date of birth entered");
                return View(memberHelper.BuildMemberRegistrationViewModel(model, false));
            }

            model.DateOfBirth = dateOfBirth;

            var errors = memberHelper.ValidateMemberRegistrationViewModel(model);

            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }

                return View(memberHelper.BuildMemberRegistrationViewModel(model, false));
            }

            model = memberHelper.CreateNewRegistration(model);

            ViewBag.Submitted = true;

            return View(memberHelper.BuildMemberRegistrationViewModel(model, true));
        }

        public ActionResult Details(int id)
        {
            var memberDataModel = dbContext.MemberIdentities.Find(id);

            if (memberDataModel.Gender == "male")
            {
                if (User.IsInRole("mens_1s_captain")
                    || User.IsInRole("mens_2s_captain")
                    || User.IsInRole("mens_3s_captain")
                    || User.IsInRole("mens_4s_captain")
                    || User.IsInRole("mens_club_captain"))
                {
                    return View(memberHelper.BuildMemberDetailsViewModel(id));
                }
            }
            else
            {
                if (User.IsInRole("ladies_1s_captain")
                    || User.IsInRole("ladies_2s_captain")
                    || User.IsInRole("ladies_3s_captain")
                    || User.IsInRole("ladies_club_captain")
                    || User.IsInRole("mens_4s_captain"))
                {
                    return View(memberHelper.BuildMemberDetailsViewModel(id));
                }
                
            }
            
            if(User.IsInRole("admin"))
            {
                return View(memberHelper.BuildMemberDetailsViewModel(id));
            }


            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }

        [AllowAnonymous]
        public ActionResult CodeOfConduct()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult DataPurposes()
        {
            return View();
        }
    }
}