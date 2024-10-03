# Private Lesson API Application

## Description

This API application is designed to manage the data of students taking private lessons and the teachers providing those lessons. Families can track the lessons and fees for the lessons taken by individuals, while teachers can register their students using surname and family information and keep track of the fees for the private lessons they provide.
You must create at least one admin and one user account to access the API endpoints. The admin account can manage all the data in the system, while the user account can only access the data related to the user's family.
Project uses JWT token based authentication and authorization for user roles. You can not access the API endpoints without a valid token.

---

## Features

- **Student Tracking:**
    - Record the lesson information of students and the private lesson teachers.
    - Maintain details of the private lessons taken by students (subject, duration, date, and time).

- **Teacher Tracking:**
    - Register students by entering the surname and family information of the private lesson teachers.
    - Record and manage the fees for the lessons provided by teachers.

- **For Families:**
    - Provides a structure for families to track the private lessons taken by individuals and the fees for these lessons.
    - Makes it easy for families to monitor payment transactions and lesson details.

---

## API Structure

### Endpoints

- **/api/account**: Endpoint for managing user accounts. Admin and User roles are adviced to create.
    - Perform GET, POST, PUT, DELETE operations to manage 

- **/api/student**: Endpoint for managing student information.
    - Perform GET, POST, PUT, DELETE operations to manage student data.
    
- **/api/teacher**: Endpoint for managing information of private lesson teachers.
    - Perform GET, POST, PUT, DELETE operations to manage teacher records.

- **/api/lesson**: Endpoint for tracking private lessons.
    - Manage details of lessons between students and teachers (duration, subject, fee, etc.).
    
- **/api/family**: Endpoint for families to monitor the lessons and fees for individuals.
    - Supports more than one family for families and teachers because you may have more than one student taking lessons in a family.

- - **/api/session**: Endpoint for connecting lessons,students and teachers for a specific lesson session taken in any period of time.
    - Track payments and payment dates for lessons.
---

## Technology Stack

- **.NET 8**: The application is built using .NET 8, leveraging the latest features and improvements.
- **C# 12.0**: The codebase utilizes C# 12.0, ensuring modern and efficient coding practices.
- **ASP.NET Core**: The API is developed using ASP.NET Core, providing a robust and scalable framework for building web APIs.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or any other compatible IDE

### Installation

1. Clone the repository:
2. Navigate to the project directory:
3. Open the project in Visual Studio or any other compatible IDE.
4. Build the project to restore the dependencies.
5. Run the application to start the API server.
6. Access the API endpoints using the base URL.
7. Start making requests to the API to manage student, teacher, lesson, and family data.
8. Enjoy the features of the Private Lesson API Application!
9. For more information, refer to the API documentation.
10. For any issues or queries, please contact the development team.
11. Happy coding!
