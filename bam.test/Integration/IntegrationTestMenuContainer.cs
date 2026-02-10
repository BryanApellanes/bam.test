using Bam.Console;
using Bam.DependencyInjection;
using Bam.Services;

namespace Bam.Test.Integration
{
    [IntegrationTestMenu()]
    public abstract class IntegrationTestMenuContainer : MenuContainer
    {
        public IntegrationTestMenuContainer() : this(BamConsoleContext.GetDefaultServiceRegistry())
        {
        }

        public IntegrationTestMenuContainer(ServiceRegistry serviceRegistry)
            : base()
        {
            this.SetDependencyProvider(serviceRegistry);
        }

        public ServiceRegistry Configure(ServiceRegistry serviceRegistry)
        {
            if (DependencyProvider != null)
            {
                serviceRegistry.CopyFrom((DependencyProvider)DependencyProvider);
            }

            DependencyProvider = serviceRegistry;
            return serviceRegistry;
        }

        public ServiceRegistry Configure(Action<ServiceRegistry>? configure = null)
        {
            configure = configure ?? ((svc) => { });
            ServiceRegistry serviceRegistry = new ServiceRegistry();
            if (DependencyProvider != null)
            {
                serviceRegistry.CopyFrom((DependencyProvider)DependencyProvider);
            }

            configure(serviceRegistry);
            DependencyProvider = serviceRegistry;
            return serviceRegistry;
        }
    }
}
