using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class Book
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string ImagePath { get; set; }

        public User user { get; set; }

        public override bool Equals(Object other)
        {
            try
            {
                var item = other as Book;
                if (item == null) return false;
                if (item.ISBN.Equals(this.ISBN)) return true;
            }catch(Exception ex)
            {
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
