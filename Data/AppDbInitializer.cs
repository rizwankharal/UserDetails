using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using UserDetails.Model;

namespace UserDetails.Data
{
    public class AppDbInitializer
    {
        public static void SeedinData(IApplicationBuilder applicationBuilder)
        {
            using (var ServiceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var Context = ServiceScope.ServiceProvider.GetService<AppDbContext>();
                
                if (!Context.Users.Any())
                {
                    Context.Users.AddRange(new User()
                    {

                        Name = "Jhon",
                        Email = "jhon@g.com",
                        phone = "+16565421414",
                        DOB = DateTime.Now.AddYears(-20).AddMonths(-10),
                        Address = "35 saint Stephen",
                        ZipCode = "AL 36104"


                    },
                    new User()
                    {

                        Name = "Abbey",
                        Email = "abbey@gmail.com",
                        phone = "abbey@gmail.com",
                        DOB = DateTime.Now.AddYears(-18).AddMonths(-10),
                        Address = "36 saint Stephen",
                        ZipCode = "AK 99801"

                    },
                    new User()
                    {

                        Name = "Matthew",
                        Email = "Matthew@gmail.com",
                        phone = "+14844145958",
                        DOB = DateTime.Now.AddYears(-20).AddMonths(-8),
                        Address = "37 saint Stephen",
                        ZipCode = "AZ 85001"

                    },
                    new User()
                    {
                        Name = "Rizwan",
                        Email = "riz@gmail.com",
                        phone = "+9233676331199",
                        DOB = DateTime.Now.AddYears(-25).AddMonths(-7).AddDays(-5),
                        Address = "Rawalpindi Shamsabad",
                        ZipCode = "46600"

                    },
                    new User()
                    {

                        Name = "Qasim",
                        Email = "QasimALi@gmail.com",
                        phone = "+9233336000000",
                        DOB = DateTime.Now.AddYears(-22),
                        Address = "Rawalpindi",
                        ZipCode = "46600"

                    });

                    Context.SaveChanges();
                }

            }
        }
    }
}
