using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Web;

namespace FINRA.Models
{
    public class Result
    {

        public TimeSpan Duration { get; set; }
        public int PossiblePermutations { get; set; }
        public int ActualPermutations { get; set; }
        public List<string> Permutations { get; set; }
        public PermutationRequest Request { get; set; }
        public string ValidationExplanation { get; set; }
        public bool IsValid
        {
            get
            {
                return this.WhyIsNotValid() == "1";
            }
            set
            {
                this.IsValid = IsValid;
            }
        }
        public Result()
        {
            this.Permutations = new List<string>();
        }


        public void CalculateDuration(DateTime BeginTime, DateTime EndTime)
        {
            this.Duration = EndTime - BeginTime;
        }
        public void Paginate(int PageSize, int PageIndex)
        {
            if (PageSize == 0)
                PageSize = 10;
            if (PageIndex == 0)
                PageIndex = 1;
            PageIndex--;
            this.Permutations = Permutations.Skip(PageIndex * PageSize).Take(PageSize).ToList();

        }
        

        public string WhyIsNotValid()
        {
            var Explanation = "1";
            if (Request.Number.Length == 10 || Request.Number.Length == 7)
            {
                if (Request.Number.StartsWith("0"))
                    Explanation = "Phone Number can not start with '0'";
            }

            else
            {
                Explanation = "Phone Number can be 10 or 7 digits.";
            }

            if (!Request.Number.All(char.IsDigit))
            {
                Explanation = "Phone Number should only contain digits.";
            }

            if (Request.PageSize < 0)
            {
                Explanation = "Page Size can not be negative";
            }
            if (Request.PageIndex < 0)
            {
                Explanation = "Page Number can not be negative";
            }
            return Explanation;
        }
        public string Clean()
        {
            return Request.Number.Replace(" ", "");
        }
        public void CalculateTotalPossibilities()
        {
            int result = 1;
            foreach (char c in Request.Number.ToCharArray())
            {
                int Possibilities = AlphaNumericList.List[int.Parse(c.ToString())].Count;
                result *= (Possibilities);
            }
            this.PossiblePermutations = result;
        }
    }

}
