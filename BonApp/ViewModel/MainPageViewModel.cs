using BonApp.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Notifications;
using NotificationsExtensions.Toasts;
using Microsoft.QueryStringDotNET;

namespace BonApp.ViewModel
{
    public class MainPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        AzureDataAccess data;

        private ICommand _loginCommand;

        

        private INavigationService _navigationService;
        

        public MainPageViewModel(INavigationService navigationService)
        {
            data = new AzureDataAccess();
            _navigationService = navigationService;
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var strHome = loader.GetString("homeTitle");
            var strOr = loader.GetString("orText");
            var strLogin = loader.GetString("loginInput");
            var strPassword = loader.GetString("passwordInput");
            var strSub = loader.GetString("subscribeTitle");
            var strLoginTitle = loader.GetString("loginTitle");
        }



        public ICommand loginCommand
        {
            get
            {
                if (this._loginCommand == null)
                {
                    this._loginCommand = new RelayCommand(() => login());
                }
                return this._loginCommand;
            }
        }

        public ICommand _subscribeCommand { get; set; }
        public ICommand subscribeCommand
        {
            get
            {
                if (this._subscribeCommand == null)
                {
                    this._subscribeCommand = new RelayCommand(() => subscribe());
                }
                return this._subscribeCommand;
            }
        }

        private String _userInput;
        public String UserInput
        {
            get { return _userInput; }
            set
            {
                _userInput = value;
                RaisePropertyChanged("UserInput");
            }
        }

        private String _passwordInput;
        public String PasswordInput
        {
            get { return _passwordInput; }
            set
            {
                _passwordInput = value;
                RaisePropertyChanged("PasswordInput");
            }
        }


        private void subscribe() {
            _navigationService.NavigateTo("Subscribe");
        }

        private async void login()
        {
            if((_userInput== null) || (_passwordInput== null) || (_userInput.Equals("") || _passwordInput.Equals(""))) {
                createToast("errorField");
            }
            else
            {
                String res = await data.FindUser(_userInput, _passwordInput);
                if (res.Equals("success"))
                {
                    _navigationService.NavigateTo("SecondPage");
                }
                else
                {
                    createToast(res);
                }
            }
        }

        public void createToast(String value)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            ToastVisual visual = new ToastVisual()
            {
                TitleText = new ToastText()
                {

                    Text = loader.GetString(value)
                },
            };

            ToastContent toastContent = new ToastContent();
            toastContent.Visual = visual;
            var toast = new ToastNotification(toastContent.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }


        public void OnNavigateTo() {
            Windows.Storage.ApplicationDataContainer localSetting = Windows.Storage.ApplicationData.Current.LocalSettings;
            if ((int)localSetting.Values["userid"] != 0)
            {
                _navigationService.NavigateTo("SecondPage");
            }
            
        }
    }
}
