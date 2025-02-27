"use strict";

var roomId;
var accountRoomId = [];
var dataHistoryMessage = [];

const connectionMessageAdmin = new signalR.HubConnectionBuilder()
    .withUrl(`/ListConversation`)
    .build();

connectionMessageAdmin.on("ListConversation", function (obj) {
    $("#kt_tab_pane_7").html("");
    listConversation();
});
connectionMessageAdmin.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
$(document).ready(async function () {
    await loadDataContact();
    $("#kt_tab_pane_7").html("");
    listConversation();
    

    //Khi ấn vào người tư vấn viên hoặc thanh thiếu niên
    // sẽ gọi đến API và add họ vào phòng
    //Nếu chưa có phòng thì sẽ thêm họ vào phòng
    //Nếu chưa có phòng thì return roomId
    $(document).on("click", ".my-contact", function () {
         pageIndexMessage = 1;
        var userName = $(this).attr('data-name');

        if ($('#myTable').css('display') === 'none') {
            $("#userName").text(userName);
            $('#myTable').css('display', 'table');
        } else {
            $("#userName").text(userName);
            $('#myTable').css('display', 'table');
        }

        var accountId1 = $(this).data('value');
        $("#checkActive").attr("data-value", accountId1);
        var objAddAccountRoom = {
            "accountId1": accountId,
            "accountId2": accountId1
        }
        $.ajax({
            url: systemURL + "Room/api/AddAccountRoom",
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
                    roomId = responseData.data[0].id;
                    loadDataMessage();
                    loadAccountRoom();
                    var checkActiveUser = ListUserOnline.find(x => x.accountId == accountId1);
                    if (checkActiveUser != null) {
                        $("#checkActive .badge ").removeClass("bg-warning").addClass("bg-success");
                        $("#checkActive .text-muted").text("Trực tuyến");
                    }
                    const connection = new signalR.HubConnectionBuilder()
                        .withUrl(`/AccountSendMessageHub?roomId=${roomId}`)
                        .build();
                    connection.on("ReceiveMessage", function (obj) {
                        if (obj.accountId == accountId) {
                            var newRow = `
                                                <div class="d-flex justify-content-end mb-10">
                                                    <span style="margin-top: 15px; margin-right: 5px;" class="text-muted fs-7 mb-1">${timeSince(obj.createdTime)}</span>
                                                    <div class="d-flex flex-column align-items-end">
                                                        <div class="p-5 rounded bg-light-primary text-dark fw-semibold mw-lg-400px text-end" data-kt-element="message-text" id="messagesList">
                                                            ${obj.text}
                                                        </div>
                                                    </div>
                                                </div>
                                `
                           
                            $("#chatMessage").val("");
                            $("#chatMessageSignalR").append(newRow);
                        } else {
                            var newRow = `
                            <div class="d-flex justify-content-start mb-10 ">
                                <div class="d-flex justify-content-start mb-10">
                                 <!--begin::Avatar-->
                                 <div class="symbol  symbol-35px symbol-circle ">
                                    <img style="margin-top: 5px;" alt="Pic" src="/admin/assets/media/avatars/300-25.jpg"/>
                                 </div><!--end::Avatar-->
                                <div class="d-flex flex-column align-items-start">
                                    <div class="p-5 rounded bg-light-info text-dark fw-semibold mw-lg-400px text-start" data-kt-element="message-text" id="messagesList">
                                    ${obj.text}
                                    </div>
                                </div>
                               <span style="margin-top: 15px; margin-left: 5px;" class="text-muted fs-7 mb-1">${timeSince(obj.createdTime)}</span>
                            </div>
                            </div>
            `
                            
                            $("#chatMessage").val("");
                            $("#chatMessageSignalR").append(newRow);
                            //xử lý khi nhận tin nhắn mới
                            $(`.my-parent[data-value= ${accountId1}] .text-muted`).text(obj.text);
                            $(`.my-parent[data-value= ${accountId1}]`).parent().parent().find('.item-createdTime').text(timeSince(obj.createdTime));
                        }
                        scroll2();
                    });
                    connection.start().then(function () {
                        document.getElementById("sendButton").disabled = false;
                    }).catch(function (err) {
                        return console.error(err.toString());
                    });
                }
            },
            error: function (e) {
                //console.log(e.message);
                Swal.fire(
                    'Lỗi!',
                    'Đã xảy ra lỗi, vui lòng thử lại',
                    'error'
                );
            }
        });
    });
});

var dataSource = [];

document.getElementById("sendButton").disabled = true;


function loadAccountRoom() {
    $.ajax({
        url: systemURL + "AccountRoom/api/Detail/" + accountId + "/" + roomId,
        type: "GET",
        contentType: "application/json",
        success: function (responseData) {
            accountRoomId = responseData.data;
        }
    })
}


//Danh sách tin nhắn trong phòng
var pageIndexMessage = 1;
var pageSizeMessage = 5;
async function loadDataMessage() {
    var dateTime;
    $("#chatMessageSignalR").html("");
    await $.ajax({
        url: systemURL + "message/api/ListMessage/" + roomId + "?pageIndex=" + pageIndexMessage + "&pageSize=" + pageSizeMessage,
        type: "GET",
        contentType: "application/json",
        success: function (responseData) {
           
            var dataSource = responseData.data;
            dataSource.forEach(function (item, index) {
                if (item.accountId == accountId) {
                    var newRow = `
                    <div class="d-flex justify-content-end mb-10">
                            <div class="d-flex flex-column align-items-end">
                                <div class="d-flex align-items-center mb-2">
                                     <span style="margin-right: 5px;" class="text-muted fs-7 mb-1">${timeSince(item.createdTime)}</span>
                                    <div class="p-5 rounded bg-light-primary text-dark fw-semibold mw-lg-400px text-end" data-kt-element="message-text" id="messagesList">
                                    ${item.text}
                                    </div>
                                </div>
                            </div>
            `
                    $("#chatMessageSignalR").append(newRow);
                    //append:  

                } else {
                    var newRow = `
                             <div class="d-flex justify-content-start mb-10">
                                 <!--begin::Avatar-->
                                 <div class="symbol  symbol-35px symbol-circle ">
                                    <img style="margin-top: 5px;" alt="Pic" src="/admin/assets/media/avatars/300-25.jpg"/>
                                 </div><!--end::Avatar-->
                                <div class="d-flex flex-column align-items-start">
                                    <div class="p-5 rounded bg-light-info text-dark fw-semibold mw-lg-400px text-start" data-kt-element="message-text" id="messagesList">
                                    ${item.text}
                                    </div>
                                </div>
                               <span style="margin-top: 15px; margin-left: 5px;" class="text-muted fs-7 mb-1">${timeSince(item.createdTime)}</span>
                            </div>
            `
                    $("#chatMessageSignalR").append(newRow);
                }
            })
        }
    })
    scroll2();

}

async function loadDataMessagePrevious() {
    await $.ajax({
        //url: systemURL + "message/api/ListMessageByRoomId/" + roomId + "?pageIndex=" + pageIndexMessage + "&pageSize=" + pageSizeMessage,
        url: systemURL + "message/api/ListMessage/" + roomId + "?pageIndex=" + pageIndexMessage + "&pageSize=" + pageSizeMessage,
        type: "GET",
        contentType: "application/json",
        success: function (responseData) {

            var dataSource = responseData.data;
            dataSource.forEach(function (item, index) {
                if (item.accountId == accountId) {
                    var newRow = `
                    <div class="d-flex justify-content-end mb-10">

                         <div class="d-flex flex-column align-items-end">
                                <div class="d-flex align-items-center mb-2">
                                     <span style="margin-right: 5px;" class="text-muted fs-7 mb-1">${timeSince(item.createdTime)}</span>
                                    <div class="p-5 rounded bg-light-primary text-dark fw-semibold mw-lg-400px text-end" data-kt-element="message-text" id="messagesList">
                                    ${item.text}
                                    </div>

                                </div>
                            </div>
            `
                    $("#chatMessageSignalR").prepend(newRow);

                } else {
                    var newRow = `
                            <div class="d-flex justify-content-start mb-10">
                                 <!--begin::Avatar-->
                                 <div class="symbol  symbol-35px symbol-circle ">
                                    <img style="margin-top: 5px;" alt="Pic" src="/admin/assets/media/avatars/300-25.jpg"/>
                                 </div><!--end::Avatar-->
                                <div class="d-flex flex-column align-items-start">
                                    <div class="p-5 rounded bg-light-info text-dark fw-semibold mw-lg-400px text-start" data-kt-element="message-text" id="messagesList">
                                    ${item.text}
                                    </div>
                                </div>
                               <span style="margin-top: 15px; margin-left: 5px;" class="text-muted fs-7 mb-1">${timeSince(item.createdTime)}</span>
                            </div>
            `
                    $("#chatMessageSignalR").prepend(newRow);
                }
            })
            $('#chatMessageSignalR').scrollTop(200);
        }
    })
}

let scrollContainer = document.getElementById("scroll-container");

//Scroll listMessage
const elementlistMessage = $('#chatMessageSignalR');
let lastScrollToplistMessage = 0;

elementlistMessage.on('scroll', function (e) {
    if (elementlistMessage.scrollTop() <= lastScrollToplistMessage) {
          pageIndexMessage += 1;
          loadDataMessagePrevious();
    }
});

//Danh sách những người liên hệ
var pageIndexDataContact = 1;
var pageSizeDataContact = 3;
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
                                    <!--begin::User-->
                                        <div class="d-flex flex-stack py-4 item-scroll-counselor">
                                            <!--begin::Details-->
                                            <div class="d-flex align-items-center">
                                                <!--begin::Avatar--><div class="symbol  symbol-45px symbol-circle "><span class="symbol-label  bg-light-danger text-danger fs-6 fw-bolder ">M</span><div class="symbol-badge bg-warning start-100 top-100 border-4 h-8px w-8px ms-n2 mt-n2"></div></div><!--end::Avatar-->
                                                <!--begin::Details-->
                                                <div class="ms-5">
                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2 toggleButton my-contact" data-value="${item.id}" data-name="${item.name}" >${item.name}</a>
                                                    <div class="fw-semibold text-muted text-list-conversation">${item.email}</div>
                                                </div>
                                                <!--end::Details-->
                                            </div>
                                            <!--end::Details-->
                                        </div>
                                <!--end::User-->
                            `

            })
            $("#kt_tab_pane_counselor").append(newRowCounselor);

            var newRowStudent = '';
            var dataListStudent = dataSource.listStudent
            dataListStudent.forEach(function (item, index) {
                newRowStudent += `
                                    <!--begin::User-->
                                        <div class="d-flex flex-stack py-4 item-scroll-student">
                                            <!--begin::Details-->
                                            <div class="d-flex align-items-center">
                                                <!--begin::Avatar--><div class="symbol  symbol-45px symbol-circle "><span class="symbol-label  bg-light-danger text-danger fs-6 fw-bolder ">M</span><div class="symbol-badge bg-warning start-100 top-100 border-4 h-8px w-8px ms-n2 mt-n2"></div></div><!--end::Avatar-->
                                                <!--begin::Details-->
                                                <div class="ms-5">
                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2 toggleButton my-contact" data-value="${item.id}" data-name="${item.name}" >${item.name}</a>
                                                    <div class="fw-semibold text-muted text-list-conversation">${item.email}</div>
                                                </div>
                                                <!--end::Details-->
                                            </div>
                                            <!--end::Details-->
                                        </div>
                                <!--end::User-->
                            `

            })
            $("#kt_tab_pane_student").append(newRowStudent);
        }
    })
}

//Scroll listCouselor
const elementCounselor = $('#kt_tab_pane_counselor');
let lastScrollTopCounselor = 0;

elementCounselor.on('scroll', function (e) {
    if (elementCounselor.scrollTop() < lastScrollTopCounselor) {
        // upscroll
        return;
    }
    lastScrollTopCounselor = elementCounselor.scrollTop() <= 0 ? 0 : elementCounselor.scrollTop();
    if (elementCounselor.scrollTop() + elementCounselor.height() >= elementCounselor[0].scrollHeight) {
        pageIndexDataContact += 1;
        loadDataContact();
    }
});


//Scroll listStudent
const elementStudent = $('#kt_tab_pane_student');
let lastScrollTopStudent = 0;

elementStudent.on('scroll', function (e) {
    if (elementStudent.scrollTop() < lastScrollTopStudent) {
        // upscroll
        return;
    }
    lastScrollTopStudent = elementStudent.scrollTop() <= 0 ? 0 : elementStudent.scrollTop();
    if (elementStudent.scrollTop() + elementStudent.height() >= elementStudent[0].scrollHeight) {
        pageIndexDataContact += 1;
        loadDataContact();
    }
});


//Danh sách đoạn hội thoại
var pageIndexListConversation = 1;
var pageSizeListConversation = 6;
function listConversation() {
    $.ajax({
        url: systemURL + "AccountRoom/api/ListConversationForWeb?pageIndex=" + pageIndexListConversation + "&pageSize=" + pageSizeListConversation,
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
                var newRow = ` <!--begin::User-->
                                        <div class="d-flex flex-stack py-4 item-scroll-listConversation">
                                            <!--begin::Details-->
                                            <div class="d-flex align-items-center">
                                                <!--begin::Avatar--><div class="symbol  symbol-45px symbol-circle "><span class="symbol-label  bg-light-danger text-danger fs-6 fw-bolder ">M</span><div class="symbol-badge bg-warning start-100 top-100 border-4 h-8px w-8px ms-n2 mt-n2"></div></div><!--end::Avatar-->
                                                <!--begin::Details-->
                                                <div class="ms-5 my-parent" data-value="${item.accountId} data-id="${item.id}">
                                                    <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2 my-contact" id="toggleButton" data-value=${item.accountId} data-roomId=${item.roomId} data-name="${item.accountName}">${item.accountName}</a>
                                                    <div class="fw-semibold text-muted text-list-conversation">${item.textMessage !== null ? item.textMessage : ""}</div>
                                                </div>
                                                <!--end::Details-->
                                            </div>
                                            <!--end::Details-->
                                            <!--begin::Lat seen-->
                                            `
                if (item.createdTime == '0001-01-01T00:00:00') {
                    newRow += ` <div class="d-flex flex-column align-items-end ms-2">
                                                                                                <span class="text-muted fs-7 mb-1 item-createdTime"></span>
                                                                                            </div>`

                } else {
                    newRow += ` <div class="d-flex flex-column align-items-end ms-2">
                                                                                                <span class="text-muted fs-7 mb-1 item-createdTime">${timeSince(item.createdTime)}</span>
                                                                                            </div>`

                }

                $("#kt_tab_pane_7").append(newRow);
            })
            for (var i = 0; i < ListUserOnline.length; i++) {
                $(".my-parent[data-value=" + ListUserOnline[i].accountId + "]").parent().find(".symbol-badge").removeClass("bg-warning").addClass("bg-success");
            }
        }
    })
}

//Scroll ListConversation
const elementListConversation = $('#kt_tab_pane_7');
let lastScrollTopListConversation = 0;

elementListConversation.on('scroll', function (e) {
    if (elementListConversation.scrollTop() < lastScrollTopListConversation) {
        // upscroll
        return;
    }
    lastScrollTopListConversation = elementListConversation.scrollTop() <= 0 ? 0 : elementListConversation.scrollTop();
    if (elementListConversation.scrollTop() + elementListConversation.height() >= elementListConversation[0].scrollHeight) {
        pageIndexListConversation += 1;
        listConversation();
    }
});

$("#sendButton").on("click", function (event) {
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
    var accountId1 = $('.my-contact').data('value');
    var obj = {
        accountRoomId: accountRoomId[0].id,
        messageStatusId: 1000001,
        messageTypeId: 1000001,
        text: message
    }
    $.ajax({
        url: systemURL + "message/api/SendMessage",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function (responseData) {
            if (responseData.status == 201 && responseData.message === "CREATED") {
                obj = responseData.data[0];
                $(`.my-parent[data-value= ${accountId1}] .text-muted`).text(message);
                $(`.my-parent[data-value= ${accountId1}]`).parent().parent().find('.item-createdTime').text(timeSince(obj.createdTime));
                scroll2();
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


function formatTimeToHHMM(dateTime) {
    var hours = dateTime.getHours().toString().padStart(2, '0');
    var minutes = dateTime.getMinutes().toString().padStart(2, '0');
    return hours + ":" + minutes;
}

//validate createdTime

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


function scroll2() {
    $('#chatMessageSignalR').scrollTop($('#chatMessageSignalR')[0].scrollHeight);
}

