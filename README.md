﻿# John Doe Agent Management System

## Assumptions Based on Business Criteria
### ID Numbers
The assumption is that once an ID is assigned to an entity, it will not be changed.  Thus, updates will not accept the updating of the ID number of the associated entity being updated.  Provided ID numbers in such an instance are used to find the entity in question instead.

### Customers by Agent ID
The assumption here is that we would want the **agent's** ID to search by for the customer data in this instance, *not* the **customer's** ID.  Thus, it is inappropriate to do a GET on the **customer** API to retrieve the **agent's** customers.  A different URL route is offered for this instance.

### CRUD Parameters Not Implemented
Deletion of agents was not requested in this exercise.  It was reasonable to assume that a DELETE request should return an error stating it was not implemented.  The same was assumed for obtaining a single **customer** by ID for this exercise.

### Agent Customer Listing
UI is slated to include the customer's name and city/state.  Assumption is this is handled in the UI and not in the API layer.  Full Customer data under the specified Agent is therefore returned.

## Technology Stack Used
This exercise uses C# and WebAPI for the API calls, and I have attempted to make this as RESTful as appropriate.

## Questions for Scrum Master/PM
- Do we need an API method for getting a single Customer's information by ID? (Which was not specifically included in the instructions).
- Should the IDs of Customers and Agents be updatable, or fixed once added to the system?
- Will Agent deletion capability be required in the future?

## Installation of the Demonstration Project

### Assumptions for Installation
- .NET Core 3.0 installed on target Windows machine.
- *Visual Studio 2019* is being used to build the project.
- *IIS* and *IIS Express* are not required for these instructions.

### Instructions
- Build the project in Visual Studio 2019.
- Select **Build** -> **Publish JohnDoeAgentMgmtSystem**
- At the **Pick a publish target** window, choose *Folder*
- Select the **Folder or File Share** to publish the self-contained application to.
    - **Default:** *bin\Release\netcoreapp3.0\publish\\*
    - Note that this default is a subfolder in the project directory.
- Click **Create Profile** and choose a name for the new publish profile.
- Click **Publish**
    - The project will build into the specified folder.
- Run **JohnDoeAgentMgmtSystem.exe** in the specified folder.  The web application will execute and begin listening for requests.
    - When I ran this, it listened on http://localhost:5000 and https://localhost:5001
    - Note that you may get security warnings in web browsers when attempting to go to the *https* address as there is unlikely to be a valid HTTPS certificate for localhost.
- Press Ctrl+C to end the program and shut down the server when you are done.

## URL Structure Once Installed

### Assumptions
The following guide assumes that the deployed project is running on http://localhost:5000.

### Agent List Functions
#### Listing of all Agents: 
GET http://localhost:5000/api/agents

#### Listing of a Single Agent by ID: 
GET http://localhost:5000/api/agents/1313

#### Add Agent: 
POST http://localhost:5000/api/agents

Example body:

    {
        "_id": 1313,
        "name": "John Doe",
        "address": "123 Any Street #200",
        "city": "Seattle",
        "state": "WA",
        "zipCode": "98101",
        "tier": 111,
        "phone": {
            "primary": "555-555-1212",
            "mobile": "555-444-3333"
        }
    }

#### Update Agent: 
PUT http://localhost:5000/api/agents/1313

Example body:

    {
        "_id": 1313,
        "name": "Jane Doe",
        "address": "123 Any Street #200",
        "city": "Seattle",
        "state": "WA",
        "zipCode": "98101",
        "tier": 99,
        "phone": {
            "primary": "555-555-1212",
            "mobile": "555-444-3333"
        }
    }

### Customer List Functions
#### Listing of all Customers: 
GET http://localhost:5000/api/customers

#### Add Customer: 
POST http://localhost:5000/api/customers

Example body:

    {
        "_id": 999999,
        "agent_id": 1313,
        "guid": "fc8c5845-1e23-4c94-b92c-a60d8a3f098e",
        "isActive": false,
        "balance": "$9,234.56",
        "age": 41,
        "eyeColor": "blue",
        "name": {
            "first": "Jean-Luc",
            "last": "Picard"
        },
        "company": "Enterprise-E",
        "email": "picard@example.com",
        "phone": "+1 (999) 888-7777",
        "address": "Picard Winery, Paris, France, FrenchPostalCode",
        "registered": "Tuesday, February 14, 2365 6:14 PM",
        "latitude": "-58.511774",
        "longitude": "-26.099681",
        "tags": [
            "alpha",
            "bravo",
            "charlie",
            "delta",
            "epsilon"
        ]
    }

#### Update Customer: 
PUT http://localhost:5000/api/customers/999999

Example body:

    {
        "_id": 999999,
        "agent_id": 1313,
        "guid": "fc8c5845-1e23-4c94-b92c-a60d8a3f098e",
        "isActive": false,
        "balance": "$19,821.00",
        "age": 55,
        "eyeColor": "brown",
        "name": {
            "first": "William",
            "last": "Riker"
        },
        "company": "Starfleet (Retired)",
        "email": "wriker@example.com",
        "phone": "+1 (111) 222-3333",
        "address": "Riker's Island, Juneau, AK, 99887",
        "registered": "Tuesday, February 14, 2378 6:14 PM",
        "latitude": "-58.511774",
        "longitude": "-26.099681",
        "tags": [
            "enterprise",
            "defiant",
            "reliant",
            "excelsior",
            "titan"
        ]
    }

#### Delete Customer: 
DELETE http://localhost:5000/api/customers/999999

## Example Client-Side Code
A pair of buttons, one for *View Agents* and another for *View Customers* has been provided as an example of how this API can be called from a front end application.
- This example functionality is on the home page of the web page project at http://localhost:5000/ (based on running a deployed build as referenced above in **Intallation of the Demonstration Project**).
- This example does *not* make use of jQuery, but only vanilla JavaScript, to acquire the Agent / Customer listing from the WebAPI depending on which button is clicked.
