using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using DCT.ILR.Model;

namespace BusinessRules.POC.SharedRules.DD28
{
    public interface IDD28PickMatchingEmpRecord : IShortRule<DD28SubModel, MessageLearnerLearnerEmploymentStatus>
    {
    }

    public class DD28PickMatchingEmpRecord : IDD28PickMatchingEmpRecord
    {

        public MessageLearnerLearnerEmploymentStatus Evaluate(DD28SubModel dd28SubModel)
        {
            //pick the emp record that is in the range of learningdelivery plan startdate
            var result = dd28SubModel.LearnerEmploymentStatusObj?.FirstOrDefault(empStatus =>
                empStatus.DateEmpStatApp >= dd28SubModel.LearningDeliveryObject.LearnStartDate &&
                empStatus.DateEmpStatApp <= dd28SubModel.LearningDeliveryObject.LearnPlanEndDate);

            return result;
        }
    }
}
