Public Class DTODiari
    Property Emp As DTOEmp
    Property Lang As DTOLang
    Property Year As Integer
    Property Month As Integer
    Property Day As Integer
    Property Channel As DTODistributionChannel
    Property Rep As DTORep
    Property Years As List(Of Integer)
    Property Brands As List(Of DTOProductBrand)
    Property MaxDisplayableBrands As Integer = 100
    Property Items As List(Of DtoDiariItem)
    Property Owner As DTOContact
    Property User As DTOUser

    Property Mode As Modes = Modes.Pdcs

    Public Enum Modes
        Pdcs
        Albs
        Fras
    End Enum

    Shared Function Factory(oMode As DTODiari.Modes, oUser As DTOUser, Optional iYear As Integer = 0, Optional iMonth As Integer = 0, Optional iDay As Integer = 0, Optional oOwner As DTOContact = Nothing, Optional oChannel As DTODistributionChannel = Nothing, Optional oRep As DTORep = Nothing) As DTODiari
        If iYear = 0 Then iYear = DateTime.Today.Year
        Dim retval As New DTODiari
        With retval
            .Year = iYear
            .Month = iMonth
            .Day = iDay
            .Channel = oChannel
            .Rep = oRep
            .Emp = oUser.Emp
            .Lang = oUser.Lang
            .User = oUser
            .Owner = oOwner
            .Mode = oMode
        End With
        Return retval
    End Function

    Public Function Fch() As Date
        Dim retval As Date = Nothing
        If Year = 0 Then
            retval = DateTime.Today
        ElseIf _Month = 0 Then
            If Year = DateTime.Today.Year Then
                retval = DateTime.Today
            Else
                retval = New Date(_Year, 12, 31)
            End If
        ElseIf _Day = 0 Then
            If _Year = DateTime.Today.Year And _Month = DateTime.Today.Month Then
                retval = DateTime.Today
            Else
                retval = New Date(_Year, _Month, Date.DaysInMonth(_Year, _Month))
            End If
        Else
            retval = New Date(_Year, _Month, _Day)
        End If
        Return retval
    End Function

    Public Function DisplayableBrandsCount() As Integer
        Dim retval As Integer= Math.Min(_Brands.Count, MaxDisplayableBrands)
        Return retval
    End Function

    Public Function DisplayableBrands() As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = _Brands.GetRange(0, DisplayableBrandsCount)
        Return retval
    End Function
End Class

Public Class DtoDiariItem
    Property Parent As DTODiari
    Property Source As Object
    Property PurchaseOrder As DTOPurchaseOrder
    Property Fch As Date
    Property Level As Levels
    Property Index As Integer
    Property ParentIndex As Integer
    Property Text As String
    Property Url As String
    Property Values As List(Of Decimal)
    Property Total As Decimal

    Public Enum Levels
        Yea
        Mes
        Dia
        Pdc
    End Enum

    Public Function Resto() As Decimal
        Dim SumaDisplayedBrands As Decimal = 0

        For Each value As Decimal In DisplayableValues()
            SumaDisplayedBrands += value
        Next
        Dim retval As Decimal = Total - SumaDisplayedBrands
        Return retval
    End Function

    Public Function DisplayableValues() As List(Of Decimal)
        Dim retval As List(Of Decimal) = _Values.GetRange(0, _Parent.DisplayableBrandsCount)
        Return retval
    End Function

End Class
