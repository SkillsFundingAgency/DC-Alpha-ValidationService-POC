using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.FileData.Interface
{
    public interface IFileData
    {
        DateTime FilePreparationDate { get; }

        void Populate(Message message);
    }
}
