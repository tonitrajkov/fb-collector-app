using System.Collections.Generic;
namespace FbCollector.Models
{
    public class PageFeedChartModel
    {
        public int PublishedOn { get; set; }

        public string PostType { get; set; }

        public int Total { get; set; }
    }

    public class BarChartResultModel
    {
        public BarChartResultModel()
        {
            Labels = new List<string>();
            Series = new List<string>();
            Data = new List<int>();
        }

        public List<string> Labels { get; set; }

        public List<string> Series { get; set; }

        public List<int> Data { get; set; }
    }
}
