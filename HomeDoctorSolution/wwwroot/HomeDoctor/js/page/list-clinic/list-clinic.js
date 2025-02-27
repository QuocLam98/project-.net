var loadData = async function () {
    var provinceId = $("#select-province").val();
    var keyword = $("#search_clinic_page").val();
    $('#list_clinic').html('<div class="not-found d-flex flex-column align-items-center"><img style="width:400px;height:auto;" src="/images/default/not-found.png"><p>Không tìm thấy kết quả!</p></div>');

    var result = await httpService.getAsync("HealthFacility/api/ListPagingView?pageIndex=" + 1 + "&pageSize=" + 12 + "&provinceId=" + provinceId + "&keyword=" + keyword);
    if (result.data == null) {
    }
    else {
        var content = '';
        result.data.forEach(function (item) {
            var itemRemoveVietNameese = remove_accents(item.name);
            var linkDetail = "chi-tiet-phong-kham/" + item.id + "-" + itemRemoveVietNameese;
            content += `<div class="item_clinic col-lg-3 col-sm-6">
                <div class="item_box">
                    <div class="img_clinic">
                    <a href="chi-tiet-phong-kham/${item.id}-${itemRemoveVietNameese}"><img src="` + item.photo +`" /></a>
                    </div>
                    <div class="infor_clinic">
                    <div class="name_clinic"><p><a href="chi-tiet-phong-kham/${item.id}-${itemRemoveVietNameese}">` + item.name + `</a></p></div>
                   
                        <div class="address_clinic">
                            <div class="title_address" ">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16" fill="none">
                                    <g clip-path="url(#clip0_762_4724)">
                                        <path d="M8 0C6.40935 0.00211004 4.88445 0.634929 3.75969 1.75969C2.63493 2.88445 2.00211 4.40935 2 6C2 10.3075 7.59 15.7025 7.8275 15.93C7.8737 15.9749 7.93558 16 8 16C8.06442 16 8.1263 15.9749 8.1725 15.93C8.41 15.7025 14 10.3075 14 6C13.9979 4.40935 13.3651 2.88445 12.2403 1.75969C11.1155 0.634929 9.59065 0.00211004 8 0ZM8 8.75C7.4561 8.75 6.92442 8.58871 6.47218 8.28654C6.01995 7.98437 5.66747 7.55488 5.45933 7.05238C5.25119 6.54988 5.19673 5.99695 5.30284 5.4635C5.40895 4.93005 5.67086 4.44005 6.05546 4.05546C6.44005 3.67086 6.93005 3.40895 7.4635 3.30284C7.99695 3.19673 8.54988 3.25119 9.05238 3.45933C9.55488 3.66747 9.98437 4.01995 10.2865 4.47218C10.5887 4.92442 10.75 5.4561 10.75 6C10.7496 6.72921 10.4597 7.42843 9.94406 7.94406C9.42843 8.45969 8.72921 8.74956 8 8.75Z" fill="url(#paint0_linear_762_4724)" fill-opacity="0.97" />
                                    </g>
                                    <defs>
                                        <linearGradient id="paint0_linear_762_4724" x1="8" y1="0" x2="8" y2="16" gradientUnits="userSpaceOnUse">
                                            <stop stop-color="#FF0019" />
                                            <stop offset="1" stop-color="#F57314" />
                                        </linearGradient>
                                        <clipPath id="clip0_762_4724">
                                            <rect width="16" height="16" fill="white" />
                                        </clipPath>
                                    </defs>
                                </svg>
                                <p>Địa chỉ:</p>
                            </div>
                        <p class="text-infor">`+ item.addressDetail +`</p>
                        </div>
                        <div class="hotline_clinic">
                            <div class="title_address" ">
                                <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" viewBox="0 0 14 14" fill="none">
                                    <g clip-path="url(#clip0_762_4758)">
                                        <path d="M13.6189 10.2746L11.6652 8.32085C10.9674 7.62308 9.7812 7.90222 9.50209 8.80928C9.29276 9.4373 8.595 9.78618 7.96701 9.6466C6.57148 9.29772 4.68752 7.48353 4.33863 6.01822C4.1293 5.39021 4.54796 4.69244 5.17595 4.48314C6.08305 4.20403 6.36215 3.01783 5.66439 2.32007L3.71064 0.366327C3.15243 -0.122109 2.31511 -0.122109 1.82668 0.366327L0.500926 1.69208C-0.824828 3.08761 0.640479 6.78576 3.91997 10.0653C7.19947 13.3448 10.8976 14.8799 12.2932 13.4843L13.6189 12.1586C14.1074 11.6003 14.1074 10.763 13.6189 10.2746Z" fill="url(#paint0_linear_762_4758)" />
                                    </g>
                                    <defs>
                                        <linearGradient id="paint0_linear_762_4758" x1="6.99995" y1="0" x2="6.99995" y2="14" gradientUnits="userSpaceOnUse">
                                            <stop stop-color="#FFC800" />
                                            <stop offset="1" stop-color="#D700B5" />
                                        </linearGradient>
                                        <clipPath id="clip0_762_4758">
                                            <rect width="14" height="14" fill="white" />
                                        </clipPath>
                                    </defs>
                                </svg>
                                <p>Hotline:</p>
                            </div>
                        <p class="text-infor">`+ item.phone +`</p>
                        </div>

                    </div>
                    <div class="hover_clinic">
                        <div class="hover_box">
                            <a href="tel:`+ item.phone.replaceAll(".","") +`" class="btn_call">Gọi ngay</a>
                            <a class="btn_booking" href="`+ linkDetail +`">Chi tiết</a>
                        </div>
                    </div>
                </div>
            </div>`
        });
        $('#list_clinic').html(content);
    }
}
async function LoadDataProvince() {
    try {
        const responseData = await $.ajax({
            url: systemURL + 'province/api/list',
            type: 'GET',
            async: true,
            contentType: 'application/json'
        });
        const data = responseData.data;
        data.forEach(function (item) {
            $('#select-province').append(new Option(item.name, item.id, false, false));
        });
        $('#select-province').on("change", function () {
            loadData();
        })
    } catch (error) {
        console.error("Error loading province data:", error);
    }
}


function remove_accents(strAccents) {
    var strAccents = strAccents.split('');
    var strAccentsOut = new Array();
    var strAccentsLen = strAccents.length;
    var accents = "ÀÁÂÃÄÅàáâãäåÒÓÔÕÕÖØòóôõöøÈÉÊËèéêëÇçðÐÌÍÎÏìíîïÙÚÛÜùúûüÑñŠšŸÿýŽž";
    var accentsOut = "AAAAAAaaaaaaOOOOOOOooooooEEEEeeeeCcdDIIIIiiiiUUUUuuuuNnSsYyyZz";
    for (var y = 0; y < strAccentsLen; y++) {
        if (accents.indexOf(strAccents[y]) != -1) {
            strAccentsOut[y] = accentsOut.substr(accents.indexOf(strAccents[y]), 1);
        } else
            strAccentsOut[y] = strAccents[y];
    }
    strAccentsOut = strAccentsOut.join('').trim().replaceAll(" ", "-").toLowerCase();

    return strAccentsOut;
}
$(document).ready(async function () {
    LoadDataProvince();
    //console.log('hiển thị danh sách phòng khám');
    //await loadData();
    let element = $(".pagination_default");
    pageIndex = 1;
    LoadPagingPage("HealthFacility/api/Count", element, loadData);
    $('#search_clinic_page').on('input', function () {
        loadData();
    });
});