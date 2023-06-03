using API_Affiliates.Models;
using API_Affiliates.Repository;
using API_Affiliates.ServiceInterfaces;

namespace API_Affiliates.Services
{
    public class AffilliateService : IAffiliateService
    {
        private readonly ProjectDbContext _context;

        public AffilliateService(ProjectDbContext context)
        {
            _context = context;
        }
        public Task Delete(string dni)
        {
            throw new NotImplementedException();
        }

        public Affiliate GetAffiliate(string dni)
        {
            // es asincronico? debo sacar async task de la definicion?
            var affiliate = _context.Affiliates.Where(x => x.DNI == dni).FirstOrDefault(); 
            return affiliate;
        }

        public IEnumerable<Affiliate> GetListAffiliate()
        {
            return _context.Affiliates;
        }

        public Task Save(Affiliate affiliate)
        {
            throw new NotImplementedException();
        }

        public Task Update(Affiliate affiliate)
        {
            throw new NotImplementedException();
        }
    }
}
