$(document).ready(function () {
    var dvinfo = $("#dv_info");
    var dv_msg = $("#dv_msg");
    var txtSend = $("#txtSend");
    var btnSend = $("#btnsend");
    var toAppend = "<div class='alert alert-info' ><span class='glyphicon glyphicon-warning-sign'></span>&nbsp;<span>@APPEND</span></div>";
    if ("WebSocket" in window) {
        dv_msg.removeClass("alert-danger");
        dv_msg.toggleClass("alert alert-success");
        dv_msg.html("<span class='glyphicon glyphicon-ok' ></span>&nbsp;&nbsp;WebSocket is supported by your Browser!<br><br><span style='font-size:12px'>Connecting...</span>");
        // Let us open a web socket
        var ws = new WebSocket("ws://localhost:9998/echo","echo-protocol");
        
        ws.onopen = function (event) {
            dv_msg.removeClass("alert-danger");
            dv_msg.addClass("alert alert-success");
            dv_msg.html("<span class='glyphicon glyphicon-ok' ></span>&nbsp;&nbsp;You are now connected to server You can send a msg now!");
        };

        ws.onmessage = function (evt) {
            var received_msg = evt.data;
            dvinfo.append(toAppend.replace("@APPEND", "Server : " + received_msg));
           
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
            dv_msg.html("<span class='glyphicon glyphicon-remove' ></span>&nbsp;&nbsp;Connection is lost, server may be down  ! <br/><br/><a style='cursor:pointer;color:#A94442' href='javascript:window.location.reload(true);'><span class='glyphicon glyphicon-refresh'></span>&nbsp;<span style='font-size:12px'>Retry</span></a>");
            
        };
    }

    else {
        // The browser doesn't support WebSocket
        dv_msg.removeClass("alert-success");
        dv_msg.addClass("alert alert-danger");
        dv_msg.html("<span class='glyphicon glyphicon-remove' ></span>&nbsp;&nbsp;WebSocket is NOT supported by your Browser!");
    }

    btnSend.click(function () {
        txtSend.val();
        ws.send(txtSend.val());
        txtSend.val("");
    });
    
});