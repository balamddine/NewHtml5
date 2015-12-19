$(document).ready(function () {

    var dv_msg = $("#dv_msg");
    if ("WebSocket" in window) {
        dv_msg.removeClass("alert-danger");
        dv_msg.toggleClass("alert alert-success");
        dv_msg.html("<span class='glyphicon glyphicon-ok' ></span>&nbsp;&nbsp;WebSocket is supported by your Browser!");

        // Let us open a web socket
        var ws = new WebSocket("ws://localhost:9998/echo");

        ws.onopen = function () {
            dv_msg.removeClass("alert-danger");
            dv_msg.toggleClass("alert alert-success");
            dv_msg.html("<span class='glyphicon glyphicon-ok' ></span>&nbsp;&nbsp;You are now connected to the server!");
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
            dv_msg.removeClass("alert-success");
            dv_msg.addClass("alert alert-danger");
            dv_msg.html("<span class='glyphicon glyphicon-remove' ></span>&nbsp;&nbsp;Connection is closed...the server may be down ! please try again later");
            
        };
    }

    else {
        // The browser doesn't support WebSocket
        dv_msg.removeClass("alert-success");
        dv_msg.toggleClass("alert alert-danger");
        dv_msg.html("<span class='glyphicon glyphicon-remove' ></span>&nbsp;&nbsp;WebSocket is NOT supported by your Browser!");
    }



});