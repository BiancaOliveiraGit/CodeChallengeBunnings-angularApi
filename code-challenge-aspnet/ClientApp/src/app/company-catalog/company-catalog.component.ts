import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'company-catalog',
  templateUrl: './company-catalog.component.html'
})
export class CompanyCatalogComponent implements OnInit{
 
  @Input() catalogs: any [];
  @Input() company: string;
  @Input() allowDelete: boolean;
  http: HttpClient;
  baseUrl: string;
  description = '';
  source = '';
  sku = '';
  updateSku = '';
  updateSupplier = '';
  updateBarcode = '';

  constructor( http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {
    
  }

  productDelete(catalog) : void {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: catalog,
    };

    this.http.delete<any>(this.baseUrl + 'catalog', options).subscribe(result => {
      this.catalogs = result;
    }, error => console.error(error));
  }

  addProduct(): void {
    let addProduct: Catalog = { description:this.description, sku:this.sku, source:this.company };
    
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };

    this.http.post<any>(this.baseUrl + 'catalog',addProduct, options).subscribe(result => {
      this.catalogs = result;
      this.description = '';
      this.sku = '';
      this.source = '';
    }, error => console.error(error));
  }


  putProduct(): void {
    let updateProduct: UpdateCatalogDto = { sku: this.updateSku, source: this.company, supplierName: this.updateSupplier, barcode: this.updateBarcode };

    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };

    this.http.put<any>(this.baseUrl + 'catalog', updateProduct, options).subscribe(result => {
      this.catalogs = result;
      this.updateBarcode = '';
    }, error => console.error(error));
  }
}

// TODO put into another folder not in component
interface Catalog {
  source: string;
  sku: string;
  description: string;
}

interface UpdateCatalogDto {
  source: string;
  sku: string;
  supplierName: string;
  barcode: string;
}
