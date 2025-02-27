'use strict';
var loadData = async function () {
    var result = await httpService.getAsync("services/api/Detail/" + id);
    console.log(result)
    var dataSource = result.data[0];
    if (result.data == null) {
        console.log('không có dữ liệu!');
    } else {
        // Định dạng giá tiền
        var formattedPrice = dataSource.price.toLocaleString('vi-VN', {
            style: 'currency',
            currency: 'VND',
            minimumFractionDigits: 0,
            maximumFractionDigits: 0
        });
        $(".link-second").text(dataSource.name)
        // Thay thế ký tự tiền tệ từ "₫" thành "đ"
        formattedPrice = formattedPrice.replace('₫', 'đ');
        var healthFacilityId = localStorage.getItem("healthFacilityId");
        var newRow = `<div class="service-detail-header d-flex flex-column gap-16">

                    <div class="d-flex justify-content-between service-des-title">
                    <div>
                    <h2 class="m-0">${dataSource.name} </h2>
                                        <div class="line-mini"></div>
                                        </div>
                   
                     <div class="service-detail-price">

                        <span class="text-right service-des-money">Giá tiền: ${formattedPrice}</span> <!-- Sử dụng giá tiền đã được định dạng -->
                      <a class="btn btnHD type-warning w-343px" href="/danh-sach-bac-si?serviceId=${id}&healthFacilityId=${healthFacilityId}">Chọn dịch vụ</a>
                    </div>
                    </div>
                      
                    <div class="service-detail-description">
                        ${dataSource.description}
                    </div>
                   
                   
                </div>`

        $(newRow).appendTo($("#service-detail-top"));
    }
}

$(document).ready(async function () {
    await loadData();
});

