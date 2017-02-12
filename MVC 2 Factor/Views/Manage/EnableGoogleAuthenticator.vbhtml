@ModelType GoogleAuthenticatorViewModel

@Code
    ViewData("Title") = "EnableGoogleAuthenticator"
End Code

<h2>EnableGoogleAuthenticator</h2>

<div class="row">
    <div class="col-md-8">
        <h3>1. Add License Manager to Google Authenticator</h3>
        <p>Open Google Authenticator and add License Manager by scanning the QR Code to the right.</p>
        <h3>2. Enter the 6 digit code that Google Authenticator generates</h3>
        <p>
            Verify that License Manager is added correctly in Google Authenticator by entering the 6 digit code which
            Google Authenticator generates for License Manager below, and then click Enable.
        </p>
        @Html.ValidationSummary(False)

        @code


            Using Html.BeginForm("EnableGoogleAuthenticator", "Manage", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})





            @<div class="form-group">
                @Html.AntiForgeryToken()
                <div class="col-md-6">
                    @Html.HiddenFor(Function(m) m.SecretKey)
                    @Html.Label(Model.SecretKey)
                    <figure>
                        <img src="@Model.SetupCode.QrCodeSetupImageUrl" />
                        <figcaption>
                            <label class="control-label">Manual Entry Code:</label> @Model.SetupCode.ManualEntryKey
                        </figcaption>
                    </figure>
                </div>
            </div>

            @<div class="form-group">
                @Html.LabelFor(Function(m) m.Code, New With {.class = "col-md-2 control-label"})
                <div class="col-md-10">
                    @Html.TextBoxFor(Function(m) m.Code, New With {.class = "form-control"})
                </div>
            </div>

            @<div class="form-group">
                <div class="col-md-offset-2 col-md-10">

                    @If Model.IsGoogleAuthenticationEnabled Then

                        @<div class="alert alert-danger alert-dismissible">
                            <button id="closeAlert" type="button" class="close">×</button>
                            Warning!  Google Athenticator is already enabled.  This will invalidate the current authenticator.
                        </div>

                        End If
                    <input type="submit" class="btn btn-default" value="Enable" />
                </div>
            </div>
            End Using
        End code
    </div>

</div>
