// NG
import { Injectable, Injector } from '@angular/core';

// VENDOR
import { Observable } from 'rxjs';

// APP
import { BaseHttpService } from '@core/services/http';
import { PhotosModel } from '@shared/models';

@Injectable({providedIn: 'root'})
export class PhotosService extends BaseHttpService {
	private photos_endpoint: string = 'api/images';

	constructor(injector: Injector) {
		super(injector);
	}

	/**
	 * get photos from api
	 */
	public getPhotos(): Observable<PhotosModel[]> {
		return this.get(this.photos_endpoint + "/trending");
	}

	/**
	 * get photos by seatch string from api
	 */
	public getPhotosSearchBy(searchBy: string): Observable<PhotosModel[]> {
		return this.get(this.photos_endpoint+ "/search/", {
			searchBy: searchBy,
		});
	}
}
