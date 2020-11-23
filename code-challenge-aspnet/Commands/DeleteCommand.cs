using System;
using System.Collections.Generic;
using System.Linq;

namespace code_challenge_aspnet.Commands
{
    public static class DeleteCommand
    {
        public static List<Catalog> DeleteCatalog (Catalog deleteCatalog)
        {
            var updatedProducts = new List<Catalog>();
            try
            {
                // file paths
                var supplierFile = "./input/suppliers" + deleteCatalog.Source + ".csv";
                var barcodeFile = "./input/barcodes" + deleteCatalog.Source + ".csv";
                var productFile = "./input/catalog" + deleteCatalog.Source + ".csv";
                // get all data for company
                List<CatalogSupplier> suppliers = FileProcessor.InportCsv<CatalogSupplier>(supplierFile);
                List<CatalogBarcodes> barcodes = FileProcessor.InportCsv<CatalogBarcodes>(barcodeFile);
                List<Catalog> products = FileProcessor.InportCsv<Catalog>(productFile);
     
                // remove product
                products = products.Where(w => w.SKU != deleteCatalog.SKU).Select(s => s).ToList();
                //remove any barcodes
                var ids = barcodes.Where(w => w.SKU == deleteCatalog.SKU).Select(s => s.SupplierID).ToList();
                barcodes = barcodes.Where(w => !ids.Any(a => a == w.SupplierID)).Select(s => s).ToList();
                //remove any suppliers
                suppliers = suppliers.Where(w => !ids.Any(a => a == w.ID)).Select(s => s).ToList();

                // save back to input files
                FileProcessor.ExportCsv<CatalogSupplier>(supplierFile, suppliers.AsEnumerable());
                FileProcessor.ExportCsv<CatalogBarcodes>(barcodeFile, barcodes.AsEnumerable());
                FileProcessor.ExportCsv<Catalog>(productFile, products.AsEnumerable());
                updatedProducts = products;
            }
            catch (Exception)
            {
                return updatedProducts;
            }
            return updatedProducts;
        }
    }
}
