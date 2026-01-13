Public Class Contact
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOContact)
        Return Await Api.Fetch(Of DTOContact)(exs, "contact", oGuid.ToString())
    End Function

    Shared Async Function FromGln(sEan As String, exs As List(Of Exception)) As Task(Of DTOContact)
        Return Await Api.Fetch(Of DTOContact)(exs, "contact/fromGln", sEan)
    End Function

    Shared Async Function FromNif(exs As List(Of Exception), oEmp As DTOEmp, nif As String) As Task(Of DTOContact)
        Return Await Api.Fetch(Of DTOContact)(exs, "contact/fromNif", oEmp.Id, nif)
    End Function

    Shared Function FindSync(oGuid As Guid, exs As List(Of Exception)) As DTOContact
        Return Api.FetchSync(Of DTOContact)(exs, "contact", oGuid.ToString())
    End Function

    Shared Async Function Logo(exs As List(Of Exception), oContact As DTOContact) As Task(Of Byte())
        Dim retval = Await Api.FetchImage(exs, "contact/logo", oContact.Guid.ToString())
        Return retval
    End Function

    Shared Function Load(ByRef oContact As DTOContact, exs As List(Of Exception)) As Boolean
        'Shared Function Load(ByRef oContact As DTOContact, exs As List(Of Exception), Optional includeLogo As Boolean = False) As Boolean
        If Not oContact.IsLoaded And Not oContact.IsNew Then
            Dim pContact = Api.FetchSync(Of DTOContact)(exs, "Contact", oContact.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOContact)(pContact, oContact, exs)
            End If
        End If

        'If includeLogo Then
        'oContact.Logo = Api.FetchImageSync(exs, "contact/logo", oContact.Guid.ToString())
        'End If

        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOContact) As Task(Of Integer)
        Dim retval As Integer
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("Logo", value.Logo)
            retval = Await Api.Upload(Of Integer)(oMultipart, exs, "Contact")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oContact As DTOContact) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOContact)(oContact, exs, "Contact")
    End Function

    Shared Async Function Search(exs As List(Of Exception), oUser As DTOUser, searchKey As String, Optional searchBy As DTOContact.SearchBy = DTOContact.SearchBy.notset) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "contact/search", oUser.Guid.ToString, CInt(searchBy), searchKey)
    End Function

    Shared Function SearchSync(exs As List(Of Exception), oUser As DTOUser, searchKey As String, Optional searchBy As DTOContact.SearchBy = DTOContact.SearchBy.notset) As List(Of DTOContact)
        Return Api.FetchSync(Of List(Of DTOContact))(exs, "contact/search", oUser.Guid.ToString, CInt(searchBy), searchKey)
    End Function


    Shared Async Function DefaultUser(oContact As DTOContact, exs As List(Of Exception)) As Task(Of DTOUser)
        Return Await Api.Fetch(Of DTOUser)(exs, "contact/defaultUser", oContact.Guid.ToString())
    End Function

    Shared Async Function PreviousContacts(oContact As DTOContact, exs As List(Of Exception)) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "contact/PreviousContacts", oContact.Guid.ToString())
    End Function

    Shared Function PreviousContactsSync(oContact As DTOContact, exs As List(Of Exception)) As List(Of DTOContact)
        Return Api.FetchSync(Of List(Of DTOContact))(exs, "contact/PreviousContacts", oContact.Guid.ToString())
    End Function

    Shared Function IsImpagatSync(oContact As DTOContact) As Boolean
        Dim exs As New List(Of Exception)
        Dim value = Api.FetchSync(Of Integer)(exs, "customer/isImpagat", oContact.Guid.ToString())
        Return value = 1
    End Function

    Shared Async Function IsImpagat(oContact As DTOContact, exs As List(Of Exception)) As Task(Of Boolean)
        Dim value = Await Api.Fetch(Of Boolean)(exs, "customer/isImpagat", oContact.Guid.ToString())
        Return value
    End Function

    Shared Async Function Tabs(exs As List(Of Exception), oContact As DTOContact) As Task(Of List(Of DTOContact.Tabs))
        Return Await Api.Fetch(Of List(Of DTOContact.Tabs))(exs, "contact/Tabs", oContact.Guid.ToString())
    End Function

    Shared Function HtmlNameAndAddress(oContact As DTOContact) As String
        Dim exs As New List(Of Exception)
        FEB2.Contact.Load(oContact, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(oContact.Nom)
        sb.AppendLine(DTOAddress.MultiLine(oContact.Address))
        Dim retval As String = sb.ToString.Replace(vbCrLf, "<br/>")
        Return retval
    End Function

    Shared Function HtmlNameComercialAndAddress(oContact As DTOContact) As String
        Dim exs As New List(Of Exception)
        FEB2.Contact.Load(oContact, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(oContact.NomComercialOrDefault())
        sb.AppendLine(DTOAddress.MultiLine(oContact.Address))
        Dim retval As String = sb.ToString.Replace(vbCrLf, "<br/>")
        Return retval
    End Function


    Shared Async Function Tel(exs As List(Of Exception), oContact As DTOContact) As Task(Of String)
        Return Await Api.Fetch(Of String)(exs, "contact/Tel", oContact.Guid.ToString())
    End Function

    Shared Function TelSync(exs As List(Of Exception), oContact As DTOContact) As String
        Return Api.FetchSync(Of String)(exs, "contact/Tel", oContact.Guid.ToString())
    End Function


    Shared Function Url(oContact As DTOContact, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oContact IsNot Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "contacto", oContact.Guid.ToString())
        End If
        Return retval
    End Function


    Shared Function MailAddress(oContact As DTOContact) As DTOAddress
        Dim exs As New List(Of Exception)
        Dim retval As DTOAddress = FEB2.Address.FindSync(oContact, DTOAddress.Codis.Correspondencia, exs)
        If retval Is Nothing Then
            retval = oContact.Address
        End If
        Return retval
    End Function

    Shared Async Function emailAddress(oContact As DTOContact, exs As List(Of Exception)) As Task(Of String)
        Dim retval As String = ""
        Dim oEmails = Await FEB2.Emails.All(exs, oContact)
        If oEmails.Count > 0 Then
            retval = oEmails.First.EmailAddress
        End If
        Return retval
    End Function

    Shared Async Function Movil(oContact As DTOContact, exs As List(Of Exception)) As Task(Of String)
        Dim retval As String = ""
        Dim oTels = Await FEB2.ContactTels.All(oContact, DTOContactTel.Cods.movil, exs)
        If oTels.Count > 0 Then
            Dim oTel As DTOContactTel = oTels.First
            retval = DTOContactTel.Formatted(oTel)
        End If
        Return retval
    End Function



End Class

Public Class Contacts
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oArea As DTOArea) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "contacts/fromArea", oEmp.Id, oArea.Guid.ToString())
    End Function
    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser, Optional oArea As DTOArea = Nothing) As Task(Of List(Of DTOContact))
        'per iMat, web
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "contacts/fromUser", oUser.Guid.ToString, OpcionalGuid(oArea))
    End Function

    Shared Async Function All(exs As List(Of Exception), oExercici As DTOExercici, oCta As DTOPgcCta) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "contacts/fromCta", oExercici.Emp.Id, oExercici.Year, oCta.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser, oClass As DTOContactClass) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "contacts/fromClass", oUser.Guid.ToString, oClass.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser, oChannel As DTODistributionChannel) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "contacts/fromChannel", oUser.Guid.ToString, oChannel.Guid.ToString())
    End Function

    Shared Async Function RaonsSocials(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "contacts/RaonsSocials", oUser.Guid.ToString())
    End Function

    Shared Function RaonsSocialsSync(exs As List(Of Exception), oUser As DTOUser) As List(Of DTOContact)
        Return Api.FetchSync(Of List(Of DTOContact))(exs, "contacts/RaonsSocials", oUser.Guid.ToString())
    End Function

    Shared Async Function Search(exs As List(Of Exception), oUser As DTOUser, searchKey As String, Optional searchBy As DTOContact.SearchBy = DTOContact.SearchBy.notset) As Task(Of List(Of DTOContact))
        Return Await Api.Execute(Of String, List(Of DTOContact))(searchKey, exs, "contacts/search", oUser.Guid.ToString, searchBy)
    End Function

    Shared Async Function Zonas(exs As List(Of Exception), oUser As DTOUser, oCountry As DTOCountry) As Task(Of List(Of DTOZona))
        Return Await Api.Fetch(Of List(Of DTOZona))(exs, "contacts/zonas", oUser.Guid.ToString, oCountry.Guid.ToString())
    End Function

    Shared Async Function MoveToClass(exs As List(Of Exception), oClass As DTOContactClass, oContacts As List(Of DTOContact)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOContact), Boolean)(oContacts, exs, "contacts/moveToClass", oClass.Guid.ToString())
    End Function

    Shared Async Function AutoCompleteString(exs As List(Of Exception), oEmp As DTOEmp, searchKey As String) As Task(Of List(Of String))
        Return Await Api.Execute(Of String, List(Of String))(searchKey, exs, "contacts/AutoCompleteString", oEmp.Id)
    End Function

    Shared Async Function reZip(exs As List(Of Exception), oZipTo As DTOZip, oContacts As List(Of DTOContact)) As Task(Of Integer)
        Dim retval = Await Api.Execute(Of List(Of DTOContact), Integer)(oContacts, exs, "Contacts/rezip", oZipTo.Guid.ToString())
        Return retval
    End Function

End Class
