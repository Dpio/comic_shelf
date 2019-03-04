import { OnInit, Component, ViewChild, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { CollectionService } from '../../shared/services/collection.service';
import { CollectionModel } from '../../shared/models/collection.model';
import { AuthenticateResponse } from '../../shared/models/authenticate.model';
import { ToastrService } from 'ngx-toastr';
import { AuthenticateService } from '../../shared/services/authenticate.service';
import { ComicCollectionModel } from '../../shared/models/comicCollection.model';

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'comic-addToCollection-modal',
    templateUrl: './comic-addToCollection.component.html',
    styleUrls: ['./comic-addToCollection.component.css']
})
export class ComicAddToCollectionComponent implements OnInit {
    @ViewChild('comicAddToCollectionModal') modal: ModalDirective;
    @ViewChild('modalContent') modalContent: ElementRef;
    currentUser: AuthenticateResponse = new AuthenticateResponse();
    collections: Array<CollectionModel>;
    collection: CollectionModel;
    collectionNames: Array<String>;
    collectionName: string;
    comicId: number;

    constructor(
        private collectionService: CollectionService,
        private toastr: ToastrService,
        private authenticateService: AuthenticateService,
    ) {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    ngOnInit(): void {
    }

    show(id: number): void {
        this.comicId = id;
        this.getCollections();
        this.modal.show();
    }

    close(): void {
        this.collections = new Array<CollectionModel>();
        this.modal.hide();
    }

    save(): void {
        const comicCollection = new ComicCollectionModel();
        this.collectionService.getCollectionByName(this.collectionName, this.currentUser.id).subscribe(data => {
            comicCollection.collectionId = data.id;
            comicCollection.comicId = this.comicId;
            this.collectionService.addComicToCollection(comicCollection).subscribe(() => {
            }, error => {
                this.toastr.error(error.error.message);
            }
            );
        });
        this.modal.hide();
    }

    getCollections() {
        this.collections = [];
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
}
