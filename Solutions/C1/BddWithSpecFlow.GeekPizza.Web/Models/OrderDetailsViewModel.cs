using System;
using System.ComponentModel.DataAnnotations;

namespace BddWithSpecFlow.GeekPizza.Web.Models
{
    /// <summary>
    /// Represents the information related to the order details
    /// </summary>
    public class OrderDetailsViewModel
    {
        [Display(Name = "Delivery Address / Street (required)")]
        [Required]
        public string DeliveryStreetAddress { get; set; }

        [Display(Name = "Delivery Address / City (required)")]
        [Required]
        public string DeliveryCity { get; set; }

        [Display(Name = "Delivery Address / Zip")]
        public string DeliveryZip { get; set; }

        [Display(Name = "Delivery Date (required)")]
        [DataType(DataType.Text), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Delivery Time")]
        [DataType(DataType.Text), DisplayFormat(DataFormatString = "{0:h\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? DeliveryTime { get; set; }
    }
}