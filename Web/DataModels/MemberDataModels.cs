using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.DataModels
{
    public class MemberIdentityDataModel
    {
        [Key]
        public int MemberIdentityId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<MemberRoleDataModel> Roles { get; set; }
    }

    public class MemberRoleDataModel
    {
        [Key]
        public int MemberRoleId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<MemberIdentityDataModel> Members { get; set; }
    }

    public class MemberRegistrationDataModel
    {
        [Key]
        public int MemberRegistrationId { get; set; }

        public string Season { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public bool PaidSubscription { get; set; }

        public string RegistrationType { get; set; }

        public string ApparentSquad { get; set; }

        [ForeignKey("Identity")]
        public int IdentityId { get; set; }
        public virtual MemberIdentityDataModel Identity { get; set; }
    }

    public class MemberLegalDataModel
    {
        [Key]
        public int MemberLegalId { get; set; }

        public bool DataConsent { get; set; }

        public bool AgreesCodeOfConduct { get; set; }

        public DateTime DateOfConsent { get; set; }

        [ForeignKey("Identity")]
        public int IdentityId { get; set; }
        public virtual MemberIdentityDataModel Identity { get; set; }
    }

    public class UmpireDataModel
    {
        [Key]
        public int UmpireId { get; set; }

        public string UmpiringLevel { get; set; }

        public string UmpiringNumber{ get; set; }

        [ForeignKey("Identity")]
        public int IdentityId { get; set; }
        public virtual MemberIdentityDataModel Identity { get; set; }
    }

    public class OrganisationRoleDataModel
    {
        public int OrganisationRoleId { get; set; }

        public int OrganisationRoleTypeId { get; set; }
        public virtual OrganisationRoleTypeDataModel RoleType { get; set; }

        public int MemberId { get; set; }
        public virtual MemberIdentityDataModel Member { get; set; }
    }

    public class OrganisationRoleTypeDataModel
    {
        public int OrganisationRoleTypeId { get; set; }

        public string Name { get; set; }
    }
}