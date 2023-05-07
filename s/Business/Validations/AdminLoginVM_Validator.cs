using Business.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class AdminLoginVM_Validator:AbstractValidator<AdminLoginViewModel>
    {
        public AdminLoginVM_Validator()
        {
            RuleFor(x=>x.UserName).NotEmpty().NotNull().MaximumLength(50).MinimumLength(5);
            RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(25).MinimumLength(8);
        }
    }
}
