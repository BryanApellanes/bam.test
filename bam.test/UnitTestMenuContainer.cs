using Bam.Console;
using Bam.DependencyInjection;
using Bam.Services;

namespace Bam.Test
{
    [UnitTestMenu()]
    public abstract class UnitTestMenuContainer : MenuContainer
    {
        public UnitTestMenuContainer() : this(BamConsoleContext.GetDefaultServiceRegistry())
        {
        }

        /// <summary>
        /// Create a new UnitTestMenuContainer using the specified dependency provider.
        /// </summary>
        /// <param name="serviceRegistry"></param>
        public UnitTestMenuContainer(ServiceRegistry serviceRegistry)
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
        /// <summary>
        /// Configures the specified service registry before setting as the DependencyProvider property.
        /// </summary>
        /// <param name="serviceRegistry">The service registry.</param>
        /// <returns>ServiceRegistry</returns>
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
