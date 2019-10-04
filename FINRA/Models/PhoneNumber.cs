using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FINRA.Models
{
    public class PermutationRequest
    {
        public string Number { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

    }
        

    
}