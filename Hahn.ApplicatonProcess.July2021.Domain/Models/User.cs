using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hahn.ApplicatonProcess.July2021.Domain.Models
{
    public class User
    {
        public long Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped] 
        public string Address { get
            {
                return $"{ZIP}, st.{Street}, #{HouseNum}";
            } 
        } 
        public string ZIP { get; set; }
        public string Street { get; set; }
        public string HouseNum { get; set; }
        public string Email { get; set; }
        public List<Asset> Assets { get; set; }
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Age).GreaterThan(18);
            RuleFor(x => x.FirstName).MinimumLength(3);
            RuleFor(x => x.LastName).MinimumLength(3);
            RuleFor(x => x.ZIP).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.HouseNum).NotEmpty();
            RuleFor(x => x.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        }

    }
}
