using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngaiaInterview.Application.Statistics.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IngaiaInterview.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<CitySearchStatisticsModel>), 200)]
        public async Task<IActionResult> Index()
        {
            return Ok(await _mediator.Send(new GetCitySearchStatisticsQuery()));
        }
    }
}
