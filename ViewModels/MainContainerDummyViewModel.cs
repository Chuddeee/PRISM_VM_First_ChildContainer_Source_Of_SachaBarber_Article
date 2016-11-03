using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using System.Windows;

namespace PrismViewModelFirst.ViewModels
{
    public class MainContainerDummyViewModel : INPCBase, INavigationAware
    {


        public MainContainerDummyViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

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

        private string id;
        private IRegionManager regionManager;

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
            get { return "Main container Dummy VM"; }
        }


        private void ExecuteCloseViewCommand(Object arg)
        {
            IRegion region = regionManager.Regions["MainRegion"];
            region.Remove(this);
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
