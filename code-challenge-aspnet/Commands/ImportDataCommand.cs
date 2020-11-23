using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_challenge_aspnet.Commands
{
    public static class ImportDataCommand
    {
        public static CatalogCollection GetCatalogs ()
        {
            var allCatalogs = new CatalogCollection();
            var mergedCatalog = new List<Catalog>();
            try
            {
                // TODO async tasks 
                // companyA
                List<CatalogSupplier> suppliersA = FileProcessor.InportCsv<CatalogSupplier>("./input/suppliersA.csv");
                List<CatalogBarcodes> barcodesA = FileProcessor.InportCsv<CatalogBarcodes>("./input/barcodesA.csv");
                List<Catalog> productsA = FileProcessor.InportCsv<Catalog>("./input/catalogA.csv");
                productsA.ForEach(p => p.Source = "A");

                // companyB
                List<CatalogSupplier> suppliersB = FileProcessor.InportCsv<CatalogSupplier>("./input/suppliersB.csv");
                List<CatalogBarcodes> barcodesB = FileProcessor.InportCsv<CatalogBarcodes>("./input/barcodesB.csv");
                List<Catalog> productsB = FileProcessor.InportCsv<Catalog>("./input/catalogB.csv");
                productsB.ForEach(p => p.Source = "B");

                var duplicateProducts = barcodesB.Where(w => barcodesA.Any(a => a.Barcode == w.Barcode))
                                                .Select(s => s.SKU).Distinct().ToList();

                // merge A + B
                mergedCatalog = new List<Catalog>(productsA);
                var ignoreDuplicate = productsB.Where(w => !duplicateProducts.Any(a => a == w.SKU)).Select(s => s).ToList();
                mergedCatalog.AddRange(ignoreDuplicate);

                allCatalogs.MergedCatalog = mergedCatalog;
                allCatalogs.CompanyA = new List<Catalog>(productsA);
                allCatalogs.CompanyB = new List<Catalog>(productsB);

                // save output
                FileProcessor.ExportCsv<Catalog>("./output/result_output.csv", mergedCatalog.AsEnumerable());
            }
            catch (Exception)
            {

                throw;
            }
            return allCatalogs;
        }
    }
}
