using FluentValidation;
using Marvel.Application.Queries.Marvel;

namespace Marvel.Application.Validators.Marvel
{
    public class GetComicsQueryValidator : AbstractValidator<GetPokemonsQuery>
    {
        public GetComicsQueryValidator()
        {
            RuleFor(x => x.Offset)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El offset debe ser mayor o igual a 0.");

            RuleFor(x => x.Limit)
                .InclusiveBetween(1, 100)
                .WithMessage("El límite debe estar entre 1 y 100.");
        }
    }
}