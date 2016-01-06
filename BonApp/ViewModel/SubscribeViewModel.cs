using BonApp.Data;
using BonApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NotificationsExtensions.Toasts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace BonApp.ViewModel
{
    public class SubscribeViewModel : ViewModelBase, INotifyPropertyChanged
    {

        AzureDataAccess data;
        private INavigationService _navigationService;

        private String _userInput;
        public String UserInput { get
            { return _userInput; }
            set
            {
                _userInput = value;
                RaisePropertyChanged("UserInput");
                
            } }

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

        private String _passwordConfInput;
        public String PasswordConfInput
        {
            get { return _passwordConfInput; }
            set
            {
                _passwordConfInput = value;
                RaisePropertyChanged("PasswordConfInput");
            }
        }

        private ICommand _subCommand;
        public ICommand SubCommand
        {
            get
            {

                if (this._subCommand == null)
                {
                    this._subCommand = new RelayCommand(() => sub());
                }
                return this._subCommand;
            }
        }

        public async void sub()
        {
            if ((_userInput == null) || (_passwordInput == null) || (_passwordConfInput==null) || (_userInput.Equals("") || _passwordInput.Equals("")))
            {
                createToast("errorField");
            }
            else
            { 
                if (_passwordInput.Equals(_passwordConfInput))
                {
                    String resSub = await data.createUser(_userInput, _passwordInput);
                    if (resSub.Equals("success"))
                    {
                        String resFind = await data.FindUser(_userInput, _passwordInput);
                        if (resFind.Equals("success"))
                        {
                            _navigationService.NavigateTo("SecondPage");
                        }
                    }
                    else {
                        createToast(resSub);
                    }
                }
                else {
                    createToast("errorPassword");
                }
            }
        }


        public SubscribeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            data = new AzureDataAccess();
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var strLoginInput = loader.GetString("loginInput");
            var strLoginTitle = loader.GetString("loginTitle");
            var strPassword = loader.GetString("passwordInput");
            var strPasswordConf = loader.GetString("passwordConfInput");
        }


        public void OnNavigatedTo(NavigationEventArgs e)
        {
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
    }
}
