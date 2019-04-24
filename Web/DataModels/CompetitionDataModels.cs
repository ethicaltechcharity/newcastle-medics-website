using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.DataModels
{
    public class CompetitionDataModel
    {
        [Key]
        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }

        public virtual IEnumerable<DivisionDataModel> Divisions { get; set; }

        public virtual IEnumerable<OrganisationRoleDataModel> RoleHolders { get; set; }
    }

    public class DivisionDataModel
    {
        [Key]
        public int DivisionId { get; set; }

        public string DivisionName { get; set; }

        public int CompetitionId { get; set; }
        public virtual CompetitionDataModel Competition { get; set; }

        public virtual IEnumerable<SquadDataModel> Squads { get; set; }

        public virtual IEnumerable<PlannedFixtureDataModel> Fixtures { get; set; }

        public virtual IEnumerable<OrganisationRoleDataModel> RoleHolders { get; set; }
    }

    public class PlannedFixtureDataModel
    {
        [Key]
        public int FixtureId { get; set; }

        public int SquadAId { get; set; }
        public virtual SquadDataModel SquadA { get; set; }

        public int SquadBId { get; set; }
        public virtual SquadDataModel SquadB { get; set; }

        public int DivisionId { get; set; }
        public virtual DivisionDataModel Division { get; set; }

        public int CompetitionId { get; set; }
        public virtual CompetitionDataModel Competition { get; set; }

        public virtual IEnumerable<OrganisationRoleDataModel> RoleHolders { get; set; }

        public int MatchRecordId { get; set; }
        public virtual MatchRecordDataModel MatchRecord { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime PushbackTime { get; set; }
    }

    public class MatchRecordDataModel
    {
        [Key]
        public int MatchRecordId { get; set; }

        public int TeamAId { get; set; }
        public virtual TeamDataModel TeamA { get; set; }

        public int TeamBId { get; set; }
        public virtual TeamDataModel TeamB { get; set; }

        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }

        public IEnumerable<MatchEventDataModel> MatchEvents { get; set; }

        public string Outcome { get; set; }
    }

    public class MatchEventDataModel
    {
        [Key]
        public int MatchEventId { get; set; }

        public string Name { get; set; }

        public TimeSpan Time { get; set; }

        public string Description { get; set; }
    }

    public class VenueDataModel
    {
        [Key]
        public int VenueId { get; set; }

        public string VenueName { get; set; }
    }
}