'use strict';
function showCardInfo() {
    $(".card-info").removeClass("card-hide-info");
    $(".card-chat").addClass("card-hide-chat");
    $(".card-order").addClass("card-hide-order");
}
function closeCardInfo() {
    $(".card-info").addClass("card-hide-info");
}

function showCardOrder() {
    $(".card-order").removeClass("card-hide-order");
    $(".card-chat").addClass("card-hide-chat");
    $(".card-info").addClass("card-hide-info");
}
function closeCardOrder() {
    $(".card-order").addClass("card-hide-order");
}
$(document).ready(async function () {
    LoadInfo()
    LoadCart();
    if (accountId > 0) {
        await getProductFromLocalStorage();
        var profile = localStorage.getItem("profile");
        var countBookingWaitDropdown = localStorage.getItem("countBookingWait")
        var name = jQuery.parseJSON(profile);
        $("#name-user").text(name.name);
        if (countBookingWaitDropdown != null) {
            $(".countBooking-value").text("(" + countBookingWaitDropdown + ")")
        }
    }
    $(".card-info").addClass("card-hide-info");
    $(".card-chat").addClass("card-hide-chat");
    $(".card-order").addClass("card-hide-order");

    if ($(window).width() < 768) {
        $('.info-button').removeAttr('onmouseover').attr('onclick', 'showCardInforWv()');

    }
    
});
function showCardInforWv() {
    window.location.href = "/app/chi-tiet-nguoi-dung";
}
async function getProductFromLocalStorage() {
    let orderDetails = localStorage.getItem("cart") || '[]';
    orderDetails = JSON.parse(orderDetails);

    const addProductPromises = orderDetails.map(async (orderDetail) => {
        try {
            const responseData = await httpService.postAsync("cartproduct/api/addcartproduct/", {
                productId: orderDetail.productId,
                action: "create",
                quantity: orderDetail.quantity
            });

            // Nếu bạn muốn hiển thị thông báo khi sản phẩm được thêm vào giỏ hàng, có thể thêm mã thông báo ở đây
            // if (responseData.status == 201 && responseData.message === "CREATED") {
            //     toastr.success(
            //         'Thành công!',
            //         'Sản phẩm đã thêm vào giỏ hàng',
            //         {timeOut: 2000, extendedTimeOut: 1000, closeButton: true, closeDuration: 1000}
            //     );
            // }
        } catch (error) {
            console.error("Error adding product to cart:", error);
        }
    });

    // Chờ tất cả các promise hoàn tất
    await Promise.all(addProductPromises);

    // Xóa giỏ hàng từ localStorage khi hoàn thành
    localStorage.removeItem("cart");

    // Tải lại giỏ hàng (nếu cần)
    LoadCart();
    LoadCartProductData();
}

async function LoadCart() {
    try {
        let cartData = [];

        if (accountId > 0) {
            const responseData = await $.ajax({
                url: `${systemURL}cart/api/Search/${accountId}`,
                type: 'GET',
                async: true,
                contentType: 'application/json',
            });

            cartData = responseData.data[0];
        } else {
            const cart = localStorage.getItem("cart");
            if (cart) {
                cartData = JSON.parse(cart);
            }
        }

        const items = cartData.reduce((total, item) => total + item.quantity, 0);
        $(".item-count-cart").text(items);

        if (items > 0) {
            const totalCartPrice = cartData.reduce((total, item) => total + (item.price * item.quantity), 0);

            $("#orderButton").show()
            $(".card-order-button").html(`
                <div class="card-order-total">
                    <span>
                        Tạm tính:
                    </span>
                    <span class="card-order-total-price">
                        0 <span>đ</span>
                    </span>
                </div>
                <div class="card-order-button-buy" onclick="window.location.href='/tao-don-hang'">
                    Đặt ngay
                </div>
            `
            )

            const html = cartData.map(item => {
                const totalPrice = item.price;
                return `
                    <div class="card-order-item" data-id="${item.productId}">
                        <div class="card-product-img">
                        <a href="/chi-tiet-san-pham/${item.productId}">
                            <img src="${item.photo}" />
                        </a>
                        </div>
                        <div class="card-product-value">
                            <a href="/chi-tiet-san-pham/${item.productId}">
                            
                                <div class="card-product-name ellipsis2">
                                    ${item.name}
                                </div>
                            </a>
                            <div class="card-product-price">
                                ${formatNumberCart(totalPrice.toString())}<span> đ</span>
                            </div>
                            <div class="card-order-footer">
                                <div class="card-order-quantity">
                                    <div class="card-order-minus" onClick="minusItem(${item.productId})">
                                        <img src="/homedoctor/img/header/minus.svg" />
                                    </div>
                                    <input type='number' value="${item.quantity}" />
                                    <div class="card-order-plus" onClick="plusItem(${item.productId})">
                                        <img src="/homedoctor/img/header/plus.svg" />
                                    </div>
                                </div>
                                <div class="card-order-delete" onclick="deleteItemCart(${item.productId})">
                                    <img src="/homedoctor/img/header/delete.svg" />
                                </div>
                            </div>
                        </div>
                    </div>`;
            }).join('');

            $(".card-list-order").html(html);
            $(".card-order-total-price").text(formatNumberCart(totalCartPrice.toString()) + " đ");
            $(".no-product").addClass("d-none");
        } else {
            $("#card-list-order").html(`
            <div class="no-product">
                <img src="/homedoctor/img/header/product-0.svg" />
                <span>Chưa có sản phẩm trong giỏ hàng</span>
            </div>
        `);
        }
    } catch (error) {
        console.error(error.message);
    }
}
async function LoadCartProductData() {
    if (accountId > 0) {
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

            const html = data.map(item => {
                items += item.quantity;
                const totalPrice = item.price * item.quantity;
                totalCartPrice += totalPrice;

                return `
                <li id="${item.productId}">
                    <div class="media">
                        <div class="order-product-item">
                            <div class="order-product-item-info-body align-items-center col-lg-6 col-xs-12 gap-22">
                                <img src="${item.photo}" />
                                <div class="order-product-item-info">
                                    <div class="order-product-item-info-name">${item.name}</div>
<!--                                    <div class="order-product-item-info-description">${item.description}</div>-->
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
            }).join('');
            $(".order-product-body ul").html(html);
            $(".form-calc-price-line span:contains('Tạm tính')").next("span").text(formatNumberCart(totalCartPrice.toString()) + " đ");
            $(".form-calc-price-line span:contains('Phí vận chuyển')").next("span").text(0 + " đ");
            $(".form-calc-price-line span:contains('Thuế(đã bao gồm)')").next("span").text(0 + " đ");
            let totalOrderPrice =
                totalCartPrice +
                //Phí vận chuyển
                0 +
                //Thuế
                0
            $(".form-calc-price-line span:contains('Tổng cộng')").next("span").text(formatNumberCart(totalOrderPrice.toString()) + " đ");
            $(".card-order-total-price").text(formatNumberCart(totalCartPrice.toString()) + " đ");
        } catch (error) {
            // console.error(error.message);
        }
    }
}
async function LoadInfo(){
    var result = await httpService.getAsync("orders/api/count-orders");
    $(".info-cart div").text(result[0].count)
    $(".info-ship div").text(result[1].count)
    // $(".info-done div").text(result[0].count)
    $(".info-cancel div").text(result[2].count)
}
