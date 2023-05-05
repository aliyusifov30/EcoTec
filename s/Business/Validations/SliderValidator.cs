using Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class SliderValidator : AbstractValidator<Slider>
    {

        public SliderValidator()
        {

            RuleFor(x => x.Title)
                .MaximumLength(50).NotEmpty().NotNull();

            RuleFor(x => x.Description)
                .MaximumLength(150).NotEmpty().NotNull();

            RuleFor(x => x.ButtonText1)
               .MaximumLength(50).NotEmpty().NotNull();

            RuleFor(x => x.ButtonText2)
               .MaximumLength(50).NotEmpty().NotNull();
        }

    }
}
