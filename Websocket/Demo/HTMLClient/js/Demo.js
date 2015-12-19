$(document).ready(function () {

    var dv_msg = $("#dv_msg");
    if ("WebSocket" in window) {
        dv_msg.removeClass("alert-danger");
        dv_msg.toggleClass("alert alert-success");
        dv_msg.html("<span class='glyphicon glyphicon-ok' ></span>&nbsp;&nbsp;WebSocket is supported by your Browser!<br><br><span style='font-size:12px'>Connecting</span>");

        // Let us open a web socket
        var ws = new WebSocket("ws://localhost:9998/echo");
        
        ws.onopen = function (event) {
            dv_msg.removeClass("alert-danger");
            dv_msg.addClass("alert alert-success");
            dv_msg.html("<span class='glyphicon glyphicon-ok' ></span>&nbsp;&nbsp;You are now connected to server You can send a msg now!");

            // Web Socket is connected, send data using send()
           // ws.send("Hi From client");
            //dv_msg.html ("Message is sent...");
        };

        ws.onmessage = function (evt) {
            var received_msg = evt.data;
            dv_msg.html("Message is received... : " + received_msg);
        };
        // Handle any errors that occur.
        ws.onerror = function (error) {
            dv_msg.removeClass("alert-success");
            dv_msg.addClass("alert alert-danger");
            dv_msg.html("<span class='glyphicon glyphicon-remove' ></span>&nbsp;&nbsp;WebSocket Error: " + error);
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
        dv_msg.addClass("alert alert-danger");
        dv_msg.html("<span class='glyphicon glyphicon-remove' ></span>&nbsp;&nbsp;WebSocket is NOT supported by your Browser!");
    }

    $("#btnsend").click(function () {
        var v = $("#txtSend").val();
        ws.send(v);
        $("#txtSend").val("");
    });

});