using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;

namespace code_challenge_aspnet
{
    public class Catalog
    {
        [Optional]
        public string Source { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        // TODO add these for GUI display
        [Optional]
        List<CatalogSupplier> CatalogSuppliers { get; set; }
        [Optional]
        List<CatalogBarcodes> CatalogBarcodes { get; set; }
    }
}
