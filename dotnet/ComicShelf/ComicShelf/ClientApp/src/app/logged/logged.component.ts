import { OnInit, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticateResponse } from '../shared/models/authenticate.model';
import { UserService } from '../shared/services/user.service';


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
                    guser.username = data.username;
                    guser.emailAddress = data.emailAddress;
                    guser.token = pr.token;
                    localStorage.setItem('currentUser', JSON.stringify(guser));
                    this.route.navigate(['']);
                    location.reload(true);
                });
            }
        });
    }
}