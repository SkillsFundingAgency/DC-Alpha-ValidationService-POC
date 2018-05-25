using BusinessRules.POC.FileData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCT.ILR.Model;

namespace BusinessRules.POC.FileData
{
    public class FileData : IFileData
    {
        public DateTime FilePreparationDate { get; private set; }

        public void Populate(Message message)
        {
            FilePreparationDate = message.Header.CollectionDetails.FilePreparationDate;
        }
    }
}
