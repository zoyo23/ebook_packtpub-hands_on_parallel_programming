using Ch07.Domain;
using Common.Interfaces;
using System;

namespace Ch07.Examples
{
    public class _IntroducingSystemLazy : IBaseExecutor
    {
        public void Run()
        {
            //ConstructionLogicEncapsulatedInsideConstructor();
            //ConstructionLogicEncapsulatedInsideConstructorUsingLazy();
            ConstructionLogicPassedDelegateToLazy();
        }

        #region Construction Logic Encapsulated Inside Constructor Examples
        private void ConstructionLogicEncapsulatedInsideConstructorUsingLazy()
        {
            Console.WriteLine("Creating Lazy object");
            Lazy<DataWrapper> lazyDataWrapper = new Lazy<DataWrapper>();
            Console.WriteLine("Lazy Object Created");
            Console.WriteLine("Now we want to access data");
            var data = lazyDataWrapper.Value.CachedData;
            Console.WriteLine("Finishing up");
        }

        private void ConstructionLogicEncapsulatedInsideConstructor()
        {
            DataWrapper dataWrapper = new DataWrapper();
        }
        #endregion

        #region Construction Logic Passed as a Delegate Examples
        private void ConstructionLogicPassedDelegateToLazy()
        {
            Console.WriteLine("Creating Lazy object");
            Func<Data> dataFetchLogic = new Func<Data>(() => LazyUsingDelegate.GetDataFromDatabase());
            Lazy<Data> lazyDataWrapper = new Lazy<Data>(dataFetchLogic);
            Console.WriteLine("Lazy Object Created");
            Console.WriteLine("Now we want to access data");
            var data = lazyDataWrapper.Value;
            Console.WriteLine("Finishing up");

        }
        #endregion
    }
}
