import { Component, OnInit, ViewChild } from '@angular/core';
import { ComicService } from '../shared/services/comic.service';
import { ToastrService } from 'ngx-toastr';
import { ComicModel } from '../shared/models/comic.model';
import { BaseApiService } from '../shared/services/base.service';
import { ComicDetailsComponent } from './comic-details/comic-details.component';
import { AuthenticateService } from '../shared/services/authenticate.service';
import { ComicAddToCollectionComponent } from './comic-addToCollection/comic-addToCollection.component';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-comic',
  templateUrl: './comic.component.html',
  styleUrls: ['./comic.component.css']
})
export class ComicComponent implements OnInit {
  @ViewChild('comicDetailsModal') comicDetailsModal: ComicDetailsComponent;
  @ViewChild('comicAddToCollectionModal') comicAddToCollectionModal: ComicAddToCollectionComponent;
  comics: Array<ComicModel>;

  constructor(
    private comicService: ComicService,
    private toastr: ToastrService,
    private authenticateService: AuthenticateService,
    private sanitizer: DomSanitizer,
  ) {
  }
  ngOnInit(): void {
    this.comicService.getAllComics().subscribe(data => {
      this.comics = BaseApiService.getObjectArrayFromApi<ComicModel>(data, ComicModel);
    }, error => {
      if (error.statusText === 'Unauthorized') {
        this.toastr.error(error.statusText);
        this.authenticateService.logout();
      } else {
        const response = error.response.replace(/['"]+/g, '');
        const message = response.replace('message:', '');
        this.toastr.error(message.replace(/[{}]+/g, ''));
      }
    }
    );
  }

  comicDetails(id: number) {
    this.comicDetailsModal.show(id);
  }

  addToCollection(id: number) {
    this.comicAddToCollectionModal.show(id);
  }
}
