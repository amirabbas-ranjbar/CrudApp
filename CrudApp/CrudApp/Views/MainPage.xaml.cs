using CrudApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrudApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            GetUserInfo();
        }

        async void GetUserInfo()
        {
            try
            {

                string url = "http://localhost/WebApi/home/getall";
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);

                var EmpList = JsonConvert.DeserializeObject<List<User>>(result);

                UList.ItemsSource = null;
                UList.ItemsSource = new ObservableCollection<User>(EmpList);


            }
            catch (Exception ex)
            {
                await DisplayAlert("Input Error", ex.Message, "OK");
                return;
            }
        }
        private async void UList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                User user = e.Item as User;
                await Navigation.PushAsync(new Registration(user));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Input Error", ex.Message, "OK");
                return;
            }
        }

        private void UList_Refreshing(object sender, EventArgs e)
        {
            GetUserInfo();
            UList.IsRefreshing = false;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            User NewUser = new User();
            NewUser.Name = null;
            NewUser.Address = null;
            NewUser.Email = null;
            NewUser.Mobile = null;
            NewUser.Password = null;
            Navigation.PushAsync(new Registration(NewUser));

        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var menu = sender as MenuItem;
                int EmpId = Convert.ToInt32(menu.CommandParameter.ToString());
                string url = $"http://localhost/WebApi/home/get/{EmpId}";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(url);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);
                if (responseData.Status == 1)
                {
                    await DisplayAlert("Info", responseData.Message, "OK");
                    GetUserInfo();
                }
                else
                {
                    await DisplayAlert("Error", responseData.Message, "ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Input Error", ex.Message, "OK");
                return;
            }

        }
    }
}