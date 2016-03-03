$(window).load(function () {
    $(".admin-inspector-edit").click(function () {
        AAYW.UI.LoadingIndicator.Show();
        $.get("[Route:EditEntity]".replace("{type}",$(this).data("type")).replace("{id}",$(this).data("target")), function (data) {
            AAYW.UI.LoadingIndicator.Close();
            AAYW.UI.Popup.Show(data);
            $(".admin-edit-entity form input").each(function (index, el) {
                var $el = $(el);
                if ($el.data("locked") == "True")
                    $el.parents(".row").addClass("hidden");
            });

            
            $('.admin-edit-entity .submit').click(function () {
                AAYW.UI.LoadingIndicator.Show();
                $.post("[Route:SaveEntity]", $(".admin-edit-entity form").serialize(), function () {
                    AAYW.UI.LoadingIndicator.Close();
                    var id = $(".admin-edit-entity form input[name='modelData[Id@System.Guid]']").val();
                    var row = $("#" + id);

                    $(".admin-edit-entity form input").each(function (index, el) {
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
});