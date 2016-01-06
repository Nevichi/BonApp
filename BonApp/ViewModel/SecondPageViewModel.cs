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

namespace BonApp.ViewModel
{
    public class SecondPageViewModel : ViewModelBase, INotifyPropertyChanged
    {


        private ICommand _searchRecipeCommand;
        private ICommand _favoriteRecipeCommand;
        private ICommand _loginCommand;

        private INavigationService _navigationService;

        public SecondPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var strSearch = loader.GetString("searchTitle");
            var strHome = loader.GetString("homeTitle");
            var strFav = loader.GetString("favoritesTitle");
            var strLogin = loader.GetString("loginTitle");
        }

        public ICommand searchRecipeCommand
        {
            get
            {
                if (this._searchRecipeCommand == null)
                {
                    this._searchRecipeCommand = new RelayCommand(() => SearchRecipeNavigate());
                }
                return this._searchRecipeCommand;
            }
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

        private void subscribe()
        {
            _navigationService.NavigateTo("Subscribe");
        }

        private void login()
        {
            _navigationService.NavigateTo("Login");
        }

        private void SearchRecipeNavigate()
        {
            _navigationService.NavigateTo("SearchRecipe");
        }


        public ICommand favoriteRecipeCommand
        {
            get
            {
                if (this._favoriteRecipeCommand == null)
                {
                    this._favoriteRecipeCommand = new RelayCommand(() => FavoriteRecipeNavigate());
                }
                return this._favoriteRecipeCommand;
            }


        }

        private void FavoriteRecipeNavigate()
        {
            _navigationService.NavigateTo("ListFavorites");
        }
    }
}
