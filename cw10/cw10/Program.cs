using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw10.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace cw10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        public static void QueryExamples()
        {
            var db = new s16520Context();


            //1. SELECT * FROM Doctor;
            //var res = db.Doctor.ToList();

            //var res = db.Doctor
            //          .Where(d => d.FirstName.StartsWith("J"))
            //          .OrderBy(d => d.LastName)
            //          .ThenBy(d => d.FirstName)
            //          .ToList();

            //2. Lazy loading + Proxies
            //   Eager loading
            //   ToList(), First(), FirstOrDefault(), Single, SingleOrDefault()
            //IQueryable<T>
            //var res = db.Doctor
            //            .Include(d => d.Prescription)
            //            .ToList(); // 1 sql

            ////N+1 problem
            //foreach(var d in res)
            //{
            //    if (d.Prescription.Count() > 1) //N sql
            //    {
            //        //..
            //    }
            //}

            //var res = db.Doctor.OrderBy(d => d.LastName);
            //var res2 = res.Where(d => d.LastName == "Kowalski");

            //var res = db.Doctor
            //          .GroupBy(d => d.FirstName)
            //          .Select(d => new
            //          {
            //              Imie = d.Key,
            //              LiczbaDoktorow = d.Count()
            //          }).ToList();



        }

    }
}
