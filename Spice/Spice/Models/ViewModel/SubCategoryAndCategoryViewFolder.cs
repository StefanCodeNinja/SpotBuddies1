using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotBuddies.Models.ViewModels
{
    public class SubCategoryAndCategoryViewFolder
    {
        public IEnumerable<Category> CategoryList { get; set; }

        public SubCategory SubCategory  { get; set; }

        public List<String> SubCategoryList { get; set; }

        public string StatusMessage { get; set; }
    }
}
