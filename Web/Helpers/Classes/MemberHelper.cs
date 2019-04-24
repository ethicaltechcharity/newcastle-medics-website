using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using Web.DataModels;
using Web.Helpers.Interfaces;
using Web.Models;

namespace Web.Helpers.Classes
{
    public class MemberHelper : IMemberHelper
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPlayerHelper playerHelper;

        public MemberHelper(ApplicationDbContext dbContext, IPlayerHelper playerHelper)
        {
            this.dbContext = dbContext;
            this.playerHelper = playerHelper;
        }

        public MemberIndexViewModel BuildMemberIndexViewModel(MemberIndexViewModel model)
        {
            model = model ?? new MemberIndexViewModel();

            if (model.Members == null)
            {
                model.Members = new List<MemberSummaryViewModel>();
            }
            
            var filteredMembers = new List<MemberIdentityDataModel>();

            foreach (var member in dbContext.MemberIdentities)
            {
                if (
                    FilterName(member, model.NameFilter)
                    && FilterGender(member, model.GenderFilter)
                    && FilterRole(member, model.RoleFilter))
                {
                    filteredMembers.Add(member);
                }
            }

            model.Roles = BuildRoleList();
            model.Genders = BuildGendersList();
            model.TotalResults = filteredMembers.Count;

            int rem;
            model.PageCount = Math.DivRem(filteredMembers.Count, 15, out rem);
            if (rem != 0)
            {
                model.PageCount++;
            }

            filteredMembers = filteredMembers
                .Skip((model.PageNumber - 1) * 10)
                .Take(10).ToList();

            foreach (var member in filteredMembers)
            {
                var roleString = string.Join(",", 
                    member.Roles.OrderBy(r => r.Name).Select(r => r.Name));

                model.Members.Add(new MemberSummaryViewModel()
                {
                    Name = member.FirstName + " " + member.LastName,
                    Gender = member.Gender,
                    Roles = roleString
                });
            }
            
            return model;
        }

        public MemberDetailsViewModel BuildMemberDetailsViewModel(int memberId)
        {
            var model = new MemberDetailsViewModel();

            var memberDataModel = dbContext.MemberIdentities.Find(memberId);

            model.Name = memberDataModel.FirstName + " " + memberDataModel.LastName;
            model.Gender = memberDataModel.Gender;
            model.Email = memberDataModel.Email;
            model.Phone = memberDataModel.Phone;
            model.DateOfBirth = memberDataModel.DateOfBirth;

            var expressions = dbContext.ExpressionsOfInterest
                .Where(eoi => eoi.IdentityId == memberId);

            if (expressions.Count() > 0)
            {
                var expression = expressions.First();

                model.PlayedBefore = expression.PlayedBefore;
                model.Position = expression.Position;
            }

            return model;
        }

        public MemberRegistrationViewModel BuildMemberRegistrationViewModel(MemberRegistrationViewModel model, bool submitted)
        {
            if (submitted)
            {
                switch (model.SelectedMemberType)
                {
                    case "fresher":
                        model.SubscriptionFee = "£85.00";
                        break;
                    case "student":
                        model.SubscriptionFee = "£100.00";
                        break;
                    case "employed":
                        model.SubscriptionFee = "£125.00";
                        break;
                    default:
                        throw new Exception();
                }

                model.PaymentReference = generatePaymentReference(
                    model.SelectedCurrentTeam, model.FirstName, model.LastName);
            }
            else
            {
                model = model ?? new MemberRegistrationViewModel
                {
                    PreRegistered = true,
                    PlayedBefore = true,
                    PlayedMedics = true,
                    InterestedInUmpiring = true,
                    UmpireQualified = true,
                    KnowsUmpireNumber = true
                };

                model.Days = playerHelper.BuildDaysList();
                model.Months = playerHelper.BuildMonthsList();
                model.Years = playerHelper.BuildYearsList();
                model.Genders = playerHelper.BuildGendersList();
                model.PreviousTeams = BuildTeamsList();
                model.CurrentTeams = new List<SelectListItem>
            {
                new SelectListItem { Text = "Unknown", Value = "unknown" },
                new SelectListItem { Text = "Men's 1s", Value = "mens1s" },
                new SelectListItem { Text = "Men's 2s", Value = "mens2s" },
                new SelectListItem { Text = "Men's 3s", Value = "mens3s" },
                new SelectListItem { Text = "Men's 4s", Value = "mens4s" },
                new SelectListItem { Text = "Ladies' 1s", Value = "ladies1s" },
                new SelectListItem { Text = "Ladies' 2s", Value = "ladies2s" },
                new SelectListItem { Text = "Ladies' 3s", Value = "ladies3s" },
                new SelectListItem { Text = "Ladies' 4s", Value = "ladies4s" },
            };
                model.MemberTypes = new List<SelectListItem>
            {
                new SelectListItem { Text = "New Member", Value = "fresher" },
                new SelectListItem { Text = "Returning Student Member", Value = "student" },
                new SelectListItem { Text = "Returning Employed Member", Value = "employed" },
            };
                model.UmpiringQualifications = BuildUmpiringQualificationsList();
            }
            
            
            return model;
        }

        public int CreateNewMemberIdentity(MemberIdentityDataModel dataModel)
        {
            dbContext.MemberIdentities.Add(dataModel);
            dbContext.SaveChanges();

            return dataModel.MemberIdentityId;
        }

        public MemberRegistrationViewModel CreateNewRegistration(MemberRegistrationViewModel viewModel)
        {
            var registrationsDataModel = new MemberRegistrationDataModel
            {
                Season = "2018/2019",
                DateOfRegistration = DateTime.Now,
                RegistrationType = viewModel.SelectedMemberType,
                ApparentSquad = viewModel.SelectedCurrentTeam,
            };

            int identityId;

            if (viewModel.PreRegistered)
            {
                var identity = dbContext.MemberIdentities.Where(id => 
                    id.Email == viewModel.EmailAddress.ToLower()
                    && DbFunctions.TruncateTime(id.DateOfBirth) == viewModel.DateOfBirth.Date);

                if (identity.Count() != 1)
                {
                    throw new Exception();
                }

                var firstIdentity = identity.First();

                identityId = firstIdentity.MemberIdentityId;

                viewModel.FirstName = firstIdentity.FirstName;
                viewModel.LastName = firstIdentity.LastName;
            }
            else
            {
                identityId = CreateNewMemberIdentity(new MemberIdentityDataModel
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    DateOfBirth = viewModel.DateOfBirth,
                    Gender = viewModel.SelectedGender.ToLower(),
                    Email = viewModel.EmailAddress.ToLower(),
                    Phone = viewModel.PhoneNumber
                });
            }

            CreateMemberLegalRecord(viewModel, identityId);

            if (viewModel.PlayedBefore)
            {
                playerHelper.CreateNewPlayingHistory(viewModel, identityId);

                if (viewModel.PlayedMedics)
                {
                    playerHelper.CreateNewPlayingShirtRecord((int) viewModel.ShirtNumber, identityId);
                }
            }

            if (viewModel.InterestedInUmpiring)
            {
                CreateUmpireDataModel(viewModel, identityId);
            }

            registrationsDataModel.IdentityId = identityId;

            dbContext.Registrations.Add(registrationsDataModel);
            dbContext.SaveChanges();

            return viewModel;
        }

        public void CreateMemberLegalRecord(MemberRegistrationViewModel viewModel, int identityId)
        {
            var legalRecord = new MemberLegalDataModel
            {
                DataConsent = viewModel.DataConsent,
                AgreesCodeOfConduct = viewModel.AgreesCodeOfConduct,
                DateOfConsent = DateTime.Now,
                IdentityId = identityId
            };

            dbContext.Consents.Add(legalRecord);
            dbContext.SaveChanges();
        }

        public void CreateUmpireDataModel(MemberRegistrationViewModel viewModel, int identityId)
        {
            var umpireDataModel = new UmpireDataModel
            {
                UmpiringLevel = viewModel.SelectedUmpiringQualification,
                UmpiringNumber = viewModel.UmpireNumber,
                IdentityId = identityId
            };

            dbContext.Umpires.Add(umpireDataModel);
            dbContext.SaveChanges();
        }

        public MemberRoleDataModel CreateNewMemberRoleIfNotExist(string name)
        {
            var model = dbContext.MemberRoles.FirstOrDefault(r => r.Name == name);

            if (model == null)
            {
                model = new MemberRoleDataModel()
                {
                    Name = name
                };

                dbContext.MemberRoles.Add(model);
                dbContext.SaveChanges();
            }
            
            return model;
        }

        public List<KeyValuePair<string, string>> ValidateMemberRegistrationViewModel(MemberRegistrationViewModel model)
        {
            var errors = new List<KeyValuePair<string, string>>();
            
            if (!model.AgreesCodeOfConduct)
            {
                errors.Add(new KeyValuePair<string, string>
                (
                    "AgreesCodeOfConduct",
                    "You must agree to our code of conduct."
                ));
            }

            if (!model.DataConsent)
            {
                errors.Add(new KeyValuePair<string, string>
                (
                    "DataConsent",
                    "You must give consent for us to process your data."
                ));
            }

            if (string.IsNullOrWhiteSpace(model.EmailAddress))
            {
                errors.Add(new KeyValuePair<string, string>
                (
                    "EmailAddress",
                    "You must provide your email address."
                ));

            }

            if (string.IsNullOrWhiteSpace(model.SelectedCurrentTeam))
            {
                errors.Add(new KeyValuePair<string, string>
                (
                    "SelectedCurrentTeam",
                    "You must provide the squad you've been selected for."
                ));
            }

            if (string.IsNullOrWhiteSpace(model.SelectedMemberType))
            {
                errors.Add(new KeyValuePair<string, string>
                (
                    "SelectedMemberType",
                    "You must provide the type of member you will be."
                ));
            }

            if (string.IsNullOrWhiteSpace(model.SelectedDay)
                || string.IsNullOrWhiteSpace(model.SelectedMonth)
                || string.IsNullOrWhiteSpace(model.SelectedYear))
            {
                errors.Add(new KeyValuePair<string, string>
                (
                    "DateOfBirth",
                    "You must provide your date of birth."
                ));
            }

            if (!model.PreRegistered)
            {
                if (!string.IsNullOrWhiteSpace(model.EmailAddress))
                {
                    if (playerHelper.NumberOfUsersWithGivenEmail(model.EmailAddress) > 0)
                    {
                        errors.Add(new KeyValuePair<string, string>
                        (
                            "EmailAddress",
                            "A registration with that email address already exists"
                        ));
                    }
                }
                

                if (string.IsNullOrWhiteSpace(model.FirstName))
                {
                    errors.Add(new KeyValuePair<string, string>
                    (
                        "FirstName",
                        "You must provide your first name."
                    ));
                }
                
                if (string.IsNullOrWhiteSpace(model.SelectedGender))
                {
                    errors.Add(new KeyValuePair<string, string>
                    (
                        "SelectedGender",
                        "You must provide your gender."
                    ));
                }

                if (string.IsNullOrWhiteSpace(model.PhoneNumber))
                {
                    errors.Add(new KeyValuePair<string, string>
                    (
                        "PhoneNumber",
                        "You must provide your phone number."
                    ));
                }

                if (string.IsNullOrWhiteSpace(model.LastName))
                {
                    errors.Add(new KeyValuePair<string, string>
                    (
                        "LastName",
                        "You must provide your last name."
                    ));
                }
            }

            if (model.PlayedBefore)
            {
                if (model.PlayedMedics)
                {
                    if (string.IsNullOrWhiteSpace(model.SelectedPreviousTeam))
                    {
                        errors.Add(new KeyValuePair<string, string>
                        (
                            "SelectedTeam",
                            "You must provide your previous team."
                        ));
                    }

                    if (model.ShirtNumber == null)
                    {
                        errors.Add(new KeyValuePair<string, string>
                        (
                            "ShirtNumber",
                            "You must provide your shirt number."
                        ));
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(model.PreviousClub))
                    {
                        errors.Add(new KeyValuePair<string, string>
                        (
                            "PreviousClub",
                            "You must provide your previous club."
                        ));
                    }

                    if (string.IsNullOrWhiteSpace(model.PreviousTeam))
                    {
                        errors.Add(new KeyValuePair<string, string>
                       (
                           "PreviousTeam",
                           "You must provide your previous team."
                       ));
                    }
                }
            }

            if (model.InterestedInUmpiring)
            {
                if (model.UmpireQualified)
                {
                    if (string.IsNullOrWhiteSpace(model.SelectedUmpiringQualification))
                    {
                        errors.Add(new KeyValuePair<string, string>
                        (
                            "SelectedUmpiringQualification",
                            "You must provide your umpiring qualification."
                        ));
                    }

                    if (model.KnowsUmpireNumber)
                    {
                        if (string.IsNullOrWhiteSpace(model.UmpireNumber))
                        {
                            errors.Add(new KeyValuePair<string, string>
                            (
                                "UmpireNumber",
                                "You must provide your umpire number."
                            ));
                        }
                    }
                }
            }

            if (errors.Any())
            {
                return errors;
            }

            if (model.PreRegistered)
            {
                var identity = dbContext.MemberIdentities.Where(id =>
                    id.Email == model.EmailAddress.ToLower()
                    && DbFunctions.TruncateTime(id.DateOfBirth) == model.DateOfBirth.Date);

                if (identity.Count() == 0)
                {
                    errors.Add(new KeyValuePair<string, string>
                    (
                        "EmailAddress",
                        "Existing user with your email address and date of birth not found, please check your spelling"
                    ));
                }
                else if (identity.Count() > 1)
                {
                    throw new Exception();
                }
            }

            return errors;
        }

        #region private

        private bool FilterName(MemberIdentityDataModel model, string nameFilter)
        {
            if (nameFilter != null && nameFilter != "")
            {
                return model.FirstName.StartsWith(nameFilter) || model.LastName.StartsWith(nameFilter);
            }
            else
            {
                return true;
            }
        }

        private bool FilterGender(MemberIdentityDataModel model, string genderFilter)
        {
            if (genderFilter != null && genderFilter != "")
            {
                return model.Gender == genderFilter;
            }
            else
            {
                return true;
            }
        }

        private bool FilterRole(MemberIdentityDataModel model, string roleFilter)
        {
            if (roleFilter != null && roleFilter != "")
            {
                return model.Roles.Where(r => r.Name == roleFilter.ToLower()).Any();
            }
            else
            {
                return true;
            }
        }

        private List<SelectListItem> BuildGendersList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Female", Value = "female" },
                new SelectListItem { Text = "Male", Value = "male" }
            };
        }

        private List<SelectListItem> BuildRoleList()
        {
            var list = new List<SelectListItem>();
            var roles = dbContext.MemberRoles;

            foreach (var role in roles)
            {
                list.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Name
                });
            }

            return list;
        }

        private List<SelectListItem> BuildTeamsList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Men's 1s", Value = "mens1s" },
                new SelectListItem { Text = "Men's 2s", Value = "mens2s" },
                new SelectListItem { Text = "Men's 3s", Value = "mens3s" },
                new SelectListItem { Text = "Ladies' 1s", Value = "ladies1s" },
                new SelectListItem { Text = "Ladies' 2s", Value = "ladies2s" },
                new SelectListItem { Text = "Ladies' 3s", Value = "ladies3s" },
            };
        }

        private List<SelectListItem> BuildUmpiringQualificationsList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Level 1 Unassessed", Value = "1u" },
                new SelectListItem { Text = "Level 1 Assessed", Value = "1a" },
                new SelectListItem { Text = "Level 2", Value = "2" },
                new SelectListItem { Text = "Level 3", Value = "3" },
            };
        }

        private string generatePaymentReference(string team, string firstName, string lastName)
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch(team)
            {
                case "mens1s":
                    stringBuilder.Append("M1");
                    break;
                case "mens2s":
                    stringBuilder.Append("M2");
                    break;
                case "mens3s":
                    stringBuilder.Append("M3");
                    break;
                case "mens4s":
                    stringBuilder.Append("M4");
                    break;
                case "ladies1s":
                    stringBuilder.Append("L1");
                    break;
                case "ladies2s":
                    stringBuilder.Append("L2");
                    break;
                case "ladies3s":
                    stringBuilder.Append("L3");
                    break;
                case "ladies4s":
                    stringBuilder.Append("L4");
                    break;
                case "unknown":
                    stringBuilder.Append("U");
                    break;
                default:
                    throw new Exception();
            }

            stringBuilder.Append(firstName.Substring(0,1));

            if (lastName.Length > 10)
            {
                stringBuilder.Append(lastName.Substring(0, 10));
            }
            else
            {
                stringBuilder.Append(lastName);
            }

            return stringBuilder.ToString().ToUpper();
        }

        #endregion
    }
}