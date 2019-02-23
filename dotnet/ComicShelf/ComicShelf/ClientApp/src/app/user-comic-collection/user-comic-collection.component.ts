import { OnInit, Component, ViewChild } from '@angular/core';
import { AuthenticateResponse } from '../shared/models/authenticate.model';
import { ComicCollectionService } from '../shared/services/comicCollection.service';
import { UserCollectionService } from '../shared/services/userCollection.service';
import { CollectionService } from '../shared/services/collection.service';
import { ComicService } from '../shared/services/comic.service';
import { ComicCollectionModel } from '../shared/models/comicCollection.model';
import { UserCollectionModel } from '../shared/models/userCollection.model';
import { CollectionModel } from '../shared/models/collection.model';
import { ComicModel } from '../shared/models/comic.model';
import { BaseApiService } from '../shared/services/base.service';
import { ToastrService } from 'ngx-toastr';
import { AuthenticateService } from '../shared/services/authenticate.service';
import { ComicDetailsComponent } from '../comic/comic-details/comic-details.component';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'app-user-comic-collection',
    templateUrl: './user-comic-collection.component.html',
    styleUrls: ['./user-comic-collection.component.css']
})
export class UserComicCollectionComponent implements OnInit {
    @ViewChild('comicDetailsModal') comicDetailsModal: ComicDetailsComponent;
    currentUser: AuthenticateResponse = new AuthenticateResponse();
    comicCollections: Array<ComicCollectionModel>;
    userCollections: Array<UserCollectionModel>;
    collections: Array<CollectionModel>;
    comics: Array<ComicModel>;
    collection: CollectionModel;
    collectionNames: Array<String>;
    collectionId: number;

    constructor(
        private collectionService: CollectionService,
        private toastr: ToastrService,
        private authenticateService: AuthenticateService,
        private userCollectionService: UserCollectionService,
        private comicCollectionService: ComicCollectionService,
    ) {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    ngOnInit(): void {
        this.getCollections();
    }

    getComics(id: number) {
        this.comics = new Array<ComicModel>();
        this.collectionId = id;
        this.collectionService.getComics(id, this.currentUser.id).subscribe(data => {
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
        this.collections = [];
        this.collectionService.getUserCollectionNames(this.currentUser.id).subscribe(data => {
            this.collectionNames = data;
            this.collectionNames.forEach(name => {
                this.collectionService.getCollectionByName(name).subscribe(collectionData => {
                    this.collections.push(collectionData);
                });
            });
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
        this.comicCollectionService.getComicCollection(this.currentUser.id, comicId).subscribe(comicCollectionData => {
            this.userCollectionService.getCollectionByComicCollectionIdAndCollectionId(comicCollectionData.id, collectionid)
                .subscribe(userCollectionData => {
                    this.userCollectionService.deleteCollection(userCollectionData.id).subscribe( () => {
                        this.getComics(collectionid);
                    });
                });
        });
    }
}
