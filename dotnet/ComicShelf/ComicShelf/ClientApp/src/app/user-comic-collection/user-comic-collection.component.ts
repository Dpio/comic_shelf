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
    collectionNames: Array<String>;
    collectionId: number;

    constructor(
        private collectionService: CollectionService,
        private toastr: ToastrService,
        private authenticateService: AuthenticateService,
    ) {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    ngOnInit(): void {
        this.getCollections();
    }

    getComics(id: number) {
        this.comics = new Array<ComicModel>();
        this.collectionId = id;
        this.collectionService.getComicsInCollection(id).subscribe(data => {
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
        const collectionid = this.collectionId;
        this.collectionService.getComicCollection(comicId, collectionid).subscribe(data => {
            this.collectionService.deleteComicFromCollection(data.id).subscribe( () => {
                this.getComics(collectionid);
            });
        });
    }
}
