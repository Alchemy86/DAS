using System;
using DAS.Domain;

namespace DAS.DAL2.Repositories
{
    public class BaseRepository
    {
        protected Model1 Context { get; }

        public BaseRepository(IUnitOfWork context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            Context = context as Model1;
        }
    }
}
