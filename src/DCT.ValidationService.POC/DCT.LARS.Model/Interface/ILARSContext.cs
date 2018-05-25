using System.Data.Entity;

namespace DCT.LARS.Model.Interface
{
    public interface ILARSContext
    {
        DbSet<LARS_LearningDelivery> LARS_LearningDelivery { get; set; }
        DbSet<LARS_LearningDeliveryCategory> LARS_LearningDeliveryCategory { get; set; }
    }
}
