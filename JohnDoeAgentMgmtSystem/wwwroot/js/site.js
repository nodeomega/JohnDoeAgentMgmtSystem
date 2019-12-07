// Gets a listing of Agents
function GetAgents() {
    const http = new XMLHttpRequest();
    const url = 'api/agents';
    http.open("GET", url, true);

    http.onreadystatechange = (e) => {
        const output = document.getElementById("output");

        // Only process if the response was not blank or otherwise "falsy".
        if (!!http.responseText) {
            // Parse the resulting JSON.
            const agents = JSON.parse(http.responseText);

            // Clear the output div.
            output.innerText = "";

            // Create the table.
            const table = document.createElement("table");

            table.style.margin = "0 auto";
            output.appendChild(table);

            // Create the table header
            const thead = table.createTHead();
            const headRow = thead.insertRow();
            const data = Object.keys(agents[0]);
            for (let key of data) {
                let th = document.createElement("th");
                let text = document.createTextNode(key);
                th.appendChild(text);
                headRow.appendChild(th);
            }

            // create the table body.
            let tbody = table.createTBody();
            for (let agent of agents) {
                const thisRow = tbody.insertRow();
                let key;
                for (key in agent) {
                    if (agent.hasOwnProperty(key)) {
                        const thisCell = thisRow.insertCell();

                        if (key === "phone") {
                            // if the Agent field is the phone number listing, use special processing to populate the primary and mobile numbers.
                            let innerKey;
                            for (innerKey in agent[key]) {
                                if (agent[key].hasOwnProperty(innerKey)) {
                                    const text = document.createTextNode(`${innerKey}: ${agent[key][innerKey]}`);
                                    thisCell.appendChild(text);
                                    thisCell.appendChild(document.createElement("br"));
                                }
                            }
                        } else {
                            // If not the phone numbers, process the cell normally.
                            const text = document.createTextNode(agent[key]);
                            thisCell.appendChild(text);
                        }
                    }
                }
            }
        }
    }

    http.send();
}

function GetCustomers() {
    // Get the listing of Customers.
    const http = new XMLHttpRequest();
    const url = 'api/customers';
    http.open("GET", url, true);

    http.onreadystatechange = (e) => {
        const output = document.getElementById("output");

        // Only process if the response was not blank or otherwise "falsy".
        if (!!http.responseText) {

            // Parse the resulting JSON.
            const customers = JSON.parse(http.responseText);

            // Clear the results div.
            output.innerText = "";

            // Generate the table for the result.
            const table = document.createElement("table");
            table.style.margin = "0 auto";

            output.appendChild(table);

            // Generate the table header.
            const thead = table.createTHead();
            const headRow = thead.insertRow();
            const data = Object.keys(customers[0]);
            for (let key of data) {
                let th = document.createElement("th");
                let text = document.createTextNode(key);
                th.appendChild(text);
                headRow.appendChild(th);
            }

            // Generate the table body.
            let tbody = table.createTBody();
            for (let customer of customers) {
                const thisRow = tbody.insertRow();
                let key;
                for (key in customer) {
                    if (customer.hasOwnProperty(key)) {
                        const thisCell = thisRow.insertCell();
                        if (key === "name") {
                            // Handle the name object a little differently.
                            let innerKey;
                            for (innerKey in customer[key]) {
                                if (customer[key].hasOwnProperty(innerKey)) {
                                    const text = document.createTextNode(`${customer[key][innerKey]} `);
                                    thisCell.appendChild(text);
                                }
                            }
                        } else {
                            // Handle this cell normally.
                            const text = document.createTextNode(customer[key]);
                            thisCell.appendChild(text);
                        }
                    }
                }

            }
        }
    }

    http.send();
}