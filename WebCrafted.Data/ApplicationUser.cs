using System.ComponentModel.DataAnnotations;

namespace WebCrafted.Data;

public class ApplicationUser
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Fornavn er påkrævet.")]
    [MaxLength(25, ErrorMessage = "Fornavn kan ikke være længere end 25 karakter.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Efternavn er påkrævet.")]
    [MaxLength(25, ErrorMessage = "Efternavn kan ikke være længere end 25 karakter.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email er påkrævet.")]
    [MaxLength(30, ErrorMessage = "Email kan ikke være længere end 30 karakter.")]
    [EmailAddress(ErrorMessage = "Indtast venligst en gyldig e-mail adresse.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Kodeord er påkrævet.")]
    [MaxLength(256, ErrorMessage = "Kodeord kan ikke være længere end 256 karakter.")]
    [MinLength(8, ErrorMessage = "Kodeord skal være mindst 8 tegn.")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Kodeordet skal indeholde mindst én bogstav, én tal og ét specialtegn.")]
    public string? Password { get; set; }
}