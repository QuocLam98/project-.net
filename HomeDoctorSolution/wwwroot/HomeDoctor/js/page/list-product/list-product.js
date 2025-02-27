
var arrCategory = [];
var pageIndex = 1;
async function LoadDataProduct() {
    var obj = {
        keywords: $("#search_product_page").val(),
        pageIndex: pageIndex,
        pageSize: 9,
        categoryProductId: arrCategory.toString()
    };
    var responseData = await httpService.postAsync("product/api/searching-product", obj);

    var result = responseData.dataSource;
    console.log(result);

    if (result.length === 0) {
        $("#lst_product").html('<div class="d-flex flex-column align-items-center"><img style="width:400px;height:auto;" src="/images/default/not-found.png"><p>Không tìm thấy kết quả nào</p></div>')
        $("#list_product").css(
                {
                    "justify-content": "center"
                }
            );
        return;
    }

    var content = ``;
    $("#lst_product").html('');
    result.forEach(function(item) {
        var img = item.photo ? item.photo : "/images/default/img_default.png"
        var price = item.productCategoryId === 1000003 ? customFormatValue(item.price, 3, ".") + "đ" : "Thuốc kê đơn"
        var addButtonHtml = item.productCategoryId === 1000003 ?
            `<div class="hover_box">
                <button class="btn_homeD2" onClick="AddToCart(${item.id})">Thêm giỏ hàng</button>
            </div>` : '';

        content += `<div class="item_product ">
            <div class="item_box">
                <div class="img_product">
                    <a href="chi-tiet-san-pham/${item.id}"><img src="${img}" /></a>
                </div>
                <div class="infor_product">
                    <a href="chi-tiet-san-pham/${item.id}"><p class="name_product" title="${item.name}">${item.name}</p></a>
                    <p class="price_product" style="min-height: 28px"><span>${price}</span></p>
                    <p class="des_product" title="${item.name}">${item.info ? item.info : ""}</p>
                </div>
                <div class="hover_product">
                    ${addButtonHtml}
                </div>
            </div>
        </div>`;
    });
    $("#lst_product").append(content);
    initPagination(responseData.totalPages, '#pagination-post');
}


function searchButton() {
    pageIndex = 1;
    LoadDataProduct();
}
$("#search_product_page").on("keyup", function (e) {
    let key = e.which;
    if (key == 13) {
        pageIndex = 1;
        LoadDataProduct();
    }
})
function initPagination(totalPage, element) {
    if (totalPage > 0) {
        let html = "";
        let startPage;
        if (totalPage <= 3) {
            startPage = 1;
        }
        else {
            if (totalPage == pageIndex) {
                startPage = totalPage - 2;
            }
            else {
                startPage = pageIndex == 1 ? 1 : pageIndex - 1;
            }
        }
        let endPage = startPage + 2 <= totalPage ? startPage + 2 : totalPage;
        if (pageIndex > 1) {
            html += `<li class="page-item paging-first-item"><a href="#!" aria-label="Previous"  class="page-link"><i class="fa fa-angle-double-left"></i></a></li>
                                    <li class="page-item paging-previous"><a href="#!" aria-label="Previous"  class="page-link"><i class="fa fa-angle-left"></i></a></li>`;
        }
        for (var i = startPage; i <= endPage; i++) {
            if (i > 0) {
                html += `<li class="page-item ${i == pageIndex ? 'active' : ''}" aria-current="page">
                                    <a class="page-link">${i}</a>
                                </li>`
            }
        }
        if (pageIndex < totalPage) {
            html += `<li class="page-item paging-next"><a href="#!" aria-label="Next"  class="page-link"><i class="fa fa-angle-right"></i></a></li>
                                    <li class="page-item paging-last-item"><a href="#!" aria-label="Next"  class="page-link"><i class="fa fa-angle-double-right"></i></a></li>`
        }
        $(element).html(html);
    }
    else {
        $(element).html("");
    }
}
$("#pagination-post").on("click", ".page-item", function (e) {
    e.preventDefault();
    window.scrollTo(0, 200);
    if ($(this).hasClass('paging-first-item')) {
        pageIndex = 1;
        LoadDataProduct();
    }
    else if ($(this).hasClass('paging-last-item')) {
        pageIndex = dataSource.total;
        LoadDataProduct();
    }
    else if ($(this).hasClass('paging-previous')) {
        pageIndex = pageIndex - 1;
        LoadDataProduct();
    }
    else if ($(this).hasClass('paging-next')) {
        pageIndex = pageIndex + 1;
        LoadDataProduct();
    }
    else {
        if (!($(this).attr('class').includes('active'))) {
            $(".page-item").removeClass('active');
            $(this).addClass('active');
            pageIndex = parseInt($(this).text());
            LoadDataProduct();
        } 
    }
})
//var loadData = async function LoadDataListProduct() {
//    var result = await httpService.getAsync("product/api/listpaging?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "");
//    $("#lst_product").html('');
//    if (result.status == "200") {
//        var content = ``;
//        result.data.forEach(function (item) {
//            var img = item.photo ? item.photo : "/images/default/img_default.png"
//            content += `<div class="item_product ">
//                        <div class="item_box">
//                            <div class="img_product">
//                                <a href="chi-tiet-san-pham/${item.id}"><img src="${img}" /></a>
//                            </div>
//                            <div class="infor_product">
//                                <a href="chi-tiet-san-pham/${item.id}"><p class="name_product" title="${item.name}">${item.name}</p ></a>
//                                <p class="price_product"><span>${customFormatValue(item.price, 3, ".")}</span> đ</p>
//                                <p class="des_product" title="${item.name}">${item.info}</p>
//                            </div>
//                            <div class="hover_product">
//                                <div class="hover_box">
//                                    <button class="btn_homeD2" onClick="AddToCart(${item.id})">Thêm giỏ hàng</button>
//                                </div>
//                            </div>
//                        </div>
//                    </div>`
//        });
//        $("#lst_product").append(content);
//    }
//}

async function loadCategoryProduct() {
    var result = await httpService.getAsync("productcategory/api/list");
    let content = ``;
    let contentModal = ``;
    $("#list_cate").html('');
    $("#list_cate_modal").html('');
    result.data.forEach(function (item) {
        /*var img = item.photo ? item.photo : "/images/default/img_default.png"*/
        content += `<li><input type="checkbox" class="category_list" value="${item.id}"/>${item.name} </li>`;
        contentModal += `<li><input type="checkbox" class="category_list_modal" value="${item.id}"/>${item.name} </li>`;
    });
    $("#list_cate").append(content);
    $("#list_cate_modal").append(contentModal);
}

$(document).ready(async function () {
    $.when(await loadCategoryProduct()).done(function () {
        LoadDataProduct();
    })
   

})

$("#list_cate").on("change", ".category_list", function () {
    pageIndex = 1;
    var value = parseInt($(this).attr('value'));
    console.log(value)
    if ($(this).is(":checked")) {
        arrCategory.push(value);
    }
    else {
        arrCategory = arrCategory.filter(x => x != value);
    }
    LoadDataProduct();
})
//$("#list_cate_modal").on("change", ".category_list_modal", function () {
//    pageIndex = 1;
//    var value = parseInt($(this).attr('value'));
//    console.log(value)
//    if ($(this).is(":checked")) {
//        arrCategory.push(value);
//    }
//    else {
//        arrCategory = arrCategory.filter(x => x != value);
//    }
//    LoadDataProduct();
//})

$("#btn_apply").on('click', function () {
    pageIndex = 1;
    arrCategory = $('.category_list_modal:checked').map(function () {
        return this.value;
    }).get();
    LoadDataProduct();
})
$("#btn_distroy").on('click', function () {
    $('.category_list_modal').prop('checked',false);
    arrCategory = [];
    pageIndex = 1;
    LoadDataProduct();
})
function customFormatValue(value, number, char) {
    if (number == undefined || number == null) {
        number = 3;
    }
    if (char == undefined || char == null) {
        char = '.';
    }
    value = value != null ? value.toString() : "";
    var pattern = new RegExp(`\\B(?=(\\d{${number}})+(?!\\d))`, 'g');
    return value
        .replace(/\D{-}/g, '')
        .replace(pattern, char);
}