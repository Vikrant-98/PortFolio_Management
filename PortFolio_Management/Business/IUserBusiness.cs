using PortFolio_Management.Models;
using System.Threading.Tasks;

namespace PortFolio_Management.Business
{
    public interface IUserBusiness
    {
        Task<bool> RegisterUser(CustomerRegister User);

        (int, bool) LoginVerification(CustomerLogin Info);
    }
}
