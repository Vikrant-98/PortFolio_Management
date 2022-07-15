using CommonServices.ModelServices;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterUser(Customer customer);

        (Customer, bool) LoginVerification(CustomerLogin Info);

        (Customer, bool) UserDetails(int Info);
    }
}
