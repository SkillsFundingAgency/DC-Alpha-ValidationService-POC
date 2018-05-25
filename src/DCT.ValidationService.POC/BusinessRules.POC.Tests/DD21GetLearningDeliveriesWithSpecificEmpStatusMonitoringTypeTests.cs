using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules.DD21;
using Moq;
using Xunit;
using DCT.ILR.Model;

namespace BusinessRules.POC.Tests
{
    public class DD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringTypeTests
    {
        [Trait("Category", "DD21-SubRule")]
        [Fact]
        public void SendsNull_ReturnsNull()
        {
            //arrange
            var refDataMock = new Mock<IReferenceData<string, string>>();
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21AllowedEmpStats))).Returns("11,12");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpTypePart1))).Returns("BSI");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpCodePart1))).Returns("3");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpTypePart2))).Returns("BSI");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpCodePart2))).Returns("4");

            //act
            var dd21GetLDsobj = new DD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType(refDataMock.Object);
            var actual = dd21GetLDsobj.Evaluate(null);
            
            //assert
            Assert.Null(actual);


        }

        [Trait("Category", "DD21-SubRule")]
        [Fact]
        public void InvalidEmpStats_ReturnsEmpty()
        {
            //arrange
            var refDataMock = new Mock<IReferenceData<string, string>>();
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21AllowedEmpStats))).Returns("11,12");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpTypePart1))).Returns("BSI");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpCodePart1))).Returns("3");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpTypePart2))).Returns("BSI");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpCodePart2))).Returns("4");

            //act
            var dd21GetLDsobj = new DD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType(refDataMock.Object);
            var actual = dd21GetLDsobj.Evaluate(new List<MessageLearnerLearnerEmploymentStatus>()
            {
                new MessageLearnerLearnerEmploymentStatus()
                {
                    EmpStat = 5,
                    DateEmpStatApp = DateTime.Now,
                    EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                    {
                        new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                        {
                            ESMCode = 5,
                            ESMType = "test"
                        }
                    }
                }
            });
            
            //assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
            


        }

        [Trait("Category", "DD21-SubRule")]
        [Fact]
        public void validEmpStats_And_validEmpTypeandCode_ReturnsSingle()
        {
            //arrange
            var refDataMock = new Mock<IReferenceData<string, string>>();
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21AllowedEmpStats))).Returns("11,12");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpTypePart1))).Returns("BSI");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpCodePart1))).Returns("3");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpTypePart2))).Returns("BSI");
            refDataMock.Setup(x => x.Get(It.Is<string>(k=> k== AppConstants.DD21EmpCodePart2))).Returns("4");

            //act
            var dd21GetLDsobj = new DD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType(refDataMock.Object);
            var actual = dd21GetLDsobj.Evaluate(new List<MessageLearnerLearnerEmploymentStatus>()
            {
                new MessageLearnerLearnerEmploymentStatus()
                {
                    EmpStat = 11,
                    DateEmpStatApp = DateTime.Now,
                    EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                    {
                        new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                        {
                            ESMCode = 3,
                            ESMType = "BSI"
                        }
                    }
                },
                new MessageLearnerLearnerEmploymentStatus()
                {
                    EmpStat = 19,
                    DateEmpStatApp = DateTime.Now,
                    EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                    {
                        new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                        {
                            ESMCode = 5,
                            ESMType = "BBI"
                        }
                    }
                }
            });
            
            //assert
            Assert.NotNull(actual);
            Assert.Single(actual);
            


        }
    }
}
