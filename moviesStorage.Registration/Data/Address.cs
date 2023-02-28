using System.ComponentModel.DataAnnotations;

namespace moviesStorage.Registration.Data;

public class Address
{
    [Required]
    public string Line1 { get; set; }
    
    public string Line2 { get; set; }
    
    [Required]
    public string Town { get; set; }
    
    [Required]
    public string Country { get; set; }
    
    [Required]
    [DataType(DataType.PostalCode)]
    public string PostCode { get; set; }
    
    [Required]
    [MaxLength(4)]
    public string CountryCode { get; set; } 
}