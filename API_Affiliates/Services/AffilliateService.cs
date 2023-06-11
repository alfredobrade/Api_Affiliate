using API_Affiliates.Models;
using API_Affiliates.Repository;
using API_Affiliates.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace API_Affiliates.Services
{
    public class AffilliateService : IAffiliateService
    {
        private readonly ProjectDbContext _context;

        public AffilliateService(ProjectDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(string dni)
        {
            try
            {
                var model = _context.Affiliates.Where(p => p.DNI == dni).FirstOrDefault();
                _context.Affiliates.Remove(model);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) { throw new Exception(); }

        }

        public async Task<Affiliate> GetAffiliate(string dni)
        {
            try
            {
                var affiliate = await _context.Affiliates.Where(x => x.DNI == dni).FirstOrDefaultAsync();
                return affiliate;
            }
            catch (Exception ex) { throw new Exception(); }

        }

        public async Task<IEnumerable<Affiliate>> GetListAffiliate()
        {
            try
            {
                var list = await _context.Affiliates.ToListAsync();
                return list;
            }
            catch (Exception ex) { throw new Exception(); }

        }

        public async Task<bool> Save(Affiliate affiliate)
        {
            try
            {
                _context.Add(affiliate);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception(); }
        }
        
        public async Task<bool> Update(Affiliate affiliate)
        {
            try
            {
                _context.Update(affiliate);
                await _context.SaveChangesAsync();
                return true;
            }catch(Exception ex) { throw new Exception(); }
        }
    }
}
