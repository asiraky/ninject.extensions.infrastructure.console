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
        public Dictionary<string, string> RawArgs { get; private set; }

        public ApplicationContext(IEnumerable<string> args)
        {
            RawArgs = new Dictionary<string, string>();
            var counter = 0;

            var enumerable = args as string[] ?? args.ToArray();

            while (enumerable.Count() > counter)
            {
                if (enumerable.Count() > counter + 1
                    && enumerable.ElementAt(counter).StartsWith("-")
                    && !enumerable.ElementAt(counter + 1).StartsWith("-"))
                {
                    //the current arg is a -arg and the next is a value of that arg e.g. -db sales
                    //so add this argument as the key and the next argument as the value and increment +2
                    RawArgs.Add(enumerable.ElementAt(counter).TrimStart('-'), enumerable.ElementAt(counter + 1));
                    counter += 2;
                }
                else if (enumerable.Count() > counter
                    && enumerable.ElementAt(counter).StartsWith("-")
                    && enumerable.ElementAt(counter + 1).StartsWith("-"))
                {
                    //the current arg is -arg and the next is also -arg which means we just add 
                    //this argument as the key and an empty value as its value then increment +1
                    RawArgs.Add(enumerable.ElementAt(counter).TrimStart('-'), string.Empty);
                    counter++;
                }
            }
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