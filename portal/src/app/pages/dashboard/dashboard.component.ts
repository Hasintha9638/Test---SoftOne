import { Component, OnInit, ViewChild } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { DashboardService } from '../dashboard.service';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms'
import { Environment } from 'src/environments/environments';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit{
  @ViewChild('closeButton') closeButton: any;
  dataSource!: CustomStore;
  profileImages: string[] = [
    './assets/images/images-1.png',
    './assets/images/images-2.png',
    './assets/images/images-3.png',
    './assets/images/images-4.png'
  ];
  studentForm !: FormGroup;
  requiredMessage: string = '';
  formTitle: string = 'Add Student';
  isValid = true;
  selectedImageIndex: number | null = null;
  selectedStudent:any = {};
  constructor(private dashboardService: DashboardService, 
    private toastr: ToastrService, private formbuilder: FormBuilder) { }
  ngOnInit(): void {
    this.loadTableData();
    this.studentForm = this.formbuilder.group({
      first_Name: ['', Validators.required],
      last_Name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      nic: ['', Validators.required],
      address: ['', Validators.required],
      dob: ['', Validators.required],
      profile_url: ['', Validators.required] 
    });
  }

  loadTableData() {
    this.dataSource = new CustomStore({
      key: 'id',
      load: () => {
        return this.dashboardService.getStudentData().toPromise().then( (data:any) => {
          console.log(data);
          return data;
        }, (error: any) => {
          this.toastr.error('Error', 'Cannot load data!');
          console.log(error);
          return null;
        });
        
      },
      update: (key, values) => {
        console.log(key, values);
        //i need to get all cell values here
        let data = {
          first_Name: values.first_Name,
          last_Name: values.last_Name,
          email: values.email,
          phone: values.phone,
          nic: values.nic,
          address: values.address,
          dob: values.dob,
        };
        return this.dashboardService.updateStudent(key, data).toPromise().then((data: any) => {
          return data;
        }, (error: any) => {
          console.log(error)
        });
      },
      remove: (key) => {
        return this.dashboardService.deleteStudent(key).toPromise().then((data: any) => {
          return data;
        }, (error: any) => {
          console.log(error)
        });
      }
    });
  }

  onSubmit(){
    console.log(this.studentForm.value);
    let data = {
      first_Name: this.studentForm.value.first_Name,
      last_Name: this.studentForm.value.last_Name,
      email: this.studentForm.value.email,
      phone: this.studentForm.value.phone,
      nic: this.studentForm.value.nic,
      address: this.studentForm.value.address,
      dob: this.studentForm.value.dob,
      profile_url: this.studentForm.value.profile_url,
    };
    if (this.studentForm.value.email == '' || this.studentForm.value.first_name == '' || this.studentForm.value.last_name == '' 
    || this.studentForm.value.phone == '' || this.studentForm.value.address == '' || this.studentForm.value.dob == '' || this.studentForm.value.nic == '') {
      this.requiredMessage = 'All data fields are required!';
      return;
    }
    this.dashboardService.addStudent(data).toPromise().then((data: any) => {
      this.toastr.success('Success', 'Student added successfully!');
      this.requiredMessage = '';
      this.closeButton.nativeElement.click();
      this.studentForm.reset();
      this.loadTableData();
    }, (error: any) => {
      console.log(error);
      this.requiredMessage = '';
      if(error.error.text == 'Email or NIC already exists'){
        this.toastr.error('Error', error.error.text);
        this.requiredMessage = 'Email or NIC already exists!';
      }else {
        this.toastr.error('Error', 'Cannot add student!');
      }
    });
  }



  

  selectImage(index: number,imagesUrl: any): void {
    this.selectedImageIndex = index;
    this.studentForm.patchValue({
      profile_url: imagesUrl
    });
  }

  selectProfile(data: any){
    this.selectedStudent = data.data;
  }
 
}
