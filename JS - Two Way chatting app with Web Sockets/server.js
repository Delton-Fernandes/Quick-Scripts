const WebSocket = require("ws");
const rxjs = require("rxjs");

const wss = new WebSocket.Server({ port: 8080 });

wss.on("connection", function connection(ws) {
    ws.on("message", function incoming(message) {
        console.log("received: %s", message);
    });

    rxjs.Observable.interval(500).map(i => {
        ws.send(JSON.stringify({ data: "test" }));
    });
});