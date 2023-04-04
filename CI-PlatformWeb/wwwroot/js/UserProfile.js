$(function () {

    $('body').on('click', '.list-group .list-group-item', function () {
        $(this).toggleClass('active');
    });
    $('.list-arrows button').click(function () {
        var $button = $(this), actives = '';
        if ($button.hasClass('move-left')) {
            actives = $('.list-right ul li.active');
            actives.clone().appendTo('.list-left ul');
            actives.remove();
        } else if ($button.hasClass('move-right')) {
            actives = $('.list-left ul li.active');
            actives.clone().appendTo('.list-right ul');
            actives.remove();
        }
    });
    $('.dual-list .selector').click(function () {
        var $checkBox = $(this);
        if (!$checkBox.hasClass('selected')) {
            $checkBox.addClass('selected').closest('.well').find('ul li:not(.active)').addClass('active');
            $checkBox.children('i').removeClass('bi bi-check').addClass('bi bi-check-all');
            $checkBox.children('i').removeClass('bg-dark').addClass('bg-primary');
        } else {
            $checkBox.removeClass('selected').closest('.well').find('ul li.active').removeClass('active');
            $checkBox.children('i').removeClass('bi bi-check-all').addClass('bi bi-check');
            $checkBox.children('i').removeClass('bg-primary').addClass('bg-dark');
        }
    });
    $('[name="SearchDualList"]').keyup(function (e) {
        var code = e.keyCode || e.which;
        if (code == '9') return;
        if (code == '27') $(this).val(null);
        var $rows = $(this).closest('.dual-list').find('.list-group li');
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    });

});
function changepass() {
    var pass1 = document.getElementById('inputPassword1').value;
    var pass2 = document.getElementById('inputPassword2').value;
    var pass3 = document.getElementById('inputPassword3').value;
    $.ajax({
        url: '/Home/changepass',
        type: 'POST',
        data: { pass1: pass1, pass2: pass2, pass3: pass3 },

        success: function (response) {

            $('.goalsheetdiv').html($(response).find('.goalsheetdiv').html());


        },
        error: function () {
            alert("could not comment");
        }
    });
}

document.getElementById('imgDiv').addEventListener("click", e => {
    document.getElementById('imgInput').click();
});