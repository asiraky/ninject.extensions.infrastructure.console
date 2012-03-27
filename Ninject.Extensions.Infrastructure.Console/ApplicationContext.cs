using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ninject.Extensions.Infrastructure.Console
{
    /// <summary>
    /// holds contextual information about a console application
    /// </summary>
    public class ApplicationContext : IEnumerable<KeyValuePair<string, string>>
    {
        /// <summary>
        /// the raw console arguments
        /// </summary>
        public List<KeyValuePair<string, string>> RawArgs { get; private set; }

        public ApplicationContext(IEnumerable<string> args)
            : this(args, ':') { }

        public ApplicationContext(IEnumerable<string> args, char splitChar)
        {
            var query = from a in args
                        let chrs = a.Split(splitChar)
                        select new KeyValuePair<string, string>(chrs[0].TrimStart('-'), chrs[1]);

            RawArgs = query.ToList();
        }

        public static implicit operator ApplicationContext(string[] args)
        {
            return new ApplicationContext(args);
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return RawArgs.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}