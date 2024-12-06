using LeaderboardAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {

        public IActionResult Index()
        {
            return Ok("Leaderboard Controller");
        }

        [HttpGet]
        public IEnumerable<CustomerScore> Get(int start, int end)
        {
            var ds = DBContext.DataSet.Where(one => one.Rank >= start && one.Rank <= end);
            if(ds != null)
                return ds.OrderByDescending(one => one.Score).ThenBy(one => one.CustomerID);
            return Enumerable.Empty<CustomerScore>();
        }

        [HttpGet("{customerid}")]
        public IEnumerable<CustomerScore> Get(Int64 customerid, int? high, int? low)
        {
            if (high == null)
                high = 0;

            if (low == null)
                low = 0;

            if (high < 0 || low < 0)
                return Enumerable.Empty<CustomerScore>();

            CustomerScore? customerScore = DBContext.DataSet.Where(one => one.CustomerID == customerid).FirstOrDefault();
            if (customerScore != null)
            {
                var ds = DBContext.DataSet.Where(one => one.Rank <= (customerScore.Rank + low) && one.Rank >= (customerScore.Rank - high));
                if (ds != null)
                    return ds.OrderByDescending(one => one.Score).ThenBy(one => one.CustomerID);
            }

            return Enumerable.Empty<CustomerScore>();
        }
    }
}
