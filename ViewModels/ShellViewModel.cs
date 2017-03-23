using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

using PrismViewModelFirst.Messages;
using PrismViewModelFirst.Regions;
using PrismViewModelFirst.Services;

namespace PrismViewModelFirst.ViewModels
{
    public class ShellViewModel : INPCBase
    {
        // 4 инициализации VM, подписка на прослушивание событий
        public ShellViewModel(IEventMessager eventMessager)
        {

            NavigateToMainContainerViewModelCommand = new SimpleCommand(x =>
                {
                    eventMessager.Publish(new CreateMainContainerViewModelMessage());
                });

            ShowSpecificMainContainerViewModelCommand = new SimpleCommand(x =>
            {
                eventMessager.Publish(new ShowSpecificMainContainerViewModelMessage() { Content = IdToShow });
            });

            NavigateToChildContainerViewModelCommand = new SimpleCommand(x =>
            {
                eventMessager.Publish(new CreateChildContainerViewModelMessage());
            });
        }


        private string idToSHow;


        public string IdToShow
        {
            get { return idToSHow; }
            set
            {
                RaiseAndSetIfChanged(ref idToSHow, value, () => IdToShow);
            }
        }

        public ICommand NavigateToMainContainerViewModelCommand { get; set; }
        public ICommand ShowSpecificMainContainerViewModelCommand { get; set; }
        public ICommand NavigateToChildContainerViewModelCommand { get; set; }
    }
}
