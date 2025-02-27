"use strict";
//var counselorId = 0;
//async function loadData() {
//    var result = await httpService.getAsync("Doctors/api/DetailViewmodel/" + id);
//    if (result.status == "200") {
//        console.log(result.data)
//        result.data.forEach(function (item) {
//            counselorId = item.accountId;
//            $("#service").val(item.license);
//            $(".detail-name").html(item.name)
//            $(".detail-des").html(item.info)
//            $("#specialist").html(item.specialist)
//            $("#language").html(item.language)
//            $("#experience").html(item.experience)
//            $("#position").html(item.position)
//            $(".detail-description-content").html(item.description)
//            $(".image-doctor img").attr("src", item.image);
//        })
//    }
//}

//function bookingDoctor() {
//    var time = $("#time").val();
//    var timeBooking = time.split("-");
//    var date = $("#day").val().split('/').reverse().join('-');
//    var startTime = date + "T" + timeBooking[0] + ":00";
//    var endTime = date + "T" + timeBooking[1] + ":00";
//    if (accountId != "") {
//        var booking = {
//            "id": 0,
//            "accountId": parseInt(accountId),
//            "bookingTypeId": 1000002,
//            "bookingStatusId": 1000001,
//            "counselorId": parseInt(id),
//            "name": $("#name").val(),
//            "address": "",
//            "URL": $("#phone-number").val(),
//            "startTime": startTime,
//            "endTime": endTime,
//            "info": $("#description").val(),
//            "reason": ""
//        };

//        Swal.fire({
//            title: 'Đặt lịch tư vấn',
//            html: "Bạn có chắc chắn muốn đặt lịch tư vấn ?",
//            showCancelButton: true,
//            confirmButtonColor: '#3085d6',
//            cancelButtonColor: '#443',
//            cancelButtonText: 'Hủy',
//            confirmButtonText: 'Đồng ý',
//        }).then((result) => {
//            booking.createdTime = new Date();
//            booking.active = 1
//            if (result.value) {
//                AddBooking(booking)
//            }
//        })
//    }
//    else {
//        Swal.fire({
//            title: 'Không đặt được lịch tư vấn',
//            html: "Bạn phải đăng nhập để sử dụng chức năng này",
//            showCancelButton: true,
//            confirmButtonColor: '#3085d6',
//            cancelButtonColor: '#443',
//            confirmButtonText: 'Xác nhận',
//        }).then((result) => {
//            if (result) {
//                window.location.href = "/dang-nhap";
//            }
//        })
//    }
//}

//async function AddBooking(booking) {
//    try {
//        /*if (booking.id == 0) {*/
//            //console.log(booking);
//            var response = await httpService.postAsync("Booking/api/add", booking);
//            if (response.status == 201 && response.message === "CREATED") {
//                Swal.fire({
//                    html: '<strong>Đặt lịch hẹn tư vấn thành công</strong>',
//                    icon: 'success'
//                });
//                console.log(response)
//                location.href = "/chi-tiet-lich-su-tu-van/" + response.data[0].id;
//            }
//            else {
//                Swal.fire({
//                    html: 'Đặt lịch hẹn không thành công, vui lòng thử lại sau !',
//                    icon: 'error'
//                });
//            }
//        /*}*/
//    }
//    catch (ex) {
//        console.log(ex);
//    }
//}

//$(document).ready(function () {
//    $(".my-rating").starRating({
//        useGradient: false,
//        starSize: 20,
//        initialRating: 4,
//        readOnly: true,
//        disableAfterRate: false,
//        starShape: 'rounded',
//    });
//    loadData();
//});
// async function

async function updateCartProductQuantity(id, quantity) {
    await httpService.postAsync("cartproduct/api/addcartproduct/", {
        productId: id,
        quantity: quantity,
    });
    LoadCartProductData();
}

async function minusOrderItem(id) {
    await updateCartProductQuantity(id, -1);
    await LoadCartProductData();
}

async function plusOrderItem(id) {
    await updateCartProductQuantity(id, 1);
    await LoadCartProductData();
}

async function LoadCartProductData() {
    if (accountId > 0) {
        if (orderId == 0) {
            await LoadUserData();
            try {
                const responseData = await $.ajax({
                    url: `${systemURL}cart/api/Search/${accountId}`,
                    type: 'GET',
                    async: 'true',
                    contentType: 'application/json',
                });

                const data = responseData.data[0];

                let items = 0;
                let totalCartPrice = 0;

                const generateProductHtml = (item) => {
                    items += item.quantity;
                    const totalPrice = item.price * item.quantity;
                    totalCartPrice += totalPrice;

                    return `
                <li id="${item.productId}">
                    <div class="media">
                        <div class="order-product-item">
                            <div class="order-product-item-info-body align-items-center col-lg-6 col-xs-12 gap-22">
                                <img src="${item.photo}" alt=""/>
                                <div class="order-product-item-info">
                                    <div class="order-product-item-info-name">${item.name}</div>
                                    <div class="order-product-item-info-mobile">
                                        <div class="order-product-item-price-mobile col-lg-2 p-0">${item.price}</div>
                                        <div class="order-product-item-quantity-mobile col-lg-2 p-0">
                                            <div class="form-inner">
                                                <input type="button" value="-" class="button-minus" data-field="quantity" onClick="minusItem(${item.productId})">
                                                <input type="number" step="1" max="" value="${item.quantity}" name="quantity" class="quantity-field">
                                                <input type="button" value="+" class="button-plus" data-field="quantity" onClick="plusItem(${item.productId})">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="order-product-item-price col-lg-2 p-0">${formatNumberCart(item.price.toString())} đ</div>
                            <div class="order-product-item-quantity col-lg-2 p-0">
                                <div class="form-inner">
                                    <input type="button" value="-" class="button-minus" data-field="quantity" onClick="minusOrderItem(${item.productId})">
                                    <input type="number" step="1" max="" value="${item.quantity}" name="quantity" class="quantity-field">
                                    <input type="button" value="+" class="button-plus" data-field="quantity" onClick="plusOrderItem(${item.productId})">
                                </div>
                            </div>
                            <div class="order-product-item-finalprice col-lg-2 p-0">${formatNumberCart(totalPrice.toString())} đ</div>
                        </div>
                    </div>
                </li>`;
                };

                const productHtml = data.map(generateProductHtml).join('');
                $(".order-product-body ul").html(productHtml);

                // Calculate and update prices
                const updatePrice = (label, value) => {
                    $(`.form-calc-price-line span:contains('${label}')`).next("span").text(formatNumberCart(value.toString()) + " đ");
                };

                updatePrice('Tạm tính', totalCartPrice);
                updatePrice('Phí vận chuyển', 0);
                updatePrice('Thuế(đã bao gồm)', 0);

                let totalOrderPrice =
                    totalCartPrice +
                    // Phí vận chuyển
                    0 +
                    // Thuế
                    0;

                updatePrice('Tổng cộng', totalOrderPrice);
                $(".card-order-total-price").text(formatNumberCart(totalCartPrice.toString()) + " đ");

            } catch (error) {
                // Handle the error if needed
                // console.error(error.message);
            }
        }
    }
}


// Hàm validate số điện thoại
function ValidatePhoneNumber(phoneNumber) {
    return /^[0-9]{10}$/.test(phoneNumber);
}

// Hàm xử lý thêm đơn hàng chi tiết
async function AddOrderDetail() {
    if (orderId > 0) {
        Swal.fire({
            title: 'Bạn có chắc chắn muốn hủy đơn hàng?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Có',
            cancelButtonText: 'Không'
        }).then((result) => {
            if (result.isConfirmed) {
                // Gửi yêu cầu AJAX để hủy đơn hàng
                $.ajax({
                    url: systemURL + "orders/api/CancelOrderById",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify({"id": orderId}),
                    beforeSend: function (xhr) {
                        if (localStorage.token) {
                            xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
                        }
                    },
                    success: async function (responseData) {
                        console.log(responseData);
                        if (responseData.status == 200 && responseData.message == "SUCCESS") {
                            Swal.fire(
                                'Thành công!',
                                'Hủy đơn hàng thành công',
                                'success'
                            ).then(() => {
                                window.location.href = `/lich-su-don-hang`;
                            });
                        }
                    },
                    error: function (e) {
                        Swal.fire(
                            'Thất bại!',
                            'Hủy đơn hàng thất bại',
                            'error'
                        );
                    }
                });
            } else {
                // Nếu người dùng hủy, không làm gì hoặc cung cấp phản hồi nếu cần thiết
            }
        });
    } else {
        const listOrderDetail = [];
        $(".order-product-item").each(function () {
            // Lặp qua từng sản phẩm để thu thập thông tin đơn hàng chi tiết
            const quantity = convertToNumber($(this).find(".quantity-field").val());
            const finalPrice = convertToNumber($(this).find(".order-product-item-finalprice").text());
            const itemId = convertToNumber($(this).closest("li").prop("id"));
            const orderDetail = {
                productId: itemId,
                quantity: quantity | 0,
                finalPrice: finalPrice,
            };
            listOrderDetail.push(orderDetail);
        });

        const phoneNumber = $("#profile_phone").val();

        // Validate số điện thoại
        if (!ValidatePhoneNumber(phoneNumber)) {
            Swal.fire(
                'Lỗi!',
                'Số điện thoại không hợp lệ. Vui lòng kiểm tra và nhập lại.',
                'error'
            );
            return;
        }
        const addressDetail = $("#shipAddressDetail").val();
        if (!addressDetail.trim()) {
            Swal.fire(
                'Lỗi!',
                'Địa chỉ chi tiết không được để trống.',
                'error'
            );
            return;
        }
        const shipRecipientName = $("#account-name").val();
        if (!shipRecipientName.trim()) {
            Swal.fire(
                'Lỗi!',
                'Tên người nhận không được để trống.',
                'error'
            );
            return;
        }

        const updatingObj = {
            orderTypeId: 10000001,
            orderStatusId: 1000001,
            orderPaymentStatusId: 1000001,
            orderStatusShipId: 1000001,
            active: 1,
            totalShipFee: 0,
            tax: 0,
            description: $(".form-calc-price-description-text").val(),
            shipAddressDetail: $("#shipAddressDetail").val(),
            accountName: $("#account-name").val(),
            accountPhone: phoneNumber,
            shipProvinceAddressId: $('#select_province').val(),
            shipDistrictAddressId: $('#select_district').val(),
            shipWardAddressId: $('#select_wards').val(),
            shipProvinceAddress: $("#select2-select_province-container").text(),
            shipDistrictAddress: $("#select2-select_district-container").text(),
            shipWardAddress: $("#select2-select_wards-container").text(),
            accountId: accountId,
            orderDetails: listOrderDetail,
        };

        try {
            Swal.fire({
                title: 'Bạn có chắc chắn muốn tạo đơn hàng?',
                icon: 'question',
                showCancelButton: true,
                confirmButtonText: 'Có',
                cancelButtonText: 'Không'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Nếu người dùng xác nhận, tiến hành gửi yêu cầu AJAX
                    $.ajax({
                        url: systemURL + "orders/api/AddOrder",
                        type: "POST",
                        contentType: "application/json",
                        data: JSON.stringify(updatingObj),
                        success: async function (responseData) {
                            if (responseData.status == 201 && responseData.message == "CREATED") {
                                var order = responseData.data[0];
                                Swal.fire(
                                    'Thành công!',
                                    'Tạo đơn hàng thành công',
                                    'success'
                                ).then(() => {
                                    window.location.href = `/don-hang/${order.id}`;
                                });
                            }
                        },
                        error: function (e) {
                            Swal.fire(
                                'Thất bại!',
                                'Tạo đơn hàng thất bại',
                                'error'
                            );
                        }
                    });
                } else {
                    // Nếu người dùng hủy, không làm gì hoặc cung cấp phản hồi nếu cần thiết
                }
            });
        } catch (error) {
            // Xử lý mọi lỗi có thể xảy ra ngoài yêu cầu AJAX
        }
    }
}


function convertToNumber(value) {
    const numericValue = Number(value.replace(/[^\d.]/g, ''));
    if (!isNaN(numericValue)) {
        return numericValue;
    } else {
        console.error("Invalid number format");
        return null;
    }
}

async function LoadOrderData() {
    if (orderId > 0) {
        $("#submitbtn").text("Hủy đơn hàng");

        try {
            const responseData = await $.ajax({
                url: `${systemURL}orders/api/DetailById/${orderId}`,
                type: 'GET',
                contentType: 'application/json'
            });

            const data = responseData.data[0];
            console.log(data);

            if (data.orderStatusId == 1000006) {
                $("#submitbtn").hide();
            }
            $("#shipAddressDetail").val(data.shipAddressDetail).prop('readonly', true);
            $("#account-name").val(data.shipRecipientName).prop('readonly', true);
            $("#profile_phone").val(data.shipRecipientPhone).prop('readonly', true);

            // Disable readonly inputs
            $(".form-calc-price-description-text, #shipAddressDetail, #account-name, #profile_phone").prop('readonly', true);
            
            // Populate shipping address
            populateShippingAddress(data);

            // Populate order products
            await populateOrderProducts(data.orderDetails);
        } catch (error) {
            console.error("Error loading order data:", error);
        }
    }
}

function populateOrderProducts(orderDetails) {
    let totalPrice = 0;

    const html = orderDetails.map(item => {
        totalPrice += parseFloat(item.finalPrice);
        return `
            <li id="${item.productId}">
                <div class="media">
                    <div class="order-product-item">
                        <div class="order-product-item-info-body align-items-center col-lg-6 col-xs-12 gap-22">
                            <img src="${item.productPhoto}" />
                            <div class="order-product-item-info">
                                <div class="order-product-item-info-name">${item.productName}</div>
                                <div class="order-product-item-info-mobile">
                                    <div class="order-product-item-price-mobile col-lg-2 p-0">${item.price}</div>
                                    <div class="order-product-item-quantity-mobile col-lg-2 p-0">
                                        <div class="form-inner">
                                            <input type="button" value="-" class="button-minus" data-field="quantity" onClick="minusItem(${item.productId})">
                                            <input type="number" step="1" max="" value="${item.quantity}" name="quantity" class="quantity-field">
                                            <input type="button" value="+" class="button-plus" data-field="quantity" onClick="plusItem(${item.productId})">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="order-product-item-price col-lg-2 p-0">${formatNumberCart(item.price.toString())} đ</div>
                        <div class="order-product-item-quantity col-lg-2 p-0">
                            <div style="text-align: left;">
                                ${item.quantity}
                            </div>
                        </div>
                        <div class="order-product-item-finalprice col-lg-2 p-0">${formatNumberCart(item.finalPrice.toString())} đ</div>
                    </div>
                </div>
            </li>`;
    }).join('');
    $(".order-product-body ul").html(html);

    const formatPrice = price => formatNumberCart(price.toString()) + " đ";

    $(".form-calc-price-line span:contains('Tạm tính')").next("span").text(formatPrice(totalPrice));
    $(".form-calc-price-line span:contains('Phí vận chuyển')").next("span").text(formatPrice(0));
    $(".form-calc-price-line span:contains('Thuế(đã bao gồm)')").next("span").text(formatPrice(0));

    const totalOrderPrice = totalPrice + 0 + 0; // Phí vận chuyển và Thuế

    $(".form-calc-price-line span:contains('Tổng cộng')").next("span").text(formatPrice(totalOrderPrice));
}

async function populateShippingAddress(data) {
    if (data.shipProvinceAddressId != null) {
        const cityId = data.shipProvinceAddressId;
        await loadDataSelectshipDistrictAddressId(cityId);
        $("#select_province").val(cityId).trigger("change").prop('readonly', true);

        if (data.shipDistrictAddressId != null) {
            const districtId = data.shipDistrictAddressId;
            await loadDataSelectshipWardAddressId(districtId);
            $("#select_district").val(districtId).trigger("change").prop('readonly', true);

            if (data.shipWardAddressId != null) {
                $("#select_wards").val(data.shipWardAddressId).trigger("change").prop('readonly', true);
            }
        }
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

async function LoadUserData() {
    const responseData = await $.ajax({
        url: systemURL + 'account/api/detail/' + accountId,
        type: 'GET',
        async: true,
        contentType: 'application/json'
    });

    const data = responseData.data;
    const dataAccount = data[0];
    console.log(dataAccount);
    $("#account-name").val(dataAccount.name);
    $("#profile_phone").val(dataAccount.phone);

    if (dataAccount.addressCity != null) {
        const cityId = dataAccount.addressCity;
        await loadDataSelectshipDistrictAddressId(cityId);
        $("#select_province").val(cityId).trigger("change");
        if (dataAccount.addressDistrict != null) {
            const districtId = dataAccount.addressDistrict;
            await loadDataSelectshipWardAddressId(districtId);
            $("#select_district").val(districtId).trigger("change");
            if (dataAccount.addressWard != null) {
                $("#select_wards").val(dataAccount.addressWard).trigger("change");
            }
        }
    }
}

$(document).ready(async function () {
    if (accountId == 0) {
        OpenModalLogin();
    }
    
    $("#select_province").on("select2:select", async function () {
        const cityId = $(this).val();
        $("#select_district").html("");
        $("#select_wards").html("");
        await loadDataSelectshipDistrictAddressId(cityId);
    });

    $("#select_district").on("select2:select", async function () {
        const districtId = $(this).val();
        $("#select_wards").html("");
        await loadDataSelectshipWardAddressId(districtId);
    });
    if(orderId > 0){
        LoadOrderData();
    }else {
        LoadCartProductData();
    }
    $('#select_district').append(new Option("Quận huyện", 0, false, false));
    $('#select_wards').append(new Option("Phường xã", 0, false, false));

    $("#select_sex, #select_province, #select_district, #select_wards").select2();

    $('b[role="presentation"]').hide();
    $('.select2-selection__arrow').append(`<svg xmlns="http://www.w3.org/2000/svg" width="14" height="8" viewBox="0 0 14 8" fill="none">
        <path fill-rule="evenodd" clip-rule="evenodd" d="M0.200091 0.994039C0.515071 0.600314 1.08959 0.536479 1.48331 0.851458L6.99938 5.26431L12.5155 0.851458C12.9092 0.536479 13.4837 0.600314 13.7987 0.994039C14.1137 1.38776 14.0498 1.96228 13.6561 2.27726L7.5697 7.14637C7.23627 7.41312 6.76249 7.41312 6.42906 7.14637L0.342671 2.27726C-0.0510528 1.96228 -0.114888 1.38776 0.200091 0.994039Z" fill="#F7AC00"/>
    </svg>`);

    $("#createOrderBtn").click(function () {
        LoadCartProductData();
    });
});
