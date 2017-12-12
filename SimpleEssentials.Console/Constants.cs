namespace SimpleEssentials.Console
{
    public static class Constants
    {
        private static string DATABASE_NAME = "Starcare";
        private static string SERVER_NAME = "DESKTOP-SOQBRCU\\SQLEXPRESS";

        private static string DATABASE_USER = "sa";
        private static string DATABASE_PASSWORD = "Haccord02!";

        public static string DbConnectionString()
        {
            //return "Data Source=DESKTOP-SOQBRCU/SQLEXPRESS;Initial Catalog=Starcare;Persist Security Info=True;User ID=sa;Password=Haccord02!;MultipleActiveResultSets=True";
            return string.Format("user id={0};password={1};Data Source={2};Database={3};MultipleActiveResultSets=True", DATABASE_USER, DATABASE_PASSWORD, SERVER_NAME, DATABASE_NAME);
        }

        public static int PAYROLL_DAYS_TO_SHOW_FUTURE = 27;
        public static int PAYROLL_DEFAULT_WEEKS_GIVEN = 1;
        public static string PAYROLL_EMPLOYEE_PAYDATE = "Thursday";
        public static int PAYROLL_MAX_REQUESTABLE_WEEKS = 3;
        public static string PAYROLL_EMPLOYEE_ANNIVERSARY_FLAG_DAY = "friday";
        public static string PAYROLL_STATUS_CANCLED_BY_EMPLOYEE = "Status Canceled By Employee";
        public static string PAYROLL_STATUS_REQUESTED = "Status Requested";
    }
}