var host = window.location.host;

// 判断耳号是否存在
$(document).on("blur", "input.js-DisplayEarNum", function () {
    var displayEarNumInput = $(this);
    var displayEarNum = displayEarNumInput.val();
    if (typeof (displayEarNum) != 'undefined' && displayEarNum) {
        $.ajax({
            type: "post",
            url: "http://" + host + "/Cow/CheckDisplayEarNum",
            data: { displayEarNum: displayEarNum },
            dataType: "json",
            cache: false,
            success: function (response) {
                if (typeof (response.result) != 'undefined' && response.result == false) {
                    alert("耳号不存在，请重新输入！");
                    displayEarNumInput.focus();
                }
            }
        });
    }
});