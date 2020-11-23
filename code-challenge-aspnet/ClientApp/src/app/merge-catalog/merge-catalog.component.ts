import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'merge-catalog-data',
  templateUrl: './merge-catalog.component.html'
})
export class MergeCatalogComponent {
  public mergeCatalog: Catalog[];
  public collectionCatalog: CatalogCollection;
  public companyA: Catalog[];
  public companyB: Catalog[];
  showA: boolean = false;
  showB: boolean = false;
  showMerge: boolean = true;
  enableDelete: boolean = false;
  companySource: string = 'A and B, Merged';
  http: HttpClient;
  baseUrl: string;
  //product: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.getMergeData();
    //this.product.description = '';
    //this.product.sku = '';
    //this.product.source = '';
  }

  getMergeData(): void {
    this.http.get<CatalogCollection>(this.baseUrl + 'catalog').subscribe(result => {
      this.collectionCatalog = result;
      this.mergeCatalog = this.collectionCatalog.mergedCatalog;
      this.companyA = this.collectionCatalog.companyA;
      this.companyB = this.collectionCatalog.companyB;
    }, error => console.error(error));
  }

  showCompanyA(): void {
    this.companySource = 'A';
    this.showA = true;
    this.showB = false;
    this.showMerge = false;
    this.enableDelete = true;
  }

  showCompanyB(): void {
    this.companySource = 'B';
    this.showB = true;
    this.showA = false;
    this.showMerge = false;
    this.enableDelete = true;
  }

  showCompanyM(): void {
    this.companySource = 'A and B, Merged';
    this.showMerge = true;
    this.showA = false;
    this.showB = false;
    this.enableDelete = false;
    this.getMergeData();
  }

}

 interface Catalog {
  source: string;
  sku: string;
  description: string;
}

interface CatalogCollection {
  mergedCatalog: [];
  companyA: [];
  companyB: [];
}
