@ModelType VerifyCodeViewModel

@Using Html.JQueryUI().Begin(New Dialog(New With {.id = "VerifyDialog"}).
                                                                            Position(HorizontalPosition.Center, VerticalPosition.Center).
                                                                            CloseOnEscape(False).
                                                                            Modal(True).
                                                                            Title("Verify Code").
                                                                            DialogClass("no-close").
                                                                            OnOpen("OpenVerify").
                                                                            AutoOpen(False).
                                                                            AppendTo("#VerifyDialogPH")
                                                                            )

    @Using Ajax.BeginForm("VerifyCode", "Account", New With {.ReturnUrl = Model.ReturnUrl}, New AjaxOptions() With {
                                                 .HttpMethod = "Post", .InsertionMode = InsertionMode.Replace, .OnSuccess = "VerifySuccess"}, New With {.class = "form-horizontal", .role = "form"})
        @Html.AntiForgeryToken()
        @Html.Hidden("provider", Model.Provider)
        @Html.Hidden("provider", Model.ReturnUrl)
        @Html.Hidden("rememberMe", Model.RememberMe)
        @<text>
            <h4>Enter verification code</h4>
            <hr />
            @Html.ValidationSummary("", New With {.class = "text-danger"})
            <div id="VerifyResultPH" class="alert alert-warning" style="display:none;">
            </div>
            <div class="form-group">
                @Html.LabelFor(Function(m) m.Code, New With {.class = "col-md-2 control-label"})
                <div class="col-md-10">
                    @Html.TextBoxFor(Function(m) m.Code, New With {.class = "form-control", .autocomplete = "off"})
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-10 col-md-offset-2">
                    @Html.EditorFor(Function(m) m.RememberBrowser)
                    @Html.Label("Remember Browser?")
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" class="btn btn-default" value="Submit" />
                </div>
            </div>
        </text>
    End Using

End Using

<script>
    function ShowVerify() {
        $("#VerifyResultPH").hide();

        $("#VerifyDialog").dialog("open");
    }

    function VerifySuccess(verifyResult) {
        if (verifyResult["Success"] == true)
            window.location = verifyResult["returnUrl"]
        else
            $("#VerifyResultPH").text(verifyResult["Message"]).fadeIn(500);
    }
</script>
