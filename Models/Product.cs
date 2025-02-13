using System.ComponentModel.DataAnnotations;

namespace MySimpleWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string Name { get; set; } = "Untitled";

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; } = "Uncategorized";

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; } = "N/A";

        [Required]
        [Display(Name = "Requested By")]
        public string RequestedBy { get; set; } = "Unknown";

        [Required]
        [Display(Name = "RMA Number")]
        public string RmaNumber { get; set; } = "N/A";

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Record Type")]
        public string RecordType { get; set; } = "In"; // Default to "In"
    }
}