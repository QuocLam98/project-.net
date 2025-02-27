$(document).ready(function () {
    loadDetailPost();
    //loadPostCategoryOnHomePage();
    //loadPostTagOnHomePage();
});


async function loadDetailPost() {
    try {
        //debugger;
        var result = await httpService.getAsync("post/api/DetailbyId/" + postId);
        if (result.status == 200) {
            var data = result.data[0];
            var currentValue = data.createdTime
            if (!currentValue.includes("T")) return
            var date = new Date(currentValue);
            var newValue = date.toLocaleDateString('vi-VN');

            var newRow = `

                <div class="title">
                    <p>
                        ${data.name}
                    </p>
                </div>
                <span class="date">${moment(data.publishedTime).format('DD/MM/YYYY')} </span>
                <div class="post-content">
                    <div class="content">
                        ${data.text}
                    </div>
                </div>
             `

            $(newRow).appendTo($("#body-post"));
        }
    } catch (e) {
        console.log(e.message);
    }
}