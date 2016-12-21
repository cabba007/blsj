
//$(document).ready(function () {
//    $.get("../DataServiceHandler.ashx?name=getpatname", function (data) { alert (data); });
//    });

$('#event_happen_time').removeAttr("data-val-date");
$('#admission_date').removeAttr("data-val-date");

$('#event_happen_time').datetimepicker({
    format: "YYYY-MM-DD HH:mm:ss",
    locale: "zh-cn"
});
$('#admission_date').datetimepicker({
    format: "YYYY-MM-DD",
    locale: "zh-cn"
});

var xx;
$("#patient_name").autocomplete({
    source: function (request, response) {
        $.getJSON("../DataServiceHandler.ashx", { name: "getpatname" }, function (data) {
            xx = data;
            response($.map(data, function (item) {
                            return {
                                label: item.BED_LABEL + '_' + item.NAME + '_' + item.INP_NO,
                                value: item.NAME
                            };
                        }));
                     });
                  },
    minLength: 0,
    select: function (event, ui) {
        $.each(xx, function (index,xxx) {
            if(xxx.INP_NO == ui.item.label.split("_")[2])
            {
                $("#bed_no").val(xxx.BED_LABEL);
                $("#inp_no").val(xxx.INP_NO);
                $("#patient_sex").val(xxx.SEX);
                $("#patient_age").val(xxx.AGE);
                $("#admission_date").val(xxx.ADMISSION_DATE);
                $("#dept_name").val(xxx.DEPT_NAME);
                $("#charge_type").val(xxx.CHARGE_TYPE);
                $("#diagnosis").val(xxx.DIAGNOSIS);
            }
        });
    }
});

$("#patient_name").attr("placeholder", "双击选取姓名")
                  .blur(function () { xx = null; })
                  .bind("dblclick", function () { $(this).autocomplete("search", ""); });

$("#patient_name").data("ui-autocomplete")._renderItem = function (ul, item) {
    return $("<li></li>")
        .data("item.autocomplete", item)
        .append(item.label.split("_")[0] + "床  " + item.label.split("_")[1])
        .appendTo(ul);
};

$("#event_type").autocomplete({
    source: ["运送途中病情变化", "误吸/窒息", "院内压疮", "坠床", "跌倒", "走失", "自杀", "猝死", "导管脱落/拔出", "咽入异物", "识别患者错误", "给药错误", "输血错误", "输液反应", "感染", "暴力行为", "针刺伤", "咬破体温计", "割伤", "烫伤", "烧伤", "火灾", "失窃", "蓄意破坏", "医疗材料故障", "仪器故障", "争吵/打架"],
    minLength: 0
}).bind("dblclick", function () {
    $(this).autocomplete("search", "");
});

$("#consciousness_status").autocomplete({
    source: ["神志清醒", "有定向力", "不安", "无定向力", "浅昏迷", "深昏迷"],
    minLength: 0
}).bind("dblclick", function () {
    $(this).autocomplete("search", "");
});

$("#event_location").autocomplete({
    source: ["病房", "治疗室", "换药室", "走廊", "卫生间", "病区外"],
    minLength: 0
}).bind("dblclick", function () {
    $(this).autocomplete("search", "");
});

$("#event_cause").autocomplete({
    source: ["年老体弱", "久病不愈", "病情恶化", "情绪不稳", "精神失常", "人为因素", "医疗材料故障", "仪器故障", "设备故障"],
    minLength: 0
}).bind("dblclick", function () {
    $(this).autocomplete("search", "");
});


