using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.DataModels;
using Web.Helpers.Interfaces;
using Web.Models;
using X.PagedList;

namespace Web.Helpers.Classes
{
    public class PlayerHelper : IPlayerHelper
    {
        private readonly ApplicationDbContext dbContext;

        #region main
        
        public PlayerHelper(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ExpressInterestViewModel BuildExpressInterestModel(ExpressInterestViewModel model)
        {
            model = model ?? new ExpressInterestViewModel();

            model.Positions = BuildPositionsList();
            model.Genders = BuildGendersList();
            model.HaveYouPlayedBefore = BuildYesNoList();
            model.Days = BuildDaysList();
            model.Months = BuildMonthsList();
            model.Years = BuildYearsList();

            return model;
        }

        public AssignNumberViewModel BuildAssignNumberViewModel(AssignNumberViewModel model)
        {
            model = model ?? new AssignNumberViewModel();

            model.Positions = BuildPositionsList();
            model.Genders = BuildGendersList();
            model.HaveYouPlayedBefore = BuildYesNoList();
            model.Days = BuildDaysList();
            model.Months = BuildMonthsList();
            model.Years = BuildYearsList();
            model.DateOfAssignment = DateTime.Now;

            return model;
        }

        public ExpressInterestViewModel BuildExpressInterestModel(int id)
        {
            ExpressInterestDataModel dataModel = dbContext
                .ExpressionsOfInterest.Where(i => i.ExpressInterestId == id).First();

            var viewModel = new ExpressInterestViewModel();

            viewModel = BuildExpressInterestModel(viewModel);
            
            viewModel.FirstName = dataModel.Identity.FirstName;
            viewModel.LastName = dataModel.Identity.LastName;
            viewModel.SelectedGender = dataModel.Identity.Gender;
            viewModel.DateOfBirth = dataModel.Identity.DateOfBirth;
            viewModel.SelectedDay = dataModel.Identity.DateOfBirth.Day.ToString();
            viewModel.SelectedMonth = dataModel.Identity.DateOfBirth.Month.ToString();
            viewModel.SelectedYear = dataModel.Identity.DateOfBirth.Year.ToString();
            viewModel.PhoneNumber = dataModel.Identity.Phone;
            viewModel.EmailAddress = dataModel.Identity.Email;
            viewModel.PlayedBeforeAnswer = dataModel.PlayedBefore;
            viewModel.SelectedPosition = dataModel.Position;

            return viewModel;
        }

        public IEnumerable<InterestedViewModel> BuildInterestedList(int pageNumber, int pageSize)
        {
            var list = new List<InterestedViewModel>();

            var dataModelList = dbContext.ExpressionsOfInterest
                .OrderBy(i => i.ExpressInterestId).ToList();
            
            foreach (var dataModel in dataModelList)
            {
                list.Add(new InterestedViewModel
                {
                    Id = dataModel.ExpressInterestId,
                    FirstName = dataModel.Identity.FirstName,
                    LastName = dataModel.Identity.LastName,
                    DateOfBirth = dataModel.Identity.DateOfBirth,
                    Gender = dataModel.Identity.Gender,
                    Email = dataModel.Identity.Email,
                    Phone = dataModel.Identity.Phone,
                    PlayedBefore = dataModel.PlayedBefore,
                    Position = dataModel.Position
                });
            }

            return list.ToPagedList(pageNumber, pageSize);
        }
        
        public int NumberOfUsersWithGivenEmail(string email)
        {
            return dbContext.MemberIdentities
                .Count(i => i.Email.Equals(email.ToLower()));
        }

        public MemberIdentityDataModel UserWithGivenId(int id)
        {
            var user = new MemberIdentityDataModel()
            {
                MemberIdentityId = id
            };

            return dbContext.MemberIdentities.Find(user);
        }

        public void CreateNewExpressionOfInterest(ExpressInterestViewModel viewModel, int userId)
        {
            var expressionOfInterest = new ExpressInterestDataModel
            {
                PlayedBefore = (bool) viewModel.PlayedBeforeAnswer,
                Position = viewModel.SelectedPosition,
                IdentityId = userId
            };

            dbContext.ExpressionsOfInterest.Add(expressionOfInterest);
            dbContext.SaveChanges();
        }

        public void CreateNewPlayingHistory(MemberRegistrationViewModel viewModel, int identityId)
        {
            var playingHistoryModel = new PlayingHistoryDataModel
            {
                IdentityId = identityId,
                NewPlayer = !viewModel.PlayedMedics,
                TimeFrame = "Prior 2018/19"
            };

            if (viewModel.PlayedMedics)
            {
                playingHistoryModel.Team = viewModel.SelectedPreviousTeam;
                playingHistoryModel.Club = "Newcastle Medics";
            }
            else
            {
                playingHistoryModel.Team = viewModel.PreviousTeam;
                playingHistoryModel.Club = viewModel.PreviousClub;
            }

            dbContext.PlayingHistories.Add(playingHistoryModel);
            dbContext.SaveChanges();
        }

        public void CreateNewPlayingShirtRecord(int playingShirtNumber, int identityId)
        {
            var shirtModel = new PlayingShirtDataModel
            {
                PlayingShirtNumber = playingShirtNumber,
                IdentityId = identityId
            };

            dbContext.PlayingShirts.Add(shirtModel);
            dbContext.SaveChanges();
        }

        public int CreateNewNumberAssignment(AssignNumberViewModel viewModel)
        {
            var numberAssignment = new NumberAssignmentDataModel
            {
                IdentityId = (int) viewModel.UserId,
                AssignedNumber = viewModel.AssignedNumber,
                DateOfAssignment = viewModel.DateOfAssignment
            };

            dbContext.NumberAssignments.Add(numberAssignment);
            dbContext.SaveChanges();

            return numberAssignment.NumberAssignmentId;
        }

        public void EditExpressionOfInterest(int id, ExpressInterestViewModel viewModel)
        {
            ExpressInterestDataModel dataModel = dbContext.ExpressionsOfInterest.Find(id);

            if (dataModel == null)
            {
                throw new Exception();
            }
            else
            {
                dataModel.Identity.FirstName = viewModel.FirstName;
                dataModel.Identity.LastName = viewModel.LastName;
                dataModel.Identity.DateOfBirth = viewModel.DateOfBirth;
                dataModel.Identity.Gender = viewModel.SelectedGender;
                dataModel.Identity.Email = viewModel.EmailAddress.ToLower();
                dataModel.Identity.Phone = viewModel.PhoneNumber;
                dataModel.PlayedBefore = (bool) viewModel.PlayedBeforeAnswer;
                dataModel.Position = viewModel.SelectedPosition;

                dbContext.SaveChanges();
            }
        }

        public void DeleteExpressionOfInterest(int id)
        {
            ExpressInterestDataModel model = dbContext.ExpressionsOfInterest.Find(id);

            if (model != null)
            {
                if (model.Identity != null)
                {
                    dbContext.MemberIdentities.Remove(model.Identity);
                }
                dbContext.ExpressionsOfInterest.Remove(model);
                dbContext.SaveChanges();
            }
        }

        public bool ParseDate(string day, string month, string year, out DateTime date)
        {
            date = new DateTime();

            if (
                !int.TryParse(day, out int parsedDay) ||
                !int.TryParse(month, out int parsedMonth) ||
                !int.TryParse(year, out int parsedYear))
            {
                return false;
            }

            if (parsedDay > DateTime.DaysInMonth(parsedYear, parsedMonth))
            {
                return false;
            }

            date = new DateTime(parsedYear, parsedMonth, parsedDay);

            return true;
        }

        public IQueryable<MemberIdentityDataModel> SearchByLastName(string name)
        {
            return dbContext.MemberIdentities.Where(i => i.LastName.StartsWith(name));
        }

        public bool NumberAlreadyAssigned(int assignedNumber, DateTime date)
        {
            return dbContext.NumberAssignments.Any(n =>
                n.AssignedNumber == assignedNumber && 
                DateTime.Compare(n.DateOfAssignment, date) == 0
                );
        }

        public bool PlayerAlreadyAssignedNumber(int userId, DateTime date)
        {
            return dbContext.NumberAssignments.Any(n =>
                n.IdentityId == userId &&
                DateTime.Compare(n.DateOfAssignment, date) == 0
                );
        }

        #endregion

        #region auxiliary

        public List<SelectListItem> BuildMonthsList()
        {
            var months = new List<SelectListItem>();

            var monthText = new String[] {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December" };

            for (int i = 1; i <= 12; i++)
            {
                months.Add(new SelectListItem
                {
                    Text = i.ToString() + " - " + monthText[i - 1],
                    Value = i.ToString()
                });
            }

            return months;
        }

        public List<SelectListItem> BuildDaysList()
        {
            var days = new List<SelectListItem>();

            for (int i = 1; i <= 31; i++)
            {
                days.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            return days;
        }

        public List<SelectListItem> BuildYearsList()
        {
            var years = new List<SelectListItem>();

            var currentDate = DateTime.Now;

            for (int i = currentDate.Year - 13; i > currentDate.Year - 125; i--)
            {
                years.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            return years;
        }

        public List<SelectListItem> BuildPositionsList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Goalkeeper", Value = "goalkeeper" },
                new SelectListItem { Text = "Defence", Value = "defence" },
                new SelectListItem { Text = "Midfield", Value = "midfield" },
                new SelectListItem { Text = "Attack", Value = "attack" },
                new SelectListItem { Text = "Anywhere", Value = "anywhere" },
                new SelectListItem { Text = "Unsure", Value = "unsure" }
            };
        }

        public List<SelectListItem> BuildGendersList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Female", Value = "female" },
                new SelectListItem { Text = "Male", Value = "male" }
            };
        }

        public List<SelectListItem> BuildYesNoList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Yes", Value = "true" },
                new SelectListItem { Text = "No", Value = "false" }
            };
        }

        public List<SelectListItem> BuildDrillList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Shooting", Value = "shooting" },
                new SelectListItem { Text = "Tackling", Value = "tackling" },
                new SelectListItem { Text = "Dribbling", Value = "dribbling" },
                new SelectListItem { Text = "5v5 Match", Value = "5v5 match" },
                new SelectListItem { Text = "Long Passing", Value = "long passing" },
                new SelectListItem { Text = "Short Passing", Value = "short passing" },
            };
        }

        public List<SelectListItem> BuildRatingList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "1 - Beginner", Value = "1" },
                new SelectListItem { Text = "2 - Poor", Value = "2" },
                new SelectListItem { Text = "3 - Intermediate", Value = "3" },
                new SelectListItem { Text = "4 - Good", Value = "4" },
                new SelectListItem { Text = "5 - Excellent", Value = "5" },
            };
        }

        #endregion
    }
}