import { OnInit, Component, ViewChild } from '@angular/core';
import { AuthenticateResponse } from '../shared/models/authenticate.model';
import { ToastrService } from 'ngx-toastr';
import { RentService } from '../shared/services/rent.service';
import { RentModel, RentStatus } from '../shared/models/rent.model';
import { BaseApiService } from '../shared/services/base.service';
import { MessageModel } from '../shared/models/message.model';
import { MessageService } from '../shared/services/messsage.service';
import moment = require('moment');
import { NavMenuComponent } from '../nav-menu/nav-menu.component';

@Component({
    selector: 'app-rent',
    templateUrl: './rent.component.html',
    styleUrls: ['./rent.component.css']
})
export class RentComponent implements OnInit {
    currentUser: AuthenticateResponse = new AuthenticateResponse();
    rents: Array<RentModel>;

    constructor(
        private toastr: ToastrService,
        private rentService: RentService,
        private messageService: MessageService,
    ) {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    // TODO: Rents history show only pending and in progress.
    // Change the dates to specific format.
    ngOnInit(): void {
        this.getRents();
    }

    getRents() {
        this.rentService.GetRentsForUser(this.currentUser.id).subscribe((data: Array<RentModel>) => {
            this.rents = BaseApiService.getObjectArrayFromApi<RentModel>(data, RentModel);
            this.rents.forEach(rent => {
                if (rent.status === 4) {
                    const newRent = rent;
                    newRent.status = 1;
                    this.rentService.putRent(newRent).subscribe(() => {
                    });
                }
            });
        });
    }

    Accept(rent: RentModel) {
        rent.status = 2;
        this.rentService.putRent(rent).subscribe(() => {
            this.getRents();
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
        // TODO: Put shouldnt require comic and two users
        rent.status = 3;
        rent.endDate = moment(Date.now());
        this.rentService.putRent(rent).subscribe(() => {
            this.getRents();
        });
    }

    getStatusName(status: number) {
        const st = RentStatus[status];
        return st;
    }
}

