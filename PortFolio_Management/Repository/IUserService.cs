using PortFolio_Management.Models;
using System.Threading.Tasks;

namespace PortFolio_Management.Repository
{
    public interface IUserService
    {
        Task<bool> RegisterUser(Customer customer);

        (int, bool) LoginVerification(CustomerLogin Info);
    }
}
