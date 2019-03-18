import { OnInit, Component } from '@angular/core';
import { AuthenticateResponse } from '../shared/models/authenticate.model';
import { RentService } from '../shared/services/rent.service';
import { RentModel, RentStatus } from '../shared/models/rent.model';
import { BaseApiService } from '../shared/services/base.service';
import { MessageModel } from '../shared/models/message.model';
import { MessageService } from '../shared/services/messsage.service';
import moment = require('moment');
import { Router, NavigationEnd } from '@angular/router';
import { UpdateRentModel } from '../shared/models/updateRent.model';

@Component({
    selector: 'app-rent',
    templateUrl: './rent.component.html',
    styleUrls: ['./rent.component.css'],
})
export class RentComponent implements OnInit {
    currentUser: AuthenticateResponse = new AuthenticateResponse();
    rents: Array<RentModel>;
    toDelete: Array<RentModel>;

    constructor(
        private rentService: RentService,
        private messageService: MessageService,
        private router: Router,
    ) {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.router.routeReuseStrategy.shouldReuseRoute = function () {
            return false;
        };
        this.router.events.subscribe((evt) => {
            if (evt instanceof NavigationEnd) {
               this.router.navigated = false;
               window.scrollTo(0, 0);
            }
        });
    }

    // TODO: Rents history, show only pending and in progress.
    ngOnInit(): void {
        this.getRents();
    }

    getRents() {
        this.rentService.GetRentsForUser(this.currentUser.id).subscribe((data: Array<RentModel>) => {
            this.rents = BaseApiService.getObjectArrayFromApi<RentModel>(data, RentModel);
            this.rents.forEach(rent => {
                if (rent.status === 4) {
                    const newRent = new UpdateRentModel();
                    newRent.id = rent.id;
                    newRent.comicId = rent.comicId;
                    newRent.giverId = rent.giverId;
                    newRent.receiverId = rent.receiverId;
                    newRent.startDate = rent.startDate;
                    newRent.endDate = rent.endDate;
                    newRent.status = 1;
                    this.rentService.putRent(newRent).subscribe(() => {
                    });
                }
            });
        });
    }

    Accept(rent: RentModel) {
        const newRent = new UpdateRentModel();
        newRent.id = rent.id;
        newRent.comicId = rent.comicId;
        newRent.giverId = rent.giverId;
        newRent.receiverId = rent.receiverId;
        newRent.startDate = rent.startDate;
        newRent.endDate = rent.endDate;
        newRent.status = 2;
        this.rentService.putRent(newRent).subscribe(() => {
            this.getRents();
        });
        this.rentService.GetPendingRequestsCountForComicByUser(this.currentUser.id, rent.comicId).subscribe(data => {
            this.toDelete = data;
            this.toDelete.forEach(rentToDelete => {
                this.rentService.deleteRent(rentToDelete.id).subscribe( () => {
                    this.getRents();
                });
            });
        });
    }

    Decline(id: number, giverName: string, title: string, receiverId: number) {
        this.rentService.deleteRent(id).subscribe(() => {
            this.getRents();
        });
        const message = new MessageModel();
        message.msg = giverName + ' has desclined your request of ' + title;
        message.userId = receiverId;
        this.messageService.BroadcastMessageForUser(message).subscribe(() => {
        });
    }

    Complete(rent: RentModel) {
        const newRent = new UpdateRentModel();
        newRent.id = rent.id;
        newRent.comicId = rent.comicId;
        newRent.giverId = rent.giverId;
        newRent.receiverId = rent.receiverId;
        newRent.startDate = rent.startDate;
        newRent.status = 3;
        newRent.endDate = moment(Date.now());
        this.rentService.putRent(newRent).subscribe(() => {
            this.getRents();
        });
    }

    getStatusName(status: number) {
        const st = RentStatus[status];
        return st;
    }
}

