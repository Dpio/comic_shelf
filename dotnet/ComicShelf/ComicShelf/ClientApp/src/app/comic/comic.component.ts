import { Component, OnInit, ViewChild } from '@angular/core';
import { ComicService } from '../shared/services/comic.service';
import { ToastrService } from 'ngx-toastr';
import { ComicModel } from '../shared/models/comic.model';
import { BaseApiService } from '../shared/services/base.service';
import { ComicDetailsComponent } from './comic-details/comic-details.component';
import { AuthenticateService } from '../shared/services/authenticate.service';
import { CollectionModel } from '../shared/models/collection.model';
import { AuthenticateResponse } from '../shared/models/authenticate.model';
import { AddCollectionComponent } from '../user-comic-collection/add-collection/add-collection.component';
import { CollectionService } from '../shared/services/collection.service';
import { ComicCollectionModel } from '../shared/models/comicCollection.model';
import { RentService } from '../shared/services/rent.service';
import { RequestRentComponent } from '../rents/request-rent/request-rent.component';

@Component({
  selector: 'app-comic',
  templateUrl: './comic.component.html',
  styleUrls: ['./comic.component.css']
})
export class ComicComponent implements OnInit {
  @ViewChild('comicDetailsModal') comicDetailsModal: ComicDetailsComponent;
  @ViewChild('addCollectionModal') addCollectionModal: AddCollectionComponent;
  @ViewChild('requestRentModal') requestRentModal: RequestRentComponent;

  comics: Array<ComicModel>;
  currentUser: AuthenticateResponse = new AuthenticateResponse();
  collections: Array<CollectionModel>;
  collection: CollectionModel;
  collectionNames: Array<String>;
  collectionName: string;
  searchText: string;
  wantLists: Array<CollectionModel>;

  constructor(
    private comicService: ComicService,
    private toastr: ToastrService,
    private authenticateService: AuthenticateService,
    private collectionService: CollectionService,
  ) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }
  ngOnInit(): void {
    this.comicService.getAllComics().subscribe(data => {
      this.comics = BaseApiService.getObjectArrayFromApi<ComicModel>(data, ComicModel);
    }, error => {
      if (error.statusText === 'Unauthorized') {
        this.toastr.error(error.statusText);
        this.authenticateService.logout();
      } else {
        this.toastr.error(error.error);
      }
    }
    );
    this.getCollections();
    this.getWantLists();
  }

  comicDetails(id: number) {
    this.comicDetailsModal.show(id);
  }

  getCollections() {
    this.collectionService.getCollectionsForUser(this.currentUser.id).subscribe(data => {
      this.collections = data;
    }, error => {
      if (error.statusText === 'Unauthorized') {
        this.toastr.error(error.statusText);
        this.authenticateService.logout();
      } else {
        this.toastr.error(error.error);
      }
    });
  }

  addToCollection(collectionId: number, comicId: number) {
    const comicCollection = new ComicCollectionModel();
    comicCollection.collectionId = collectionId;
    comicCollection.comicId = comicId;
    this.collectionService.addComicToCollection(comicCollection).subscribe(() => {
    }, error => {
      this.toastr.error(error.error.message);
    }
    );
  }

  addCollection(isWantList: boolean) {
    this.addCollectionModal.show(isWantList);
  }

  getWantLists() {
    this.collectionService.getWantListForUser(this.currentUser.id).subscribe(data => {
      this.wantLists = data;
    }, error => {
      if (error.statusText === 'Unauthorized') {
        this.toastr.error(error.statusText);
        this.authenticateService.logout();
      } else {
        this.toastr.error(error.error);
      }
    });
  }

  RequestComic(id: number) {
    this.collectionService.findUsersWithComic(this.currentUser.id, id).subscribe(data => {
      this.requestRentModal.show(id);
  }, error => {
      this.toastr.error(error.error);
  });
  }
}
