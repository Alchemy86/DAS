using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Repositories;
using DAS.Domain;
using DAS.Domain.GoDaddy.Users;
using DAS.Domain.Users;
using Lunchboxweb.BaseFunctions;
using Moq;
using Ninject.Modules;

namespace Test
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            var mockObject = new Mock<IGoDaddySession>();
            mockObject.Setup(x => x.Username).Returns("michaelgipmedia");
            mockObject.Setup(x => x.Password).Returns("Aaron1972.xlz");
            mockObject.Setup(x => x.GoDaddyAccount).Returns(
                new DAS.Domain.GoDaddy.Users.GoDaddyAccount
                {
                    AccountId = Guid.NewGuid(),
                    Username = "michaelgipmedia",
                    Password = "Aaron1972.xlz",
                    Verified = false
                });
            Bind<IGoDaddySession>().ToConstant(mockObject.Object);
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUnitOfWork>().To<ASEntities>();
            Bind<ITextManipulation>().To<TextManipulation>();
            Bind<ISystemRepository>().To<SystemRepository>();
        }
    }
}
