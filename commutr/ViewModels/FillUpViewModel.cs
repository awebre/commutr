﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using commutr.Services;
using commutr.Models;
using commutr.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace commutr.ViewModels
{
    public class FillUpViewModel : BaseViewModel
    {
        private readonly IDataStore<FillUp> dataStore;
        private FillUp fillUp;

        public FillUpViewModel(IDataStore<FillUp> dataStore)
        {
            this.dataStore = dataStore;
            FillUps = new ObservableCollection<FillUp>();
            AddFillUpCommand = new Command(async () => await ExecuteAddFillUpCommand());
            DeleteFillUpCommand = new Command<FillUp>(async (fillUp) => await ExecuteDeleteFillUpCommand(fillUp));
            LoadFillUpsCommand = new Command(async () => await ExecuteLoadFillUpsCommand());
        }
        
        public Vehicle CurrentVehicle { get; set; }

        public new string Title => "Fill Ups";
        
        public int SelectedVehicleId { get; set; }

        public ObservableCollection<FillUp> FillUps { get; set; }

        public FillUp FillUp
        {
            get => fillUp;
            set
            {
                fillUp = value;
                if (fillUp != null)
                {
                    Application.Current.MainPage.Navigation.PushAsync(new AddFillUpPage(SelectedVehicleId, fillUp));
                }
            }
        }

        public ICommand AddFillUpCommand { get; }
        
        public ICommand DeleteFillUpCommand { get; }
        
        public ICommand LoadFillUpsCommand { get; }

        private async Task ExecuteAddFillUpCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddFillUpPage(SelectedVehicleId));
        }

        private async Task ExecuteDeleteFillUpCommand(FillUp fillUp)
        {
            var result = await dataStore.DeleteItemAsync(fillUp.Id);
            if (result == 1)
            {
                FillUps.Remove(fillUp);
            }
        }

        private async Task ExecuteLoadFillUpsCommand()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                FillUps.Clear();
            
                var fillUps = (await dataStore.GetItemsAsync()).Where(x => x.VehicleId == SelectedVehicleId).ToList();
                fillUps.ForEach(x => FillUps.Add(x));
                fillUps = fillUps.OrderByDescending(x => x.Date).ToList();
                FillUps = new ObservableCollection<FillUp>(fillUps);
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            FillUp = null;
            LoadFillUpsCommand.Execute(null);
        }
    }
}
