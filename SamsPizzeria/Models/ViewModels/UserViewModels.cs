using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Models.ViewModels
{
    public class CreateModel
    {
        [Display(Name="Förnamn")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Ogiltig email")]
        public string Email { get; set; }

        [Display(Name = "Lösenord")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Gatuadress")]
        [Required]
        public string StreetAddress { get; set; }

        [Display(Name = "Postnummer")]
        [Required]
        public string ZipCode { get; set; }

        [Display(Name = "Postort")]
        [Required]
        public string PostTown { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Ogiltig email")]
        [UIHint("email")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }

    public class CreateDefaultUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class UserRolesModificationModel
    {

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [Required]
        public string[] SelectedRoleIds { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }

}
