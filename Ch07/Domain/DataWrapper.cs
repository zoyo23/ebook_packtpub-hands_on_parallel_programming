using System;
using System.Threading;

namespace Ch07.Domain
{
    public class DataWrapper
    {
        #region Attributes
        public Data CachedData { get; set; }
        #endregion

        #region Constructors
        public DataWrapper()
        {
            CachedData = GetDataFromDatabase();
            Console.WriteLine("Object initialized");
        }
        #endregion

        #region Methods

        #region Private Methods
        private Data GetDataFromDatabase()
        {
            //Dummy Delay
            Thread.Sleep(5000);
            return new Data();
        }
        #endregion

        #endregion
    }
}
