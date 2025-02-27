$(document).ready(async function () {
    await loadDataWait();
    // await loadDataConfirm();
    // await loadDataDone();
    // await loadDataCancel();
    $(".pagination_default").attr("class", "navigation pagination pagination_default wait align-items-end justify-content-end");
    let element = $(".pagination_default.wait");
    LoadPagingPage("orders/api/count-order-by-accountId?orderStatusId=" + 1000001, element, loadDataWait);
})

var loadDataWait =  async function () {
    $("#consulting_history_wait").addClass("parent-button-container-active");
    var result = await httpService.getAsync("orders/api/list-order-by-orderStatusId?pageIndex=" + pageIndex + "&pageSize=" + 10 + "&orderStatusId=" + 1000001);
    var content = ``;
    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#lst_history_wait").html('');
            var dataSource = result.data;
            console.log(dataSource);
            dataSource.forEach(function (item, index) {
                orderdetails = item.orderDetails;

                // Định dạng totalPrice thành kiểu tiền tệ
                var formattedTotalPrice = item.totalPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });

                var productHTML = orderdetails.map(function(orderDetail) {
                    return `<div class="footer-item d-flex">
                                <div class="product-name" style="width: 85%">${orderDetail.productName}</div>
                                <div class="quantity">
                                    <span class="quantity-value">
                                        x ${orderDetail.quantity}
                                        <i class="fas fa-dice-d6 f-22" style="color:#006169"></i>
                                    </span>
                                </div>
                            </div>`;
                    }).join('');


                content += `<div class="item-history d-flex">
                                <div class="item-content">
                                <a href="don-hang/${item.id}"><p class="name-doctor">#${item.id}</p></a>
                                    <div class="footer-item d-flex ">
                                        <div class="box-left">
                        <!--                    <p class="font-weight-bold" >${item.orderStatusShipName}</p>-->
                                            <p class="time-history">${item.shipRecipientName}</p>
                                            <p class="time-history">${item.shipRecipientPhone}</p>
                                            <p class="time-history">${formatDate(item.createdTime)}</p>
                                        </div>
                                        <div class="product">
                                            ${productHTML}
                                        </div>
                                        <div class="price">
                                            ${formattedTotalPrice} <!-- Sử dụng totalPrice đã được định dạng -->
                                        </div>
                                    </div>
                                </div>
                            </div>`;
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
    var result = await httpService.getAsync("orders/api/list-order-by-orderStatusId?pageIndex=" + pageIndex + "&pageSize=" + 10 + "&orderStatusId=" + 1000002);
    var content = ``;
    
    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#lst_history_confirm").html('');
            var dataSource = result.data;
            console.log(dataSource);
            dataSource.forEach(function (item, index) {
                orderdetails = item.orderDetails;

                // Định dạng totalPrice thành kiểu tiền tệ
                var formattedTotalPrice = item.totalPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });

                var productHTML = orderdetails.map(function(orderDetail) {
                    return `<div class="footer-item d-flex">
                                <div class="product-name" style="width: 85%">${orderDetail.productName}</div>
                                <div class="quantity">
                                    <span class="quantity-value">
                                        x ${orderDetail.quantity}
                                        <i class="fas fa-dice-d6 f-22" style="color:#006169"></i>
                                    </span>
                                </div>
                            </div>`;
                }).join('');


                content += `<div class="item-history d-flex">
                                <div class="item-content">
                                <a href="don-hang/${item.id}"><p class="name-doctor">#${item.id}</p></a>
                                    <div class="footer-item d-flex ">
                                        <div class="box-left">
                        <!--                    <p class="font-weight-bold" >${item.orderStatusShipName}</p>-->
                                            <p class="time-history">${item.shipRecipientName}</p>
                                            <p class="time-history">${item.shipRecipientPhone}</p>
                                            <p class="time-history">${formatDate(item.createdTime)}</p>
                                        </div>
                                        <div class="product">
                                            ${productHTML}
                                        </div>
                                        <div class="price">
                                            ${formattedTotalPrice} <!-- Sử dụng totalPrice đã được định dạng -->
                                        </div>
                                    </div>
                                </div>
                            </div>`;
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
var loadDataDone =  async function () {
    $("#consulting_history_done").addClass("parent-button-container-active");
    var result = await httpService.getAsync("orders/api/list-order-by-orderStatusId?pageIndex=" + pageIndex + "&pageSize=" + 10 + "&orderStatusId=" + 1000003);
    var content = ``;

    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#lst_history_done").html('');
            var dataSource = result.data;
            console.log(dataSource);
            dataSource.forEach(function (item, index) {
                orderdetails = item.orderDetails;

                // Định dạng totalPrice thành kiểu tiền tệ
                var formattedTotalPrice = item.totalPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });

                var productHTML = orderdetails.map(function(orderDetail) {
                    return `<div class="footer-item d-flex">
                                <div class="product-name" style="width: 85%">${orderDetail.productName}</div>
                                <div class="quantity">
                                    <span class="quantity-value">
                                        x ${orderDetail.quantity}
                                        <i class="fas fa-dice-d6 f-22" style="color:#006169"></i>
                                    </span>
                                </div>
                            </div>`;
                }).join('');


                content += `<div class="item-history d-flex">
                                <div class="item-content">
                                <a href="don-hang/${item.id}"><p class="name-doctor">#${item.id}</p></a>
                                    <div class="footer-item d-flex ">
                                        <div class="box-left">
                        <!--                    <p class="font-weight-bold" >${item.orderStatusShipName}</p>-->
                                            <p class="time-history">${item.shipRecipientName}</p>
                                            <p class="time-history">${item.shipRecipientPhone}</p>
                                            <p class="time-history">${formatDate(item.createdTime)}</p>
                                        </div>
                                        <div class="product">
                                            ${productHTML}
                                        </div>
                                        <div class="price">
                                            ${formattedTotalPrice} <!-- Sử dụng totalPrice đã được định dạng -->
                                        </div>
                                    </div>
                                </div>
                            </div>`;
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
var loadDataCancel = async function() {
    $("#consulting_history_cancel").addClass("parent-button-container-active");
    var result = await httpService.getAsync("orders/api/list-order-by-orderStatusId?pageIndex=" + pageIndex + "&pageSize=" + 10 + "&orderStatusId=" + 1000006);
    var content = ``;

    if (result.status == "200") {
        if (result.data != null && result.data.length != 0) {
            $("#lst_history_cancel").html('');
            var dataSource = result.data;
            console.log(dataSource);
            dataSource.forEach(function (item, index) {
                orderdetails = item.orderDetails;

                // Định dạng totalPrice thành kiểu tiền tệ
                var formattedTotalPrice = item.totalPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });

                var productHTML = orderdetails.map(function(orderDetail) {
                    return `<div class="footer-item d-flex">
                                <div class="product-name" style="width: 85%">${orderDetail.productName}</div>
                                <div class="quantity">
                                    <span class="quantity-value">
                                        x ${orderDetail.quantity}
                                        <i class="fas fa-dice-d6 f-22" style="color:#006169"></i>
                                    </span>
                                </div>
                            </div>`;
                }).join('');


                content += `<div class="item-history d-flex">
                                <div class="item-content">
                                <a href="don-hang/${item.id}"><p class="name-doctor">#${item.id}</p></a>
                                    <div class="footer-item d-flex ">
                                        <div class="box-left">
                        <!--                    <p class="font-weight-bold" >${item.orderStatusShipName}</p>-->
                                            <p class="time-history">${item.shipRecipientName}</p>
                                            <p class="time-history">${item.shipRecipientPhone}</p>
                                            <p class="time-history">${formatDate(item.createdTime)}</p>
                                        </div>
                                        <div class="product">
                                            ${productHTML}
                                        </div>
                                        <div class="price">
                                            ${formattedTotalPrice} <!-- Sử dụng totalPrice đã được định dạng -->
                                        </div>
                                    </div>
                                </div>
                            </div>`;
            });
            $("#lst_history_cancel").append(content);
            $(".item-history").prop()
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
    LoadPagingPage("orders/api/count-order-by-accountId?orderStatusId=" + 1000001, element, loadDataWait);
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
    LoadPagingPage("orders/api/count-order-by-accountId?orderStatusId=" + 1000002, element, loadDataConfirm);
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
    LoadPagingPage("orders/api/count-order-by-accountId?orderStatusId=" + 1000003, element, loadDataDone);

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
    LoadPagingPage("orders/api/count-order-by-accountId?orderStatusId=" + 1000004, element, loadDataCancel);

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