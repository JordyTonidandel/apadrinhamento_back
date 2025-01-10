using EncantoApadrinhamento.Core.Util;
using Microsoft.AspNetCore.Identity;

namespace EncantoApadrinhamento.Api.Configurations.DB
{
    public class DataBaseSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task UseSeedAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Owner"))
                await _roleManager.CreateAsync(new IdentityRole("Owner"));
            
            if (!await _roleManager.RoleExistsAsync("Administrator"))
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));

            if (!await _roleManager.RoleExistsAsync("Company"))
                await _roleManager.CreateAsync(new IdentityRole("Company"));

            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            var defaultUser = await _userManager.FindByEmailAsync("jordytonidandell@hotmail.com");
            if (defaultUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = Util.GenerateNickName("Jordy", "Tonidandel"),
                    Email = "jordytonidandell@hotmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "(31) 97365-7242",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    LockoutEnd = null,
                    NormalizedEmail = "jordytonidandell@hotmail.com".ToUpper(),
                };

                var result = await _userManager.CreateAsync(user, "Admin@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, ["Administrator", "Owner"]);
                }
            }
        }
    }
}
