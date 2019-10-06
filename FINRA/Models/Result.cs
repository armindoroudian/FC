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
        public string Source { get; set; }
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
            if (Request.Number.Length == 10 || Request.Number.Length == 7)//Phone digits is within limits            
            {
                if (Request.Number.StartsWith("0"))
                    Explanation = "Phone Number can not start with '0'";
            }

            else//Phone Number is not 7 or 10 digits
            {
                Explanation = "Phone Number can be 10 or 7 digits.";
            }

            if (!Request.Number.All(char.IsDigit))//Phone Number has letters in it
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
                result *= (Possibilities);//Multiply by number of permutations for each digit
            }
            this.PossiblePermutations = result;
        }
        public static List<string> GenerateRawPermutations(string PhoneNumber)
        {
            var ints = new List<int>();
            List<string> Permutations = new List<string>();
            var permutations = AlphaNumericList.List;

            var chars = PhoneNumber.ToCharArray();
            var tmpList = new List<string>();
            for (int i = chars.Count() - 1; i >= 0; i--)
            {
                int index = int.Parse(chars[i].ToString());
                //result.Permutations.Add(chars[i].ToString());
                foreach (var p in permutations[index])
                {
                    if (i == chars.Count() - 1)
                    {
                        Permutations.Add(p);
                    }
                    else
                    {

                        foreach (var permutation in Permutations)
                        {
                            tmpList.Add(p + permutation);
                        }
                    }

                }
                Permutations.AddRange(tmpList);
                tmpList.Clear();
                Permutations = Permutations.Where(p => p.Length == PhoneNumber.Length - i).ToList();
            }
            return Permutations;
        }
    }

}
