Public Class DTOAmortization
    Inherits DTOBaseGuid

    Property Emp As DTOEmp
    Property Cta As DTOPgcCta
    Property Fch As Date
    Property Amt As DTOAmt
    Property Tipus As Decimal
    Property Dsc As String
    Property Alta As DTOCca
    Property Baixa As DTOCca
    Property Items As List(Of DTOAmortizationItem)


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oAltaCca As DTOCca) As DTOAmortization
        Dim retval As DTOAmortization = Nothing
        Dim oActivableCcbs = oAltaCca.Items.Where(Function(x) DTOPgcCta.IsActivable(x.Cta)).ToList
        For Each oCcb In oActivableCcbs
            retval = New DTOAmortization
            With retval
                .Cta = oCcb.Cta
                .Alta = oAltaCca
                .Fch = oAltaCca.Fch
                .Dsc = oAltaCca.Concept
                .Amt = oCcb.Amt
                .Items = New List(Of DTOAmortizationItem)
            End With
            With oAltaCca
                .Ccd = DTOCca.CcdEnum.InmovilitzatAlta
            End With
            Exit For
        Next
        Return retval
    End Function

    Shared Function Saldo(value As DTOAmortization) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If value.Baixa Is Nothing Then
            Dim DcValorAdquisicio As Decimal = value.Amt.Eur
            Dim DcAmortitzat As Decimal = value.Items.Sum(Function(x) x.Amt.Eur)
            retval = DTOAmt.Factory(DcValorAdquisicio - DcAmortitzat)
        Else
            retval = DTOAmt.Empty
        End If

        Return retval
    End Function

    Shared Function Amortitzat(value As DTOAmortization) As DTOAmt
        Dim DcAmortitzat As Decimal = value.Items.Sum(Function(x) x.Amt.Eur)
        Dim retval As DTOAmt = DTOAmt.Factory(DcAmortitzat)
        Return retval
    End Function


    Shared Function BaixaItem(oAmortization As DTOAmortization) As DTOAmortizationItem
        Dim retval As New DTOAmortizationItem
        With retval
            .Parent = oAmortization
            .Fch = DateTime.Today
            .Cod = DTOAmortizationItem.Cods.Baixa
            .Amt = DTOAmortization.Saldo(oAmortization)
            .Tipus = .Amt.Eur / oAmortization.Amt.Eur
        End With
        Return retval
    End Function


    Shared Function CtaCodAmrtAcumulada(oCodImmobilitzat As DTOPgcPlan.Ctas) As DTOPgcPlan.Ctas
        Dim retval As DTOPgcPlan.Ctas = DTOPgcPlan.Ctas.AmrtAcumAltres
        Select Case oCodImmobilitzat
            Case DTOPgcPlan.Ctas.InmobilitzatIntangible
                retval = DTOPgcPlan.Ctas.AmrtAcumIntangible
            Case DTOPgcPlan.Ctas.InmobilitzatSoftware
                retval = DTOPgcPlan.Ctas.AmrtAcumSoftware
            Case DTOPgcPlan.Ctas.InmobilitzatTerrenys
                retval = DTOPgcPlan.Ctas.NotSet
            Case DTOPgcPlan.Ctas.InmobilitzatConstruccions
                retval = DTOPgcPlan.Ctas.AmrtAcumConstruccions
            Case DTOPgcPlan.Ctas.InmobilitzatInstalacionsAltres
                retval = DTOPgcPlan.Ctas.AmrtAcumInstalacionsAltres
            Case DTOPgcPlan.Ctas.InmobilitzatMobles
                retval = DTOPgcPlan.Ctas.AmrtAcumMobles
            Case DTOPgcPlan.Ctas.InmobilitzatVehicles
                retval = DTOPgcPlan.Ctas.AmrtAcumVehicles
            Case DTOPgcPlan.Ctas.InmobilitzatHardware
                retval = DTOPgcPlan.Ctas.AmrtAcumHardware
            Case DTOPgcPlan.Ctas.InmobilitzatAltres
                retval = DTOPgcPlan.Ctas.AmrtAcumAltres
        End Select
        Return retval
    End Function

    Shared Function CtaCodDotacio(oCodImmobilitzat As DTOPgcPlan.Ctas) As DTOPgcPlan.Ctas
        Dim retval As DTOPgcPlan.Ctas = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial
        Select Case oCodImmobilitzat
            Case DTOPgcPlan.Ctas.InmobilitzatSoftware
                retval = DTOPgcPlan.Ctas.DotacioAmortitzacioIntangible
            Case DTOPgcPlan.Ctas.InmobilitzatConstruccions
                retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial
            Case DTOPgcPlan.Ctas.InmobilitzatInstalacionsAltres
                retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial
            Case DTOPgcPlan.Ctas.InmobilitzatMobles
                retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial
            Case DTOPgcPlan.Ctas.InmobilitzatHardware
                retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial
            Case DTOPgcPlan.Ctas.InmobilitzatVehicles
                retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial
            Case DTOPgcPlan.Ctas.InmobilitzatAltres
                retval = DTOPgcPlan.Ctas.DotacioAmortitzacioMaterial
        End Select
        Return retval
    End Function

End Class

Public Class DTOAmortizationItem
    Inherits DTOBaseGuid

    Property Parent As DTOAmortization
    Property Fch As Date
    Property Tipus As Decimal
    Property Amt As DTOAmt
    Property Cca As DTOCca
    Property Saldo As DTOAmt
    Property Cod As Cods

    Public Enum Cods
        Amortitzacio
        Baixa
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function ConcepteAmortitzacio(oItem As DTOAmortizationItem) As String
        Dim oAmortization As DTOAmortization = oItem.Parent
        Dim oCtaImmobilitzat As DTOPgcCta = oAmortization.Cta
        Dim retval As String = "Cta." & oCtaImmobilitzat.Id & " " & oItem.Tipus & "% s/" & DTOAmt.CurFormatted(oAmortization.Amt) & "-" & oAmortization.Dsc
        retval = TextHelper.VbLeft(retval, 60)
        Return retval
    End Function
End Class

Public Class DTOAmortizationTipus
    Property Cta As DTOPgcCta
    Property Tipus As Decimal

    Shared Function ForCta(oDefaultTipus As List(Of DTOAmortizationTipus), oCta As DTOPgcCta) As Decimal
        Dim retval As Decimal = 0
        Dim item = oDefaultTipus.FirstOrDefault(Function(x) x.Cta.Equals(oCta))
        If item IsNot Nothing Then
            retval = item.Tipus
        End If
        Return retval
    End Function
End Class
