using FluentValidation;
using Marvel.Application.Queries.Marvel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Application.Validators.Marvel
{
    public class GetComicDetailQueryValidator : AbstractValidator<GetComicDetailQuery>
    {
        public GetComicDetailQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("El ID del cómic no puede estar vacío.");
        }
    }
}
