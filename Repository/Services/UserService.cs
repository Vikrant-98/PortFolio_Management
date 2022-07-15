using CommonServices.ModelServices;
using Repository.ApplicationDB;
using Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> RegisterUser(Customer customer)
        {

            try
            {
                //Validation for unique MailID
                var Validation = _dbContext.Customer.Any(u => u.MailID == customer.MailID);

                if (Validation)
                {
                    return false;
                }

                var Result = await _dbContext.Customer.AddAsync(customer).ConfigureAwait(false);
                _dbContext.SaveChanges();
                if (Result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public (int, bool) LoginVerification(CustomerLogin Info)
        {
            try
            {
                var Result = _dbContext.Customer.Where(u => u.MailID == Info.MailID && u.Password == Info.Password).FirstOrDefault();

                if (Result != null)
                {
                    return (Result.ID, true);
                }
                else
                {
                    return (0, false);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
