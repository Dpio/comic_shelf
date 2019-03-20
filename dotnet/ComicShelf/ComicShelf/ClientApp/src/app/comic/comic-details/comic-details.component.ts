import { Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ComicModel } from '../../shared/models/comic.model';
import { ComicService } from '../../shared/services/comic.service';
import moment = require('moment');
import { ComicAddToCollectionComponent } from '../comic-addToCollection/comic-addToCollection.component';

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'comic-details-modal',
    templateUrl: './comic-details.component.html',
    styleUrls: ['./comic-details.component.css']
})
export class ComicDetailsComponent implements OnInit {
    @ViewChild('comicDetailsModal') modal: ModalDirective;
    @ViewChild('modalContent') modalContent: ElementRef;
    @ViewChild('comicAddToCollectionModal') comicAddToCollectionModal: ComicAddToCollectionComponent;
    comic: ComicModel = new ComicModel();
    error: any;
    image: File;
    premierDate: string;
    constructor(
        private comicService: ComicService,
    ) {
    }
    // TODO : Failed to load resource: net::ERR_UNKNOWN_URL_SCHEME [unsafe:data:image/jpeg;base64,]
    // WARNING: sanitizing unsafe URL value data:image/jpeg;base64, (see http://g.co/ng/security#xss)

    ngOnInit(): void {
    }

    refresh(id: number): void {
        this.comicService.getComic(id).subscribe(data => {
            this.comic = data;
            if (data) {
                this.premierDate = moment(data.premierDate).format('DD.MM.YYYY');
            }
        });
    }

    show(id: number): void {
        this.refresh(id);
        this.modal.show();
    }

    close(): void {
        this.modal.hide();
    }

    addToCollection(id: number) {
        this.comicAddToCollectionModal.show(id);
    }
}
