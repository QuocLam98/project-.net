$(document).ready(function () {
    loadDataBooking();
    //loadDataUser();
})    

async function loadDataUser() {
    try {
        var result = await httpService.getAsync("account/api/profile");
        if (result.status = "200") {
            var data = result.data[0];
            $(".accountBooking-name").text(data.name);
            if (data.phone.length > 0) {
                $(".accountBooking-phone").text(' - ' + formatPhoneNumber(data.phone));
            }
            $(".accountBooking-address").text(data.address + ', ' + data.addressWardName + ', ' + data.addressDistrictName + ', ' + data.addressCityName)
        }
        else {
            window.location.href = '/'
        }
    } catch (error) {
        console.error("Error loading accountData", error);
    }
}
var dataBooking;
async function loadDataBooking() {
    try {
        const profile = localStorage.getItem("profile");
        $(".reason").css("display", "none");
        const responseData = await $.ajax({
            url: systemURL + 'booking/api/DetailViewModel/' + bookingId,
            type: 'GET',
            async: true,
            contentType: 'application/json'
        });
        const data = responseData.data;
        dataBooking = data[0];
        console.log(dataBooking);
        if (accountId === dataBooking.accountId.toString()) {
            //data 
            $(".accountBooking-name").text(dataBooking.name);
            if (dataBooking.url != null) {
                $(".accountBooking-phone").text(formatPhoneNumber(dataBooking.url));
            }
            //$(".accountBooking-address").text(data.address + ', ' + data.addressWardName + ', ' + data.addressDistrictName + ', ' + data.addressCityName)
            $(".accountBooking-address").text(dataBooking.address)
            // data booking
            $(".booking-detail-address-info").text(dataBooking.address);

            $(".booking-detail-name").text(dataBooking.counselorName);
            $(".booking-detail-avatar img").attr("src", dataBooking.photo);
            $(".booking-detail-time-info-top").text(timeSince(dataBooking.startTime) + ' - ' + formatDate(dataBooking.startTime));
            $(".booking-detail-time-info").text(timeSince(dataBooking.startTime) + ' giờ, ngày ' + formatDate(dataBooking.startTime));
            $(".booking-detail-reason-detail").text(dataBooking.serviceName);
            if (dataBooking.bookingTypeId == 1000001) {
                $(".booking-detail-type").html('<span class="label label-success">' + dataBooking.bookingTypeName + '</span>');
            }
            else {
                $(".booking-detail-type").html('<span class="label label-primary">' + dataBooking.bookingTypeName + '</span>');
            }
            if (dataBooking.bookingStatusId == 1000001) {
                $(".booking-detail-status").html('<span class="label label-warning">' + dataBooking.bookingStatusName + '</span>');
            }
            else if (dataBooking.bookingStatusId == 1000002) {
                $(".booking-detail-status").html('<span class="label label-primary">' + dataBooking.bookingStatusName + '</span>');
            }
            else if (dataBooking.bookingStatusId == 1000003) {
                $(".booking-detail-status").html('<span class="label label-success">' + dataBooking.bookingStatusName + '</span>');
            }
            else if (dataBooking.bookingStatusId == 1000004) {
                $(".booking-detail-status").html('<span class="label label-danger">' + dataBooking.bookingStatusName + '</span>');
            }
            else {
                $(".booking-detail-status").html('<span class="label label-default">' + dataBooking.bookingStatusName + '</span>');
            }
            if (dataBooking.reason != "") {
                $(".reason").css("display", "block");
                $(".booking-detail-reason").html(dataBooking.serviceName);
            }
            if (dataBooking.info != null) {
                $(".booking-detail-info-content").html(dataBooking.info.length > 0 ? dataBooking.info : "Không có ghi chú");
            } else {
                $(".booking-detail-info-content").html("Không có ghi chú");
            }
            $(".doctor-evaluate").html("Hiện chưa có đánh giá nào từ tư vấn viên !");
            $(".booking-detail-price-value").html(dataBooking.price)
            formatNumber();
        }
        else {
            if (accountId == "0") {
                OpenModalLogin();
            }
            else {
                window.location.href = "/";
            }
        }
    } catch (error) {
        console.error("Error loading account information:", error);
    }
}
function PayRequest() {
    Swal.fire({
        title: "Dịch vụ đang tạm ngưng",
        text: "Dịch vụ này đang được cập nhật!",
        icon: "warning"
    });
}
function formatNumber() {
    $('.column-price , .column-openingPrice , .column-value , .column-amount, .column-available, .column-balance1, .price-format').each(function (event) {
        // format number
        $(this).text(function (index, value) {
            return value
                .replace(/\D/g, '')
                .replace(/\B(?=(\d{3})+(?!\d))/g, '.');
        });
    });
}
function timeSince(dateTime) {
    const currentTime = new Date();
    const timeDifference = (currentTime - new Date(dateTime)) / 1000;
    return formatTime(new Date(dateTime));
}
function formatTime(date) {
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return `${hours}:${minutes}`;
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
}
function formatPhoneNumber(phoneNumber) {
    // Check if the phone number is a valid 10-digit number
    if (!/^\d{10}$/.test(phoneNumber)) {
        throw new Error("Invalid phone number format. Please enter a valid 10-digit phone number.");
    }

    // Split the phone number into three parts and join them with dots
    return `${phoneNumber.slice(0, 4)}.${phoneNumber.slice(4, 7)}.${phoneNumber.slice(7)}`;
}
