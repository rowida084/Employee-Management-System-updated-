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
