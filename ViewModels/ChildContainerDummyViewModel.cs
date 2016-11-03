using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using System.Windows;
using PrismViewModelFirst.Services;

namespace PrismViewModelFirst.ViewModels
{
    public class ChildContainerDummyViewModel : DisposableViewModel, INavigationAware
    {
        private string id;
        private IRegionManager regionManager;

        public ChildContainerDummyViewModel(IRegionManager regionManager, ISomeDummyDisposableService someDummyDisposableService)
        {
            this.regionManager = regionManager;

            string guidFromService = someDummyDisposableService.GetGuidString();
            CloseViewCommand = new SimpleCommand(ExecuteCloseViewCommand);
            IsCloseable = true;
        }





        private bool isCloseable;

        public bool IsCloseable
        {
            get { return isCloseable; }
            set
            {
                RaiseAndSetIfChanged(ref isCloseable, value, () => IsCloseable);
            }
        }



        public string ID
        {
            get { return id; }
            set
            {
                RaiseAndSetIfChanged(ref id, value, () => ID);
            }
        }


        public string DisplayName
        {
            get { return "Child container Dummy VM"; }
        }


        private void ExecuteCloseViewCommand(Object arg)
        {
            IRegion region = regionManager.Regions["MainRegion"];
            region.Remove(this);
            this.Dispose();
        }


        public ICommand CloseViewCommand { get; private set; }


        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string id = navigationContext.Parameters["ID"];
            ID = string.Format("ID:{0}", id);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            string id = navigationContext.Parameters["ID"];
            return ID == string.Format("ID:{0}", id);
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
