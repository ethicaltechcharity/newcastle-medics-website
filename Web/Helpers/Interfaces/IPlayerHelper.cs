using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.DataModels;
using Web.Models;

namespace Web.Helpers.Interfaces
{
    public interface IPlayerHelper
    {
        ExpressInterestViewModel BuildExpressInterestModel(ExpressInterestViewModel model);

        AssignNumberViewModel BuildAssignNumberViewModel(AssignNumberViewModel model);

        ExpressInterestViewModel BuildExpressInterestModel(int id);
        
        IEnumerable<InterestedViewModel> BuildInterestedList(int pageNumber, int pageSize);

        int NumberOfUsersWithGivenEmail(string email);

        void CreateNewExpressionOfInterest(ExpressInterestViewModel viewModel, int userId);

        void CreateNewPlayingHistory(MemberRegistrationViewModel viewModel, int identityId);

        void CreateNewPlayingShirtRecord(int playingShirtNumber, int identityId);

        void EditExpressionOfInterest(int id, ExpressInterestViewModel viewModel);

        void DeleteExpressionOfInterest(int id);

        bool ParseDate(string day, string month, string year, out DateTime date);

        IQueryable<MemberIdentityDataModel> SearchByLastName(string name);

        int CreateNewNumberAssignment(AssignNumberViewModel viewModel);

        bool NumberAlreadyAssigned(int assignedNumber, DateTime date);

        bool PlayerAlreadyAssignedNumber(int userId, DateTime date);

        List<SelectListItem> BuildMonthsList();

        List<SelectListItem> BuildDaysList();

        List<SelectListItem> BuildYearsList();

        List<SelectListItem> BuildPositionsList();

        List<SelectListItem> BuildGendersList();

        List<SelectListItem> BuildYesNoList();

        List<SelectListItem> BuildDrillList();

        List<SelectListItem> BuildRatingList();
    }
}
