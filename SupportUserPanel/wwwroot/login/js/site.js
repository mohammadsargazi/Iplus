
var validate = true;
function validateError() {
    validate = true;
    $('input').each(function () {
        if ($.inArray(@Convert.ToDecimal(RolesEnum.Admin), roleIds) !== -1) {
        if ($(this).val() == "") {
            var id = $(this).attr('id');
            var txt = $('label[for="' + id + '"]').text();
            if (txt != "") {
                txt = txt + " را وارد نمایید."
                $.growl.warning({ message: txt, title: "کاربر گرامی" });
                validate = false;
            }
        }
    }
    if ($.inArray(@Convert.ToDecimal(RolesEnum.Admin), roleIds) === -1 && !$(this).closest('div').hasClass('show-to-admin')) {
        if ($(this).val() == "") {
            var id = $(this).attr('id');
            var txt = $('label[for="' + id + '"]').text();
            if (txt != "") {
                txt = txt + " را وارد نمایید."
                $.growl.warning({ message: txt, title: "کاربر گرامی" });
                validate = false;
            }
        }
    }


});
$('select').each(function () {
    if ($.inArray(@Convert.ToDecimal(RolesEnum.Admin), roleIds) !== -1) {
    if ($(this).val() == "" || $(this).val() == 0) {
        var id = $(this).attr('id');
        var txt = $('label[for="' + id + '"]').text();
        if (txt != "") {
            txt = txt + " را وارد نمایید."
            $.growl.warning({ message: txt, title: "کاربر گرامی" });
            validate = false;
        }
    }
}
if ($.inArray(@Convert.ToDecimal(RolesEnum.Admin), roleIds) === -1 && !$(this).closest('div').hasClass('show-to-admin')) {
    if ($(this).val() == "" || $(this).val() == 0) {
        var id = $(this).attr('id');
        var txt = $('label[for="' + id + '"]').text();
        if (txt != "") {
            txt = txt + " را وارد نمایید."
            $.growl.warning({ message: txt, title: "کاربر گرامی" });
            validate = false;
        }
    }
}
        })
    }
