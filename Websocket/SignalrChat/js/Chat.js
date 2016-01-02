var ChatToAppend = " <div id='chat_@ID' class='col-lg-3 col-lg-offset-10 dvchatscontainer fullHeight'>"
                              + "<div class='col-lg-12 chathead nodistance'>"
                              + "<ul class='list-inline' >"
                              + "<li class='name'>@CHAT_HEAD</li>"
                              + "<li>"
                              + "<a href=\"javascript:CloseChat('chat_@ID');\" class=\"aclose\"></a>"
                              + "</li>"
                              + "</ul>"
                              + "</div>"
                              + "<div class='col-lg-12 chatbody nodistance'>"
                              +"<ul class='nodistance'>@CHAT_BODY</ul>"
                              +"</div>"
                              + "<div class='col-lg-12 chatfooter nodistance'>"
                              + "<div class='col-lg-12 nodistance'>"
                              + "<input type='text'  class='form-control input-sm txtinput' placeholder='Enter Message' id='txt_@ID' onkeydown='prevententer(event);' onkeyup='SendMessage(event,@ID);' />"
                              + "</div>"
                              + "<div class='col-lg-12 tools'></div>"
                              + "</div>"
                              + "</div>";
var UserID = $("#hfUserId").val();
var messagePageSize = 20;
// Declare a proxy to reference the hub. 
var chat = $.connection.chatHub;
$(function () {
    var ainfo = $("#ainfo");
    var dvStatus = $("#dvStatus");
    var dvContextId = $("#dvContextId");
    var dvChatContainer = $("#dvChatContainer");
    ainfo.fancybox({ width: "500px" });
    dvStatus.html("<span class='glyphicon glyphicon-ok' ></span>&nbsp;&nbsp;<span>Connecting...</span>");

    $.connection.hub.start().done(function () {
        dvStatus.html("<span class='glyphicon glyphicon-ok' ></span>&nbsp;&nbsp;Connected!");
        dvContextId.html($.connection.hub.id);
        UpdateUserContextID();    
    });
});
window.logout = function () {
    chat.server.logout(UserID);
    window.location = "Login.aspx?lgtout=T";
};
window.UpdateUserContextID = function () {
    var hf_contextID = $("#hf_contextID");
    var dvlastConnected = $("#dvlastConnected");
    hf_contextID.val($.connection.hub.id);

    $.ajax({
        type: 'POST',
        url: 'Chat.aspx/UpdateUserInfo',
        data: '{ContextID:"' + $.connection.hub.id + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            chat.server.addUser(UserID);
            dvlastConnected.html(data.d);
        },
        error: function (xhr) {
            alert("responseText: " + xhr.responseText);
        }
    });
};
window.InitChat = function (userID) {
    var id = $("#chat_" + userID);
    if (id.length == 0) {
        GetchatBody(userID);
    }
       
    setTimeout(function () {
        var txt = $("#txt_" + userID);
        txt.focus();

    }, 50);
    
};
window.prevententer = function (event) {
    var x = event.which || event.keyCode;
    if (x == 13) {
        event.preventDefault();
        return false;
    }
};
window.GetchatBody = function (ToId) {
    var dvChatContainer = $("#dvChatContainer");

    $.ajax({
        type: 'POST',
        url: 'Chat.aspx/GetchatBody',
        data: '{ToId:' + ToId + ',pageSize:' + messagePageSize + '}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            var chatName = "";
            dvChatContainer.append(ChatToAppend.replace(/@ID/g, ToId).replace("@CHAT_BODY", data.d).replace("@CHAT_HEAD", chatName));
            var txt = $("#txt_" + ToId);
            txt.focus();
            scrollbottom($("#chat_" + ToId).find(".chatbody"));
        },
        error: function (xhr) {
            alert("responseText: " + xhr.responseText);
        }
    });

};
window.SendMessage = function (event, ToID) {

    var txt = $("#txt_" + ToID);
    var msg = txt.val();
    //console.log(txt.val());
    var x = event.which || event.keyCode;
    var FromUserID = UserID
    if (x == 13) {
        if (msg != "") {
            chat.server.sendMessage(FromUserID, ToID, msg);
            txt.val("");
        }
        event.preventDefault();
        return false;

    }

};
chat.client.ClientMessageReceived = function (message, FromID, FromName, ToID, ToName) {
    var div ;
    if (FromID == UserID) {
        div= $("#chat_" + ToID).find(".chatbody")
        div.find("ul").append("<li>You: " + message + "</li>");
    }
    else {
        InitChat(FromID);
        div = $("#chat_" + FromID).find(".chatbody");
        div.find("ul").append("<li><span style='font-weight:bold'>" + FromName + "</span>: " + message + "</li>");
    }
    scrollbottom(div);
};
window.scrollbottom = function (ID) {
    setTimeout(function () {
        ID.animate({ scrollTop: ID.prop("scrollHeight") }, 500);
    },50);
    
};
window.CloseChat = function (userID) {
    var id = $("#"+userID);
    var dvChatContainer = $("#dvChatContainer");
    if (id.length > 0) {
        id.remove();
    }
};