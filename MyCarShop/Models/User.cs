using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCarShop.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync
                               (Microsoft.AspNet.Identity.UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                                    DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}