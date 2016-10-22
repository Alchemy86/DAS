using System;
using DAS.Domain;

namespace DAS.DAL2.Repositories
{
    public class BaseRepository
    {
        private readonly Model1 _context;
        protected Model1 Context
        {
            get
            {
                return _context;
            }
        }

        public BaseRepository(IUnitOfWork context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = context as Model1;
        }
    }
}
