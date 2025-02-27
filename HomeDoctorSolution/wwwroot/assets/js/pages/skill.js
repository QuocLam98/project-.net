﻿/**
 * Author: Nam Anh
 * CreatedTime: 21/08/2023
 */
$(document).ready(function () {
    loadData();
    $("#btnAddNew").on("click", function () {
        editItem(0);
    })

    $("#tableData").on("click", ".btn-admin-edit", function () {
        var id = parseInt($(this).attr("data-idItem"));
        editItem(id);
    })

    $("#tableData").on("click", ".btn-admin-delete", function () {
        var id = parseInt($(this).attr("data-idItem"));
        deleteItem(id);
    })

    $("#submitButton").on("click", function () {
        validateSkill();
    })

    $("#form-submit-skill-level").on("submit", function (e) {
        e.preventDefault();
        validateSkill();
    })

    $("#table_search_all").on('keyup', function (e) {
        if (e.keyCode == 13) {
            table.search($(this).val()).draw();
        }
    });
    $("#btnTableSearch").click(function () {
        tableSearch();
    });

    $("#tableData thead:nth-child(2)").find("input").keypress(function (e) {
        let key = e.which;
        if (key == 13) {
            $("#btnTableSearch").click();
        }
    })

    $("#btnTableResetSearch").click(function () {
        $("#fillter_startDate_value").val("");
        $("#fillter_endDate_value").val("");
        $("#searchSkillName").val("");
        $("#searchSkillDescription").val("");
        //linked1.clear();
        //linked2.clear();
        tableSearch();
    });
})

async function editItem(id) {
    updatingId = id;
    autosize.destroy($('#skillDescription'));
    autosize($('#skillDescription'));
    $("#loading").addClass("show");

    if (id > 0) {
        updatingObj = getItemById(updatingId);

        if (updatingObj != null && updatingObj != undefined) {
            //console.log(updatingObj);
            $("#skillName").val(updatingObj.name);
            $("#skillDescription").text(updatingObj.description);
            $("#createdTime").val(moment(updatingObj.createTime).format("DD/MM/YYYY HH:mm:ss"));
        }
        else {
            swal.fire(
                'Loại kỹ năng',
                'Không thể cập nhật kỹ năng, hãy kiểm tra lại thông tin',
                'Error'
            );
        }
    } else {
        $("#skillName").val("").trigger("change");
        $("#skillDescription").val("").trigger("change");
        $("#createdTime").val(moment().format("DD/MM/YYYY HH:mm:ss"));
    }

    $("#skillModalTitle").text(id > 0 ? "Cập nhật kỹ năng" : "Thêm mới kỹ năng");
    $("#loading").removeClass("show");
    $("#skillModal").modal("show");
}

function getItemById(id) {
    const result = dataSource.find(item => parseInt(item.id) === parseInt(id));
    return result;
}

function loadData() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/skill/list",
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            dataSource = response.resources;
            loadTable();
            if (tableUpdating === 0) {
                initTable();
            }
        },
        error: function (e) {
            initTable();
        }
    });
}

function initTable() {
    table = $('#tableData').DataTable({
        language: systemConfig.languageDataTable,
        searching: {
            regex: true
        },
        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: [0, -1], orderable: false },
            { targets: "no-search", searchable: false },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }

                    return data;
                }
            },
            { targets: "date-type", type: "date-eu" },

        ],

        'order': [
            [3, 'desc']
        ],

        aLengthMenu: [
            [10, 25, 50, 100],
            [10, 25, 50, 100]
        ],
        drawCallback: function () {
            $('#tableData tfoot').html("");
            $("#tableData thead:nth-child(1) tr").clone(true).appendTo("#tableData tfoot");
            $("#rowSearch").removeClass("d-none");
        }
    });

    table.on('order.dt search.dt', function () {
        table.column(0, {
            search: 'applied',
            order: 'applied'
        }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

function loadTable() {
    var index = 0;
    $("#tableData tbody").html("");
    dataSource.forEach(function (item, index) {
        var rowContent = "<tr>";
        rowContent += "<td style='text-align: center;'>" + (index + 1) + "</td>";
        showItem.forEach(function (key) {
            if (item[key]) {
                if (key == "createdTime") {
                    rowContent += "<td class=' row-" + item.id + "-column column-" + key + "' property='" + key + "'>" + moment(item[key], "YYYY-MM-DD hh:mm:ss").format("DD/MM/YYYY hh:mm:ss") + "</td>"
                }
                else {
                    rowContent += "<td class='row" + item.id + "-column column-" + key + "' property='" + key + "'>" + item[key] + "</td>";
                }
            }
            else {
                rowContent += "<td class='row" + item.id + "-column column-" + key + "' property='" + key + "'>" + "</td>";
            }
        })
        rowContent += "<td class='row" + item.id + "-column column-' property=''>"
            + "<div class='d-flex justify-content-center'>";
        rowContent += "<button  type='button' class='btn btn-icon btn-admin-edit' data-idItem='" + item.id + "'><span class='svg-icon-primary svg-icon  svg-icon-1'> " + systemConfig.editIcon + " </span></button>"

        rowContent += "<button type='button' class='btn btn-icon btn-admin-delete' data-idItem='" + item.id + "' ><span class='svg-icon-danger svg-icon  svg-icon-1'>" + systemConfig.deleteIcon + "</span></button></div>";

        rowContent += "</div></td ></tr>";
        $(rowContent).appendTo($("#tableData tbody"));
    })
}

function deleteItem(id) {
    updatingObj = getItemById(id);
    swal.fire({
        title: 'Xóa kỹ năng',
        html: 'Bạn có chắc chắn muốn xóa kỹ năng <b>' + updatingObj.name + '</b>',
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Xóa'
    }).then((result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");
            $.ajax({
                url: systemConfig.defaultAPIURL + "api/skill/delete/" + id,
                type: "DELETE",
                success: function (response) {
                    $("#loading").removeClass("show");
                    if (response.isSucceeded) {
                        Swal.fire(
                            'Xóa kỹ năng',
                            'Kỹ năng <b>' + updatingObj.name + '</b> đã được xóa thành công.',
                            'success'
                        );
                        reGenTable();
                    } else {
                        Swal.fire(
                            'Xóa kỹ năng',
                            'Xóa kỹ năng không thành công, <br> vui lòng thử lại sau!',
                            'Error'
                        )
                    }
                },
                error: function (e) {
                    $("#loading").removeClass("show");
                    if (e.status === 401) {
                        Swal.fire(
                            'Kỹ năng',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'Error'
                        ).then(function () {
                            window.localtion.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Kỹ năng',
                            'Bạn không có quyền sử dụng tính năng này',
                            'Error'
                        );
                    }
                    else {
                        Swal.fire(
                            'Xóa kỹ năng',
                            'Xóa kỹ năng không thành công, <br> vui lòng thử lại sau!',
                            'Error'
                        );
                    }
                }
            })
        }
    })
}

function validateSkill() {
    var errorList = [];
    if ($("#skillName").val().length == 0) {
        errorList.push("Tên không được để trống.");
    } else if ($("#skillName").val().length > 255) {
        errorList.push("Tên không được dài quá 255 ký tự.");
    }
    if ($("#skillDescription").val().length > 500) {
        errorList.push("Mô tả không được dài quá 500 ký tự.");
    }

    if (errorList.length > 0) {
        var contentError = "<ul>";
        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        })
        contentError += "</ul>";
        var actionName = (updatingId > 0 ? "Cập nhật" : "Thêm mới");
        var swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName + "không thành công</p>";
        Swal.fire(
            'Loại kỹ năng' + swalSubTitle,
            contentError,
            'warning'
        );
    } else {
        submit();
    }
}

function submit() {
    var obj = {
        name: $("#skillName").val().trim(),
        description: $("#skillDescription").val()
    }

    if (updatingId > 0) {
        obj.id = updatingId;
    }
    var actionName = (updatingId > 0 ? "Cập nhật" : "Thêm mới");

    Swal.fire({
        title: actionName + "kỹ năng",
        html: "Bạn có chắc chắn muốn" + actionName.toLowerCase() + "kỹ năng <b>" + $("#skillName").val() + '</b>?',
        icon: 'info',
        showCancelButton: true,
        confirmButtonText: 'Lưu',
        cancelButtonText: 'Hủy'
    }).then((result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");

            //CALL AJAX TO CREATE
            if (updatingId == 0) {
                $.ajax({
                    type: 'POST',
                    url: systemConfig.defaultAPIURL + "api/skill/add",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    success: function (response) {
                        $("#loading").removeClass("show");
                        if (response.isSucceeded) {
                            Swal.fire(
                                'Thêm mới kỹ năng',
                                'Kỹ năng <b>' + $("#skillName").val() + '</b> đã được thêm mới thành công.',
                                'success'
                            ).then(function () {
                                $("#skillModal").modal("hide");
                                //window.location.reload();
                                reGenTable();
                            });
                        } else {
                            Swal.fire(
                                'Thêm mới kỹ năng',
                                'Thêm mới kỹ năng không thành công, <br> vui lòng thử lại sau!',
                                'Error'
                            )
                        }

                        if (response.status == 400) {
                            var swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName.toLowerCase() + "không thành công</p>";
                            if (response.errors != null) {
                                var contentError = "<ul>";
                                response.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start'>" + item + "</li>";
                                })
                                contentError += "</ul>";

                                Swal.fire(
                                    'Kỹ năng' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            } else {
                                var contentError = `<ul><li class='text-start'>` + response.message + `</li></ul>`;
                                Swal.fire(
                                    'Kỹ năng' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            }
                        }

                    },
                    error: function (e) {
                        $("#loading").removeClass("show");
                        if (e.status === 401) {
                            Swal.fire(
                                'Kỹ năng',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Kỹ năng',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                        else {
                            Swal.fire(
                                'Quản lý kỹ năng',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                    }
                });
            };

            //CALL AJAX TO UPDATE
            if (updatingId > 0) {
                $.ajax({
                    type: 'PUT',
                    url: systemConfig.defaultAPIURL + "api/skill/update",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    success: function (response) {
                        $("#loading").removeClass("show");

                        if (response.isSucceeded) {
                            Swal.fire(
                                'Cập nhật kỹ năng',
                                'Kỹ năng <b>' + $("#skillName").val() + '</b> đã được cập nhật thành công.',
                                'success'
                            ).then((result) => {
                                $("#skillModal").modal("hide");
                                reGenTable();
                            });
                        } else {
                            Swal.fire(
                                'Cập nhật kỹ năng',
                                'Cập nhật kỹ năng không thành công, <br> vui lòng thử lại sau!',
                                'Error'
                            )
                        }

                        if (response.status == 400) {
                            var swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName.toLowerCase() + " không thành công</p>";

                            if (response.errors != null) {
                                var contentError = "<ul>";
                                response.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start'>" + item + "</li>";
                                })
                                contentError += "</ul>";

                                Swal.fire(
                                    'Kỹ năng' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            } else {
                                var contentError = `<ul><li class='text-start'>` + response.message + `</li></ul>`;

                                Swal.fire(
                                    'Kỹ năng' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            }
                        }
                    },
                    error: function (e) {
                        $("#loading").removeClass("show");

                        if (e.status === 401) {
                            Swal.fire(
                                'Kỹ năng',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Kỹ năng',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            );
                        }
                    }
                });
            };
        }
    })
}

function tableSearch() {
    table.column(1).search($("#searchSkillName").val());
    table.column(2).search($("#searchSkillDescription").val());
    table.draw();
}

$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        var date = new Date(moment(data[3], "DD/MM/YYYY HH:mm:ss"));
        var startDate = $("#fillter_startDate_value").val();
        var endDate = $("#fillter_endDate_value").val();
        var min = startDate != "" ? new Date(moment(startDate, "DD/MM/YYYY ").format("YYYY-MM-DD 00:00:00")) : null;
        var max = endDate != "" ? new Date(moment(endDate, "DD/MM/YYYY").format("YYYY-MM-DD 23:59:59")) : null;
        if (
            (min === null && max === null) ||
            (min === null && date <= max) ||
            (min <= date && max === null) ||
            (min <= date && date <= max)
        ) {
            return true;
        }
        return false;
    }
);