using Business.Interface;
using CommonServices.ModelServices;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserService _userService;
        public UserBusiness(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> RegisterUser(CustomerRegister User)
        {
            Customer customer = new Customer()
            {
                MailID = User.MailID,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Password = User.Password,
                ModifiedDate = DateTime.Now
            };
            var result = await _userService.RegisterUser(customer).ConfigureAwait(false);
            return result;
        }

        public (int, bool) LoginVerification(CustomerLogin Info)
        {
            try
            {
                var Result = _userService.LoginVerification(Info);                               //get result true or false
                if (Result.Item2)
                {
                    return (Result.Item1, true);
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
