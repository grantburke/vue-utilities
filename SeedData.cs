using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using vue_utilities.Models;

namespace vue_utilities
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<UsersContext>();

            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                using (StreamReader r = new StreamReader("Users.json"))
                {
                    string json = r.ReadToEnd();
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
                    context.Users.AddRange(users);
                    context.SaveChanges();
                }
            }
        }
    }
}