Public Class DTOOrderConfirmation
    Property Supplier As DTOEdiversaInterlocutor 'NADSU
    Property Buyer As DTOEdiversaInterlocutor 'NADBY
    Property DeliveryPlatform As DTOEdiversaInterlocutor 'NADDP
    Property InvoiceTo As DTOEdiversaInterlocutor 'NADDIV
    Property Currency As DTOCur
    Property Order As DTOPurchaseOrder
    Property Fch As Date
    Property Num As String
    Property OrderNumber As String
    Property Items As List(Of Item)
    Property Exceptions As List(Of Exception)

    Public Sub New()
        MyBase.New
        _Items = New List(Of Item)
        _Exceptions = New List(Of Exception)
    End Sub

    Shared Function Factory(sFilename As String, exs As List(Of Exception)) As DTOOrderConfirmation
        Dim retval As DTOOrderConfirmation = Nothing
        Dim segments As List(Of String) = TextHelper.ReadFileToStringList(sFilename, exs)
        If segments IsNot Nothing Then
            retval = Factory(segments)
        End If
        Return retval
    End Function

    Shared Function Factory(segments As List(Of String)) As DTOOrderConfirmation
        Dim retval As New DTOOrderConfirmation
        Dim provider As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture

        Dim item As DTOOrderConfirmation.Item = Nothing
        For Each segment As String In segments
            Dim fields As List(Of String) = segment.Split("|").ToList
            Select Case fields.First
                Case "BGM"
                    retval.Num = fields(1)
                Case "DTM"
                    retval.Fch = Date.ParseExact(fields(1), "yyyyMMdd", provider)
                Case "RFF"
                    If fields.Count > 2 AndAlso fields(1) = "ON" Then
                        retval.OrderNumber = fields(2)
                    End If
                Case "NADSU"
                    retval.Supplier = New DTOEdiversaInterlocutor(segment)
                Case "NADBY"
                    retval.Buyer = New DTOEdiversaInterlocutor(segment)
                Case "NADDP"
                    retval.DeliveryPlatform = New DTOEdiversaInterlocutor(segment)
                Case "NADIV"
                    retval.InvoiceTo = New DTOEdiversaInterlocutor(segment)
                Case "CUX"
                    retval.Currency = DTOCur.Factory(fields(1))
                Case "LIN"
                    item = New DTOOrderConfirmation.Item(segment)
                Case "PIALIN"
                    If fields.Count > 2 AndAlso fields(1) = "SA" AndAlso item IsNot Nothing Then
                        item.SupplierRef = fields(2)
                    End If
                Case "IMDLIN"
                    If fields.Count > 3 AndAlso item IsNot Nothing Then
                        item.Description = fields(3)
                    End If
                Case "QTYLIN"
                    If fields.Count > 2 AndAlso item IsNot Nothing Then
                        item.Qty = fields(2)
                    End If
                Case "DTMLIN"
                    item.DeliveryTime = Date.ParseExact(fields(1), "yyyyMMdd", provider)
                Case "PRILIN"
                    If fields.Count > 2 AndAlso item IsNot Nothing Then
                        item.Price = fields(2)
                    End If
            End Select
        Next
        Return retval
    End Function
    Public Class Item
        Property Ean As String
        Property Lin As Integer
        Property SupplierRef As String
        Property Description As String
        Property Sku As DTOProductSku
        Property Qty As Integer
        Property Price As Decimal
        Property Dto As Decimal
        Property DeliveryTime As Date

        Public Sub New(segment As String)
            MyBase.New
            Dim fields As List(Of String) = segment.Split("|").ToList
            If fields.Count > 1 Then
                _Ean = fields(1)
                If fields.Count > 3 Then
                    _Lin = fields(3)
                End If
            End If
        End Sub
    End Class

End Class
