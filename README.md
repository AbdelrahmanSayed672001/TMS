# TMS
A web application built using ASP.NET Core 8, SQL Server, and modern web technologies like HTML, CSS, Bootstrap, JavaScript, JQuery. This web application is built using 
Repository Design Pattern. The project in master branch with its Database script.

The attached ERD represents the database schema for the Task Management System (TMS). It outlines the relationships between various entities within the system, including Users, Teams, Tasks, and more. The key entities are:

Users (Security): Stores details about users, including their usernames, email, and security-related attributes. The Users table has one-to-many relationships with several other tables.

Roles (Security): Contains role definitions such as Admin, Team Lead, and Regular User, used to assign roles to users through the UserRoles table, establishing a many-to-many relationship.

Teams: Represents teams in the system, with a one-to-many relationship with the UserTeams table, which links users to specific teams.

Tasks: Defines the tasks assigned to users, including details like title, description, attachments, due date, status, and priority. Each task is associated with both a User and a Team, forming a one-to-many relationship.

Comments: Stores comments made on tasks, linked to both the Task and the User who created the comment.

UserTeams: Acts as a junction table linking Users and Teams in a many-to-many relationship, allowing users to belong to multiple teams.

TeamLeads: Links team leaders to specific teams through the UserId reference.

RegUsers: Contains additional information specific to regular users, extending the Users entity.

This diagram illustrates the core relationships that allow TMS to manage users, teams, tasks, and roles effectively.
![TMS_ERD](https://github.com/user-attachments/assets/c533c2b1-de34-44d2-8892-2e87a6ef2112)
