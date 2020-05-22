using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IngaiaInterview.Application.Playlist.Queries.GetPlaylistForCity
{
    public class GetPlaylistForCityQueryValidator : AbstractValidator<GetPlaylistForCityQuery>
    {
        public GetPlaylistForCityQueryValidator()
        {
            RuleFor(x => x.CityName).NotNull().NotEmpty();
        }
    }
}
