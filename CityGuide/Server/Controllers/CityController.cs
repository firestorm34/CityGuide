using CityGuide.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CityGuide.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<CityController> _logger;
        CityGuideContext context;

        public CityController(ILogger<CityController> logger, CityGuideContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public List<City> Get()
        {
            context.Cities.Add(new City
            {
                Id = 1,
                FoundationDate = new DateTime(2001, 12, 11),
                Name = "City1",
                Population = 12000
            });
            
            context.SaveChanges();
            return context.Cities.ToList();

        }
    }
}