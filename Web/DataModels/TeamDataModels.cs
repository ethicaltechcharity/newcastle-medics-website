using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.DataModels
{
    public class ClubDataModel
    {
        [Key]
        public int ClubId { get; set; }

        public string ClubName { get; set; }

        public virtual IEnumerable<OrganisationRoleDataModel> RoleHolders { get; set; }

        public virtual IEnumerable<SquadDataModel> Squads { get; set; }
    }
    
    public class SquadDataModel
    {
        [Key]
        public int SquadId { get; set; }

        public string SquadName { get; set; }

        public int ClubId { get; set; }
        public virtual ClubDataModel Club { get; set; }

        public virtual IEnumerable<MemberIdentityDataModel> Players { get; set; }

        public virtual IEnumerable<OrganisationRoleDataModel> RoleHolders { get; set; }
    }

    public class TeamDataModel
    {
        [Key]
        public int TeamId { get; set; }

        public int SquadId { get; set; }
        public virtual SquadDataModel Squad { get; set; }

        public virtual IEnumerable<MemberIdentityDataModel> Players { get; set; }

        public virtual IEnumerable<OrganisationRoleDataModel> RoleHolders { get; set; }
    }
    
}