using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngaiaInterview.Application.Playlist.Queries.GetPlaylistForCity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IngaiaInterview.WebApi.Controllers
{
    [ApiController, Route("[controller]")]
    public class PlaylistController : ControllerBase
    {
        IMediator _mediator;

        public PlaylistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet,Route("{city}")]
        [ProducesResponseType(typeof(CityPlaylistModel), 200)]
        public async Task<IActionResult> Index([FromRoute]string city)
        {
            return Ok(await _mediator.Send(new GetPlaylistForCityQuery(city)));
        }
    }
}
