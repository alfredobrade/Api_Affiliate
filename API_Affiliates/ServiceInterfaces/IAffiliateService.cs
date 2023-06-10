using API_Affiliates.Models;

namespace API_Affiliates.ServiceInterfaces
{
    public interface IAffiliateService
    {
        Task<IEnumerable<Affiliate>> GetListAffiliate();
        Task<Affiliate> GetAffiliate(string dni);
        Task<bool> Save(Affiliate affiliate);
        Task<bool> Update(Affiliate affiliate);
        Task<bool> Delete(string dni);
    }
}
