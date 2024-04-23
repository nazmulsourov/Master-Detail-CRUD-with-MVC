using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_Speaker.Models
{
   
        public enum Category { J1 = 1, J2, J3, J4, J5, H1 = 1, H2, H3, H4 }
        public class SpeakerModel
        {
            public int SpeakerModelId { get; set; }
            [Required, StringLength(50), Display(Name = "Model Name")]
            public string ModelName { get; set; }
            //nev
            public virtual ICollection<Speaker> Speakers { get; set; } = new List<Speaker>();
        }
        public class Brand
        {
            public int BrandId { get; set; }
            [Required, StringLength(50), Display(Name = "Brand Name")]
            public string BrandName { get; set; }
            public virtual ICollection<Speaker> Speakers { get; set; } = new List<Speaker>();
        }
        public class Speaker
        {
            public int SpeakerId { get; set; }
            [Required, StringLength(50)]
            public string Name { get; set; }
            [Required, Column(TypeName = "date"), Display(Name = "Realese"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
            public DateTime Realese { get; set; }
            public bool Purchasable { get; set; }
            public string Picture { get; set; }
            //fk
            [ForeignKey("SpeakerModel")]
            public int SpeakerModelId { get; set; }
            [ForeignKey("Brand")]
            public int BrandId { get; set; }
            //nev
            public virtual SpeakerModel SpeakerModel { get; set; }
            public virtual Brand Brand { get; set; }
            public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
        }
        public class Stock
        {
            public int StockId { get; set; }
            public Category Category { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            [Required, ForeignKey("Speaker")]
            public int SpeakerId { get; set; }
            //
            public virtual Speaker Speaker { get; set; }
        }
        public class SpeakerDbContext : DbContext
        {

            public DbSet<Brand> Brands { get; set; }
            public DbSet<SpeakerModel> SpeakerModels { get; set; }
            public DbSet<Speaker> Speakers { get; set; }
            public DbSet<Stock> Stocks { get; set; }
        }
    
}