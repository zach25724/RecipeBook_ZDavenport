using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Models
{
    public class Recipe : ICloneable, IEquatable<Recipe>, IComparable<Recipe>
    {
        #region fields
        private string _recipeName;
        private int _prepTime;
        private int _cookTime;
        private Ingredient _ingredients;
        private Step _steps;
        private Comment _comments;
        #endregion

        #region properties
        public string RecipeName
        {
            get { return _recipeName; }
            set { _recipeName = value; }
        }
        public int PrepTime
        {
            get { return _prepTime; }
            set { _prepTime = value; }
        }
        public int CookTime
        {
            get { return _cookTime; }
            set { _cookTime = value; }
        }
        public Ingredient Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; }
        }
        public Step Steps
        {
            get { return _steps; }
            set { _steps = value; }
        }

        public Comment Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
        #endregion

        public Recipe()
        {
            _recipeName = "";
            _prepTime = 0;
            _cookTime = 0;
            _ingredients = new Ingredient();
            _steps = new Step();
            _comments = new Comment();
        }

        public Recipe(string name = "", int prepTime = 0, int cookTime = 0,
                       Ingredient ingredients = null, Step steps = null, Comment comments = null)
        {
            _recipeName = AddRecipeName(name);
            _prepTime = AddTimeInMin(prepTime);
            _cookTime = AddTimeInMin(cookTime);
            _ingredients = new Ingredient(ingredients);
            _steps = new Step(steps);
            _comments = new Comment(comments);
        }

        public Recipe(Recipe otherRecipe)
        {
            if(otherRecipe != null)
            {
                _recipeName = AddRecipeName(otherRecipe.RecipeName);
                _prepTime = AddTimeInMin(otherRecipe.PrepTime);
                _cookTime = AddTimeInMin(otherRecipe.CookTime);
                _ingredients = new Ingredient(otherRecipe.Ingredients);
                _steps = new Step(otherRecipe.Steps);
                _comments = new Comment(otherRecipe.Comments);
            }
        }

        //Add recipe name to recipe
        private string AddRecipeName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return CapitalizeFirstLetters(name);
            }
            else
                return "";
        }

        //Update recipe name
        public void UpdateRecipeName(string name)
        {
            _recipeName = AddRecipeName(name);
        }

        //Add times in minutes to recipe
        private int AddTimeInMin(int time)
        {
            if (time >= 0)
                return time;
            return 0;
        }

        //Update prep time with new time
        public void UpdatePrepTime(int time)
        {
            _prepTime = AddTimeInMin(time);
        }

        //Update cook time with new time
        public void UpdateCookTime(int time)
        {
            _cookTime = AddTimeInMin(time);
        }

        //Remove recipe and set objects to null
        public void DeleteRecipe()
        {
            _recipeName = "";
            _prepTime = 0;
            _cookTime = 0;
            _ingredients.RemoveAllIngredients();
            _ingredients = null;
            _steps.RemoveAllSteps();
            _steps = null;
            _comments.RemoveAllComments();
            _comments = null;
        }


        //Capitalize first letter of each word
        private string CapitalizeFirstLetters(string name)
        {
            string[] nameList = name.Trim().Split(' ');
            int count = 0;
            int listLength = nameList.Length;
            string returnString = "";

            foreach(string s in nameList)
            {
                if (string.IsNullOrWhiteSpace(s))
                    continue;
                returnString += char.ToUpper(s[0]).ToString() + s.Substring(1, s.Length - 1);
                if (count++ < listLength - 1)
                    returnString += " ";
            }
            return returnString;
        }

        //Overloaded Equals()
        public override bool Equals(object obj)
        {
            if(obj != null && obj is Recipe)
            {
                Recipe otherRecipe = obj as Recipe;
                return Equals(otherRecipe);
            }
            return false;
        }

        //IEquateable<T> Equals()
        public bool Equals(Recipe other)
        {
            if(other != null)
            {
                return (string.Compare(this._recipeName, other._recipeName, true) == 0);
            }
            return false;
        }

        //Hashcode method ---Having problem where it eventually sets hashValue*= to zero---
        public override int GetHashCode()
        {
            int hashValue = 1;
            foreach (char c in _recipeName)
            {
                hashValue += Convert.ToInt32(c);
            }
            return Math.Abs(hashValue);
        }

        //Overloaded == operator
        public static bool operator==(Recipe leftRecipe, Recipe rightRecipe)
        {
            return leftRecipe?.Equals(rightRecipe) ?? false;
        }

        //Overloaded != operator
        public static bool operator!=(Recipe leftRecipe, Recipe rightRecipe)
        {
            return !leftRecipe?.Equals(rightRecipe) ?? false;
        }

        //Overloaded > operator
        public static bool operator>(Recipe leftRecipe, Recipe rightRecipe)
        {
            if (string.Compare(leftRecipe._recipeName, rightRecipe._recipeName, true) > 0)
                return true;
            else
                return false;
        }

        //Overloaded < operator
        public static bool operator<(Recipe leftRecipe, Recipe rightRecipe)
        {
            if (string.Compare(leftRecipe._recipeName, rightRecipe._recipeName, true) < 0)
                return true;
            else
                return false;
        }

        //IComparable interface for sorting recipes by recipeName
        public int CompareTo(Recipe other)
        {
            if (other != null)
            {
                if (this < other)
                    return -1;
                else if (this > other)
                    return 1;
                else
                    return 0;
            }
            else
                throw new ArgumentException("Object is not of type Recipe");
        }

        //Display recipe
        public override string ToString()
        {
            if (_ingredients != null && _steps != null && _comments != null)
            {
                return ("Recipe Name: " + _recipeName + "\n\nPrep Time: " + _prepTime + " minutes\nCook Time: " +
                            _cookTime + " minutes\n\nIngredients:\n" + _ingredients.ToString() +
                            "\nSteps:\n" + _steps.ToString() + "\nComments:\n" + _comments.ToString());
            }
            return "";
        }

        //Deep copy recipe
        public object Clone()
        {
            Recipe temp = new Recipe(this._recipeName, this._prepTime, this._cookTime,
                                        this._ingredients, this._steps, this._comments);
            return temp;
        }
    }
}
