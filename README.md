# eHospital.PatientWebApi
REDESIGNED ACCORDING TO AGREED CONVENTIONS!
This is a part of larger project of web-interface and digital data storage for hospital.
e-Hospital-Patient-WebApi implements back-end for Patient-part.

It contains controllers, business logic and tests for handling Http requests:
- GET:  ../api/patient/ == Delivers list of all Patients (sorted in alphabet order descending);
- GET:  ../api/patient/recent == Delivers list of recent Patients (sorted according to last appointments);
- GET:  ../api/patient/{%%%} == Delivers list of Patients whose Surnames or Names contains the text entered in the search field
- GET:  ../api/image/ == Delivers list of all patient's images available in the database;
- GET:  ../api/image/{id} == Selects image by Id;
- POST: ../api/patient/+{Object in body} == Adds new Patient to the database if data provided is valid;
- GET:  ../api/patient/{id} == Delivers Patient with requested id - all fields, detailed info;
- PUT:  ../api/patient/{id}+{Object in body} == Updates existing Patient with the data entered by User;
 -DELETE:  ../api/patient/{id} == Disables a specific Patient in the database (soft delete).

User stories to be matched:

As a doctor/nurse/receptionist I want to:
- See List of all Patients (sorted in alphabet order descending) on the top right;
- See List of recent Patients (sorted according to last appointments. Limit to last 5) on the bottom right;
- Be able to search the Patient entring part of name or surname in the search field.

As a nurse/receptionist I want to be able to create new Patient:
- Choose Image by getting the list of all available images;
- Create Patient, making sure that all required fields are filled in with valid data.

As a nurse/receptionist I want to:
- See patient details on Basic/Details window;
- Be able to update Patient personal data and image, making sure that all required fields are filled in and data is valid;
- Be able to remove this Patient from database.

The project implements the following features:
- Swagger;
- Automapper (in both: business logic and unit tests);
- 3-Tier Layered architecture as in the example:
https://www.c-sharpcorner.com/UploadFile/4d9083/create-and-implement-3-tier-architecture-in-Asp-Net/ 

I found this architecture and class/folders structure the most logical and straitforward enough even for stupid me.
Below is explanation of the projects/folder structure.
The solution consists of the following projects:

PatientApi - contains Controllers and startup files. It is a start-up project. 
It also contains Automapper configuration class.

PatientDA - DataAccess Layer - contains IDataProvider interface and DataContext class implementing IDataProvider.

PatientBO - Business Objects - contains Models and Views. 
The models are Image, Patient, Appoinment. 
ViewModel is one only - PatientView.

PatientBL - Business Logic Layer - contains Services. Actually, it is one service only - PatientService.

With this structure, we also avoid circular referencing problem that I experienced in past.
Data Access Layer refers Business Objects.
Business Lofic referes Data Access.
PatientAPI refers Business Logic.
Tests refer everything but that is not a problem.

I would appreciate your feedback. There are several most important points that I need advise with:
1. How to make controller to return meaningful responses. Especiall when object not found or model is not valid or so. 
And shell we, actually. Because, it is not yet clear how we deliver such response data in the front-end.
2. Better way to wirte Update logic. I remember, Anton posted a link about updating, but I never checked it up so far.
