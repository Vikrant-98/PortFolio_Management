using CommonServices.ModelServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUserBusiness
    {
        Task<bool> RegisterUser(CustomerRegister User);

        (int, bool) LoginVerification(CustomerLogin Info);
    }
}
