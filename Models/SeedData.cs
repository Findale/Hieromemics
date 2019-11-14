using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Hieromemics.Data;
using System;
using System.Linq;

namespace Hieromemics.Models {

    public class SeedData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new HieromemicsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<HieromemicsContext>>())) {
                        if (context.users.Any()) {
                            return;
                        }

                        context.users.AddRange(
                            new users {
                                userName = "David"
                            },

                            new users {
                                userName = "Miguel"
                            }
                        );
                        context.SaveChanges();
                    }
        }
    }
}