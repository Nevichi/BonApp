using BonApp.Data;
using BonApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace BonApp.ViewModel
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        AzureDataAccess data;

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

        private ICommand _loginCommand;
        public ICommand LoginCommand
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

        public async void login() {
            String res = await data.FindUser(UserInput, PasswordInput);
            if (res.Equals("success"))
            {
                //retour sur la page principale
            }
            else {
                //échec
            }
        }


        public LoginViewModel()
        {
            data = new AzureDataAccess();
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var strLoginInput = loader.GetString("loginInput");
            var strLoginTitle = loader.GetString("loginTitle");
            var strPassword = loader.GetString("passwordInput");
        }


        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
