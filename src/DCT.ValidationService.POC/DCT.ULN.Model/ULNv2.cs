namespace DCT.ULN.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using DCT.ULN.Model.Interface;

    public partial class ULNv2 : DbContext, IULNv2Context
    {
        public ULNv2()
            : base("name=ULNv2")
        {
        }

        public virtual DbSet<UniqueLearnerNumbers2> UniqueLearnerNumbers2 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
