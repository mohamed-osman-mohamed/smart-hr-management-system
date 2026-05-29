$(document).ready(function () {
    // إظهار وإخفاء كلمة المرور (Show/Hide Password)
    $('#togglePass').on('click', function () {
        const passField = $('#passInput');
        const icon = $(this).find('i');

        if (passField.attr('type') === 'password') {
            passField.attr('type', 'text');
            icon.removeClass('bi-eye').addClass('bi-eye-slash');
        } else {
            passField.attr('type', 'password');
            icon.removeClass('bi-eye-slash').addClass('bi-eye');
        }
    });
});