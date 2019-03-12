import { OnInit, Component, ViewChild } from '@angular/core';
import { AuthenticateResponse } from '../shared/models/authenticate.model';
import { CollectionService } from '../shared/services/collection.service';
import { CollectionModel } from '../shared/models/collection.model';
import { ComicModel } from '../shared/models/comic.model';
import { ToastrService } from 'ngx-toastr';
import { AuthenticateService } from '../shared/services/authenticate.service';
import { ComicDetailsComponent } from '../comic/comic-details/comic-details.component';

@Component({
    selector: 'app-user-comic-collection',
    templateUrl: './user-comic-collection.component.html',
    styleUrls: ['./user-comic-collection.component.css']
})
export class UserComicCollectionComponent implements OnInit {
    @ViewChild('comicDetailsModal') comicDetailsModal: ComicDetailsComponent;
    currentUser: AuthenticateResponse = new AuthenticateResponse();
    collections: Array<CollectionModel>;
    comics: Array<ComicModel>;
    collection: CollectionModel;
    wantLists: Array<CollectionModel>;

    constructor(
        private collectionService: CollectionService,
        private toastr: ToastrService,
        private authenticateService: AuthenticateService,
    ) {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    // TODO: Grey out comics that are currently rented.
    ngOnInit(): void {
        this.getCollections();
        this.getWantLists();
    }

    getComics(collection: CollectionModel) {
        this.comics = new Array<ComicModel>();
        this.collection = collection;
        this.collectionService.getComicsInCollection(collection.id).subscribe(data => {
            this.comics = data;
        }, error => {
            if (error.statusText === 'Unauthorized') {
                this.toastr.error(error.statusText);
                this.authenticateService.logout();
            } else {
                this.toastr.error(error.error);
            }
        });
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

    comicDetails(id: number) {
        this.comicDetailsModal.show(id);
    }

    deleteComicFromCollection(comicId: number) {
        const collection = this.collection;
        this.collectionService.getComicCollection(comicId, collection.id).subscribe(data => {
            this.collectionService.deleteComicFromCollection(data.id).subscribe(() => {
                this.getComics(collection);
                this.collection = new CollectionModel();
            });
        });
    }

    deleteCollection(id: number) {
        this.collectionService.getComicCollectionByCollectionId(id).subscribe(data => {
            const comicCollections = data;
            comicCollections.forEach(comicCollection => {
                this.collectionService.deleteComicFromCollection(comicCollection.id);
                this.comics = new Array<ComicModel>();
            });
        });
        this.collectionService.deleteCollection(id).subscribe(() => {
            this.getCollections();
            this.getWantLists();
        });
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
}
