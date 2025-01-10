import * as WebSocket from "ws"

const ws = new WebSocket("ws://localhost:8080")

ws.on("open", function open() {
  ws.send("something")
})

ws.on("message", function incoming(data: Object) {
  console.log(data)
})