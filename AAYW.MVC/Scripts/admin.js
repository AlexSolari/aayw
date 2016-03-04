$(window).load(function () {
    var lastFieldIndex = 0;

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

    $(".admin-mail-settings .submit").click(function () {
        AAYW.UI.LoadingIndicator.Show();
        $.post("[Route:MailSettings]", $(".admin-mail-settings form").serialize(), function () {
            AAYW.UI.LoadingIndicator.Close();
        })
    });

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
});