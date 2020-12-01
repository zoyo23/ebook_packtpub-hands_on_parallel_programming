using System;
using System.Threading;

namespace Ch07.Domain
{
    public class LazyUsingDelegate
    {
        #region Attributes
        public Data CachedData { get; set; }
        #endregion

        #region Methods
        public static Data GetDataFromDatabase()
        {
            Console.WriteLine("Fetching data");
            //Dummy Delay
            Thread.Sleep(5000);
            return new Data();
        }
        #endregion

    }
}
