using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;

namespace code_challenge_aspnet
{
    public class CatalogCollection
    {      
        public List<Catalog> MergedCatalog { get; set; }
        public List<Catalog> CompanyA { get; set; }
        public List<Catalog> CompanyB { get; set; }
    }
}
