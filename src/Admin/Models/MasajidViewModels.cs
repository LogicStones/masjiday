using System;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
    public class Masjid
    {
        //[Required(ErrorMessage = "Title is required.")]
        //public string Title { get; set; }

        //[Required(ErrorMessage = "Description is required.")]
        //public string Description { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Masjid name is required.")]
        public string Name { get; set; }

        public int CityId { get; set; }


        //[Required(ErrorMessage = "Offset is required.")]
        public int OffSet { get; set; }

        //[Required(ErrorMessage = "Title is required.")]
        public TimeSpan Fajar { get; set; }

        //[Required(ErrorMessage = "Title is required.")]
        public TimeSpan Zohar { get; set; }

        //[Required(ErrorMessage = "Title is required.")]
        public TimeSpan Asar { get; set; }

        //[Required(ErrorMessage = "Title is required.")]
        public TimeSpan Magrib { get; set; }

        //[Required(ErrorMessage = "Title is required.")]
        public TimeSpan Isha { get; set; }

        //[Required(ErrorMessage = "Title is required.")]
        public TimeSpan Juma { get; set; }
 
    }
}