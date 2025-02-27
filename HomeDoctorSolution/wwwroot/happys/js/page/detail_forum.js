

// Lấy URL từ địa chỉ hiện tại
var currentURL = window.location.href;

// Tạo đối tượng URL từ URL hiện tại
var urlObject = new URL(currentURL);
var editor;

// Lấy giá trị của tham số "1000001"
var id = forumId;
var textWithDashes;
$(document).ready(async function () {

    await loadDetailForum();
    await loadCategorySideBar();
    await LoadDataFeaturedAccount();
    CKEDITOR.ClassicEditor.create(document.getElementById("postContent"), {
        toolbar: {
            items: [
                'undo', 'redo',
                'heading', '|',
                'bold', 'italic', 'underline',
                'alignment',
                'bulletedList', 'numberedList', 'todoList',
                'outdent', 'indent',
                'link', 'insertImage', 'mediaEmbed', 'blockQuote', 'insertTable',
                'fontSize', 'fontFamily', 'fontColor',
            ],
            shouldNotGroupWhenFull: false
        },
        // Changing the language of the interface requires loading the language file using the <script> tag.
        language: 'vi',
        ckfinder: {
            uploadUrl: `${systemURL}api/file-explorer/upload-file-ck`,
        },
        list: {
            properties: {
                styles: true,
                startIndex: true,
                reversed: true
            }
        },
        heading: {
            options: [
                { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
                { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
                { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' },
                { model: 'heading3', view: 'h3', title: 'Heading 3', class: 'ck-heading_heading3' },
                { model: 'heading4', view: 'h4', title: 'Heading 4', class: 'ck-heading_heading4' },
                { model: 'heading5', view: 'h5', title: 'Heading 5', class: 'ck-heading_heading5' },
                { model: 'heading6', view: 'h6', title: 'Heading 6', class: 'ck-heading_heading6' }
            ]
        },
        placeholder: 'Nhập bình luận',
        // https://ckeditor.com/docs/ckeditor5/latest/features/font.html#configuring-the-font-family-feature
        fontFamily: {
            options: [
                'default',
                'Arial, Helvetica, sans-serif',
                'Courier New, Courier, monospace',
                'Georgia, serif',
                'Lucida Sans Unicode, Lucida Grande, sans-serif',
                'Tahoma, Geneva, sans-serif',
                'Times New Roman, Times, serif',
                'Trebuchet MS, Helvetica, sans-serif',
                'Verdana, Geneva, sans-serif'
            ],
            supportAllValues: true
        },
        fontSize: {
            options: [10, 12, 14, 'default', 18, 20, 22],
            supportAllValues: true
        },
        // Be careful with the setting below. It instructs CKEditor to accept ALL HTML markup.
        // https://ckeditor.com/docs/ckeditor5/latest/features/general-html-support.html#enabling-all-html-features
        htmlSupport: {
            allow: [
                {
                    name: /.*/,
                    attributes: true,
                    classes: true,
                    styles: true
                }
            ]
        },
        htmlEmbed: {
            showPreviews: true
        },
        link: {
            decorators: {
                addTargetToExternalLinks: true,
                defaultProtocol: 'https://',
                toggleDownloadable: {
                    mode: 'manual',
                    label: 'Downloadable',
                    attributes: {
                        download: 'file'
                    }
                }
            }
        },
        mention: {
            feeds: [
                {
                    marker: '@',
                    feed: [
                        '@apple', '@bears', '@brownie', '@cake', '@cake', '@candy', '@canes', '@chocolate', '@cookie', '@cotton', '@cream',
                        '@cupcake', '@danish', '@donut', '@dragée', '@fruitcake', '@gingerbread', '@gummi', '@ice', '@jelly-o',
                        '@liquorice', '@macaroon', '@marzipan', '@oat', '@pie', '@plum', '@pudding', '@sesame', '@snaps', '@soufflé',
                        '@sugar', '@sweet', '@topping', '@wafer'
                    ],
                    minimumCharacters: 1
                }
            ]
        },
        // The "super-build" contains more premium features that require additional configuration, disable them below.
        // Do not turn them on unless you read the documentation and know how to configure them and setup the editor.
        removePlugins: [
            'CKBox',
            'EasyImage',
            'RealTimeCollaborativeComments',
            'RealTimeCollaborativeTrackChanges',
            'RealTimeCollaborativeRevisionHistory',
            'PresenceList',
            'Comments',
            'TrackChanges',
            'TrackChangesData',
            'RevisionHistory',
            'Pagination',
            'WProofreader',
            'MathType'
        ]
    }).then(newEditor => {
        editor = newEditor;
    }).catch(error => {
        console.error(error);
    });

    //editor = CKEDITOR.replace('postContent', {
    //    toolbar: [
    //        { name: 'styles', items: ['Styles', 'Format'] },
    //        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
    //        { name: 'editing', items: ['Find', 'SelectAll'] },
    //        { name: 'insert', items: ['Image', 'Table'] },
    //        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline'] },
    //        { name: 'paragraph', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'NumberedList', 'BulletedList',] },
    //    ]
    //});
    //CKEDITOR.on('dialogDefinition', function (e) {
    //    var dialogName = e.data.name;
    //    var dialog = e.data.definition.dialog;
    //    dialog.on('show', function () {
    //        setupCKUploadFile();
    //    });
    //});

    let element = $(".pagination_default")

    LoadPagingPage("forumpost/api/countCmt?id=" + id, element, loadCmt, 6);
})

//function convertToSlug(input) {

//    var slug = input.normalize("NFD").replace(/[\u0300-\u036f]/g, "").replace(/[^a-zA-Z0-9\s]/g, '');

//    slug = slug.replace(/\s+/g, '-').toLowerCase();

//    return slug;
//}
//hàm load trang chi tiết forum
async function loadDetailForum() {
    var dataSourse = await httpService.getAsync("forumpost/api/DetailForumEndUser/" + forumId);
    var data = dataSourse.data[0];
    var img = data.forumPostAuthorPhoto ? data.forumPostAuthorPhoto : "/images/default/NoImage.png";
    var content = `
        <div class="row">
            <div class="col-lg-9 ">
                <div class="forum-post-top">
                    <a class="author-avatar" href="#">
                        <img src="`+ img + `" alt="">
                    </a>
                    <div class="top-post-author d-flex flex-column">
                        <div class="forum-author-meta d-flex">
                            <a href="/danh-sach-thu-muc-dien-dan?id=${data.forumCategoryId}"><span>` + data.forumCategoryName + `</span></a>
                            <span>`+ moment(data.publishedTime).format("DD/MM/YYYY HH:mm") + `</span>
                        </div>
                        <div class="d-flex " style="gap:8px;">
                            <a class="author-name" href="/thong-tin-ca-nhan/${data.forumAccountUserName}">` + data.forumPostAuthorName + `</a>
                            ${data.forumPostStatusId == systemConstant.forum_post_type_waitting_id ? `<span style="color:#fb5401;">Bài viết chờ duyệt</span>` : ``}
                        </div>
                </div>
            </div>
            </div>
            <div class="col-lg-3">
            </div>
        </div>

        <!-- Forum post content -->
        <div class="q-title">
            <h1>`+ data.name + `</h1>
        </div>
        <div class="forum-post-content">
            <div class="content">
                `+ data.text + `
            </div>
            <div class="forum-post-btm d-flex flex-column">
                <div class=" col-12 justify-content-between p-0 d-none">
                    <div class="taxonomy forum-post-tags g-10 ">
                        <div class="icon_tag">
                            <a><img class="mr-0" src="/happys/img/detail_forum/icon_tag.svg" /></a>
                        </div>

                        <div class="tag_list_forum d-flex g-10">
                            <button class="btn btnHS2 tag_item">Swagger</button>
                            <button class="btn btnHS2 tag_item">Docy</button>
                            <button class="btn btnHS2 tag_item">Business</button>
                        </div>
                    </div>
                    <div class="taxonomy forum-post-cat d-flex forum_social">
                        <div class="icon_like_forum d-flex align-items-center">
                            <img src="/happys/img/detail_forum/icon_like.svg" />
                            <div class="content_like">
                                <div class="content_like"><p>`+ data.likeCount + ` Yêu thích</p></div>
                            </div>
                        </div>
                        <div class="icon_comment_forum d-flex align-items-center">
                            <img src="/happys/img/detail_forum/icon_cmt.svg" />
                            <div class="content_comment"><p>`+ data.commentCount + ` Bình luận</p></div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="action-button-container action-btns">
                <h1 id="heade_cmt">Tất cả bình luận</h1>
            </div>
       </div>
    `;
    let contentCmtText = accountID > 0 ? `
                <div class="col-lg-12 mt-3 p-0">
                    <textarea id="postContent" rows="50" style="display: none; visibility: hidden;" class="form-control form-control mb-3 mb-lg-0 text_editor"></textarea>
                    <input type="file" class="d-none" id="postUpload"/>
                </div>
                <div class="btn_comment d-flex flex-row justify-content-between">
                    <label for="confirm" class="d-flex flex-row align-items-center justify-content-center mt-2">
                        <input class="input_checkbox" name="hideaccount" id="confirm" type="checkbox" value="1000002">
                        <span>Ẩn thông tin của bạn với người dùng khác</span>
                    </label>
                    <button class="btn btnHS2" id="btn_cmt">Bình luận</button>
                </div>`: `<a class="btn btnHS2" href = "/dang-nhap">Đăng nhập để bình luận</a>`
       ;

    $("#detail_forum_header").append(content);
    $("#comment_text").append(contentCmtText);
}
// load toàn bộ bình luận
var loadCmt = async function loadAllComent() {
    try {
        var dataComment = await httpService.getAsync("forumpost/api/AllCommentPaggingEndUser?id=" + id + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);
        let dataCmt = dataComment.data;
        let contentComt = ``;
        $.each(dataCmt, function (index, element) {
            let imgCmt = element.forumPostCmtPhoto ? element.forumPostCmtPhoto : "/images/default/authdefaultimage.png";
            contentComt += `
            <div class="forum-post-top">

                <a class="author-avatar" href="#">
                    <img src="`+ (element.forumPostTypeId == systemConstant.notification_status_read ? imgCmt : '/images/default/authdefaultimage.png') + `" alt="author avatar">
                </a>
                <div class="forum-post-author">
                    <div class="head-post-top d-flex">
                        <a class="author-name" href="${(element.forumPostTypeId == systemConstant.notification_status_read ? `/thong-tin-ca-nhan/` + element.forumAccountUserName +`` : '') }">` + (element.forumPostTypeId == systemConstant.notification_status_read ? element.forumPostAccountName : 'Ẩn Danh') + `</a>
                        <span>`+ moment(element.createdTime).format("DD/MM/YYYY HH:mm") + `</span>
                    </div>
                    <div class="author_description">
                        <p>`+ element.text + `</p>
                    </div>        
                </div>
            </div>
        `;
        })
        $("#list_cmt_forum").html(contentComt);
    }
    catch (e) {

        $("#heade_cmt").text("Chưa có bình luận");
    }
    
}
async function loadCategorySideBar() {
    var result = await httpService.getAsync("forumcategory/api/typical-category");
    $("#typical-category").html('');
    if (result.status == "200") {
        var query = result.data
        query.forEach(function (item) {
            if (item.lastedPost == null) {
                $("#typical-category").append(`
                    <li class="d-flex cate_item">
                        <img src="`+ item.photo + `" class="photo_cte_sidebar" alt="category">
                        <a href="/danh-sach-thu-muc-dien-dan?id=`+ item.id + `">` + item.name + `</a> <span class="count">` + item.countPost + `</span>
                    </li>
                `
                )
            }
        })
    }
}
$(document).on("click", "#btn_cmt", async function () {

    if (editor.getData() != "" && editor.getData() != null) {

        Swal.fire({
            title: "Bình luận",
            text: "Bạn xác nhận bình luận?",
            icon: "question",
            showCancelButton: true,
            confirmButtonText: 'OK',
            cancelButtonText: 'Huỷ'

        }).then(async  (result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                let parentId = id;
                let forumPostType = $(".input_checkbox[name=hideaccount]:checked").val()
                var forumElement = await httpService.getAsync("forumPost/api/detail/" + id);

                var updatingObj = {
                    "parentId": id,
                    "active": 1,
                    "forumPostTypeId": forumPostType ? forumPostType : systemConstant.notification_status_read,
                    "forumPostStatusId": forumElement.data[0].forumPostStatusId,
                    "forumCategoryId": forumElement.data[0].forumCategoryId,
                    "forumPostAccountId": 0,
                    "photo": null,
                    "video": null,
                    "commentCount": null,
                    "likeCount": null,
                    "url": "",
                    "name": forumElement.data[0].name,
                    "description": null,
                    "downloadLink": null,
                    "text": editor.getData(),
                    "publishedTime": formatDatetimeUpdate(moment(new Date()).format("DD/MM/YYYY HH:mm:ss")),
                    "createdTime": formatDatetimeUpdate(moment(new Date()).format("DD/MM/YYYY HH:mm:ss"))
                }
                var result = await httpService.postAsync("forumPost/api/addcmt", updatingObj)
                if (result.status == '201' && result.message == 'CREATED') {
                    Swal.fire({
                        icon: "success",
                        title: "Bình luận thành công!",
                    });
                    let element = $(".pagination_default")
                    $("#heade_cmt").text("Tất cả bình luận");
                    LoadPagingPage("forumpost/api/countCmt?id=" + id, element, loadCmt, 6);
                }
                else if (result.status == '401') {
                    window.href('/dang-nhap')
                }
                editor.setData(""); 
            } else if (result.isDenied) {
                Swal.fire("Changes are not saved", "", "info");
            }
        });
        
    }
    else {
        Swal.fire({
            icon: "error",
            title: "Xin mời nhập thông tin bình luận!",
        });
    }

})
function formatDatetimeUpdate(dateStr) {
    //debugger;
    var [date, time] = dateStr.split(" ");
    var [day, month, year] = date.split("/");
    var localISOTime = year + "-" + month + "-" + day + "T" + time;
    return localISOTime;
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
                                    <a href="/thong-tin-ca-nhan/`+ item.username + `">` + item.authorName + `</a>
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