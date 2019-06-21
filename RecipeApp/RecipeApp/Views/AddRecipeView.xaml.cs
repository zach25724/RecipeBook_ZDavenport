using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RecipeApp.ViewModels;

namespace RecipeApp.Views
{
    /// <summary>
    /// Interaction logic for AddRecipeView.xaml
    /// </summary>
    public partial class AddRecipeView : UserControl
    {
        public AddRecipeView()
        {
            InitializeComponent();
        }

        //Enter key pressed - Step command
        private void StepTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                AddStepButton.Command.Execute(sender);
            }
        }

        //Step list box selected item focus changed - populates StepTextBox
        private void StepList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StepTextBox.Text = StepList.SelectedItem as string;
        }

        //Enter key pressed - Ingredient command
        private void IngredientTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddIngredientButton.Command.Execute(sender);
            }
        }

        //Select text of Prep and Cook times on textbox focus
        private void PrepTimeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.SelectAll();
        }

        private void CookTimeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.SelectAll();
        }
    }
}
