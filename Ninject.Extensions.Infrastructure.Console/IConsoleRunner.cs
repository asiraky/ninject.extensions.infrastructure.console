namespace Ninject.Extensions.Infrastructure.Console
{
    /// <summary>
    /// defines an entry point for a console application
    /// </summary>
    public interface IConsoleRunner
    {
        /// <summary>
        /// the entry point into the console app
        /// </summary>
        /// <param name="context">the console application context</param>
        void Execute(ApplicationContext context);
    }
}