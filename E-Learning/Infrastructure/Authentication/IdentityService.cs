using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Identity;
using Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Identity;
namespace Infrastructure.Authentication
{

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Guid> CreateUserAsync(Guid userId,
            string email,
            string password,
            Guid DomainUserId,
            CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                Id = userId,
                Email = email,
                UserName = email,
                DomainUserId = DomainUserId
            };

            var result = await _userManager.CreateAsync(
                user,
                password);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(x => x.Description));

                throw new InvalidOperationException(errors);
            }

            return user.Id;
        }

        public async Task<bool> CheckPasswordAsync(
            Guid userId,
            string password,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(
                userId.ToString());

            if (user is null)
                return false;

            return await _userManager.CheckPasswordAsync(
                user,
                password);
        }
    }
}