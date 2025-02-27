'use strict';

async function loadData() {
    var result = await httpService.getAsync("healthFacility/api/Detail/" + id);
    if (result.data == null) {
        console.log('không có dữ liệu!');
    } else {
        localStorage.setItem("healthFacilityId", id);
        var content = '';
        var infoClinic = '';
        result.data.forEach(function (item) {
            var newContent = ''
            var schedule = item.openDate

            var splittedSchedule = schedule.split(',');
            splittedSchedule.forEach(function (itemOpenDate){
                newContent += `<p class="text-infor mb-0">${itemOpenDate}</p>`
            })
            content += `<div class="col-lg-6">
                <div class="d-flex flex-column w-100">
                    <ul class="nav small-image-list d-flex flex-row gap-16px wow mode-register-gallery" data-wow-duration="1.5s" data-wow-delay=".4s" id="photos-gallery">
                        <li class="nav-item">
                            <div id="details-img1" class="gallery" data-bs-toggle="pill" data-bs-target="#img-first" aria-controls="gallery-img1">
                                <a href="${item.photo}" class="fancybox" data-fancybox-group="gallery1" title="Thực phẩm chức năng Zero Acnes" alt="Thực phẩm chức năng Zero Acnes"><img alt="image" src="${item.photo}" class="img-fluid img-thumb img-header-clinic lazy lz-entered lz-loaded" data-src="${item.photo}" data-ll-status="loaded"></a>
                            </div>
                        </li>
                        <li class="nav-item">
                            <div id="details-img1" class="gallery" data-bs-toggle="pill" data-bs-target="#img-first" aria-controls="gallery-img1">
                                <a href="/HomeDoctor/img/list-clinic/anh-phong-kham-apage.png" class="fancybox" data-fancybox-group="gallery1" title="Thực phẩm chức năng Zero Acnes" alt="Thực phẩm chức năng Zero Acnes"><img alt="image" src="/HomeDoctor/img/list-clinic/anh-phong-kham-apage.png" class="img-fluid img-thumb lazy lz-entered lz-loaded" data-src="/HomeDoctor/img/list-clinic/anh-phong-kham-apage.png" data-ll-status="loaded"></a>
                            </div>
                        </li>
                        <li class="nav-item">
                            <div id="details-img1" class="gallery" data-bs-toggle="pill" data-bs-target="#img-first" aria-controls="gallery-img1">
                                <a href="/HomeDoctor/img/list-clinic/RHM AGAPE 2_1.jpg" class="fancybox" data-fancybox-group="gallery1" title="Thực phẩm chức năng Zero Acnes" alt="Thực phẩm chức năng Zero Acnes"><img alt="image" src="/HomeDoctor/img/list-clinic/RHM AGAPE 2_1.jpg" class="img-fluid img-thumb lazy lz-entered lz-loaded" data-src="/HomeDoctor/img/list-clinic/RHM AGAPE 2_1.jpg" data-ll-status="loaded"></a>
                            </div>
                        </li>
                        <li class="nav-item">
                            <div id="details-img1" class="gallery" data-bs-toggle="pill" data-bs-target="#img-first" aria-controls="gallery-img1">
                                <a href="/HomeDoctor/img/list-clinic/8a9d508d68489d16c459.jpg" class="fancybox"  data-fancybox-group="gallery1" title="Thực phẩm chức năng Zero Acnes" alt="Thực phẩm chức năng Zero Acnes"><img alt="image" src="/HomeDoctor/img/list-clinic/8a9d508d68489d16c459.jpg" class="img-fluid img-thumb lazy lz-entered lz-loaded" data-src="/HomeDoctor/img/list-clinic/8a9d508d68489d16c459.jpg" data-ll-status="loaded"></a>
                            </div>
                        </li>
                    </ul>

                </div>
            </div>
            <div class="col-lg-6 mt-xs-16 mt-sm-32">
                <div class="clinic-info-container d-flex flex-column gap-16 gap-xs-10">
                    <div class="product-info-header d-flex flex-column gap-16 gap-xs-10">
                        <h3 class="m-0">${item.name}</h3>
                        <div class="line-mini"></div>
                        <h5 class="m-0">Giới thiệu</h3>
                        <p class="mb-0">${item.description} <a href="#info-clinic" class="viewmore-btn">Xem thêm</a></p>
                        <h5 class="m-0">Thông tin chi tiết</h3>
                       <div class="address_clinic">
                            <div class="title_address">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16" fill="none">
                                    <g clip-path="url(#clip0_762_4724)">
                                        <path d="M8 0C6.40935 0.00211004 4.88445 0.634929 3.75969 1.75969C2.63493 2.88445 2.00211 4.40935 2 6C2 10.3075 7.59 15.7025 7.8275 15.93C7.8737 15.9749 7.93558 16 8 16C8.06442 16 8.1263 15.9749 8.1725 15.93C8.41 15.7025 14 10.3075 14 6C13.9979 4.40935 13.3651 2.88445 12.2403 1.75969C11.1155 0.634929 9.59065 0.00211004 8 0ZM8 8.75C7.4561 8.75 6.92442 8.58871 6.47218 8.28654C6.01995 7.98437 5.66747 7.55488 5.45933 7.05238C5.25119 6.54988 5.19673 5.99695 5.30284 5.4635C5.40895 4.93005 5.67086 4.44005 6.05546 4.05546C6.44005 3.67086 6.93005 3.40895 7.4635 3.30284C7.99695 3.19673 8.54988 3.25119 9.05238 3.45933C9.55488 3.66747 9.98437 4.01995 10.2865 4.47218C10.5887 4.92442 10.75 5.4561 10.75 6C10.7496 6.72921 10.4597 7.42843 9.94406 7.94406C9.42843 8.45969 8.72921 8.74956 8 8.75Z" fill="url(#paint0_linear_762_4724)" fill-opacity="0.97" />
                                    </g>
                                    <defs>
                                        <linearGradient id="paint0_linear_762_4724" x1="8" y1="0" x2="8" y2="16" gradientUnits="userSpaceOnUse">
                                            <stop stop-color="#FF0019" />
                                            <stop offset="1" stop-color="#F57314" />
                                        </linearGradient>
                                        <clipPath id="clip0_762_4724">
                                            <rect width="16" height="16" fill="white" />
                                        </clipPath>
                                    </defs>
                                </svg>
                                <p class="mb-0">Địa chỉ:</p>
                            </div>
                            <p class="text-infor mb-0">${item.addressDetail}</p>
                        </div>
                        <div class="hotline_clinic">
                            <div class="title_address">
                                <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" viewBox="0 0 14 14" fill="none">
                                    <g clip-path="url(#clip0_762_4758)">
                                        <path d="M13.6189 10.2746L11.6652 8.32085C10.9674 7.62308 9.7812 7.90222 9.50209 8.80928C9.29276 9.4373 8.595 9.78618 7.96701 9.6466C6.57148 9.29772 4.68752 7.48353 4.33863 6.01822C4.1293 5.39021 4.54796 4.69244 5.17595 4.48314C6.08305 4.20403 6.36215 3.01783 5.66439 2.32007L3.71064 0.366327C3.15243 -0.122109 2.31511 -0.122109 1.82668 0.366327L0.500926 1.69208C-0.824828 3.08761 0.640479 6.78576 3.91997 10.0653C7.19947 13.3448 10.8976 14.8799 12.2932 13.4843L13.6189 12.1586C14.1074 11.6003 14.1074 10.763 13.6189 10.2746Z" fill="url(#paint0_linear_762_4758)" />
                                    </g>
                                    <defs>
                                        <linearGradient id="paint0_linear_762_4758" x1="6.99995" y1="0" x2="6.99995" y2="14" gradientUnits="userSpaceOnUse">
                                            <stop stop-color="#FFC800" />
                                            <stop offset="1" stop-color="#D700B5" />
                                        </linearGradient>
                                        <clipPath id="clip0_762_4758">
                                            <rect width="14" height="14" fill="white" />
                                        </clipPath>
                                    </defs>
                                </svg>
                                <p class="mb-0">Hotline:</p>
                            </div>
                            <p class="text-infor mb-0">${item.phone}</p>
                        </div>
                        <div class="hotline_clinic">
                            <div class="title_address">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16" fill="none">
                                    <g clip-path="url(#clip0_762_5832)">
                                        <path d="M7.99984 0.666626C6.54944 0.666626 5.13162 1.09672 3.92566 1.90252C2.7197 2.70831 1.77977 3.85362 1.22472 5.19361C0.669681 6.53361 0.524457 8.00809 0.807415 9.43062C1.09037 10.8531 1.78881 12.1598 2.81439 13.1854C3.83998 14.211 5.14665 14.9094 6.56918 15.1924C7.99171 15.4753 9.4662 15.3301 10.8062 14.7751C12.1462 14.22 13.2915 13.2801 14.0973 12.0741C14.9031 10.8682 15.3332 9.45036 15.3332 7.99996C15.3309 6.05574 14.5575 4.19181 13.1828 2.81704C11.808 1.44227 9.94406 0.66892 7.99984 0.666626ZM10.4712 10.4713C10.3462 10.5963 10.1766 10.6665 9.99984 10.6665C9.82306 10.6665 9.65353 10.5963 9.52851 10.4713L7.52851 8.47129C7.40347 8.3463 7.33321 8.17676 7.33317 7.99996V3.99996C7.33317 3.82315 7.40341 3.65358 7.52844 3.52855C7.65346 3.40353 7.82303 3.33329 7.99984 3.33329C8.17665 3.33329 8.34622 3.40353 8.47124 3.52855C8.59627 3.65358 8.66651 3.82315 8.66651 3.99996V7.72396L10.4712 9.52863C10.5962 9.65364 10.6664 9.82318 10.6664 9.99996C10.6664 10.1767 10.5962 10.3463 10.4712 10.4713Z" fill="url(#paint0_linear_762_5832)" />
                                    </g>
                                    <defs>
                                        <linearGradient id="paint0_linear_762_5832" x1="-1.49016" y1="15.4673" x2="13.7878" y2="3.44596" gradientUnits="userSpaceOnUse">
                                            <stop stop-color="#1BA0DB" />
                                            <stop offset="1" stop-color="#57BF99" />
                                        </linearGradient>
                                        <clipPath id="clip0_762_5832">
                                            <rect width="16" height="16" fill="white" />
                                        </clipPath>
                                    </defs>
                                </svg>
                                <p class="mb-0">Giờ mở cửa:</p>
                            </div>
                            `+ newContent +`
                        </div>
                        <div class="product-info-button">
                            <a class="btn-chat" href="tel:`+ item.phone.replaceAll(".", "") +`">Gọi ngay</a>
                            <a class="btn-booking" data-toggle="modal" data-target="#product">Đặt lịch khám</a>
                        </div>
                    </div>
                </div>
            </div>`

            infoClinic = `<div class="product-info-body d-flex flex-column gap-32">
                        <div class="product-info-description m-0">
                        </div>
                        <div class="product-info-header d-flex flex-column gap-16 gap-xs-10">
                            <h4 class="m-0">Giới thiệu phòng khám</h4>
                            <div class="line-mini"></div>
                        </div>
                         <div class="clinic-description">
                            ${item.info}
                        </div>
                    </div>`
        });
        $('.clinic-info-container').html(content);
        $('.info-clinic').html(infoClinic)

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
var loadDataServices = async function () {
    var result = await httpService.getAsync("services/api/list_top_4?id="+id);
    if (result.data == null) {
        console.log('không có dữ liệu!');
    } else {
        var newRow = '';
        
       
        result.data.forEach(function (item) {
            var formattedPrice = item.price.toLocaleString('vi-VN', {
                style: 'currency',
                currency: 'VND',
                minimumFractionDigits: 0,
                maximumFractionDigits: 0
            });
            // Thay thế ký tự tiền tệ từ "₫" thành "đ"
            formattedPrice = formattedPrice.replace('₫', 'đ');
           
             newRow += `<a class="product-item first border border-white" href="/chi-tiet-dich-vu/${item.id}">
                        <div class="product-content">
                            <span class="title-product">
                                ${item.name}
                            </span>
                            <span class="product-text">
                                Giá chỉ từ
                            </span>
                            <span class="product-price">
                                ${formattedPrice}
                            </span>
                        </div>
                        <img src="${item.photo}" />
                    </a>`
        })
        $('.doc_community_info').html(newRow)
    }
}
var loadDataServicesModal = async function () {
    var result = await httpService.getAsync("services/api/ListPagingClinic?pageIndex=1&pageSize=100&clinicId=" + id);
    if (result.data == null) {
        console.log('không có dữ liệu!');
    } else {
        var newRow = '';
        var colorList = ["linear-gradient(266deg, #D0F4FD 0%, #E0FBF9 100%);", "linear-gradient(266deg, #F8DEFF 0%, #FBEBFF 100%);"];

        result.data.forEach(function (item,index) {
            var formattedPrice = item.price.toLocaleString('vi-VN', {
                style: 'currency',
                currency: 'VND',
                minimumFractionDigits: 0,
                maximumFractionDigits: 0
            });
            // Thay thế ký tự tiền tệ từ "₫" thành "đ"
            formattedPrice = formattedPrice.replace('₫', 'đ');
            var colorIndex = index % colorList.length;
            var assignedColor = colorList[colorIndex];
            newRow += `<a class="product-item first border border-white" href="/chi-tiet-dich-vu/${item.id}" style='background:${assignedColor} width:100%; height:135px'>
                        <div class="product-content">
                            <span class="title-product">
                                ${item.name}
                            </span>
                            <span class="product-text">
                                Giá chỉ từ
                            </span>
                            <span class="product-price">
                                ${formattedPrice}
                            </span>
                        </div>
                        <img src="${item.photo}" style='max-height:102px'/>
                    </a>`
        })
        $('.service-list').html(newRow)
    }
}
function redirect() {
    window.location.href = "/danh-sach-dich-vu?clinicId=" + id;
}
$(document).ready(async function () {
    $.when(loadData()).done(function () {
        openPhotoSwipe();
    });
    await loadDataServices();
    await loadDataServicesModal();
    
    //$("#hide-less-description").hide();
    //$("#load-more-description").click(function () {
    //    $(".clinic-description").css('height', '100%');     
    //    $("#load-more-description").hide();
    //    $("#hide-less-description").show();
    //});
    //$("#hide-less-description").click(function () {
    //    $(".clinic-description").css('height', '140px');
    //    $("#load-more-description").show();
    //    $("#hide-less-description").hide();
    //});
})

