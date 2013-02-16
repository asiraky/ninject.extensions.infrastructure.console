using System;
using System.Collections.Generic;

namespace Ninject.Extensions.Infrastructure.Console.Syntax
{
    public interface IBootStrappedAndConfiguredSyntax : IExecutedSyntax
    {
        IBootStrappedAndConfiguredSyntax FireBeforeEvent(Action<ApplicationContext> @event);
        IBootStrappedAndConfiguredSyntax FireBeforeEvents(IEnumerable<Action<ApplicationContext>> events);
        IExecutedSyntax AndStartTheConsoleRunner();
    }
}