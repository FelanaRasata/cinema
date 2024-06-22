using System.ComponentModel.DataAnnotations;

namespace backoffice.Models;

public class User
{
    [Key] 
    public int Id { get; set; }
    [Required] 
    [StringLength(100)]
    public string Name { get; set; }
    [Required] 
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    [Required]
    [StringLength(100)]
    public string Password { get; set; }
}