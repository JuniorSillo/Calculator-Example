using Microsoft.AspNetCore.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        var admin = await userManager.FindByNameAsync("Max");

        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = "Max",
                Email = "Max@Calculator.com"
            };

            await userManager.CreateAsync(admin, "Max123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}