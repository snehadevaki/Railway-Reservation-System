import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Seats } from 'src/app/Models/Seat';
import { Train } from 'src/app/Models/Train';
import { NavbarService } from 'src/app/navbar.service';
import { SharedService } from 'src/app/shared.service';


@Component({
  selector: 'app-trains',
  templateUrl: './trains.component.html',
  styleUrls: ['./trains.component.css']
})
export class TrainsComponent implements OnInit {
  uniqueArrivalStations: string[] = [];
  uniqueDeparatureStations: string[] = [];
  formValue!: FormGroup;
  trainModelObj: Train = new Train()
  trainData!: any;
  seats !: Seats[];
  seatData!: any;
  traindata: any;
  constructor(private fb: FormBuilder, private shared: SharedService, private nav: NavbarService, private router: Router) { }

  ngOnInit(): void {

    this.nav.hide();
    this.nav.doSomethingElseUseful();
    this.formValue = this.fb.group({
      ArrivalStation: [''],
      DepartureStation: [''],
      Date: ['']
    });
    this.getAllTrain();
  }
  SearchTrain() {
    // // this.trainModelObj.arrivalStation=this.formValue.value.ArrivalStation;
    // // this.trainModelObj.departureStation=this.formValue.value.DepartureStation;
    // // this.trainModelObj.arrivalDate=this.formValue.value.Date;
    this.shared.SearchTrain(this.formValue.value.ArrivalStation, this.formValue.value.DepartureStation, this.formValue.value.Date).subscribe(res => {


      //yyy console.log(res);
      this.trainData = res;
      this.seatData = res;
      if (res == null || Object.keys(res).length === 0) {
        alert("No Train Found");
      }
      this.formValue.reset();


    }, error => {
      alert("No Train Found");
    });
  }

  GetTrainById(id: number) {
    this.trainModelObj.trainId = id;
    this.shared.getTrainbyId(id).subscribe((res) => {
      console.log(res);
      localStorage.setItem('trainId', JSON.stringify(res));
      this.router.navigateByUrl('/login/user/dashboard/add-passenger');
    })
  }
  getAllTrain() {
    this.shared.getAllTrains().subscribe(response => {
      this.traindata = response;
      console.log(response);
      let arrivalStationsSet = new Set<string>();
      this.traindata.forEach((row: { ArrivalStation: string; }) => {
        arrivalStationsSet.add(row.ArrivalStation);
      });
      this.uniqueArrivalStations = Array.from(arrivalStationsSet);

      let departureStationsSet = new Set<string>();
      this.traindata.forEach((row: { DepartureStation: string; }) => {
        departureStationsSet.add(row.DepartureStation);
      });
      this.uniqueDeparatureStations = Array.from(departureStationsSet);

    })
  }

}
