import { OnInit, Component, ViewChild, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'comic-addToCollection-modal',
    templateUrl: './comic-addToCollection.component.html',
    styleUrls: ['./comic-addToCollection.component.css']
})
export class ComicAddToCollectionComponent implements OnInit {
    @ViewChild('comicAddToCollectionModal') modal: ModalDirective;
    @ViewChild('modalContent') modalContent: ElementRef;

    ngOnInit(): void {
    }

    refresh(id: number): void {
        // this.comicService.getComic(id).subscribe(data => {
        //     this.comic = data;
        //     if (data) {
        //         this.premierDate = moment(data.premierDate).format('DD.MM.YYYY');
        //     }
        // });
    }

    show(id: number): void {
        // this.refresh(id);
        this.modal.show();
    }

    close(): void {
        this.modal.hide();
    }

    save(): void {
    }
}
