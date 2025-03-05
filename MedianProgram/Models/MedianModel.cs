using System.ComponentModel.DataAnnotations;

namespace MedianProgram.Models
{
    public class MedianModel
    {
        public int Id { get; set; }
        [Required]
        public int InputNumber { get; set; }
        [Required]
        public string MedianPrimes { get; set; }
    }
}
