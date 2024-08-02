import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Train } from 'src/app/Models/Train';
import { NavbarService } from 'src/app/navbar.service';
import { HttpErrorResponse } from '@angular/common/http';
import { SharedService } from 'src/app/shared.service';
import { DatePipe } from '@angular/common';



@Component({
  selector: 'app-save-trains',
  templateUrl: './save-trains.component.html',
  styleUrls: ['./save-trains.component.css'],
  providers: [DatePipe] // Add DatePipe to the providers array
})
export class SaveTrainsComponent implements OnInit {
  errorMessage: string;
  formValue !: FormGroup;
  trainModelObj: Train = new Train();
  trainData !: any;
  showAdd !: boolean;
  showUpdate !: boolean;

  constructor(private shared:SharedService,private fb:FormBuilder,private nav:NavbarService, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.nav.hide();

    this.formValue=this.fb.group({

      TrainId:[''],
      Name:[''],
      ArrivalTime:[''],
      DepartureTime: [''],
      ArrivalDate:[''],
      DepartureDate:[''],
      ArrivalStation:[''],
      DepartureStation:[''],
      Distance:[''],
      /*
      TrainId:[''],
      Name:['CHENNAI EXP'],
      ArrivalTime:['20:00'],
      DepartureTime: ['08:00'],
      ArrivalDate:[this.datePipe.transform('11/06/2024', 'yyyy-MM-dd') ?? ''],
      DepartureDate:[this.datePipe.transform('10/06/2024', 'yyyy-MM-dd') ?? ''],
      ArrivalStation:['Madurai'],
      DepartureStation:['Chennai'],
      Distance:['480'],
      */
    })
    this.getAllTrain();
  }


onEdit(row:any){
  this.errorMessage = "";
    this.showAdd=false;
    this.showUpdate=true;
    let formattedDate = this.datePipe.transform(row.DepartureDate, 'yyyy-MM-dd') ?? '';

    this.formValue.controls['DepartureDate'].setValue(formattedDate )

    //formattedDate = this.datePipe.transform(row.ArrivalDate, 'dd/MM/yyyy')?? '';
    formattedDate = this.datePipe.transform(row.ArrivalDate, 'yyyy-MM-dd') ?? '';
    this.formValue.controls['ArrivalDate'].setValue(formattedDate);




    this.formValue.controls['TrainId'].setValue(row.TrainId)
    // this.formValue.controls['SeatId'].setValue(row.seatId);
    this.formValue.controls['Name'].setValue(row.Name.toUpperCase());
    this.formValue.controls['ArrivalTime'].setValue(row.ArrivalTime);
    this.formValue.controls['DepartureTime'].setValue(row.DepartureTime);
    //this.formValue.controls['ArrivalDate'].setValue(row.ArrivalDate);
    //this.formValue.controls['DepartureDate'].setValue(row.DepartureDate);
    this.formValue.controls['ArrivalStation'].setValue(row.ArrivalStation.toUpperCase());
    this.formValue.controls['DepartureStation'].setValue(row.DepartureStation.toUpperCase());
    this.formValue.controls['Distance'].setValue(row.distance);
}
updateTrain(){
  this.errorMessage = "";
  this.trainModelObj.trainId=this.formValue.value.TrainId;
  this.trainModelObj.name=this.formValue.value.Name.toUpperCase();
  this.trainModelObj.arrivalDate=this.formValue.value.ArrivalDate;
  //this.trainModelObj.departureDate=this.formValue.value.DepartureDate;

  this.trainModelObj.departureDate = this.datePipe.transform(this.formValue.value.DepartureDate, 'dd/MM/yyyy') ?? '';

  this.trainModelObj.departureTime=this.formValue.value.DepartureTime;
  this.trainModelObj.arrivalTime=this.formValue.value.ArrivalTime;
  this.trainModelObj.arrivalStation=this.formValue.value.ArrivalStation.toUpperCase();
  this.trainModelObj.departureStation=this.formValue.value.DepartureStation.toUpperCase();
  this.trainModelObj.distance=this.formValue.value.Distance;
  /*
  this.shared.updateTrain(this.trainModelObj).subscribe(res=>{
    alert("Updated successfully");
    let ref = document.getElementById("cancel")
    ref?.click();
    this.formValue.reset();
    this.getAllTrain();
  })
  */
  this.shared.updateTrain(this.trainModelObj).subscribe(
    (response: any) => {
      alert("Updated successfully");
      let ref = document.getElementById("cancel")
      ref?.click();
      this.formValue.reset();
      this.getAllTrain();
    },
    (error: HttpErrorResponse) => {
      if (error.status === 400) {
        const errorMessages = error.error.errors;
        let errorMessage = '';
        for (const key in errorMessages) {
          if (errorMessages.hasOwnProperty(key)) {
            errorMessage += `${key}: ${errorMessages[key][0]}\n`;
          }
        }
        this.errorMessage = errorMessage;

      }
      else {
        console.clear();

        console.error(error.error.message); // "Error occurred. Please try again."
        console.error(error.error.innerException); // Inner exception message
        console.error(error.error.stackTrace); // Full stack trace
        this.errorMessage = error.error.message;
        alert(error.error.message + '\n' + error.error.innerException + '\n' + error.error.stackTrace);
      }
    }

  )
}

deleteTrain(id:number){
  if(confirm('Are you sure?')){
    this.shared.deleteTrain(id).subscribe(data=>{
      console.log(data);

    });
    location.reload();
  }

}
clickAddTrain(){
  this.errorMessage = "";
  this.formValue.reset();
  this.showAdd=true;
  this.showUpdate=false;
}
postTrainDetails(){
  this.errorMessage = "";
  this.trainModelObj.trainId = 0;
  this.trainModelObj.name=this.formValue.value.Name.toUpperCase();
  this.trainModelObj.arrivalDate=this.formValue.value.ArrivalDate;
  this.trainModelObj.departureDate=this.formValue.value.DepartureDate;
  this.trainModelObj.departureTime=this.formValue.value.DepartureTime;
  this.trainModelObj.arrivalTime=this.formValue.value.ArrivalTime;
  this.trainModelObj.arrivalStation=this.formValue.value.ArrivalStation.toUpperCase();;
  this.trainModelObj.departureStation=this.formValue.value.DepartureStation.toUpperCase();;
  this.trainModelObj.distance=this.formValue.value.Distance;
  this.shared.saveTrain(this.trainModelObj).subscribe(
    (response: any) => {
      if (1==1) { //response.message === "Saved"
        alert("Train added successfully");
        let ref = document.getElementById("cancel");
        if (ref) {
          ref.click();
        }
        this.formValue.reset();
        this.getAllTrain();
      } else {
        //alert("from saved seats");
        console.error(response.message); // "Error occurred. Please try again."
        console.error(response.innerException); // Inner exception message
        console.error(response.stackTrace); // Full stack trace
        alert(response.message + '\n' + response.innerException + '\n' + response.stackTrace);

        //console.error(response.message); // "Error occurred. Please try again."
        //alert(response.message);
      }
    },
    (error: HttpErrorResponse) => {
      if (error.status === 400) {
        const errorMessages = error.error.errors;
        let errorMessage = '';
        for (const key in errorMessages) {
          if (errorMessages.hasOwnProperty(key)) {
            errorMessage += `${key}: ${errorMessages[key][0]}\n`;
          }
        }
        this.errorMessage = errorMessage;

      }
      else {
        console.clear();

        console.error(error.error.message); // "Error occurred. Please try again."
        console.error(error.error.innerException); // Inner exception message
        console.error(error.error.stackTrace); // Full stack trace
        this.errorMessage = error.error.message;
        alert(error.error.message + '\n' + error.error.innerException + '\n' + error.error.stackTrace);
        //alert("An error occurred while saving(b): " + error.error.message);
      }
    }

  );
  /*
  this.shared.saveTrain(this.trainModelObj).subscribe(res=>{
    console.log(res),
    alert("Train added successfully");
    let ref = document.getElementById("cancel")
    ref?.click();
    this.formValue.reset();
    this.getAllTrain();
  },
  error=>{
    alert("Something is wrong");
  });
  */
}
getAllTrain(){
  this.shared.getAllTrains().subscribe(res=>{
    this.trainData = res;
  },
  (error: HttpErrorResponse) => {
    if (error.status === 400) {
      const errorMessages = error.error.errors;
      let errorMessage = '';
      for (const key in errorMessages) {
        if (errorMessages.hasOwnProperty(key)) {
          errorMessage += `${key}: ${errorMessages[key][0]}\n`;
        }
      }
      this.errorMessage = errorMessage;

    }
    else {
      console.clear();

      console.error(error.error.message); // "Error occurred. Please try again."
      console.error(error.error.innerException); // Inner exception message
      console.error(error.error.stackTrace); // Full stack trace
      this.errorMessage = error.error.message;
      alert(error.error.message + '\n' + error.error.innerException + '\n' + error.error.stackTrace);
      //alert("An error occurred while saving(b): " + error.error.message);
    }
  }

)
}


}
