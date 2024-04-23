using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Speaker.Models.ViewModels
{
    public class SpeakerEditModel
    {
        public int SpeakerId { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Realese"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        public DateTime Realese { get; set; }
        public bool Purchasable { get; set; }
        public HttpPostedFileBase Picture { get; set; }
        public int SpeakerModelId { get; set; }
        public int BrandId { get; set; }
        public virtual List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}