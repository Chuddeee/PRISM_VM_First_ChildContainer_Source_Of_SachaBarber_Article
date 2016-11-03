using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;

namespace PrismViewModelFirst.ExtensionMethods
{
    public static class UnityContainerExtensions
    {
        public static void RegisterTypeForNavigation<T>(this IUnityContainer container)
        {
            container.RegisterType(typeof(Object), typeof(T), typeof(T).FullName);
        }

        public static void RegisterTypeForNavigationWithChildContainer<T>(this IUnityContainer container)
        {
            container.RegisterType(typeof(Object), typeof(T), typeof(T).FullName, 
                new InjectionMethod("AddDisposable", new object[] { container }));
        }
    }

}
