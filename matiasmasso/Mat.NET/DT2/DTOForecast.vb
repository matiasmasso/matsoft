Public Class DTOForecast
    Property Customer As DTOCustomer
    Property Sku As DTOProductSku
    Property YearMonth As DTOYearMonth
    Property Qty As Integer
    Property Sold As Integer
    Property UserCreated As DTOUser
End Class

Public Class DTOForecastProposal
    Property Sku As DTOProductSku
    Property Forecasts As List(Of DTOForecast)
    Property Forecasted As Integer
    Property Suggested As Integer

    Property OptimizedQty As Integer
End Class

Public Class DTOProductSkuForecast
    Inherits DTOProductSku

    Public Shadows Class Collection
        Inherits List(Of DTOProductSkuForecast)

        Public Function GetFollowUp(oBrand As DTOProductBrand, fch As Date) As FollowUp
            Dim retval As New FollowUp(oBrand)
            For Each Item As DTOProductSkuForecast In Me.Where(Function(x) x.Category.Brand.Equals(oBrand))
                Dim followUp = Item.GetFollowUp(fch)
                With retval
                    .FcastQty += followUp.FcastQty
                    .SoldQty += followUp.SoldQty
                    .FcastAmt.add(followUp.FcastAmt)
                    .SoldAmt.add(followUp.SoldAmt)
                End With
            Next
            Return retval
        End Function

        Public Function GetFollowUp(oCategory As DTOProductCategory, fch As Date) As FollowUp
            Dim retval As New FollowUp(oCategory)
            For Each Item As DTOProductSkuForecast In Me.Where(Function(x) x.Category.Equals(oCategory))
                Dim followUp = Item.GetFollowUp(fch)
                With retval
                    .FcastQty += followUp.FcastQty
                    .SoldQty += followUp.SoldQty
                    .FcastAmt.add(followUp.FcastAmt)
                    .SoldAmt.add(followUp.SoldAmt)
                End With
            Next
            Return retval
        End Function

    End Class

    Public Class FollowUp
        Property Product As DTOProduct
        Property FcastQty As Integer
        Property SoldQty As Integer
        Property FcastAmt As DTOAmt
        Property SoldAmt As DTOAmt

        Public Sub New(oProduct As DTOProduct)
            _Product = oProduct
            _FcastAmt = DTOAmt.Factory
            _SoldAmt = DTOAmt.Factory
        End Sub
    End Class

    Property Forecasts As List(Of Forecast)

    Public Sub New() 'for serialization
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Forecasts = New List(Of Forecast)
    End Sub

    Shared Function Volume(oSkus As DTOProductSkuForecast.Collection, DtFch As Date) As Decimal
        Dim retval As Decimal
        Dim oSkusTrimmed = oSkus.Where(Function(x) x.Forecasts.Count > 0).Where(Function(y) y.Forecasts.Any(Function(z) z.Target <> 0)).ToList
        For Each oSku In oSkusTrimmed
            Dim iForecast = Forecasted(oSku, DtFch)
            Dim iProposal = Proposal(oSku, iForecast)
            Dim iOptimized = OptimizedProposal(oSku, iProposal)
            Dim DcVolume = iOptimized * DTOProductSku.volumeM3OrInherited(oSku)
            retval += DcVolume
        Next
        Return retval
    End Function

    Shared Function Forecasted(oSku As DTOProductSkuForecast, DtFch As Date) As Integer
        Dim firstYearMonth = DTOYearMonth.current
        Dim lastYearMonth = DTOYearMonth.fromFch(DtFch)
        Dim dcForecasted As Decimal = 0

        Dim oFcasts = oSku.Forecasts.Where(Function(x) x.YearMonth.isInRange(DateTime.Now, DtFch)).ToList
        For Each item In oFcasts
            Dim monthMinutes = item.YearMonth.daysInmonth * 24 * 60
            If item.YearMonth.isLowerThan(firstYearMonth) Then
                'skip past forecasts
            ElseIf item.YearMonth.Equals(firstYearMonth) And item.YearMonth.Equals(lastYearMonth) Then
                'If item.Target > 100 Then Stop ' --------------------------------------------------------------------
                Dim minutesSpent = (DtFch - DateTime.Now).TotalMinutes
                dcForecasted += item.Target * minutesSpent / monthMinutes
                Exit For
            ElseIf item.YearMonth.Equals(firstYearMonth) Then
                Dim minutesSpent = (item.YearMonth.lastFch - DateTime.Now).TotalMinutes
                dcForecasted += item.Target * minutesSpent / monthMinutes
            ElseIf item.YearMonth.Equals(lastYearMonth) Then
                Dim minutesSpent = DtFch.Day * 24 * 60 + DtFch.Hour * 60 + DtFch.Minute
                dcForecasted += item.Target * minutesSpent / monthMinutes
            ElseIf item.YearMonth.isGreaterThan(lastYearMonth) Then
                Exit For
            Else
                dcForecasted += item.Target
            End If
        Next

        Dim retval As Integer = Math.Round(dcForecasted)
        Return retval
    End Function


    Public Function GetFollowUp(DtFch As Date) As DTOProductSkuForecast.FollowUp
        Dim retval As New DTOProductSkuForecast.FollowUp(Me)

        Dim oPastMonths = Me.Forecasts.Where(Function(x) x.YearMonth.year = DtFch.Year And x.YearMonth.month < DtFch.Month).ToList
        retval.FcastQty = oPastMonths.Sum(Function(x) x.Target)
        retval.SoldQty = oPastMonths.Sum(Function(x) x.Sold)

        Dim oCurrentMonth = Me.Forecasts.FirstOrDefault(Function(x) x.YearMonth.year = DtFch.Year And x.YearMonth.month = DtFch.Month)
        If oCurrentMonth IsNot Nothing Then
            Dim monthMinutes = DTOYearMonth.fromFch(DtFch).daysInmonth * 24 * 60
            Dim minutesSpent = DtFch.Day * 24 * 60 + DtFch.Hour * 60 + DtFch.Minute
            retval.FcastQty += oCurrentMonth.Target * minutesSpent / monthMinutes
            retval.SoldQty += oCurrentMonth.Sold
        End If

        If Me.Cost IsNot Nothing Then
            retval.FcastAmt = DTOAmt.Factory(retval.FcastQty * Me.Cost.Eur)
            retval.SoldAmt = DTOAmt.Factory(retval.SoldQty * Me.Cost.Eur)
        End If
        Return retval
    End Function


    Shared Function Proposal(oSku As DTOProductSku, iForecast As Integer) As Integer
        Dim retval As Integer = iForecast + oSku.Clients + oSku.SecurityStock - oSku.Stock - oSku.Proveidors
        Return retval
    End Function

    Shared Function OptimizedProposal(oSku As DTOProductSku, iProposal As Integer) As Integer
        Dim retval As Integer = iProposal
        If oSku.LastProduction Then
            retval = 0
        ElseIf retval >= 0 Then
            If oSku.InnerPack > 1 Then
                Dim resto As Integer = retval Mod oSku.InnerPack
                retval += resto
            End If
        Else
            retval = 0
        End If
        Return retval
    End Function

    Shared Function Excel(values As DTOProductSkuForecast.Collection, oLang As DTOLang) As MatHelperStd.ExcelHelper.Sheet
        Dim dtFch = values.SelectMany(Function(x) x.Forecasts).Max(Function(y) y.FchCreated)
        Dim sSheetName As String = "Forecast " & TextHelper.VbFormat(dtFch, "dd/MM/yy HH:mm")
        Dim sFileName = String.Format("M+O Forecast {0:yyyy.MM.dd.HH.mm}.xlsx", dtFch)
        Dim sCurrentTag = DTOYearMonth.current.tag
        Dim sMinTag = values.SelectMany(Function(x) x.Forecasts).Where(Function(y) y.YearMonth.Tag >= sCurrentTag).Min(Function(z) z.YearMonth.Tag)
        Dim sMaxTag = values.SelectMany(Function(x) x.Forecasts).Max(Function(y) y.YearMonth.Tag)

        Dim oRange = DTOYearMonth.range(sMinTag, sMaxTag)
        Dim retval As New MatHelperStd.ExcelHelper.Sheet(sSheetName, sFileName)
        With retval
            .AddColumn("Brand")
            .AddColumn("Category")
            .AddColumn("Code")
            .AddColumn("Product")
            .AddColumn("Stock", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Customer pending", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Suplier pending", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Net Cost", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Volume", MatHelperStd.ExcelHelper.Sheet.NumberFormats.m3trimmed)
            For Each item In oRange
                .AddColumn(item.Formatted(DTOLang.ENG), MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            Next
        End With

        For Each value In values.Where(Function(x) x.HasTargetsInRange(oRange))
            Dim dcCost As Decimal = 0
            If value.cost IsNot Nothing Then
                dcCost = value.cost.Eur * (100 - value.customerDto) / 100
            End If

            Dim oRow = retval.AddRow()
            oRow.AddCell(value.category.brand.nom.Tradueix(oLang))
            oRow.AddCell(value.category.nom.Tradueix(oLang))
            oRow.AddCell(value.refProveidor)
            oRow.AddCell(value.nomProveidor)
            oRow.AddCell(value.stock)
            oRow.AddCell(value.clients)
            oRow.AddCell(value.proveidors)
            oRow.AddCell(dcCost)
            oRow.AddCell(value.VolumeM3OrInherited)
            For Each item In oRange
                Dim oFc As DTOProductSkuForecast.Forecast = value.Forecasts.FirstOrDefault(Function(x) x.YearMonth.Equals(item))
                If oFc Is Nothing Then
                    oRow.AddCell(0)
                Else
                    oRow.AddCell(oFc.Target)
                End If
            Next
        Next

        Return retval
    End Function



    Public Class Forecast
        Property YearMonth As DTOYearMonth
        Property Target As Integer
        Property Sold As Integer
        Property UserCreated As DTOUser
        Property FchCreated As Date
    End Class

    Public Function HasTargetsInRange(oRange As List(Of DTOYearMonth)) As Boolean
        Dim retval = _Forecasts.Where(Function(x) x.Target > 0).Any(Function(y) y.YearMonth.IsInRange(oRange))
        Return retval
    End Function

End Class
