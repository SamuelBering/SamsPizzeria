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
        [Required(ErrorMessage ="Förnamn är obligatoriskt")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [Required(ErrorMessage = "Efternamn är obligatoriskt")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email är obligatoriskt")]
        [UIHint("email")]
        [EmailAddress(ErrorMessage = "Ogiltig email")]
        public string Email { get; set; }

        [UIHint("password")]
        [Display(Name = "Lösenord")]
        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        public string Password { get; set; }

        [UIHint("password")]
        [Display(Name = "Konfirmera nytt lösenord")]
        [Required(ErrorMessage = "Konfirmera lösenord är obligatoriskt")]
        [Compare(nameof(Password), ErrorMessage = "Lösenorden överrensstämmer inte")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Gatuadress")]
        [Required(ErrorMessage = "Gatuadress är obligatoriskt")]
        public string StreetAddress { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Postnummer är obligatoriskt")]
        public string ZipCode { get; set; }

        [Display(Name = "Postort")]
        [Required(ErrorMessage = "Postort är obligatoriskt")]
        public string PostTown { get; set; }
    }

    public class EditModel
    {
        [Display(Name = "Förnamn")]
        [Required(ErrorMessage = "Förnamn är obligatoriskt")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [Required(ErrorMessage = "Efternamn är obligatoriskt")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email är obligatoriskt")]
        [UIHint("email")]
        [EmailAddress(ErrorMessage = "Ogiltig email")]
        public string Email { get; set; }

        [UIHint("password")]
        [Required(ErrorMessage ="Lösenord är obligatoriskt")]
        [Display(Name = "Lösenord")]        
        public string Password { get; set; }

        [UIHint("password")]
        [Display(Name = "Nytt lösenord")]
        public string NewPassword { get; set; }

        [UIHint("password")]
        [Display(Name = "Konfirmera nytt lösenord")]
        [Compare(nameof(NewPassword),ErrorMessage ="Lösenorden överrensstämmer inte")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Gatuadress")]
        [Required(ErrorMessage = "Gatuadress är obligatoriskt")]
        public string StreetAddress { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage ="Postnummer är obligatoriskt")]
        public string ZipCode { get; set; }

        [Display(Name = "Postort")]
        [Required(ErrorMessage = "Postort är obligatoriskt")]
        public string PostTown { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage ="Email är obligatoriskt")]
        [EmailAddress(ErrorMessage ="Ogiltig email")]
        [UIHint("email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
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

        [Required(ErrorMessage = "Du måste ange minst en roll")]
        public string[] SelectedRoleIds { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }

}
