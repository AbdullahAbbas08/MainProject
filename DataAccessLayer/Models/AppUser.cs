using System;

namespace DataAccessLayer.Models
{
    public class AppUser:IdentityUser
    {
        [Required(ErrorMessage = "PleaseEnterFirstName")]
        [MaxLength(50, ErrorMessage = "Maximum50Characters")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }



        [Required(ErrorMessage = "PleaseEnterLastName")]
        [MaxLength(50, ErrorMessage = "Maximum50Characters")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [NotMapped]
        [Computed]
        public string FullName
        {
            get
            {
                return FirstName + "_" + LastName;
            }
        }

        public string? ImagePath { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }

    }
}
