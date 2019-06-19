using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeApp.Views;
using RecipeApp.ViewModels;
using System.Xml.Serialization;
using System.IO;

namespace RecipeApp
{
   public static class RecipeBookState
    {
        public static void OpenRead(ref RecipeListViewModel recipeListVM, NavigationViewModel navigationViewModel)
        {
            if (recipeListVM == null)
            {
                if (File.Exists("RecipeBook.xml"))
                {
                    using (var stream = File.OpenRead("RecipeBook.xml"))
                    {
                        var serializer = new XmlSerializer(typeof(RecipeListViewModel));
                        recipeListVM = serializer.Deserialize(stream) as RecipeListViewModel;
                        recipeListVM.NavigationViewModel = navigationViewModel;
                    }
                }
                else
                    recipeListVM = new RecipeListViewModel(navigationViewModel);
            }
        }

        public static void Save(RecipeListViewModel recipeListVM)
        {
            using (var stream = File.Open("RecipeBook.xml", FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(RecipeListViewModel));
                serializer.Serialize(stream, recipeListVM);
            }
        }
    }
}
