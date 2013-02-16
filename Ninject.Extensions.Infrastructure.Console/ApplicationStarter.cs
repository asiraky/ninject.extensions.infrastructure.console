using System;
using System.Collections.Generic;
using System.Reflection;
using Ninject.Extensions.Infrastructure.Console.Syntax;

namespace Ninject.Extensions.Infrastructure.Console
{
    public class ApplicationStarter : IBootStrappedSyntax
    {
        private readonly IocContainer iocContainer;

        private ApplicationStarter(IocContainer iocContainer)
        {
            this.iocContainer = iocContainer;
        }

        public static IBootStrappedSyntax BootStrapThisAssembly()
        {
            return new ApplicationStarter(new IocContainer()).BootStrap(Assembly.GetCallingAssembly().GetName().Name);
        }

        public static IBootStrappedSyntax BootStrapAssembliesMatching(string assemblyScanningAlgorithm)
        {
            return new ApplicationStarter(new IocContainer()).BootStrap(assemblyScanningAlgorithm);
        }

        public static IBootStrappedSyntax BootStrapAssemblies(params string[] assemblies)
        {
            return new ApplicationStarter(new IocContainer()).BootStrap(assemblies);
        }

        public IBootStrappedAndConfiguredSyntax InjectContext(string[] args)
        {
            iocContainer.Resolver.Bind<ApplicationContext>().ToConstant((ApplicationContext)args);
            return this;
        }

        public IBootStrappedAndConfiguredSyntax FireBeforeEvent(Action<ApplicationContext> @event)
        {
            @event(iocContainer.Resolver.Get<ApplicationContext>());
            return this;
        }

        public IBootStrappedAndConfiguredSyntax FireBeforeEvents(IEnumerable<Action<ApplicationContext>> events)
        {
            foreach (var @event in events)
                FireBeforeEvent(@event);
            return this;
        }

        public IExecutedSyntax AndStartTheConsoleRunner()
        {
            iocContainer.Resolver.Get<IConsoleRunner>()
                .Execute(iocContainer.Resolver.Get<ApplicationContext>());
            return this;
        }

        public void FireAfterEvent(Action<ApplicationContext> @event)
        {
            @event(iocContainer.Resolver.Get<ApplicationContext>());
        }

        public void FireAfterEvents(IEnumerable<Action<ApplicationContext>> events)
        {
            foreach (var @event in events)
                FireAfterEvent(@event);
        }

        private IBootStrappedSyntax BootStrap(string assemblyScanningAlgorithm)
        {
            iocContainer.WireDependenciesInAssemblyMatching(assemblyScanningAlgorithm);
            return this;
        }

        private IBootStrappedSyntax BootStrap(params string[] assemblies)
        {
            iocContainer.WireDependenciesInAssemblies(assemblies);
            return this;
        }
    }
}