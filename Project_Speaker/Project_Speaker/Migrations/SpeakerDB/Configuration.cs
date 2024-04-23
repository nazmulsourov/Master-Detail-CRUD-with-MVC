namespace Project_Speaker.Migrations.SpeakerDB
{
    using Project_Speaker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project_Speaker.Models.SpeakerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\SpeakerDB";
        }

        protected override void Seed(Project_Speaker.Models.SpeakerDbContext db)
        {
            db.Brands.AddRange(new Brand[]
 {
     new Brand{BrandName="JBL"},
     new Brand{BrandName="Havit"}
 });
            db.SpeakerModels.AddRange(new SpeakerModel[]
            {
     new SpeakerModel{ModelName="JBL Flip"},
     new SpeakerModel{ModelName="Havit HV"},
            });
            db.SaveChanges();
            Speaker s = new Speaker
            {
                Name = "JBL Pulse",
                SpeakerModelId = 1,
                BrandId = 1,
                Realese = new DateTime(2024, 04, 06),
                Purchasable = true,
                Picture = "jbl.jpg"
            };
            s.Stocks.Add(new Stock { Category = Category.J2, Quantity = 5, Price = 2500 });
            s.Stocks.Add(new Stock { Category = Category.J3, Quantity = 10, Price = 2 });
            db.Speakers.Add(s);
            db.SaveChanges();
        }
    }
}
