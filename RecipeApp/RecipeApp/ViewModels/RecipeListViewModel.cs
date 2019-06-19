using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using System.IO;
using RecipeApp.Models;

namespace RecipeApp.ViewModels
{
    [Serializable]
    public class RecipeListViewModel : INotifyPropertyChanged
    {
        private RecipeBook _recipeBook;
        private Recipe _currentRecipe;
        private ObservableCollection<NumberedStep> _steps;
        private ObservableCollection<string> _ingredientsLeft;
        private ObservableCollection<string> _ingredientsRight;
        private NavigationViewModel _navigationViewModel;       

        //Commands
        [XmlIgnore]
        public RelayCommand AddRecipeViewCommand { get; private set; }

        //Constructors
        public RecipeListViewModel()
        {
            RecipeBook = new RecipeBook();
            _steps = new ObservableCollection<NumberedStep>();
            _ingredientsLeft = new ObservableCollection<string>();
            _ingredientsRight = new ObservableCollection<string>();
            AddRecipeViewCommand = new RelayCommand(OpenAddRecipeView);
        }

        public RecipeListViewModel(NavigationViewModel navigationViewModel) :this()
        {
            _navigationViewModel = navigationViewModel;
        }

        //Properties
        public RecipeBook RecipeBook
        {
            get { return _recipeBook; }
            set { _recipeBook = value; }
        }

        [XmlIgnore]
        public ObservableCollection<Recipe> RecipeList
        {
            get
            {
                return new ObservableCollection<Recipe>(RecipeBook.Recipes);
            }
        }

        [XmlIgnore]
        public Recipe CurrentRecipe
        {
            get { return _currentRecipe; }
            set
            {
                _currentRecipe = value;
                NumberSteps();
                UpdateIngredients();
                OnPropertyChange(nameof(CurrentRecipe));
            }
        }

        [XmlIgnore]
        public ObservableCollection<NumberedStep> Steps
        {
            get { return _steps; }        
        }

        [XmlIgnore]
        public ObservableCollection<string> IngredientsLeft
        {
            get { return _ingredientsLeft; }
        }

        [XmlIgnore]
        public ObservableCollection<string> IngredientsRight
        {
            get { return _ingredientsRight; }
        }

        [XmlIgnore]
        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set { _navigationViewModel = value; }
        }

        //Number list of steps for display
        private void NumberSteps()
        {
            if (CurrentRecipe != null)
            {
                _steps.Clear();
                for (int i = 0; i < CurrentRecipe.Steps.Steps.Count; i++)
                {
                    _steps.Add(new NumberedStep((i + 1), CurrentRecipe.Steps.Steps[i]));
                }
            }
        }

        //Update list of ingredients for display
        private void UpdateIngredients()
        {
            if (CurrentRecipe != null)
            {
                _ingredientsLeft.Clear();
                _ingredientsRight.Clear();
                for(int i = 0; i < CurrentRecipe.Ingredients.Ingredients.Count; i=i+2)
                {
                    _ingredientsLeft.Add(CurrentRecipe.Ingredients.Ingredients[i]);
                    if((i + 1) < CurrentRecipe.Ingredients.Ingredients.Count)
                        _ingredientsRight.Add(CurrentRecipe.Ingredients.Ingredients[i + 1]);
                }
            }
        }

        public void ClearStepIngedientLists()
        {
            Steps.Clear();
            IngredientsLeft.Clear();
            IngredientsRight.Clear();
        }

        //Add recipe to RecipeBook
        public void AddRecipe(Recipe recipe)
        {
            RecipeBook.AddRecipe(recipe);
            RecipeBook.Sort();
        }

        //Command implementation
        private void OpenAddRecipeView(object obj)
        {
            _navigationViewModel.SelectedViewModel = new RecipeViewModel(this, _navigationViewModel);
        }

        //INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
