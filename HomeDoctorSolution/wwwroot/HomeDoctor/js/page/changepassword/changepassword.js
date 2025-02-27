function checkPasswork(confirmnewpassword) {
    if (confirmnewpassword.toString().trim() == $("#fw-password").val().toString().trim()) {
        return true;
    }
    return false;
}

function validPassword(password) {
    //const passwordCheck = /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;
    //if (!password.match(passwordCheck)) {
    //    return false;
    //}
    //return true;
    if (password.length >= 6) {
        return true;
    }
    return false;
}

function nextPrev() {
    // Exit the function if any field in the current tab is invalid:
    if (!validateForm()) {
        validate();
        return false;
    }
    else {
        changePassword();
    }

}

async function changePassword() {
    let data = {
        "password": $("#old-password").val(),
        "newPassword": $("#fw-password").val(),
    };
    var result = await httpService.postAsync("account/api/change-password", data);
    if (result.status == "200") {
        Swal.fire({
            icon: 'success',
            title: 'Đổi mật khẩu thành công',
            html: 'Vui lòng đăng nhập để sử dụng hệ thống',
            showCloseButton: false,
            focusConfirm: true,
            confirmButtonText: 'Đồng ý',
        }).then(async (res) => {
            if (res.isConfirmed) {
                await httpService.postAsync("account/api/logout");
                window.location.href = "/";
            }
            
        })
    }
    if (result.status == "400") {
        Swal.fire({
            icon: 'warning',
            title: 'Đổi mật khẩu không thành công',
            html: result.data[0] != '' ? result.data[0] : 'Vui lòng kiểm tra lại thông tin của bạn',
            showCloseButton: false,
            focusConfirm: true,
            confirmButtonText: 'Ok',
        })
    }
}
function validate() {
    let errors = [];
    let swalSubTitle = "<p class='swal__admin__subtitle'>Đổi mật khẩu không thành công!</p>";
    var specialCharacterRegex = /[!@#$%^&*(),.?":{}|<>]/;
    if ($("#old-password").val().length == 0) {
        errors.push("Mật khẩu cũ không được để trống");
    }
    if (!specialCharacterRegex.test($("#old-password").val().trim())) {
        errors.push("Mật khẩu cũ phải chứa ít nhất một ký tự đặc biệt.");
    }
    if ($("#fw-password").val().length == 0) {
        errors.push("Mật khẩu mới không được để trống");
    }
    if (!specialCharacterRegex.test($("#fw-password").val().trim())) {
        errors.push("Mật khẩu mới phải chứa ít nhất một ký tự đặc biệt.");
    }
    if ($("#fw-password").val().trim() != $("#confirm-password").val().trim()) {
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

    listErrors = errors;

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
    else {
        changePassword() 
    }
}
const togglePassword = document.querySelector('#toggle_password');
const toggleOldPassword = document.querySelector('#toggle_old_password');
const toggleConfirmPassword = document.querySelector('#toggle_confirm_password');
const password = document.querySelector('#fw-password');
const oldPassword = document.querySelector('#old-password');
const confirmpassword = document.querySelector('#confirm-password');

if (toggleOldPassword) {
    toggleOldPassword.addEventListener('click', function (e) {
        const type = oldPassword.getAttribute('type') === 'password' ? 'text' : 'password';
        oldPassword.setAttribute('type', type);
        this.classList.toggle('bi-eye');
    });
}
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