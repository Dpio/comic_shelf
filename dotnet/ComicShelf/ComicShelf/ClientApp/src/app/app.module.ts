import { BrowserModule, DomSanitizer } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ComicComponent } from './comic/comic.component';
import { ComicService } from './shared/services/comic.service';
import { ToastrService, ToastrModule, ToastContainerModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthGuard } from './login/auth.guard';
import { AuthenticateService } from './shared/services/authenticate.service';
import { LoggedComponent } from './logged/logged.component';
import { UserService } from './shared/services/user.service';
import { JwtInterceptor } from './login/jwt.interceptor';
import { ComicDetailsComponent } from './comic/comic-details/comic-details.component';
import { ModalModule, TypeaheadModule } from 'ngx-bootstrap';
import { ComicAddToCollectionComponent } from './comic/comic-addToCollection/comic-addToCollection.component';
import { UserComicCollectionComponent } from './user-comic-collection/user-comic-collection.component';
import { CollectionService } from './shared/services/collection.service';
import { FilterPipe } from './shared/Utils/filter-pipe';
import { AddCollectionComponent } from './user-comic-collection/add-collection/add-collection.component';
import { MessageService } from './shared/services/messsage.service';
import { RentComponent } from './rents/rent.component';
import { RentService } from './shared/services/rent.service';
import { RequestRentComponent } from './rents/request-rent/request-rent.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ComicComponent,
    LoggedComponent,
    ComicDetailsComponent,
    ComicAddToCollectionComponent,
    UserComicCollectionComponent,
    FilterPipe,
    AddCollectionComponent,
    RentComponent,
    RequestRentComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ModalModule.forRoot(),
    TypeaheadModule.forRoot(),
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'comic', component: ComicComponent, canActivate: [AuthGuard] },
      { path: 'collection', component: UserComicCollectionComponent, canActivate: [AuthGuard] },
      { path: 'Logged', component: LoggedComponent },
      { path: 'Logged/:id/:token', component: LoggedComponent },
      { path: 'rent', component: RentComponent, canActivate: [AuthGuard] },
    ]),
    ToastrModule.forRoot({
      closeButton: true,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    }),
    ToastContainerModule,
  ],
  providers: [
    ComicService,
    ToastrService,
    AuthenticateService,
    UserService,
    AuthGuard,
    CollectionService,
    MessageService,
    RentService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
