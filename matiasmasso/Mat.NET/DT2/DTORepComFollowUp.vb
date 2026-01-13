Public Class DTORepComFollowUp
    Inherits DTOBaseGuid
    Property Source As Object

    Property Parent As DTORepComFollowUp
    Property Level As Levels
    Property Period As String
    Property Ordered As Decimal
    Property Delivered As Decimal
    Property Invoiced As Decimal
    Property Liquid As Decimal

    Public Enum Levels
        Month
        Day
        Order
    End Enum

    Public Sub New()
        MyBase.New
    End Sub

    Shadows Function Equals(oCandidate As DTORepComFollowUp) As Boolean
        Dim retval As Boolean
        If oCandidate IsNot Nothing Then
            retval = MyBase.Guid.Equals(oCandidate.Guid)
        End If
        Return retval
    End Function

    Shared Function Months(oOrders As List(Of DTOPurchaseOrder), oLang As DTOLang) As List(Of DTORepComFollowUp)
        Dim retval As List(Of DTORepComFollowUp) = oOrders.
            GroupBy(Function(g) New With {Key g.Fch.Year, Key g.Fch.Month}).
                Select(Function(group) New DTORepComFollowUp With {
                    .Source = New DTOYearMonth(group.Key.Year, group.Key.Month),
                    .Level = DTORepComFollowUp.Levels.Month,
                    .Period = String.Format("{0:0000} {1}", group.Key.Year, oLang.MesAbr(group.Key.Month)),
                    .Ordered = group.Sum(Function(o) o.Items.Sum(Function(item) item.Amount.Eur)),
                    .Delivered = group.Sum(Function(o) o.Items.Sum(Function(item) item.Deliveries?.Sum(Function(d) d.Import.Eur))),
                    .Invoiced = group.Sum(Function(o) o.Items.Sum(Function(item) item.Deliveries?.Where(Function(arc) arc.Delivery.Invoice IsNot Nothing).Sum(Function(d) d.Import.Eur))),
                    .Liquid = group.Sum(Function(o) o.Items.Sum(Function(item) item.Deliveries?.Where(Function(arc) arc.RepLiq IsNot Nothing).Sum(Function(d) d.Import.Eur)))
                    }).ToList

        Return retval
    End Function

    Shared Function Days(oOrders As List(Of DTOPurchaseOrder), oParent As DTORepComFollowUp, oLang As DTOLang) As List(Of DTORepComFollowUp)
        Dim oYearMonth As DTOYearMonth = oParent.Source
        Dim retval As List(Of DTORepComFollowUp) = oOrders.
            Where(Function(o) o.Fch.Year = oYearMonth.Year And o.Fch.Month = oYearMonth.Month).
                    GroupBy(Function(g) New With {Key g.Fch}).
                        Select(Function(group) New DTORepComFollowUp With {
                            .Source = group.Key.Fch,
                            .Parent = oParent,
                            .Level = DTORepComFollowUp.Levels.Day,
                            .Period = String.Format("    {0:00} {1}", group.Key.Fch.Day, oLang.WeekDay(group.Key.Fch)),
                            .Ordered = group.Sum(Function(o) o.Items.Sum(Function(item) item.Amount.Eur)),
                            .Delivered = group.Sum(Function(o) o.Items.Sum(Function(item) item.Deliveries?.Sum(Function(d) d.Import.Eur))),
                            .Invoiced = group.Sum(Function(o) o.Items.Sum(Function(item) item.Deliveries?.Where(Function(arc) arc.Delivery.Invoice IsNot Nothing).Sum(Function(d) d.Import.Eur))),
                            .Liquid = group.Sum(Function(o) o.Items.Sum(Function(item) item.Deliveries?.Where(Function(arc) arc.RepLiq IsNot Nothing).Sum(Function(d) d.Import.Eur)))
                            }).ToList

        Return retval
    End Function

    Shared Function Orders(oOrders As List(Of DTOPurchaseOrder), oParent As DTORepComFollowUp) As List(Of DTORepComFollowUp)
        Dim DtFch As Date = oParent.Source
        Dim retval As List(Of DTORepComFollowUp) = oOrders.
            Where(Function(o) o.Fch = DtFch).
                Select(Function(x) New DTORepComFollowUp With {
                .Source = x,
                .Parent = oParent,
                .Level = DTORepComFollowUp.Levels.Order,
                .Period = String.Format("        {0}", x.Contact.FullNom),
                .Ordered = x.Items.Sum(Function(item) item.Amount.Eur),
                .Delivered = x.Items.Sum(Function(item) item.Deliveries?.Sum(Function(d) d.Import.Eur)),
                .Invoiced = x.Items.Sum(Function(item) item.Deliveries?.Where(Function(d) d.Delivery.Invoice IsNot Nothing).Sum(Function(inv) inv.Import.Eur)),
                .Liquid = x.Items.Sum(Function(item) item.Deliveries?.Where(Function(d) d.RepLiq IsNot Nothing).Sum(Function(liq) liq.Import.Eur))
                    }).ToList
        Return retval
    End Function

End Class
