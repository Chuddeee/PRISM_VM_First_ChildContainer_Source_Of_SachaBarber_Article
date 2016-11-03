using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Disposables;
using System.Linq;
using System.Linq.Expressions;
using System.Security.RightsManagement;
using System.Text;

namespace PrismViewModelFirst
{
    public abstract class DisposableViewModel : INPCBase, IDisposable
    {
        CompositeDisposable disposables = new CompositeDisposable();


        public void AddDisposable(IDisposable disposable)
        {
            disposables.Add(disposable);
        }


        public void Dispose()
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose(); 
            } 
        }
    }
}
