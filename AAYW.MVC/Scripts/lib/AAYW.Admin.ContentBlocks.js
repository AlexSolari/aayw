$(window).load(function myfunction() {
    window.AAYW = window.AAYW || {};

    AAYW.Admin = AAYW.Admin || {};

    AAYW.Admin.ContentBlocks = AAYW.Admin.ContentBlocks || (function (window) {
        return {
            Apply: function () {
                var showContentsPopup = function (data) {
                    var callback = function (data) {
                        function onsubmit() {
                            AAYW.UI.LoadingIndicator.Show();
                            $.post("[Route:CreateContentBlock]", $(".popup-editor form").serialize(), function (result) {
                                AAYW.UI.LoadingIndicator.Close();
                                AAYW.UI.Popup.Close();

                                if (typeof result == "string") {
                                    AAYW.UI.Popup.Show(result);
                                    $('.popup-editor .submit').click(onsubmit);
                                }
                                else {
                                    AAYW.Engine.Links.RedirectTo("[Route:ContentBlockList]".replace("{page}", 0));
                                }

                            })
                            return false;
                        }

                        function ontypeselect() {
                            if ($('.type-dropdown select').val() == "html") {
                                $('.content-row').show();
                            }
                            else if ($('.type-dropdown select').val() == "redirect") {
                                $('.content-row').show();
                            }
                            else {
                                $('.content-row').hide();
                            }

                        }

                        AAYW.UI.LoadingIndicator.Close();
                        AAYW.UI.Popup.Show(data);
                        AAYW.UI.HtmlEditor.Bind();

                        $('.popup-editor .submit').click(onsubmit);
                        $('.type-dropdown select').change(ontypeselect);

                        ontypeselect();
                    };

                    AAYW.UI.LoadingIndicator.Show();

                    if (typeof data == "string") {
                        $.get("[Route:EditContentBlock]".replace("{id}", data), callback);
                    }
                    else {
                        $.get("[Route:CreateContentBlock]", callback);
                    }
                }

                $('.admin-contents .create').click(showContentsPopup);

                $('.admin-contents .edit-contents').click(function () {
                    showContentsPopup($(this).parents("tr").data("id"));
                    return false;
                });
            },
        };
    })(this);
});