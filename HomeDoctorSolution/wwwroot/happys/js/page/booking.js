"use strict";

var online = $('.booking-online')
var offline = $('.booking-offline')
var idSelect = 0;
var bookingType = '';
var address = ''
var start = ''
var end = ''
var timeselect = ''
var startTime = ''
var endTime = ''
var bookingStatus = ''
var m = new Date();
var counselorId = ''
var accountName = ''
var timeSelectSplit = ''
var datePcSelect = ''
var dateMobileSelect = ''
var bookingAccountId = 0;
var bookingCounselorId = 0;
var bookingId = 0
var id = 0
var bookingDetailHTML = document.getElementById("booking-detail");
var checkSchool = 0;
//var load = async () => {
//    var result;
//    if (systemConstant.accountId > 0) {
//        var booking_detail = await httpService.getAsync("Booking/api/Detail/" + systemConstant.accountId);
//        if (booking_detail.status == "200") {
//            if (booking_detail.data[0].accountId == systemConstant.accountId || booking_detail.data[0].counselorId == systemConstant.accountId) {                            
//                $(".img").html('');
//                $(".address").html('')
//                $(".counselorName").html('')
//                $("#status").html('')
//                //check xem có phải tư vấn viên không
//                if (booking_detail.data[0].counselorId == systemConstant.accountId) {
//                    result = await httpService.getAsync("Account/api/Detail/" + booking_detail.data[0].accountId);
//                    $('#status').val(1000002);
//                    if (result.status == "200") {
//                        result.data.forEach(function (item, index) {
//                            $(".img").append(`
//                        <img class="shadow" id='booking-photo' src="`+ item.photo + `"/>
//                        `)
//                            $(".counselorName").append(`
//                        <label class="name" name='counselorName' data-value='`+ item.id + `' data-name='` + item.name + `'>
//                                    `+ item.name + `
//                                </label>
//                        `)
//                            $(".address").append(`
//                        <label class="title-input">Địa chỉ tư vấn</label>
//                        <input class="input-booking booking-address" id="address" name="address" value="`+ booking_detail.data[0].address + `"/>
//                        `)
//                        });
//                        $('#btnUpdateBooking').html("Xác nhận");

//                    }
//                    else {
//                        window.location.href = "/Error404";
//                    }
//                    bookingAccountId = booking_detail.data[0].accountId;
//                    bookingCounselorId = booking_detail.data[0].counselorId;
//                }
//                else {
//                    result = await httpService.getAsync("Account/api/Detail/" + booking_detail.data[0].counselorId);
//                    if (result.status == "200") {
//                        result.data.forEach(function (item, index) {
//                            $(".img").append(`
//                        <img class="shadow" id='booking-photo' src="`+ item.photo + `"/>
//                        `)
//                            $(".counselorName").append(`
//                        <label class="name" name='counselorName' data-value='`+ item.id + `' data-name='` + item.name + `'>
//                                    `+ item.name + `
//                                </label>
//                        `)
//                            $(".address").append(`
//                        <label class="title-input">Địa chỉ tư vấn</label>
//                        <input class="input-booking booking-address" id="address" name="address" value="`+ booking_detail.data[0].address + `"/>
//                        `)
//                        })
//                        $('#btnUpdateBooking').html("Lưu");
//                    }
//                    else {
//                        window.location.href = "/Error404";
//                    }

//                }


//                if (booking_detail.data[0].bookingTypeId == systemConstant.bookingType_online) {
//                    online.prop('checked', true);
//                } else {
//                    offline.prop('checked', true);
//                }
//                $("#date-pc").val(moment(booking_detail.data[0].startTime).format("DD/MM/YYYY"));
//                var startTimeDetail = moment(booking_detail.data[0].startTime).format("HH:mm");
//                var endTimeDetail = moment(booking_detail.data[0].endTime).format("HH:mm");
//                var schedule = startTimeDetail + '-' + endTimeDetail;
//                $("#time-pc").val(schedule).trigger("change");
//                if ($("#time-pc").val() == null) {
//                    $('#time-pc').append(new Option(schedule, schedule, false, false)).trigger('change');
//                    $("#time-pc").val(schedule).trigger("change");
//                }
//                $("#changeCounselor").hide();
//                $("#booking-description").val(booking_detail.data[0].info);
//                $('#edit-booking').removeClass('d-none');
//                $('#cancel-booking').removeClass('d-none');
//                $('#btnUpdateBooking').hide();
//                $("#booking-description").prop("readonly", true).prop("disabled", true);
//                $("#time-pc").prop("readonly", true).prop("disabled", true);
//                $("#date-pc").prop("readonly", true).prop("disabled", true);
//                $(':radio').attr('disabled', true);
//                $("#address").prop("readonly", true).prop("disabled", true);


//                if (booking_detail.data[0].bookingStatusId == systemConstant.bookingStatus_wait_accept) {
//                    $("#wait").removeClass("d-none");
//                }
//                else if (booking_detail.data[0].bookingStatusId == systemConstant.bookingStatus_wait_done) {
//                    $("#accept").removeClass("d-none");
//                }
//                else if (booking_detail.data[0].bookingStatusId == systemConstant.bookingStatus_done) {
//                    $("#done").removeClass("d-none");
//                    $("#btnUpdateBooking").addClass("d-none");
//                    $("#edit-booking").addClass("d-none");
//                    $("#cancel-booking").addClass("d-none");
//                }
//                else if (booking_detail.data[0].bookingStatusId == systemConstant.bookingStatus_cancel) {
//                    $("#cancel").removeClass("d-none");
//                    $("#btnUpdateBooking").addClass("d-none");
//                    $("#edit-booking").addClass("d-none");
//                    $("#cancel-booking").addClass("d-none");
//                }
//                else if (booking_detail.data[0].bookingStatusId == systemConstant.bookingStatus_reject) {
//                    $("#reject").removeClass("d-none");
//                    $("#btnUpdateBooking").addClass("d-none");
//                    $("#edit-booking").addClass("d-none");
//                    $("#cancel-booking").addClass("d-none");
//                }
//            }
//            else {
//                //debugger;
//                window.location.href = "/Error404";
//            }
//        }
//        else {
//            //debugger;
//            window.location.href = "/Error404";
//        }
//    }
//    else {
//        var result = await httpService.getAsync("Account/api/DetailCounselor/" + id);
//        $(".img").html('');
//        $(".address").html('')
//        $(".counselorName").html('')
//        $("#status").html('')
//        if (result.status == "200") {
//            result.data.forEach(function (item, index) {
//                $(".img").append(`
//            <img class="shadow" id='booking-photo' src="`+ item.photo + `"/>
//            `)
//                $(".counselorName").append(`
//            <label class="name" name='counselorName' data-value='`+ item.accountId + `' data-name='` + item.accountName + `'>
//                        `+ item.accountName + `
//                    </label>
//            `)
//                $(".address").append(`
//            <label class="title-input">Địa chỉ</label>
//            <input class="input-booking booking-address" name="address" value="`+ item.healthFacilityName + `"/>
//            `)
//            })
//        }
//    }
//}

var addBooking = async () => {
    if (bookingDetailHTML.style.display === "none") {
        //var result = await httpService.getAsync("Account/api/detail/" + systemConstant.accountId);
        var school = await httpService.getAsync("Account/api/DetailCounselor/" + systemConstant.counselorDefautId + "?accountId=" + systemConstant.accountId);
        if (school.status == "200" && school.data != null) {
            $("#booking-address").val(school.data[0].address);
            checkSchool = 1;
        } else {
            $("#booking-address").val('(Bạn chưa cập nhật dữ liệu về trường học trong thông tin cá nhân)');
        }
        bookingDetailHTML.style.display = "block"
        $("#btnAddBooking").html("Đóng")
        $("#btnAddBooking").addClass("cancel")

    }
    else {
        bookingDetailHTML.style.display = "none"
        $("#btnAddBooking").html("Tạo lịch tư vấn")
        $("#btnAddBooking").removeClass("cancel")

    }
}

var loadDataByStatusWait = async function () {
    var result = await httpService.getAsync("booking/api/list-booking-by-bookingStatusId?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&bookingStatusId=" + systemConstant.bookingStatus_wait_accept);
    if (result != null) {
        bookingDetailHTML.style.display = "none"
    }
    var account = await httpService.getAsync("Account/api/Detail/" + systemConstant.accountId);
    var detailBooking = account.data[0].roleId == systemConstant.role_counselor_id ? "/app/booking-detail/" : "/app/booking-detail-user/";
    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#consulting_history_wait").html('');
            var newRow = '';
            var dataSource = result.data;

            dataSource.forEach(function (item, index) {
                var name = account.data[0].roleId == systemConstant.role_counselor_id ? item.accountName : item.counselorName;
                newRow += `<div class="card-history">
                        <div class="item" id="item-consuling-history-wait" data-statusId="${item.bookingStatusId}">
                            <div class="img">
                                <img src="${item.photo}" />
                            </div>

                            <div class="info">
                                <div class="title">
                                    <label class="name mb-2">${name}</label>
                                </div>
                                <div class="tags">
                                     <div class="tag">
                                        <span class="content">
                                            ${timeSince(item.startTime)} - ${timeSince(item.endTime)} ${formatDate(item.startTime)}
                                        </span>
                                    </div>
                                    <div class="tag">
                                        <span class="content">
                                            Đặt lịch hẹn ${item.bookingTypeName}
                                        </span>
                                    </div>
                                </div>

                                <p class="des">
                                    ${item.description != null ? item.description : " "}
                                </p>
                                <a class="see-details" href="${detailBooking}` + item.id + `" >Xem chi tiết
                                </a>
                            </div>
                        </div>
                    </div>`
            });
            $("#parent-consulting-button-wait").addClass("parent-button-container-active")
            $("#consulting_history_accept").addClass("d-none")
            $("#consulting_history_done").addClass("d-none")
            $("#consulting_history_cancel").addClass("d-none")
            $("#consulting_history_wait").append(newRow);
        }
        else {
            $("#consulting_history_wait").html('');
            var newRow = `<div class="not-found d-flex flex-column align-items-center">
            <img style="width:400px;height:auto;" src="/images/default/not-found.png"/>
            <p>Chưa có dữ liệu hiển thị</p>
            </div>`;

            $("#parent-consulting-button-wait").addClass("parent-button-container-active")
            $("#consulting_history_accept").addClass("d-none")
            $("#consulting_history_done").addClass("d-none")
            $("#consulting_history_cancel").addClass("d-none")
            $("#consulting_history_wait").append(newRow);
        }
    }

}

var loadDataByStatusAccept = async function () {
    var result = await httpService.getAsync("booking/api/list-booking-by-bookingStatusId?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&bookingStatusId=" + systemConstant.bookingStatus_wait_done);
    var account = await httpService.getAsync("Account/api/Detail/" + systemConstant.accountId);
    var detailBooking = account.data[0].roleId == systemConstant.role_counselor_id ? "/app/booking-detail/" : "/app/booking-detail-user/";
    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#consulting_history_accept").html('');
            var newRow = '';
            var dataSource = result.data;

            dataSource.forEach(function (item, index) {
                var name = account.data[0].roleId == systemConstant.role_counselor_id ? item.accountName : item.counselorName;
                newRow += `<div class="card-history">
                        <div class="item" id="item-consuling-history-accept" data-statusId="${item.bookingStatusId}">
                            <div class="img">
                                <img src="${item.photo}" />
                            </div>

                            <div class="info">
                                <div class="title">
                                    <label class="name mb-2">${name}</label>
                                </div>
                                <div class="tags">
                                     <div class="tag">
                                        <span class="content">
                                            ${timeSince(item.startTime)} - ${timeSince(item.endTime)} ${formatDate(item.startTime)}
                                        </span>
                                    </div>
                                    <div class="tag">
                                        <span class="content">
                                            Đặt lịch hẹn ${item.bookingTypeName}
                                        </span>
                                    </div>
                                </div>

                                <p class="des">
                                    ${item.description != null ? item.description : " "}
                                </p>
                                <a class="see-details" href="${detailBooking}` + item.id + `" >Xem chi tiết
                                </a>
                            </div>
                        </div>
                    </div>`
            });
            $("#consulting_history_accept").append(newRow);
        }
        else {
            $("#consulting_history_accept").html('');
            var newRow = `<div class="not-found d-flex flex-column align-items-center">
            <img style="width:400px;height:auto;" src="/images/default/not-found.png"/>
            <p>Chưa có dữ liệu hiển thị</p>
            </div>`;

            $("#consulting_history_accept").append(newRow);
        }
    }
}

var loadDataByStatusDone = async function () {
    var result = await httpService.getAsync("booking/api/list-booking-by-bookingStatusId?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&bookingStatusId=" + systemConstant.bookingStatus_done);

    var account = await httpService.getAsync("Account/api/Detail/" + systemConstant.accountId);
    var detailBooking = account.data[0].roleId == systemConstant.role_counselor_id ? "/app/booking-detail/" : "/app/booking-detail-user/";
    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#consulting_history_done").html('');
            var newRow = '';
            var dataSource = result.data;

            dataSource.forEach(function (item, index) {
                var name = account.data[0].roleId == systemConstant.role_counselor_id ? item.accountName : item.counselorName;
                newRow += `<div class="card-history">
                        <div class="item" id="item-consuling-history-done" data-statusId="${item.bookingStatusId}">
                            <div class="img">
                                <img src="${item.photo}" />
                            </div>

                            <div class="info">
                                <div class="title">
                                    <label class="name mb-2">${name}</label>
                                </div>
                                <div class="tags">
                                     <div class="tag">
                                        <span class="content">
                                            ${timeSince(item.startTime)} - ${timeSince(item.endTime)} ${formatDate(item.startTime)}
                                        </span>
                                    </div>
                                    <div class="tag">
                                        <span class="content">
                                            Đặt lịch hẹn ${item.bookingTypeName}
                                        </span>
                                    </div>
                                </div>

                                <p class="des">
                                    ${item.description != null ? item.description : " "}
                                </p>
                                <a class="see-details" href="${detailBooking}` + item.id + `" >Xem chi tiết
                                </a>
                            </div>
                        </div>
                    </div>`
            });
            $("#consulting_history_done").append(newRow);
        }
        else {
            $("#consulting_history_done").html('');
            var newRow = `<div class="not-found d-flex flex-column align-items-center">
            <img style="width:400px;height:auto;" src="/images/default/not-found.png"/>
            <p>Chưa có dữ liệu hiển thị</p>
            </div>`;

            $("#consulting_history_done").append(newRow);
        }
    }
}

var loadDataByStatusCancel = async function () {
    var result = await httpService.getAsync("booking/api/list-booking-by-bookingStatusId?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&bookingStatusId=" + systemConstant.bookingStatus_cancel);

    var account = await httpService.getAsync("Account/api/Detail/" + systemConstant.accountId);
    var detailBooking = account.data[0].roleId == systemConstant.role_counselor_id ? "/app/booking-detail/" : "/app/booking-detail-user/";
    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#consulting_history_cancel").html('');
            var newRow = '';
            var dataSource = result.data;

            dataSource.forEach(function (item, index) {
                var name = account.data[0].roleId == systemConstant.role_counselor_id ? item.accountName : item.counselorName;
                newRow += `<div class="card-history">
                        <div class="item" id="item-consuling-history-cancel" data-statusId="${item.bookingStatusId}">
                            <div class="img">
                                <img src="${item.photo}" />
                            </div>

                            <div class="info">
                                <div class="title">
                                    <label class="name mb-2">${name}</label>
                                </div>
                                <div class="tags">
                                     <div class="tag">
                                        <span class="content">
                                            ${timeSince(item.startTime)} - ${timeSince(item.endTime)} ${formatDate(item.startTime)}
                                        </span>
                                    </div>
                                    <div class="tag">
                                        <span class="content">
                                            Đặt lịch hẹn ${item.bookingTypeName}
                                        </span>
                                    </div>
                                </div>

                                <p class="des">
                                    ${item.description != null ? item.description : " "}
                                </p>
                                <a class="see-details" href="${detailBooking}` + item.id + `" >Xem chi tiết
                                </a>
                            </div>
                        </div>
                    </div>`
            });
            $("#consulting_history_cancel").append(newRow);
        }
        else {
            $("#consulting_history_cancel").html('');
            var newRow = `<div class="not-found d-flex flex-column align-items-center">
            <img style="width:400px;height:auto;" src="/images/default/not-found.png"/>
            <p>Chưa có dữ liệu hiển thị</p>
            </div>`;

            $("#consulting_history_cancel").append(newRow);
        }
    }
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

var checked = () => {
    offline.prop('checked', true)
    //$('.address').removeClass('d-none')
    //$('.phone').addClass('d-none')
    address = $(".booking-address").val()
    bookingType = $("input[name*='radio-bookingtype']:checked").val()
    counselorId = $("label[name*='counselorName']").data('value')
}
$(document).ready(async function () {
    if (roleId == systemConstant.role_counselor_id) {
        $("#btnAddBooking").addClass('d-none')
    }
    /*    await load()*/
    await checked();
    await loadDataByStatusAccept();
    await loadDataByStatusDone();
    await loadDataByStatusCancel();
    $(".pagination_default").attr("class", "navigation pagination pagination_default wait align-items-end justify-content-end");
    let element = $(".pagination_default.wait");
    LoadPagingPage("booking/api/count-list-booking-by-accountId?bookingStatusId=" + systemConstant.bookingStatus_wait_accept, element, loadDataByStatusWait);
})

var wait = () => {
    $("#parent-consulting-button-wait").addClass("parent-button-container-active")
    $("#parent-consulting-button-accept").removeClass("parent-button-container-active")
    $("#parent-consulting-button-done").removeClass("parent-button-container-active")
    $("#parent-consulting-button-cancel").removeClass("parent-button-container-active")
    $("#consulting_history_wait").removeClass("d-none")
    $("#consulting_history_accept").addClass("d-none")
    $("#consulting_history_done").addClass("d-none")
    $("#consulting_history_cancel").addClass("d-none")
    
    $(".pagination_default").attr("class", "navigation pagination pagination_default wait align-items-end justify-content-end");
    let element = $(".pagination_default.wait");
    LoadPagingPage("booking/api/count-list-booking-by-accountId?bookingStatusId=" + systemConstant.bookingStatus_wait_accept, element, loadDataByStatusWait);
}

var accept = () => {
    $("#parent-consulting-button-wait").removeClass("parent-button-container-active")
    $("#parent-consulting-button-accept").addClass("parent-button-container-active")
    $("#parent-consulting-button-done").removeClass("parent-button-container-active")
    $("#parent-consulting-button-cancel").removeClass("parent-button-container-active")
    $("#consulting_history_accept").removeClass("d-none")
    $("#consulting_history_wait").addClass("d-none")
    $("#consulting_history_done").addClass("d-none")
    $("#consulting_history_cancel").addClass("d-none")
    $(".pagination_default").attr("class", "navigation pagination pagination_default accept align-items-end justify-content-end");
    let element = $(".pagination_default.accept");
    LoadPagingPage("booking/api/count-list-booking-by-accountId?bookingStatusId=" + systemConstant.bookingStatus_wait_done, element, loadDataByStatusAccept);
}

var done = () => {
    $("#parent-consulting-button-wait").removeClass("parent-button-container-active")
    $("#parent-consulting-button-accept").removeClass("parent-button-container-active")
    $("#parent-consulting-button-done").addClass("parent-button-container-active")
    $("#parent-consulting-button-cancel").removeClass("parent-button-container-active")
    $("#consulting_history_accept").addClass("d-none")
    $("#consulting_history_wait").addClass("d-none")
    $("#consulting_history_done").removeClass("d-none")
    $("#consulting_history_cancel").addClass("d-none")
    $(".pagination_default").attr("class", "navigation pagination pagination_default done align-items-end justify-content-end");
    let element = $(".pagination_default.done");
    LoadPagingPage("booking/api/count-list-booking-by-accountId?bookingStatusId=" + systemConstant.bookingStatus_done, element, loadDataByStatusDone);
}

var cancel = () => {
    $("#parent-consulting-button-wait").removeClass("parent-button-container-active")
    $("#parent-consulting-button-accept").removeClass("parent-button-container-active")
    $("#parent-consulting-button-done").removeClass("parent-button-container-active")
    $("#parent-consulting-button-cancel").addClass("parent-button-container-active")
    $("#consulting_history_accept").addClass("d-none")
    $("#consulting_history_wait").addClass("d-none")
    $("#consulting_history_done").addClass("d-none")
    $("#consulting_history_cancel").removeClass("d-none")
    $(".pagination_default").attr("class", "navigation pagination pagination_default cancel align-items-end justify-content-end");
    let element = $(".pagination_default.cancel");
    LoadPagingPage("booking/api/count-list-booking-by-accountId?bookingStatusId=" + systemConstant.bookingStatus_cancel, element, loadDataByStatusCancel);
}

online.on('change', () => {
    online.prop('checked', true)
    $('.address').addClass('d-none')
    $('.phone').removeClass('d-none')
    address = '';
    bookingType = $("input[name*='radio-bookingtype']:checked").val()
})

offline.on('change', () => {
    offline.prop('checked', true)
    $('.address').removeClass('d-none')
    $('.phone').removeClass('d-none')
    $("#booking-phone").val("")
    address = $(".booking-address").val()
    bookingType = $("input[name*='radio-bookingtype']:checked").val()
})

$('#time-pc').on('change', function (e) {
    timeselect = $("#time-pc").val()
})


var booking = async () => {
    datePcSelect = $("#date-pc").val().split('/').reverse().join('-');
    bookingStatus = $('#status').val();
    $("input[name*='radio-bookingtype']").on('change', () => {
        bookingType = $("input[name*='radio-bookingtype']:checked").val();
    });
    timeselect = $("#time-pc").val();
    timeSelectSplit = timeselect.split("-");
    start = timeSelectSplit[0];
    end = timeSelectSplit[1];
    startTime = datePcSelect + 'T' + start + ':' + '00';
    endTime = datePcSelect + 'T' + end + ':' + '00';

    var updatingObj = {
        "id": 0,
        "accountId": systemConstant.accountId,
        "bookingTypeId": parseInt(bookingType),
        "bookingStatusId": systemConstant.bookingStatus_wait_accept,
        "counselorId": systemConstant.counselorDefautId,
        "name": accountName,
        "address": address,
        "photo": $("#booking-photo").attr('src'),
        "info": $("#booking-description").val(),
        "startTime": startTime,
        "endTime": endTime,
        "URL": $("#booking-phone").val(),
    };

    //console.log(updatingObj)
    Swal.fire({
        title: 'Đặt lịch tư vấn',
        html: "Bạn có chắc chắn muốn đặt lịch tư vấn của <b>" + accountName + '</b>?',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Đồng ý',
    }).then((result) => {
        updatingObj.createdTime = new Date();
        updatingObj.active = 1
        if (result.value) {
            AddBooking(updatingObj)
        }
    })
}

var AddBooking = async (updatingObj) => {
    debugger
    if (updatingObj.id == 0) {
        updatingObj.bookingStatusId = systemConstant.bookingStatus_wait_accept;
        let response = await httpService.postAsync("booking/api/add", updatingObj);
        if (response.status == 201 && response.message === "CREATED") {
            Swal.fire({
                html: '<strong>Đặt lịch hẹn tư vấn thành công</strong>',
                icon: 'success'
            }).then(async () => {
                //var url = "booking/api/send-email-booking-success?accountId=" + systemConstant.accountId;
                //await httpService.getAsync(url);
                window.location.href = "/user-profile";
            });
        }
        else {
            Swal.fire({
                html: 'Đặt lịch hẹn không thành công, vui lòng thử lại sau !',
                icon: 'error'
            });
        }
    }
    else {
        let response = await httpService.postAsync("booking/api/update", updatingObj);
        var html = updatingObj.bookingStatusId == systemConstant.bookingStatus_cancel ? 'hủy' : 'đặt';
        if (response.status == 200 && response.message === "SUCCESS") {
            Swal.fire({
                html: '<strong>Xác nhận ' + html + ' lịch hẹn tư vấn</strong>',
                icon: 'success'
            }).then(async () => {
                if (html == 'hủy') {
                    $("#reason-cancel-booking").modal('hide');
                    Swal.fire({
                        html: 'Hủy lịch hẹn thành công!',
                        icon: 'success'
                    }).then(() => {
                        window.location.href = "/user-profile";
                    });
                }
                else {
                    Swal.fire({
                        html: 'Cập nhật lịch hẹn thành công!',
                        icon: 'success'
                    }).then(() => {
                        window.location.href = "/user-profile";
                    });
                }

                //window.location.href = "/user-profile#consultingHistory";
            });
        }
        else {
            Swal.fire({
                html: html + ' lịch hẹn không thành công, vui lòng thử lại sau !',
                icon: 'error'
            });
        }
    }
}


var bnt = $('#btnUpdateBooking')
bnt.on('click', async () => {
    validate();
})

async function validate() {
    var errorList = [];

    var regex = /^0\d{9}$/;
    var checkPhoneNumber = regex.test($("#booking-phone").val());
    if (!checkPhoneNumber) {
        errorList.push("Bạn phải nhập số điện thoại có 10 kí tự là số");
    }

    if ($("#time-pc").val() == null || $("#time-pc").val() == '') {
        errorList.push("Bạn chưa chọn thời gian đặt lịch.");
    }
    if ($("#date-pc").val() == null || $("#date-pc").val() == '') {
        errorList.push("Bạn chưa chọn ngày đặt lịch.");
    }

    //thời điểm hiện tại
    var checkTimeBooking = moment().format("YYYY-MM-DD HH:mm");
    datePcSelect = $("#date-pc").val().split('/').reverse().join('-')
    timeselect = $("#time-pc").val();
    timeSelectSplit = timeselect.split("-");
    start = timeSelectSplit[0];
    end = timeSelectSplit[1];
    startTime = datePcSelect + ' ' + start + ':' + '00';
    endTime = datePcSelect + ' ' + end + ':' + '00';
    // Lấy hiệu giữa date2 và date1 trong đơn vị giờ
    var differenceInSeconds = moment(startTime).diff(moment(checkTimeBooking), 'seconds');
    //console.log(differenceInHours);
    if (differenceInSeconds < 21600) {
        errorList.push("Thời gian đặt lịch hẹn phải cách hiện tại tối thiểu 6 giờ.");
    }


    if (errorList.length > 0) {
        var contentError = "<ul>";
        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        })
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal__admin__subtitle'>" + 'Đặt lịch' + " không thành công</p>";
        Swal.fire(
            'Quản lý lịch hẹn' + swalSubTitle,
            contentError,
            'warning'
        );
    } else {
        await booking();
    }
}

