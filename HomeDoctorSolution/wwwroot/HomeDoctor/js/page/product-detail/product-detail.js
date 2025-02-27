"use strict"

//load product detail
async function loadDataProduct() {
    $(".product-info-button").html('')
    var result = await httpService.getAsync("product/api/detail-product/" + productId);
    if (result.status == "200") {
        var data = result.data[0];
        console.log(data)
        var formattedPrice = data.price.toLocaleString('vi-VN', {
            style: 'currency',
            currency: 'VND',
            minimumFractionDigits: 0,
            maximumFractionDigits: 0
        });

        // Thay thế ký tự tiền tệ từ "₫" thành "đ"
        formattedPrice = formattedPrice.replace('₫', '');

        var formattedPromotionPrice = '';
        if (data.promotionPrice !== null && data.promotionPrice !== undefined) {
            formattedPromotionPrice = data.promotionPrice.toLocaleString('vi-VN', {
                style: 'currency',
                currency: 'VND',
                minimumFractionDigits: 0,
                maximumFractionDigits: 0
            });

            // Thay thế ký tự tiền tệ từ "₫" thành "đ"
            formattedPromotionPrice = formattedPromotionPrice.replace('₫', '');
        }

        //Append info
        $(".product-info-header h3").text(data.name);
        $(".product-info-body .product-info-description").text(data.shortDescription != null ? data.shortDescription : "Nội dung chưa cập nhật");
        $(".product-info-body .product-info-price-val").text(data.price != null && data.price > 0 ? formattedPrice +"đ" :  data.productCategoryName == "Thuốc kê đơn" ? "Thuốc kê đơn" : "Liên hệ");
        $(".product-info-body .product-info-price-line-through").text(data.promotionPrice != null ? formattedPromotionPrice + "đ" : "");
        $(".product-info-text").text(data.info != null ? data.info : "Nội dung chưa cập nhật");
        $(".product-info-description-text").text(data.description != null ? data.description : "Nội dung chưa cập nhật");
        //Append Image
        $("#photos-gallery").html("");
        $("#photos-gallery").append(`
              <li class="nav-item">
                            <div id="details-img" class="gallery" data-bs-toggle="pill" data-bs-target="#img-first" aria-controls="gallery-img1">
                                <a class="fancybox" href="${data.photo}" data-fancybox-group="gallery1" title="${data.name}" alt="${data.name}"><img alt="image" src="${data.photo}" class="img-fluid img-thumb lazy lz-entered lz-loaded" data-src="${data.photo}" data-ll-status="loaded"></a>
                            </div>
                        </li>
            `);

        $(".product-info-button").append(formattedPrice == 0 ? "" : `
        <button type="button" class="btn-home buy-now" onclick="OrderNow(${data.id})" >Đặt ngay</button>
        <button type="button" class="btn-home add-to-cart" onClick="AddToCart(${data.id})">Thêm giỏ hàng</button>`)
        data.listPhotos.forEach(function (item, index) {
            $("#photos-gallery").append(`
              <li class="nav-item">
                            <div id="details-img" class="gallery" data-bs-toggle="pill" data-bs-target="#img-first" aria-controls="gallery-img1">
                                <a class="fancybox" href="${item.photo}" data-fancybox-group="gallery1" title="${item.description}" alt="${item.description}"><img alt="image" src="${item.photo}" class="img-fluid img-thumb lazy lz-entered lz-loaded" data-src="${item.photo}" data-ll-status="loaded"></a>
                            </div>
                        </li>
            `);
        });
    }
}

var openPhotoSwipe = function () {
    var galerryItem = [];
    $('.gallery').each(function () {
        var $link = $(this).find('a'),
            item = {
                src: $link.attr('href'),
                w: 724,
                h: 483,
                title: $link.attr('title')
            };
        galerryItem.push(item);
    });

    $('.fancybox').click(function (event) {

        // Prevent location change
        event.preventDefault();

        // Define object and gallery options
        var pswpElement = document.querySelectorAll('.pswp')[0];
        var options = {
            index: $('.fancybox').index(this),
            bgOpacity: 0.7,
            showHideOpacity: true
        };
        // Initialize PhotoSwipe
        var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, galerryItem, options);
        gallery.init();
    });
};
$(".fancybox").jqPhotoSwipe({
    galleryOpen: function (gallery) {
        gallery.toggleDesktopZoom();
    }
});
//This option forces plugin to create a single gallery and ignores `data-fancybox-group` attribute.
$(".forcedgallery > a").jqPhotoSwipe({
    forceSingleGallery: true
});

async function loadDataProductBrand() {
    var result = await httpService.getAsync("product/api/list-product-brand/" + productId);
    if (result.status == "200") {
        $(".list-inner").html("");
        var data = result.data;
        console.log(data);
        var html = "";
        data.forEach(function (item) {
            var formattedPrice = item.price.toLocaleString('vi-VN', {
                style: 'currency',
                currency: 'VND',
                minimumFractionDigits: 0,
                maximumFractionDigits: 0
            });

            // Thay thế ký tự tiền tệ từ "₫" thành "đ"
            formattedPrice = formattedPrice.replace('₫', 'đ');
            html += `
                        <div class="col-lg-3 col-sm-4 col-xs-6 item">
                            <div class="item-product">
                                <div class="item-photo">
                                    <a href='/chi-tiet-san-pham/${item.id}'>
                                        <img src="${item.photo}">
                                    </a>
                                </div>
                                <div class="item-body d-flex flex-column gap-8">
                                    <div class="item-name ellipsis2">
                                        <a href='/chi-tiet-san-pham/${item.id}' style="color:black">
                                            ${item.name}    
                                        </a>
                                    </div>
                                    <div class="item-price">
                                        ${item.price != null && item.price > 0 ? formattedPrice + "đ" : item.productCategoryId == 1000004 ? "Thuốc kê đơn" : "Liên hệ"}
                                    </div>
                                    <div class="item-description ellipsis3">
                                        ${item.shortDescription != null ? item.shortDescription : "Chưa có dữ liệu"}
                                    </div>
                                </div>
                                ${item.productCategoryId !== 1000004 ? `
                                <div class="item-hover">
                                    <div class="hover_box">
                                        <button class="btn-home add-to-cart w-100" onClick="AddToCart(${item.id})">Thêm giỏ hàng</button>
                                    </div>
                                </div>` : ''}
                            </div>
                        </div>`
        });
        $(".list-inner").append(html);
    }
}

async function AddOrder() {
    if (accountId == 0) {
        OpenModalLogin();
        //Swal.fire("Cảnh báo", "Bạn chưa đăng nhập vui lòng đăng nhập để sử dụng", "warning").then(function () {
        //    OpenModalLogin();
        //});
    } else {
        var order = {
            "accountId": accountId,
            "totalShipFee": "20000",
            "tax": "0",
            "orderDetails": [{
                "productId": productId,
                "quantity": 1
            }]
        }
        var result = await httpService.postAsync("orders/api/addOrder", order);
        if (result.status == "201") {
            Swal.fire("Thành công", "Bạn đã tạo đơn hàng thành công", "success").then(function () {
                location.href = "/don-hang/" + result.data[0].id;
            });
        } else {
            Swal.fire("Thất bại", "Đơn hàng của bạn chưa được tạo. Vui lòng thử lại", "error")
        }
    }
}

async function AddToCart(productId) {
    const result = await httpService.getAsync(`product/api/detail/${productId}`);

    if (accountId > 0) {
        let responseData = await httpService.postAsync("cartproduct/api/addcartproduct/", {
            productId: result.data[0].id,
            action: "create",
        });
        if (responseData.status == 201 && responseData.message === "CREATED") {
            Swal.fire(
                'Thành công!',
                'Sản phẩm đã thêm vào giỏ hàng',
                'success'
            );
        }
        LoadCart();
    } else {
        const data = result.data[0];
        console.log(data);

        let orderDetails = localStorage.getItem("cart") || '[]';
        orderDetails = JSON.parse(orderDetails);

        const existingCartItem = orderDetails.find(c => c.productId == productId);

        if (existingCartItem) {
            existingCartItem.quantity += 1;
            existingCartItem.price = data.price * existingCartItem.quantity;
        } else {
            orderDetails.push({
                photo: data.photo,
                productId: data.id,
                quantity: 1,
                price: data.price,
                name: data.name
            });
        }

        localStorage.setItem("cart", JSON.stringify(orderDetails));

        // LoadCart();
    }
}

async function OrderNow(productId) {
    await httpService.postAsync("cartproduct/api/addcartproduct/", {
        productId: productId,
        quantity: 1
    });
    LoadCart();
    window.location.href = '/tao-don-hang';
}

$(document).ready(function () {
    $.when(loadDataProduct()).done(function () {
        openPhotoSwipe();
    });
    loadDataProductBrand();
});

