using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipe2.Data
{
    public class Source
    {
        [Key]
        public int SourceId { get; set; }
        public string SName { get; set; }
        public string SOrigin { get; set; }
        public Guid OwnerId { get; set; }

    }
}
