namespace LeaderboardAPI.Models
{
    public class CustomerScore
    {
        public Int64 CustomerID { get; set; }
        public decimal Score { get; set; }


        /// <summary>
        /// The customer with the highest score is at rank 1
        /// Two customers with the same score, their ranks are determined by their CustomerID, lower is first.
        /// </summary>
        public int Rank { get; set; }
    }
}
