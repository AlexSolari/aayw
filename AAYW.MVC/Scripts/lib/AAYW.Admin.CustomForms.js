$(window).load(function myfunction() {
    window.AAYW = window.AAYW || {};

    AAYW.Admin = AAYW.Admin || {};

    AAYW.Admin.CustomForms = AAYW.Admin.CustomForms || (function (window) {
        return {
            Apply: function () {
                var lastFieldIndex = 0;

                var bindFormConstructor = function () {

                    $('.form-constructor .add').click(function () {
                        AAYW.UI.LoadingIndicator.Show();
                        $.post("[Route:CustomFormField]", { index: lastFieldIndex }, function (result) {
                            AAYW.UI.LoadingIndicator.Close();
                            $(".form-fields").append(result);
                            $(window).scrollTop($("body").height());
                        });
                        lastFieldIndex++;
                        return false;
                    });

                    $('.form-constructor .remove').click(function (e) {
                        e.preventDefault();
                        $(".form-fields .field").last().remove();
                        lastFieldIndex--;
                        if (lastFieldIndex < 0)
                            lastFieldIndex = 0;
                        return false;
                    });
                };

                var showFormPopup = function (data) {
                    var callback = function (data) {
                        function onsubmit() {
                            AAYW.UI.LoadingIndicator.Show();
                            $.post("[Route:CreateCustomForm]", $(".popup-editor form").serialize(), function (result) {
                                AAYW.UI.LoadingIndicator.Close();
                                AAYW.UI.Popup.Close();

                                if (typeof result == "string") {
                                    AAYW.UI.Popup.Show(result);
                                    $('.popup-editor .submit').click(onsubmit);
                                }
                                else {
                                    AAYW.Engine.Links.RedirectTo("[Route:CustomFormsList]".replace("{page}", 0));
                                }

                            })
                            return false;
                        }

                        AAYW.UI.LoadingIndicator.Close();
                        AAYW.UI.Popup.Show(data);

                        bindFormConstructor();

                        $('.popup-editor .submit').click(onsubmit);
                    };

                    AAYW.UI.LoadingIndicator.Show();

                    if (typeof data == "string") {
                        $.get("[Route:EditUserForm]".replace("{id}", data), callback);
                    }
                    else {
                        $.get("[Route:CreateCustomForm]", callback);
                    }
                }

                $('.admin-custom-form .create').click(showFormPopup);

                $('.admin-custom-form .edit-form').click(function () {
                    showFormPopup($(this).parents("tr").data("id"));
                    return false;
                });
            },
        };
    })(this);
});