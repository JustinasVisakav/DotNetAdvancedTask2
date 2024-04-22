using LiteDB;

namespace Task.DAL.DalAppBuilders
{
    public static class DatabaseBuilders
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddSingleton<LiteDatabase>(x => new LiteDatabase("@My.Database"));
        }
    }
}
