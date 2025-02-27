$(document).ready(function () {
    // Cấu hình Moment.js để sử dụng ngôn ngữ tiếng Việt
    moment.locale('vi');
    document.querySelectorAll(".datepicker").forEach(function (item) {
        new tempusDominus.TempusDominus(item, datePickerOption);
    })
    $("body").on('click', '[data-range-key="Custom Range"]', function (e) {
        $(".cancelBtn").html("Hủy");
        $(".applyBtn").html("Áp dụng");
    });
    // KTChartsWidget38.init(loadBooking);
    var loadChart = [{
        payLoad: loadBooking,
        target: 'kt_charts_widget_38_chart_1',
        option: {
            title: 'Biểu đồ thống kê về đặt lịch phỏng vấn',
            serieName: 'Tổng số lịch hẹn được đặt',
            format: "lần",
        }
    },
    {
        payLoad: loadPost,
        target: 'kt_charts_widget_38_chart_2',
        option: {
            title: 'Biểu đồ thống kê về đặt lịch phỏng vấn',
            serieName: 'Tổng số bài viết',
            format: "bài",
        }
    }];

    loadChart.forEach(function (item) {
        KTChartsWidget38.init(item.payLoad, item.target, item.option);
    });
});
"use strict";
// Class definition
var KTChartsWidget38 = function () {
    // Private methods
    var initChart = async function (loadData, selector, customOption) {
        // lấy chuỗi thời gian hiển thị trên biểu đồ
        var time = $("#timeFilter[data-target=" + selector + "] #timeToFilter").html();
        var datesArray = [];
        if (time.indexOf(" - ") !== -1) {
            var rangeTime = ($("#timeFilter[data-target=" + selector + "] #timeToFilter").html()).split(' - ');
            //console.log("rangeTime: " + rangeTime)

            var startDate = moment(rangeTime[0], 'DD/MM/YYYY');
            var endDate = moment(rangeTime[1], 'DD/MM/YYYY');

            var currentDate = startDate.clone();

            while (currentDate.isSameOrBefore(endDate)) {
                datesArray.push(currentDate.format('DD/MM/YYYY'));
                currentDate.add(1, 'days');
            }
            //console.log(datesArray);
        } else {
            var oneTime = moment(time, 'DD/MM/YYYY')
            datesArray.push(oneTime.format('DD/MM/YYYY'));
        }
        var valuesArray = [];
        await loadData.call().then(result => {
            if (result.length != 0) {//trường hợp có data
                datesArray.forEach(function (itemTime, index) {
                    var count = 0;
                    result.forEach(function (itemValue) {
                        if (moment(itemValue.createdTime).format("DD/MM/YYYY") == itemTime) {
                            count++;
                        }
                    });
                    valuesArray.push(count);
                })
            } else {
                datesArray.forEach(function () {
                    valuesArray.push(0);
                });
            }
        }).catch(error => {
            //debugger;
            datesArray = [];
            valuesArray = [];
        });
        var hideValuesArray = valuesArray.map((e, i) => e == 0 ? i : undefined).filter(x => x);
        if (hideValuesArray.length > 0) {
            datesArray = datesArray.filter((_, index) => hideValuesArray.indexOf(index) === -1);
            valuesArray = valuesArray.filter((_, index) => hideValuesArray.indexOf(index) === -1);
        }
        else {
            valuesArray = valuesArray.filter(x => x != 0);
        }
        var chart = {
            self: null,
            rendered: false
        };
        var element = document.getElementById(selector);

        if (!element) {
            return;
        }

        var height = parseInt(KTUtil.css(element, 'height'));
        var labelColor = KTUtil.getCssVariableValue('--bs-gray-900');
        var borderColor = KTUtil.getCssVariableValue('--bs-border-dashed-color');

        var options = {
            series: [{
                name: customOption.serieName,
                data: valuesArray
            }],
            chart: {
                fontFamily: 'inherit',
                type: 'bar',
                height: height,
                toolbar: {
                    show: false
                }
            },
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: ['30%'],
                    borderRadius: 5,
                    dataLabels: {
                        position: "top" // top, center, bottom
                    },
                    startingShape: 'flat'
                },
            },
            legend: {
                show: false
            },
            dataLabels: {
                enabled: true,
                offsetY: -28,
                style: {
                    fontSize: '13px',
                    colors: [labelColor]
                },
                formatter: function (val) {
                    return val;// + "H";
                }
            },
            stroke: {
                show: true,
                width: 2,
                colors: ['transparent']
            },
            xaxis: {
                categories: datesArray,
                axisBorder: {
                    show: false,
                },
                axisTicks: {
                    show: false
                },
                labels: {
                    style: {
                        colors: KTUtil.getCssVariableValue('--bs-gray-500'),
                        fontSize: '13px'
                    }
                },
                crosshairs: {
                    fill: {
                        gradient: {
                            opacityFrom: 0,
                            opacityTo: 0
                        }
                    }
                }
            },
            yaxis: {
                labels: {
                    style: {
                        colors: KTUtil.getCssVariableValue('--bs-gray-500'),
                        fontSize: '13px'
                    },
                    formatter: function (val) {
                        return val + " " + customOption.format;
                    }
                }
            },
            fill: {
                opacity: 1
            },
            states: {
                normal: {
                    filter: {
                        type: 'none',
                        value: 0
                    }
                },
                hover: {
                    filter: {
                        type: 'none',
                        value: 0
                    }
                },
                active: {
                    allowMultipleDataPointsSelection: false,
                    filter: {
                        type: 'none',
                        value: 0
                    }
                }
            },
            tooltip: {
                style: {
                    fontSize: '12px'
                },
                y: {
                    formatter: function (val) {
                        return + val + ' ' + customOption.format;
                    }
                }
            },
            noData: {
                text: 'Không có dữ liệu',
                align: 'center',
                verticalAlign: 'middle',
                offsetX: 0,
                offsetY: 0,
                style: {
                    color: KTUtil.getCssVariableValue('--bs-gray-500'),
                    fontSize: '14px',
                    fontFamily: undefined,
                }
            },
            colors: [KTUtil.getCssVariableValue('--bs-primary'), KTUtil.getCssVariableValue('--bs-primary-light')],
            grid: {
                borderColor: borderColor,
                strokeDashArray: 4,
                yaxis: {
                    lines: {
                        show: true
                    }
                }
            }
        };

        chart.self = new ApexCharts(element, options);

        // Set timeout to properly get the parent elements width
        // setTimeout(function () {
        //     chart.self.render();
        //     chart.rendered = true;
        // }, 200);
        chart.self.render();
        chart.rendered = true;
    }
    var createDateRangePickers = function (loadData, selector, customOption) {
        // Check if jQuery included
        if (typeof jQuery == 'undefined') {
            return;
        }

        // Check if daterangepicker included
        if (typeof $.fn.daterangepicker === 'undefined') {
            return;
        }
        var elements = [].slice.call(document.querySelectorAll('#timeFilter[data-target=' + selector + ']'));
        var start = moment();
        var end = moment();

        elements.map(function (element) {
            if (element.getAttribute("data-kt-initialized") === "1") {
                return;
            }

            var display = element.querySelector('div');
            var attrOpens = element.hasAttribute('data-kt-daterangepicker-opens') ? element.getAttribute('data-kt-daterangepicker-opens') : 'left';
            var range = element.getAttribute('data-kt-daterangepicker-range');

            var cb = function (start, end) {
                var current = moment();

                if (display) {
                    if (current.isSame(start, "day") && current.isSame(end, "day")) {
                        display.innerHTML = start.format('DD/MM/YYYY');
                    } else {
                        display.innerHTML = start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY');
                    }
                }


                // sau khi chọn thời gian filer thì xóa bảng cũ
                $("#" + selector).html("")
                // xóa xong gọi lại bảng mới
                initChart(loadData, selector, customOption);
            }

            if (range === "Hôm nay") {
                start = moment();
                end = moment();
            }

            $(element).daterangepicker({
                startDate: start,
                endDate: end,
                opens: attrOpens,
                ranges: {
                    'Hôm nay': [moment(), moment()],
                    'Hôm qua': [moment().subtract(1, 'day'), moment().subtract(1, 'day')],
                    '7 ngày gần đây': [moment().subtract(6, 'days'), moment()],
                    '30 ngày gần đây': [moment().subtract(29, 'days'), moment()],
                    'Tháng này': [moment().startOf('month'), moment().endOf('month')],
                    'Tháng trước': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);

            cb(start, end);

            element.setAttribute("data-kt-initialized", "1");
        });
    }
    // Public methods
    return {
        init: async function (loadData, selector, customOption) {
            await createDateRangePickers(loadData, selector, customOption);
            //// Update chart on theme mode change
            //KTThemeMode.on("kt.thememode.change", function () {
            //    if (chart.rendered) {
            //        chart.self.destroy();
            //    }
            //    await createDateRangePickers(loadData, selector, customOption);
            //});
        },
    }
}();
// Webpack support
if (typeof module !== 'undefined') {
    module.exports = KTChartsWidget38;
}

var loadBooking = async function loadDataBooking() {
    var timeToFilter = $("#timeFilter[data-target='kt_charts_widget_38_chart_1'] #timeToFilter").html().split('-');
    var startDate = timeToFilter[0];
    var endDate = timeToFilter[1];
    if (endDate == undefined) {
        endDate = startDate
    }
    //startDate = formatDatetime(startDate.trim());
    //endDate = formatDatetime(endDate.trim());
    var dataListBooking;
    await $.ajax({
        url: systemURL + 'booking/api/FilterReport?startDate=' + startDate + '&endDate=' + endDate,
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            }
        },
        success: function (responseData) {
            var data = responseData.data;
            dataListBooking = data;
            //console.log(dataListBooking);
            //KTChartsWidget38.init(dataListBooking);
        },
        error: function (e) {
            //console.log(e.message);
        }
    });
    return dataListBooking;
}
var loadPost = async function loadDataPost() {
    var timeToFilter = $("#timeFilter[data-target='kt_charts_widget_38_chart_2'] #timeToFilter").html().split('-');
    var startDate = timeToFilter[0];
    var endDate = timeToFilter[1];
    var dataListPost;
    if (endDate == undefined) {
        endDate = startDate
    }
    //startDate = formatDatetime(startDate.trim());
    //endDate = formatDatetime(endDate.trim());
    await $.ajax({
        url: systemURL + 'post/api/FilterReport?startDate=' + startDate + '&endDate=' + endDate,
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            }
        },
        success: function (responseData) {
            var data = responseData.data;
            dataListPost = data;
        },
        error: function (ex) {
            console.log(ex.message);
        }
    });
    return dataListPost;
}