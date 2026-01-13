Public Class DTOPgcCta
    Inherits DTOPgcEpgBase



    Property id As String
    Property plan As DTOPgcPlan
    Property dsc As String
    Property act As Acts
    Property isBaseImponibleIva As Boolean
    Property isQuotaIva As Boolean
    Property isQuotaIrpf As Boolean
    Property nextCta As DTOPgcCta
    'Property Epg As DTOPgcEpgBase 'TO DEPRECATE
    Property pgcClass As DTOPgcClass
    Property yearMonths As List(Of DTOYearMonth)


    Property codi As DTOPgcPlan.Ctas

    Public Enum Acts
        NotSet
        Deutora
        Creditora
    End Enum

    Public Enum Bals
        NotSet
        Balance
        Explotacion
    End Enum

    Public Enum Digits
        Digits3
        Digits4
        Digits5
        Full
    End Enum


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function FullNom(oCta As DTOPgcCta, oLang As DTOLang) As String
        Dim retval As String = ""
        If oCta IsNot Nothing Then
            retval = String.Format("{0} {1}", oCta.Id, oCta.Nom.tradueix(oLang))
        End If
        Return retval
    End Function

    Shared Function FromCodi(oCtas As List(Of DTOPgcCta), oCodi As DTOPgcPlan.Ctas, exs As List(Of Exception)) As DTOPgcCta 'TO DEPRECATE
        Dim retval = oCtas.FirstOrDefault(Function(x) x.Codi = oCodi)
        If retval Is Nothing Then exs.Add(New Exception(String.Format("No s'ha trobat cap compte amb el codi {0}", oCodi.ToString())))
        Return retval
    End Function

    Shared Function FromCodi(oCtas As List(Of DTOPgcCta), oCodi As DTOPgcPlan.Ctas) As DTOPgcCta
        Return oCtas.FirstOrDefault(Function(x) x.Codi = oCodi)
    End Function

    Public Function YearMonthValue(oYearMonth As DTOYearMonth) As Decimal
        Dim retval As Decimal
        If oYearMonth IsNot Nothing Then
            If _yearMonths IsNot Nothing Then
                Dim pYearMonth = _yearMonths.FirstOrDefault(Function(x) x.Equals(oYearMonth))
                If pYearMonth IsNot Nothing Then
                    retval = pYearMonth.Eur
                End If
            End If
        End If
        Return retval
    End Function

    'Shared Function Nom(oCta As DTOPgcCta, oLang As DTOLang) As String
    'Dim retval As String = ""
    'If oCta IsNot Nothing Then
    '       retval = oLang.tradueix(oCta.NomEsp, oCta.NomCat, oCta.NomEng)
    'End If
    'Return retval
    'End Function

    Shared Function FormatAccountId(oCta As DTOPgcCta, oContact As DTOContact) As String
        Dim ContactId As Integer
        If oContact IsNot Nothing Then ContactId = oContact.id
        Dim retval = FormatAccountId(oCta.id, ContactId)
        Return retval
    End Function

    Shared Function FormatAccountId(sCtaId As String, oContactId As Integer) As String
        Dim retval As String = String.Format("{0}{1:00000}", sCtaId, oContactId)
        Return retval
    End Function

    Shared Function FormatAccountDsc(oCta As DTOPgcCta, oContact As DTOContact, oLang As DTOLang) As String
        Dim sCtaNom = oCta.nom.tradueix(oLang)
        Dim sContactNom As String = ""
        If oContact IsNot Nothing Then
            If oContact.nom = "" Then
                sContactNom = oContact.FullNom
            Else
                sContactNom = oContact.nom
            End If
        End If
        Dim retval As String = FormatAccountDsc(sCtaNom, sContactNom)
        Return retval
    End Function

    Shared Function FormatAccountDsc(sCtaNom As String, sContactNom As String) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(sCtaNom)
        sb.Append(" " & sContactNom)
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function Bal(value As DTOPgcCta) As DTOPgcCta.Bals
        Dim retval As DTOPgcCta.Bals = DTOPgcCta.Bals.NotSet
        If value.Id > "6" Then
            retval = DTOPgcCta.Bals.Explotacion
        Else
            retval = DTOPgcCta.Bals.Balance
        End If
        Return retval
    End Function

    Shared Function Saldo(DcSaldoAnterior As Decimal, oCcb As DTOCcb) As Decimal
        Dim retval As Decimal = DcSaldoAnterior
        If oCcb.Cta.Act = oCcb.Dh Then
            retval += oCcb.Amt.Eur
        Else
            retval -= oCcb.Amt.Eur
        End If
        Return retval
    End Function

    Shared Function isBaseIrpf(ByRef oCta As DTOPgcCta) As Boolean
        Dim retval As Boolean
        If (oCta.Id.StartsWith("6") Or oCta.Id.StartsWith("2")) Then
            If Not oCta.Id.StartsWith("643") And oCta.Codi <> DTOPgcPlan.Ctas.Dietas Then
                retval = True
            End If
        End If
        Return retval
    End Function

    Shared Function GetCtaProveedors(ByVal oCur As DTOCur) As DTOPgcPlan.Ctas
        Dim retval As DTOPgcPlan.Ctas
        Select Case oCur.Tag
            Case "USD"
                retval = DTOPgcPlan.Ctas.ProveidorsUsd
            Case "GBP"
                retval = DTOPgcPlan.Ctas.ProveidorsGbp
            Case Else
                retval = DTOPgcPlan.Ctas.ProveidorsEur
        End Select
        Return retval
    End Function

    Shared Function IsExplotacio(oCta As DTOPgcCta) As Boolean
        Dim retval As Boolean = oCta.Id >= "6"
        Return retval
    End Function

    Shared Function IsActivable(oCta As DTOPgcCta) As Boolean
        Return oCta.id.StartsWith("2")
    End Function

    ReadOnly Property Digits3 As String
        Get
            Dim retval As String = Left(_Id, 3)
            Return retval
        End Get
    End Property

    ReadOnly Property Digits4 As String
        Get
            Dim retval As String = Left(_Id, 4)
            Return retval
        End Get
    End Property

    Public Class Sdo
        Property Fch As Date
        Property Eur As Decimal
    End Class
End Class
