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
    console.log(stateVector);
}

