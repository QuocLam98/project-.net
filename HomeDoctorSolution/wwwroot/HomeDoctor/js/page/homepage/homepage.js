async function loadProduct() {
    var service = await httpService.getAsync("services/api/list-four-service");
    $(".doc_community_info").html('');
    service.data.forEach(function (item) {
        $(".doc_community_info").append(`<a href="/chi-tiet-dich-vu/` + item.id + `" class='product-item first border border-white'>
            <div class="product-content">
                <span class="title-product" id="name-service">
                    `+ item.name +`
                </span>
                <span class="product-text">
                    Giá chỉ từ
                </span>
                <span class="product-price column-price">
                    `+ item.price +`
                </span>
            </div>
            <img class="icon-service" src='${item.photo}'></>
        </a>`)
    });
}

async function loadDoctor() {
    var doctor = await httpService.getAsync("doctors/api/list-three-doctor");
    $(".psychologist-body").html('');
    doctor.data.forEach(function (item) {
        $(".psychologist-body").append(`<a href="/chi-tiet-bac-si/` + item.id + `-` + `bac-si-` + remove_accents(item.name) +`" class="psychologist-detail shadow-sm wow fadeInUp" style="visibility: visible; animation-name: fadeInUp;">
            <div>
                <img src="`+ item.image +`" />
            </div>
            <div class="psychologist-detail-content">

                <div class="psychologist">
                    <div class="psychologist-name">
                        `+ item.name +`
                    </div>
                </div>
                <label class="psychologist-hospital psychologist-width">
                    `+ item.specialist +`
                </label>
                <div class="psychologist-overview">
                    `+ item.info +`
                </div>
            </div>
        </a>`)
    });
}

async function loadPost() {
    var post = await httpService.getAsync("post/api/list-four-post");
    console.log(post.data)
    $(".featured-news-detail").html("");
    post.data.forEach(function (item) {
        var getTime = moment(item.createdTime).format('DD/MM/YYYY');
        $(".featured-news-detail").append(`<a href="/chi-tiet-tin-tuc/`+ item.id +`" class="featured-news-first shadow wow fadeInUp" style="visibility: visible; animation-name: fadeInUp;">
            <div class="photo-post">
                <img src="`+ item.photo +`" />
            </div>
            <div class="featured-news-body">
                <div class="post_name" href="/#">
                    `+ item.name +`
                </div>
                <div class="post-overview">
                   `+ item.description +`
                </div>
                <div class="media post_author">
                    <div class="post-publish-time">`+ getTime +`</div>
                </div>
            </div>

        </a>`)
    })
}
function remove_accents(strAccents) {
    var strAccents = strAccents.split('');
    var strAccentsOut = new Array();
    var strAccentsLen = strAccents.length;
    var accents = "ÀÁÂÃÄÅàáâãäåạầÒÓÔÕÕÖØòóôõöợøÈÉÊËèéêëễÇçðÐÌÍÎÏìíîïÙÚÛÜùúûüưÑñŠšŸÿýŽž";
    var accentsOut = "AAAAAAaaaaaaaaOOOOOOOoooooo0EEEEeeeeeCcdDIIIIiiiiUUUUuuuuuNnSsYyyZz";
    for (var y = 0; y < strAccentsLen; y++) {
        if (accents.indexOf(strAccents[y]) != -1) {
            strAccentsOut[y] = accentsOut.substr(accents.indexOf(strAccents[y]), 1);
        } else
            strAccentsOut[y] = strAccents[y];
    }
    strAccentsOut = strAccentsOut.join('').trim().replaceAll(" ", "-").toLowerCase();

    return strAccentsOut;
}
$(document).ready(async function () {
    loadProduct();
    setTimeout(function () {
        formatNumber();
    },1000)
    loadDoctor();
    loadPost();
    
});
function formatNumber() {
    //debugger;
    $('.column-price , .column-openingPrice , .column-value , .column-amount, .column-available, .column-balance1, .price-format').each(function (event) {
        // format number
        $(this).text(function (index, value) {
            return value
                .replace(/\D/g, '')
                .replace(/\B(?=(\d{3})+(?!\d))/g, '.');
        });
    });

    $('.input-price,.input-depositPrice,.input-openingPrice,.input-buyPrice,.input-stepPrice , .input-value, .input-balance, .input-available, .input-auctionPropertyRegisterFee, .input-registerFee').each(function (event) {
        $(this).attr('type', 'text');

        // format number
        $(this).val(function (index, value) {
            return value
                .replace(/\D/g, '')
                .replace(/\B(?=(\d{3})+(?!\d))/g, '.');
        });
    });

    $('.column-createdTime , .column-publishedTime , .column-openTime , .column-closedTime').each(function (event) {
        //debugger;
        var currentValue = $(this).text();
        if (!currentValue.includes("T")) return;
        var date = new Date(currentValue);
        var newValue = date.toLocaleDateString() + " " + date.toLocaleTimeString();
        //$(this).text(newValue);
        $(this).text(formatDisplayTimeFromDBTime(currentValue));
    });

    // $('.column-createdTime').each(function (event) {
    //    //debugger;
    //    var currentValue = $(this).text();
    //    $(this).text(datetimeFormatVietNam(currentValue));
    //});


    $('.input-createdTime , .input-publishedTime').each(function (event) {
        //debugger;
        var inputObj = $(this).find("input");
        var currentValue = inputObj.val();
        if (!currentValue.includes("T")) return;
        var date = new Date(currentValue);
        var newValue = date.toLocaleDateString() + " " + date.toLocaleTimeString();
        inputObj.val(newValue);
    });

}