var headerAdded = false;
var elements = {};
var template, table;

function toColor(num) {
    num >>>= 0;
    var b = num & 0xFF,
        g = (num & 0xFF00) >>> 8,
        r = (num & 0xFF0000) >>> 16,
        a = ((num & 0xFF000000) >>> 24) / 255;
    return "rgba(" + [r, g, b, a].join(",") + ")";
}

const connection = new signalR.HubConnectionBuilder()
    .withUrl("ws://localhost:64323/stateVectors", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    })
    .configureLogging(signalR.LogLevel.Debug)
    .build();

connection.start().then(function () {
   template = document.getElementById("template");
   table = document.getElementById("vectors");
   console.log("connected");
});

connection.on("ReceiveStateVector", (stateVector) => {
    ReceiveStateVector(stateVector);
});

function ReceiveStateVector(stateVector) {

    if (!headerAdded) {
        var header = template.cloneNode(true);
        header.id = "header";
        let counter = 0;
        for (const key of Object.keys(stateVector)) {
            header.childNodes[counter].textContent = key;
            counter++;
        }
        table.append(header);
        headerAdded = true;
    }

    var existingRow = document.getElementById(stateVector.icao24);

    if (!existingRow) {
        let numberOfItems = document.getElementById("numberOfItemsNew");
        numberOfItems.textContent++;

        var row = template.cloneNode(true);
        row.id = stateVector.icao24;
        let counter = 0;
        let columns = row.getElementsByTagName("td");
        for (const key of Object.keys(stateVector)) {
            columns[counter].textContent = stateVector[key];
            counter++;
            if (key === 'baroAltitude') {
                row.style.backgroundColor = toColor(Math.abs(parseInt(stateVector[key]))*-1);
            }
        }

        table.append(row);
    }
    else {
        let numberOfItems = document.getElementById("numberOfItemsUpdates");
        numberOfItems.textContent++;

        let counter = 0;
        let columns = existingRow.getElementsByTagName("td");

        for (const key of Object.keys(stateVector)) {
            let item = columns[counter];
            if (item.textContent != stateVector[key]) {
                item.className = "blink_me";
                item.textContent = stateVector[key];
                setTimeout(() => {
                    item.className = "";
                }, 2000);

                if (key === 'baroAltitude') {
                    existingRow.style.backgroundColor = toColor(Math.abs(parseInt(stateVector[key])) * -1);
                }
            }
            counter++;
        }
    }


}

