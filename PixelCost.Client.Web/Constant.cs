namespace PixelCost.Client.Web
{
    public static class Constant
    {
        public static string mainScheme = "MainCookieHandler";
        public static string CookieName = "WalletCookie";
        public static string walletPolicy = "WalletPolicy";
        public static string adminPolicy = "AdminPolicy";
        
        public static string userRole = "User";
        public static string adminRole = "Admin";
        public static string statusClaim = "Status";

        public static string statusActicated = "Activated";
        public static string statusDeactivated = "DeActivated";

        // Api name
        public static string walletUserApi = "walletUserAPI";
        public static string paymentMethodApi = "paymentMethodAPI";
        public static string durationApi = "durationAPI";
        public static string primaryExpenseApi = "primaryExpenseAPI";

        // PaymentMethodAPI's endpoint Name
        public static string payment_RetrieveByUserId = "userId/";
        public static string payment_RetrieveById = "id/";
        public static string paymentExpense_RetrieveById = "expense/";
        public static string paymentPrimaryExpense_RetrieveById = "primaryExpense/";
        public static string paymentRevenue_RetrieveById = "revenue/";
        public static string payment_topUp = "topup/";




    }
}