# Employee Management System

## Overview

The **Employee Management System** is a **C# Console Application** designed to practice **.NET Collections**, **Object-Oriented Programming (OOP)**, **Generics**, **Delegates**, and **Events**.

It simulates a simple employee management workflow where employees are added to an onboarding queue, processed into active employees, assigned to departments, and managed using different collection types.

---

## Objectives

- Practice using C# Collections.
- Apply Object-Oriented Programming (OOP) concepts.
- Understand and use Generics.
- Understand and use Delegates.
- Understand and use Events.
- Build a console-based management system.
- Perform searching, filtering, and reporting operations.

---

## Technologies

- C#
- .NET Console Application
- Visual Studio

---

## OOP Concepts Used

- Classes
- Objects
- Constructors
- Encapsulation
- Inheritance (`Manager` inherits from `Employee`)
- Interfaces
- Separation of Models and Services

---

## Collections Used

| Collection | Purpose |
|---|---|
| `Queue<Employee>` | Stores employees waiting for onboarding. |
| `Stack<string>` | Maintains employee action history. |
| `List<Employee>` | Stores active employees. |
| `Dictionary<int, Department>` | Stores departments using Department ID as the key. |
| `HashSet<string>` | Stores unique employee skills without duplicates. |

---

## Project Structure

```text
ConsoleApp4
│
├── Models
│   ├── Employee.cs
│   ├── Manager.cs
│   ├── Department.cs
│   ├── Results.cs
│   └── IHasId.cs
│
├── Services
│   └── Company.cs
│
└── Program.cs
## Features

### Employee Management

* Add employee to onboarding queue.
* Process the next employee into the active employees list.
* Search employee by ID.
* Search employee by name.
* Filter employees by different conditions.

### Department Management

* Add new department.
* Display employees belonging to a specific department.

### Manager Management

* Promote an employee to Manager.
* Display all Managers.
* Assign employees to Managers.
* Display Manager team members.

### Reports

* Calculate average employee salary.
* Display the number of employees in each department.
* Print action history.
* Display all unique employee skills.

---

## Main Menu

```text
========== Company Management ==========

1. Add Employee To Onboarding
2. Process Next Employee
3. Add Department
4. Search Employee By ID
5. Search Employee By Name
6. Print Employees By Department
7. Calculate Salary Average
8. Employees Report By Department
9. Print Action History
10. Print Unique Skills
11. Promote To Manager
12. Print All Managers
13. Assign Employee To Manager
14. Print Manager Team
15. Filter Employee
0. Exit
```
## Seed Data

The project starts with predefined data including:

* Multiple Departments
* Multiple Employees
* Employee skills
* Initial onboarding employees
* Initial active employees

The seed data allows immediate testing of the project features without entering all data manually.

---

## Data Flow

```text
Employee
   │
   ▼
Onboarding Queue
   │
   │ Process Employee
   ▼
Active Employees List
   │
   ├── Search
   ├── Filtering
   ├── Reports
   ├── Promotion
   └── Manager Assignment
```

---

## Validation

The system validates:

* Duplicate employee IDs.
* Duplicate department IDs.
* Duplicate department names.
* Invalid user input using `TryParse`.
* Invalid salary values.
* Existing employees before performing operations.
* Existing managers and departments.
* Empty collections.
* Preventing an employee from being assigned to themselves as a manager.
* Preventing an employee from being promoted if they are already a Manager.

---

## Delegates

The project uses a custom `EmployeeFilter` delegate to make employee filtering reusable.

It is used with **Lambda Expressions** to filter employees by different conditions such as:

* Manager ID
* Salary
* Department

This demonstrates how a delegate can pass different filtering behaviors to the same method.

---

## Events

The project uses **Events** to notify other parts of the application about important employee lifecycle actions.

### Events Used

* `EmployeeOnboarded`
* `EmployeePromoted`

The `Company` class acts as the **Publisher**, while other methods or classes can act as **Subscribers**.

Events demonstrate:

* Event declaration.
* Subscription using `+=`.
* Unsubscription using `-=`.
* Raising events only from inside `Company`.

---

## Generics

The project implements a **generic search** using an `IHasId` interface and a **Generic Constraint**.

### IHasId

`Employee`, `Manager`, and `Department` implement the common ID contract.

The generic search uses:

```csharp
T : IHasId
```

This allows the same search logic to work with different types that have an ID.

The search returns:

```csharp
Results<T>
```

instead of creating separate search logic for every supported type.

---

## Results<T>

The project uses a generic `Results<T>` class to return operation results.

It provides:

* Success status
* Message
* Returned data

This makes method results consistent across different operations and data types.

---

## Learning Outcomes

This project demonstrates practical usage of:

* Queue (FIFO)
* Stack (LIFO)
* List
* Dictionary
* HashSet
* Searching
* Filtering
* Reporting
* Lambda Expressions
* Delegates
* Events
* Generics
* Generic Constraints
* Interfaces
* Inheritance
* Console Input Validation

---

## Project Constraints

The project does **not** use:

* LINQ
* Async/Await
* File Handling
* Database
* Entity Framework Core
* ASP.NET Core
* Dependency Injection
* CQRS
* Clean Architecture

---

## Author

**Rowida Hany**
