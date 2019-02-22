import { BrowserModule, DomSanitizer } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ComicComponent } from './comic/comic.component';
import { ComicService } from './shared/services/comic.service';
import { ToastrService, ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthGuard } from './login/auth.guard';
import { AuthenticateService } from './shared/services/authenticate.service';
import { LoggedComponent } from './logged/logged.component';
import { UserService } from './shared/services/user.service';
import { JwtInterceptor } from './login/jwt.interceptor';
import { ComicDetailsComponent } from './comic/comic-details/comic-details.component';
import { ModalModule, BsDropdownModule } from 'ngx-bootstrap';
import { ComicAddToCollectionComponent } from './comic/comic-addToCollection/comic-addToCollection.component';
import { UserComicCollectionComponent } from './user-comic-collection/user-comic-collection.component';
import { UserCollectionService } from './shared/services/userCollection.service';
import { ComicCollectionService } from './shared/services/comicCollection.service';
import { CollectionService } from './shared/services/collection.service';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ComicComponent,
    LoggedComponent,
    ComicDetailsComponent,
    ComicAddToCollectionComponent,
    UserComicCollectionComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ModalModule.forRoot(),
    BsDropdownModule.forRoot(),
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'comic', component: ComicComponent, canActivate: [AuthGuard] },
      { path: 'collection', component: UserComicCollectionComponent, canActivate: [AuthGuard] },
      { path: 'Logged', component: LoggedComponent },
      { path: 'Logged/:id/:token', component: LoggedComponent },
    ]),
    ToastrModule.forRoot(),
  ],
  providers: [
    ComicService,
    ToastrService,
    AuthenticateService,
    UserService,
    AuthGuard,
    ComicCollectionService,
    UserCollectionService,
    CollectionService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
