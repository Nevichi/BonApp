using BonApp.Data;
using BonApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NotificationsExtensions.Toasts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace BonApp.ViewModel
{
    public class FavoriteViewModel : ViewModelBase, INotifyPropertyChanged
    {
        AzureDataAccess data;
        
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
        }


        public FavoriteViewModel()
        {
            data = new AzureDataAccess();
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var str = loader.GetString("recipe");
        }


        public async void RemoveFavorite()
        {

            if (await data.RemoveFavorite(SelectedRecipe)) {
                createToast("recipeRemoved");
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
    }
}
