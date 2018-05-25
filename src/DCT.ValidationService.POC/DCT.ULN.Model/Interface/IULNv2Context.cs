using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT.ULN.Model.Interface
{
    public interface IULNv2Context
    {
        DbSet<UniqueLearnerNumbers2> UniqueLearnerNumbers2 { get; set; }
    }
}
