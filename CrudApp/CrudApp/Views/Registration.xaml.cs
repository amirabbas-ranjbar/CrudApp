using CrudApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrudApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public Registration(User user)
        {
            InitializeComponent();

            if (user.Name != null && user.Address != null && user.Email != null && user.Mobile != null && user.Password != null)
            {

                txtName.Text = user.Name;
                txtAddress.Text = user.Address;
                txtEmail.Text = user.Email;
                txtMobile.Text = user.Mobile;

                txtMobile.IsEnabled = false;
                txtPassword.Text = user.Password;
                BtnRegistration.IsVisible = false;
                BtnUpdate.IsVisible = true;
            }
            else
            {
                txtMobile.IsEnabled = true;
                BtnRegistration.IsVisible = true;
                BtnUpdate.IsVisible = false;
            }

        }



        private async void BtnRegistration_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtName.Text))
            {
                await DisplayAlert("Input Error", "Name is Required", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                await DisplayAlert("Input Error", "Address is Required", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                await DisplayAlert("Input Error", "Email Address is Required", "OK");
                return;
            }
            bool bEmail;
            bEmail = Regex.IsMatch(txtEmail.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (bEmail == false)
            {
                await DisplayAlert("Input Error", "Invalid Email Address.", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtMobile.Text))
            {
                await DisplayAlert("Input Error", "Mobile Number is Required", "OK");
                return;
            }
            bool b;
            b = Regex.IsMatch(txtMobile.Text, @"^[7-9]\d{9}$");
            if (b == false)
            {
                await DisplayAlert("Input Error", "Invalid Mobile Number.", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Input Error", "Password is Required", "OK");
                return;
            }

            try
            {
                BtnRegistration.IsEnabled = false;
                User userType = new User();
                userType.Name = txtName.Text;
                userType.Address = txtAddress.Text;
                userType.Email = txtEmail.Text;
                userType.Mobile = txtMobile.Text;
                userType.Password = txtPassword.Text;


                string url = "http://127.0.0.1:8080/api/User/SaveUser";

                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(userType);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);
                if (responseData.Status == 1)
                {
                    await Navigation.PopAsync();
                    BtnRegistration.IsEnabled = true;

                }
                else
                {
                    await DisplayAlert("Message", responseData.Message, "ok");
                    BtnRegistration.IsEnabled = true;
                    return;
                }

                BtnRegistration.IsEnabled = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message, "ok");
                BtnRegistration.IsEnabled = true;
                return;
            }



        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                await DisplayAlert("Input Error", "Customer Name is Required", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                await DisplayAlert("Input Error", "Customer Address is Required", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                await DisplayAlert("Input Error", "Customer Email Address is Required", "OK");
                return;
            }
            bool bEmail;
            bEmail = Regex.IsMatch(txtEmail.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (bEmail == false)
            {
                await DisplayAlert("Input Error", "Invalid Email Address.", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtMobile.Text))
            {
                await DisplayAlert("Input Error", "Customer Mobile Number is Required", "OK");
                return;
            }
            bool b;
            b = Regex.IsMatch(txtMobile.Text, @"^[7-9]\d{9}$");
            if (b == false)
            {
                await DisplayAlert("Input Error", "Invalid Mobile Number.", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Input Error", "Password is Required", "OK");
                return;
            }


            try
            {
                BtnUpdate.IsEnabled = false;
                User user = new User();
                user.Name = txtName.Text;
                user.Address = txtAddress.Text;
                user.Email = txtEmail.Text;
                user.Mobile = txtMobile.Text;
                user.Password = txtPassword.Text;


                string url = "http://127.0.0.1:8080/api/User/UpdateUser";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);
                if (responseData.Status == 1)
                {
                    await DisplayAlert("Message", responseData.Message, "ok");
                    BtnUpdate.IsEnabled = true;
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Message", responseData.Message, "ok");
                    BtnUpdate.IsEnabled = true;
                    return;
                }




            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message, "ok");
                BtnUpdate.IsEnabled = true;
                return;
            }

        }

    }
}