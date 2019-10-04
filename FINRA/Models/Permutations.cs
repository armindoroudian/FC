using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINRA.Models
{
    public static class AlphaNumericList
    {
        public static List<List<string>> List = new List<List<string>>
            {
            new List<string>() {"0"},
            new List<string>() {"1"},
            new List<string>(){ "2","a","b","c"},
            new List<string>(){ "3", "d","e","f"},
            new List<string>(){ "4", "g","h","i"},
            new List<string>(){ "5", "j","k","l"},
            new List<string>(){ "6", "m","n","o"},
            new List<string>(){ "7", "p","q","r","s"},
            new List<string>(){ "8", "t","u","v"},
            new List<string>(){ "9", "w","x","y","z"}
            };

    }
}