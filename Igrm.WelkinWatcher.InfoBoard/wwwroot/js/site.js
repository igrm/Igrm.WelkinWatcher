var headerAdded = false;

const connection = new signalR.HubConnectionBuilder()
    .withUrl("ws://localhost:64323/stateVectors", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    })
    .configureLogging(signalR.LogLevel.Debug)
    .build();

connection.start().then(function () {
    console.log("connected");
});

connection.on("ReceiveStateVector", (stateVector) => {
    ReceiveStateVector(stateVector);
});

function ReceiveStateVector(stateVector) {

    if (!headerAdded) {
        var head = $("#template").clone();
        var headCounter = 1;
        $.each(stateVector, function (propertyName, valueOfProperty) {
            var element = head.find("td:nth-child(" + headCounter + ")");
            element.text(propertyName);
            headCounter++;
        });
        $("#vectors").append("<tr id='head'>" + head.html() + "</tr>");
        headerAdded = true;
    }

    var copy = $("#template").clone();
    var counter = 1;

    $.each(stateVector, function (propertyName, valueOfProperty) {
        var element = copy.find("td:nth-child(" + counter + ")");

        if ($("#" + stateVector.icao24 + "-" + propertyName).length > 0 && $("#" + stateVector.icao24 + "-" + propertyName).text() != valueOfProperty) {
            element.addClass("blink_me");
        }
        else {
            $("#" + stateVector.icao24 + "-" + propertyName).removeClass("blink_me");
            element.removeClass("blink_me");
        }

        element.text(valueOfProperty);
        element.attr("id", stateVector.icao24 + "-" + propertyName);
        
        counter++;
    });

    if ($("#" + stateVector.icao24).length === 0) {
        $("#vectors").append("<tr id='" + stateVector.icao24 + "'>" + copy.html() + "</tr>");
    }
    else {
        $("#" + stateVector.icao24).html(copy.html());
    }
}

