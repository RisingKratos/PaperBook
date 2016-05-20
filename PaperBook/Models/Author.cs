using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaperBook.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}