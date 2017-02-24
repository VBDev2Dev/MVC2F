Imports System.ComponentModel.DataAnnotations
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography

Module Extensions

    <Extension>
    Public Function GetAttribute(Of T As Type)(MI As MemberInfo, AttType As T, Optional Inherit As Boolean = True) As Attribute

        Dim Attrs As Attribute() = CType(MI.GetCustomAttributes(AttType, Inherit), Attribute())
        If Attrs.Count > 0 Then
            Return Attrs(0)
        End If

        Attrs = CType(MI.DeclaringType.GetCustomAttributes(GetType(MetadataTypeAttribute), True), Attribute())
        Dim metaAttr As MetadataTypeAttribute = If(Attrs.Count < 1, CType(Nothing, MetadataTypeAttribute), TryCast(Attrs(0), MetadataTypeAttribute))
        Dim metaProps As MemberInfo() = If(metaAttr IsNot Nothing, metaAttr.MetadataClassType.GetMember(MI.Name), CType(Nothing, MemberInfo()))
        If metaProps.Count = 0 Then
            Return Nothing
        End If

        Attrs = CType(metaProps(0).GetCustomAttributes(AttType, True), Attribute())
        If Attrs.Length < 1 Then
            Return Nothing
        End If
        Return Attrs(0)
    End Function

    <Extension>
    Public Function ToSHA256(input As String, Optional Salt As String = "") As String

        Using sha As SHA256Managed = CType(SHA256Managed.Create, SHA256Managed)

            Dim enc = Encoding.UTF8
            Dim strBytes = enc.GetBytes(input & Salt)
            Return enc.GetString(sha.ComputeHash(strBytes))

        End Using

    End Function

    <Extension>
    Public Function RenderToString(Result As ViewResultBase, ControllerContext As ControllerContext, TempData As TempDataDictionary) As String
        Dim viewdata As ViewDataDictionary = Result.ViewData
        Dim viewResult As ViewEngineResult = ViewEngines.Engines.FindPartialView(ControllerContext, Result.ViewName)

        Using writer As New System.IO.StringWriter

            Dim viewContext As ViewContext = New ViewContext(ControllerContext, viewResult.View, viewdata, TempData, writer)

            viewResult.View.Render(viewContext, writer)
            Return writer.GetStringBuilder().ToString()
        End Using
    End Function

    <Extension>
    Function AbsoluteContent(helper As UrlHelper, relPath As String) As String
        Dim url As New Uri(HttpContext.Current.Request.Url, helper.Content(relPath))
        Return url.AbsoluteUri
    End Function


End Module
