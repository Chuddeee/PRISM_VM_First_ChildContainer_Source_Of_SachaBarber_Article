using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrismViewModelFirst.Services
{
    public interface ISomeDummyDisposableService : IDisposable
    {
        string GetGuidString();
    }


    public class SomeDummyDisposableService : ISomeDummyDisposableService
    {
        public void Dispose()
        {
            Console.WriteLine("SomeDummyDisposableService disposed");
        }

        public string GetGuidString()
        {
            return Guid.NewGuid().ToString();
        }
    }

}
