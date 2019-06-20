using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Models
{
    public class RecipeBook
    {
        private List<Recipe> _recipes;

        public List<Recipe> Recipes
        {
            get { return _recipes; }
            private set { _recipes = value; }
        }

        public RecipeBook()
        {
            _recipes = new List<Recipe>();
        }

        public RecipeBook(List<Recipe> list) : this()
        {
            if (list != null)
            {
                if (list.Count > 0)
                {
                    foreach (Recipe r in list)
                    {
                        AddRecipe(r);
                    }
                }
            }
        }

        public RecipeBook(RecipeBook otherBook) : this()
        {
            if(otherBook != null && otherBook.Recipes.Count > 0)
            {
                foreach(Recipe r in otherBook)
                {
                    AddRecipe(r);
                }
            }
        }

        //Recipe book indexer
        public Recipe this[int recipeIndex]
        {
            get { return _recipes[recipeIndex] as Recipe; }
            set { _recipes[recipeIndex] = value; }
        }

        //Add recipe to book  
        public void AddRecipe(Recipe recipe)
        {
            if (_recipes == null)
                _recipes = new List<Recipe>();
            if (!_recipes.Contains(recipe))
            {
                _recipes.Add(recipe);
            }
        }

        //Update recipe in book
        public void UpdateRecipe(Recipe currentRecipe, Recipe newRecipe)
        {
            if (_recipes.Contains(currentRecipe) && !_recipes.Contains(newRecipe))
            {
                int index = _recipes.IndexOf(currentRecipe);
                _recipes[index] = newRecipe;
            }
        }

        //Remove recipe from book
        public void RemoveRecipe(ref Recipe recipe)
        {
            if(_recipes != null && _recipes.Count > 0)
            {
                if(recipe != null && _recipes.Contains(recipe))
                {
                    _recipes.Remove(recipe);
                    recipe.DeleteRecipe();
                }
                if (_recipes.Count == 0)
                    _recipes = null;
            }
        }

        //Remove all recipes from book
        public void RemoveAllRecipes()
        {
            if (_recipes != null)
            {
                foreach (Recipe r in _recipes)
                {
                    r.DeleteRecipe();
                }
                _recipes.Clear();
                _recipes = null;
            }
        }

        //Enumerate over recipe book
        public IEnumerator<Recipe> GetEnumerator()
        {
            foreach(Recipe r in this._recipes)
            {
                yield return r;
            }
        }

        //Sort recipes in alphabetical order
        public void Sort()
        {
            this._recipes.Sort();
        }      
    }
}


