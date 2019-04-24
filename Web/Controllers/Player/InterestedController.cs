using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.DataModels;
using Web.Helpers;
using Web.Helpers.Interfaces;
using Web.Models;

namespace Web.Controllers.Player
{
    [Authorize(Roles = "admin")]
    public class InterestedController : Controller
    {
        private readonly IPlayerHelper playerHelper;
        private readonly IMemberHelper memberHelper;

        public InterestedController(IPlayerHelper playerHelper, IMemberHelper memberHelper)
        {
            this.playerHelper = playerHelper;
            this.memberHelper = memberHelper;
        }

        [AllowAnonymous]
        public ActionResult Express()
        {
            return View("~/Views/Player/Interested/Express.cshtml",
                playerHelper.BuildExpressInterestModel(null));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Express(ExpressInterestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Player/Interested/Express.cshtml",
                    playerHelper.BuildExpressInterestModel(model));
            }

            if (playerHelper.NumberOfUsersWithGivenEmail(model.EmailAddress) > 0)
            {
                ModelState.AddModelError("EmailAddress", 
                    "An expression of interest with that email address already exists");

                return View("~/Views/Player/Interested/Express.cshtml",
                    playerHelper.BuildExpressInterestModel(model));
            }

            var date = new DateTime();

            if (!playerHelper.ParseDate(model.SelectedDay, model.SelectedMonth, model.SelectedYear, out date))
            {
                ModelState.AddModelError("DateOfBirth", "You have entered an invalid date.");

                return View("~/Views/Player/Interested/Express.cshtml",
                    playerHelper.BuildExpressInterestModel(model));
            }

            model.DateOfBirth = date;

            MemberRoleDataModel roleDataModel = memberHelper.CreateNewMemberRoleIfNotExist("interested");

            var userIdentity = new MemberIdentityDataModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.SelectedGender,
                Email = model.EmailAddress.ToLower(),
                Phone = model.PhoneNumber,
                Roles = new List<MemberRoleDataModel>
                {
                    roleDataModel
                }
            };

            var userId = memberHelper.CreateNewMemberIdentity(userIdentity);

            playerHelper.CreateNewExpressionOfInterest(model, userId);
            
            model.Submitted = true;

            return View("~/Views/Player/Interested/Express.cshtml", model);
        }
        
        public ActionResult List(int? page)
        {
            int pageSize = 50;

            return View("~/Views/Player/Interested/List.cshtml",
                playerHelper.BuildInterestedList(page ?? 1, pageSize));
        }
        
        public ActionResult Edit(int id)
        {
            var model = playerHelper.BuildExpressInterestModel(id);

            ViewBag.Id = id;

            return View("~/Views/Player/Interested/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(int id, ExpressInterestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Player/Interested/Edit.cshtml",
                    playerHelper.BuildExpressInterestModel(model));
            }

            if (playerHelper.NumberOfUsersWithGivenEmail(model.EmailAddress) > 1)
            {
                ModelState.AddModelError("EmailAddress",
                    "Another expression of interest with that email address already exists");

                return View("~/Views/Player/Interested/Express.cshtml",
                    playerHelper.BuildExpressInterestModel(model));
            }

            playerHelper.EditExpressionOfInterest(id, model);

            ViewBag.Saved = "Changes Saved";

            return View("~/Views/Player/Interested/Edit.cshtml",
                playerHelper.BuildExpressInterestModel(id));
        }

        public ActionResult Delete(int id)
        {
            var model = playerHelper.BuildExpressInterestModel(id);

            ViewBag.Deleted = false;
            ViewBag.Id = id;

            return View("~/Views/Player/Interested/Delete.cshtml", model);
        }

        [HttpPost]
        public ActionResult Delete(int id, object model)
        {
            playerHelper.DeleteExpressionOfInterest(id);

            ViewBag.Deleted = true;

            return View("~/Views/Player/Interested/Delete.cshtml");
        }

        [AllowAnonymous]
        public ActionResult AssignNumber()
        {
            return View("~/Views/Player/Interested/AssignNumber.cshtml", 
                playerHelper.BuildAssignNumberViewModel(null));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AssignNumber(AssignNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Player/Interested/AssignNumber.cshtml",
                    playerHelper.BuildAssignNumberViewModel(model));
            }

            if (playerHelper.NumberAlreadyAssigned(model.AssignedNumber, model.DateOfAssignment))
            {
                ModelState.AddModelError("UserId",
                    "Number Assignment failed, this number has already been assigned today, if this " +
                    "number has been assigned in error please contact the administrator.");

                return View("~/Views/Player/Interested/AssignNumber.cshtml",
                        playerHelper.BuildAssignNumberViewModel(model));
            }

            if (model.PreRegistered)
            {
                if (model.UserId == null)
                {
                    ModelState.AddModelError("UserId",
                        "If player has pre-registered, use search to select.");

                    return View("~/Views/Player/Interested/AssignNumber.cshtml",
                        playerHelper.BuildAssignNumberViewModel(model));
                }

                if (playerHelper.PlayerAlreadyAssignedNumber((int) model.UserId, model.DateOfAssignment))
                {
                    ModelState.AddModelError("AssignedNumber",
                        "Number Assignment failed, this user has already been assigned a number, if this " +
                        "player has been assigned a number in error please contact the administrator.");

                    return View("~/Views/Player/Interested/AssignNumber.cshtml",
                        playerHelper.BuildAssignNumberViewModel(model));
                }
            }
            else
            {
                if (model.FirstName == null || model.LastName == null || model.EmailAddress == null
                    || model.SelectedGender == null || model.SelectedDay == null || model.SelectedMonth == null
                    || model.SelectedYear == null || model.EmailAddress == null || model.PhoneNumber == null
                    || model.PlayedBeforeAnswer == null || model.SelectedPosition == null)
                {
                    ModelState.AddModelError("", "You have failed to fill a required field.");

                    return View("~/Views/Player/Interested/AssignNumber.cshtml",
                        playerHelper.BuildAssignNumberViewModel(model));
                }

                if (playerHelper.NumberOfUsersWithGivenEmail(model.EmailAddress) > 0)
                {
                    ModelState.AddModelError("EmailAddress",
                        "An expression of interest with that email address already exists");

                    return View("~/Views/Player/Interested/AssignNumber.cshtml",
                        playerHelper.BuildAssignNumberViewModel(model));
                }

                var date = new DateTime();

                if (!playerHelper.ParseDate(model.SelectedDay, model.SelectedMonth, model.SelectedYear, out date))
                {
                    ModelState.AddModelError("DateOfBirth", "You have entered an invalid date.");

                    return View("~/Views/Player/Interested/AssignNumber.cshtml",
                        playerHelper.BuildAssignNumberViewModel(model));
                }

                model.DateOfBirth = date;

                var userId = memberHelper.CreateNewMemberIdentity(new MemberIdentityDataModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.SelectedGender,
                    Email = model.EmailAddress,
                    Phone = model.PhoneNumber
                });

                model.UserId = userId;

                playerHelper.CreateNewExpressionOfInterest(new ExpressInterestViewModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth,
                    SelectedGender = model.SelectedGender,
                    SelectedDay = model.SelectedDay,
                    SelectedMonth = model.SelectedMonth,
                    SelectedYear = model.SelectedYear,
                    PhoneNumber = model.PhoneNumber,
                    EmailAddress = model.EmailAddress,
                    PlayedBeforeAnswer = model.PlayedBeforeAnswer,
                    SelectedPosition = model.SelectedPosition,
                }, userId);
            }

            playerHelper.CreateNewNumberAssignment(model);
            
            return View("~/Views/Player/Interested/AssignNumber.cshtml",
                playerHelper.BuildAssignNumberViewModel(
                    new AssignNumberViewModel { Submitted = true }));
        }
        
        [AllowAnonymous]
        public JsonResult SearchByLastName(string term)
        {
            if (term == null || term.Length < 2)
            {
                return Json(new SelectItemsViewModel
                {
                    results = new SelectItemsViewModel.SelectResultViewModel[] { }
                }, JsonRequestBehavior.AllowGet);
            }

            var search = playerHelper.SearchByLastName(term);
            
            var result = new SelectItemsViewModel
            {
                results = search.Select(user =>
                    new SelectItemsViewModel.SelectResultViewModel
                    {
                        id = user.MemberIdentityId,
                        text = user.FirstName + " " + user.LastName
                    }).ToArray()
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}