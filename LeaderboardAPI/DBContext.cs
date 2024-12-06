using LeaderboardAPI.Models;
using System.Collections.Concurrent;

namespace LeaderboardAPI
{
    public class DBContext
    {
        public static ConcurrentBag<CustomerScore> DataSet = new ConcurrentBag<CustomerScore>();

        public static void SortRank()
        {

            {
                var list = DBContext.DataSet.OrderByDescending(one => one.Score).ThenBy(one => one.CustomerID);
                for (int index = 0; index < list.Count(); index++)
                    list.ElementAt(index).Rank = index + 1;
            }
        }
    }
}
