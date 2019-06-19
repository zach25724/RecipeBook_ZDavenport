using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Models
{
    public class NumberedStep
    {
        private int _number;
        private string _step;

        public int Number
        { get { return _number; } set { _number = value; } }
        public string Step { get { return _step; } set { _step = value; } }

        public NumberedStep(int n, string s)
        {
            Number = n;
            Step = s;
        }
    }
}
