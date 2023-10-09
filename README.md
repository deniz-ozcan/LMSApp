<p align="center">
    <img src="netcore.svg" alt="App Logo" width="128px" height="128px" />
</p>
<h1 align="center">Learning Management System</h1>

<!-- TABLE OF CONTENTS -->
<h2 id="table-of-contents">:book: Table of Contents</h2>
<details open="open">
    <summary>Table of Contents</summary>
    <ol>
        <li><a href="#about-the-project"> ➤ About The Project</a></li>
        <li><a href="#overview"> ➤ Overview</a></li>
        <li><a href="#howtoinstall"> ➤ How to install</a></li>
        <li><a href="#project-files-description"> ➤ Project Folders Description</a></li>
        <li><a href="#Credits"> ➤ Credits</a></li>
    </ol>
</details>

![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)

<!-- ABOUT THE PROJECT -->
<h2 id="about-the-project">:pencil: About The Project</h2>

<p align="justify">
    This is a Learning Management System (LMS) project. Users can enroll and learn as a `student` or create a course and upload content as a `instructor`
</p>

<ul>
    <li>
        In order to use the roles, 3 panels (admin, student, instructor) is designed in the interfaces.
    </li>
    <li>
        All required information is stored in a relational database (Sqlite).
    </li>
    <li>The database is designed with migrations.</li>
    <li>All performed operations can be viewed via in website.</li>
    <li>
        You can register, login, log out as both student, instructor and admin and use admin panel to update, delete or add to courses or change role of the users.
    </li>
</ul>

![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)

<!-- OVERVIEW -->
<h2 id="overview">:cloud: Overview</h2>

<p align="justify">
    The project idea based on <a href="https://www.udemy.com/">Udemy</a> and <a href="https://www.coursera.org/">Coursera</a>. You can enroll to courses and learn or create your own courses.
</p>

![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)
<h2 id="howtoinstall">⛓️ How to install</h2>

<p align="justify">
    There is one easy way to deal with it:
<ol>
    <li>Build the project with ide or cmd</li>
    <ul>
        <li> Open the location where all the documents are located in visual studio code, visual studio, cmd or terminal.</li>
        <li> dotnet run --project lmsapp.webui </li>
        <li> Run this line of code in command line of ide or Cmd or terminal.</li>
        <li> You can see the website on localhost in your browser</li>
    </ul>
</ol>
</p>

![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)
<!-- PROJECT FILES DESCRIPTION -->
<h2 id="project-files-description">📝 Project Folders Description</h2>

<ul>
    <li><b>lmsapp.webui:</b>This layer includes functionalities related to the web user interface (UI). It typically handles tasks such as creating web pages, managing user login and logout, and processing user interactions. This layer receives requests from web browsers, processes them, and sends the results back to the user. User interface code is used to create web pages using technologies like HTML, CSS, and JavaScript.</li>
    <li><b>lmsapp.entity:</b>This layer includes entity classes that represent database tables and data models. The data model defines how the data is structured and how it relates to each other. This layer uses Object-Relational Mapping (ORM) tools to interact with the database and perform database operations.</li>
    <li><b>lmsapp.data:</b>This layer contains data access code used to perform database operations. It includes tasks such as establishing a connection to the database, adding, updating, and deleting data. Database communication is typically done using ORM tools (such as Entity Framework or Dapper).In this case Entity Framework.</li>
    <li><b>lmspapp.business:</b>This layer encompasses the business logic and rules of the application. It houses the code responsible for executing the core functions of the application. It handles user requests, coordinates database operations, and manages business processes. This layer ensures that the application functions according to the business requirements and rules.</li>
</ul>
<p>The mentioned layers are typically used in a layered architecture model. This model enables better scalability, maintainability, and testability of the application. Each layer has distinct responsibilities, which helps in organizing and managing the code effectively.</p>
