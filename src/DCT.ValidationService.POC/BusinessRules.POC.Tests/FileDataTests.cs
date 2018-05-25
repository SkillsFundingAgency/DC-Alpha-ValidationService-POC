using DCT.ILR.Model;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class FileDataTests
    {
        [Fact]
        public void Populate_FilePreparationDate()
        {
            var fileData = new FileData.FileData();

            var filePreparationDate = new DateTime(2018, 1, 5);

            var message = new Message()
            {
                Header = new MessageHeader()
                {
                    CollectionDetails = new MessageHeaderCollectionDetails()
                    {
                        FilePreparationDate = filePreparationDate
                    }
                }
            };

            fileData.Populate(message);

            fileData.FilePreparationDate.Should().Be(filePreparationDate);
        }
    }
}
