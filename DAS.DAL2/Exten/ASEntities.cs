using DAS.Domain;

namespace DAS.DAL2
{
    // ReSharper disable once InconsistentNaming
    public partial class Model1 : IUnitOfWork
    {
        public void Save()
        {
            SaveChanges();
        }
    }
}
