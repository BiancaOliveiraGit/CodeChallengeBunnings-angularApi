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
    }
}
