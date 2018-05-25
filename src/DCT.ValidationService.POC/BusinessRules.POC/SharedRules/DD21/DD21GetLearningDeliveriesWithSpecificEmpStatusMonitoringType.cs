using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using DCT.ILR.Model;

namespace BusinessRules.POC.SharedRules.DD21
{
    public interface IDD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType: IShortRule<IEnumerable<MessageLearnerLearnerEmploymentStatus>, List<MessageLearnerLearnerEmploymentStatus>>
    {
        
    }

    public class DD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType : IDD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType
    {
        private readonly List<string> _allowedEmpStats;
        private readonly string _allowedEsmTypePart1;
        private readonly string _allowedEsmCodePart1;
        private readonly string _allowedEsmTypePart2;
        private readonly string _allowedEsmCodePart2;

        public DD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType(IReferenceData<string, string> referenceData)
        {
            _allowedEmpStats = referenceData.Get(AppConstants.DD21AllowedEmpStats).Split(',').ToList();
            _allowedEsmTypePart1 = referenceData.Get(AppConstants.DD21EmpTypePart1);
            _allowedEsmCodePart1 = referenceData.Get(AppConstants.DD21EmpCodePart1);
            _allowedEsmTypePart2 = referenceData.Get(AppConstants.DD21EmpTypePart2);
            _allowedEsmCodePart2 = referenceData.Get(AppConstants.DD21EmpCodePart2);


        }
        public List<MessageLearnerLearnerEmploymentStatus> Evaluate(IEnumerable<MessageLearnerLearnerEmploymentStatus> empRecords)
        {
            if (_allowedEmpStats == null || empRecords == null) return null;
            
            return (from empRecord in empRecords
                    where (empRecord.EmploymentStatusMonitoring != null)
                    from empStatusMonitoring in empRecord.EmploymentStatusMonitoring
                    where (_allowedEmpStats.Contains(empRecord.EmpStat.ToString())) &&
                    ((empStatusMonitoring.ESMCode.ToString() == _allowedEsmCodePart1 && empStatusMonitoring.ESMType == _allowedEsmTypePart1) ||
                    (empStatusMonitoring.ESMCode.ToString() == _allowedEsmCodePart2 && empStatusMonitoring.ESMType == _allowedEsmTypePart2))
                    select empRecord).ToList();


        }
    }
}
