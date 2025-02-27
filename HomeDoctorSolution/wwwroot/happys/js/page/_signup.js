"use strict";
 
var tab;
//TrungHieuTr
var currentTab = 0; // Current tab is set to be the first tab (0)
showTab(currentTab); // Display the current tab
$(document).ready(function () {
    //showTab(currentTab); // Display the current tab
    //loadDataSchool();
    //$("#school").on("change", function () {
    //    var schoolId = $("#school").val();
    //    loadDataClass(schoolId);
    //})
})
/**
 * Author: Cuong
 * Created: 24/10/2023
 * Description: Toggle password
 */

const togglePassword = document.querySelector('#toggle_password');
const toggleConfirmPassword = document.querySelector('#toggle_confirm_password');
const password = document.querySelector('#signup-password');
const confirmpassword = document.querySelector('#confirm-password');

if (togglePassword) {
    togglePassword.addEventListener('click', function (e) {
        const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
        password.setAttribute('type', type);
        this.classList.toggle('bi-eye');
    });
}
if (toggleConfirmPassword) {
    toggleConfirmPassword.addEventListener('click', function (e) {
        const type = confirmpassword.getAttribute('type') === 'password' ? 'text' : 'password';
        confirmpassword.setAttribute('type', type);
        this.classList.toggle('bi-eye');
    });
}
function validForm() {
    const element = $("form input");
    $(element).each(function (index,item) {
        if ($(item).val().trim().length == 0) {
            $(item).addClass("invalid");
            $(item).parent().find(".invalid_text").text($(item)[0].name + " không được để trống").css("display", "block");
            //$(item)[0].name;
        }
    });
}
$("form input").on("keypress", function () {
    $(this).removeClass("invalid");
    $(this).parent().find(".invalid_text").text("").css("display", "none");
});



function showTab(n) {
    // This function will display the specified tab of the form...
    var x = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    //... and fix the Previous/Next buttons:

    //if (n == (x.length - 1)) {
    //    document.getElementById("nextBtn").innerHTML = "Đăng ký";
    //}
    //else {
    //    document.getElementById("nextBtn").innerHTML = "Tiếp theo";
    //}
    //if (n == 0) {
    //    document.getElementById("prevBtn").style.display = "none";
    //    document.getElementById("nextBtn").innerHTML = "Tiếp theo";
    //}
    //else {
    //    document.getElementById("prevBtn").style.display = "inline-flex";
    //}
    //... and run a function that will display the correct step indicator:
    fixStepIndicator(n)
}

function nextPrev(n) {
    var x = document.getElementsByClassName("tab");
    validate();
    if (n == 1 && !validateForm()) return false;
    if (currentTab + n >= x.length) {
        // ... the form gets submitted:
        signUp();
        return false;
    }
    else {
        x[currentTab].style.display = "none";
    }
    currentTab = currentTab + n;
    showTab(currentTab);
}

function validateForm() {
    // This function deals with validation of the form fields
    var x, y, i, valid = true;
    x = document.getElementsByClassName("tab");
    y = x[currentTab].getElementsByTagName("input");
    // A loop that checks every input field in the current tab:
    for (i = 0; i < y.length; i++) {
        // If a field is empty...
        if (y[i].value == "") {
            // add an "invalid" class to the field:
            y[i].className += " invalid";
            // and set the current valid status to false
            valid = false;
        }
    }
    // If the valid status is true, mark the step as finished and valid:
    if (valid) {
        document.getElementsByClassName("step")[currentTab].className += " finish";
    }
    return valid; // return the valid status
}

function fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class on the current step:
    x[n].className += " active";
}
$("#regForm").on("submit", function (e) {
    e.preventDefault();
    signUp();
});
$("#signup-password").keyup(function () {
    var password = $("#signup-password").val();
    if (password.length < 8) {
        $(".at_least_8_characters").css("color", "#BFBFBF");
    }
    else {
        $(".at_least_8_characters").css("color", "#30C069");
    }
    var ch;
    var capitalFlag = false;
    var lowerCaseFlag = false;
    var numberFlag = false;
    var specialFlag = false;
    var format = /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;
    if (password.length > 0) {
        for (var i = 0; i < password.length; i++) {
            ch = password.charAt(i);
            if (!isNaN(ch)) {
                numberFlag = true;
            }
            if (ch == ch.toUpperCase() && isNaN(ch) && !format.test(ch)) {
                capitalFlag = true;
            }
            if (ch == ch.toLowerCase() && isNaN(ch) && !format.test(ch)) {
                lowerCaseFlag = true;
            }
            if (format.test(ch)) {
                specialFlag = true;
            }

        }
    }
    if (capitalFlag == true) {
        $(".contain_upper_character").css("color", "#30C069");
    }
    else {
        $(".contain_upper_character").css("color", "#BFBFBF");

    }

    if (lowerCaseFlag == true) {
        $(".contain_lower_character").css("color", "#30C069");
    }
    else {
        $(".contain_lower_character").css("color", "#BFBFBF");
    }

    if (numberFlag == true) {
        $(".contain_number").css("color", "#30C069");

    }
    else {
        $(".contain_number").css("color", "#BFBFBF");
    }

    if (specialFlag == true) {
        $(".cotain_special_char").css("color", "#30C069");
    }
    else {
        $(".cotain_special_char").css("color", "#BFBFBF");
    }
});
$(document).ready(function () {
    $('.select_formgroup').select2();
    $('b[role="presentation"]').hide();
    $('.select2-selection__arrow').append(`<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                  <path fill-rule="evenodd" clip-rule="evenodd" d="M6.41438 9.53151C6.67313 9.20806 7.1451 9.15562 7.46855 9.41438L12 13.0396L16.5315 9.41438C16.855 9.15562 17.3269 9.20806 17.5857 9.53151C17.8444 9.85495 17.792 10.3269 17.4685 10.5857L12.4685 14.5857C12.1946 14.8048 11.8054 14.8048 11.5315 14.5857L6.53151 10.5857C6.20806 10.3269 6.15562 9.85495 6.41438 9.53151Z" fill="#1B1E28"/>
                </svg>`);
});

function validPassword(password) {
    const passwordCheck = /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;
    if (!password.match(passwordCheck)) {
        return false;
    }
    return true;
}

function validate() {
    let errors = [];
    let swalSubTitle = "<p class='swal__admin__subtitle'>Đăng ký không thành công!</p>";
    var specialCharacterRegex = /[!@#$%^&*(),.?":{}|<>]/;
    if ($("#username").val().trim().length == 0) {
        errors.push("Tài khoản không được để trống");
    }
    if ($("#signup-password").val().length == 0) {
        errors.push("Mật khẩu không được để trống");
    }
    if ($("#email").val().length == 0) {
        errors.push("Email không được để trống");
    }
    if ($("#fullname").val().trim().length == 0) {
        errors.push("Tên không được để trống");
    }
    if ($("#signup-password").val().trim() != $("#confirm-password").val().trim()) {
        errors.push("Xác nhận mật khẩu không trùng khớp. Vui lòng thử lại");
    }
    if (!specialCharacterRegex.test($("#signup-password").val().trim())) {
        errors.push("Mật khẩu phải chứa ít nhất một ký tự đặc biệt.");
    }
    //if ($("#dob").val().trim().length == 0) {
    //    errors.push("Ngày sinh không được để trống");
    //}
    //if (data.middleName.trim().length == 0) {
    //    errors.push("Tên đệm không được để trống");
    //}
    //if (!validPassword(data.password)) {
    //    errors.push("Mật khẩu không hợp lệ, mật khẩu chứa ít nhất 8 ký tự, trong đó có ít nhất một số và bao gồm cả chữ thường và chữ hoa và ký tự đặc biệt");
    //}
    if (errors.length > 0) {
        let contentError = "<ul>";
        errors.forEach(function (item, index) {
            contentError += "<li class='text-start' style= 'text-align:justify;' >" + item + "</li>";
        })
        contentError += "</ul>";
        Swal.fire(
            '' + swalSubTitle,
            contentError,
            'warning'
        );
    }
}
async function signUp() {
    let data = {
        "firstName": $("#fname").val(),
        "lastName": $("#name").val(),
        "middleName": $("#mname").val(),
        "email": $("#email").val(),
        "username": $("#username").val(),
        "password": $("#signup-password").val(),
    };
    var fullName = $("#fullname").val().split(" ");
    if (fullName.length > 0) {
        if (fullName.length > 3) {
            data.firstName = fullName.slice(0, 2).join(' ');
        } else {
            data.firstName = fullName.slice(0, 1).join(' ');
        }

        if (fullName.length > 2) {
            data.lastName = fullName.slice(-2, -1).join(' ');
            data.middleName = fullName.slice(-1).join(' ');
        } else {
            data.lastName = fullName.slice(-1).join(' ');
            data.middleName = "";
        }
    }
    validForm();
    let errors = [];
    let swalSubTitle = "<p class='swal__admin__subtitle'>Đăng ký không thành công!</p>";
 
    if (data.username.trim().length == 0) {
        errors.push("Tài khoản không được để trống");
    }
    if (data.password.trim().length == 0) {
        errors.push("Mật khẩu không được để trống");
    }
    if (data.email.trim().length == 0) {
        errors.push("Email không được để trống");
    }
    if (data.lastName.trim().length == 0) {
        errors.push("Tên không được để trống");
    }
    if (data.firstName.trim().length == 0) {
        errors.push("Họ không được để trống");
    }
    if (data.password.trim() != $("#confirm-password").val().trim()) {
        errors.push("Xác nhận mật khẩu không trùng khớp. Vui lòng thử lại");
    }
    //if ($("#dob").val().trim().length == 0) {
    //    errors.push("Ngày sinh không được để trống");
    //}
    //if (data.middleName.trim().length == 0) {
    //    errors.push("Tên đệm không được để trống");
    //}
    //if (!validPassword(data.password)) {
    //    errors.push("Mật khẩu không hợp lệ, mật khẩu chứa ít nhất 8 ký tự, trong đó có ít nhất một số và bao gồm cả chữ thường và chữ hoa và ký tự đặc biệt");
    //}
    if (errors.length > 0) {
        let contentError = "<ul>";
        errors.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        })
        contentError += "</ul>";
        Swal.fire(
            '' + swalSubTitle,
            contentError,
            'warning'
        );
        return;
    }
    let result = await httpService.postAsync("account/api/register", data);
    if (result.status == "200") {
        Swal.fire("Đăng ký thành công", "Vui lòng kiểm tra email để kích hoạt tài khoản.", "success").then(function () {
            location.href = "/";
        });
    }
    else if (result.status == "202") {
        Swal.fire("Đăng ký không thành công", "Email đã tồn tại. Vui lòng thử lại.", "error");
    }
    else if (result.status == "203") {
        Swal.fire("Đăng ký không thành công", "Tên đăng nhập đã tồn tại. Vui lòng thử lại", "error");
    }
    else if (result.status == "204") {
        Swal.fire("Đăng ký không thành công", "Email nhập không đúng định dạng. Vui lòng thử lại.", "error");
    }
}

//async function loadDataSchool() {
//    let result = await httpService.getAsync("school/api/list");
//    result.data.forEach(function (item) {
//        var newOption = new Option(item.name, item.id, false, false);
//        $('#school').append(newOption).trigger('change');
//        //console.log(schoolId)
//    })
//}

//async function loadDataClass(schoolId) {
//    $('#class')
//        .find('option')
//        .remove()
//        .end()
//    let result = await httpService.getAsync("class/api/list-class-by-schoolId/" + schoolId);
//    result.data.forEach(function (item) {
//        var newOption = new Option(item.name, item.id, false, false);
//        $('#class').append(newOption).trigger('change');
//    })
//}