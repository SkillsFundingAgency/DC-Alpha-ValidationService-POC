namespace DCT.ULN.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UniqueLearnerNumbers2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ULN { get; set; }
    }
}
