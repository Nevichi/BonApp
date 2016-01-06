using BonApp.Data;
using BonApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BonApp.ViewModel
{
    public class ListFavoritesViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Recipe> _recipes;
        private Recipe _selectedRecipe;
        private INavigationService _navigationService;
        AzureDataAccess data;


        private ICommand _showRecipeCommand;
        public ICommand ShowRecipeCommand
        {
            get
            {
                if (this._showRecipeCommand == null)
                {
                    this._showRecipeCommand = new RelayCommand(() => ShowRecipe());
                }
                return this._showRecipeCommand;
            }
        }

        [PreferredConstructor]
        public ListFavoritesViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
            data = new AzureDataAccess();
            Recipes = new ObservableCollection<Recipe>();
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var str = loader.GetString("favRecipeDesc");
            //GetAllRecipes();

        }

        public ObservableCollection<Recipe> Recipes
        {
            get { return _recipes; }
            set
            {
                _recipes = value;
                RaisePropertyChanged("Recipes");
            }
        }

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                if (_selectedRecipe != null)
                {
                    RaisePropertyChanged("SelectedRecipe");
                }
            }
        }

        private void ShowRecipe()
        {
            //élément sélectionné?
            if (CanExecute())
            {
                _navigationService.NavigateTo("Favorite", SelectedRecipe);
            }
        }

        public bool CanExecute()
        {
            return (SelectedRecipe == null) ? false : true; //renvoie false
        }

        public void OnNavigatedTo()
        {
            GetAllRecipes();
        }

        public async void GetAllRecipes()
        {

            List<Recipe> listRecipes = await data.GetRecipeFavorite();

            foreach (var item in listRecipes)
            {
                Recipes.Add(item);
            }
        }
    }

}
