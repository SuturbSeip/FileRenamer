using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileRenamer
{
    public class ExtensionFilters
    {
        public string ExtensionFilter { get; set; }
        public bool IsActive { get; set; }

        public ExtensionFilters(string extensionFilter) 
        {
            ExtensionFilter = extensionFilter;
            IsActive = true;
        }
    }
}
