using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeApp.Models;
using RecipeApp.Views;
using System.Windows.Controls;

namespace RecipeApp.ViewModels
{
    public class RecipeViewModel : INotifyPropertyChanged
    {
        private Recipe _recipe;
        private string _step;
        private string _ingredient;
        private readonly NavigationViewModel _navigationViewModel;
        private readonly RecipeListViewModel _recipeListViewModel;

        //Commands
        public RelayCommand AddStepCommand { get; private set; }
        public RelayCommand DeleteStepCommand { get; private set; }
        public RelayCommand UpdateStepCommand { get; private set; }
        public RelayCommand AddIngredientCommand { get; private set; }
        public RelayCommand DeleteIngredientCommand { get; private set; }
        public RelayCommand SaveRecipeCommand { get; private set; }
        public RelayCommand CancelRecipeCommand { get; private set; }

        //Constructors
        public RecipeViewModel(RecipeListViewModel recipeListViewModel, NavigationViewModel navigationViewModel)
        {
            _recipe = new Recipe();
            AddStepCommand = new RelayCommand(AddStep, CanAddStep);
            DeleteStepCommand = new RelayCommand(DeleteStep, CanDeleteStep);
            UpdateStepCommand = new RelayCommand(UpdateStep, CanUpdateStep);
            AddIngredientCommand = new RelayCommand(AddIngredient, CanAddIngredient);
            DeleteIngredientCommand = new RelayCommand(DeleteIngredient, CanDeleteIngredient);
            SaveRecipeCommand = new RelayCommand(SaveRecipe, CanSaveRecipe);
            CancelRecipeCommand = new RelayCommand(CancelRecipe);
            _navigationViewModel = navigationViewModel;
            _recipeListViewModel = recipeListViewModel;
        }

        

        public Recipe Recipe
        {
            get { return _recipe; }
            set { _recipe = value; }
        }

        public string RecipeName
        {
            get { return _recipe.RecipeName; }
            set
            {
                _recipe.UpdateRecipeName(value);
                OnPropertyChange(nameof(RecipeName));
            }
        }

        public int PrepTime
        {
            get { return _recipe.PrepTime; }
            set
            {
                _recipe.UpdatePrepTime(value);
                OnPropertyChange(nameof(PrepTime));
            }
        }

        public int CookTime
        {
            get { return _recipe.CookTime; }
            set
            {
                _recipe.UpdateCookTime(value);
                OnPropertyChange(nameof(CookTime));
            }
        }

        //Ingredient properties
        public ObservableCollection<string> Ingredients
        {
            get { return _recipe.Ingredients.Ingredients; }
            set
            {
                _recipe.Ingredients = new Ingredient(value);
                OnPropertyChange(nameof(Ingredients));
            }
        }

        public string Ingredient
        {
            get { return _ingredient; }
            set
            {
                _ingredient = value;
                OnPropertyChange(nameof(Ingredient));
            }
        }

        //Step properties
        public ObservableCollection<string> Steps
        {
            get
            { return  _recipe.Steps.Steps; }
            set
            {
                _recipe.Steps = new Step(value);
                OnPropertyChange(nameof(Steps));
            }
        }

        public string Step
        {
            get { return _step; }
            set
            {
                _step = value;
                OnPropertyChange(nameof(Step));
            }    
        }
 
        //Command implementation
        //AddStep
        private void AddStep(object obj)
        {
            _recipe.Steps.AddStep(Step);
            TextBox textbox = obj as TextBox;
            textbox.Clear();
            textbox.Focus();
        }

        private bool CanAddStep(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Step) && !Steps.Contains(Step))
                return true;
            return false;
        }

        //UpdateStep
        private void UpdateStep(object obj)
        {
            string stepToUpdate = obj as string;
            _recipe.Steps.UpdateStep(stepToUpdate, Step);
        }

        private bool CanUpdateStep(object obj)
        {
            string step = obj as string;
            if (!string.IsNullOrWhiteSpace(step) && !Steps.Contains(Step))
            {
                return true;
            }
            return false;
        }

        //DeleteStep
        private void DeleteStep(object obj)
        {
            string step = obj as string;
            _recipe.Steps.RemoveStep(step);          
        }

        private bool CanDeleteStep(object obj)
        {
            string step = obj as string;
            if (!string.IsNullOrWhiteSpace(step))
                return true;
            return false;
        }

        //AddIngredient
        private void AddIngredient(object obj)
        {
            _recipe.Ingredients.AddIngredient(Ingredient);
            TextBox textbox = obj as TextBox;
            textbox.Clear();
            textbox.Focus();
        }

        private bool CanAddIngredient(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Ingredient) && !Ingredients.Contains(Ingredient))
                return true;
            return false;
        }

        //DeleteIngredient
        private void DeleteIngredient(object obj)
        {
            string ingredient = obj as string;
            _recipe.Ingredients.RemoveIngredient(ingredient);
        }

        private bool CanDeleteIngredient(object obj)
        {
            string ingredient = obj as string;
            if (!string.IsNullOrWhiteSpace(ingredient))
                return true;
            return false;
        }

        //SaveRecipe
        private void SaveRecipe(object obj)
        {
            _recipeListViewModel.AddRecipe(Recipe);
            RecipeBookState.Save(_recipeListViewModel);
            _recipeListViewModel.ClearStepIngedientLists();
            _navigationViewModel.SelectedViewModel = _recipeListViewModel;
        }

        private bool CanSaveRecipe(object obj)
        {
            if (!string.IsNullOrWhiteSpace(RecipeName) && !_recipeListViewModel.RecipeBook.Recipes.Contains(Recipe))
                return true;
            return false;
        }

        //CancelRecipe
        private void CancelRecipe(object obj)
        {
            _recipeListViewModel.ClearStepIngedientLists();
            _navigationViewModel.SelectedViewModel = _recipeListViewModel;
        }

        //INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
