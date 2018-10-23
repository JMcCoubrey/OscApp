namespace OscApp.DAL
{
    public interface ITenancyRepository
    {
        void CreateTenancy(string tenancyName, string userId);
    }
}
