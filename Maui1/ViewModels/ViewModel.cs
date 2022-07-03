using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace Maui1.ViewModels
{
    public class ViewModel : ObservableObject
    {
        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(FullName))]
        public string FirstName;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(FullName))]
        public string LastName;

        public string FullName => $"{FirstName},{LastName}";

        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        public bool IsBusy;
        
        [ICommand]
        public void Tap()
        {
            IsBusy = true;
            Console.WriteLine(FullName);
            IsBusy = false;
        }
    }
}
