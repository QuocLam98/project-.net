var ListUserOnline = [];
const connection = new signalR.HubConnectionBuilder()
    .withUrl(`/AccountSendMessageHub?accountId=${accountId}`)
    .build();

connection.on("SendAccountOnline", function (obj) {
    $(`.card-message .message-avatar`).removeClass("online").addClass("offline");
    $(`.message-item .message-left-avatar`).removeClass("online");
    ListUserOnline = obj;
    for (var i = 0; i < ListUserOnline.length; i++) {
        var item = ListUserOnline[i];
        $(`.card-message[data-id=${item.accountId}] .message-avatar`).removeClass("offline").addClass("online");
        $(`.message-item[data-account=${item.accountId}] .message-left-avatar`).addClass("online");
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return err;
});