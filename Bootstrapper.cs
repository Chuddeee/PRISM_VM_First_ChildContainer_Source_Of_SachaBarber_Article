using System;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

using PrismViewModelFirst.ExtensionMethods;
using PrismViewModelFirst.Regions;
using PrismViewModelFirst.Services;
using PrismViewModelFirst.ViewModels;
using System.Windows;


namespace PrismViewModelFirst
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            // 2
            return this.Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

     
        protected override void ConfigureContainer()
        {
            // 1
            base.ConfigureContainer();
            Container.RegisterType<ShellViewModel>(new TransientLifetimeManager());

            //регистрируем сервисы
            Container.RegisterType<IEventMessager, EventMessager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMessageBoxService, MessageBoxService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMessageListener, MessageListener>(new ContainerControlledLifetimeManager());

            
            //регистрируем стафф для навигации Prism
            //custom region stuff to support child container navigation
            Container.RegisterType<IRegionNavigationContentLoader, CustomRegionNavigationContentLoader>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRegionNavigationService, CustomRegionNavigationService>(new ContainerControlledLifetimeManager());

            //регистрируем тип для навигации
            //this is how to use ViewModel 1st Unity registration
            Container.RegisterTypeForNavigation<MainContainerDummyViewModel>();


            // получаем и запускам службу ообщений
            Container.Resolve<IMessageListener>().Start();
        }
    }


   
}
