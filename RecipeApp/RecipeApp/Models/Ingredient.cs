using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Models
{
    public class Ingredient :ICloneable
    {
        private ObservableCollection<string> _ingredients;

        public ObservableCollection<string> Ingredients
        {
            get { return _ingredients; }
            private set { _ingredients = value; }
        }

        public Ingredient()
        {
            _ingredients = new ObservableCollection<string>();
        }

        public Ingredient(ObservableCollection<string> list) : this()
        {
            if (list != null && list.Count > 0)
            {
                foreach (string s in list)
                {
                    AddIngredient(s);
                }
            }
        }

        public Ingredient(Ingredient ingredients) : this()
        {
            if (ingredients != null && ingredients.Ingredients.Count > 0)
            {
                foreach(string s in ingredients.Ingredients)
                {
                    AddIngredient(s);
                }
            }
        }

        //Add new ingredient to list of ingredients
        public void AddIngredient(string ingredient)
        {
            if (_ingredients == null)
                _ingredients = new ObservableCollection<string>();
            if (!string.IsNullOrWhiteSpace(ingredient))
            {
                if (_ingredients != null)
                {
                    foreach (string s in _ingredients)
                    {
                        if (string.Compare(s, ingredient, true) == 0)
                            return;
                    }
                }
                _ingredients.Add(ingredient.Trim());
            }
        }

        //Update current ingredient in list of ingredients
        public void UpdateIngredient(string ingedientToUpdate, string updatedIngredient)
        {
            if (_ingredients != null && !string.IsNullOrWhiteSpace(updatedIngredient))
            {
                if (_ingredients.Contains(ingedientToUpdate))
                {
                    int index = _ingredients.IndexOf(ingedientToUpdate);
                    _ingredients[index] = updatedIngredient;
                }
            }
        }

        //Remove ingredient from list of ingredients
        public void RemoveIngredient(string ingredient)
        {
            if (!string.IsNullOrWhiteSpace(ingredient))
            {
                if (_ingredients != null && _ingredients.Count > 0)
                {
                    foreach (string s in _ingredients)
                    {
                        if (string.Compare(s, ingredient, true) == 0)
                        {
                            _ingredients.Remove(ingredient);
                            break;
                        }                           
                    }
                }
            }
        }

        //Remove last ingredient entered
        public void RemoveLastIngredient()
        {
            if (_ingredients != null && _ingredients.Count > 0)
            {
                _ingredients.RemoveAt(_ingredients.Count - 1);
            }
        }

        //Remove all ingredients from list of ingredients
        public void RemoveAllIngredients()
        {
            if (_ingredients != null)
            {
                _ingredients.Clear();
                _ingredients = null;
            }
        }

        //Enumerate over list
        public IEnumerator<string> GetEnumerator()
        {
            foreach(string s in _ingredients)
            {
                yield return s;
            }
        }
      
        //Display ingredients
        public override string ToString()
        {
            if (_ingredients != null)
            {
                StringBuilder list = new StringBuilder();
                foreach (string s in _ingredients)
                {
                    list.Append(s + "\n");
                }
                return (list.ToString());
            }
            return "";
        }

        //Deep copy ingredients
        public object Clone()
        {
            Ingredient temp = new Ingredient(this._ingredients);
            return temp;
        }
    }
}
