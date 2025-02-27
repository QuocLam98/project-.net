var currentURL = window.location.href;

// Tạo đối tượng URL từ URL hiện tại
var urlObject = new URL(currentURL);

// Lấy giá trị của tham số "id"
var forumCategoryId = urlObject.searchParams.get("id");
var formCategoryName;
$(document).ready(function () {
    //debugger;
    LoadDataTypicalCategory();
    let element = $(".pagination_default")
    LoadPagingPage("forumpost/api/CountByCategory?id=" + forumCategoryId, element, loadPost, 10);
    LoadDataFeaturedAccount();
})

async function myFunction() {
    window.location.href = '/tao-moi-bai-viet';
}
//List danh mục diễn đàn
async function LoadDataTypicalCategory() {
    var result = await httpService.getAsync("forumcategory/api/typical-category");
    $("#typical-category").html('');
    if (result.status == "200") {
        var query = result.data
        query.forEach(function (item) {
            if (item.lastedPost == null) {
                $("#typical-category").append(`
                    <li class="d-flex justify-content-between">
                        <div class="d-flex">
                            <img src=`+ item.photo + ` alt="category">
                            <a href="/danh-sach-thu-muc-dien-dan?id=${item.id}">` + item.name + `</a> 
                        </div>
                        <span class="count">` + item.countPost + `</span>
                    </li>
                `
                )
            }
        })
    }
}
//List danh sách bài viết diễn đàn 
var loadPost = async function LoadDataListForumPost() {
    var result = await httpService.getAsync("forumpost/api/list-by-category?categoryId=" + forumCategoryId + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);
    $("#list-forum-post").html('');
    if (result.status == "200") {
        var query = result.data
        $("#title_categor_forum").text(query[0].forumCategoryName);
        query.forEach(function (item) {
            let cmtAccount = ``;
            item.listAccountCmt.forEach(function (e) {
                cmtAccount += `<li class="dropdown">
                    <a class="dropdown-toggle" href="#" role="button"
                        id="dropdownMenuLink" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        <img class="img_cmt" title="${e.name}" style="width:25px;height:25px;" src="${e.accountForumTypeId != systemConstant.forum_post_type_waitting_id ? e.photo : `/images/default/authdefaultimage.png`}" alt="">
                    </a>
                </li>`;
            })
            $("#list-forum-post").append(`
                <li >
                    <div class="media">
                        <div class="d-flex">
                            <img class="rounded-circle" src=`+ item.photo + ` alt="">
                        </div>
                        <div class="media-body">
                            <div class="t_title">
                                <a href="dien-dan/chi-tiet-bai-viet/${item.id}-${item.url}">
                                    <h4>`+ item.name + `</h4>
                                </a>
                            </div>
                                            
                            <h6>
                                <i class="icon_clock_alt"></i> `+ moment(item.publishedTime, "YYYY-MM-DD HH:mm:ss").format("DD-MM-YYYY HH:mm:ss") + `
                            </h6>
                        </div>
                        <div class="media-right">
                            <ul class="nav" >
                                ${cmtAccount}            
                            </ul>
                                            
                            <a class="count" href="#"><ion-icon name="chatbubbles-outline"></ion-icon> `+ (item.commentCount ? item.commentCount : 0) + `</a>
                        </div>
                    </div>
                </li>
            `)
        })
    }
}

async function LoadDataFeaturedAccount() {
    var result = await httpService.getAsync("forumpost/api/featured-account");
    $("#featureda-account").html('');
    if (result.status == "200") {
        var query = result.data
        query.forEach(function (item) {
            $("#featureda-account").append(`
                <div class="col-12" style="display: flex;align-items: center;">
                    <div class="featured-member-photo">
                        <img src=`+ (item.photoAccount ? item.photoAccount : "/images/default/NoImage.png") + `>
                    </div>
                    <div class="featured-member-detail">
                        <div class="featured-member-name" style="font-weight: bold;">
                            <a href="${window.origin}/thong-tin-ca-nhan/` + item.username + `">` + item.authorName + `</a>
                        </div>
                        <div class="forum-post-count">
                            `+ item.totalForumPost + ` bài viết
                        </div>
                    </div>
                </div>
            `
            )
        })
    }
}