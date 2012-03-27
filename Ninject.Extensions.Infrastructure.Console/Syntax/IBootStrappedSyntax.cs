namespace Ninject.Extensions.Infrastructure.Console.Syntax
{
    public interface IBootStrappedSyntax : IBootStrappedAndConfiguredSyntax
    {
        IBootStrappedAndConfiguredSyntax InjectContext(string[] args);
    }
}