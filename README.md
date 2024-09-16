# Attendance System

This is a web-based attendance management system built using ASP.NET Core MVC. The system allows for student management, attendance tracking, and generation of student identity cards as PDFs.

## Features

- **Student Management:** Register and manage student information including their images, departments, intakes, and programs.
- **Attendance Tracking:** Mark attendance and generate attendance reports.
- **Excel Import:** Easily import student data from Excel files.
- **PDF Generation:** Generate a student card as a PDF, including the student's photo, name, program, intake, and department.
- **User Authentication and Authorization:** Secure login using ASP.NET Core Identity with role-based access control.

## Technologies Used

- **Frontend:** HTML, CSS, JavaScript, jQuery
- **Backend:** ASP.NET Core MVC, Entity Framework Core
- **Database:** SQL Server
- **PDF Generation:** DinkToPdf
- **Excel Processing:** EPPlus

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/abdulrahman-nabil99/AttendanceSystem
   cd AttendanceSystem
   ```

2. Install the required .NET packages:

   ```bash
   dotnet restore
   ```

3. Set up the database:

   - Update the connection string in `appsettings.json` to match your SQL Server configuration.
   - Run the migrations to create the database schema:

   ```bash
   dotnet ef database update
   ```

4. Run the application:

   ```bash
   dotnet run
   ```

5. Open the application in your browser:

   ```
   http://localhost:5000
   ```

6. Default Admin User:

   ```
   email : admin@admin.com
   password : Pass1234
   ```

## Usage

### Adding Students From pdf

1. Go to the **/Manage/Students/ImportStudents** page.
2. Upload an Excel file to batch import students.

### Generating Student Cards

1. Go to the **/Mycard/{StudentId}**.
2. Download the generated PDF student card.

## Project Structure

```plaintext
ITIAttendanceSystem/
│
├── Controllers/
│   ├── AccountController.cs
│   ├── HomeController.cs
│   ├── ManageController.cs
│   └── ...
│
├── Models/
│   ├── User.cs
│   ├── Department.cs
│   ├── Program.cs
│   ├── Intake.cs
│   ├── AttendanceRecord.cs
│   └── ...
│
├── Services/
│   ├── IUserService.cs
│   ├── UserService.cs
│   ├── IGenericService.cs
│   ├── GenericService.cs
│   ├── IAdminService.cs
│   ├── AdminService.cs
│   └── ...
│
├── Views/
│   ├── Account/
│   ├── Home/
│   ├── Manage/
│   └── ...
│
├── wwwroot/
│   ├── imgs/
│   ├── css/
│   ├── js/
│   └── ...
│
├── appsettings.json
└── Program.cs
```

## Contributing

Contributions are welcome! Please fork this repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

This README provides a comprehensive overview of the project, how to get started, and key features.
