Public Class TestClass
    Public Class Order
        Property OrderDate As Date
        Property Items As List(Of OrderItem)
    End Class

    Public Class OrderItem
        Property Qty As Integer
        Property Price As Decimal
        Property Shipments As List(Of Shipment)
    End Class

    Public Class Shipment
        Property OrderItem As OrderItem
        Property ShippedQty As Integer
        Property Price As Decimal
    End Class

    Shared Sub DoTest()
        Dim Orders As New List(Of Order)

        Dim Order1 As New Order
        Order1.OrderDate = Today.AddDays(-1)
        Order1.Items = New List(Of OrderItem)
        Orders.Add(Order1)

        Dim OrderItem1 As New OrderItem
        OrderItem1.Qty = 3
        OrderItem1.Price = 50
        OrderItem1.Shipments = New List(Of Shipment)
        Order1.Items.Add(OrderItem1)

        Dim OrderItem2 As New OrderItem
        OrderItem2.Qty = 5
        OrderItem2.Price = 45
        OrderItem2.Shipments = New List(Of Shipment)
        Order1.Items.Add(OrderItem2)

        Dim Shipment1 As New Shipment
        Shipment1.ShippedQty = 2
        Shipment1.Price = 43
        OrderItem2.Shipments.Add(Shipment1)

        Dim result = Orders.GroupBy(Function(g) g.OrderDate.ToString("yyyy-MM")).
        Select(Function(group) New With {
            .Month = group.Key,
            .OrderedSum = group.Sum(Function(o) o.Items.Sum(Function(item) item.Qty * item.Price)),
            .ShippedSum = group.Sum(Function(o) o.Items.Sum(Function(item) item.Shipments?.Sum(Function(ship) ship.ShippedQty * ship.Price)))
            })

        'Print result
        For Each item In result
            Console.WriteLine($"{item.Month}{vbTab}Ordered: {item.OrderedSum}, Shipped: {item.ShippedSum}")
        Next
    End Sub

End Class
