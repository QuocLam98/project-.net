﻿"use strict";

var loadDataCommentForumPost = async function LoadForumPost() {
    var search = $("#searchCommentForumPost").val();
    var result = await httpService.getAsync("forumpost/api/commentForumPostByAccountIdCustomer?id=" + id +"&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&search=" + search + "");
    var html = '';
    $("#commentForumPost").html('');
    if (result.status == 200 && result.data.length == 0) {
        var statusAccept = `<i class="icon_check"></i> ` + 0 + ` bài đã duyệt`;
        var statusAwait = `<i class="icon_clock"></i> ` + 0 + ` bài chờ duyệt`;
        var statusCancel = `<i class="icon_error-circle_alt"></i>` + 0 + ` bài bị từ chối`;
        $("#comment-forum-post-status-accept").html(statusAccept);
        $("#comment-forum-post-status-await").html(statusAwait);
        $("#comment-forum-post-status-cancel").html(statusCancel);
        $(".pagination_default").hide();
        html = `<li>
                    <h3>Không có dữ liệu</3>
                </li>`;
        $("#commentForumPost").html(html);
    }
    if (result.status == 200 && result.data.length != 0) {
        var statusAccept = `<i class="icon_check"></i> ` + result.data[0].forumPostStatusCountAccept + ` bài đã duyệt`;
        var statusAwait = `<i class="icon_clock"></i> ` + result.data[0].forumPostStatusCountAwait + ` bài chờ duyệt`;
        var statusCancel = `<i class="icon_error-circle_alt"></i>` + result.data[0].forumPostStatusCountCancel + ` bài bị từ chối`;
        $("#comment-forum-post-status-accept").html(statusAccept);
        $("#comment-forum-post-status-await").html(statusAwait);
        $("#comment-forum-post-status-cancel").html(statusCancel);
        result.data.forEach(function (item) {
            let commentCount = item.commentCount == null ? 0 : item.commentCount
            let likeCount = item.likeCount == null ? 0 : item.likeCount
            let imageForumPost = item.photo == null ? "/images/default/NoImage.png" : systemURL + item.photo;
            html += `<li>
                    <div class="media">
                        <div class="d-flex">
                            <img class="rounded-circle" src="`+ imageForumPost + `" alt="" style="width:68px; height:68px;">
                        </div>
                        <div class="media-body">
                            <div class="t_title">
                                <a href="dien-dan/chi-tiet-bai-viet/`+ item.id +`"
                                    target="_blank">
                                    <h4>`+ item.name + `</h4>
                                </a>
                            </div>
                            <a href="`+ systemURL +`forum-topic" target="_blank">
                                <h6>
                                    <i class="icon_document"></i> `+ item.forumCategoryName + `
                                </h6>
                            </a>
                            
                            <div class="t_title">
                                <i class="icon_calendar"></i>
                                <h6 style="padding-left: 3px; padding-right: 20px;">
                                    Ngày đăng
                                </h6>`+ moment(item.publishedTime).format("YYYY-MM-DD HH:mm:ss") + ` 
                            </div>
                        </div>
                        <div class="media-right">
                            <a class="count " href="#">
                                <i class="icon_chat"></i>`+ commentCount + `
                            </a>
                            <a class="count rate" href="#">
                                <i class="icon_like"></i>`+ likeCount + `
                            </a>
                        </div>
                    </div>
                </li>`;
        });
        $("#commentForumPost").html(html);
    }
}

$(document).ready(async function () {
    await loadDataCommentForumPost.call();
    
});
$("#btnSearchCommentForumPost").click(function () {
    loadDataCommentForumPost.call();
});

$('#searchCommentForumPost').on('keyup', function (e) {
    if (e.key === 'Enter' || e.keyCode === 13) {
        // Thực hiện tìm kiếm khi phím Enter được nhấn
        loadDataCommentForumPost.call();
    }
});