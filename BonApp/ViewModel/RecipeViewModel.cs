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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace BonApp.ViewModel
{
    public class RecipeViewModel : ViewModelBase, INotifyPropertyChanged
    {
        AzureDataAccess data;

        private BitmapImage iconFav;

        public BitmapImage IconFav
        {
            get { return iconFav; }
            set
            {
                iconFav = value;
                RaisePropertyChanged("IconFav");
            }
        }


        private bool isFavorite;

        public bool IsFavorite
        {
            get { return isFavorite; }
            set { isFavorite = value; }
        }

        private ICommand _addToFavoriteCommand;
        public ICommand AddToFavoriteCommand
        {
            get
            {
                if (this._addToFavoriteCommand == null)
                {
                    this._addToFavoriteCommand = new RelayCommand(() => AddToFavorite());
                }
                return this._addToFavoriteCommand;
            }
        }

        private ICommand _removeFavoriteCommand;
        public ICommand RemoveFavoriteCommand
        {
            get
            {
                if (this._removeFavoriteCommand == null)
                {
                    this._removeFavoriteCommand = new RelayCommand(() => RemoveFavorite());
                }
                return this._removeFavoriteCommand;
            }
        }


        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                RaisePropertyChanged("SelectedRecipe");
            }
        }

        private Uri _uri;
        public Uri uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                RaisePropertyChanged("uri");
            }
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            SelectedRecipe = (Recipe)e.Parameter;
            uri = new Uri(SelectedRecipe.source_url);
            iconFav = new BitmapImage(new Uri("ms-appx:///Assets/Star-Empty.png"));
        }

        public async void AddToFavorite()
        {
            //if (data.AddToFavorite(SelectedRecipe)) {
            //    IToastText01 templateContent = ToastContentFactory.CreateToastText01();
            //    templateContent.TextBodyWrap.Text = "Body text that wraps over three lines";
            //    toastContent = templateContent;
            //}
            
            
                if (await data.AddToFavorite(SelectedRecipe))
                {
                    iconFav.UriSource = new Uri("ms-appx:///Assets/Star-Full.png");
                }
            
        }

        public RecipeViewModel()
        {
            data = new AzureDataAccess();
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var str = loader.GetString("recipe");
        }


        public void RemoveFavorite()
        {
            //if (data.AddToFavorite(SelectedRecipe)) {
            //    IToastText01 templateContent = ToastContentFactory.CreateToastText01();
            //    templateContent.TextBodyWrap.Text = "Body text that wraps over three lines";
            //    toastContent = templateContent;
            //}

            
        }
    }
}
