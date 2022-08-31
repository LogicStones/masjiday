using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class Notification
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
    
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select at least one masjid.")]
        public string MasajidIds { get; set; }

        //[Required(ErrorMessage = "Please select at least one option.")]
        //public string MethodTypeIds { get; set; }
        public List<Audiance> lstAudiance { get; set; }
        //public List<NotificationMethod> lstNotificationMethod { get; set; }
    }

    public class Audiance
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class NotificationMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}