using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcJav.Data;
using System;
using System.Linq;

namespace MvcJav.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcJavContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcJavContext>>()))
        {
            // Look for any javs.
            if (context.Jav.Any())
            {
                return;   // DB has been seeded
            }
            context.Jav.AddRange(
                new Jav
                {
                    Name = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Description = "Romantic Comedy",
                    Price = 7.99M,
                    Rating = "R",
                    Actor = "Henry Fonda"
                },
                new Jav
                {
                    Name = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Description = "Comedy",
                    Price = 8.99M,
                    Rating = "S",
                    Actor = "Gal Gadot"
                },
                new Jav
                {
                    Name = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Description = "Comedy",
                    Price = 9.99M,
                    Rating = "A",
                    Actor = "El Pacino"
                },
                new Jav
                {
                    Name = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Description = "Western",
                    Price = 3.99M,
                    Rating = "C",
                    Actor = "Orlando Bloom"
                }
            );
            context.SaveChanges();
        }
    }
}