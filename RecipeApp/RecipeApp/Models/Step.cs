using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Models
{
    public class Step : ICloneable
    {
        private ObservableCollection<string> _steps;

        public ObservableCollection<string> Steps
        {
            get { return _steps; }
            private set { _steps = value; }
        }

        public Step()
        {
            _steps = new ObservableCollection<string>();
        }

        public Step(ObservableCollection<string> list) : this()
        {
            if (list != null && list.Count > 0)
            {
                foreach (string s in list)
                {
                    AddStep(s);
                }
            }
        }

        public Step(Step steps) : this()
        {
            if(steps != null && steps.Steps.Count > 0)
            {
                foreach(string s in steps.Steps)
                {
                    AddStep(s);
                }
            }
        }

        //Add new step to list of steps
        public void AddStep(string step)
        {
            if (_steps == null)
                _steps = new ObservableCollection<string>();
            if (!string.IsNullOrWhiteSpace(step))
            {
                foreach (string s in _steps)
                {
                    if (string.Compare(s, step, true) == 0)
                        return;
                }
                _steps.Add(step.Trim());
            }
        }

        //Insert new step into list of steps, before selected step
        public void InsertStep(string insertBefore, string stepToInsert)
        {
            if (_steps != null && !string.IsNullOrWhiteSpace(stepToInsert))
            {
                if (_steps.Contains(insertBefore))
                {
                    int index = _steps.IndexOf(insertBefore);
                    _steps.Insert(index, stepToInsert);
                }
            }
        }

        //Update current step in list of steps
        public void UpdateStep(string stepToUpdate, string updatedStep)
        {
            if (_steps != null && !string.IsNullOrWhiteSpace(updatedStep))
            {
                if (_steps.Contains(stepToUpdate))
                {
                    int index = _steps.IndexOf(stepToUpdate);
                    _steps[index] = updatedStep;
                }
            }
        }

        //Remove step from list of steps
        public void RemoveStep(string step)
        {
            if (!string.IsNullOrWhiteSpace(step))
            {
                if (_steps != null && _steps.Count > 0)
                {
                    foreach (string s in _steps)
                    {
                        if (string.Compare(s, step, true) == 0)
                        {
                            _steps.Remove(step);
                            break;
                        }
                    }
                }
            }
        }

        //Remove last step entered
        public void RemoveLastStep()
        {
            if (_steps != null && _steps.Count > 0)
            {
                _steps.RemoveAt(_steps.Count - 1);
            }
        }

        //Remove all steps from list of steps
        public void RemoveAllSteps()
        {
            if (_steps != null)
            {
                _steps.Clear();
                _steps = null;
            }
        }

        //Enumerate over list
        public IEnumerator<string> GetEnumerator()
        {
            foreach (string s in _steps)
            {
                yield return s;
            }
        }

        //Display steps
        public override string ToString()
        {
            if (_steps != null)
            {
                StringBuilder list = new StringBuilder();
                int count = 1;

                foreach (string s in _steps)
                {
                    list.Append(count++ + ". " + s + "\n");
                }
                return (list.ToString());
            }
            return "";
        }

        //Deep copy steps
        public object Clone()
        {
            Step temp = new Step(this._steps);
            return temp;
        }
    }
}
