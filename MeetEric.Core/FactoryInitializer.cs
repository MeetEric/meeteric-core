namespace MeetEric
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common;
    using Diagnostics;
    using Security;

    public static class FactoryInitializer
    {
        public static bool IsInitialized { get; private set; }

        public static void Initialize()
        {
            MeetEricFactory.RegisterService<ILoggingService>(() => new SimpleFrameworkLogger());
            MeetEricFactory.RegisterService<IIdentityFactory>(() => new IdentifierFactory());
        }
    }
}
