import { Component, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import { ComicModel } from '../../shared/models/comic.model';
import { ComicService } from '../../shared/services/comic.service';

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'comic-details-modal',
    templateUrl: './comic-details.component.html',
    styleUrls: ['./comic-details.component.css']
})
export class ComicDetailsComponent {
    @ViewChild('modalContent') modalContent: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    error: any;
    comic: ComicModel = new ComicModel();

    constructor(
      private comicService: ComicService
    ) {
    }
}
