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
using Windows.UI.Core;

namespace BonApp.ViewModel
{
    public class ListRecipesViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Recipe> _recipes;
        private Recipe _selectedRecipe;
        private INavigationService _navigationService;
        F2fDataAccess data;

        private ICommand _showRecipeCommand;
        public ICommand ShowRecipeCommand
        {
            get
            {
                if(this._showRecipeCommand == null)
                {
                    this._showRecipeCommand = new RelayCommand(() => ShowRecipe());
                }
                return this._showRecipeCommand;
            }
        }

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                if(_selectedRecipe != null)
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
                _navigationService.NavigateTo("Recipe", SelectedRecipe);
            }
        }

        public bool CanExecute()
        {
            return (SelectedRecipe == null) ? false : true; //renvoie false
        }



        [PreferredConstructor]
        public ListRecipesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            data = new F2fDataAccess();
            Recipes = new ObservableCollection<Recipe>();
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var str = loader.GetString("searchDesc");
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


        public void OnNavigatedTo()
        {
            

            GetAllRecipes();
        }

        

        public async void GetAllRecipes()
        {
            List<Recipe> listRecipes = await data.GetAllRecipes("");
            
            foreach (var item in listRecipes)
            {
                Recipes.Add(item);
            }
        }
    }
}
