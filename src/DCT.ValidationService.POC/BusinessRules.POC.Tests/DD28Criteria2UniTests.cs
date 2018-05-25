using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules.DD28;
using Moq;
using Xunit;
using DCT.ILR.Model;

namespace BusinessRules.POC.Tests
{
    public class DD28Criteria2UniTests
    {
        private readonly Mock<IReferenceData<string, string>> _mock;

        public DD28Criteria2UniTests()
        {
            _mock = new Mock<IReferenceData<string, string>>();
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria2LearnerEmplStatusEmpStats)))
                .Returns("10,11,12,98");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria2EmpStatusMonitoringESMType)))
                .Returns("BSI");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria2EmpStatusMonitoringESMCodes)))
                .Returns("1,2");
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void FundModel_Invalid_ReturnsFalse()
        {
            //arrange
            var dd28RuleFundModelAndEmpStatEmpCodeCheck = new DD28RuleCriteria2(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new MessageLearnerLearningDelivery()
                {
                    FundModel = 34
                },
                LearnerEmploymentStatusObj = new List<MessageLearnerLearnerEmploymentStatus>()
                {
                    new MessageLearnerLearnerEmploymentStatus()
                    {
                        EmpStat = 11,
                        EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                        {
                            new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                            {
                                ESMCode = 3,
                                ESMType = "BSI"
                            },
                            new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                            {
                                ESMCode = 1,
                                ESMType = "DUmmy"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28RuleFundModelAndEmpStatEmpCodeCheck.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void Invalid_EMPStat_ReturnsFalse()
        {
            //arrange
            var dd28RuleFundModelAndEmpStatEmpCodeCheck = new DD28RuleCriteria2(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new MessageLearnerLearningDelivery()
                {
                    FundModel = 35
                },
                LearnerEmploymentStatusObj = new List<MessageLearnerLearnerEmploymentStatus>()
                {
                    new MessageLearnerLearnerEmploymentStatus()
                    {
                        EmpStat = 110,
                        EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                        {
                            new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                            {
                                ESMCode = 3,
                                ESMType = "BSI"
                            },
                            new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                            {
                                ESMCode = 1,
                                ESMType = "DUmmy"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28RuleFundModelAndEmpStatEmpCodeCheck.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void Invalid_ESMType_ReturnsFalse()
        {
            //arrange
            var dd28RuleFundModelAndEmpStatEmpCodeCheck = new DD28RuleCriteria2(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new MessageLearnerLearningDelivery()
                {
                    FundModel = 35
                },
                LearnerEmploymentStatusObj = new List<MessageLearnerLearnerEmploymentStatus>()
                {
                    new MessageLearnerLearnerEmploymentStatus()
                    {
                        EmpStat = 11,
                        EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                        {
                            new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                            {
                                ESMCode = 1,
                                ESMType = "Dummy2"
                            },
                            new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                            {
                                ESMCode = 1,
                                ESMType = "DUmmy"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28RuleFundModelAndEmpStatEmpCodeCheck.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void valid_Allvalues_ReturnsTrue()
        {
            //arrange
            var dd28RuleFundModelAndEmpStatEmpCodeCheck = new DD28RuleCriteria2(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new MessageLearnerLearningDelivery()
                {
                    FundModel = 35
                },
                LearnerEmploymentStatusObj =
                    new List<MessageLearnerLearnerEmploymentStatus>()
                    {
                        new MessageLearnerLearnerEmploymentStatus()
                        {
                            EmpStat = 11,
                            EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                            {
                                new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                                {
                                    ESMCode = 1,
                                    ESMType = "BSI"
                                },
                                new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                                {
                                    ESMCode = 1,
                                    ESMType = "DUmmy"
                                }
                            }
                        }
                    }
            };

            //act
            var actual = dd28RuleFundModelAndEmpStatEmpCodeCheck.Evaluate(param);

            //assert
            Assert.True(actual);
        }

    }
}
