using LeaderboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LeaderboardAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private static readonly object _locker = new object();
        public IActionResult Index()
        {
            return Ok("Customer Controller");
        }


        [HttpPost("{id}/score/{score}")]
        public decimal UpdateCustomerRank(Int64 id, decimal score)
        {
            lock(_locker)
            {
                CustomerScore? customerScore = DBContext.DataSet.Where(one => one.CustomerID == id).FirstOrDefault();
                if (customerScore == null)
                {
                    customerScore = new CustomerScore();
                    customerScore.Score = score;
                    customerScore.CustomerID = id;
                    DBContext.DataSet.Add(customerScore);

                    DBContext.SortRank();
                }
                else
                {
                    if (score >= -1000 && score <= 1000)
                    {
                        customerScore.Score = customerScore.Score + score;

                        DBContext.SortRank();
                    }
                }

                return customerScore.Score;
            }
        }

        [HttpGet("GetAllTestData")]
        public IActionResult GetAllTestData()
        {
            return Ok(DBContext.DataSet.OrderByDescending(one => one.Score).ThenBy(one => one.CustomerID));
        }



    }
}
