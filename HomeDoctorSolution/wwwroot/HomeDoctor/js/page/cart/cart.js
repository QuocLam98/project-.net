async function AddToCart(productId) {
    try {
        const result = await httpService.getAsync(`product/api/detail/${productId}`);

        let responseData;

        if (accountId > 0) {
            responseData = await httpService.postAsync("cartproduct/api/addcartproduct/", {
                productId: productId,
                // action: "create",
                quantity: 1
            });

            if (responseData.status == 201 && responseData.message == "CREATED") {
                updateCartUI();
                toastr.success(
                    'Thành công!',
                    'Sản phẩm đã thêm vào giỏ hàng',
                    {
                        timeOut: 2000,
                        closeButton: true,
                        positionClass: 'toast-top-right'
                    }
                );
            }
            LoadCart();
        } else {
            const data = result.data[0];
            const orderDetails = JSON.parse(localStorage.getItem("cart")) || [];
            const existingCartItem = orderDetails.find(item => item.productId === productId);

            if (existingCartItem) {
                existingCartItem.quantity += 1;
                existingCartItem.totalprice = data.price * existingCartItem.quantity;
            } else {
                orderDetails.push({
                    photo: data.photo,
                    productId: data.id,
                    quantity: 1,
                    price: data.price,
                    name: data.name,
                    totalPrice: 0
                });
                updateCartUI();
            }

            toastr.success(
                'Thành công!',
                'Sản phẩm đã thêm vào giỏ hàng',
                {
                    timeOut: 2000,
                    closeButton: true,
                    positionClass: 'toast-top-right'
                }
            );
            localStorage.setItem("cart", JSON.stringify(orderDetails));
            await LoadCart();
        }
    } catch (error) {
        console.error(error.message);
    }
}

function updateCartUI() {
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

// Tạo một hàm tính tổng giá trị giỏ hàng
function calculateTotalPrice(dataItem) {
    let totalPrice = 0;
    dataItem.forEach(function (item) {
        totalPrice += item.price;
    });
    return totalPrice;
}

async function updateCartProductQuantity(id, quantity) {
    if (accountId > 0) {
        // const result = await httpService.getAsync(`product/api/detail/${id}`);
        await httpService.postAsync("cartproduct/api/addcartproduct/", {
            productId: id,
            // action: "create",
            quantity: quantity,
        });
    } else {
        //lấy dữ liệu từ localStorage
        var getItem = localStorage.getItem("cart") || '[]';
        var dataItem = JSON.parse(getItem);

        // tìm sản phẩm trong giỏ hàng theo id
        var checkItem = dataItem.find(x => x.productId == id);
        checkItem.totalPrice = (checkItem.price / checkItem.quantity) * quantity;
        checkItem.quantity = quantity;

        // cập nhật localStorage
        localStorage.setItem("cart", JSON.stringify(dataItem));

        // tính tổng giá trị giỏ hàng
        var totalPrice = calculateTotalPrice(dataItem);

        // cập nhật HTML
        $(`.card-order-item[data-id=${id}]`).find("input").val(quantity);
        $(`.card-order-item[data-id=${id}]`).find(".card-product-price").text(formatNumberCart(checkItem.price.toString()) + " đ");
        $(".card-order-total-price").text(formatNumberCart(totalPrice.toString()) + " đ");

        // Kiểm tra giỏ hàng có rỗng không và hiển thị hình ảnh nếu cần
        if (dataItem.length === 0) {
            $(".card-list-order").html(`
                <div class="no-product">
                    <img src="/homedoctor/img/header/product-0.svg" />
                    <span>Chưa có sản phẩm trong giỏ hàng</span>
                </div>
            `);
        }
    }

    // Load giỏ hàng sau khi cập nhật
    LoadCart();
}

async function plusItem(id) {
    if (accountId > 0) {
        await updateCartProductQuantity(id, 1);
    } else {
        var value = $(`.card-order-item[data-id=${id}]`).find("input").val();
        value++;
        updateCartProductQuantity(id, value);
    }
}

async function minusItem(id) {
    if (accountId > 0) {
        const itemCount = parseInt($(".item-count-cart").text(), 10);
        if (itemCount === 1) {
            await deleteItemCart(id)
        }
        else{
            await updateCartProductQuantity(id, -1);
        }
    } else {
        var value = $(`.card-order-item[data-id=${id}]`).find("input").val();
        value--;
        if (value <= 0) {
            // Nếu số lượng giảm xuống 0 hoặc âm, xóa sản phẩm khỏi local storage và giỏ hàng
            deleteItemCart(id);
        } else {
            updateCartProductQuantity(id, value);
        }
    }
}

async function deleteItemCart(id) {
    if (accountId > 0) {
        await updateCartProductQuantity(id, 0);
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
    }

    //lấy dữ liệu từ localStorage
    let getItem = localStorage.getItem("cart") || '[]';
    let dataItem = JSON.parse(getItem);

    // xóa sản phẩm khỏi local storage và giỏ hàng
    const index = dataItem.findIndex(x => x.productId == id);
    if (index !== -1) {
        dataItem.splice(index, 1);
        localStorage.setItem("cart", JSON.stringify(dataItem));
    }

    // tính tổng giá trị giỏ hàng
    let totalPrice = calculateTotalPrice(dataItem);

    // cập nhật HTML
    $(`.card-order-item[data-id=${id}]`).remove();
    $(".card-order-total-price").text(formatNumberCart(totalPrice.toString()) + " đ");

    // Kiểm tra giỏ hàng có rỗng không và hiển thị hình ảnh nếu cần
    if (dataItem.length === 0) {
        $(".card-list-order").append(`
            <div class="no-product">
                <img src="/homedoctor/img/header/product-0.svg" />
                <span>Chưa có sản phẩm trong giỏ hàng</span>
            </div>
        `);
        $("#orderButton").hide()
        $(".card-order-button").html("");
    } else {
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
    }
    
    $(".item-count-cart").text(dataItem.length);
    LoadCart();
}

$(document).ready(function () {
    LoadCart();
    const itemCount = parseInt($(".item-count-cart").text(), 10);
    if (itemCount === 0) {
        $("#card-list-order").html(`
            <div class="no-product">
                <img src="/homedoctor/img/header/product-0.svg" />
                <span>Chưa có sản phẩm trong giỏ hàng</span>
            </div>
        `);
        $("#orderButton").hide()
    } else {
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
    }
});
