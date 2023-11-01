# EMS

Developed a RESTful API for an employee management system with the following endpoints:

1. GET /employees - Retrieves a list of all employees.

2. GET /employees/{id} - Fetches details of a specific employee by their ID.

3. POST /employees - Creates a new employee in the system.

4. PUT /employees/{id} - Updates an existing employee's information by their ID.

5. DELETE /employees/{id} - Deletes an employee from the system by their ID.

The employee resource will contain the following attributes:

Employee Table:
- Id (integer)
- FirstName (string)
- MiddleName (string)
- LastName (string)


1) The API is be implemented using .NET Framework or .NET Core, adhering to the principles of REST architecture, ensuring seamless handling of resources.

2. The API is implement the repository pattern for data access, separating the data access logic from the business logic. This pattern will promote maintainability and flexibility.

3. A relational database will be used to store the employee information. The provided database table and columns above will serve as the model for the employee resource. Any relational database management system (RDBMS) can be chosen, such as MSSQL,  MySQL, PostgreSQL, or SQLite, depending on the project's specific needs.

4. The API is capable of handling errors gracefully, returning appropriate HTTP status codes and meaningful error messages. This will assist clients in understanding and resolving issues encountered during API interactions.