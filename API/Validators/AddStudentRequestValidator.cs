using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DomainModels;
using Core.RepoInterfaces;
using FluentValidation;

namespace API.Validators
{
    public class AddStudentRequestValidator : AbstractValidator<AddStudentRequestDto>
    {
        public AddStudentRequestValidator(IStudent stuRepo)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).GreaterThan(99).LessThan(1000000000);
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = stuRepo.GetGendersAsync().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if(gender != null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Select a valid Gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();
        }
    }
}