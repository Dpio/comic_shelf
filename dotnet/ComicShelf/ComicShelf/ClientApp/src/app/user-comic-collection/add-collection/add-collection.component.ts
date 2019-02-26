import { Component, ViewChild, ElementRef, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CollectionModel } from '../../shared/models/collection.model';
import { CollectionService } from '../../shared/services/collection.service';

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

    constructor(
        private collectionService: CollectionService,
        private toastr: ToastrService
    ) {
    }

    show(): void {
        this.modal.show();
    }

    close(): void {
        this.saving = false;
        this.collection = new CollectionModel();
        this.modal.hide();
    }

    save(): void {
        this.saving = true;
        this.collection.isPublic = false;
        this.collectionService.createCollection(this.collection).subscribe(() => {
            this.close();
            this.modalSave.emit(null);
        }, error => {
            this.toastr.error(error.error);
        });
    }
}
