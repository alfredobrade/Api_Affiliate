using API_Affiliates.Models;

namespace API_Affiliates.ServiceInterfaces
{
    public interface IAffiliateService
    {
        public IEnumerable<Affiliate> GetListAffiliate();
        public Affiliate GetAffiliate(string dni);
        public Task Save(Affiliate affiliate);
        public Task Update(Affiliate affiliate);
        public Task Delete(string dni);
    }
}
