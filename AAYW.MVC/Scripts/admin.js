$(window).load(function () {
    var lastFieldIndex = 0;

    //Entity inspector
    $(".admin-inspector-edit").click(function () {
        AAYW.UI.LoadingIndicator.Show();
        $.get("[Route:EditEntity]".replace("{type}",$(this).data("type")).replace("{id}",$(this).data("target")), function (data) {
            AAYW.UI.LoadingIndicator.Close();
            AAYW.UI.Popup.Show(data);
            $(".popup-editor form input").each(function (index, el) {
                var $el = $(el);
                if ($el.data("locked") == "True")
                    $el.parents(".row").addClass("hidden");
            });

            
            $('.popup-editor .submit').click(function () {
                AAYW.UI.LoadingIndicator.Show();
                $.post("[Route:SaveEntity]", $(".popup-editor form").serialize(), function () {
                    AAYW.UI.LoadingIndicator.Close();
                    var id = $(".popup-editor form input[name='modelData[Id@System.Guid]']").val();
                    var row = $("#" + id);

                    $(".popup-editor form input").each(function (index, el) {
                        var $el = $(el);
                        $("#" + $el.prop("id").split("edit-")[1]).html($el.val());
                    });

                    AAYW.UI.Popup.Close();
                })
            });
        });
    });

    //Mail settings
    $(".admin-mail-settings .submit").click(function () {
        AAYW.UI.LoadingIndicator.Show();
        $.post("[Route:MailSettings]", $(".admin-mail-settings form").serialize(), function () {
            AAYW.UI.LoadingIndicator.Close();
        })
    });

    //Mail templates
    var showTemplatePopup = function (data) {
        var callback = function (data) {
            function onsubmit() {
                AAYW.UI.LoadingIndicator.Show();
                $.post("[Route:CreateMailTemplate]", $(".popup-editor form").serialize(), function (result) {
                    AAYW.UI.LoadingIndicator.Close();
                    AAYW.UI.Popup.Close();

                    if (!result) {
                        document.location.href = "[Route:MailTemplates]".replace("{page}", 0);
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

            $('.popup-editor .submit').click(onsubmit);
        };

        AAYW.UI.LoadingIndicator.Show();

        if (data instanceof String)
        {
            $.get("[Route:EditMailTemplate]".replace("{id}", data), callback);
        }
        else
        {
            $.get("[Route:CreateMailTemplate]", callback);            
        }
    }

    $('.admin-mail-template .create').click(showTemplatePopup);

    $('.admin-mail-template .edit-template').click(function () {
        showTemplatePopup($(this).parents("tr").data("id"));
        return false;
    });

    //Custom forms
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

        $('.form-constructor .remove').click(function () {
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
                        document.location.href = "[Route:CustomFormsList]".replace("{page}", 0);
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
});