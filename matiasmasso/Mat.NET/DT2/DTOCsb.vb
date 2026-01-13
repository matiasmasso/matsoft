Public Class DTOCsb
    Inherits DTOBaseGuid

    Property csa As DTOCsa
    Property id As Integer
    Property contact As DTOContact
    Property txt As String
    Property amt As DTOAmt
    Property vto As Date
    Property iban As DTOIban
    Property fraNum As String
    Property fraYea As Integer

    'Property Reclamat As Boolean
    'Property Impagat As Boolean

    Property result As Results
    Property resultCca As DTOCca

    Property invoice As DTOInvoice
    Property pnd As DTOPnd
    Property sepaTipoAdeudo As TiposAdeudo

    Property exceptionCode As ExceptionCodes

    Property despesa As DTOAmt

    Property mailingLogs As List(Of DTOMailingLog)

    Public Enum TiposAdeudo
        NotSet
        FRST
        RCUR
        FNAL
        OOFF
    End Enum

    Public Enum ExceptionCodes
        Success
        NoValue
        Negative
        NoIban
        NoBn2
        NoBn1
        WrongBIC
        NoMandate
        NoMandateFch
    End Enum

    Public Enum Results
        Pendent
        Vençut
        Reclamat
        Impagat
    End Enum

    Public Sub New()
        MyBase.New()
        _MailingLogs = New List(Of DTOMailingLog)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _MailingLogs = New List(Of DTOMailingLog)
    End Sub

    Public Function FormattedId() As String
        Dim retval As String = String.Format("{0}{1:000}", _Csa.formattedId, _Id)
        Return retval
    End Function

    Public Function ReadableFormat() As String
        Dim retval As String = String.Format("{0}.{1}", _Csa.ReadableFormat, _Id)
        Return retval
    End Function

    Shared Function Country(oCsb As DTOCsb) As DTOCountry
        Dim retval As DTOCountry = Nothing
        If oCsb IsNot Nothing Then
            If oCsb.Iban IsNot Nothing Then
                If oCsb.Iban.BankBranch IsNot Nothing Then
                    If oCsb.Iban.BankBranch.Bank IsNot Nothing Then
                        retval = oCsb.Iban.BankBranch.Bank.Country
                    End If
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function Validate(oCsbs As List(Of DTOCsb), oFormat As DTOCsa.FileFormats) As Boolean
        Dim retval As Boolean = True
        For Each item As DTOCsb In oCsbs
            If Not DTOCsb.Validate(item, oFormat) Then retval = False
        Next
        Return retval
    End Function

    Shared Function Validate(ByRef oCsb As DTOCsb, oFormat As DTOCsa.FileFormats) As Boolean
        'If oCsb.Iban.Digits.StartsWith("AD55") Then Stop
        With oCsb
            .ExceptionCode = DTOCsb.ExceptionCodes.Success
            If .Amt.Eur = 0 Then
                .ExceptionCode = DTOCsb.ExceptionCodes.NoValue
            ElseIf .Amt.Eur < 0 Then
                .ExceptionCode = DTOCsb.ExceptionCodes.Negative
            ElseIf .Iban Is Nothing Then
                .ExceptionCode = DTOCsb.ExceptionCodes.NoIban
            ElseIf .Iban.BankBranch Is Nothing Then
                .ExceptionCode = DTOCsb.ExceptionCodes.NoBn1
            ElseIf .Iban.BankBranch.Bank Is Nothing Then
                .ExceptionCode = DTOCsb.ExceptionCodes.NoBn2
            ElseIf Not DTOBank.ValidateBIC(.Iban.BankBranch.Bank.Swift) Then
                .ExceptionCode = DTOCsb.ExceptionCodes.WrongBIC
            ElseIf oFormat <> DTOCsa.FileFormats.NormaAndorrana And oFormat <> DTOCsa.FileFormats.RemesesExportacioLaCaixa And .Iban.FchFrom = Nothing Then
                .ExceptionCode = DTOCsb.ExceptionCodes.NoMandateFch
            ElseIf oFormat <> DTOCsa.FileFormats.NormaAndorrana And oFormat <> DTOCsa.FileFormats.RemesesExportacioLaCaixa And .Iban.DocFile Is Nothing Then
                .ExceptionCode = DTOCsb.ExceptionCodes.NoMandate
            End If
        End With
        Dim retval As Boolean = oCsb.ExceptionCode = DTOCsb.ExceptionCodes.Success
        Return retval
    End Function

    Shared Function ValidationText(oCode As DTOCsb.ExceptionCodes) As String
        Dim retval As String = ""
        Select Case oCode
            Case DTOCsb.ExceptionCodes.NoValue
                retval = "sense import"
            Case DTOCsb.ExceptionCodes.Negative
                retval = "import negatiu"
            Case DTOCsb.ExceptionCodes.NoIban
                retval = "sense Iban vigent"
            Case DTOCsb.ExceptionCodes.NoBn1
                retval = "entitat bancaria no registrada"
            Case DTOCsb.ExceptionCodes.NoBn2
                retval = "oficina bancaria no registrada"
            Case DTOCsb.ExceptionCodes.WrongBIC
                retval = "BIC incorrecte a entitat bancaria"
            Case DTOCsb.ExceptionCodes.NoMandateFch
                retval = "mandat sense data"
            Case DTOCsb.ExceptionCodes.NoMandate
                retval = "sense document de mandat"
        End Select
        Return retval
    End Function


    Shared Function TotalNominal(oCsbs As List(Of DTOCsb)) As DTOAmt
        Dim DcEur As Decimal = oCsbs.Sum(Function(x) x.Amt.Eur)
        Dim retval As DTOAmt = DTOAmt.factory(DcEur)
        Return retval
    End Function

    Shared Function TotalDespeses(oCsbs As List(Of DTOCsb), oTerm As DTOBancTerm) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each oCsb As DTOCsb In oCsbs
            retval.Add(DTOBancTerm.Cost(oCsb, oTerm))
        Next
        Return retval
    End Function

End Class
