// Get the query string from the URL
const queryString = window.location.search;

// Create a URLSearchParams object from the query string
const params = new URLSearchParams(queryString);

// Retrieve the value of a specific parameter
const serviceId = params.get('serviceId');
const healthFacilityId = params.get('healthFacilityId');
const district = params.get('district');
var loadData = async function LoadDataListDoctor() {

    var urlData = "doctors/api/ListPagingViewModelFilter?pageIndex=" + pageIndex + "&pageSize=" + 6 + "&serviceId=" + $("#select_services").val() + "&healthFacilityId=" + $("#select_healthFacility").val() + "&keyword=" + $("#search_doctor").val() + "&district=" + $("#select_districts").val();
    $("#list_doctor").html('<div class="not-found d-flex flex-column align-items-center"><img style = "width:400px;height:auto;" src = "/images/default/not-found.png" ><p>Hiện chưa có bác sĩ nào phù hợp cho dịch vụ hiện tại</p></div > ');
    $("#list_doctor").css('justify-content', 'center');
    $(".navigation").css("display", "none");
    if (serviceId !== '' && healthFacilityId !== '' && serviceId !== null && healthFacilityId !== null) {
        urlData = "doctors/api/ListPagingViewModelFilter?pageIndex=" + 1 + "&pageSize=" + 6 + "&serviceId=" + serviceId + "&healthFacilityId=" + healthFacilityId + "&district=" + district
    }

    var result = await httpService.getAsync(urlData);

    if (result.status == "200") {
        console.log(result.data);
        var content = ``;
        $("#list_doctor").html('');
        $("#list_doctor").css('justify-content', 'unset');
        $(".navigation").css("display", "flex");
        //<a href="/chi-tiet-phong-kham/${item.healthFacilityId}-` + remove_accents(item.healthFacilityName) + `" class="name_healthfacility">${item.healthFacilityName}</a>

        result.data.forEach(function (item) {
            console.log(item);
            var img = item.image ? item.image : "/HomeDoctor/img/list_doctor/img_doctor.png"
            content += `<div class="item_doctor col-sm-12 col-xl-4 col-md-6 col-lg-4 " data-serviceId="` + item.servicesId + `" data-healthFacility="` + item.healthFacilityId + `" data-doctor="` + item.name + `">
                <div class="item_box">
                    <div class="img_doctor">
                        <a href="chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><img src="` + img + `" /></a>
                    </div>
                    <div class="infor_doctor">
                        <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><p class="name_doctor">${item.name}</p></a>
                        <a href="/chi-tiet-bac-si/${item.id}-` + remove_accents(item.name) + `" class="name_healthfacility">${item.healthFacilityName}</a>
                        <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><p class="medical_doctor">${item.specialist}</p></a>
                        <div >
                            <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `" class="btn_call">Chi tiết</a>
                            <a onclick="openModalBooking(${item.id})" class="btn_booking">Đặt lịch</a>
                        </div>
                        <div class="des_doctor">
                            <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><p>Dịch vụ: ${item.serviceName} <br> Giới thiệu: ${item.info}</p></a>
                        </div>
                    </div>
                </div>
            </div>`
        });
        $("#list_doctor").append(content);
    } else {
        $("#list_doctor").html('');
    }
}

async function loadBookingDoctor() {
    var urlData = "doctors/api/getBookingDoctor";
    $(".doctor_booking_container").css("display", "none");
    var result = await httpService.getAsync(urlData);
    console.log(result)
    if (result.status == "200") {
        var content = ``;
        $(".doctor_booking_container").css("display", "block");
        result.data[0].forEach(function (item) {
            /*var img = item.image ? item.image : "/HomeDoctor/img/list_doctor/img_doctor.png"*/
            content += `<div class="item_doctor col-sm-12 col-xl-4 col-md-6 col-lg-4 " data-serviceId="` + item.servicesId + `" data-healthFacility="` + item.healthFacilityId + `" data-doctor="` + item.name + `">
                <div class="item_box">
                    <div class="img_doctor">
                        <a href="chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><img src="` + item.image + `" /></a>
                    </div>
                    <div class="infor_doctor">
                        <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><p class="name_doctor">${item.name}</p></a>
                        <a href="/chi-tiet-bac-si/${item.id}-` + remove_accents(item.name) + `" class="name_healthfacility">${item.healthFacilityName}</a>
                        <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><p class="medical_doctor">${item.specialist}</p></a>
                        <div >
                            <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `" class="btn_call">Chi tiết</a>
                            <a onclick="openModalBooking(${item.id})" class="btn_booking">Đặt lịch</a>
                        </div>
                        <div class="des_doctor">
                            <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><p>Dịch vụ: ${item.serviceName} <br> Giới thiệu: ${item.info}</p></a>
                        </div>
                    </div>
                </div>
            </div>`
        });
        $(".doctorBooking").append(content);
    } else {
        $(".doctorBooking").css("display", "none");
    }
}

async function openModalBooking(id) {
    if (accountId == '0') {
        //Swal.fire({
        //    title: 'Đăng nhập để đặt lịch',
        //    html: "Bạn phải đăng nhập để sử dụng chức năng này",
        //    icon: "warning",
        //    showCancelButton: true,
        //    confirmButtonColor: '#3085d6',
        //    cancelButtonColor: '#443',
        //    confirmButtonText: 'Đăng nhập',
        //}).then((result) => {
        //    if (result.value) {
        //        OpenModalLogin();
        //    }
        //})
        //return;
        OpenModalLogin();
    } else {
        $("#booking").modal("show");
        $('#time').html("");
        $('#time').append(new Option("Chọn thời gian", "", false, false)).trigger('change');
        localStorage.setItem("idDoctor", id);
        var result = await httpService.getAsync("doctors/api/DetailViewmodel/" + id);
        if (result.status == "200") {
            var data = result.data[0];
            $("#doctor-name").val(data.name);
            $("#service").val(data.serviceName);
            healthFacilityAddress = data.healthFacilityAddress;
            accountName = data.accountName;
            // Example usage with 30-minute intervals from "09:00:00" to "18:00:00"
            var intervals = generateTimeIntervals(data.startTime, data.endTime, 30);
            intervals.forEach(interval => $('#time').append(new Option(interval, interval, false, false)).trigger('change'));
        }
        $("#name").val(JSON.parse(localStorage.profile).name);
        $("#phone-number").val(JSON.parse(localStorage.profile).phone);
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
    let timeIntervals = Array.from({length: numIntervals + 1}, (_, index) => startMinutes + index * intervalMinutes);

    // Convert back to hours and minutes in the "HH:mm" format
    let formattedIntervals = timeIntervals.map(interval => {
        let hours = Math.floor(interval / 60);
        let minutes = Math.floor(interval % 60);

        return `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}`;
    });

    return formattedIntervals;
}

async function loadDocOutStanding() {
    var urlData = "doctors/api/get6DoctorOutstanding"
    var content = '';
    var result = await httpService.getAsync(urlData);
    if (result.status == "200") {
        result.data[0].forEach(function (item) {
            var img = item.image ? item.image : "/HomeDoctor/img/list_doctor/img_doctor.png"
            content += ` <a class="dropdown-item" href="chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `">
                        <div class="item_doctor col-sm-12 col-xl-12 col-md-12 col-lg-12 ">
                            <div class="item_box">
                                <div class="img_doctor">
                                    <img src="` + img + `" />
                                </div>
                                <div class="infor_doctor">
                                    <p class="name_doctor">${item.name}</p>
                                    <p class="medical_doctor">${item.specialist}</p>                              
                                </div>
                            </div>
                        </div>
                    </a>`
        });
        $(".dropdown-menu-outstanding-doctors").append(content);
    }
}


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
var healthFacilityAddress = "";

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
    } else {
        bookingDoctor();
    }
}

function ValidatePhone(e) {
    return !!/^[0-9\-\+]{9,15}$/.test(e)
}

function bookingDoctor() {
    var time = $("#time").val();
    var timeBooking = time.split("-");
    var date = $("#day").val().split('/').reverse().join('-');
    var startTime = date + "T" + timeBooking[0] + ":00";
    var endTime = date + "T" + timeBooking[1] + ":00";
    var bookingtypeId = 1000002;
    var doctorId = localStorage.getItem("idDoctor");
    if ($("#349").is(':checked')) {
        bookingtypeId = 1000001;
    }
    var booking = {
        "id": 0,
        "accountId": parseInt(accountId),
        "bookingTypeId": bookingtypeId,
        "bookingStatusId": 1000001,
        "counselorId": parseInt(doctorId),
        "name": $("#name").val(),
        "address": $("#booking-address").val(),
        "URL": $("#phone-number").val(),
        "startTime": startTime,
        "endTime": "",
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
        /*if (booking.id == 0) {*/
        //console.log(booking);
        $("#loading").addClass("show")
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
        } else {
            $("#loading").removeClass("show")
            Swal.fire({
                html: 'Đặt lịch hẹn không thành công, vui lòng thử lại sau !',
                icon: 'error'
            });
        }
    } catch (ex) {
        console.log(ex);
    }
}

$("#select_healthFacility").on("change", function () {
    loadData()
})
$("#select_services").on("change", function () {
    loadData()
})
$("#select_districts").on("change", function () {
    loadData()
})
$("#search_doctor").on("keyup", function () {
    clearTimeout(timeout);

    // Make a new timeout set to go off in 1000ms (1 second)
    timeout = setTimeout(function () {
        loadData()
    }, 500);
})
let timeout = null;

// async function filter() {
//     var urlData = "doctors/api/ListPagingViewModelFilter?pageIndex=" + 1 + "&pageSize=" + 6 + "&serviceId=" + $("#select_services").val() + "&healthFacilityId=" + $("#select_healthFacility").val() + "&keyword=" + $("#search_doctor").val() + "&district=" + $("#select_districts").val();
//     $("#list_doctor").html('<div class="not-found d-flex flex-column align-items-center"><img style = "width:400px;height:auto;" src = "/images/default/not-found.png" ><p>Không tìm thấy kết quả!</p></div > ');
//     $("#list_doctor").css('justify-content', 'center');
//     $(".navigation").css("display", "none");
//     $(".dropdown-menu-outstanding-doctors").removeClass("show");
//     var result = await httpService.getAsync(urlData);
//     if (result.status == "200") {
//         console.log(result.data);
//         var content = ``;
//         $("#list_doctor").html('');
//         $("#list_doctor").css('justify-content', 'unset');
//         $(".navigation").css("display", "flex");
//         result.data.forEach(function (item) {
//             console.log(item);
//             var img = item.image ? item.image : "/HomeDoctor/img/list_doctor/img_doctor.png"
//             content += `<div class="item_doctor col-sm-12 col-xl-4 col-md-6 col-lg-4 " data-serviceId="` + item.servicesId + `" data-healthFacility="` + item.healthFacilityId + `" data-doctor="` + item.name + `">
//                 <div class="item_box">
//                     <div class="img_doctor">
//                         <a href="chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><img src="` + img + `" /></a>
//                     </div>
//                     <div class="infor_doctor">
//                         <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><p class="name_doctor">${item.name}</p></a>
//                         <a href="/chi-tiet-bac-si/${item.id}-` + remove_accents(item.name) + `" class="name_healthfacility">${item.healthFacilityName}</a>
//                         <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><p class="medical_doctor">${item.specialist}</p></a>
//                         <div >
//                             <a href="/chi-tiet-bac-si/${item.id}\` + \`-bac-si-\` + remove_accents(item.name) + \`" class="btn_call">Chi tiết</a>
//                             <a onclick="openModalBooking(${item.id})" class="btn_booking">Đặt lịch</a>
//                         </div>
//                         <div class="des_doctor">
//                             <a href="/chi-tiet-bac-si/${item.id}` + `-bac-si-` + remove_accents(item.name) + `"><p>Dịch vụ: ${item.serviceName} <br> Giới thiệu: ${item.info}</p></a>
//                         </div>
//                     </div>
//                 </div>
//             </div>`
//         });
//         $("#list_doctor").append(content);
//         LoadPagingPage("doctors/api/ListPagingViewModelCount?serviceId=" + $("#select_services").val() + "&healthFacilityId=" + $("#select_healthFacility").val() + "&keyword=" + $("#search_doctor").val() + "&district=" + $("#select_districts").val(), element, loadData);
//     } else {
//         $("#list_doctor").html('');
//     }
// }

function remove_accents(strAccents) {
    var strAccents = strAccents.split('');
    var strAccentsOut = new Array();
    var strAccentsLen = strAccents.length;
    var accents = "ÀÁÂÃÄÅàáâãäåạầÒÓÔÕÕÖØòóôõöợøÈÉÊËèéêëễÇçðÐÌÍÎÏìíîïÙÚÛÜùúûüưÑñŠšŸÿýŽž";
    var accentsOut = "AAAAAAaaaaaaaaOOOOOOOoooooo0EEEEeeeeeCcdDIIIIiiiiUUUUuuuuuNnSsYyyZz";
    for (var y = 0; y < strAccentsLen; y++) {
        if (accents.indexOf(strAccents[y]) != -1) {
            strAccentsOut[y] = accentsOut.substr(accents.indexOf(strAccents[y]), 1);
        } else
            strAccentsOut[y] = strAccents[y];
    }
    strAccentsOut = strAccentsOut.join('').trim().replaceAll(" ", "-").toLowerCase();

    return strAccentsOut;
}

function loadHealthFacility() {
    $('#select_healthFacility').append($('<option>', {
        value: 0,
        text: "Chọn phòng khám"
    }));
    $.ajax({
        url: systemURL + "HealthFacility/api/list",
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        success: function (responseData) {
            responseData.data.forEach(function (item) {
                $('#select_healthFacility').append($('<option>', {
                    value: item.id,
                    text: item.name
                }));
            })
            if (healthFacilityId != null && healthFacilityId != "") {
                $('#select_healthFacility').val(parseInt(healthFacilityId)).trigger("change");
            }
        }
    })
}

function loadService() {
    $('#select_services').append($('<option>', {
        value: 0,
        text: "Chọn dịch vụ"
    }));
    $.ajax({
        url: systemURL + "Services/api/list",
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        success: function (responseData) {
            responseData.data.forEach(function (item) {
                $('#select_services').append($('<option>', {
                    value: item.id,
                    text: item.name
                }));
            })
            if (serviceId != null && serviceId != "") {
                $('#select_services').val(parseInt(serviceId)).trigger("change");
            }
        }
    })

}

async function loadDataDistric(provinceId) {
    try {
        const responseData = await $.ajax({
            url: systemURL + 'district/api/ListByProvinceId/' + provinceId,
            type: 'GET',
            async: true,
            contentType: 'application/json'
        });
        const data = responseData.data;
        for (const item of data) {
            $('#select_districts').append(new Option(item.name, item.id, false, false));
        }
    } catch (error) {
        console.error("Error loading district data:", error);
    }
}

$(document).ready(function () {
    loadHealthFacility();
    loadService();
    let element = $(".pagination_default");
    // LoadPagingPage("doctors/api/count", element, loadData, 6);
    LoadPagingPage("doctors/api/ListPagingViewModelCount?serviceId=" + $("#select_services").val() + "&healthFacilityId=" + $("#select_healthFacility").val() + "&keyword=" + $("#search_doctor").val() + "&district=" + $("#select_districts").val(), element, loadData, 6);

    loadDocOutStanding();
    loadBookingDoctor();
    $('#select_districts').append(new Option("Chọn quận huyện", 0, false, false));
    loadDataDistric(1001);
})