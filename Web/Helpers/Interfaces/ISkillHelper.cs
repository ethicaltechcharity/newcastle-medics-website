using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Helpers.Interfaces
{
    public interface ISkillHelper
    {
        TrialAssessmentViewModel BuildTrialAssessmentViewModel(TrialAssessmentViewModel model);

        ViewAssessmentViewModel BuildViewAssessmentViewModel(ViewAssessmentViewModel model);
    }
}
