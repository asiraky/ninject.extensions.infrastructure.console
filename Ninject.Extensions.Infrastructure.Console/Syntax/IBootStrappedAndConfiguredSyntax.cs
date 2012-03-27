using System;
using System.Collections.Generic;

namespace Ninject.Extensions.Infrastructure.Console.Syntax
{
    public interface IBootStrappedAndConfiguredSyntax : IExecutedSyntax
    {
        IBootStrappedAndConfiguredSyntax FireBeforeEvent(Action @event);
        IBootStrappedAndConfiguredSyntax FireBeforeEvents(IEnumerable<Action> events);
        IExecutedSyntax AndStartTheConsoleRunner();
    }
}