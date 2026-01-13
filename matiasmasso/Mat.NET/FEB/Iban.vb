Public Class Iban


    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOIban)
        Return Await Api.Fetch(Of DTOIban)(exs, "Iban", oGuid.ToString())
    End Function

    Shared Async Function FromCcc(exs As List(Of Exception), ccc As String) As Task(Of DTOIban)
        Dim cleanCcc = DTOIban.CleanCcc(ccc)
        Return Await Api.Fetch(Of DTOIban)(exs, "Iban/FromCcc", cleanCcc)
    End Function

    Shared Function Load(ByRef oIban As DTOIban, exs As List(Of Exception)) As Boolean
        If oIban IsNot Nothing AndAlso Not oIban.IsLoaded AndAlso Not oIban.IsNew Then
            Dim pIban = Api.FetchSync(Of DTOIban)(exs, "Iban", oIban.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOIban)(pIban, oIban, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOIban, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.docFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.docFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.docFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Iban")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oIban As DTOIban, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOIban)(oIban, exs, "Iban")
    End Function

    Shared Async Function FromContact(exs As List(Of Exception), oContact As DTOContact, Optional oCod As DTOIban.Cods = DTOIban.Cods._NotSet, Optional DtFch As Date = Nothing) As Task(Of DTOIban)
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Dim retval = Await Api.Fetch(Of DTOIban)(exs, "Iban/FromContact", oContact.Guid.ToString, oCod, DtFch.ToString("yyyy-MM-dd"))
        Return retval
    End Function

    Shared Function FromContactSync(exs As List(Of Exception), oContact As DTOContact, Optional oCod As DTOIban.Cods = DTOIban.Cods._NotSet, Optional DtFch As Date = Nothing) As DTOIban
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Return Api.FetchSync(Of DTOIban)(exs, "Iban/FromContact", oContact.Guid.ToString, oCod, DtFch.ToString("yyyy-MM-dd"))
    End Function

    Shared Function Swift(oIban As DTOIban) As String
        Dim retval As String = ""
        If oIban.BankBranch Is Nothing Then
            Dim oBank = DTOIban.Bank(oIban)
            If oBank IsNot Nothing Then
                retval = oBank.Swift
            End If
        Else
            Dim exs As New List(Of Exception)
            retval = Iban.Swift(oIban.BankBranch, exs)
        End If
        Return retval
    End Function

    Shared Function Swift(oBranch As DTOBankBranch, exs As List(Of Exception)) As String
        Dim retval As String = ""
        If oBranch IsNot Nothing Then
            BankBranch.Load(oBranch, exs)

            retval = oBranch.swift
            If retval = "" Then
                If oBranch.Bank IsNot Nothing Then
                    Bank.Load(oBranch.Bank, exs)
                    retval = oBranch.Bank.Swift
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function DownloadUrl(oIban As DTOIban, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = UrlHelper.Dox(AbsoluteUrl, DTODocFile.Cods.IbanMandato, "guid", oIban.Guid.ToString())
        Return retval
    End Function

    Shared Async Function ToMultilineString(oIban As DTOIban, Optional ExcludeSwiftIfSpanish As Boolean = False, Optional HideFirstDigits As Boolean = False, Optional exs As List(Of Exception) = Nothing) As Task(Of String)
        Dim retval = Await Iban.ToString(oIban, ExcludeSwiftIfSpanish, HideFirstDigits, exs)
        Return retval
    End Function

    Shared Function ToHtml(oIban As DTOIban, Optional ExcludeSwiftIfSpanish As Boolean = False, Optional HideFirstDigits As Boolean = False) As String
        Dim src As String = Iban.ToMultilineString(oIban, ExcludeSwiftIfSpanish, HideFirstDigits).Result
        Dim retval As String = src.Replace(vbCrLf, "<br/>")
        Return retval
    End Function

    Shared Shadows Async Function ToString(oIban As DTOIban, Optional ExcludeSwiftIfSpanish As Boolean = False, Optional HideFirstDigits As Boolean = False, Optional exs As List(Of Exception) = Nothing) As Task(Of String)
        Dim sb As New Text.StringBuilder
        If HideFirstDigits Then
            sb.AppendLine(String.Format("IBAN: ...{0}", TextHelper.VbRight(DTOIban.Formated(oIban), 4)))
        Else
            sb.AppendLine(String.Format("IBAN: {0}", DTOIban.Formated(oIban)))
        End If

        If Not ExcludeSwiftIfSpanish Then
            Dim sSwift As String = Iban.Swift(oIban)
            If sSwift > "" Then sb.AppendLine(String.Format("Swift: {0}", sSwift))
        End If

        If oIban.Guid.Equals(System.Guid.Empty) And oIban.Digits > "" Then
            oIban.BankBranch = Await IbanStructure.GetBankBranch(oIban.Digits, exs)
        End If

        If oIban.BankBranch Is Nothing Then
            If exs IsNot Nothing Then
                exs.Add(New Exception("entitat bancària desconeguda"))
            End If
        Else
            If oIban.BankBranch.Bank Is Nothing Then
                If exs IsNot Nothing Then
                    exs.Add(New Exception("entitat bancària desconeguda"))
                End If
            Else
                sb.AppendLine(oIban.BankBranch.Bank.raoSocial)
            End If

            If oIban.BankBranch.address = "" Then
                If exs IsNot Nothing Then
                    exs.Add(New Exception("falta la adreça del banc"))
                End If
            Else
                sb.AppendLine(oIban.BankBranch.address)
            End If

            If oIban.BankBranch.location Is Nothing Then
                If exs IsNot Nothing Then
                    exs.Add(New Exception("falta la població del banc"))
                End If
            Else
                sb.AppendLine(oIban.BankBranch.location.FullNom(DTOLang.Factory("ESP")))
            End If
        End If
        Return sb.ToString
    End Function


    Shared Function ImgUrl(sIban As String, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Factory(AbsoluteUrl, "img", DTO.Defaults.ImgTypes.Iban, sIban)
    End Function

    Shared Async Function Img(exs As List(Of Exception), oIban As DTOIban, Optional oLang As DTOLang = Nothing) As Task(Of Byte())
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Return Await Api.FetchImage(exs, "Iban/img", oIban.Guid.ToString, oLang.Tag)
    End Function

    Shared Async Function Img(exs As List(Of Exception), sDigits As String, Optional oLang As DTOLang = Nothing) As Task(Of Byte())
        Dim retval As Byte()  = Nothing
        If sDigits > "" Then
            If oLang Is Nothing Then oLang = DTOLang.ESP
            retval = Await Api.FetchImage(exs, "Iban/img/fromCcc", sDigits, oLang.Tag)
        End If
        Return retval
    End Function


    Shared Function ValidateBankBranch(value As DTOBankBranch, exs As List(Of DTOIban.Exceptions)) As Boolean
        If value Is Nothing Then
            exs.Add(DTOIban.Exceptions.missingBankBranch)
        Else
            Dim oBranch As DTOBankBranch = value
            If oBranch.address = "" Then exs.Add(DTOIban.Exceptions.missingBranchAddress)
            If oBranch.location Is Nothing Then exs.Add(DTOIban.Exceptions.missingBranchLocation)
            If oBranch.Bank Is Nothing Then
                exs.Add(DTOIban.Exceptions.missingBankNom)
            Else
                If oBranch.Bank.nomComercial = "" And oBranch.Bank.raoSocial = "" Then exs.Add(DTOIban.Exceptions.missingBankNom)
                If oBranch.Bank.Swift = "" Then exs.Add(DTOIban.Exceptions.missingBIC)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Country(sDigits As String, exs As List(Of Exception)) As Task(Of DTOCountry)
        Dim retval As DTOCountry = Nothing
        Dim ISO As String = DTOIban.Structure.GetCountryISO(sDigits).ToUpper
        If Not String.IsNullOrEmpty(ISO) Then
            retval = Await FEB.Country.FromIso(ISO, exs)
        End If
        Return retval
    End Function

    Shared Async Function Country(oIban As DTOIban, exs As List(Of Exception)) As Task(Of DTOCountry)
        Dim retval As DTOCountry = Nothing
        If oIban IsNot Nothing Then
            If oIban.BankBranch IsNot Nothing Then
                If oIban.BankBranch.Bank IsNot Nothing Then
                    retval = oIban.BankBranch.Bank.Country
                End If
            End If
        End If
        If retval Is Nothing Then
            If oIban IsNot Nothing Then
                If Not String.IsNullOrEmpty(oIban.Digits) Then
                    retval = Await Iban.Country(oIban.Digits, exs)
                End If
            End If
        End If
        Return retval
    End Function


    Shared Async Function GetBankFromDigits(oIban As DTOIban, exs As List(Of Exception)) As Task(Of DTOBank)
        Dim retval As DTOBank = Nothing
        Dim oCountry = Await Iban.Country(oIban, exs)
        If oCountry IsNot Nothing Then
            Dim oBanks = Await Banks.All(oCountry, exs)
            Dim sId As String = Iban.BankId(oIban)
            retval = oBanks.Find(Function(x) x.Id = sId)
        End If
        Return retval
    End Function

    Shared Async Function GetBankBranchFromDigits(oIban As DTOIban, exs As List(Of Exception)) As Task(Of DTOBankBranch)
        Return Await IbanStructure.GetBankBranch(oIban.Digits, exs)
    End Function



    Shared Function BankId(oIban As DTOIban) As String
        Dim retval As String = ""
        If oIban IsNot Nothing Then
            If oIban.BankBranch IsNot Nothing Then
                retval = oIban.BankBranch.Bank.Id
            End If

            If retval = "" Then
                retval = IbanStructure.GetBankId(oIban.Digits)
            End If

        End If
        Return retval
    End Function


    Shared Async Function BankOrNew(sIbanDigits As String, exs As List(Of Exception)) As Task(Of DTOBank)
        Dim retval As DTOBank = Nothing
        Dim oCountry As DTOCountry = Await Iban.Country(sIbanDigits, exs)
        If oCountry IsNot Nothing Then
            Dim oStructure = Await IbanStructure.Find(oCountry, exs)
            Dim sId As String = DTOIban.Structure.BankDigits(oStructure, sIbanDigits)
            Dim oBanks As List(Of DTOBank) = Await Banks.All(oCountry, exs)
            retval = oBanks.Find(Function(x) x.id = sId)
            If retval Is Nothing Then
                retval = DTOBank.Factory(oCountry, sId)
            End If
        End If

        Return retval
    End Function

    Shared Async Function BranchOrNew(sDigits As String, Optional oTitularGuid As Guid = Nothing) As Task(Of DTOBankBranch)
        Dim exs As New List(Of Exception)
        Dim retval = Await IbanStructure.GetBankBranch(sDigits, exs)
        If retval Is Nothing Then
            Dim oBank = Await IbanStructure.GetBank(sDigits, exs)
            If oBank IsNot Nothing Then
                retval = DTOBankBranch.Factory(oBank)
                retval.Id = Await IbanStructure.GetBankBranchId(sDigits)
                If Await BankBranch.Update(retval, exs) Then
                    Dim oUser = DTOUser.Wellknown(DTOUser.Wellknowns.info)
                    Dim sCustomerNom As String = ""
                    If oTitularGuid <> Nothing Then
                        Dim oCustomer = Await Contact.Find(oTitularGuid, exs)
                        If oCustomer IsNot Nothing Then sCustomerNom = oCustomer.FullNom
                    End If
                    Await MailMessage.Send(oUser, DTOMailMessage.wellknownRecipients.Cuentas, Subject:="Completar dades bancàries per l'Iban " & sDigits & " " & sCustomerNom)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Async Function NewForDownload(sDigits As String, oTitularGuid As Guid, oUsrDownloaded As DTOUser) As Task(Of DTOIban)
        Dim retval As New DTOIban
        With retval
            .emp = oUsrDownloaded.emp
            .digits = sDigits
            .BankBranch = Await Iban.BranchOrNew(sDigits, oTitularGuid)
            .titular = New DTOContact(oTitularGuid)
            .fchFrom = DTO.GlobalVariables.Today()
            .format = DTOIban.Formats.SEPACore
            .FchDownloaded = DTO.GlobalVariables.Now()
            .usrDownloaded = oUsrDownloaded
            .status = DTOIban.StatusEnum.pendingUpload
            .cod = DTOIban.Cods.client
        End With
        Return retval
    End Function


    Shared Async Function Upload(oIban As DTOIban, oDocFile As DTODocFile, oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        Iban.Load(oIban, exs)
        With oIban
            .docFile = oDocFile
            .FchUploaded = DTO.GlobalVariables.Now()
            .usrUploaded = oUser
            .fchApproved = Nothing
            .usrApproved = Nothing
        End With
        Dim retval = Await Iban.Update(oIban, exs)
        Return retval
    End Function

    Shared Async Function UploadAndApprove(oIban As DTOIban, oDocFile As DTODocFile, oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        Iban.Load(oIban, exs)
        With oIban
            .docFile = oDocFile
            .FchUploaded = DTO.GlobalVariables.Now()
            .usrUploaded = oUser
            .fchApproved = .fchUploaded
            .usrApproved = .usrUploaded
        End With
        Dim retval = Await Iban.Update(oIban, exs)
        Return retval
    End Function

    Shared Function CheckIfOthersMatchingTimeSpan(exs As List(Of Exception), oIban As DTOIban, ByRef sWarningMessage As String) As Boolean
        Dim retval As Boolean
        Dim FchFrom As Date = oIban.fchFrom
        Dim FchTo As Date = If(oIban.fchTo = Nothing, Date.MaxValue, oIban.fchTo)
        Dim sb As New System.Text.StringBuilder
        Dim oIbans = Ibans.FromContactSync(exs, oIban.titular)
        For Each pIban As DTOIban In oIbans
            If Not pIban.Guid.Equals(oIban.Guid) Then
                Dim FchFrom2 As Date = pIban.fchFrom
                Dim FchTo2 As Date = If(pIban.fchTo = Nothing, Date.MaxValue, pIban.fchTo)
                If FchTo < FchFrom2 Then
                    'caduca abans que comenci
                ElseIf FchFrom > FchTo2 Then
                    'comença despres que hagi caducat
                Else
                    sb.Append("coincideix en el temps amb " & DTOIban.Formated(pIban))
                    sb.Append(" (")
                    If pIban.fchFrom <> Nothing Then
                        sb.Append(TextHelper.VbFormat(FchFrom2, "dd/MM/yy"))
                    End If
                    sb.Append("-")
                    If pIban.fchTo <> Nothing Then
                        sb.Append(" -" & TextHelper.VbFormat(FchTo2, "dd/MM/yy"))
                    End If
                    sb.AppendLine(")")
                    retval = True
                End If
            End If
        Next
        sWarningMessage = sb.ToString
        Return retval
    End Function
End Class

Public Class Ibans

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oStatus As DTOIban.StatusEnum) As Task(Of List(Of DTOIban))
        Return Await Api.Fetch(Of List(Of DTOIban))(exs, "Ibans", oEmp.Id, oStatus)
    End Function

    Shared Async Function Clients(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOIban))
        Return Await Api.Fetch(Of List(Of DTOIban))(exs, "Ibans/Clients", oEmp.Id)
    End Function

    Shared Async Function FromBank(exs As List(Of Exception), oEmp As DTOEmp, oBank As DTOBank, Optional OnlyVigent As Boolean = False) As Task(Of List(Of DTOIban))
        Return Await Api.Fetch(Of List(Of DTOIban))(exs, "Ibans/FromBank", oEmp.Id, oBank.Guid.ToString, If(OnlyVigent, 1, 0))
    End Function

    Shared Async Function FromBankBranch(exs As List(Of Exception), oEmp As DTOEmp, oBranch As DTOBankBranch, Optional OnlyVigent As Boolean = False) As Task(Of List(Of DTOIban))
        Return Await Api.Fetch(Of List(Of DTOIban))(exs, "Ibans/FromBankBranch", oEmp.Id, oBranch.Guid.ToString, If(OnlyVigent, 1, 0))
    End Function

    Shared Async Function FromContact(exs As List(Of Exception), oContact As DTOContact, Optional OnlyVigent As Boolean = False, Optional oCod As DTOIban.Cods = DTOIban.Cods._NotSet) As Task(Of List(Of DTOIban))
        Return Await Api.Fetch(Of List(Of DTOIban))(exs, "Ibans/FromContact", oContact.Guid.ToString, If(OnlyVigent, 1, 0), oCod)
    End Function

    Shared Function FromContactSync(exs As List(Of Exception), oContact As DTOContact, Optional OnlyVigent As Boolean = False, Optional oCod As DTOIban.Cods = DTOIban.Cods._NotSet) As List(Of DTOIban)
        Return Api.FetchSync(Of List(Of DTOIban))(exs, "Ibans/FromContact", oContact.Guid.ToString, If(OnlyVigent, 1, 0), oCod)
    End Function

    Shared Async Function PendingUploads(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOIban))
        Return Await Api.Fetch(Of List(Of DTOIban))(exs, "Ibans/PendingUploads", oUser.Guid.ToString())
    End Function

    Shared Function PendingUploadsSync(exs As List(Of Exception), oUser As DTOUser) As List(Of DTOIban)
        Return Api.FetchSync(Of List(Of DTOIban))(exs, "Ibans/PendingUploads", oUser.Guid.ToString())
    End Function
End Class
