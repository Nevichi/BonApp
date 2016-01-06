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

namespace BonApp.ViewModel
{
    public class SearchRecipeViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ICommand _listRecipesCommand;
        private INavigationService _navigationService;

        public SearchRecipeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var strSearchDesc = loader.GetString("searchDesc");
            var strSearchInput = loader.GetString("searchInput");
            var strSearch = loader.GetString("searchTitle");
        }

        public ICommand ListRecipesCommand
        {
            get
            {
                if (this._listRecipesCommand == null)
                {
                    this._listRecipesCommand = new RelayCommand(() => ListRecipesNavigate());
                }
                return this._listRecipesCommand;
            }
        }

        private void ListRecipesNavigate()
        {
            _navigationService.NavigateTo("ListRecipes");
        }


    }
}
