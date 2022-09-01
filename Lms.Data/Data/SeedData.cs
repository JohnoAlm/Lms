using Bogus;
using Lms.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data
{

#nullable disable

    public class SeedData
    {
        // Databas context
        private static LmsApiContext _context;

        public static async Task InitAsync(LmsApiContext context)
        {
            // Nullcheck på context
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Kollar om det finns några courses redan
            //if (await _context.Course.AnyAsync()) return;

            // Sätter våran _context på context variabeln
            _context = context;

            // Skapar faker
            var faker = new Faker("sv");

            // Gör en ny lista av Course "courses" som håller seedade kurser
            var courses = new List<Course>();

            // Gör en for-loop för att skapa 10 kurser
            for (int i = 0; i < 10; i++)
            {
                courses.Add(new Course
                {
                    Title = faker.Company.CatchPhrase(),
                    StartDate = DateTime.Now,
                    Modules = new Module[]
                    {
                        new Module
                        {
                            Title = faker.Company.Bs(),
                            StartDate = DateTime.Now,
                        },

                        new Module
                        {
                            Title = faker.Company.Bs(),
                            StartDate = faker.Date.Between(DateTime.Now, new DateTime(2023, 03, 31))
                        },

                        new Module
                        {
                            Title = faker.Company.Bs(),
                            StartDate = faker.Date.Between(new DateTime(2023, 03, 31), new DateTime(2023, 05, 31))
                        }
                    }
                });
            }

            // Lägger till kurserna till databasen
            context.AddRange(courses);

            // Sparar ändringarna till databasen
            await context.SaveChangesAsync();

        }
    }
}
