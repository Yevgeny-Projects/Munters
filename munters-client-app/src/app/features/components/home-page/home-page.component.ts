// NG
import { Component, OnInit } from '@angular/core';

// VENDOR
import { SubSink } from 'subsink';
import { GridOptions } from 'ag-grid-community';

// APP
import { PhotosModel } from '@shared/models';
import { PhotosService } from '@shared/services/photos/photos-shared.service';

/**
 * Home page component
 */
@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
  
  photosModel: PhotosModel[] = [];
  selectedPhoto: PhotosModel;
  gridOptions: GridOptions;
  columnDefs = [];
  searchText = "";
  headerName = 'Trending GIF of the days';
  private subs = new SubSink(); 
  private gridApi;
  grid: any;


  /**
   * Constuctor 
   * Here PhotosService injected
   * @param photosService 
   */
  constructor(private photosService: PhotosService) { }

  /**
   * OnInit Hook
   * Here we call get pthotos api 
   */
  ngOnInit() {
    this.columnDefs = [
      {
        headerName: this.headerName,
        children: [
          { field: 'id' },
          { field: 'url', width:500 },
          { field: 'height'}]
        }
    ];
    this.gridOptions = <GridOptions>{
      columnDefs: this.columnDefs 
    }
    this.getTrendimgPhotos();
  }

  /**
   * Subscribe to getPhotos
   * @param data 
   */
  photosSub(data: PhotosModel[]): void {
    this.photosModel = data;
    this.updateHeader(this.headerName);
  }
  
  getTrendimgPhotos() {
    this.headerName = 'Trending GIF of the days';

    if (this.gridApi)  this.gridApi.showLoadingOverlay();

    this.subs.sink =this.photosService.getPhotos()
      .subscribe(data => this.photosSub(data));
  }

  getPhotosSearchBy(searchBy: string) {

    if (searchBy === '') return;

    this.headerName = 'Result of the search';
    if (this.gridApi)  this.gridApi.showLoadingOverlay();

    this.subs.sink =this.photosService.getPhotosSearchBy(searchBy)
    .subscribe(data => this.photosSub(data));
  }

  updateHeader(headerName: string) {
    if (this.gridOptions.columnDefs.length > 0) {

      // update the header name
      this.columnDefs[0]['headerName'] = headerName;
      this.gridOptions.api.setColumnDefs([]);
      this.gridOptions.api.setColumnDefs(this.columnDefs);
    }

    if (this.gridApi)  this.gridApi.hideOverlay();
  }

  onGridReady(params) {
    this.gridApi = params.api;
  }

  /**
   * OnOestroy Hook
   * Here we unsubscribe from getPhotos and interval
   */
  public ngOnOestroy() {
    this.subs.unsubscribe();
  }
}
