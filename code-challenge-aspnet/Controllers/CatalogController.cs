using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using code_challenge_aspnet.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace code_challenge_aspnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger)
        {
            _logger = logger;
        }
       
        //TODO update all routes to be async
        [HttpGet]
        public CatalogCollection Get()
        {
            return ImportDataCommand.GetCatalogs();
        }

        [HttpDelete]
        public List<Catalog> Delete([FromBody] Catalog catalog)
        {           
            return DeleteCommand.DeleteCatalog(catalog);
        }

        [HttpPost]
        public List<Catalog> Post([FromBody] Catalog catalog)
        {
            return AddCommand.AddCatalog(catalog);
        }

        [HttpPut]
        public List<Catalog> Put([FromBody] UpdateCatalogDto catalog)
        {
            return AddCommand.UpdateCatalog(catalog);
        }
    }
}
