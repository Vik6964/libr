using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraAddin
{
    public class Timetracking
    {
        public string originalEstimate { get; set; }
        public int originalEstimateSeconds { get; set; }

        private const decimal DayToSecFactor = 8 * 3600;
        public decimal originalEstimateDays
        {
            get
            {
                return (decimal)originalEstimateSeconds / DayToSecFactor;
            }
            set
            {
                originalEstimate = string.Format("{0}d", value);
                originalEstimateSeconds = (int)(value * DayToSecFactor);
            }
        }
    }
}
