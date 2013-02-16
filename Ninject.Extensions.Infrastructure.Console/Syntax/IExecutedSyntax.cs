using System;
using System.Collections.Generic;

namespace Ninject.Extensions.Infrastructure.Console.Syntax
{
    public interface IExecutedSyntax
    {
        void FireAfterEvent(Action<ApplicationContext> @event);
        void FireAfterEvents(IEnumerable<Action<ApplicationContext>> events);
    }
}