using System.ComponentModel.DataAnnotations;

namespace TaskWebApplication.Models
{
    public class Tasks : IValidatableObject
    {
        [Required]
        public int id { get; set; }

        [StringLength(30 , ErrorMessage ="First Name cannot be exceeded more than 30 characters")]
        public string? FirstName { get; set; }

        [StringLength(30, ErrorMessage = "Last Name cannot be exceeded more than 30 characters")]
        public string? LastName { get; set;}
        
        
        public string? City { get; set; }

        [Range(0, 34)]
        public int? age { get; set; }

        public DateTime? DateOfJoining { get; set; }

        public string? Department { get; set; }

        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DateOfJoining.Value.Year <= 2002)
            {
                yield return new ValidationResult("Joining Date is not below than 2002");
            }
        }
        
    }
}
