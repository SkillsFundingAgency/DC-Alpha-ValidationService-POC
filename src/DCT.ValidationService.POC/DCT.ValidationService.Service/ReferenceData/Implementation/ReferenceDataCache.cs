using DCT.ULN.Model.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DCT.ValidationService.Service.ReferenceData.Interface;
using System.Threading.Tasks;
using DCT.LARS.Model.Interface;
using DCT.ReferenceData.Model;

namespace DCT.ValidationService.Service.ReferenceData.Implementation
{
    public class ReferenceDataCache : IReferenceDataCache
    {
        private bool _initialized;

        private readonly IULNv2Context _ulnv2Context;
        private readonly ILARSContext _larsContext;

        public ReferenceDataCache(IULNv2Context ulnv2Context, ILARSContext larsContext)
        {
            _ulnv2Context = ulnv2Context;
            _larsContext = larsContext;
            //Populate();
        }

        private IEnumerable<long> _ulns = new HashSet<long>();
        private IDictionary<string, LearningDelivery> _learningDeliveries = new Dictionary<string, LearningDelivery>();

        public IEnumerable<long> ULNs
        {
            get { return _ulns; }
            private set { _ulns = value; }
        }

        public IDictionary<string, LearningDelivery> LearningDeliveries
        {
            get { return _learningDeliveries; }
            private set { _learningDeliveries = value; }
        }

        public void Populate(IEnumerable<long> ulns, IEnumerable<string> learnAimRefs)
        {
            if (!_initialized)
            {
                Task ulnTask = Task.Run(() =>
                {
                    ULNs = new HashSet<long>(_ulnv2Context.UniqueLearnerNumbers2
                        .Where(u => ulns.Contains(u.ULN))
                        .Select(uln => uln.ULN));
                });

                Task learningDeliveriesTask = Task.Run(() =>
                {
                    LearningDeliveries = _larsContext.LARS_LearningDelivery
                                        .Where(ld => learnAimRefs.Contains(ld.LearnAimRef))
                                        .Select(ld => new LearningDelivery()
                                        {
                                            LearnAimRef = ld.LearnAimRef,
                                            NotionalNVQLevelv2 = ld.NotionalNVQLevelv2,
                                            LearningDeliveryCategories = ld.LARS_LearningDeliveryCategory.Select
                                            (
                                                ldc => new LearningDeliveryCategory()
                                                {
                                                    CategoryRef = ldc.CategoryRef,
                                                    EffectiveFrom = ldc.EffectiveFrom,
                                                    EffectiveTo = ldc.EffectiveTo,
                                                    LearnAimRef = ldc.LearnAimRef
                                                }
                                            ),
                                            FrameworkAims = ld.LARS_FrameworkAims.Select
                                            (
                                                fa => new FrameworkAim()
                                                {
                                                    FworkCode = fa.FworkCode,
                                                    ProgType  = fa.ProgType,
                                                    PwayCode = fa.PwayCode,
                                                    LearnAimRef = fa.LearnAimRef,
                                                    EffectiveFrom = fa.EffectiveFrom,
                                                    EffectiveTo = fa.EffectiveTo,
                                                    FrameworkComponentType = fa.FrameworkComponentType
                                                }
                                            ),
                                            AnnualValues = ld.LARS_AnnualValue.Select
                                            (
                                                av => new AnnualValue()
                                                {
                                                    LearnAimRef = av.LearnAimRef,
                                                    EffectiveFrom = av.EffectiveFrom,
                                                    EffectiveTo = av.EffectiveTo,
                                                    BasicSkills = av.BasicSkills
                                                }
                                            )
                                        }).ToDictionary(ld => ld.LearnAimRef, ld => ld);
                });

                Task.WaitAll(ulnTask, learningDeliveriesTask);
                
                _initialized = true;
            }            
        }
    }
}
