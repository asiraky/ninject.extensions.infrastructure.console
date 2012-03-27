using System;
using System.Collections.Generic;
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

        public static IBootStrappedSyntax BootStrapAssembliesMatching(string assemblyScanningAlgorithm)
        {
            return new ApplicationStarter(new IocContainer()).BootStrap(assemblyScanningAlgorithm);
        }

        public static IBootStrappedSyntax BootStrapAssemblies(params string[] assemblies)
        {
            return new ApplicationStarter(new IocContainer()).BootStrap(assemblies);
        }

        public static IBootStrappedSyntax BootStrapAssembliesContaining<T>()
        {
            return new ApplicationStarter(new IocContainer()).BootStrap<T>();
        }

        public IBootStrappedAndConfiguredSyntax InjectContext(string[] args)
        {
            iocContainer.Resolver.Bind<ApplicationContext>().ToConstant(args);
            return this;
        }

        public IBootStrappedAndConfiguredSyntax FireBeforeEvent(Action @event)
        {
            @event();
            return this;
        }

        public IBootStrappedAndConfiguredSyntax FireBeforeEvents(IEnumerable<Action> events)
        {
            foreach (var @event in events) @event();
            return this;
        }

        public IExecutedSyntax AndStartTheConsoleRunner()
        {
            iocContainer.Resolver.Get<IConsoleRunner>().Execute(iocContainer.Resolver.Get<ApplicationContext>());
            return this;
        }

        public void FireAfterEvent(Action @event)
        {
            @event();
        }

        public void FireAfterEvents(IEnumerable<Action> events)
        {
            foreach (var @event in events) @event();
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

        private IBootStrappedSyntax BootStrap<T>()
        {
            iocContainer.WireDependenciesInAssemblyContaining<T>();
            return this;
        }
    }
}