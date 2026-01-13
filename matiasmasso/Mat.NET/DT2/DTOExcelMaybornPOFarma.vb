Public Class DTOExcelMaybornPOFarma
    Inherits  MatHelperStd.ExcelHelper.Sheet

    Public Enum Cols
        Customer
        Delivery_Sequence
        Name
        Customer_Item_Ref
        Item
        Description
        Order_Number
        Customer_Order_Ref
        Order_Line
        Quantity_Outstanding
        Date_Delivery_Required
        Order_Value
        Allocated
        Delivery_Address
        Despatch_Date
        Quantity_Allocated
        [Date]
        Validation
    End Enum

    Shared Shadows Function Factory(oSheet As  MatHelperStd.ExcelHelper.Sheet) As DTOExcelMaybornPOFarma
        Dim retval As New DTOExcelMaybornPOFarma
        'Dim iLastCol = [Enum].ToObject(GetType(Cols), [Enum].GetValues(GetType(Cols)).Cast(Of Integer).Last)
        Dim oColumn As ExcelHelper.Column = Nothing
        For Each oRow In oSheet.Rows
            Try
                If oRow.Equals(oSheet.Rows.First) Then
                    For iCol = 0 To Cols.Validation
                        Select Case iCol
                            Case Cols.Quantity_Allocated
                                oColumn = New ExcelHelper.Column(oRow.Cells(iCol).Content, NumberFormats.Integer)
                                'oColumn.ForeColor = Color.Navy
                                'oColumn.BackColor = Color.AliceBlue
                            Case Cols.Validation
                                oColumn = New ExcelHelper.Column("Validation", NumberFormats.PlainText)
                                'oColumn.ForeColor = Color.Navy
                                'oColumn.BackColor = Color.AliceBlue
                            Case Else
                                oColumn = New ExcelHelper.Column(oRow.Cells(iCol).Content, NumberFormats.NotSet)
                        End Select
                        retval.Columns.Add(oColumn)
                    Next
                Else
                    oRow.Cells.Add(New  MatHelperStd.ExcelHelper.Cell) 'Add validation cell
                    retval.Rows.Add(oRow)
                End If
            Catch ex As Exception
                Stop
            End Try
        Next
        Return retval
    End Function

    Public Function Validate(oInventari As List(Of DTOProductSku), oCustomers As List(Of DTOPrvCliNum))
        For Each oRow In MyBase.Rows
            Dim sb As New Text.StringBuilder

            If Not oCustomers.Any(Function(x) x.CliNum = oRow.GetString(Cols.Customer)) Then
                sb.Append("Unknown customer. ")
                'oRow.Cells(Cols.Customer).ForeColor = Color.Red
            End If
            Dim oSku = me.Sku(oRow, oInventari)
            If oSku Is Nothing Then
                sb.Append("Unknown Sku. ")
                'oRow.Cells(Cols.Item).ForeColor = Color.Red
            Else
                Dim iQty = oRow.GetInt(Cols.Quantity_Outstanding)
                Dim iStock = DTOProductSku.StockAvailable(oSku)
                Dim iAvailable = Math.Min(iQty, iStock)
                If iAvailable = 0 Then
                    sb.Append("Not available")
                    'oRow.Cells(Cols.Quantity_Outstanding).ForeColor = Color.Red
                    'oRow.Cells(Cols.Quantity_Allocated).ForeColor = Color.Red
                ElseIf iAvailable = iQty Then
                    oRow.Cells(Cols.Quantity_Allocated).Content = iAvailable
                Else
                    sb.Append("Partial quantity")
                    'oRow.Cells(Cols.Quantity_Outstanding).ForeColor = Color.Red
                    'oRow.Cells(Cols.Quantity_Allocated).ForeColor = Color.Red
                End If

                Dim iMoq = DTOProductSku.OuterPackOrInherited(oSku)
                If iMoq = 0 Then iMoq = DTOProductSku.InnerPackOrInherited(oSku)
                If iQty Mod iMoq <> 0 Then
                    sb.Append(String.Format("Quantity not multiple of {0}", iMoq))
                    'oRow.Cells(Cols.Quantity_Outstanding).ForeColor = Color.Red
                End If
            End If

            If sb.ToString = "" Then
                oRow.Cells(Cols.Validation).Content = "Ok"
            Else
                oRow.Cells(Cols.Validation).Content = sb.ToString
                'oRow.Cells(Cols.Validation).ForeColor = Color.Red
            End If
        Next
        Return Me
    End Function


    Public Function PurchaseOrders(oInventari As List(Of DTOProductSku), oPrvCliNums As List(Of DTOPrvCliNum), oUser As DTOUser) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        For Each oFarmaOrder In Me.FarmaOrders
            Dim oPurchaseOrder = PurchaseOrder(oFarmaOrder, oInventari, oPrvCliNums, oUser)
            retval.Add(oPurchaseOrder)
        Next
        Return retval
    End Function

    Private Function PurchaseOrder(oFarmaOrder As FarmaOrder, oInventari As List(Of DTOProductSku), oPrvCliNums As List(Of DTOPrvCliNum), oUser As DTOUser) As DTOPurchaseOrder
        Dim oPrvCliNum = oPrvCliNums.FirstOrDefault(Function(x) x.CliNum = oFarmaOrder.CustomerNum)
        Dim sConcept = String.Format("{0} (Mayborn #{1})", oFarmaOrder.CustomerOrderNum, oFarmaOrder.MaybornOrderNum)
        Dim retval = DTOPurchaseOrder.Factory(oPrvCliNum.Customer, oUser, oFarmaOrder.OrderDate, DTOPurchaseOrder.Sources.ExcelMayborn, sConcept)
        For Each oRow In FarmaOrderRows(oFarmaOrder.MaybornOrderNum)
            Dim item = PurchaseOrderItem(oRow, oInventari)
            If item.Qty > 0 Then
                retval.Items.Add(PurchaseOrderItem(oRow, oInventari))
            End If
        Next
        Return retval
    End Function

    Private Function PurchaseOrderItem(oRow As  MatHelperStd.ExcelHelper.Row, oInventari As List(Of DTOProductSku)) As DTOPurchaseOrderItem
        Dim retval As New DTOPurchaseOrderItem()
        With retval
            .Sku = Sku(oRow, oInventari)
            .Qty = oRow.GetInt(Cols.Quantity_Allocated)
            .Pending = .Qty
            If .Sku IsNot Nothing Then
                .Price = .Sku.Cost
            End If
        End With
        Return retval
    End Function

    Private Function Sku(oRow As  MatHelperStd.ExcelHelper.Row, oInventari As List(Of DTOProductSku)) As DTOProductSku
        Return oInventari.FirstOrDefault(Function(x) x.RefProveidor = oRow.GetString(Cols.Item))
    End Function


    Public Function FarmaOrders() As List(Of FarmaOrder)
        Dim retval As New List(Of FarmaOrder)
        For Each oRow In MyBase.Rows
            Dim oFarmaOrder = DTOExcelMaybornPOFarma.FarmaOrder.Factory(oRow.GetString(Cols.Order_Number), oRow.GetString(Cols.Customer_Order_Ref), oRow.GetString(Cols.Customer), oRow.GetFchSpain(Cols.Date))
            If oFarmaOrder.MaybornOrderNum > "" Then
                If Not retval.Any(Function(x) x.MaybornOrderNum = oFarmaOrder.MaybornOrderNum) Then
                    retval.Add(oFarmaOrder)
                End If
            End If
        Next
        Return retval
    End Function

    Public Function FarmaOrderRows(MaybornOrderNum As String) As List(Of  MatHelperStd.ExcelHelper.Row)
        Dim retval = MyBase.rows.Where(Function(x) x.getString(Cols.Order_Number) = MaybornOrderNum).ToList
        Return retval
    End Function

    Public Class FarmaOrder
        Property MaybornOrderNum As String
        Property CustomerOrderNum As String
        Property CustomerNum As String
        Property OrderDate As Date

        Shared Function Factory(MaybornOrderNum As String, CustomerOrderNum As String, CustomerNum As String, OrderDate As Date) As FarmaOrder
            Dim retval As New FarmaOrder
            With retval
                .MaybornOrderNum = MaybornOrderNum
                .CustomerOrderNum = CustomerOrderNum
                .CustomerNum = CustomerNum
                .OrderDate = OrderDate
            End With
            Return retval
        End Function
    End Class
End Class
