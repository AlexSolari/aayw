$(window).load(function myfunction() {
    window.AAYW = window.AAYW || {};

    AAYW.Admin = AAYW.Admin || {};

    AAYW.Admin.CustomPages = AAYW.Admin.CustomPages || (function (window) {
        return {
            Apply: function () {
                var showPagesPopup = function (data) {
                    var callback = function (data) {
                        function onsubmit() {
                            AAYW.UI.LoadingIndicator.Show();
                            $.post("[Route:CreatePage]", $(".popup-editor form").serialize(), function (result) {
                                AAYW.UI.LoadingIndicator.Close();
                                AAYW.UI.Popup.Close();

                                if (typeof result == "string") {
                                    AAYW.UI.Popup.Show(result);
                                    $('.popup-editor .submit').click(onsubmit);
                                }
                                else {
                                    AAYW.Engine.Links.RedirectTo("[Route:PageList]".replace("{page}", 0));
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
                        $.get("[Route:EditPage]".replace("{id}", data), callback);
                    }
                    else {
                        $.get("[Route:CreatePage]", callback);
                    }
                }

                $('.admin-pages .create').click(showPagesPopup);

                $('.admin-pages .edit-pages').click(function () {
                    showPagesPopup($(this).parents("tr").data("id"));
                    return false;
                });
            },
        };
    })(this);
});