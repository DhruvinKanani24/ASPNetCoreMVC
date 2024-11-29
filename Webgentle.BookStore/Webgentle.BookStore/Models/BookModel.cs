using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Enums;
using Webgentle.BookStore.Helper;

namespace Webgentle.BookStore.Models
{
    public class BookModel
    {
        //[DataType(DataType.DateTime )]
        //[Display(Name ="Choose Date and Time")]
        //public string MyField { get; set; }
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the title of book")]
        //[MyCustomValidation("Azure")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the Author of book")]
        public string Author { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Catagory { get; set; }
        [Required(ErrorMessage = "Please enter the Language of book")]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Please choose the languages of book")]
        //public LanguageEnum LanguageEnum { get; set; }
        //[Required(ErrorMessage = "Please enter the totalpages of book")]
        //[Display(Name ="Total Pages of book")]
        public int? TotalPages { get; set; }
        [Display(Name = "Please choose the cover photo of book ")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
        [Display(Name = "Please choose the gallery images of book ")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryModel> Gallery { get; set; }
        [Display(Name = "Upload your pdf in pdf format ")]
        [Required]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }
    }
}   
