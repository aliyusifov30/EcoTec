using Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
	public class ContactUs_Validator:AbstractValidator<ContactUs>
	{
		public ContactUs_Validator()
		{
			RuleFor(x=>x.FullName).NotNull().NotEmpty().MaximumLength(50);
			RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(60);
			RuleFor(x=>x.Text).NotNull().NotEmpty().MaximumLength(800);

		}
	}
}
