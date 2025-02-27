"use strict";
let receiverAccountIdFocus = 0;
var dataCreatedTimeEndUser = [];
var checkDateEndUser = [];
var listConnection = [];
let lastScrollToplistMessage = 0;
let receiverRoomNameFocus = "";
var pageIndexMessage = 1;
var pageSizeMessage = 10;

function ConnectSignalr(accountConnectId) {
    var flagCheckConnect = false;
    if (listConnection.length > 0) {
        for (var i = 0; i < listConnection.length; i++) {
            var item = listConnection[i];
            if (item.connection.baseUrl.includes(accountId)) {
                flagCheckConnect = false;
                break;
            }
            else {
                flagCheckConnect = true;
            }
        }
    }
    else {
        flagCheckConnect = true;
    }
    if (flagCheckConnect) {
        var connectionMessage = new signalR.HubConnectionBuilder().withUrl("/AccountSendMessageHub?roomId=" + accountId).build();

        connectionMessage.on("ReceiveMessage", async function (obj) {
            if (obj.receiverId == accountId) {
                $(`.message-body-container[data-id=${obj.accountId}]`).prepend(`
        <div class="message-body-inner-item">
                                <div class="message-body-text">
                                    <div class="message-body-inner message-body-text-inner">
                                        <span class="message-text">
                                            ${obj.text}
                                        </span>
                                        <span class="message-date">
                                           ${moment(obj.createdTime).format('HH:mm')}
                                        </span>
                                    </div>
                                </div>
                            </div>`);
            }
            scrollSendMessgage();
            await Replace(obj);
            await LoadUnread();
        });

        connectionMessage.start().then(function () {
            document.getElementById("text-send-message").disabled = false;
        }).catch(function (err) {
            return console.error(err.toString());
        });
        listConnection.push(connectionMessage);
    }

}
async function Replace(obj) {
    var result = await httpService.getAsync(`message/api/ListContact?pageIndex=${pageIndexConversation}&pageSize=${pageSizeConversation}`);
    var item = result.data.find(c => c.id == obj.id);
    var element = $(`.pills-message-tab-container .message-item[data-account=${item.receiverId}]`);
    if (element != undefined) {
        element.remove();
        $(".pills-message-tab-container").prepend(` <div class="message-item" data-account='${item.receiverId}' data-accountname='${item.accountName}' data-photo='${item.accountPhoto}'>
                                    <div class="message-left">
                                        <div class="message-left-avatar">
                                            <img src="${item.accountPhoto}">
                                        </div>
                                    </div>
                                    <div class="message-center">
                                        <div class="message-center-account">
                                            ${item.accountName}
                                        </div>
                                        <div class="message-center-message ellipsis1">
                                            ${item.text}
                                        </div>
                                    </div>
                                    <div class="message-right">
                                        <div class="message-right-count-message">
                                            ${item.countTotalUnread}
                                        </div>
                                        <div class="message-right-date">
                                            ${moment(item.createdTime).format('HH:mm')}
                                        </div>
                                    </div>
                                </div>`);
    }
    else {
        $(".pills-message-tab-container").prepend(` <div class="message-item" data-account='${item.receiverId}' data-accountname='${item.accountName}' data-photo='${item.accountPhoto}'>
                                    <div class="message-left">
                                        <div class="message-left-avatar">
                                            <img src="${item.accountPhoto}">
                                        </div>
                                    </div>
                                    <div class="message-center">
                                        <div class="message-center-account">
                                            ${item.accountName}
                                        </div>
                                        <div class="message-center-message ellipsis1">
                                            ${item.text}
                                        </div>
                                    </div>
                                    <div class="message-right">
                                        <div class="message-right-count-message">
                                            ${item.countTotalUnread}
                                        </div>
                                        <div class="message-right-date">
                                            ${moment(item.createdTime).format('HH:mm')}
                                        </div>
                                    </div>
                                </div>`);
    }
    $(`.message-item .message-left-avatar`).removeClass("online");
    for (var i = 0; i < ListUserOnline.length; i++) {
        var item = ListUserOnline[i];
        $(`.message-item[data-account=${item.accountId}] .message-left-avatar`).addClass("online");
    }
}
//JS template
$("#pills-contact-tab").click(function () {
    LoadContact();
});
$("#pills-message-tab").click(function () {
    LoadContactMessage();
});
$("#pills-message, #pills-contact").on('click', '.message-item', async function () {
    pageIndexMessage = 1;
    checkDateEndUser = [];
    var lastScrollTopListConversation = 0;
    var lastScrollContact = 0;
    $(".message-body").html(`<div class="message-body-container"></div>`);
    $('.message-body-container').on('scroll', async function (e) {
        var elementHeight = $(this)[0].scrollHeight - 1;
        var currentPosition = $(this)[0].clientHeight - $(this)[0].scrollTop;
        console.log(elementHeight - currentPosition);
        if (elementHeight - currentPosition < 0) {
            pageIndexMessage += 1;
            await LoadMessage(receiverAccountIdFocus, receiverRoomNameFocus);
        }
    });

    $(".pills-contact-tab-contact").removeClass("hd-scroll").addClass(`hd-scroll`);
    $(".pills-contact-tab-contact.hd-scroll").on('scroll', async function () {
        var element = $(this)[0];
        if (element.scrollTop < lastScrollContact) {
            // upscroll
            return;
        }
        lastScrollContact = element.scrollTop <= 0 ? 0 : element.scrollTop;
        if (element.scrollTop + element.clientHeight >= element.scrollHeight) {
            pageIndexContact += 1;
            await LoadMoreContact();
        }
    });
    $(".pills-message-tab-container").removeClass("hd-scroll").addClass(`hd-scroll`);
    $(".pills-message-tab-container.hd-scroll").on('scroll', async function () {
        var element = $(this)[0];
        if (element.scrollTop < lastScrollTopListConversation) {
            // upscroll
            return;
        }
        lastScrollTopListConversation = element.scrollTop <= 0 ? 0 : element.scrollTop;
        if (element.scrollTop + element.clientHeight >= element.scrollHeight) {
            pageIndexConversation += 1;
            await LoadMoreContactMessage();
        }
    });
    if ($("body").width() <= 991) {
        $(".contact-section").css("display", "none");
        $(".message-section").css("display", "block");
    }
    var receiverId = $(this).data("account");
    var roomName = $(this).data("room");
    $(".message-body-container").attr('data-id', receiverId);
    $(".message-body-container").attr('data-room', roomName);
    var accountName = $(this).data("accountname");
    var accountPhoto = $(this).data("photo");
    $(".message-avatar img").attr('src', accountPhoto);

    $(".message-user-name").text(accountName);
    receiverAccountIdFocus = receiverId;
    receiverRoomNameFocus = roomName;
    $(".card-message").attr("data-id", receiverAccountIdFocus);
    ConnectSignalr(receiverId);
    await ReadedMessage(receiverId, roomName);
    await LoadMessage(receiverId, roomName);
    $(`.card-message[data-id=${receiverAccountIdFocus}] .message-avatar`).removeClass("online").addClass("offline");
    for (var i = 0; i < ListUserOnline.length; i++) {
        var item = ListUserOnline[i];
        $(`.card-message[data-id=${item.accountId}] .message-avatar`).removeClass("offline").addClass("online");
    }
});

$(".chat-hide").click(function () {
    if ($("body").width() <= 991) {
        $(".contact-section").css("display", "block");
        $(".message-section").css("display", "none");
    }
});
$(window).resize(function () {
    if ($("body").width() <= 991) {
        $(".contact-section").css("display", "none");
        $(".message-section").css("display", "block");
    }
    else {
        $(".contact-section").css("display", "block");
        $(".message-section").css("display", "block");
    }
});
//JS template end

//Send message logic
$("#btnSendMessage").click(function () {
    SendMessage();
});

async function SendMessage() {
    var obj = {
        "accountId": accountId,
        "receiverId": receiverAccountIdFocus,
        "messageStatusId": 1000001,
        "messageTypeId": 1000001,
        "text": $("#text-send-message").val()
    }
    if ($("#text-send-message").val() == '') {
        return;
    }
    else if ($("#text-send-message").val().trim() == '') {
        return;
    }
    var result = await httpService.postAsync("message/api/SendMessage", obj);
    if (result.status == '201') {
        $("#text-send-message").val('');
        $(".message-body-container").prepend(`
        <div class="message-body-inner-item">
                                <div class="message-body-text right">
                                    <div class="message-body-inner message-body-text-inner">
                                        <span class="message-text">
                                            ${obj.text}
                                        </span>
                                        <span class="message-date">
                                            ${moment(obj.createdTime).format('HH:mm')}
                                        </span>
                                    </div>
                                </div>
                            </div>`);
        $(`.message-item[data-account=${receiverAccountIdFocus}] .message-center-message`).text(obj.text);
        $(`.message-item[data-account=${receiverAccountIdFocus}] .message-right-date`).text(moment(obj.createdTime).format('HH:mm'));
        await Replace(result.data[0]);
    }
    scrollSendMessgage();
}

$("#text-send-message").on("keypress", function (event) {
    if (event.which === 13) {
        SendMessage();
    }
});

async function LoadMessage(receiverId, roomName) {
    dataCreatedTimeEndUser = [];
    var result = await httpService.getAsync(`message/api/LoadMessage?accountId=${receiverId}&pageIndex=${pageIndexMessage}&pageSize=${pageSizeMessage}&roomName=${roomName}`);
    if (result.status == '200') {
        result.data.forEach(function (item) {
            dataCreatedTimeEndUser.push({ date: moment(item.createdTime).format("DD/MM/YYYY"), message: item.text, accountId: item.accountId, time: moment(item.createdTime).format("HH:mm") });

        });
        GroupMessage(dataCreatedTimeEndUser);
    }
    await LoadUnread();
}
function GroupMessage(dataCreatedTimeEndUser) {
    var dataArray = groupBy(dataCreatedTimeEndUser, "date");
    var arrayFromObjectList = Object.values(dataArray);
    arrayFromObjectList.forEach(function (item, index) {
        var dateGroup = "";
        item.forEach(function (itemData) {
            var data = itemData.item;
            var dateGroupString = "";
            if (dateGroup == "") {
                if (!checkDateEndUser.some(x => x.date == data.date)) {
                    dateGroupString = `<div class='group' data-date='` + data.date + `'><div class="group-date-body"><p>` + data.date + `</p></div></div>`;
                    checkDateEndUser.push({ date: data.date });
                }
            }
            else if (dateGroup != data.date) {
                dateGroupString = `<div class='group' data-date='` + data.date + `'><p>` + data.date + `</p></div>`;
                checkDateEndUser.push({ date: data.date });
            }
            if (data.accountId != accountId) {
                var newRow = `
                                <div class="message-body-inner-item">
                                    <div class="message-body-text">
                                        <div class="message-body-inner message-body-text-inner">
                                            <span class="message-text">
                                                ${data.message}
                                            </span>
                                            <span class="message-date">
                                                ${data.time}
                                            </span>
                                        </div>
                                    </div>
                                </div>
                `;
                $(".message-body-container").append(dateGroupString);
                $(`.group[data-date='${data.date}']`).prepend(newRow);
            }
            else {
                var newRow = `
                               <div class="message-body-inner-item">
                                    <div class="message-body-text right">
                                        <div class="message-body-inner message-body-text-inner">
                                            <span class="message-text">
                                                ${data.message}
                                            </span>
                                            <span class="message-date">
                                                ${data.time}
                                            </span>
                                        </div>
                                    </div>
                                </div>
                `;
                $(".message-body-container").append(dateGroupString);
                $(`.group[data-date='${data.date}']`).prepend(newRow);
            }
            dateGroup = data.date;
        });
    });
}

var pageIndexConversation = 1;
var pageSizeConversation = 10;
async function LoadContactMessage() {
    pageIndexConversation = 1;
    var result = await httpService.getAsync(`message/api/ListContact?pageIndex=${pageIndexConversation}&pageSize=${pageSizeConversation}`);
    if (result.status == '200') {
        if (result.data.length == 0) {
            //$("#pills-contact-tab").click();
            $(".pills-message-tab-container").html(`<div class="d-flex w-100 h-100 align-items-center justify-content-center ">
                                <div class="no-message">
                                    Chọn một người để bắt đầu nhắn tin
                                </div>
                            </div>`);
        }
        else {
            $(".pills-message-tab-container").html('');
            result.data.forEach(function (item) {
                $(".pills-message-tab-container").append(`
                <div class="message-item" data-account='${item.receiverId}' data-accountname='${item.accountName}' data-photo='${item.accountPhoto}' data-room="${item.roomName}">
                                    <div class="message-left">
                                        <div class="message-left-avatar">
                                            <img src="${item.accountPhoto}">
                                        </div>
                                    </div>
                                    <div class="message-center">
                                        <div class="message-center-account">
                                            ${item.accountName}
                                        </div>
                                        <div class="message-center-message ellipsis1">
                                            ${item.text}
                                        </div>
                                    </div>
                                    <div class="message-right">
                                        <div class="message-right-count-message">
                                            ${item.countTotalUnread}
                                        </div>
                                        <div class="message-right-date">
                                            ${moment(item.createdTime).format('HH:mm')}
                                        </div>
                                    </div>
                                </div>
            `);
            });
            $(`.message-item .message-left-avatar`).removeClass("online");
            for (var i = 0; i < ListUserOnline.length; i++) {
                var item = ListUserOnline[i];
                $(`.message-item[data-account=${item.accountId}] .message-left-avatar`).addClass("online");
            }
            $(".message-item")[0].click();
        }
    }
}

async function LoadMoreContactMessage() {
    var result = await httpService.getAsync(`message/api/ListContact?pageIndex=${pageIndexConversation}&pageSize=${pageSizeContact}`);
    if (result.status == '200') {
        result.data.forEach(function (item) {
            $(".pills-message-tab-container").append(`
                <div class="message-item" data-account='${item.receiverId}' data-accountname='${item.accountName}' data-photo='${item.accountPhoto}'>
                                    <div class="message-left">
                                        <div class="message-left-avatar">
                                            <img src="${item.accountPhoto}">
                                        </div>
                                    </div>
                                    <div class="message-center">
                                        <div class="message-center-account">
                                            ${item.accountName}
                                        </div>
                                        <div class="message-center-message ellipsis1">
                                            ${item.text}
                                        </div>
                                    </div>
                                    <div class="message-right">
                                        <div class="message-right-count-message">
                                            ${item.countTotalUnread}
                                        </div>
                                        <div class="message-right-date">
                                            ${moment(item.createdTime).format('HH:mm')}
                                        </div>
                                    </div>
                                </div>
            `);
        });
        $(`.message-item .message-left-avatar`).removeClass("online");
        for (var i = 0; i < ListUserOnline.length; i++) {
            var item = ListUserOnline[i];
            $(`.message-item[data-account=${item.accountId}] .message-left-avatar`).addClass("online");
        }
    }
}

async function LoadUnread() {
    var result = await httpService.getAsync(`message/api/LoadUnread?pageIndex=${pageIndexContact}&pageSize=${pageSizeContact}`);
    if (result.status == '200') {
        result.data.forEach(function (item) {
            $(`.message-item[data-account=${item.receiverId}] .message-right-count-message`).text(item.countTotalUnread);
            $(`.message-item[data-account=${item.receiverId}] .message-center-message`).text(item.text);
        });
    }
}

async function ReadedMessage(receiverId, roomName) {
    var result = await httpService.getAsync(`message/api/ReadedMessage?accountId=${receiverId}&roomName=${roomName}`);
}
const groupBy = (array, key) => {
    return array.reduce((result, item) => {
        const groupKey = item[key];

        // If the group doesn't exist yet, create it
        if (!result[groupKey]) {
            result[groupKey] = [];
        }

        // Add the current item to the group
        result[groupKey].push({ item });

        return result;
    }, {});
};

var pageIndexContact = 1;
var pageSizeContact = 10;
async function LoadContact() {
    pageIndexContact = 1;
    var result = await httpService.getAsync(`account/api/ListContact?pageIndex=${pageIndexContact}&pageSize=${pageSizeContact}`);
    $('.pills-contact-tab-contact').html("");
    if (result.status == '200') {
        console.log(result)
        var group = "";
        if (result.data[0].countDoctor >= 0) {
            var rowDoctor = "";
            result.data[0].listDoctor.forEach(function (item) {
                rowDoctor += `
                     <div class="message-item" data-account='${item.id}' data-accountname='${item.name}' data-photo='${item.photo}' data-room="${item.roomName}">
                                    <div class="message-left">
                                        <div class="message-left-avatar">
                                            <img src="${item.photo}">
                                        </div>
                                    </div>
                                    <div class="message-center">
                                        <div class="message-center-account">
                                            ${item.name}
                                        </div>
                                    </div>
                                </div>
                `
            });
            group += `<div class="group-contact"><h5 class="m-0">Bác sĩ</h5>` + rowDoctor + `</div>`;

        }
        if (result.data[0].countUser >= 0) {
            var rowUser = "";
            result.data[0].listUser.forEach(function (item) {
                rowUser += `
                     <div class="message-item" data-account='${item.id}' data-accountname='${item.name}' data-photo='${item.photo}'>
                                    <div class="message-left">
                                        <div class="message-left-avatar">
                                            <img src="${item.photo}">
                                        </div>
                                    </div>
                                    <div class="message-center">
                                        <div class="message-center-account">
                                            ${item.name}
                                        </div>
                                    </div>
                                </div>
                `
            });
            group += `<div class="group-contact"><h5 class="m-0">Người dùng</h5>` + rowUser + `</div>`;

        }
        $(".pills-contact-tab-contact").append(group);
        $(`.message-item .message-left-avatar`).removeClass("online");
        for (var i = 0; i < ListUserOnline.length; i++) {
            var item = ListUserOnline[i];
            $(`.message-item[data-account=${item.accountId}] .message-left-avatar`).addClass("online");
        }
    }
}
async function LoadMoreContact() {
    var result = await httpService.getAsync(`account/api/ListContact?pageIndex=${pageIndexContact}&pageSize=${pageSizeContact}`);
    if (result.status == '200') {
        var row = "";
        var group = "";
        if (result.data[0].countUser >= 0) {
            result.data[0].listUser.forEach(function (item) {
                row += `
                     <div class="message-item" data-account='${item.id}' data-accountname='${item.name}' data-photo='${item.photo}' data-room="${item.roomName}">
                                    <div class="message-left">
                                        <div class="message-left-avatar">
                                            <img src="${item.photo}">
                                        </div>
                                    </div>
                                    <div class="message-center">
                                        <div class="message-center-account">
                                            ${item.name}
                                        </div>
                                    </div>
                                </div>
                `
            });
        }
        $(".group-contact:last-child").append(row);
        $(`.message-item .message-left-avatar`).removeClass("online");
        for (var i = 0; i < ListUserOnline.length; i++) {
            var item = ListUserOnline[i];
            $(`.message-item[data-account=${item.accountId}] .message-left-avatar`).addClass("online");
        }
    }
}

function scrollSendMessgage() {
    $('.message-body-container').animate({ scrollTop: $('.message-body-container').prop("scrollHeight") }, 1);
}

$(".card-message").click(async function () {
    $.when(ReadedMessage(receiverAccountIdFocus)).done(async function () {
        await LoadUnread();
    })
})
//Send message logic end

$(document).ready(async function () {
    await LoadContactMessage();
    await LoadContact();
    $(`.message-item .message-left-avatar`).removeClass("online");
    for (var i = 0; i < ListUserOnline.length; i++) {
        var item = ListUserOnline[i];
        $(`.message-item[data-account=${item.accountId}] .message-left-avatar`).addClass("online");
    }
});


let emoji = ['128512', '128513', '128514', '128515', '128516', '128517', '128518', '128519', '128520', '128521', '128522', '128523', '128524', '128525', '128526', '128527', '128528', '128529', '128530', '128531', '128532', '128533', '128534', '128535', '128536', '128537', '128538', '128539', '128540', '128541', '128542', '128543', '128544', '128545', '128546', '128547', '128548', '128549', '128550', '128551', '128552', '128553', '128554', '128555', '128556', '128557', '128558', '128559', '128560', '128561', '128562', '128563', '128564', '128565', '128566'];
emoji.forEach(function (item) {
    $('.emoji-box').append(` <li class="emoji-item" data-value='&#${item};'>
                                        &#${item};
                                    </li>`);
});
$(".emoji-item").on("click", function () {
    var emojiValue = $(this).data("value");
    var currentText = $("#text-send-message").val();
    var newText = currentText + emojiValue;
    $(".emoji-box").removeClass("active");
    $("#text-send-message").val(newText);
});
