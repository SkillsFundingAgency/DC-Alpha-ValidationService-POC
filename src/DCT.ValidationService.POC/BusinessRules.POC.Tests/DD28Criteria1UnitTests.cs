using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules.DD28;
using Moq;
using Xunit;
using DCT.ILR.Model;

namespace BusinessRules.POC.Tests
{
    public class DD28Criteria1UnitTests
    {
        private Mock<IReferenceData<string, string>> _mock;

        public DD28Criteria1UnitTests()
        {
            _mock = new Mock<IReferenceData<string, string>>();
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria1EMPStats)))
                .Returns("11,12");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria1EMPType)))
                .Returns("BSI");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria1ESMCodes)))
                .Returns("3,4");
        }

        [Trait("Category", "DD28-SubRule-Rule")]
        [Fact]
        public void EMpStat_NotInAllowedlist_ReturnsFalse()
        {
            //arrange
            var dd28EmpStatTypeCode11Rule = new DD28Criteria1(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new MessageLearnerLearningDelivery()
                {

                },
                LearnerEmploymentStatusObj = new List<MessageLearnerLearnerEmploymentStatus>()
                {
                    new MessageLearnerLearnerEmploymentStatus()
                    {
                        EmpStat = 15,
                        EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                        {
                            new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                            {
                                ESMCode = 5,
                                ESMType = "BIS"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28EmpStatTypeCode11Rule.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Trait("Category", "DD28-SubRule-Rule")]
        [Fact]
        public void EmpStatESMTypeAndCode_InAllowedValues_ReturnsTrue()
        {
            //arrange
            var dd28EmpStatTypeCode11Rule = new DD28Criteria1(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new MessageLearnerLearningDelivery()
                {

                },
                LearnerEmploymentStatusObj = new List<MessageLearnerLearnerEmploymentStatus>() { 
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
            var actual = dd28EmpStatTypeCode11Rule.Evaluate(param);

            //assert
            Assert.True(actual);
        }

        [Trait("Category", "DD28-SubRule-Rule")]
        [Fact]
        public void ESMTypeAndCode_NotInAllowedValues_ReturnsFalse()
        {
            //arrange
            var dd28EmpStatTypeCode11Rule = new DD28Criteria1(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new MessageLearnerLearningDelivery()
                {

                },
                LearnerEmploymentStatusObj = new List<MessageLearnerLearnerEmploymentStatus>()
                {
                    new MessageLearnerLearnerEmploymentStatus()
                    {
                        EmpStat = 12,
                        EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                        {
                            new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                            {
                                ESMCode = 1,
                                ESMType = "DUMMy"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28EmpStatTypeCode11Rule.Evaluate(param);

            //assert
            Assert.False(actual);
        }
    }
}
