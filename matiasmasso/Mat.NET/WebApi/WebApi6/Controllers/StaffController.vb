Public Class StaffController
    Inherits _BaseController

    <HttpPost>
    <Route("api/staffs")>
    Public Function Staffs(user As DTOGuidNom) As List(Of DUI.Staff)
        Dim retval As New List(Of DUI.Staff)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        If oUser IsNot Nothing Then
            Dim oStaffs As List(Of DTOStaff) = BLLStaffs.All()
            For Each oStaff As DTOStaff In oStaffs
                Dim item As New DUI.Staff
                With item
                    .Guid = oStaff.Guid
                    .Abr = oStaff.Abr
                    .Nom = BLLStaff.AliasOrNom(oStaff)
                    .Posicio = BLLStaffPos.Nom(oStaff.StaffPos, oUser.Lang)
                End With
                retval.Add(item)
            Next
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/staff")>
    Public Function Load(src As DUI.UserStaff) As DUI.Staff
        Dim retval As DUI.Staff = Nothing
        Dim oStaff As DTOStaff = BLLStaff.Find(src.Staff.Guid)
        If oStaff IsNot Nothing Then
            retval = New DUI.Staff
            oStaff.IsLoaded = False
            BLLContact.Load(oStaff)
            Dim oTels As List(Of DTOContactTel) = BLLContactTels.All(oStaff)
            Dim oEmails As List(Of DTOEmail) = BLLEmails.All(oStaff)
            With retval
                .Guid = src.Staff.Guid
                .Abr = oStaff.Abr
                .Nom = oStaff.Nom
                .Nif = oStaff.Nif
                .Alta = IIf(oStaff.Alta = Nothing, "", Format(oStaff.Alta, "yyyyMMdd"))
                .Baixa = IIf(oStaff.Baixa = Nothing, "", Format(oStaff.Baixa, "yyyyMMdd"))
                .Birth = IIf(oStaff.Birth = Nothing, "", Format(oStaff.Birth, "yyyyMMdd"))
                .AvatarUrl = BLLStaff.AvatarUrl(oStaff, True)
                .NumSs = oStaff.NumSs
                .Iban = BLLIban.Formated(oStaff.Iban)
                .Address = BLLAddress.Text(oStaff.Address)
                .Location = BLLAddress.ZipyCit(oStaff.Address)
                .Posicio = BLLStaffPos.Nom(oStaff.StaffPos, DTOLang.CAT)

                .Tels = New List(Of DUI.Tel)
                For Each oTel As DTOContactTel In oTels
                    Dim item As New DUI.Tel
                    item.Num = BLLTel.Formatted(oTel.Value)
                    item.Obs = oTel.Obs
                    .Tels.Add(item)
                Next

                .Emails = New List(Of DUI.Email)
                For Each oEmail As DTOEmail In oEmails
                    Dim item As New DUI.Email
                    item.Address = oEmail.EmailAddress
                    item.Obs = oEmail.Obs
                    .Emails.Add(item)
                Next
            End With
        End If

        Return retval
    End Function


    <HttpPost>
    <Route("api/nomines")>
    Public Function Nomines(src As DUI.Staff) As DUI.Staff
        Dim retval As DUI.Staff = Nothing
        Dim oStaff As DTOStaff = BLLStaff.Find(src.Guid)
        If oStaff IsNot Nothing Then
            retval = New DUI.Staff
            oStaff.IsLoaded = False
            BLLContact.Load(oStaff)
            Dim oTels As List(Of DTOContactTel) = BLLContactTels.All(oStaff)
            Dim oEmails As List(Of DTOEmail) = BLLEmails.All(oStaff)
            With retval
                .Guid = src.Guid
                .Abr = oStaff.Abr
                .Nom = oStaff.Nom
                .Alta = IIf(oStaff.Alta = Nothing, "", Format(oStaff.Alta, "yyyyMMdd"))
                .Baixa = IIf(oStaff.Baixa = Nothing, "", Format(oStaff.Baixa, "yyyyMMdd"))
                .Birth = IIf(oStaff.Birth = Nothing, "", Format(oStaff.Birth, "yyyyMMdd"))
                .AvatarUrl = BLLStaff.AvatarUrl(oStaff, True)
                .NumSs = oStaff.NumSs
                .Iban = BLLIban.Formated(oStaff.Iban)
                .Address = BLLAddress.Text(oStaff.Address)
                .Location = BLLAddress.ZipyCit(oStaff.Address)
                .Posicio = BLLStaffPos.Nom(oStaff.StaffPos, DTOLang.CAT)

                .Tels = New List(Of DUI.Tel)
                For Each oTel As DTOContactTel In oTels
                    Dim item As New DUI.Tel
                    item.Num = BLLTel.Formatted(oTel.Value)
                    item.Obs = oTel.Obs
                    .Tels.Add(item)
                Next

                .Emails = New List(Of DUI.Email)
                For Each oEmail As DTOEmail In oEmails
                    Dim item As New DUI.Email
                    item.Address = oEmail.EmailAddress
                    item.Obs = oEmail.Obs
                    .Emails.Add(item)
                Next
            End With
        End If

        Return retval
    End Function

End Class
