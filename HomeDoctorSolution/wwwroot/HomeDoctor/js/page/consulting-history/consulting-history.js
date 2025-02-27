$(document).ready(async function () {
    await loadDataWait();
    await loadDataConfirm();
    await loadDataDone();
    await loadDataCancel();
    $(".pagination_default").attr("class", "navigation pagination pagination_default wait align-items-end justify-content-end");
    let element = $(".pagination_default.wait");
    await LoadPagingPage("booking/api/count-list-booking-by-accountId?bookingStatusId=" + systemConstant.bookingStatus_wait_accept, element, loadDataWait);
})

var loadDataWait = async function () {
    $("#consulting_history_wait").addClass("parent-button-container-active");
    var result = await httpService.getAsync("booking/api/list-booking-by-bookingStatusId?pageIndex=" + pageIndex + "&pageSize=" + 10 + "&bookingStatusId=" + systemConstant.bookingStatus_wait_accept);
    var content = ``;
    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#lst_history_wait").html('');
            var dataSource = result.data;
            console.log(dataSource);
            dataSource.forEach(function (item, index) {
                /*var name = account.data[0].roleId == systemConstant.role_counselor_id ? item.accountName : item.counselorName;*/
                content += `<div class="item-history d-flex">
                        <div class="img_doctor">
                            <a href="chi-tiet-lich-su-tu-van/${item.id}"><img src="${item.photo}" /></a>
                        </div>
                        <div class="item-content">
                            <a href="chi-tiet-lich-su-tu-van/${item.id}"><p class="name-doctor">${item.counselorName}</p></a>
                            <p class="time-history">${timeSince(item.startTime)} - ${formatDate(item.startTime)}</p>
                            <div class="footer-item d-flex ">
                                <p>Dịch vụ: ${item.serviceName}</p>
                                <div class="more-history">
                                <a href="chi-tiet-lich-su-tu-van/${item.id}">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" viewBox="0 0 36 36" fill="none">
                                        <g filter="url(#filter0_d_811_2614)">
                                            <rect x="25.999" y="22" width="16" height="16" rx="5" transform="rotate(-180 25.999 22)" fill="white" fill-opacity="0.2" shape-rendering="crispEdges" />
                                            <path fill-rule="evenodd" clip-rule="evenodd" d="M16.0835 17.8857C15.8585 17.7057 15.822 17.3774 16.002 17.1524L18.5237 14.0004L16.002 10.8483C15.822 10.6233 15.8585 10.295 16.0835 10.115C16.3085 9.93506 16.6368 9.97153 16.8168 10.1965L19.5991 13.6745C19.7516 13.865 19.7516 14.1357 19.5991 14.3263L16.8168 17.8042C16.6368 18.0292 16.3085 18.0657 16.0835 17.8857Z" fill="black" />
                                        </g>
                                        <defs>
                                            <filter id="filter0_d_811_2614" x="-0.000976562" y="0" width="36" height="36" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                                                <feFlood flood-opacity="0" result="BackgroundImageFix" />
                                                <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha" />
                                                <feOffset dy="4" />
                                                <feGaussianBlur stdDeviation="5" />
                                                <feComposite in2="hardAlpha" operator="out" />
                                                <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.15 0" />
                                                <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_811_2614" />
                                                <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_811_2614" result="shape" />
                                            </filter>
                                        </defs>
                                    </svg>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>`
            });
            $("#lst_history_wait").append(content);
            $(".content-history-confirm").addClass("d-none");
            $(".content-history-done").addClass("d-none");
            $(".content-history-cancel").addClass("d-none");
            $("#consulting_history_done").removeClass("parent-button-container-active")
            $("#consulting_history_confirm").removeClass("parent-button-container-active")
            $("#consulting_history_cancel").removeClass("parent-button-container-active")
        }
        else {
            $("#lst_history_wait").html('');
            var newRow = `<div class="not-found d-flex flex-column align-items-center">
                    <img style="width:400px;height:auto;" src="/images/default/not-found.png"/>
                    <p>Chưa có dữ liệu hiển thị</p>
                    </div>`;
            $("#lst_history_wait").append(newRow);
            $(".content-history-done").addClass("d-none")
            $(".content-history-confirm").addClass("d-none");
            $(".content-history-cancel").addClass("d-none");
            $("#consulting_history_confirm").removeClass("parent-button-container-active")
            $("#consulting_history_done").removeClass("parent-button-container-active")
            $("#consulting_history_cancel").removeClass("parent-button-container-active")
        }
    }

}
var loadDataConfirm = async function () {
    $("#consulting_history_confirm").addClass("parent-button-container-active");
    var result = await httpService.getAsync("booking/api/list-booking-by-bookingStatusId?pageIndex=" + pageIndex + "&pageSize=" + 10 + "&bookingStatusId=" + systemConstant.bookingStatus_confirm);
    var content = ``;

    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#lst_history_confirm").html('');
            var dataSource = result.data;
            dataSource.forEach(function (item, index) {
                /*var name = account.data[0].roleId == systemConstant.role_counselor_id ? item.accountName : item.counselorName;*/
                content += `<div class="item-history d-flex">
                        <div class="img_doctor">
                            <a href="chi-tiet-lich-su-tu-van/${item.id}"><img src="${item.photo}" /></a>
                        </div>
                        <div class="item-content">
                            <a href="chi-tiet-lich-su-tu-van/${item.id}"><p class="name-doctor">${item.counselorName}</p></a>
                            <p class="time-history">${timeSince(item.startTime)} - ${formatDate(item.startTime)}</p>
                            <div class="footer-item d-flex ">
                                <p>Dịch vụ: ${item.serviceName}</p>
                                <div class="more-history">
                                <a href="chi-tiet-lich-su-tu-van/${item.id}">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" viewBox="0 0 36 36" fill="none">
                                        <g filter="url(#filter0_d_811_2614)">
                                            <rect x="25.999" y="22" width="16" height="16" rx="5" transform="rotate(-180 25.999 22)" fill="white" fill-opacity="0.2" shape-rendering="crispEdges" />
                                            <path fill-rule="evenodd" clip-rule="evenodd" d="M16.0835 17.8857C15.8585 17.7057 15.822 17.3774 16.002 17.1524L18.5237 14.0004L16.002 10.8483C15.822 10.6233 15.8585 10.295 16.0835 10.115C16.3085 9.93506 16.6368 9.97153 16.8168 10.1965L19.5991 13.6745C19.7516 13.865 19.7516 14.1357 19.5991 14.3263L16.8168 17.8042C16.6368 18.0292 16.3085 18.0657 16.0835 17.8857Z" fill="black" />
                                        </g>
                                        <defs>
                                            <filter id="filter0_d_811_2614" x="-0.000976562" y="0" width="36" height="36" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                                                <feFlood flood-opacity="0" result="BackgroundImageFix" />
                                                <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha" />
                                                <feOffset dy="4" />
                                                <feGaussianBlur stdDeviation="5" />
                                                <feComposite in2="hardAlpha" operator="out" />
                                                <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.15 0" />
                                                <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_811_2614" />
                                                <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_811_2614" result="shape" />
                                            </filter>
                                        </defs>
                                    </svg>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>`
            });
            $("#lst_history_confirm").append(content);
        }
        else {
            $("#lst_history_confirm").html('');
            var newRow = `<div class="not-found d-flex flex-column align-items-center">
                    <img style="width:400px;height:auto;" src="/images/default/not-found.png"/>
                    <p>Chưa có dữ liệu hiển thị</p>
                    </div>`;
            $("#lst_history_confirm").append(newRow);
        }
    }
}
var loadDataDone = async function () {
    $("#consulting_history_done").addClass("parent-button-container-active");
    var result = await httpService.getAsync("booking/api/list-booking-by-bookingStatusId?pageIndex=" + pageIndex + "&pageSize=" + 10 + "&bookingStatusId=" + systemConstant.bookingStatus_done);
    var content = ``;

    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#lst_history_done").html('');
            var dataSource = result.data;
            dataSource.forEach(function (item, index) {
                /*var name = account.data[0].roleId == systemConstant.role_counselor_id ? item.accountName : item.counselorName;*/
                content += `<div class="item-history d-flex">
                        <div class="img_doctor">
                            <a href="chi-tiet-lich-su-tu-van/${item.id}"><img src="${item.photo}" /></a>
                        </div>
                        <div class="item-content">
                            <a href="chi-tiet-lich-su-tu-van/${item.id}"><p class="name-doctor">${item.counselorName}</p></a>
                            <p class="time-history">${timeSince(item.startTime)} - ${formatDate(item.startTime)}</p>
                            <div class="footer-item d-flex ">
                                <p>Dịch vụ: ${item.serviceName}</p>
                                <div class="more-history">
                                <a href="chi-tiet-lich-su-tu-van/${item.id}">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" viewBox="0 0 36 36" fill="none">
                                        <g filter="url(#filter0_d_811_2614)">
                                            <rect x="25.999" y="22" width="16" height="16" rx="5" transform="rotate(-180 25.999 22)" fill="white" fill-opacity="0.2" shape-rendering="crispEdges" />
                                            <path fill-rule="evenodd" clip-rule="evenodd" d="M16.0835 17.8857C15.8585 17.7057 15.822 17.3774 16.002 17.1524L18.5237 14.0004L16.002 10.8483C15.822 10.6233 15.8585 10.295 16.0835 10.115C16.3085 9.93506 16.6368 9.97153 16.8168 10.1965L19.5991 13.6745C19.7516 13.865 19.7516 14.1357 19.5991 14.3263L16.8168 17.8042C16.6368 18.0292 16.3085 18.0657 16.0835 17.8857Z" fill="black" />
                                        </g>
                                        <defs>
                                            <filter id="filter0_d_811_2614" x="-0.000976562" y="0" width="36" height="36" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                                                <feFlood flood-opacity="0" result="BackgroundImageFix" />
                                                <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha" />
                                                <feOffset dy="4" />
                                                <feGaussianBlur stdDeviation="5" />
                                                <feComposite in2="hardAlpha" operator="out" />
                                                <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.15 0" />
                                                <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_811_2614" />
                                                <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_811_2614" result="shape" />
                                            </filter>
                                        </defs>
                                    </svg>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>`
            });
            $("#lst_history_done").append(content);
        }
        else {
            $("#lst_history_done").html('');
            var newRow = `<div class="not-found d-flex flex-column align-items-center">
                    <img style="width:400px;height:auto;" src="/images/default/not-found.png"/>
                    <p>Chưa có dữ liệu hiển thị</p>
                    </div>`;
            $("#lst_history_done").append(newRow);
        }
    }

}
var loadDataCancel = async function () {
    $("#consulting_history_cancel").addClass("parent-button-container-active");
    var result = await httpService.getAsync("booking/api/list-booking-by-bookingStatusId?pageIndex=" + pageIndex + "&pageSize=" + 10 + "&bookingStatusId=" + systemConstant.bookingStatus_cancel);
    var content = ``;

    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#lst_history_cancel").html('');
            var dataSource = result.data;
            dataSource.forEach(function (item, index) {
                /*var name = account.data[0].roleId == systemConstant.role_counselor_id ? item.accountName : item.counselorName;*/
                content += `<div class="item-history d-flex">
                        <div class="img_doctor">
                            <a href="chi-tiet-lich-su-tu-van/${item.id}"><img src="${item.photo}" /></a>
                        </div>
                        <div class="item-content">
                            <a href="chi-tiet-lich-su-tu-van/${item.id}"><p class="name-doctor">${item.counselorName}</p></a>
                            <p class="time-history">${timeSince(item.startTime)} - ${formatDate(item.startTime)}</p>
                            <div class="footer-item d-flex ">
                                <p>Dịch vụ: ${item.serviceName}</p>
                                <div class="more-history">
                                <a href="chi-tiet-lich-su-tu-van/${item.id}">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" viewBox="0 0 36 36" fill="none">
                                        <g filter="url(#filter0_d_811_2614)">
                                            <rect x="25.999" y="22" width="16" height="16" rx="5" transform="rotate(-180 25.999 22)" fill="white" fill-opacity="0.2" shape-rendering="crispEdges" />
                                            <path fill-rule="evenodd" clip-rule="evenodd" d="M16.0835 17.8857C15.8585 17.7057 15.822 17.3774 16.002 17.1524L18.5237 14.0004L16.002 10.8483C15.822 10.6233 15.8585 10.295 16.0835 10.115C16.3085 9.93506 16.6368 9.97153 16.8168 10.1965L19.5991 13.6745C19.7516 13.865 19.7516 14.1357 19.5991 14.3263L16.8168 17.8042C16.6368 18.0292 16.3085 18.0657 16.0835 17.8857Z" fill="black" />
                                        </g>
                                        <defs>
                                            <filter id="filter0_d_811_2614" x="-0.000976562" y="0" width="36" height="36" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                                                <feFlood flood-opacity="0" result="BackgroundImageFix" />
                                                <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha" />
                                                <feOffset dy="4" />
                                                <feGaussianBlur stdDeviation="5" />
                                                <feComposite in2="hardAlpha" operator="out" />
                                                <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.15 0" />
                                                <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_811_2614" />
                                                <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_811_2614" result="shape" />
                                            </filter>
                                        </defs>
                                    </svg>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>`
            });
            $("#lst_history_cancel").append(content);
        }
        else {
            $("#lst_history_cancel").html('');
            var newRow = `<div class="not-found d-flex flex-column align-items-center">
                    <img style="width:400px;height:auto;" src="/images/default/not-found.png"/>
                    <p>Chưa có dữ liệu hiển thị</p>
                    </div>`;
            $("#lst_history_cancel").append(newRow);
        }
    }
}

var wait = () => {
    pageIndex = 1

    $("#consulting_history_wait").addClass("parent-button-container-active")
    $("#consulting_history_confirm").removeClass("parent-button-container-active")
    $("#consulting_history_done").removeClass("parent-button-container-active")
    $("#consulting_history_cancel").removeClass("parent-button-container-active")

    $(".content-history-wait").removeClass("d-none");
    $(".content-history-confirm").addClass("d-none");
    $(".content-history-done").addClass("d-none");
    $(".content-history-cancel").addClass("d-none");

    $(".pagination_default").attr("class", "navigation pagination pagination_default wait align-items-end justify-content-end");
    let element = $(".pagination_default.wait");
    LoadPagingPage("booking/api/count-list-booking-by-accountId?bookingStatusId=" + systemConstant.bookingStatus_wait_accept, element, loadDataWait);
}
var confirm = () => {
    pageIndex = 1

    $("#consulting_history_confirm").addClass("parent-button-container-active")
    $("#consulting_history_wait").removeClass("parent-button-container-active")
    $("#consulting_history_done").removeClass("parent-button-container-active")
    $("#consulting_history_cancel").removeClass("parent-button-container-active")

    $(".content-history-confirm").removeClass("d-none");
    $(".content-history-done").addClass("d-none");
    $(".content-history-wait").addClass("d-none");
    $(".content-history-cancel").addClass("d-none");

    $(".pagination_default").attr("class", "navigation pagination pagination_default confirm align-items-end justify-content-end");
    let element = $(".pagination_default.confirm");
    LoadPagingPage("booking/api/count-list-booking-by-accountId?bookingStatusId=" + systemConstant.bookingStatus_confirm, element, loadDataConfirm);
}
var done = () => {
    pageIndex = 1

    $("#consulting_history_wait").removeClass("parent-button-container-active")
    $("#consulting_history_done").addClass("parent-button-container-active")
    $("#consulting_history_cancel").removeClass("parent-button-container-active")
    $("#consulting_history_confirm").removeClass("parent-button-container-active")


    $(".content-history-done").removeClass("d-none");
    $(".content-history-cancel").addClass("d-none");
    $(".content-history-confirm").addClass("d-none");
    $(".content-history-wait").addClass("d-none");

    $(".pagination_default").attr("class", "navigation pagination pagination_default done align-items-end justify-content-end");
    let element = $(".pagination_default.done");
    LoadPagingPage("booking/api/count-list-booking-by-accountId?bookingStatusId=" + systemConstant.bookingStatus_done, element, loadDataDone);
}
var cancel = () => {
    pageIndex = 1

    $("#consulting_history_wait").removeClass("parent-button-container-active")
    $("#consulting_history_done").removeClass("parent-button-container-active")
    $("#consulting_history_confirm").removeClass("parent-button-container-active")
    $("#consulting_history_cancel").addClass("parent-button-container-active")

    $(".content-history-cancel").removeClass("d-none");
    $(".content-history-confirm").addClass("d-none");
    $(".content-history-done").addClass("d-none");
    $(".content-history-wait").addClass("d-none");

    $(".pagination_default").attr("class", "navigation pagination pagination_default cancel align-items-end justify-content-end");
    let element = $(".pagination_default.cancel");
    LoadPagingPage("booking/api/count-list-booking-by-accountId?bookingStatusId=" + systemConstant.bookingStatus_cancel, element, loadDataCancel);
}
function timeSince(dateTime) {
    const currentTime = new Date();
    const timeDifference = (currentTime - new Date(dateTime)) / 1000;
    return formatTime(new Date(dateTime));
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
function formatTime(date) {
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return `${hours}:${minutes}`;
}