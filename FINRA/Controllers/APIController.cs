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
        private string Source = "";

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
            var result = new Result();
            result.Request = request;
            try
            {
                if (result.IsValid)
                {
                    //Calculate number of possible permutations
                    result.CalculateTotalPossibilities();

                    //Generate the list of permutations
                    result.Permutations = GeneratePermutations(request.Number);

                    //Set the source (Cache vs Generated)
                    result.Source = Source;

                    //Assign actual returned permutations count
                    result.ActualPermutations = result.Permutations.Count;

                    //Paginate
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
            if (HttpContext.Cache[PhoneNumber] != null)//Number is already calculated and cached
            {
                Permutations = (List<string>)HttpContext.Cache[PhoneNumber];
                Source = "From Server Cache";
            }
            else//Phone number is entered for first time and needs to be generated and cached
            {
                //Generate Permutations
                Permutations = Result.GenerateRawPermutations(PhoneNumber);

                //Cache this phone number permutations
                HttpContext.Cache[PhoneNumber] = Permutations;
                Source = "Calculated";
            }

            return Permutations;
        }

        
    }
}