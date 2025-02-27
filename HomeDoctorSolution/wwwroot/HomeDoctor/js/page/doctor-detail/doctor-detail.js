"use strict";
var counselorId = 0;
var healthFacilityAddress = "";
async function loadData() {
    $('#time').html("");
    $('#time').append(new Option("Chọn thời gian", "", false, false)).trigger('change');
    var result = await httpService.getAsync("Doctors/api/DetailViewmodel/" + id);
    if (result.status == "200") {
        result.data.forEach(function (item) {
            healthFacilityAddress = item.healthFacilityAddress
            counselorId = item.accountId;
            $(".link-second").text("Bác sĩ " + item.name);
            $("#doctor-name").val(item.name);
            $("#healthFacility").html(item.healthFacilityName);
            //$("#service").val(item.license);
            $("#service").val(item.license + " (" + formatNumberCart(item.serviceFee.toString()) + " đ)");
            $(".detail-name").html("BS. " + item.name)
            $(".detail-des").html(item.info)
            $("#specialist").html(item.specialist)
            $("#language").html(item.language)
            $("#experience").html(item.experience)
            $("#position").html(item.position)
            $(".detail-description-content").html(item.description)
            $(".image-doctor img").attr("src", item.image);
            $("#name").val(JSON.parse(localStorage.profile).name);
            $("#phone-number").val(JSON.parse(localStorage.profile).phone);
            var intervals = generateTimeIntervals(item.startTime, item.endTime, 30);
            intervals.forEach(interval => $('#time').append(new Option(interval, interval, false, false)).trigger('change'));
        })
    }
}
function generateTimeIntervals(startTime, endTime, intervalMinutes) {
    // Parse start and end times to minutes
    let parseTime = timeString => {
        let [hours, minutes, seconds] = timeString.split(':').map(Number);
        return hours * 60 + minutes + seconds / 60;
    };

    let startMinutes = parseTime(startTime);
    let endMinutes = parseTime(endTime);

    // Calculate the total number of intervals
    let numIntervals = Math.floor((endMinutes - startMinutes) / intervalMinutes);

    // Generate equally spaced time intervals
    let timeIntervals = Array.from({ length: numIntervals + 1 }, (_, index) => startMinutes + index * intervalMinutes);

    // Convert back to hours and minutes in the "HH:mm" format
    let formattedIntervals = timeIntervals.map(interval => {
        let hours = Math.floor(interval / 60);
        let minutes = Math.floor(interval % 60);

        return `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}`;
    });

    return formattedIntervals;
}
function openModalBooking() {
    if (accountId == '0') {
        OpenModalLogin();
    }
    else {
        $("#booking").modal("show");
    }
}
function validateBooking() {
    var errorList = [];
    var date = new Date()
    var getDate = $("#day").val();
    var data = getDate.split("/");
    const checked = $('#pxp-confirmCheckbox').is(':checked');
    var getTime = $("#time").val();
    var splitTime = getTime.split("-")
    var validDate = new Date(data[1] + "/" + data[0] + "/" + data[2] + " " + splitTime[0]); 
    if ($("#booking-address").val() == "") {
        errorList.push("Địa chỉ không được để trống.");
    }
    if ($("#name").val() == "") {
        errorList.push("Họ tên người khám không được để trống.");
    }
    if ($("#phone-number").val() == "") {
        errorList.push("Số điện thoại không được để trống.");
    }
    if (!ValidatePhone($("#phone-number").val())) {
        errorList.push("Số điện thoại không hợp lệ.");
    }
    if ($("#day").val() == "") {
        errorList.push("Ngày đặt không được để trống.");
    }
    if ($("#time").val() == "") {
        errorList.push("Giờ đặt không được để trống.");
    }
    if (validDate < date) {
        errorList.push("Không thể hẹn giờ trước giờ hiện tại")
    }
    if (!checked) {
        errorList.push("Bạn chưa đồng ý với điều khoản.")
    }
    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal__admin__subtitle'>Đặt lịch khám không thành công</p>";

        Swal.fire(
            swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        bookingDoctor();
    }
}
function ValidatePhone(e) {
    return !!/^[0-9\-\+]{9,15}$/.test(e)
}
function RedirectToChat() {
    if (accountId == 0) {
        OpenModalLogin();
    }
    else {
        window.location.href = '/tin-nhan/' + counselorId;
    }
}
function bookingDoctor() {
    var time = $("#time").val();
    var timeBooking = time.split("-");
    var date = $("#day").val().split('/').reverse().join('-');
    var startTime = date + "T" + timeBooking[0] + ":00";
    /*var endTime = date + "T" + timeBooking[1] + ":00";*/
    var bookingtypeId = 1000002;
    if ($("#349").is(':checked')) {
        bookingtypeId = 1000001;
    }
    var booking = {
        "id": 0,
        "accountId": parseInt(accountId),
        "bookingTypeId": bookingtypeId,
        "bookingStatusId": 1000001,
        "counselorId": parseInt(id),
        "name": $("#name").val(),
        "address": $("#booking-address").val(),
        "URL": $("#phone-number").val(),
        "startTime": startTime,
        "endTime": null,
        "info": $("#description").val(),
        "reason": ""
    };

    Swal.fire({
        title: 'Đặt lịch khám',
        html: "Bạn có chắc chắn muốn đặt lịch khám?",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Đồng ý',
    }).then((result) => {
        booking.createdTime = new Date();
        booking.active = 1
        if (result.value) {
            AddBooking(booking)
        }
    })
}

async function AddBooking(booking) {
    try {
        $("#loading").addClass("show")
        /*if (booking.id == 0) {*/
        //console.log(booking);
        var response = await httpService.postAsync("Booking/api/add", booking);
        if (response.status == 201 && response.message === "CREATED") {
            $("#loading").removeClass("show")
            $(".line-process-2").attr("src", "/HomeDoctor/img/booking-process/line-active.png")
            $(".confirm-process").attr("src", "/HomeDoctor/img/booking-process/confirm-active.png")
            $(".line-process-2-mb").attr("src", "/HomeDoctor/img/booking-process-mobile/mobile-line-active.png")
            $(".confirm-process-mb").attr("src", "/HomeDoctor/img/booking-process-mobile/mobile-confirm-active.png")
            Swal.fire({
                html: '<strong>Đặt lịch hẹn tư vấn thành công</strong>',
                icon: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Đồng ý',
            }).then((result) => {
                if (result) {
                    location.href = "/chi-tiet-lich-su-tu-van/" + response.data[0].id;
                }
            })
        }
        else {
            $("#loading").removeClass("show")
            Swal.fire({
                html: 'Đặt lịch hẹn không thành công, vui lòng thử lại sau !',
                icon: 'error'
            });
        }
    }
    catch (ex) {
        console.log(ex);
    }
}

$(document).ready(function () {
    $(".my-rating").starRating({
        useGradient: false,
        starSize: 20,
        initialRating: 5,
        readOnly: true,
        disableAfterRate: false,
        starShape: 'rounded',
    });
    loadData();
    $("#350").click(function () {
        if ($(this).is(':checked')) {
            $("#booking-address").attr("disabled", true);
            $("#booking-address").attr("readonly", true);
            $("#booking-address").val(healthFacilityAddress);
        }
    });
    $("#349").click(function () {
        if ($(this).is(':checked')) {
            $("#booking-address").attr("disabled", false);
            $("#booking-address").attr("readonly", false);
            $("#booking-address").val("");

        }
    });
    $("#351").click(function () {
        if (!$(this).is(':checked')) {
            $("#name").attr("disabled", true);
            $("#name").attr("readonly", true);
            $("#phone-number").attr("disabled", true);
            $("#phone-number").attr("readonly", true);
            $("#name").val(JSON.parse(localStorage.profile).name);
            $("#phone-number").val(JSON.parse(localStorage.profile).phone);
        } else {
            $("#name").attr("disabled", false);
            $("#name").attr("readonly", false);
            $("#phone-number").attr("disabled", false);
            $("#phone-number").attr("readonly", false);
            $("#name").val("");
            $("#phone-number").val("");
        }
    });
});