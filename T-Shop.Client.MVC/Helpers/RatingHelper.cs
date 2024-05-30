namespace T_Shop.Client.MVC.Helpers;

public class RatingHelper
{
    public class RatingDetail
    {
        public string PercentageColor { get; set; }
        public string PercentageColor2 { get; set; }
        public int Key { get; set; }
    }

    public static List<RatingDetail> GetRatingDetails(decimal rating)
    {
        var ratingDetails = new List<RatingDetail>();
        if (rating == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                ratingDetails.Add(new RatingDetail { PercentageColor = "0%", PercentageColor2 = "0%", Key = i });
            }
        }
        else
        {
            int ratingInt = (int)Math.Floor(rating);
            decimal ratingDecimal = rating - ratingInt;
            bool endStar = false;

            for (int i = 0; i < 5; i++)
            {
                string percentageColor = "0%";
                string percentageColor2 = "0%";

                if (i < ratingInt)
                {
                    percentageColor = "100%";
                    percentageColor2 = "20%";
                }
                else if (ratingDecimal > 0 && !endStar)
                {
                    percentageColor = $"{ratingDecimal * 100}%";
                    percentageColor2 = "0%";
                    endStar = true;
                }

                ratingDetails.Add(new RatingDetail { PercentageColor = percentageColor, PercentageColor2 = percentageColor2, Key = i });
            }
        }

        return ratingDetails;
    }
}
