using FluentValidation;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Models;

namespace RiwiTalent.Validators
{
    public class GroupCoderValidator : AbstractValidator<GruopCoder>
    {
        public GroupCoderValidator()
        {
            Include(new GroupNameRule());
            Include(new GroupDescriptionRule());
        }


        //validations
        public class GroupNameRule : AbstractValidator<GruopCoder>
        {
            public GroupNameRule()
            {
                RuleFor(group => group.Name).NotEmpty()
                                            .WithMessage("The field Name is required");
            }
        }

        public class GroupDescriptionRule : AbstractValidator<GruopCoder>
        {
            public GroupDescriptionRule()
            {
                RuleFor(group => group.Description).NotEmpty()
                                                    .WithMessage("The field Description is required")
                                                    .MaximumLength(100)
                                                    .WithMessage("Description must be maximum 100 characters");
            }
        }
    }
}