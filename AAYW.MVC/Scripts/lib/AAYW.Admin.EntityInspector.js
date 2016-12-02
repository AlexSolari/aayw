$(window).load(function myfunction() {
    window.AAYW = window.AAYW || {};

    AAYW.Admin = AAYW.Admin || {};

    AAYW.Admin.EntityInspector = AAYW.Admin.EntityInspector || (function (window) {
        return {
            Apply: function () {
                $(".admin-inspector-edit").click(function () {
                    AAYW.UI.LoadingIndicator.Show();
                    $.get("[Route:EditEntity]".replace("{type}", $(this).data("type")).replace("{id}", $(this).data("target")), function (data) {
                        AAYW.UI.LoadingIndicator.Close();
                        AAYW.UI.Popup.Show(data);
                        AAYW.UI.HtmlEditor.Bind();
                        $(".popup-editor form input").each(function (index, el) {
                            var $el = $(el);
                            if ($el.data("locked") == "True")
                                $el.parents(".row").addClass("hidden");
                        });

                        $('.popup-editor .delete').click(function (e) {
                            e.preventDefault();
                            var id = $('form input[name="modelData[Id@System.Guid]"]').val();
                            var type = $("form input[name=type]").val();
                            AAYW.UI.LoadingIndicator.Show();
                            $.post("[Route:DeleteEntity]", { id: id, type: type }, function () {
                                AAYW.UI.LoadingIndicator.Close();
                                AAYW.UI.Popup.Close();
                                $("tr:has(#" + id + "_Id)").remove();
                            })
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
            },
        };
    })(this);
});