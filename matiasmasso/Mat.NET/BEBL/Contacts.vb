Public Class Contact

    Shared Function Find(oGuid As Guid) As DTOContact
        Dim retval As DTOContact = ContactLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromNum(oEmp As DTOEmp, Id As Integer) As DTOContact
        Dim retval As DTOContact = ContactLoader.FromNum(oEmp, Id)
        Return retval
    End Function

    Shared Function FromNif(oEmp As DTOEmp, nif As String) As DTOContact
        Dim retval As DTOContact = ContactLoader.FromNif(oEmp, nif)
        Return retval
    End Function

    Shared Function Search(sSearchKey As String, oUser As DTOUser, oEmp As DTOEmp, Optional SearchBy As DTOContact.SearchBy = DTOContact.SearchBy.notset) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Select Case SearchBy
            Case DTOContact.SearchBy.notset
                retval = ContactsLoader.Search(oUser, oEmp, sSearchKey)
            Case DTOContact.SearchBy.tel
                retval = ContactsLoader.SearchByTel(sSearchKey, oUser)
            Case DTOContact.SearchBy.email
                retval = ContactsLoader.SearchByEmail(sSearchKey, oUser)
        End Select
        Return retval
    End Function

    Shared Function Tabs(oContact As DTOContact) As List(Of DTOContact.Tabs)
        Dim retval As List(Of DTOContact.Tabs) = ContactLoader.Tabs(oContact)
        Return retval
    End Function

    Shared Function Tel(oContact As DTOContact) As String
        Dim retval As String = ContactLoader.Tel(oContact)
        Return retval
    End Function

    Shared Function Update(oContact As DTOContact, exs As List(Of Exception)) As Boolean
        ContactLoader.Update(oContact, exs)
        Return True
    End Function

    Shared Function FromGLN(oGln As DTOEan) As DTOContact
        Dim retval As DTOContact = ContactLoader.FromGln(oGln)
        Return retval
    End Function

    Shared Function Load(ByRef oContact As DTOContact) As Boolean
        Dim retval As Boolean = ContactLoader.Load(oContact)
        Return retval
    End Function

    Shared Function Delete(ByRef oContact As DTOContact, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContactLoader.Delete(oContact, exs)
        Return retval
    End Function

    Shared Function DefaultUser(oContact As DTOContact) As DTOUser
        Dim retval As DTOUser = Nothing
        Dim oUsers As List(Of DTOUser) = UsersLoader.All(oContact)
        If oUsers.Count > 0 Then
            retval = oUsers(0)
        End If
        Return retval
    End Function

    Shared Function PreviousContacts(ByVal oContact As DTOContact) As List(Of DTOContact)
        Dim exs As New List(Of Exception)
        Dim retval As New List(Of DTOContact)
        Do While oContact IsNot Nothing
            If retval.Any(Function(x) x.Guid.Equals(oContact.Guid)) Then Exit Do
            BEBL.Contact.Load(oContact)
            retval.Add(oContact)
            oContact = oContact.nomAnterior
        Loop
        Return retval
    End Function

End Class
Public Class Contacts

    Shared Function All(oEmp As DTOEmp, oArea As DTOArea) As List(Of DTOContact)
        Return ContactsLoader.All(oEmp, oArea)
    End Function

    Shared Function All(oUser As DTOUser, Optional oArea As DTOArea = Nothing) As List(Of DTOContact)
        Return ContactsLoader.All(oUser, oArea)
    End Function

    Shared Function All(oExercici As DTOExercici, oCta As DTOPgcCta) As List(Of DTOContact)
        Return ContactsLoader.All(oExercici, oCta)
    End Function

    Shared Function All(oUser As DTOUser, oClass As DTOContactClass) As List(Of DTOContact)
        Return ContactsLoader.All(oUser, oClass)
    End Function

    Shared Function All(oUser As DTOUser, oChannel As DTODistributionChannel) As List(Of DTOContact)
        Return ContactsLoader.All(oUser.Emp, oChannel, oUser)
    End Function


    Shared Function FromGLNs(eanValues As HashSet(Of String)) As List(Of DTOContact)
        Return ContactsLoader.FromGLNs(eanValues)
    End Function

    Shared Function RaonsSocials(oUser As DTOUser) As List(Of DTOContact)
        Dim retval As List(Of DTOContact) = ContactsLoader.RaonsSocials(oUser)
        Return retval
    End Function

    Shared Function Countries(oUser As DTOUser) As List(Of DTOCountry)
        'iMat
        Dim oContacts As List(Of DTOContact) = All(oUser)
        Dim retval As List(Of DTOCountry) = oContacts.Select(Function(x) x.address.Zip.Location.Zona.Country).Distinct.ToList
        Return retval
    End Function

    Shared Function Zonas(oUser As DTOUser, oCountry As DTOCountry) As List(Of DTOZona)
        'iMat
        Dim oContacts As List(Of DTOContact) = All(oUser, oCountry)
        Dim retval As List(Of DTOZona) = oContacts.Select(Function(x) x.Address.Zip.Location.Zona).Distinct.ToList
        Return retval
    End Function

    Shared Function Locations(oUser As DTOUser, oZona As DTOZona) As List(Of DTOLocation)
        'iMat
        Dim oContacts As List(Of DTOContact) = All(oUser, oZona)
        Dim retval As List(Of DTOLocation) = oContacts.Select(Function(x) x.Address.Zip.Location).Distinct.ToList
        Return retval
    End Function

    Shared Function Search(oUser As DTOUser, sSearchKey As String, Optional SearchBy As DTOContact.SearchBy = DTOContact.SearchBy.notset) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Select Case SearchBy
            Case DTOContact.SearchBy.notset
                retval = ContactsLoader.Search(oUser, oUser.Emp, sSearchKey, SearchBy)
            Case DTOContact.SearchBy.tel
                retval = ContactsLoader.SearchByTel(sSearchKey, oUser)
            Case DTOContact.SearchBy.email
                retval = ContactsLoader.SearchByEmail(sSearchKey, oUser)
            Case DTOContact.SearchBy.nif
                retval = ContactsLoader.SearchByNif(sSearchKey, oUser)
        End Select
        Return retval
    End Function

    Shared Function MoveToClass(exs As List(Of Exception), oClass As DTOContactClass, oContacts As List(Of DTOContact)) As Boolean
        Dim retval As Boolean = ContactsLoader.MoveToClass(oClass, oContacts, exs)
        Return retval
    End Function

    Shared Function AutoCompleteString(oEmp As DTOEmp, sKey As String) As List(Of String)
        Return ContactsLoader.AutoCompleteString(oEmp, sKey)
    End Function


    Shared Function reZip(exs As List(Of Exception), oZipTo As DTOZip, oContacts As List(Of DTOContact)) As Integer
        Return ContactsLoader.reZip(exs, oZipTo, oContacts)
    End Function
End Class
