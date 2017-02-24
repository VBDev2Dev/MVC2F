@ModelType LoginViewModel
@Code
    ViewBag.Title = "Log in"
End Code

<h2>@ViewBag.Title.</h2>
<div class="row">
    <div class="col-md-8">
        @ViewBag.ReturnUrl

        <section id="loginForm">
            @Using Ajax.BeginForm("Login", "Account", New With {.ReturnUrl = ViewBag.ReturnUrl}, New AjaxOptions With {
                                            .HttpMethod = "Post",
                                            .InsertionMode = InsertionMode.InsertAfter,
                                            .UpdateTargetId = "VerifyDialogPH",
                                            .OnSuccess = "LoginSuccess",
                                           .OnBegin = "DoLogin"
                                 }, New With {.class = "form-horizontal", .role = "form"})
                @*@Using Html.BeginForm("Login", "Account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})*@
                @Html.AntiForgeryToken()
                @<text>

                    <div id="Notice" style="display:none">
                    </div>
                    <h4>Use a local account to log in.</h4>
                    <hr />
                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                    <div class="form-group">
                        @Html.LabelFor(Function(m) m.Email, New With {.class = "col-md-2 control-label"})
                        <div class="col-md-10">
                            @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        @Html.LabelFor(Function(m) m.Password, New With {.class = "col-md-2 control-label"})
                        <div class="col-md-10">
                            @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                @Html.CheckBoxFor(Function(m) m.RememberMe)
                                @Html.LabelFor(Function(m) m.RememberMe)
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <input type="submit" value="Log in" class="btn btn-default" onclick="HideNotice();" />
                        </div>
                    </div>
                    <p>
                        @Html.ActionLink("Register as a new user", "Register")
                    </p>
                    @* Enable this once you have account confirmation enabled for password reset functionality
                        <p>
                            @Html.ActionLink("Forgot your password?", "ForgotPassword")
                        </p>*@
                </text>
            End Using
        </section>
    </div>
    <div class="col-md-4">
        <section id="socialLoginForm">
            @Html.Partial("_ExternalLoginsListPartial", New ExternalLoginListViewModel With {.ReturnUrl = ViewBag.ReturnUrl})
        </section>
    </div>
</div>

<div id="VerifyDialogPH">
    @Html.Partial("VerifyGoogleCode", New VerifyCodeViewModel() With {
                                                                                                                               .Provider = "GoogleAuthenticator",
                                                                                                                               .ReturnUrl = ViewBag.returnurl,
                                                                                                                               .Code = "",
                                                                                                                               .RememberBrowser = False,
                                                                                                                               .RememberMe = False})
</div>

@Section Scripts

    <script>

        function HideNotice() {
            $("#Notice").hide();
        }

        function LoginSuccess(loginResult) {
            HideWait();
            dtfmt = new Intl.DateTimeFormat("en-us", { hour12: true, hour: 'numeric', minute: 'numeric', second: 'numeric', year: 'numeric', month: 'long', day: 'numeric' });
            switch (loginResult["Status"]) {
                case 0:
                    //success
                    window.location.replace( loginResult["ReturnURL"]);
                    break;
                case 1:
                    //lockout
                    message = '<div id="Notice">Your account has been locked out.  Please check back after ' +
                        dtfmt.format(ToJavaScriptDate(loginResult["Lockout"])) + ' to login again.</div>';

                    $("#Notice").replaceWith(message);
                    $("#Notice").addClass("alert alert-warning").show();
                    break;
                case 2:
                    //RequiresVerification
                    ShowVerify();
                    break;
                case 3:
                    //Failure

                    message = '<div id="Notice">The User Name/Password combination does not match.</div>';

                    $("#Notice").replaceWith(message);
                    $("#Notice").addClass("alert alert-danger").show();

                    break;
            }
        }

        function DoLogin() {
            ShowWait("<h1>Logging in...</h1>");
        }
    </script>
End Section
