using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrismViewModelFirst.Services
{
    public interface IEventMessager
    {
        IObservable<T> Observe<T>();
        void Publish<T>(T @event);
    }
}
