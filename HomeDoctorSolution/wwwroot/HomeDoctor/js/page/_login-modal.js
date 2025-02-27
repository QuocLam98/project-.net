"use strict";
/**
 * Author: TUNGTD
 * Created: 09/08/2023
 * Description: Login page javascript
 */
const accessKey = "Authorization";

/**
 * Author: Cuong
 * Created: 24/10/2023
 * Description: Toggle password
 */
const togglePassword = document.querySelector('#toggle_password');
const password = document.querySelector('#password');
if (togglePassword) {
    togglePassword.addEventListener('click', function (e) {
        const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
        password.setAttribute('type', type);
        this.classList.toggle('bi-eye');
    });
}

/**
 * Author: TUNGTD
 * Created: 09/08/2023
 * Description: submit login
 */
$("#loginForm").on("submit", function (e) {
    e.preventDefault();
    signIn();
});

async function signIn() {
    try {
        let checked = $("#check_remember_password").is(":checked");
        if (checked) {
            localStorage.setItem("usname", $("#username").val());
            localStorage.setItem("pass", $("#password").val());
            localStorage.setItem("remember_account", checked);
        }
        else {
            localStorage.removeItem("usname");
            localStorage.removeItem("pass");
            localStorage.setItem("remember_account", checked);
        }
        let data = {
            'username': $("#username").val(),
            "password": $("#password").val()
        };
        let errors = [];
        let swalSubTitle = "<p class='swal__admin__subtitle'>Đăng nhập không thành công!</p>";
        if (data.username.trim().length == 0) {
            errors.push("Tài khoản không được để trống");
        }
        if (data.password.trim().length == 0) {
            errors.push("Mật khẩu không được để trống");
        }
        if (errors.length > 0) {
            let contentError = "<ul>";
            errors.forEach(function (item, index) {
                contentError += "<li class='text-start'>" + item + "</li>";
            })
            contentError += "</ul>";
            Swal.fire(
                'Đăng nhập' + swalSubTitle,
                contentError,
                'warning',
            );
            return;
        }

        let result = await httpService.postAsync("account/api/login", data);
        if (result.status == "200") {
            let token = result.resources.accessToken;
            let profile = result.resources.profile;
            let countBookingWait = result.resources.countBookingWait;

            localStorage.setItem("token", token);
            localStorage.setItem("profile", JSON.stringify(profile));
            localStorage.setItem("countBookingWait", countBookingWait);
            document.cookie = `${accessKey}=${token}`;
            Swal.fire("Đăng nhập thành công", "Chào mừng <b>" + data.username + "</b> trở lại.", "success").then(function () {
                window.location.reload();
            });
        }
        else {
            if (result.status == "403") {
                Swal.fire(
                    'Đăng nhập' + swalSubTitle,
                    result.resources,
                    'warning'
                );
            }
            else {
                Swal.fire(
                    "Đăng nhập",
                    result.errors[0],
                    "error");
            }
        }
    } catch (e) {
        Swal.fire(
            "Đăng nhập thất bại",
            "Tài khoản hoặc mật khẩu không chính xác. <br>Vui lòng thử lại sau!",
            "error");
        console.error(e);
    }
}
$("#btnLogin").on("click", function (e) {
    e.preventDefault();
    signIn();
})
//$("#modal-signin").on('keyup', function (e) {
//    let key = e.which;
//    if (key == 13) {
//        signIn();
//    }
//});

$("#loginForm").on("input change keypress keydown", "input", function (e) {
    let text = $(this).val().trim();
    $(this).val(text);
    if (e.which == 13) {
        signIn();
    }
})
$(".none-space").on("change input blur", function () {
    let e = $(this);
    let text = e.val().trim();
    e.val(text);
})
$(".btn_show_pass").on("click", function (e) {
    var target = $($(this).attr("data-target"));
    if (target.attr("type") == "password") {
        target.attr("type", "text");
        $(this).html(`<i class="ki-duotone ki-eye-slash fs-3">
                                            <span class="path1 ki-uniEC07"></span>
                                            <span class="path2 ki-uniEC08"></span>
                                            <span class="path3 ki-uniEC09"></span>
                                            <span class="path4 ki-uniEC0A"></span>
                                        </i>`);
    }
    else {
        target.attr("type", "password");
        $(this).html(`<i class="ki-duotone ki-eye fs-3">
                                            <span class="path1 ki-uniEC0B"></span>
                                            <span class="path2 ki-uniEC0B"></span>
                                            <span class="path3 ki-uniEC0D"></span>
                                        </i>`);
    }
});
//login google
google.accounts.id.initialize({
    client_id: '50294875820-f2opokj2gljfet85hisv53k85e3or4n7.apps.googleusercontent.com',
    callback: handleCredentialResponse,
    ux_mode: "popup",

});
google.accounts.id.prompt();
const createFakeGoogleWrapper = () => {
    const googleLoginWrapper = document.createElement("div");
    googleLoginWrapper.style.display = "none";
    googleLoginWrapper.classList.add("custom-google-button");

    // Add the wrapper to body
    document.body.appendChild(googleLoginWrapper);

    // Use GSI javascript api to render the button inside our wrapper
    // You can ignore the properties because this button will not appear
    google.accounts.id.renderButton(googleLoginWrapper, {
        theme: 'outline',
    });

    const googleLoginWrapperButton =
        googleLoginWrapper.querySelector("div[role=button]");

    return {
        click: () => {
            googleLoginWrapperButton.click();
        },
    };
}

const googleButtonWrapper = createFakeGoogleWrapper();
$("#login-width-google").on("click", function (e) {
    e.preventDefault();
    googleButtonWrapper.click()
})

function handleCredentialResponse(response) {
    // decodeJwtResponse() is a custom function defined by you
    // to decode the credential response.
    if (response == null || response == undefined) {
        Swal.fire(
            'Đăng nhập thất bại',
            'Vui lòng kiểm tra lại thông tin đăng nhập!',
            'error'
        );
    }
    const responsePayload = decodeJwtResponse(response.credential);

    var obj = {
        id: responsePayload.sub,
        fullName: responsePayload.name,
        photo: responsePayload.picture,
        email: responsePayload.email,
    }

    loginWithGoogle(obj);
}

async function loginWithGoogle(obj) {
    $.ajax({
        url: systemURL + 'account/api/sign-in-google',
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function (responseData) {
            console.log(responseData);
            if (responseData.status == 200 && responseData.message == "SUCCESS") {
                if (responseData.data != null) {
                    localStorage.setItem("profile", JSON.stringify(responseData.data[0]));
                    localStorage.setItem("token", responseData.data[0].token);
                    Swal.fire({
                        title: 'Đăng nhập thành công',
                        html: `Chào mừng <b>${responseData.data[0].name ? responseData.data[0].name : ""}</b> quay trở lại.`,
                        icon: 'success',
                        focusConfirm: true,
                        allowEnterKey: true,
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    }).then(() => {
                        window.location.reload();
                    })
                } else {
                    localStorage.setItem("profile", JSON.stringify(responseData.resources));
                    localStorage.setItem("token", responseData.resources.token);
                    Swal.fire({
                        title: 'Đăng nhập thành công',
                        html: `Chào mừng <b>${responseData.resources.name ? responseData.resources.name : ""}</b> quay trở lại.`,
                        icon: 'success',
                        focusConfirm: true,
                        allowEnterKey: true,
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    }).then(() => {
                        window.location.reload();
                    })
                }
            }
            else if (responseData.status == 400 && responseData.message === "BAD_REQUEST") {
                Swal.fire({
                    title: 'Đăng nhập thất bại',
                    html: responseData.title,
                    icon: 'warning',
                    focusConfirm: true,
                    allowEnterKey: true,
                    showCancelButton: false,
                    confirmButtonText: 'OK'
                })
            }
        },
        error: function (e) {
            //console.log(e);
            Swal.fire(
                'Đăng nhập thất bại',
                'Vui lòng thử lại!',
                'error'
            );
        }
    })
}

//login facebook:

$("#login-width-facebook").on("click", function (e) {
    e.preventDefault();
    FB.login(function (response) {
        // handle the response
        if (response.status === 'connected') {
            // Logged into your webpage and Facebook.
            FB.api('/me', { locale: 'en_US', fields: 'id,first_name,middle_name,last_name,email,birthday,gender,picture,name' }, function (response) {
                console.log(response.picture.data.url);
                if (response && !response.error) {
                    /* handle the result */
                    var facebookAccount = {
                        "id": response.id,
                        "fullname": response.name,
                        "email": response.email || response.id,
                        "photo": response.picture.data.url,
                    };
                    //console.log(facebookAccount, );
                    loginWithFacebook(facebookAccount);
                } else {
                    Swal.fire(
                        'Đăng nhập thất bại',
                        'Vui lòng thử lại!',
                        'error'
                    );
                }


            });
        } else {
            // The person is not logged into your webpage or we are unable to tell.

        }

    }, { scope: 'public_profile,email' });
})

function loginWithFacebook(obj) {
    $.ajax({
        url: systemURL + 'account/api/sign-in-facebook',
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function (responseData) {
            if (responseData.status == 200 && responseData.message == "SUCCESS") {
                if (responseData.data != null) {
                    localStorage.setItem("profile", JSON.stringify(responseData.data[0]));
                    localStorage.setItem("token", responseData.data[0].token);
                    Swal.fire({
                        title: 'Đăng nhập thành công',
                        html: `Chào mừng <b>${responseData.data[0].name ? responseData.data[0].name : ""}</b> quay trở lại.`,
                        icon: 'success',
                        focusConfirm: true,
                        allowEnterKey: true,
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    }).then(() => {
                        window.location.reload();

                    })
                } else {
                    localStorage.setItem("profile", JSON.stringify(responseData.resources));
                    localStorage.setItem("token", responseData.resources.token);
                    Swal.fire({
                        title: 'Đăng nhập thành công',
                        html: `Chào mừng <b>${responseData.resources.name ? responseData.resources.name : ""}</b> quay trở lại.`,
                        icon: 'success',
                        focusConfirm: true,
                        allowEnterKey: true,
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    }).then(() => {
                        window.location.reload();

                    })
                }
            }
            else if (responseData.status == 400 && responseData.message === "BAD_REQUEST") {
                Swal.fire({
                    title: 'Đăng nhập thất bại',
                    html: responseData.title,
                    icon: 'warning',
                    focusConfirm: true,
                    allowEnterKey: true,
                    showCancelButton: false,
                    confirmButtonText: 'OK'
                })
            }
        },
        error: function (e) {
            //console.log(e);
            Swal.fire(
                'Đăng nhập thất bại',
                'Vui lòng kiểm tra lại thông tin đăng nhập!',
                'error'
            );
        }
    })
}

function decodeJwtResponse(token) {
    var base64Url = token.split(".")[1];
    var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    var jsonPayload = decodeURIComponent(
        atob(base64)
            .split("")
            .map(function (c) {
                return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
            })
            .join("")
    );

    return JSON.parse(jsonPayload);
}

$("#password").on("keypress", function (e) {
    if (e.which === 13) {
        e.preventDefault(); 
        signIn(); 
    }
});