"use strict";
let dataBanner1 = JSON.parse(localStorage.profile);
$("#avatar-children").attr("src", dataBanner1.photo ? dataBanner1.photo : "/images/default/authdefaultimage.png");
$("#avatar-big").attr("src", dataBanner1.photo ? dataBanner1.photo : "/images/default/authdefaultimage.png");
$("#avatar").on("change", function (e) {
    let file = e.target.files[0];
     uploadFile(file);
});
const uploadFile = async (file) => {
    var formData = new FormData();
    formData.append("file", file);
    axios.post("account/api/ChangeAvatar", formData, {
        headers: {
            "Content-Type": "multipart/form-data",
            "Authorization": `Bearer ${localStorage.token}`
        },
    }).then((response) => {
        fnSuccess(response);
    }).catch((error) => {
        fnFail(error);
    });
}
const fnSuccess = (response) => {
    $("#avatar-children").attr("src", response.data.data[0]);
    $("#avatar-big").attr("src", response.data.data[0]);
    Swal.fire("Cập nhật thành công", "Cập nhật ảnh đại diện thành công.", "success");
    let dataBanner = JSON.parse(localStorage.profile);
    dataBanner.photo = response.data.data[0];
    localStorage.setItem("profile", JSON.stringify(dataBanner));
    $("#account_avatar").attr('src', response.data.data[0])
}
const fnFail = (error) => {
    Swal.fire("Cập nhật thất bại", "Cập nhật ảnh đại diện không thành công.", error);
}