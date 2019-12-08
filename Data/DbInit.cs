using Hieromemics.Models;
using System;
using System.Linq;

namespace Hieromemics.Data
{
    public static class DbInit
    {
        public static void Initialize(HieromemicsContext context)
        {
            context.Database.EnsureCreated();

            if (context.users.Any())
            {
                return;
            }

            var usrs = new users[]
            {
                new users
                {
                    userName = "David"
                },
                new users
                {
                    userName = "Sean"
                },
                new users
                {
                    userName = "Drake"
                },
                new users
                {
                    userName = "Miguel"
                }
            };
            foreach (users u in usrs)
            {
                context.users.Add(u);
            }
            context.SaveChanges();

            var pics = new pictures[]
            {
                new pictures
                {
                    StoragePath = "https://www.ancient-origins.net/sites/default/files/field/image/Ancient-Egyptian-Hieroglyphs.jpg"
                },
                new pictures
                {
                    StoragePath = "https://askleo.askleomedia.com/wp-content/uploads/2015/07/2149-600x315.jpg"
                } 
            };
            foreach (pictures p in pics)
            {
                context.pictures.Add(p);
            }
            context.SaveChanges();

        }
    }
}