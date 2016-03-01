$(window).load(function () {
    $(".admin-inspector-edit").click(function () {
        AAYW.UI.LoadingIndicator.Show();
        $.get("/admin/entity/edit/" + $(this).data("type") + "/" + $(this).data("target"), function (data) {
            AAYW.UI.LoadingIndicator.Close();
            AAYW.UI.Popup.Show(data);
            $(".admin-edit-entity form input").each(function (index, el) {
                var $el = $(el);
                if ($el.data("locked") == "True")
                    $el.prop("disabled", "disabled");
            });

            
            $('.submit').click(function () {
                AAYW.UI.LoadingIndicator.Show();
                $.post("/admin/entity/save", $(".admin-edit-entity form").serialize(), function () {
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
});