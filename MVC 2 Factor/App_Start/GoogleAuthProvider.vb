Imports Microsoft.AspNet.Identity
Imports System.Threading.Tasks

Public Class GoogleAuthProvider
    Implements IUserTokenProvider(Of ApplicationUser, String)

    Public Function GenerateAsync(purpose As String, manager As UserManager(Of ApplicationUser, String), user As ApplicationUser) As Task(Of String) Implements IUserTokenProvider(Of ApplicationUser, String).GenerateAsync
        Return Nothing

    End Function

    Public Function IsValidProviderForUserAsync(manager As UserManager(Of ApplicationUser, String), user As ApplicationUser) As Task(Of Boolean) Implements IUserTokenProvider(Of ApplicationUser, String).IsValidProviderForUserAsync
        Return Task.FromResult(user.IsGoogleAuthenticatorEnabled)
    End Function

    Public Function NotifyAsync(token As String, manager As UserManager(Of ApplicationUser, String), user As ApplicationUser) As Task Implements IUserTokenProvider(Of ApplicationUser, String).NotifyAsync
        Return Task.FromResult(True)
    End Function

    Public Function ValidateAsync(purpose As String, token As String, manager As UserManager(Of ApplicationUser, String), user As ApplicationUser) As Task(Of Boolean) Implements IUserTokenProvider(Of ApplicationUser, String).ValidateAsync

        Dim tfa As New Google.Authenticator.TwoFactorAuthenticator
        tfa.DefaultClockDriftTolerance = TimeSpan.FromMinutes(1.5)

        Return Task.FromResult(tfa.ValidateTwoFactorPIN(user.GoogleAuthenticatorSecretKey, token))
    End Function

    Shared Function GenerateSecretKey() As String
        Dim buffer As Byte() = New Byte(19) {}
        Dim rng As New System.Security.Cryptography.RNGCryptoServiceProvider
        rng.GetNonZeroBytes(buffer)

        Dim sb As New StringBuilder
        For Each bt In buffer
            Dim num As Integer = bt

            sb.Append(ChrW(num))
        Next
        Return HttpUtility.UrlEncode(sb.ToString)

    End Function

End Class
