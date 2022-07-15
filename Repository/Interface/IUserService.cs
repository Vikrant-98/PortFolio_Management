using CommonServices.ModelServices;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterUser(Customer customer);

        (int, bool) LoginVerification(CustomerLogin Info);
    }
}
