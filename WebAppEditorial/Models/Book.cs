using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEditorial.Models
{
    public class Book
    {
		public int BookId { get; set; }
		public string BookTitle { get; set; }
		public int BookYear { get; set; }
		public string BookGender { get; set; }
		public string BookPages { get; set; }
		public int BookEditorialId { get; set; }
		public int BookAuthorId { get; set; }

	}
}
