$(document).ready(function () {

    var dv_msg = $("#dv_msg");
    if ("WebSocket" in window) {
        dv_msg.css("color","#65b042");
        dv_msg.html("WebSocket is supported by your Browser!");

        // Let us open a web socket
        var ws = new WebSocket("ws://localhost:9998/echo");

        ws.onopen = function () {
            dv_msg.html("You are now connected !");
            // Web Socket is connected, send data using send()
            ws.send("Message to send");
            dv_msg.html ("Message is sent...");
        };

        ws.onmessage = function (evt) {
            var received_msg = evt.data;
            dv_msg.html("Message is received... : " + received_msg);
        };

        ws.onclose = function () {
            // websocket is closed.
            dv_msg.css("color", "red");
            dv_msg.html("Connection is closed...the server may be down ");
        };
    }

    else {
        // The browser doesn't support WebSocket
        dv_msg.html("WebSocket NOT supported by your Browser!");
    }



});