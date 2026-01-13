Public Class Iban


    Shared Function Find(oGuid As Guid) As DTOIban
        Dim retval As DTOIban = IbanLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromCcc(ccc As String) As DTOIban
        Dim retval As DTOIban = IbanLoader.FromCcc(ccc)
        If retval IsNot Nothing Then
            IbanLoader.Load(retval)
        End If
        Return retval
    End Function

    Shared Function FromDigits(sDigits As String) As DTOIban
        Dim retval As New DTOIban
        retval.Digits = sDigits
        Return retval
    End Function


    Shared Function NewIban(oTitular As DTOContact) As DTOIban
        Dim retval As New DTOIban
        With retval
            .Titular = oTitular
            .FchFrom = DTO.GlobalVariables.Today()
            .Format = DTOIban.Formats.SEPACore
        End With
        Return retval
    End Function

    Shared Function NewForDownload(sDigits As String, oTitularGuid As Guid, oUsrDownloaded As DTOUser) As DTOIban
        Dim retval As New DTOIban
        With retval
            .Digits = sDigits
            .BankBranch = BranchOrNew(sDigits)
            .Titular = New DTOContact(oTitularGuid)
            .FchFrom = DTO.GlobalVariables.Today()
            .Format = DTOIban.Formats.SEPACore
            .FchDownloaded = DTO.GlobalVariables.Now()
            .UsrDownloaded = oUsrDownloaded
            .Status = DTOIban.StatusEnum.PendingUpload
            .Cod = DTOIban.Cods.Client
        End With
        Return retval
    End Function

    Shared Function Load(oIban As DTOIban) As Boolean
        Dim retval As Boolean = IbanLoader.Load(oIban)
        Return retval
    End Function

    Shared Function Update(oIban As DTOIban, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IbanLoader.Update(oIban, exs)
        Return retval
    End Function

    Shared Function Delete(oIban As DTOIban, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IbanLoader.Delete(oIban, exs)
        Return retval
    End Function

    Shared Function DownloadUrl(oIban As DTOIban, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = BEBL.UrlHelper.Dox(AbsoluteUrl, DTODocFile.Cods.IbanMandato, "guid", oIban.Guid.ToString())
        Return retval
    End Function

    Shared Function AccountNumber(oIban As DTOIban) As String
        Dim src As String = oIban.Digits
        Dim sResult As String = ""
        If src.Length > 2 Then
            Dim oCountry As DTOCountry = CountryLoader.Find(src.Substring(0, 2))
            Dim oStructure As New DTOIban.Structure
            oStructure.Country = oCountry
            Dim iPos As Integer = oStructure.AccountPosition
            Dim iLen As Integer = oStructure.AccountLength
            Select Case src.Length
                Case Is >= iPos + iLen
                    sResult = src.Substring(iPos, iLen)
                Case Is > iPos
                    sResult = src.Substring(iPos)
            End Select
            If oCountry.ISO = "AD" Then
                'carregat els zeros a la esquerra
                Dim i As Integer
                For i = 0 To sResult.Length
                    If sResult.Substring(i, 1) <> "0" Then Exit For
                Next
                sResult = sResult.Substring(i).PadRight(iLen)
            End If
        End If
        Return sResult
    End Function

    Shared Function Swift(oBranch As DTOBankBranch) As String
        Dim retval As String = ""
        If oBranch IsNot Nothing Then
            BankBranchLoader.Load(oBranch)

            retval = oBranch.Swift
            If retval = "" Then
                If oBranch.Bank IsNot Nothing Then
                    BankLoader.Load(oBranch.Bank)
                    retval = oBranch.Bank.Swift
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function Swift(oIban As DTOIban) As String
        Dim retval As String = ""
        If oIban.BankBranch Is Nothing Then
            Dim oBank As DTOBank = Bank(oIban)
            If oBank IsNot Nothing Then
                retval = oBank.Swift
            End If
        Else
            retval = Swift(oIban.BankBranch)
        End If
        Return retval
    End Function

    Shared Function DisplayNom(oIban As DTOIban) As String
        Dim retval As String = DTOBank.NomComercialORaoSocial(oIban.BankBranch.Bank)
        Return retval
    End Function

    Shared Function Formated(oIban As DTOIban) As String
        Dim retval As String = ""
        If oIban IsNot Nothing Then retval = Formated(oIban.Digits)
        Return retval
    End Function

    Shared Function Formated(src As String) As String
        Dim cleanString As String = DTOIban.Structure.CleanCcc(src)
        Dim sb As New System.Text.StringBuilder
        Dim iPos As Integer
        Do While iPos < cleanString.Length '-1
            Dim tmp As String = cleanString.Substring(iPos)
            If sb.Length > 0 Then sb.Append(".")
            sb.Append(Left(tmp, 4))
            iPos += 4
        Loop
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function BranchOrNew(sDigits As String) As DTOBankBranch
        Dim retval As DTOBankBranch = IbanStructure.GetBankBranch(sDigits)
        If retval Is Nothing Then
            Dim oBank As DTOBank = IbanStructure.GetBank(sDigits)
            If oBank IsNot Nothing Then
                retval = New DTOBankBranch
                retval.Bank = oBank
                retval.Id = IbanStructure.GetBankBranchId(sDigits)
            End If
        End If
        Return retval
    End Function

    Shared Function BranchLocationAndAdr(oIban As DTOIban) As String
        Dim sb As New System.Text.StringBuilder
        If oIban.BankBranch IsNot Nothing Then
            If oIban.BankBranch.Location IsNot Nothing Then
                sb.Append(oIban.BankBranch.Location.Nom)
                sb.Append(" - ")
                sb.Append(oIban.BankBranch.Address)
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Shadows Function ToString(oIban As DTOIban, Optional ExcludeSwiftIfSpanish As Boolean = False, Optional HideFirstDigits As Boolean = False, Optional exs As List(Of Exception) = Nothing) As String
        Dim s As String = ""
        If HideFirstDigits Then
            s = s & "IBAN: ..." & Right(Formated(oIban), 4) & vbCrLf
        Else
            s = s & "IBAN: " & Formated(oIban) & vbCrLf
        End If

        If Not ExcludeSwiftIfSpanish Then
            Dim sSwift As String = Swift(oIban)
            If sSwift > "" Then s = s & "Swift: " & sSwift & vbCrLf
        End If

        If oIban.Guid.Equals(System.Guid.Empty) And oIban.Digits > "" Then
            oIban.BankBranch = IbanStructure.GetBankBranch(oIban.Digits)
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
                s = s & oIban.BankBranch.Bank.RaoSocial & vbCrLf
            End If

            If oIban.BankBranch.Address = "" Then
                If exs IsNot Nothing Then
                    exs.Add(New Exception("falta la adreça del banc"))
                End If
            Else
                s = s & oIban.BankBranch.Address
                If oIban.BankBranch.Location IsNot Nothing Then s = s & " - "
            End If

            If oIban.BankBranch.Location Is Nothing Then
                If exs IsNot Nothing Then
                    exs.Add(New Exception("falta la població del banc"))
                End If
            Else
                s = s & oIban.BankBranch.Location.FullNom(DTOLang.Factory("ESP"))
            End If
        End If
        Return s
    End Function

    Shared Function ToMultilineString(oIban As DTOIban, Optional ExcludeSwiftIfSpanish As Boolean = False, Optional HideFirstDigits As Boolean = False, Optional exs As List(Of Exception) = Nothing) As String
        Dim retval As String = ToString(oIban, ExcludeSwiftIfSpanish, HideFirstDigits, exs)
        Return retval
    End Function

    Shared Function ToHtml(oIban As DTOIban, Optional ExcludeSwiftIfSpanish As Boolean = False, Optional HideFirstDigits As Boolean = False) As String
        Dim src As String = ToMultilineString(oIban, ExcludeSwiftIfSpanish, HideFirstDigits)
        Dim retval As String = src.Replace(vbCrLf, "<br/>")
        Return retval
    End Function

    Shared Function FromContact(oContact As DTOContact, oCod As DTOIban.Cods) As DTOIban
        Dim retval As DTOIban = Nothing
        Dim oIbans As List(Of DTOIban) = IbansLoader.All(oContact.Emp, oContact, -1, True, DTOIban.StatusEnum.All, oCod)
        If oIbans.Count > 0 Then
            retval = oIbans(0)
            BankBranchLoader.Load(retval.BankBranch)
        End If
        Return retval
    End Function

    Shared Function IsActive(oIban As DTOIban) As Boolean
        Dim retval As Boolean = oIban.FchFrom <= DTO.GlobalVariables.Today()
        If oIban.FchTo > Nothing Then
            If oIban.FchTo < DTO.GlobalVariables.Today() Then retval = False
        End If
        Return retval
    End Function

    Shared Function IsMissingMandato(oIban As DTOIban) As Boolean
        Dim retval As Boolean = True
        If oIban IsNot Nothing Then
            If oIban.DocFile IsNot Nothing Then
                retval = False
            End If
        End If
        Return retval
    End Function

    Shared Function CheckIfOthersMatchingTimeSpan(oIban As DTOIban, ByRef sWarningMessage As String) As Boolean
        Dim retval As Boolean
        Dim FchFrom As Date = oIban.FchFrom
        Dim FchTo As Date = IIf(oIban.FchTo = Nothing, Date.MaxValue, oIban.FchTo)

        Dim sb As New System.Text.StringBuilder
        Dim oIbans As List(Of DTOIban) = IbansLoader.All(oIban.Emp, oIban.Titular)

        For Each pIban As DTOIban In oIbans
            If Not pIban.Guid.Equals(oIban.Guid) Then
                Dim FchFrom2 As Date = pIban.FchFrom
                Dim FchTo2 As Date = IIf(pIban.FchTo = Nothing, Date.MaxValue, pIban.FchTo)
                If FchTo < FchFrom2 Then
                    'caduca abans que comenci
                ElseIf FchFrom > FchTo2 Then
                    'comença despres que hagi caducat
                Else
                    sb.Append("coincideix en el temps amb " & Formated(pIban))
                    sb.Append(" (")
                    If pIban.FchFrom <> Nothing Then
                        sb.Append(Format(FchFrom2, "dd/MM/yy"))
                    End If
                    sb.Append("-")
                    If pIban.FchTo <> Nothing Then
                        sb.Append(" -" & Format(FchTo2, "dd/MM/yy"))
                    End If
                    sb.AppendLine(")")
                    retval = True
                End If
            End If
        Next
        sWarningMessage = sb.ToString
        Return retval
    End Function

    Shared Function Is_SEPAB2B_Enabled(oIban As DTOIban) As Boolean
        Dim retval As Boolean
        Dim oBank As DTOBank = Bank(oIban)
        If oBank.SEPAB2B Then
            If oBank.Swift = "" Then
            ElseIf oIban.DocFile Is Nothing Then
            ElseIf oIban.Format <> DTOIban.Formats.SEPAB2B Then
            ElseIf Not IsActive(oIban) Then
            Else
                retval = True
            End If
        End If
        Return retval
    End Function

    Shared Function Is_SEPAB2B_Enabled(sDigits As String) As Boolean
        Dim retval As Boolean
        Dim oIban As New DTOIban
        oIban.Digits = sDigits
        retval = Is_SEPAB2B_Enabled(oIban)
        Return retval
    End Function

    Shared Function ImgUrl(sIban As String, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Image(DTO.Defaults.ImgTypes.Iban, sIban, AbsoluteUrl)
        Return retval
    End Function




    Shared Function Validated(src As String) As Boolean
        Dim retval As Boolean = IbanStructure.Validate(src)
        Return retval
    End Function

    Shared Function Upload(oIban As DTOIban, oDocFile As DTODocFile, oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Load(oIban)
        With oIban
            .DocFile = oDocFile
            .FchUploaded = DTO.GlobalVariables.Now()
            .UsrUploaded = oUser
            .FchApproved = Nothing
            .UsrApproved = Nothing
        End With
        Dim retval As Boolean = Update(oIban, exs)
        Return retval
    End Function

    Shared Function UploadAndApprove(oIban As DTOIban, oDocFile As DTODocFile, oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Load(oIban)
        With oIban
            .DocFile = oDocFile
            .FchUploaded = DTO.GlobalVariables.Now()
            .UsrUploaded = oUser
            .FchApproved = .FchUploaded
            .UsrApproved = .UsrUploaded
        End With
        Dim retval As Boolean = Update(oIban, exs)
        Return retval
    End Function

    Shared Function Country(oIban As DTOIban) As DTOCountry
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
                    retval = Country(oIban.Digits)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function Country(sDigits As String) As DTOCountry
        Dim ISO As String = IbanStructure.GetCountryISO(sDigits).ToUpper
        Dim retval As DTOCountry = CountryLoader.Find(ISO)
        Return retval
    End Function

    Shared Function BankOrNew(sIbanDigits As String) As DTOBank
        Dim retval As DTOBank = Nothing
        Dim oCountry As DTOCountry = Country(sIbanDigits)
        If oCountry IsNot Nothing Then
            Dim oStructure As DTOIban.Structure = IbanStructure.Find(oCountry)
            Dim sId As String = IbanStructure.BankDigits(oStructure, sIbanDigits)
            Dim oBanks As List(Of DTOBank) = BanksLoader.FromCountry(oCountry)
            retval = oBanks.Find(Function(x) x.Id = sId)

            If retval Is Nothing Then
                retval = New DTOBank
                retval.Country = oCountry
                retval.Id = sId
            End If
        End If

        Return retval
    End Function

    Shared Function Bank(oIban As DTOIban) As DTOBank
        Dim retval As DTOBank = Nothing
        If oIban IsNot Nothing Then
            If oIban.BankBranch IsNot Nothing Then
                retval = oIban.BankBranch.Bank
            End If
        End If
        Return retval
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

    Shared Function BankNom(oIban As DTOIban) As String
        Dim retval As String = ""
        Dim oBank As DTOBank = Bank(oIban)
        If oBank IsNot Nothing Then
            retval = DTOBank.NomComercialORaoSocial(oBank)
        End If
        Return retval
    End Function

    Shared Function GetBankBranchFromDigits(oIban As DTOIban) As DTOBankBranch
        Dim retval As DTOBankBranch = IbanStructure.GetBankBranch(oIban.Digits)
        Return retval
    End Function

    Shared Function GetBankFromDigits(oIban As DTOIban) As DTOBank
        Dim retval As DTOBank = Nothing
        Dim oCountry As DTOCountry = Country(oIban)
        If oCountry IsNot Nothing Then
            Dim oBanks As List(Of DTOBank) = BanksLoader.FromCountry(oCountry)
            Dim sId As String = BankId(oIban)
            retval = oBanks.Find(Function(x) x.Id = sId)
        End If
        Return retval
    End Function

    Shared Function BranchId(oIban As DTOIban) As String
        Dim retval As String = ""
        If oIban IsNot Nothing Then
            If oIban.BankBranch IsNot Nothing Then
                retval = oIban.BankBranch.Id
            End If
        End If
        Return retval
    End Function

    Shared Function Img(oIban As DTOIban, oLang As DTOLang) As Byte()
        Dim retval As Byte() = Nothing
        If oIban IsNot Nothing Then
            retval = Img(oIban.Digits, oLang)
        End If
        Load(oIban)
        Return retval
    End Function

    Shared Function Img(sDigits As String, oLang As DTOLang) As Byte()
        Dim oCountry = Country(sDigits)
        Dim oBranch As DTOBankBranch = IbanStructure.GetBankBranch(sDigits)
        Dim oBank As DTOBank = Nothing
        If oBranch Is Nothing Then
            oBank = IbanStructure.GetBank(sDigits)
        Else
            oBank = oBranch.Bank
        End If

        Dim retval = LegacyHelper.IbanHelper.Img(sDigits, oLang, DTOIban.ValidateDigits(sDigits), oCountry, oBank, oBranch)
        Return retval
    End Function

End Class

Public Class Ibans
    Shared Function Valids(oEmp As DTOEmp, Optional DtFch As Date = Nothing) As List(Of DTOIban)
        Dim retval As List(Of DTOIban) = IbansLoader.Valids(oEmp, DtFch)
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, oStatus As DTOIban.StatusEnum) As List(Of DTOIban)
        Dim retval As List(Of DTOIban) = IbansLoader.All(oEmp,, , , oStatus)
        Return retval
    End Function

    Shared Function Clients(oEmp As DTOEmp) As List(Of DTOIban)
        Dim retval As List(Of DTOIban) = IbansLoader.Clients(oEmp)
        Return retval
    End Function

    Shared Function FromContact(oContact As DTOContact, OnlyVigent As Boolean, oCod As DTOIban.Cods, DtFch As Date) As List(Of DTOIban)
        Dim retval As List(Of DTOIban) = IbansLoader.All(oContact.Emp, oContact, -1, OnlyVigent, DTOIban.StatusEnum.All, oCod, DtFch)
        Return retval
    End Function

    Shared Function FromBank(oEmp As DTOEmp, oBank As DTOBank, Optional OnlyVigent As Boolean = False) As List(Of DTOIban)
        Dim retval As List(Of DTOIban) = IbansLoader.FromBank(oEmp, oBank, OnlyVigent)
        Return retval
    End Function

    Shared Function FromBankBranch(oEmp As DTOEmp, oBranch As DTOBankBranch, Optional OnlyVigent As Boolean = False) As List(Of DTOIban)
        Dim retval As List(Of DTOIban) = IbansLoader.FromBankBranch(oEmp, oBranch, OnlyVigent)
        Return retval
    End Function

    Shared Function PendingUploads(oUser As DTOUser) As List(Of DTOIban)
        Dim retval As List(Of DTOIban) = IbansLoader.PendingUploads(oUser)
        Return retval
    End Function
End Class

