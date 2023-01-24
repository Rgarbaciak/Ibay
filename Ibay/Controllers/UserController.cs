using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using ClassIbay;
using IbayApi.Services;

namespace Ibay.Controllers
{
    public class UserController : ControllerBase

    {
        private IbayContext _userContext;
        public UserController(IbayContext context)
        {
            _userContext = context;
        }
        [HttpGet]
        public List<User> Get()
        {
            return _userContext.Users.ToList();
        }
    }
}
