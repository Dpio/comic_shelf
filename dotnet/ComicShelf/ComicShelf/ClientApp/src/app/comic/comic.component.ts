import { Component, OnInit } from '@angular/core';
import { ComicService } from '../shared/services/comic.service';
import { ToastrService } from 'ngx-toastr';
import { ComicModel } from '../shared/models/comic.model';
import { BaseApiService } from '../shared/services/base.service';

@Component({
  selector: 'app-comic',
  templateUrl: './comic.component.html',
  styleUrls: ['./comic.component.css']
})
export class ComicComponent implements OnInit {

  comics: Array<ComicModel>;

  constructor(
    private comicService: ComicService,
    private toastr: ToastrService,
  ) {
  }
  ngOnInit(): void {
    this.comicService.getAllComics().subscribe(data => {
      this.comics = BaseApiService.getObjectArrayFromApi<ComicModel>(data, ComicModel);
    }, error => {
      if (error.statusText === 'Unauthorized') {
        this.toastr.error(error.statusText);
      } else {
        const response = error.response.replace(/['"]+/g, '');
        const message = response.replace('message:', '');
        this.toastr.error(message.replace(/[{}]+/g, ''));
      }
    }
    );
  }

}
