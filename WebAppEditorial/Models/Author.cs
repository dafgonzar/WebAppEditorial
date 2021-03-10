using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEditorial.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
	    public string AuthorFullName { get; set; }
        public string AuthorDateBirth { get; set; }
        public string AuthorCity { get; set; }
        public string AuthorEmail { get; set; }
    }
}
