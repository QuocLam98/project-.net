"use strict";

 
const accessKey = "Authorization";
const params = new URL(document.location).searchParams;
const id = params.get("id");

const load = async () => {
    var result = await httpService.getAsync("healthfacility/api/detail/" + id);
    $(".hfd_profile").html('');
    $('.hfd_info_content').html('')
    $('.hfd_map').html('')
    if (result.status == "200") {
        console.log(result.data)
        result.data.forEach(function (item, index) {
            $('.hfd_profile').append(
                `
                <div class="hfd_profile_img">
                                <img src="`+ item.photo +`" />
                            </div>
                            <div class="hfd_profile_contact w-100">
                                <h4>`+ item.name +`</h4>
                                <div class="hfd_profile_contact_row">
                                    <div class="hfd_profile_contact_input">
                                        <span>Tỉnh thành</span>
                                        <span><b>Hà Nội</b></span>
                                    </div>
                                    <div class="hfd_profile_contact_input">
                                        <span>Hotline</span>
                                        <span><b>`+ item.phone +`</b></span>
                                    </div>
                                </div>
                                <div class="hfd_profile_contact_row">
                                    <div class="hfd_profile_contact_input">
                                        <span>Địa chỉ cụ thể</span>
                                        <span><b>`+ item.addressDetail +`</b></span>
                                    </div>
                                    <div class="hfd_profile_contact_input">
                                        <span>Giờ mở cửa</span>
                                        <span><b>`+ item.openDate +`</b></span>
                                    </div>
                                </div>
                            </div>`
            )
            $('.hfd_info_content').append(`
            <p class="hfd_info_content">`+ item.info +`</p>
            `)
            $('.hfd_map').append(`
            <h4>Bản đồ vị trí</h4>
                    `+ item.linkmap +`
            `)
        })
    }
}

$(document).ready(async function () {
    await load.call();
});