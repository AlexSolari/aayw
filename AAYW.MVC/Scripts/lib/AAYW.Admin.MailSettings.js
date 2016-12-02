$(window).load(function myfunction() {
    window.AAYW = window.AAYW || {};

    AAYW.Admin = AAYW.Admin || {};

    AAYW.Admin.MailSettings = AAYW.Admin.MailSettings || (function (window) {
        return {
            Apply: function () {
                $(".admin-mail-settings .submit").click(function () {
                    AAYW.UI.LoadingIndicator.Show();
                    $.post("[Route:MailSettings]", $(".admin-mail-settings form").serialize(), function () {
                        AAYW.UI.LoadingIndicator.Close();
                    })
                });
            },
        };
    })(this);
});