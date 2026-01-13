Public Class DTOIban
    Inherits DTOBaseGuid
    Property emp As DTOEmp
    Property cod As Cods
    Property bankBranch As DTOBankBranch
    Property digits As String
    Property fchFrom As Date
    Property fchTo As Date
    Property titular As DTOContact
    Property personNom As String
    Property personDni As String
    Property format As Formats
    Property docFile As DTODocFile
    Property status As StatusEnum
    Property fchDownloaded As Date
    Property usrDownloaded As DTOUser
    Property fchUploaded As Date
    Property usrUploaded As DTOUser
    Property fchApproved As Date
    Property usrApproved As DTOUser

    Property ibanStructure As DTOIbanStructure

    Public Enum Cods
        _NotSet
        proveidor
        client
        staff
        banc
    End Enum

    Public Enum Formats
        notSet
        noValid
        SEPAB2B
        SEPACore
        Q58
    End Enum

    Public Enum StatusEnum
        all
        pendingDownload
        pendingUpload
        pendingApproval
        downloaded
        uploaded
        approved
        denied
    End Enum

    Public Enum Exceptions
        success
        missingBankBranch
        missingBankNom
        missingBIC
        missingBranchAddress
        missingBranchLocation
        wrongDigits
        missingMandateFch
        missingMandateDocument
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(sDigits As String) As DTOIban
        Dim retval As New DTOIban
        With retval
            .Digits = DTOIban.CleanCcc(sDigits)
        End With
        Return retval
    End Function

    Shared Function Factory(oEmp As DTOEmp, oTitular As DTOContact, oCod As DTOIban.Cods) As DTOIban
        Dim retval As New DTOIban
        With retval
            .Titular = oTitular
            .FchFrom = Today
            .Format = DTOIban.Formats.SEPACore
            .Cod = oCod
        End With
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


    Public Function IsActive() As Boolean
        Dim retval As Boolean = _FchFrom <= Today
        If _FchTo > Nothing Then
            If _FchTo < Today Then retval = False
        End If
        Return retval
    End Function

    Public Function IsSepa() As Boolean
        Dim retval As Boolean
        If _BankBranch IsNot Nothing Then
            If _BankBranch.Bank IsNot Nothing Then
                retval = _BankBranch.Bank.IsSepa
            End If
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

    Shared Function Bank(oIban As DTOIban) As DTOBank
        Dim retval As DTOBank = Nothing
        If oIban IsNot Nothing Then
            If oIban.BankBranch IsNot Nothing Then
                retval = oIban.BankBranch.Bank
            End If
        End If
        Return retval
    End Function


    Shared Function BankNom(oIban As DTOIban) As String
        Dim retval As String = ""
        Dim oBank As DTOBank = DTOIban.Bank(oIban)
        If oBank IsNot Nothing Then
            retval = DTOBank.NomComercialORaoSocial(oBank)
        End If
        Return retval
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
            retval = DTOIban.Swift(oIban.BankBranch, exs)
        End If
        Return retval
    End Function

    Shared Function Swift(oBranch As DTOBankBranch, exs As List(Of Exception)) As String
        Dim retval As String = ""
        If oBranch IsNot Nothing Then

            retval = oBranch.Swift
            If retval = "" Then
                If oBranch.Bank IsNot Nothing Then
                    retval = oBranch.Bank.Swift
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function Formated(oIban As DTOIban) As String
        Dim retval As String = ""
        If oIban IsNot Nothing Then retval = Formated(oIban.Digits)
        Return retval
    End Function

    Shared Function Formated(src As String) As String
        Dim cleanString As String = DTOIban.CleanCcc(src)
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

    Shared Function CleanCcc(src As String) As String
        Dim retval As String = TextHelper.RegexSuppress(src, "[^A-Za-z0-9]").ToUpper
        Return retval
    End Function

    Shared Function SegmentedDigits(oIban As DTOIban) As List(Of String)
        'per omplir les caselles de 4 digits de la web
        Dim retval As New List(Of String)
        If oIban IsNot Nothing Then
            Dim src As String = oIban.Digits
            Dim segmentLength As Integer = 4
            retval = Enumerable.Range(0, src.Length / segmentLength).Select(Function(x) src.Substring(x * segmentLength, segmentLength)).ToList
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
End Class



