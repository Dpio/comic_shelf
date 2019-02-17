import { Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ComicModel } from '../../shared/models/comic.model';
import { ComicService } from '../../shared/services/comic.service';
import { Moment } from 'moment';
import moment = require('moment');

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'comic-details-modal',
    templateUrl: './comic-details.component.html',
    styleUrls: ['./comic-details.component.css']
})
export class ComicDetailsComponent implements OnInit {
    @ViewChild('comicDetailsModal') modal: ModalDirective;
    @ViewChild('modalContent') modalContent: ElementRef;
    comic: ComicModel = new ComicModel();
    error: any;
    premierDate: string;
    constructor(
        private comicService: ComicService
    ) {
    }

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
}
