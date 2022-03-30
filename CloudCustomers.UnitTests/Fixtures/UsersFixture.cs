using CloudCustomers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
            { 
                new User
                {
                    Name = "Makandia Bifwoli",
                    Email = "makabifwol@hotcake.com",
                    Address = new Address
                    {
                        Street = "Muindi Mbingu Street",
                        City = "Nairobi",
                        Zipcode = "0100",
                    }
                },

                new User
                {
                    Name = "Sheila Wangari",
                    Email = "wangasheilz@bestie.com",
                    Address = new Address
                    {
                        Street = "DeMatthew Corner",
                        City = "Karatina",
                        Zipcode = "0200",
                    }
                },

                new User
                {
                    Name = "Koigi Njagi",
                    Email = "koiginjagi@yahoo.co.ke",
                    Address = new Address
                    {
                        Street = "Dala Dala",
                        City = "Kisumu",
                        Zipcode = "0300",
                    }
                },

            new User
            {
                Name = "Mwanahamisi Zainab",
                Email = "mwanazainab@hotmail.org",
                Address = new Address
                {
                    Street = "Maskani Safi",
                    City = "Mombasa",
                    Zipcode = "0400",
                }
            },
        };
    }
}
