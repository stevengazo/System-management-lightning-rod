### Welcome

## Lightning Rod Management App

Thanks for comming  to my respository and project ❤

This app is focus to use in the management the maintanances, warranties and incidents of lightning rod and their installations. The project is development by [Steven Gazo](https://www.linkedin.com/in/stevengazo/) just like a sample and exercise of programming in C# and proof his skills like a Full Stack Developer.



The technologies used in this project are:

- .NetCore 5 (project written in c#)
- Blazor: Server App. Proof of SPA page (Single Page Application)
- EntityFrameWork: ORM used to create the database (Code First) and manage the data between the app and the DB.
- Microsoft Identity: Used to administer the access and authorization in the app, using accounts and roles.
- SQL Server: DB Engine to create the database. Also i use Stored Procedures, to have better performance.


## Objetives of the Project

The original purpose of this project, is proof my skills and abilities like Developer. Also i evaluate a one problem in a companies focus in the installation and maintenances of lightning rod. This companies need a smart (basic) control of the lightning rod installed, and the status of this devices. Why is this so necesary? Because this systems are focus on the segurity of things like buildings, structures and more, but also this system provided segurity at the people. 

For that reason, i built this software to manage the maintenances and the installations of this devices, also the maintenances and the criterios established by international regulations. This software if focus on a specific type of lightning rod, de-ionizing lightning rod.



### Description of the project
I going to descript the structure of the application and the general use:

#### Basic Information
This part of the software is the basic information required to registered a new Device in the database. This is the first information to registered in the system, to have a good experience. All this information is used to create graphs, and filters and others things. All this entities haved a CRUD to manage the data.

##### Customers 
Customer information. Only need the name of de customer. The system create a drop down list with the clients.

##### Salemans
The salemans are the responsable every year to do the maintenance of the device.

##### Models
Model of the device. Exists more thant one model of the device. The lightning rod model, is used to determine the radius of protection of the device..

##### Countries
List of countries used in the database. This information specified the country where is installed the device. This information is used to see how many devices are per country.
##### Type of device 
List of types of device, for example "device of sell", "device of rental", "device of proof", etc This entity specifite the type of device. This information is important for the salemans, to reallized the maintanance.


### Device information
This entity have an CRUD (Create, Read, Update and Delete) methods to manage the devices. To continuation, i present imagenes of the interface

##### Creation of Device
This interface present the basic inputs, to registered a new device. This interface register the follow information:

- Alias of the device
- Ubication (longitude and latitude)
- Serial Number
- Date of installation
- Type of device
- Model
- Customer who sell the device
- Salemans responsable of the device

###### Image Sample

![Image](https://user-images.githubusercontent.com/43178863/146663219-f5e0f8a2-8019-42df-a01a-066dcf72c428.png)

##### Update of Device
This interface allow update the information of the device

![Image](https://image.shutterstock.com/image-vector/sample-stamp-grunge-texture-vector-260nw-1389188336.jpg)

##### Delete of Device
This interface allow delete the device only if this device doesn't have warranties, maintenances, incidents or replacements registered

![Image](https://image.shutterstock.com/image-vector/sample-stamp-grunge-texture-vector-260nw-1389188336.jpg)
##### Search of Devices
This interface allow search devices in the database and return the devices by a specific parameters (id, alias,country and year of installation)

![Image](https://image.shutterstock.com/image-vector/sample-stamp-grunge-texture-vector-260nw-1389188336.jpg)

### Maintenance information
Before register a new maintenance, is required create a new Technicians (is used to linked the maintenance with the technician responsable)


This information is important to registered the maintenance, recommendations and status of the device. This is used to do new maintenances by the technicians

![Image](https://image.shutterstock.com/image-vector/sample-stamp-grunge-texture-vector-260nw-1389188336.jpg)

#### Create Maintenance
This interface present the basic inputs, to registered a new maintenance. This 

![Image](https://image.shutterstock.com/image-vector/sample-stamp-grunge-texture-vector-260nw-1389188336.jpg)
![Actualización de dispositivo](https://user-images.githubusercontent.com/43178863/146663216-1529e965-bbfb-45d2-bac4-2f690b6c7300.png)
![Eliminación de dispositivo](https://user-images.githubusercontent.com/43178863/146663217-bac80ec9-5f6e-4c17-ae81-2adf48b66068.png)
![Busqueda de dispositivo](https://user-images.githubusercontent.com/43178863/146663218-1ac76c31-7ad7-4281-9922-46e5f6fb4bbc.png)
![Creación de dispositivos](https://user-images.githubusercontent.com/43178863/146663219-f5e0f8a2-8019-42df-a01a-066dcf72c428.png)
