using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Models
{
    public class Comment :ICloneable
    {
        private List<string> _comments;

        public List<string> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public Comment()
        {
            _comments = new List<string>();
        }

        public Comment(List<string> list) : this()
        {
            if (list != null && list.Count > 0)
            {
                foreach (string s in list)
                {
                    AddComment(s);
                }
            }
        }

        public Comment(Comment comments) : this()
        {
            if (comments != null && comments.Comments.Count > 0)
            {
                foreach (string s in comments.Comments)
                {
                    AddComment(s);
                }
            }
        }

        //Add new comment to list of comments
        public void AddComment(string comment)
        {
            if (_comments == null)
                _comments = new List<string>();
            if (!string.IsNullOrWhiteSpace(comment))
            {
                _comments.Add(comment.Trim());
            }
        }

        //Update current comment in list of comments
        public void UpdateComment(string commentToUpdate, string updatedComment)
        {
            if (_comments != null && !string.IsNullOrWhiteSpace(updatedComment))
            {
                if (_comments.Contains(commentToUpdate))
                {
                    int index = _comments.IndexOf(commentToUpdate);
                    _comments[index] = updatedComment;
                }
            }
        }

        //Remove comment from list of comments
        public void RemoveComment(string comment)
        {
            if (!string.IsNullOrWhiteSpace(comment))
            {
                if (_comments != null && _comments.Count > 0)
                {
                    foreach (string s in _comments)
                    {
                        if (string.Compare(s, comment, true) == 0)
                        {
                            _comments.Remove(comment);
                            break;
                        }
                    }
                }
            }
        }

        //Remove all comments from list of comments
        public void RemoveAllComments()
        {
            if (_comments != null)
            {
                _comments.Clear();
                _comments = null;
            }
        }

        //Enumerate over list
        public IEnumerator<string> GetEnumerator()
        {
            foreach (string s in _comments)
            {
                yield return s;
            }
        }

        //Display comments
        public override string ToString()
        {
            if (_comments != null)
            {
                StringBuilder list = new StringBuilder();
                int count = 1;

                foreach (string s in _comments)
                {
                    list.Append(count++ + ". " + s + "\n");
                }
                return (list.ToString());
            }
            return "";
        }

        //Deep copy comments
        public object Clone()
        {
            Comment temp = new Comment(this._comments);
            return temp;
        }
    }
}
