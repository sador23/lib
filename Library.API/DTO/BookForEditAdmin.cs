using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.DTO
{
    public class BookForEditAdmin
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string ImagePath { get; set; }

        public string status { get; set; }
    }
}
