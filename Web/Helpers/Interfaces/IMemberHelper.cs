using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Web.DataModels;
using Web.Models;

namespace Web.Helpers.Interfaces
{
    public interface IMemberHelper
    {
        MemberIndexViewModel BuildMemberIndexViewModel(MemberIndexViewModel model);

        MemberDetailsViewModel BuildMemberDetailsViewModel(int memberId);

        MemberRegistrationViewModel BuildMemberRegistrationViewModel(MemberRegistrationViewModel model, bool submitted);

        int CreateNewMemberIdentity(MemberIdentityDataModel dataModel);

        MemberRegistrationViewModel CreateNewRegistration(MemberRegistrationViewModel viewModel);

        void CreateMemberLegalRecord(MemberRegistrationViewModel viewModel, int identityId);
        
        void CreateUmpireDataModel(MemberRegistrationViewModel viewModel, int identityId);
            
        MemberRoleDataModel CreateNewMemberRoleIfNotExist(string name);

        List<KeyValuePair<string, string>> ValidateMemberRegistrationViewModel(MemberRegistrationViewModel model);
    }
}
