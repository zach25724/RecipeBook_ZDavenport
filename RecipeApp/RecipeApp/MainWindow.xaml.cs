﻿
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
using System.Windows.Shapes;
using RecipeApp.Views;
using RecipeApp.ViewModels;
using System.Xml.Serialization;
using System.IO;


namespace RecipeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RecipeListViewModel recipeListVM;

        public MainWindow()
        {
            InitializeComponent();
            NavigationViewModel NavigationVM = new NavigationViewModel();
            RecipeBookState.OpenRead(ref recipeListVM, NavigationVM);
            NavigationVM.SelectedViewModel = recipeListVM;
            this.DataContext = NavigationVM;
        }
    }
}

        

        
    

