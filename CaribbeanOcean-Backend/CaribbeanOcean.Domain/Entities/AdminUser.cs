using System.ComponentModel.DataAnnotations;

namespace CaribbeanOcean.Domain.Entities
{
    public class AdminUser
    {
        [Key]
        [Required]
        public int id { get; set; }

        [Required(ErrorMessage = "The username is required.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "The username can only contain letters, numbers and underscores.")]
        [MaxLength(50, ErrorMessage = "The username cannot exceed 50 characters.")]
        public string username { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        [MaxLength(255, ErrorMessage = "The password cannot exceed 255 characters.")]
        [MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
        public string password_hash { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "The name can only contain letters and spaces.")]
        [MaxLength(255, ErrorMessage = "The name cannot exceed 255 characters.")]
        public string name { get; set; }

        [Required(ErrorMessage = "The email is required.")]
        [EmailAddress(ErrorMessage = "The email is not valid.")]
        [MaxLength(255, ErrorMessage = "The email cannot exceed 255 characters.")]
        public string email { get; set; }
        public DateTime last_login { get; set; }

        public AdminUser() { }

        public static AdminUser Create(string username, string name, string email, string passwordHash)
        {
            
            var user = new AdminUser
            {
                username = username?.Trim() ?? "",
                name = name?.Trim() ?? "",
                email = email?.Trim().ToLower() ?? "",
                password_hash = passwordHash ?? "",
            };

            var context = new ValidationContext(user);
            Validator.ValidateObject(user, context, validateAllProperties: true);
            return user;
        }
    }
}