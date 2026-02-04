# 📒 Agenda – Blazor CRUD Application

Agenda is a **CRUD web application** built with **Blazor Server**, **C#**, and **SQL Server**, designed to manage a list of contacts.  
The application allows users to add, view, edit, search, classify, and delete contacts using a clean and interactive interface.

This project was developed as a learning project focused on **web development**, **database interaction**, and **Blazor component architecture**.

---

## 🚀 Features

- Add new contacts using a modal-based form
- View contacts in a dynamic table (QuickGrid)
- Edit contacts directly from the main page using a pop-up modal
- Delete contacts
- Search contacts by name or surname
- Validate input data:
  - Name is required and accepts only letters
  - Phone number must be unique (validated against the database)
- Classify contacts by relationship type (family, friends, colleagues, etc.)
- Separate pages for contact categories (Family / Friends)

---

## 🛠️ Technologies Used

- **C#**
- **Blazor Server**
- **.NET**
- **SQL Server**
- **ADO.NET**
- **HTML / CSS**
- **Bootstrap**
- **QuickGrid**

---

## 🗄️ Database & Data Access

The application uses **SQL Server** as the database.  
All database operations are implemented using **ADO.NET**, without Entity Framework.

### Architectural decision:
During development, **Entity Framework was replaced with ADO.NET** in order to better understand low-level database operations and gain more control over SQL queries.

As part of this change:
- The `Migrations` folder was removed
- `AgendaContext.cs` and all `DbContext`-related code were deleted
- Entity Framework references were removed from Blazor components
- A dedicated data access class, `DatabaseHelper.cs`, was created to handle all CRUD operations
- The database service was registered in `Program.cs`
- Blazor pages (`Contacte`, `Delete`, `Details`, `Edit`) were updated to use ADO.NET methods

---

## 🧾 Contact Management & Validation

- The contact creation form is displayed only when the user clicks the **Add** button
- A **modal pop-up** is used for adding new contacts
- Input validation is implemented using **DataAnnotations**
- Phone number uniqueness is checked before inserting a new record into the database
- Errors are displayed directly in the UI when validation fails

---

## 👨‍👩‍👧 Contact Classification

- Added a **Relationship** field to each contact
- Implemented a dropdown selection for relationship type
- Contacts are automatically classified into categories such as **Family** or **Friends**
- Categories are accessible as separate pages from the left-side navigation menu
- Created a custom Blazor component (`Contacte.razor`) to handle classification and filtering logic

---

## 🔄 UI Refactoring

Initially, editing contacts was implemented using a separate `Edit.razor` page.  
Due to issues with updating records correctly, the edit functionality was refactored and integrated into the main page using a **modal-based edit form**.

This change improved both:
- Data update reliability
- User experience

---

## 🧠 What I Learned

- Building CRUD applications with **Blazor Server**
- Using **ADO.NET** for database access and validation
- Managing Blazor component state and modal dialogs
- Refactoring UI components to solve real application issues
- Designing simple but structured web applications

---

## ▶️ How to Run the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/CiutacuLigia/Agenda
2. Open the solution in Visual Studio
3. Configure the SQL Server connection string
4. Run the application
