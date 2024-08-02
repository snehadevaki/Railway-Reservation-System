import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Seats } from 'src/app/Models/Seat';
import { HttpErrorResponse } from '@angular/common/http';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-save-seats',
  templateUrl: './save-seats.component.html',
  styleUrls: ['./save-seats.component.css']
})
export class SaveSeatsComponent implements OnInit {
  errorMessage: string;
  SeatModelObj: Seats = new Seats();
  seatData!: any;
  formValue !: FormGroup;
  trains!: any
  showAdd !: boolean;
  showUpdate !: boolean;
  constructor(private shared: SharedService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.getAllSeats();
    this.formValue = this.fb.group({

      SeatId: [''],
      TrainId: [''],
      FirstAC: [''],
      SecondAC: [''],
      Sleeper: [''],
      Total: [''],

    })
  }
  getAllSeats() {
    this.shared.getAllSeats().subscribe(res => {
      console.log(res);
      this.seatData = res;

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
  }
  onEdit(row: any) {
    this.errorMessage = "";
    this.showAdd = false;
    this.showUpdate = true;
    this.SeatModelObj.SeatId = row.SeatId;
    this.SeatModelObj.TrainId = row.TrainId;
    this.formValue.controls['TrainId'].setValue(row.TrainId);
    this.formValue.controls['FirstAC'].setValue(row.FirstAC);
    this.formValue.controls['SecondAC'].setValue(row.SecondAC);
    this.formValue.controls['Sleeper'].setValue(row.Sleeper);
    this.formValue.controls['Total'].setValue(row.Total);
  }
  updateSeat() {
    this.errorMessage = "";
    this.SeatModelObj.FirstAC = this.formValue.value.FirstAC;
    this.SeatModelObj.SecondAC = this.formValue.value.SecondAC;
    this.SeatModelObj.Sleeper = this.formValue.value.Sleeper;
    this.SeatModelObj.Total = this.formValue.value.Total
    this.shared.updateSeats(this.SeatModelObj.SeatId, this.SeatModelObj).subscribe(
      (response: any) => {
        alert("Updated successfully");
        let ref = document.getElementById("cancel")
        ref?.click();
        this.formValue.reset();
        this.getAllSeats();
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
  clickAddSeat() {
    this.errorMessage = "";
    this.formValue.reset();
    this.showAdd = true;
    this.showUpdate = false;
  }
  postSeatDetails() {
    this.errorMessage = "";
    this.SeatModelObj.TrainId = this.formValue.value.TrainId;
    this.SeatModelObj.FirstAC = this.formValue.value.FirstAC;
    this.SeatModelObj.SecondAC = this.formValue.value.SecondAC;
    this.SeatModelObj.Sleeper = this.formValue.value.Sleeper;
    this.SeatModelObj.Total = this.formValue.value.Total
    this.shared.saveSeat(this.SeatModelObj).subscribe(
      (response: any) => {
        if (response.message === "Saved") {
          alert("Saved successfully");
          let ref = document.getElementById("cancel");
          if (ref) {
            ref.click();
          }
          this.formValue.reset();
          this.getAllSeats();
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

  }

}
