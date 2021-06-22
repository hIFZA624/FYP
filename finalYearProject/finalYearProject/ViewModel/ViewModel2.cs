using finalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finalYearProject.ViewModel
{
    public class ViewModel2
    {
        public IEnumerable<CommentBox> Comment { get; set; }
        public CommentBox commentBox { get; set; }
    }
}
