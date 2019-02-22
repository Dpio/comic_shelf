import { OnInit, Component } from '@angular/core';
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

@Component({
    selector: 'app-user-comic-collection',
    templateUrl: './user-comic-collection.component.html',
    styleUrls: ['./user-comic-collection.component.css']
})
export class UserComicCollectionComponent implements OnInit {

    currentUser: AuthenticateResponse = new AuthenticateResponse();
    comicCollections: Array<ComicCollectionModel>;
    userCollections: Array<UserCollectionModel>;
    collections: Array<CollectionModel>;
    comics: Array<ComicModel>;
    collection: CollectionModel;

    constructor(
        private comicCollectionService: ComicCollectionService,
        private userCollectionService: UserCollectionService,
        private collectionService: CollectionService,
        private comicService: ComicService,
    ) {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    ngOnInit(): void {
    }
}
