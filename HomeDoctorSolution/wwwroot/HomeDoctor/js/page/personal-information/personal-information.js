$(document).ready(async function () {
    $("#select_province").on("select2:select", async function (e) {
        var id = $(this).val();
        $("#select_district").html("");
        $("#select_wards").html("");
        await loadDataSelectshipDistrictAddressId(id);
    })
    $("#select_district").on("select2:select", async function (e) {
        var id = $(this).val();
        $("#select_wards").html("");
        await loadDataSelectshipWardAddressId(id);
    })
    $('#select_district').append(new Option("Quận huyện", 0, false, false));
    $('#select_wards').append(new Option("Phường xã", 0, false, false));
    $("#select_sex").select2();
    $("#select_province").select2();
    $("#select_district").select2();
    $("#select_wards").select2();
    $('b[role="presentation"]').hide()
    $('.select2-selection__arrow').append(`<svg xmlns="http://www.w3.org/2000/svg" width="14" height="8" viewBox="0 0 14 8" fill="none">
                  <path fill-rule="evenodd" clip-rule="evenodd" d="M0.200091 0.994039C0.515071 0.600314 1.08959 0.536479 1.48331 0.851458L6.99938 5.26431L12.5155 0.851458C12.9092 0.536479 13.4837 0.600314 13.7987 0.994039C14.1137 1.38776 14.0498 1.96228 13.6561 2.27726L7.5697 7.14637C7.23627 7.41312 6.76249 7.41312 6.42906 7.14637L0.342671 2.27726C-0.0510528 1.96228 -0.114888 1.38776 0.200091 0.994039Z" fill="#F7AC00"/>
                </svg>`);
    await loadAccountInformation();
})

async function loadDataSelectshipDistrictAddressId(provinceId) {
    try {
        const responseData = await $.ajax({
            url: systemURL + 'district/api/ListByProvinceId/' + provinceId,
            type: 'GET',
            async: true,
            contentType: 'application/json'
        });
        const data = responseData.data;
        for (const item of data) {
            $('#select_district').append(new Option(item.name, item.id, false, false));
        }
    } catch (error) {
        console.error("Error loading district data:", error);
    }
}

async function loadDataSelectshipWardAddressId(districtId) {
    try {
        const responseData = await $.ajax({
            url: systemURL + 'ward/api/ListByDistrictId/' + districtId,
            type: 'GET',
            async: true,
            contentType: 'application/json'
        });
        const data = responseData.data;
        for (const item of data) {
            $('#select_wards').append(new Option(item.name, item.id, false, false));
        }
    } catch (error) {
        console.error("Error loading ward data:", error);
    }
}

var dataAccount;

async function loadAccountInformation() {
    try {
        const profile = localStorage.getItem("profile");
        const responseData = await $.ajax({
            url: systemURL + 'account/api/detail/' + idAccount,
            type: 'GET',
            async: true,
            contentType: 'application/json'
        });
        const data = responseData.data;
        dataAccount = data[0];
        $("#account_name").html(data[0].username);
        $("#account_avatar").attr("src", data[0].photo ? data[0].photo : "/images/default/authdefaultimage.png");
        $("#profile_name").val(data[0].name);
        $("#profile_email").val(data[0].email);
        $("#profile_phone").val(data[0].phone);
        $("#dob").val(moment(data[0].dob).format("DD/MM/YYYY"));
        $("#profile_address").val(data[0].address);
        if (data[0].gender != null) {
            $("#select_sex").val(data[0].gender).trigger("change");
        }
        if (data[0].addressCity != null) {
            await loadDataSelectshipDistrictAddressId(data[0].addressCity);
            $("#select_province").val(data[0].addressCity).trigger("change");
            if (data[0].addressDistrict != null) {
                await loadDataSelectshipWardAddressId(data[0].addressDistrict);
                $("#select_district").val(data[0].addressDistrict).trigger("change");
                if (data[0].addressWard != null) {
                    $("#select_wards").val(data[0].addressWard).trigger("change");

                }
            }
        }
    } catch (error) {
        console.error("Error loading account information:", error);
    }
}


function formatDatetimeUpdate(dateStr) {
    if (!dateStr) return null;

    var [date, time] = dateStr.split(" ");
    if (!time) time = "00:00:00";

    var [day, month, year] = date.split("/");
    var localISOTime = year + "-" + month + "-" + day + "T" + time;
    return localISOTime;
}

function validate() {
    var phoneValid = /(84|0[3|5|7|8|9])+([0-9]{8})\b/;
    var errorList = [];

    if ($("#profile_phone").val().length != 10 && $("#profile_phone").val().length != 0 || !phoneValid) {
        errorList.push("Số điện thoại không hợp lệ");
    }
    if ($("#profile_name").val().length == 0) {
        errorList.push("Họ và tên không được để trống.");
    }
    if ($("#dob").val().length == 0) {
        errorList.push("Ngày sinh không được để trống.");
    }
    if (errorList.length > 0) {
        var contentError = "<ul>";
        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        })
        contentError += "</ul>";
        var actionName = (idAccount > 0 ? "Cập nhật" : "Thêm mới");
        var swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName + " không thành công</p>";
        Swal.fire(
            'Cập nhật tài khoản' + swalSubTitle,
            contentError,
            'warning'
        );
    } else {
        updateItem(idAccount);
    }
}

function updateItem(id) {
    var updateItem = {
        "id": id,
        "address": $("#profile_address").val(),
        "gender": $("#select_sex").val(),
        "addressCity": $("#select_province").val(),
        "addressDistrict": $("#select_district").val(),
        "addressWard": $("#select_wards").val(),
        "name": $("#profile_name").val(),
        "phone": $("#profile_phone").val(),
        "active": dataAccount.active,
        "isActivated": dataAccount.isActivated,
        "info": dataAccount.info,
        "photo": dataAccount.photo,
        "firstName": dataAccount.firstName,
        "lastName": dataAccount.lastName,
        "email": $("#profile_email").val(),
        "username": dataAccount.username,
        "password": dataAccount.password,
        "classId": dataAccount.classId,
        "schoolId": dataAccount.schoolId,
        "dob": formatDatetimeUpdate($("#dob").val()),
        "idCardNumber": dataAccount.idCardNumber,
        "roleId": dataAccount.roleId,
        "accountTypeId": dataAccount.accountTypeId,
        "accountStatusId": dataAccount.accountStatusId,
        "createdTime": dataAccount.createdTime,
    };
    Swal.fire({
        title: "Cập nhật thông tin tài khoản",
        text: "Bạn có muốn cập nhật thông tin không?",
        icon: "info",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Xác nhận"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: systemURL + "account/api/update",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(updateItem),
                success: function (responseData) {
                    Swal.fire({
                        title: "Cập nhật tài khoản thành công!",
                        text: "Thông tin của bạn đã được cập nhật.",
                        icon: "success"
                    });
                },
                error: function (e) {
                    console.log(e)
                    let errorMessage = '';

                    e.responseJSON.forEach(error => {
                        errorMessage += error.message + '<br>';
                    });

                    Swal.fire({
                        title: "Cập nhật tài khoản không thành công!",
                        html: errorMessage,
                        icon: "error"
                    });
                }
            })
        }
    });
}

$("#btn-update").click(function () {
    validate();
})