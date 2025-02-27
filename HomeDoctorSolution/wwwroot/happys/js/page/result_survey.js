const bg_normal = "rgba(54, 161, 0, 0.10)";
const bg_light = "rgba(255, 193, 51, 0.20)";
const bg_fit = "rgba(255, 193, 51, 0.20)";
const bg_heavy = "rgba(255, 0, 0, 0.20)";
const bg_weighty = "rgba(255, 0, 0, 0.20)";
   
function removeVietnameseTones(str) {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D");
    // Some system encode vietnamese combining accent as individual utf-8 characters
    // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
    str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
    str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
    // Remove extra spaces
    // Bỏ các khoảng trắng liền nhau
    str = str.replace(/ + /g, " ");
    str = str.trim();
    // Remove punctuations
    // Bỏ dấu câu, kí tự đặc biệt
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, " ");
    return str;
}
async function loadResultSurvey() {

    let resultApi = await httpService.getAsync("surveyAccount/api/ListBySurveyAccountId/" + surveyAccountId);
    let resultSurvey = resultApi.data[0];

    let maxScore = -Infinity; 
    for (const section of resultSurvey.surveySectionAccounts) {
        if (section.score > maxScore) {
            maxScore = section.score;
        }

        if (removeVietnameseTones("Lo Âu") == removeVietnameseTones(section.name)) {
            $(".mark_concerned").each(function (index, item) {
                if ($(item).attr("data-value") != section.score) {
                    $(item).addClass("mark_active");
                }
            })
        }

        if (removeVietnameseTones("Trầm Cảm") == removeVietnameseTones(section.name)) {
            $(".mark_depression").each(function (index, item) {
                if ($(item).attr("data-value") != section.score) {
                    $(item).addClass("mark_active");
                }
            })
        }

        if (removeVietnameseTones("Stress") == removeVietnameseTones(section.name)) {
            $(".mark_stress").each(function (index, item) {
                if ($(item).attr("data-value") != section.score) {
                    $(item).addClass("mark_active");
                }
            })
        }
    }

    switch (maxScore) {
        case 1:
            $("#noti_result").css('background-color', bg_normal);
            break;
        case 2:
            $("#noti_result").css('background-color', bg_light);
            break;
        case 3:
            $("#noti_result").css('background-color', bg_fit);
            break;
        case 4:
            $("#noti_result").css('background-color', bg_heavy);
            break;
        case 5:
            $("#noti_result").css('background-color', bg_weighty);
            break;  
    }
    var detail_survey = $("#detail_surey");
    var newRow = `
        <div class="btn_survey ">
            <p id="result_massage">`+ resultSurvey.text +`</p>
        </div>
       `;
    detail_survey.append(newRow);

}
var emailString = "";
$("#addEmail").on("click", function () {
    validateEmail();
});
$("#validate_email").on("keydown", function (event) {
    if (event.key === "Enter") {
        event.preventDefault(); // Ngăn chặn hành động mặc định của phím Enter trên trường nhập liệu
        validateEmail(); // Gọi hàm xử lý khi nhấn Enter
    }
});
function validateEmail() {
    var email = $("#validate_email").val();

    var gmailRegex = /^[a-zA-Z0-9._-]+@gmail\.com$/;

    if (gmailRegex.test(email)) {
        let content = `
            <span class="email_item" id="email_list">${email}<span class="close_email"><i class="bi bi-x"></i></span></span>
        `;
        $("#list_email").append(content);

        // Thêm email vào chuỗi, cách nhau bằng dấu phẩy
        emailString += email + ",";

        $("#validate_email").val("");
        $("#email_error").text(""); // Xóa thông báo lỗi nếu có
    } else {
        $("#email_error").text("Vui lòng nhập một địa chỉ email Gmail hợp lệ.");
    }
}

$(document).on("click", ".close_email", function (e) {
    var emailToRemove = $(this).closest('.email_item').text().trim();

    // Xóa email từ chuỗi
    emailString = emailString.replace(emailToRemove + ",", "");

    $(this).closest('.email_item').remove();
})
$(document).on("click", "#btnConfirmEmail", async function (e) {
    if (emailString.endsWith(',')) {
        emailString = emailString.slice(0, -1);
    }
    if (emailString == "" || emailString == null) {
        Swal.fire({
            title: "Vui lòng nhập email!!",
            icon: "error"
        });
    }
    else {
        await $.ajax({
            url: systemURL + "survey/api/send-email-result-survey" + "?surveyAccountId=" + surveyAccountId + "&emailString=" + emailString,
            method: "GET",
            success: function (responseData) {
                Swal.fire({
                    text: "Chia sẻ thành công!!!",
                    icon: "success"
                });
            },
            error: function (e) {
            },
        });
        $('#exampleModal').modal('hide');
        $("#list_email").html("");
    }
    
})
$(document).ready(async function () {
    await loadResultSurvey();
});



