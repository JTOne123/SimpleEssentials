namespace SimpleEssentials.Console
{
    public static class Constants
    {
        private static string DATABASE_NAME = "sc82_Employee_Portal";
        private static string SERVER_NAME = "(local)";

        private static string DATABASE_USER = "testuser";
        private static string DATABASE_PASSWORD = "Test321!";

        public static string DbConnectionString()
        {
            return
                $"user id={DATABASE_USER};password={DATABASE_PASSWORD};Data Source={SERVER_NAME};Database={DATABASE_NAME};MultipleActiveResultSets=True";
        }
    }
}