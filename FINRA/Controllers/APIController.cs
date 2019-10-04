using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FINRA.Models;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace FINRAChallenge.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calculate()
        {
            return View();
        }

        [HttpPost]
        public string CalculatePermutations(PermutationRequest request)
        {
            var beginTime = DateTime.Now;
            //request.Number = "2404376186";
            var result = new Result();
            result.Request = request;
            try
            {
                if (result.IsValid)
                {
                    result.CalculateTotalPossibilities();
                    result.Permutations = GeneratePermutations(request.Number);
                    result.ActualPermutations = result.Permutations.Count;
                    result.Paginate(request.PageSize, request.PageIndex);
                }
                else
                {
                    result.ValidationExplanation = result.WhyIsNotValid();
                }
                var endTime = DateTime.Now;
                result.CalculateDuration(beginTime, endTime);
            }
            catch (Exception ex)
            {
                result.ValidationExplanation = ex.Message;
            }
            return JsonConvert.SerializeObject(result,
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
        }

        //Caching by the phone number entered
        [OutputCache(Duration = int.MaxValue, VaryByParam = "PhoneNumber")]
        public List<string> GeneratePermutations(string PhoneNumber)
        {



            List<string> Permutations = new List<string>();
            if (HttpContext.Cache[PhoneNumber] != null)
                Permutations = (List<string>)HttpContext.Cache[PhoneNumber];
            else
            {
                var ints = new List<int>();

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

                HttpContext.Cache[PhoneNumber] = Permutations;

            }

            return Permutations;

        }
    }
}