$(document).ready(async function () {
    $("#list-conversation-end-user").html("");
    listConversation();
    loadDataContact();
})

const connectionMessageUser = new signalR.HubConnectionBuilder()
    .withUrl(`/ListConversation`)
    .build();

connectionMessageUser.on("ListConversation", function (obj) {
    $("#list-conversation-end-user").html("");
    listConversation();
});
connectionMessageUser.start().then(function () {
    document.getElementById("btnSendMessage").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

let roomChatId;
var pageIndexMessage = 1;
var pageSizeMessage = 6;

$("#list-conversation-end-user").on("click", '.list-conversation-item', function (e) {
    $(".chat-list-conversation-end-user").addClass("d-none");
    $("#listmessage").removeClass("d-none");
    $("#btnSendMessage").attr("data-id", $(this).data("id"));
    $("#kt_chatMessageSignalR_enduser").html("");
})


$("#list-conversation-end-user,#couselor-end-user-id,#phone-book-end-user").on("click", ".my-contact", function (e) {
    e.preventDefault();
    e.stopPropagation();
    var userName = $(this).attr('data-name');
    $("#userName").text(userName);
    var imageUser = $(this).attr('data-image');
    console.log(imageUser);
    $("#imageUser").attr("src", imageUser);
    pageIndexMessage = 1;
    var accountId1 = $(this).attr("data-accountId");
    var objAddAccountRoom = {
        "accountId1": accountId,
        "accountId2": accountId1
    }
    $.ajax({
        url: systemURL + "Room/api/AddAccountRoomMobile",
        type: "POST",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            }
        },
        data: JSON.stringify(objAddAccountRoom),
        success: function (responseData) {
            if (responseData.status == 201 && responseData.message === "CREATED") {
                roomChatId = responseData.data[0].id;
                $(".chat-list-conversation-end-user").addClass("d-none");
                $("#listmessage").removeClass("d-none");
                $("#btnSendMessage").attr("data-id", responseData.data[0].accountRoomId);
                $("#kt_chatMessageSignalR_enduser").html("");
                loadDataMessage();

                const connection = new signalR.HubConnectionBuilder()
                    .withUrl(`/AccountSendMessageHub?roomId=${roomChatId}`)
                    .build();
                connection.on("ReceiveMessage", function (item) {
                    var newRow = `
                                        <div class="d-flex justify-content-${item.accountId != accountId ? "start" : "end"}">
                                        <div class="${item.accountId != accountId ? "customer" : "user"}">
                                            <p class="content-chat">${item.text} </p>
                                            <p class="time"> ${timeSince(item.createdTime)} - ${formatDate(item.createdTime)}</p>
                                        </div>
                                    </div>
                                `;
                    $("#chatMessage").val("");
                    $("#kt_chatMessageSignalR_enduser").append(newRow);
                    scroll2();
                });
                connection.start().then(function () {
                    document.getElementById("btnSendMessage").disabled = false;
                }).catch(function (err) {
                    return console.error(err.toString());
                });
            }
        },
        error: function (e) {
            Swal.fire(
                'Lỗi!',
                'Đã xảy ra lỗi, vui lòng thử lại',
                'error'
            );
        }


    });
})


function loadDataMessage() {
    $("#kt_chatMessageSignalR_enduser").html("");
    $.ajax({
        url: systemURL + "message/api/ListMessage/" + roomChatId + "?pageIndex=" + pageIndexMessage + "&pageSize=" + pageSizeMessage,
        type: "GET",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            }
        },
        success: function (responseData) {

            var dataSource = responseData?.data?.sort((a, b) => a.id - b.id);
            // let test = dataSource.sort((a, b) => a.id - b.id);
            //console.log(test);
            dataSource.forEach(function (item, index) {
                var newRow = `
                    <div class="d-flex justify-content-${item.accountId != accountId ? "start" : "end"}">
                    <div class="${item.accountId != accountId ? "customer" : "user"}">
                        <p class="content-chat">${item.text} </p>
                        <p class="time m-0"> ${timeSince(item.createdTime)} - ${formatDate(item.createdTime)}</p>
                    </div>
                </div>
            `;
                $("#kt_chatMessageSignalR_enduser").append(newRow);
                scroll2();


            })
        }
    })

}



function loadDataMessagePrevious() {
    $.ajax({
        url: systemURL + "message/api/ListMessage/" + roomChatId + "?pageIndex=" + pageIndexMessage + "&pageSize=" + pageSizeMessage,
        type: "GET",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            }
        },
        success: function (responseData) {

            var dataSource = responseData.data;
            dataSource.forEach(function (item, index) {
                var newRow = `
                                        <div class="d-flex justify-content-${item.accountId != accountId ? "start" : "end"}">
                                        <div class="${item.accountId != accountId ? "customer" : "user"}">
                                            <p class="content-chat">${item.text} </p>
                                            <p class="time"> ${timeSince(item.createdTime)} - ${formatDate(item.createdTime)}</p>
                                        </div>
                                    </div>
                                `;
                $("#kt_chatMessageSignalR_enduser").prepend(newRow);
            })
            $('#kt_chatMessageSignalR_enduser').scrollTop(200);
        }
    })
}

let scrollContainer = document.getElementById("scroll-container");

//Scroll listMessage
const elementlistMessage = $('#kt_chatMessageSignalR_enduser');
let lastScrollToplistMessage = 0;

elementlistMessage.on('scroll', function (e) {
    if (elementlistMessage.scrollTop() <= lastScrollToplistMessage) {
        pageIndexMessage += 1;
        loadDataMessagePrevious();
    }
});

$(".back-message").click(function () {
    $(".chat-list-conversation-end-user").removeClass("d-none");
    $("#listmessage").addClass("d-none");
});



var pageIndexConversation = 1;
var pageSizeConversation = 6;
async function listConversation() {
    await $.ajax({
        url: systemURL + "AccountRoom/api/ListConversationForWeb?pageIndex=" + pageIndexConversation + "&pageSize=" + pageSizeConversation,
        type: "GET",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            }
        },
        success: function (responseData) {
            var dataSource = responseData.data;

            dataSource.forEach(function (item, index) {
                var newRow = ` <div id="conversation-id-${item.id}"
                data-image="${item.photo}"data-name="${item.accountName}" data-roomChatId=${item.roomChatId} data-id=${item.id} data-accountId=${item.accountId} class="list-conversation-item my-contact">
                                    <div class="item-conversation">
                                        <div class="image-parent">
                                            <img class="image-end-user" src="${item.photo}" />
                                            <div class="check-active"></div>
                                        </div>
                                        <div>
                                            <p class="user-name-conversation">${item.accountName}</p>
                                            <span class="text-conversation">${item.textMessage !== null ? item.textMessage : ""}</span>
                                        </div>
                                    </div>
                                    <div class="noti-message">
                                        <div>
                                       
                                        </div>
                                        <span class="createdTinme-conversation">${timeSince(item.createdTime)}</span>
                                    </div>
                            </div>
                 `
                $("#list-conversation-end-user").append(newRow);
            })
            //for (var i = 0; i < ListUserOnline.length; i++) {
            //    $(".my-parent[data-value=" + ListUserOnline[i].accountId + "]").parent().find(".symbol-badge").removeClass("bg-warning").addClass("bg-success");
            //}
        }
    })
}

var pageIndexDataContact = 1;
var pageSizeDataContact = 5;
async function loadDataContact() {
    await $.ajax({
        url: systemURL + "Account/api/ListContact?pageIndex=" + pageIndexDataContact + "&pageSize=" + pageSizeDataContact,
        type: "GET",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            }
        },
        success: function (responseData) {
            var dataSource = responseData.data[0];
            var newRowCounselor = '';
            var countCounselor = dataSource.countConselor;
            $("#countCounselor").text(" (" + countCounselor + ")");
            var countStudent = dataSource.countStudent;
            $("#countStudent").text(" (" + countStudent + ")");
            var dataListCounselor = dataSource.listConselor;
            dataListCounselor.forEach(function (item, index) {
                newRowCounselor += `
                                    <div class="counselor-parent my-contact d-flex" data-name="${item.name}" data-accountId=${item.id} data-image="${item.photo}">
                                        <div>
                                            <img class="image-counselor" src="${item.photo}" />
                                        </div>
                                        <div class="icon_work"></div>
                                        <div class="d-flex auth_info">
                                            <span class="name-counselor">${item.name}</span>
                                        </div>
                                    </div>
                            `

            })
            $("#couselor-end-user-id").append(newRowCounselor);

            var newRowStudent = '';
            var dataListStudent = dataSource.listStudent
            dataListStudent.forEach(function (item, index) {
                newRowStudent += `
                                   <div class="counselor-parent my-contact d-flex" data-accountId=${item.id} data-name=${item.name} data-image="${item.photo}">
                                        <div>
                                            <img class="image-counselor" src="${item.photo}" />
                                        </div>
                                        <div class="icon_work"></div>
                                        <div class="d-flex auth_info">
                                            <span class="name-counselor">${item.name}</span>
                                        </div>
                                    </div>
                            `

            })
            $("#phone-book-end-user").append(newRowStudent);
        }
    })
}

$("#btnSendMessage").on("click", function (event) {
    sendMessage();
});

//Enter send message
$("#chatMessage").on("keypress", function (event) {
    if (event.which === 13) {
        sendMessage();
    }
});

//SendMessage
function sendMessage() {
    var message = document.getElementById("chatMessage").value;
    var accountRoomId = $("#btnSendMessage").attr("data-id");
    //var accountId1 = $('.my-contact').data('value');
    var obj = {
        accountRoomId: accountRoomId,
        messageStatusId: 1000001,
        messageTypeId: 1000001,
        text: message
    }
    $.ajax({
        url: systemURL + "message/api/SendMessage",
        type: "POST",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            }
        },
        data: JSON.stringify(obj),
        success: function (responseData) {
            if (responseData.status == 201 && responseData.message === "CREATED") {
            }
        },
        error: function (e) {
            Swal.fire(
                'Lỗi!',
                'Đã xảy ra lỗi, vui lòng thử lại',
                'error'
            );
            submitButton.removeAttribute('data-kt-indicator');
            submitButton.disabled = false;
        }
    });
}

function formatTime(date) {
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return `${hours}:${minutes}`;
}

function timeSince(dateTime) {
    const currentTime = new Date();
    const timeDifference = (currentTime - new Date(dateTime)) / 1000;
    return formatTime(new Date(dateTime));
}

function formatDate(dateObject) {
    var d = new Date(dateObject);
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var date = day + "/" + month + "/" + year;

    return date;
};
function scroll2() {
    $('#kt_chatMessageSignalR_enduser').scrollTop($('#kt_chatMessageSignalR_enduser')[0].scrollHeight);
}