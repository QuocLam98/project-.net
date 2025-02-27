$(document).ready(async function () {
    moment.locale("vi");
    /*let element = $(".pagination_default");*/
    /*LoadPagingPage("post/api/count", element, loadData);*/
    //loadCategoryPost();
    $.when(await loadCategoryPost()).done(function () {
        LoadDataPost();
    })
})

var arrCategory = [];
var pageIndex = 1;
async function LoadDataPost() {
    var obj = {
        keywords: $("#search_post_page").val(),
        pageIndex: pageIndex,
        pageSize: 9,
        categoryPostId: arrCategory.toString()
    }
    var responseData = await httpService.postAsync("post/api/searching-post", obj);
    var result = responseData.dataSource;
    if (result.length === 0) {
        
        $("#lst_post").html('<div class="d-flex flex-column align-items-center"><img style="width:400px;height:auto;" src="/images/default/not-found.png"><p>Không tìm thấy kết quả nào</p></div>')
        $("#list_post").css(
            {
                "justify-content": "center"
            }
        );;
        return;
    }
    var content = ``;
    result.forEach(function (item) {
        var img = item.photo ? item.photo : "/images/default/img_default.png"
        content += `<div class="item_post col-sm-12 col-xl-4 col-md-6 col-lg-6">
                        <div class="item_box">
                            <div class="img_post">
                                <img src="${item.photo}" />
                            </div>
                            <a class="infor_post" href="/chi-tiet-tin-tuc/${item.id}">
                                <p class="name_post">${item.name}</p >
                                <p class="des_post">${item.description}</p>
                                <p class="date_post">${moment(item.publishedTime).format('DD/MM/YYYY')}</p>
                            </a>
                        </div>
                    </div>`
    });
    $("#lst_post").html('');
    $("#lst_post").append(content);
    initPagination(responseData.totalPages, '#pagination-post');
}

$("#search_post_page").on("keyup", function (e) {
    let key = e.which;
    if (key == 13) {
        pageIndex = 1;
        LoadDataPost();
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
        LoadDataPost();
    }
    else if ($(this).hasClass('paging-last-item')) {
        pageIndex = dataSource.total;
        LoadDataPost();
    }
    else if ($(this).hasClass('paging-previous')) {
        pageIndex = pageIndex - 1;
        LoadDataPost();
    }
    else if ($(this).hasClass('paging-next')) {
        pageIndex = pageIndex + 1;
        LoadDataPost();
    }
    else {
        if (!($(this).attr('class').includes('active'))) {
            $(".page-item").removeClass('active');
            $(this).addClass('active');
            pageIndex = parseInt($(this).text());
            LoadDataPost();
        }
    }
})


var loadData = async function LoadDataListPost() {
    var result = await httpService.getAsync("post/api/listpaging?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "");
    $("#lst_post").html('');
    if (result.status == "200") {
        var content = ``;
        result.data.forEach(function (item) {
            var img = item.photo ? item.photo : "/images/default/img_default.png"
            content += `<div class="item_post col-sm-12 col-xl-4 col-md-6 col-lg-6">
                       
                        <div class="item_box">
                            <div class="img_post">
                                <a href="chi-tiet-tin-tuc/${item.id}"><img src="${item.photo}" /></a>
                            </div>
                            <div class="infor_post">
                                 <a href="chi-tiet-tin-tuc/${item.id}"><p class="name_post">${item.name}</p ></a>
                                 <a href="chi-tiet-tin-tuc/${item.id}"><p class="des_post">${item.description}</p></a>
                                 <a href="chi-tiet-tin-tuc/${item.id}"><p class="date_post">${moment(item.publishedTime).format('D MMM, YYYY')}</p></a>
                            </div>
                        </div>
                        </a>
                    </div>`
        });
        $("#lst_post").append(content);
    }
}
async function loadCategoryPost() {
    //debugger;
    var result = await httpService.getAsync("postcategory/api/list");
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


$("#list_cate").on("change", ".category_list", function () {
    pageIndex = 1;
    var value = parseInt($(this).attr('value'));
    if ($(this).is(":checked")) {
        arrCategory.push(value);
    }
    else {
        arrCategory = arrCategory.filter(x => x != value);
    }
    LoadDataPost();
})
$("#list_cate_modal").on("change", ".category_list_modal", function () {
    pageIndex = 1;
    var value = parseInt($(this).attr('value'));
    console.log(value);
    if ($(this).is(":checked")) {
        arrCategory.push(value);
    }
    else {
        arrCategory = arrCategory.filter(x => x != value);
    }
    LoadDataPost();
})