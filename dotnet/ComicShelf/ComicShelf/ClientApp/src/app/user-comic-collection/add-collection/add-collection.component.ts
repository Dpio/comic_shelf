import { Component, ViewChild, ElementRef, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CollectionModel } from '../../shared/models/collection.model';
import { CollectionService } from '../../shared/services/collection.service';
import { AuthenticateResponse } from '../../shared/models/authenticate.model';

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'add-collection-modal',
    templateUrl: './add-collection.component.html',
    styleUrls: ['./add-collection.component.css']
})
export class AddCollectionComponent {
    @ViewChild('addCollectionModal') modal: ModalDirective;
    @ViewChild('modalContent') modalContent: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;
    collection: CollectionModel = new CollectionModel();
    error: any;
    currentUser: AuthenticateResponse = new AuthenticateResponse();
    isWantList: boolean;

    constructor(
        private collectionService: CollectionService,
        private toastr: ToastrService
    ) {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    show(isWantList: boolean): void {
        this.isWantList = isWantList;
        this.modal.show();
    }

    close(): void {
        this.saving = false;
        this.collection = new CollectionModel();
        this.modal.hide();
    }

    save(): void {
        this.saving = true;
        // TODO Public private collections.
        this.collection.isPublic = false;
        if (this.isWantList) {
            const isWantList = this.isWantList;
            if (isWantList) {
                this.collection.isWantList = true;
            } else {
                this.collection.isWantList = false;
            }
        }
        this.collection.userId = this.currentUser.id;
        this.collectionService.createCollection(this.collection).subscribe(() => {
            this.close();
            this.modalSave.emit(null);
        }, error => {
            this.saving = false;
            this.toastr.error(error.error);
        });
    }
}
