namespace T_Shop.Client.MVC.Helpers.Constants;

public static class TransactionConstants
{
    public static string PENDING = "Pending";
    public static string INPROCESS = "InProcess";
    public static string COMPLETED = "Completed";
    public static string CANCELED = "Canceled";

    public static List<string> AVAILABLE_UPDATE_TRANSACTION_STATUS = new List<string>
        {
            INPROCESS, COMPLETED, CANCELED
        };

    public static List<int> ALLOW_RATING_INPUT = new List<int> { 1, 2, 3, 4, 5 };
}
