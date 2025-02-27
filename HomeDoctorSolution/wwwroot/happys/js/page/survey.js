
var dataSource = [];
var idSurvey;
function formatDate(dateObject) {
    var d = new Date(dateObject);
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var date = day + "/" + month + "/" + year;

    return date;
};
async function showSurvey() {
    var result = await httpService.getAsync("survey/api/GetSurvey/" + systemConstant.survey_id);
    dataSource = result.data[0];
    idSurvey = dataSource.id;
    var detail_survey = $("#detail_survey");
    var newRow = `
        <div class="title_detail"><h3>Tham gia phiếu Trắc Nghiệm Sức Khỏe Tâm Thần</h3></div>
        <div class="img_detail"><img src="/happys/img/detail_survey/detail_survey.png"></div>
        <div class="name_detail"><h5>`+ dataSource.name +`</h5></div>
        <div class="question_detail"><h3>21 câu hỏi</h3></div>
        <div class="instruct">
            <p>Hãy đọc mỗi câu hỏi và chọn câu trả lời tương ứng với tình trạng mà bạn cảm thấy<span style="font-weight:700;"> trong một tuần qua</span>. Không có câu trả lời đúng hay sai. Và đừng dừng lại quá lâu ở bất kỳ câu nào. Hãy liên hệ với chúng tôi hoặc cán bộ tư vấn trên hệ thống nếu bạn cần hỗ trợ thực hiện khảo sát này.  
            </p>
            <p>Chúng ta cùng bắt đầu làm nhé!</p>
        </div>
        <div class="btn_detail">
            <button class="btn btnHS2" id="begin_survey">Bắt đầu</button>
        </div>
       `;
    detail_survey.append(newRow);
}


 $(document).ready(async function () {
    await showSurvey();
    $('#begin_survey').click(function () {
        // Chuyển hướng đến liên kết mong muốn
        window.location.href = `/danh-sach-cau-hoi/` + idSurvey;
    });
})