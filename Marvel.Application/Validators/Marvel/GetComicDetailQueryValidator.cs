using FluentValidation;
using Marvel.Application.Queries.Marvel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Application.Validators.Marvel
{
    public class GetComicDetailQueryValidator : AbstractValidator<GetPokemonDetailQuery>
    {
        public GetComicDetailQueryValidator()
        {
            RuleFor(x => x.IdOrName)
                .NotEmpty()
                .WithMessage("El ID del Poken o Nombre no puede estar vacío.");
        }
    }
}
