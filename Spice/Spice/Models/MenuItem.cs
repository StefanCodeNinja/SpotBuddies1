using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace SpotBuddies.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name{ get; set; }
       
        public string Description { get; set; }

        public string Spicyness { get; set; }
        public enum ESpicy { NA=0, NotSpicy=1, Spicy=2, VerySpicy=3}

        public string Image { get; set; }
       

        [Display(Name="Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }
        
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Price should be greater then ${1}")]
        public double Price { get; set; }


        //--------- spotBuddies.com
        public string Gender { get; set; }
        public enum EGender { Male=0, Female=1}
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string ShortDesc { get; set; }
        public bool active { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadByUser { get; set; }
       
        public double SizeNum { get; set; }
        public string SizeLetter { get; set; }
        public string SubCat1 { get; set; }
        public string SubCat2 { get; set; }
        public string SubCar3 { get; set; }
        public int CountStorage { get; set; }
        //public ICollection<Image> Images { get; set; }
        public bool Child { get; set; }
    }

    //public class Image
    //{
    //    public int Id { get; set; }
    //    public byte[] ImageData { get; set; }
    //    public int ProductId { get; set; }
    //}
}
