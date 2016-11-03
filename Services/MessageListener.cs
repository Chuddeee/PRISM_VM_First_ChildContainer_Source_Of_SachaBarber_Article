using System;
using System.Disposables;

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

using PrismViewModelFirst.ExtensionMethods;
using PrismViewModelFirst.Messages;
using PrismViewModelFirst.ViewModels;
using PrismViewModelFirst.Regions;
using PrismViewModelFirst.Services;



namespace PrismViewModelFirst.Services
{
    public class MessageListener : IMessageListener
    {
        private IEventMessager eventMessager;
        private IUnityContainer mainAppContainer;
        private CompositeDisposable disposables = new CompositeDisposable();
        private int mainContainerCounter = 1;
        private int childContainerCounter = 1;

        private IRegionManager regionManager;

        private IMessageBoxService messageBoxService;

        public MessageListener(IEventMessager eventMessager, IUnityContainer mainAppContainer, 
            IRegionManager regionManager, IMessageBoxService messageBoxService)
        {
            this.messageBoxService = messageBoxService;
            this.regionManager = regionManager;
            this.mainAppContainer = mainAppContainer;
            this.eventMessager = eventMessager;
        }

        public void Start()
        {
            disposables.Add(eventMessager.Observe<CreateMainContainerViewModelMessage>()
                                .Subscribe(NavigateToMainContainerViewModel));
            disposables.Add(eventMessager.Observe<CreateChildContainerViewModelMessage>()
                                .Subscribe(NavigateToChildContainerViewModel));
            disposables.Add(eventMessager.Observe<ShowSpecificMainContainerViewModelMessage>()
                                .Subscribe(ShowSpecificMainContainerViewModel));
        }

        #region Main container navigation
        private void NavigateToMainContainerViewModel(CreateMainContainerViewModelMessage message)
        {
            UriQuery parameters = new UriQuery();
            parameters.Add("ID", mainContainerCounter++.ToString());

            var uri = new Uri(typeof(MainContainerDummyViewModel).FullName + parameters, UriKind.RelativeOrAbsolute);
            regionManager.RequestNavigate("MainRegion", uri, HandleNavigationCallback);
        }


        private void ShowSpecificMainContainerViewModel(ShowSpecificMainContainerViewModelMessage message)
        {

            if (mainContainerCounter == 1)
            {
                messageBoxService.ShowInformation("You need to add a Main container viewmodel first");
                return;            
            }

            int value = -1;
            if (!int.TryParse(message.Content, out value))
            {
                messageBoxService.ShowError(string.Format("Specific ViewModel value must be an integer between 1-{0}", mainContainerCounter));
                return;
            }

            if (value <= 0 || value > mainContainerCounter)
            {
                messageBoxService.ShowError(string.Format("Specific ViewModel value must be an integer between 1-{0}", mainContainerCounter));
                return;
            }

            UriQuery parameters = new UriQuery();
            parameters.Add("ID", message.Content);

            var uri = new Uri(typeof(MainContainerDummyViewModel).FullName + parameters, UriKind.RelativeOrAbsolute);
            regionManager.RequestNavigate("MainRegion", uri, HandleNavigationCallback);
        }
        #endregion

        #region Child container navigation

        /// <summary>
        /// Creates a child container for a viewmodel and registers its dependencies and then shows the ViewModel. It
        /// also ties the lifetime of the child container to that of the newly instantatied ViewModel.
        /// </summary>
        /// <param name="message">The message that has been seen to create a new ViewModel to show</param>
        private void NavigateToChildContainerViewModel(CreateChildContainerViewModelMessage message)
        {
            var childcontainer = mainAppContainer.CreateChildContainer();

            //note if you want to have services that are disposable that you want disposed when child container is disposed
            //they need to be registered using the "HierarchicalLifetimeManager"
            childcontainer.RegisterType<ISomeDummyDisposableService, SomeDummyDisposableService>(new HierarchicalLifetimeManager());

            //this is how to use ViewModel 1st Unity registration
            childcontainer.RegisterTypeForNavigationWithChildContainer<ChildContainerDummyViewModel>();

            UriQuery parameters = new UriQuery();
            parameters.Add("ID", childContainerCounter++.ToString());


            var uri = new Uri(typeof(ChildContainerDummyViewModel).FullName + parameters, UriKind.RelativeOrAbsolute);

            //use the custom extension methods to specify our own container to use
            regionManager.RequestNavigateUsingSpecificContainer("MainRegion", uri, HandleNavigationCallback, childcontainer);
        }

        #endregion

        private void HandleNavigationCallback(NavigationResult navigationCallback)
        {
            if (navigationCallback.Error != null)
            {
                messageBoxService.ShowError("There was an error navigating to the viewmodel");
            }
        }
    }
}
