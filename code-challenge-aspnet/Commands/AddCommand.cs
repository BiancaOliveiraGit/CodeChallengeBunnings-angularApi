using System;
using System.Collections.Generic;
using System.Linq;

namespace code_challenge_aspnet.Commands
{
    public static class AddCommand
    {
        public static List<Catalog> AddCatalog (Catalog addCatalog)
        {
            var updatedProducts = new List<Catalog>();
            try
            {
                // file paths
                var productFile = "./input/catalog" + addCatalog.Source + ".csv";
                // get data for company
                List<Catalog> products = FileProcessor.InportCsv<Catalog>(productFile);
                products.ForEach(p => p.Source = addCatalog.Source);
                // add product - should have vaildation before adding
                products.Add(addCatalog);
               
                // save back to input files
                FileProcessor.ExportCsv<Catalog>(productFile, products.AsEnumerable());
                updatedProducts = products;
            }
            catch (Exception)
            {
                return updatedProducts;
            }
            return updatedProducts;
        }

        public static List<Catalog> UpdateCatalog(UpdateCatalogDto updateCatalog)
        {
            var updatedProducts = new List<Catalog>();
            try
            {
                // file paths
                var supplierFile = "./input/suppliers" + updateCatalog.Source + ".csv";
                var barcodeFile = "./input/barcodes" + updateCatalog.Source + ".csv";
                var productFile = "./input/catalog" + updateCatalog.Source + ".csv";

                // get all data for company
                List<CatalogSupplier> suppliers = FileProcessor.InportCsv<CatalogSupplier>(supplierFile);
                List<CatalogBarcodes> barcodes = FileProcessor.InportCsv<CatalogBarcodes>(barcodeFile);
                List<Catalog> products = FileProcessor.InportCsv<Catalog>(productFile);
                products.ForEach(p => p.Source = updateCatalog.Source);

                //find product
                var existProduct = products.Where(w => w.SKU == updateCatalog.SKU).Select(s => s).FirstOrDefault();
                if(existProduct != null)
                {
                    // supplier
                    var supplierID = suppliers.Max(m => m.ID) + 1;
                    var supplier = suppliers.Where(w => w.Name == updateCatalog.SupplierName).Select(s => s).FirstOrDefault();
                    if(supplier == null)
                    {
                        // add supplier
                        supplier = new CatalogSupplier()
                        {
                            ID = supplierID,
                            Name = updateCatalog.SupplierName
                        };
                        // add supplier
                        suppliers.Add(supplier);
                    }
                    else
                    {
                        supplierID = supplier.ID;
                    }
                    // barcode
                    var newBarcode = new CatalogBarcodes()
                    {
                        SupplierID = supplierID,
                        SKU = updateCatalog.SKU,
                        Barcode = updateCatalog.Barcode
                    };
                    barcodes.Add(newBarcode);
                }
    
                // save back to input files
                FileProcessor.ExportCsv<Catalog>(productFile, products.AsEnumerable());
                FileProcessor.ExportCsv<CatalogSupplier>(supplierFile, suppliers.AsEnumerable());
                FileProcessor.ExportCsv<CatalogBarcodes>(barcodeFile, barcodes.AsEnumerable());
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
