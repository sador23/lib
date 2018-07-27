import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommunicationService } from '../_services/communication.service';

@Component({
  selector: 'app-userpage',
  templateUrl: './userpage.component.html',
  styleUrls: ['./userpage.component.css']
})
export class UserpageComponent implements OnInit {
  id: any;
  model: any;

  constructor(private route: ActivatedRoute, private service : CommunicationService) {
    this.route.params.subscribe(params => {
      this.id = params.id;
    });
  }

  ngOnInit() {
    this.getUser();
  }

  getUser() {
    this.service.getUser(this.id).subscribe(data => {
      this.model = data;
      //console.log(data);
    });
  }

}
