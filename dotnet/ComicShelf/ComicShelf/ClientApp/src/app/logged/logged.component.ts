import { OnInit, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticateResponse } from '../shared/models/authenticate.model';
import { UserService } from '../shared/services/user.service';
import { RentService } from '../shared/services/rent.service';


@Component({
    selector: 'app-logged',
    templateUrl: './logged.component.html'
})
export class LoggedComponent implements OnInit {

    constructor(private route: Router,
        private router: ActivatedRoute,
        private userService: UserService,
    ) { }

    ngOnInit(): void {
        this.router.params.subscribe(pr => {
            if (pr.id && pr.token) {
                this.userService.getUser(pr.id).subscribe(data => {
                    const guser = new AuthenticateResponse();
                    guser.id = data.id;
                    guser.givenName = data.givenName;
                    guser.email = data.email;
                    guser.token = pr.token;
                    localStorage.setItem('currentUser', JSON.stringify(guser));
                    this.route.navigate(['']);
                    location.reload(true);
                });
            }
        });
    }
}
