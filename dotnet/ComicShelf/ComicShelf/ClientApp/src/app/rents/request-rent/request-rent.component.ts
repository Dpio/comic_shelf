import { OnInit, Component, ViewChild, ElementRef, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { RentService } from '../../shared/services/rent.service';
import { CollectionService } from '../../shared/services/collection.service';
import { AuthenticateResponse } from '../../shared/models/authenticate.model';
import { UserModel } from '../../shared/models/user.model';
import { BaseApiService } from '../../shared/services/base.service';
import { ToastrService } from 'ngx-toastr';
import { RentModel } from '../../shared/models/rent.model';
import moment = require('moment');
import { MessageService } from '../../shared/services/messsage.service';
import { MessageModel } from '../../shared/models/message.model';
import { ComicService } from '../../shared/services/comic.service';
import { ComicModel } from '../../shared/models/comic.model';

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'request-rent-modal',
    templateUrl: './request-rent.component.html',
    styleUrls: ['./request-rent.component.css']
})
export class RequestRentComponent implements OnInit {
    @ViewChild('requestRentModal') modal: ModalDirective;
    @ViewChild('modalContent') modalContent: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    error: any;
    comicId: number;
    saving = false;
    currentUser: AuthenticateResponse = new AuthenticateResponse();
    users: Array<UserModel>;
    comic: ComicModel;

    // TODO:  Nake html if there is no user with comic in collection
    // User can make only 4 request
    constructor(
        private rentService: RentService,
        private collectionService: CollectionService,
        private toastrService: ToastrService,
        private messageService: MessageService,
        private comicService: ComicService,
    ) {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    ngOnInit(): void {
        this.users = [];
    }

    refresh(id: number): void {
        this.collectionService.findUsersWithComic(this.currentUser.id, id).subscribe(data => {
            this.users = BaseApiService.getObjectArrayFromApi<UserModel>(data, UserModel);
        });
    }

    show(id: number): void {
        this.comicService.getComic(id).subscribe(data => {
            this.comic = data;
        });
        this.refresh(id);
        this.modal.show();
    }

    close(): void {
        this.saving = false;
        this.modal.hide();
    }

    // TODO: User can only make 4 requests for a comic.
    save(): void {
        this.users.filter(e => e.isSelected).forEach(e => {
            this.saving = true;
            const rent = new RentModel();
            rent.comicId = this.comic.id;
            rent.startDate = moment(Date.now());
            rent.status = 1;
            rent.receiverId = this.currentUser.id;
            rent.giverId = e.id;
            this.rentService.createRent(rent).subscribe(() => {
                const message = new MessageModel();
                message.msg = this.currentUser.givenName + ' has requested: ' + this.comic.title;
                message.userId = e.id;
                this.messageService.BroadcastMessageForUser(message).subscribe(() => {
                });
                this.close();
                this.modalSave.emit(null);
            }, error => {
                this.toastrService.error(error.error.message);
            });
        });
    }
}
