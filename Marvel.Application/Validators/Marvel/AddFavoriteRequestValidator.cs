using FluentValidation;
using Marvel.Application.DTOs.Marvel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Application.Validators.Marvel
{
    public class AddFavoriteRequestValidator : AbstractValidator<AddFavoriteRequest>
    {
        public AddFavoriteRequestValidator()
        {
            RuleFor(x => x.PokemonId)
                .NotEmpty().WithMessage("PokemonId no puede estar vacío.");
        }
    }
}
