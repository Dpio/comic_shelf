import { OnInit, Component, ViewChild, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { CollectionService } from '../../shared/services/collection.service';
import { CollectionModel } from '../../shared/models/collection.model';
import { AuthenticateResponse } from '../../shared/models/authenticate.model';
import { ToastrService } from 'ngx-toastr';
import { AuthenticateService } from '../../shared/services/authenticate.service';
import { Observable } from 'rxjs/Observable';
import { UserCollectionService } from '../../shared/services/userCollection.service';
import { ComicCollectionService } from '../../shared/services/comicCollection.service';
import { ComicCollectionModel } from '../../shared/models/comicCollection.model';
import { UserCollectionModel } from '../../shared/models/userCollection.model';

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
        private userCollectionService: UserCollectionService,
        private comicCollectionService: ComicCollectionService,
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
        this.collectionService.getCollectionByName(this.collectionName).subscribe(col => {
            this.collection = col;
            const comicCollection = new ComicCollectionModel;
            comicCollection.userId = this.currentUser.id;
            comicCollection.comicId = this.comicId;
            const userCollection = new UserCollectionModel;
            this.comicCollectionService.getComicCollection(this.currentUser.id, this.comicId).subscribe(comcolData => {
                if (comcolData) {
                    userCollection.collectionId = col.id;
                    userCollection.comicCollectionId = comcolData.id;
                    this.userCollectionService.createCollection(userCollection).subscribe(userColData => {
                    }, error => {
                        if (error.statusText === 'Unauthorized') {
                            this.toastr.error(error.statusText);
                            this.authenticateService.logout();
                        } else {
                            this.toastr.error(error.error);
                        }
                    });
                } else {
                    this.comicCollectionService.createCollection(comicCollection).subscribe(comicData => {
                        userCollection.collectionId = col.id;
                        userCollection.comicCollectionId = comicData.id;
                        this.userCollectionService.createCollection(userCollection).subscribe(userColData => {
                        }, error => {
                            if (error.statusText === 'Unauthorized') {
                                this.toastr.error(error.statusText);
                                this.authenticateService.logout();
                            } else {
                                this.toastr.error(error.error);
                            }
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
            }, error => {
                if (error.statusText === 'Unauthorized') {
                    this.toastr.error(error.statusText);
                    this.authenticateService.logout();
                } else {
                    this.toastr.error(error.error);
                }
            });
        });

        this.modal.hide();
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
}
