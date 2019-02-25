import { Component, OnInit, ViewChild } from '@angular/core';
import { ComicService } from '../shared/services/comic.service';
import { ToastrService } from 'ngx-toastr';
import { ComicModel } from '../shared/models/comic.model';
import { BaseApiService } from '../shared/services/base.service';
import { ComicDetailsComponent } from './comic-details/comic-details.component';
import { AuthenticateService } from '../shared/services/authenticate.service';
import { ComicAddToCollectionComponent } from './comic-addToCollection/comic-addToCollection.component';
import { DomSanitizer } from '@angular/platform-browser';
import { CollectionModel } from '../shared/models/collection.model';
import { AuthenticateResponse } from '../shared/models/authenticate.model';
import { CollectionService } from '../shared/services/collection.service';
import { UserCollectionService } from '../shared/services/userCollection.service';
import { ComicCollectionService } from '../shared/services/comicCollection.service';
import { ComicCollectionModel } from '../shared/models/comicCollection.model';
import { UserCollectionModel } from '../shared/models/userCollection.model';

@Component({
  selector: 'app-comic',
  templateUrl: './comic.component.html',
  styleUrls: ['./comic.component.css']
})
export class ComicComponent implements OnInit {
  @ViewChild('comicDetailsModal') comicDetailsModal: ComicDetailsComponent;
  comics: Array<ComicModel>;
  currentUser: AuthenticateResponse = new AuthenticateResponse();
  collections: Array<CollectionModel>;
  collection: CollectionModel;
  collectionNames: Array<String>;
  collectionName: string;

  constructor(
    private comicService: ComicService,
    private toastr: ToastrService,
    private authenticateService: AuthenticateService,
    private collectionService: CollectionService,
    private userCollectionService: UserCollectionService,
    private comicCollectionService: ComicCollectionService,
  ) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }
  ngOnInit(): void {
    this.comicService.getAllComics().subscribe(data => {
      this.comics = BaseApiService.getObjectArrayFromApi<ComicModel>(data, ComicModel);
    }, error => {
      if (error.statusText === 'Unauthorized') {
        this.toastr.error(error.statusText);
        this.authenticateService.logout();
      } else {
        this.toastr.error(error.error);
      }
    }
    );
    this.getCollections();
  }

  comicDetails(id: number) {
    this.comicDetailsModal.show(id);
  }

  getCollections() {
    this.collections = [];
    this.collectionService.getUserCollectionNames(this.currentUser.id).subscribe(data => {
      this.collectionNames = data;
      this.collectionNames.forEach(name => {
        this.collectionService.getCollectionByName(name).subscribe(collectionData => {
          this.collections.push(collectionData);
        });
      });
    }, error => {
      if (error.statusText === 'Unauthorized') {
        this.toastr.error(error.statusText);
        this.authenticateService.logout();
      } else {
        this.toastr.error(error.error);
      }
    });
  }

  addToCollection(collectionName: String, comicId: number) {
    this.collectionService.getCollectionByName(collectionName).subscribe(col => {
      this.collection = col;
      const comicCollection = new ComicCollectionModel;
      comicCollection.userId = this.currentUser.id;
      comicCollection.comicId = comicId;
      const userCollection = new UserCollectionModel;
      this.comicCollectionService.getComicCollection(this.currentUser.id, comicId).subscribe(comcolData => {
        if (comcolData) {
          userCollection.collectionId = col.id;
          userCollection.comicCollectionId = comcolData.id;
          this.userCollectionService.createCollection(userCollection).subscribe(userColData => {
          }, error => {
            if (error.statusText === 'Unauthorized') {
              this.toastr.error(error.statusText);
              this.authenticateService.logout();
            } else {
              this.toastr.error(error.error);
            }
          });
        } else {
          this.comicCollectionService.createCollection(comicCollection).subscribe(comicData => {
            userCollection.collectionId = col.id;
            userCollection.comicCollectionId = comicData.id;
            this.userCollectionService.createCollection(userCollection).subscribe(userColData => {
            }, error => {
              if (error.statusText === 'Unauthorized') {
                this.toastr.error(error.statusText);
                this.authenticateService.logout();
              } else {
                this.toastr.error(error.error);
              }
            });
          }, error => {
            if (error.statusText === 'Unauthorized') {
              this.toastr.error(error.statusText);
              this.authenticateService.logout();
            } else {
              this.toastr.error(error.error);
            }
          });
        }
      }, error => {
        if (error.statusText === 'Unauthorized') {
          this.toastr.error(error.statusText);
          this.authenticateService.logout();
        } else {
          this.toastr.error(error.error);
        }
      });
    });
  }
}
