$(window).load(function myfunction() {
    window.AAYW = window.AAYW || {};

    AAYW.Admin = AAYW.Admin || {};

    AAYW.Admin.MailTemplates = AAYW.Admin.MailTemplates || (function (window) {
        return {
            Apply: function () {
                var showTemplatePopup = function (data) {
                    var callback = function (data) {
                        function onsubmit() {
                            AAYW.UI.LoadingIndicator.Show();
                            $.post("[Route:CreateMailTemplate]", $(".popup-editor form").serialize(), function (result) {
                                AAYW.UI.LoadingIndicator.Close();
                                AAYW.UI.Popup.Close();

                                if (!result) {
                                    AAYW.Engine.Links.RedirectTo("[Route:MailTemplates]".replace("{page}", 0));
                                }
                                else {
                                    AAYW.UI.Popup.Show(result);
                                    $('.popup-editor .submit').click(onsubmit);
                                }

                            })
                            return false;
                        }

                        AAYW.UI.LoadingIndicator.Close();
                        AAYW.UI.Popup.Show(data);
                        AAYW.UI.HtmlEditor.Bind();

                        $('.popup-editor .submit').click(onsubmit);
                    };

                    AAYW.UI.LoadingIndicator.Show();

                    if (typeof data == "string") {
                        $.get("[Route:EditMailTemplate]".replace("{id}", data), callback);
                    }
                    else {
                        $.get("[Route:CreateMailTemplate]", callback);
                    }
                }

                $('.admin-mail-template .create').click(showTemplatePopup);

                $('.admin-mail-template .edit-template').click(function () {
                    showTemplatePopup($(this).parents("tr").data("id"));
                    return false;
                });
            },
        };
    })(this);
});