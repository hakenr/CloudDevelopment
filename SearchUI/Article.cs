using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SearchUI
{
    public class Article
    {
        public Article()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Text { get; set; }

        //[JsonIgnore]
        //public double Score { get; set; }
    }
}
