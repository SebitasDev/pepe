using FluentValidation;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Validators
{
    public class GroupCoderValidator : AbstractValidator<GroupCoderDto>
    {
        public GroupCoderValidator()
        {
            Include(new GroupNameRule());
            Include(new GroupDescriptionRule());
        }


        //validations
        public class GroupNameRule : AbstractValidator<GroupCoderDto>
        {
            public GroupNameRule()
            {
                RuleFor(group => group.Name).NotEmpty()
                                            .WithMessage("The field Name is required");
            }
        }

        public class GroupDescriptionRule : AbstractValidator<GroupCoderDto>
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