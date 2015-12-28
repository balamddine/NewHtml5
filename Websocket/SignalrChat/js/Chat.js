var ChatToAppend = " <div id='chat_@ID' class='col-lg-3 col-lg-offset-10 dvchatscontainer fullHeight'>"
                              + "<div class='col-lg-12 chathead nodistance'>"
                              +"<ul class='list-inline' >"
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
                              + "<input type='text'  class='form-control input-sm txtinput' placeholder='Enter Message' id='txt_@ID' />"
                              + "</div>"
                              + "<div class='col-lg-12 tools'></div>"
                              + "</div>"
                              + "</div>";
$(function () {
    var ainfo = $("#ainfo");
    var dvStatus = $("#dvStatus");
    var dvContextId = $("#dvContextId");
    var dvChatContainer = $("#dvChatContainer");
    var UserID = $("#hfUserId").val();
    ainfo.fancybox({ width: "500px" });

    // Declare a proxy to reference the hub. 
    var chat = $.connection.chatHub;

    dvStatus.html("<span class='glyphicon glyphicon-ok' ></span>&nbsp;&nbsp;<span>Connecting...</span>");
    $.connection.hub.start().done(function () {
        dvStatus.html("<span class='glyphicon glyphicon-ok' ></span>&nbsp;&nbsp;Connected!");
        dvContextId.html($.connection.hub.id);
        UpdateUserContextID();

        chat.server.addUser(UserID);
    });
    chat.client.broadcastMessage = function (msg) {
        // dvinfo.append(toAppend.replace("@APPEND", "Server : " + msg));
    }

});

window.UpdateUserContextID = function () {
    var chat = $.connection.chatHub;
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
            dvlastConnected.html(data.d);
        },
        error: function (xhr) {
            alert("responseText: " + xhr.responseText);
        }
    });
};
window.InitChat = function (userID) {
    var dvChatContainer = $("#dvChatContainer");
    var id = $("#chat_" + userID);
    var chatName = "";
    var chatBody = "";
    if (id.length == 0) {
        dvChatContainer.append(ChatToAppend.replace(/@ID/g, userID).replace("@CHAT_BODY", chatBody).replace("@CHAT_HEAD", chatName));
    }
    var txt = $("#txt_" + userID);
    setTimeout(function () {
        txt.focus();
    }, 500);
};

window.CloseChat = function (userID) {
    var id = $("#"+userID);
    var dvChatContainer = $("#dvChatContainer");
    if (id.length > 0) {
        id.remove();
    }
};