
Public Class PdfLogisticLabelEroski
    Inherits _PdfBase
    Private _Delivery As DTODelivery

    Public Sub New(oDelivery As DTODelivery)
        MyBase.New(landscape:=True)
        _Delivery = oDelivery

        MyBase.SetMargins(30, 100, 100, 30)
        Dim firstLabel As Boolean = True
        For Each item As DTODeliveryItem In oDelivery.Items
            item.Delivery = oDelivery
            For iBulto As Integer = 1 To DTODeliveryItem.Bultos(item)
                If firstLabel Then firstLabel = False Else MyBase.NewPage()
                DrawPage(item, iBulto)
            Next
        Next
    End Sub



    Private Sub DrawPage(item As DTODeliveryItem, iBulto As Integer)
        Dim oLang = DTOLang.POR
        Dim textLeftMargin As Integer = 40
        MyBase.Font = New System.Drawing.Font("Helvetica", 8)
        MyBase.X = 10
        MyBase.Y = 10
        'MyBase.DrawStringLine("TO:   ENT. TEXTIL")
        'MyBase.DrawStringLine("         E.N. 3 KM7")
        'MyBase.DrawStringLine("         ARNEIRO")
        'MyBase.DrawStringLine("         2050                AZAMBUJA")


        MyBase.DrawRectangle(0, 100) 'frame
        MyBase.DrawRectangle(20, 180, 200, 28) 'cod proveidor
        MyBase.DrawRectangle(240, 180, MyBase.MarginsRectangle.Width - 240 - 20, 28) 'nom proveidor
        MyBase.DrawRectangle(20, 226, MyBase.MarginsRectangle.Width - 240 - 40, 28) 'num.comanda
        'MyBase.DrawRectangle(MyBase.MarginsRectangle.Width - 240 - 20, 226, 120, 28) 'num.caixa
        'MyBase.DrawRectangle(MyBase.MarginsRectangle.Width - 120 - 20, 226, 120, 28) 'total caixas
        MyBase.DrawRectangle(20, 288, MyBase.MarginsRectangle.Width - 40, 28) 'EAN14
        MyBase.DrawRectangle(20, 334, MyBase.MarginsRectangle.Width - 360 - 40, 28) 'Sku
        MyBase.DrawRectangle(MyBase.MarginsRectangle.Width - 360 - 20, 334, 360, 28) 'Qty
        MyBase.DrawRectangle(20, 380, MyBase.MarginsRectangle.Width - 40, 28) 'descripc.producte
        MyBase.DrawRectangle(20, 426, MyBase.MarginsRectangle.Width - 40, MyBase.MarginsRectangle.Height - 426 - 20) 'obs

        MyBase.Font = New System.Drawing.Font("Helvetica", 9)
        MyBase.X = 20
        MyBase.Y = 210
        MyBase.DrawString("Código proveedor")

        MyBase.Y += 46
        MyBase.DrawString("Número de pedido")
        'MyBase.X = MyBase.MarginsRectangle.Width - 240 - 20
        'MyBase.DrawString("N° Caixa")
        'MyBase.X = MyBase.MarginsRectangle.Width - 120 - 20
        'MyBase.DrawString("Total de Caixas")
        'MyBase.Y += Me.Font.Height
        'MyBase.X = MyBase.MarginsRectangle.Width - 240 - 20
        'MyBase.DrawString("Carton Number")
        'MyBase.X = MyBase.MarginsRectangle.Width - 120 - 20
        'MyBase.DrawString("Total Cartons")

        MyBase.X = 20
        MyBase.Y = 318
        MyBase.DrawString("EAN13")

        MyBase.Y += 46
        MyBase.DrawString("Referencia Eroski")

        MyBase.X = MyBase.MarginsRectangle.Width - 360 - 20
        MyBase.DrawString("Unidades")

        MyBase.X = 20
        MyBase.Y += 46
        MyBase.DrawString("Descripción del producto")

        'If item.Sku.MadeIn Is Nothing Then
        '    Dim sMsg As String = String.Format("falta pais origen a {0}", item.Sku.NomLlarg.Tradueix(oLang))
        '    If Not MyBase.exs.Any(Function(x) x.Message = sMsg) Then
        '        MyBase.exs.Add(New Exception(sMsg))
        '    End If
        'Else
        '    MyBase.Font = New System.Drawing.Font("Helvetica", 19, FontStyle.Bold)
        '    MyBase.X = 350
        '    MyBase.Y = 20
        '    MyBase.DrawStringLine(String.Format("MADE IN {0}", item.Sku.MadeIn.LangNom.Tradueix(DTOLang.ENG)))
        'End If

        MyBase.Font = New System.Drawing.Font("Helvetica", 19, FontStyle.Bold)

        MyBase.X = textLeftMargin
        MyBase.Y = 183
        MyBase.DrawString(item.Delivery.Customer.CcxOrMe.SuProveedorNum)

        MyBase.X = 350
        MyBase.DrawString("MATIAS MASSO, S.A.")

        MyBase.X = textLeftMargin
        MyBase.Y = 229
        MyBase.DrawString(item.PurchaseOrderItem.PurchaseOrder.Concept)

        'Dim iDataWidth As Integer = MyBase.MeasureStringWidth(iBulto.ToString())
        'MyBase.X = MyBase.MarginsRectangle.Width - 20 - 240 + (120 - iDataWidth) / 2
        'MyBase.DrawString(iBulto)

        'Dim iBultos As Integer = DTODeliveryItem.Bultos(item)
        'iDataWidth = MyBase.MeasureStringWidth(iBultos.ToString())
        'MyBase.X = MyBase.MarginsRectangle.Width - 20 - 120 + (120 - iDataWidth) / 2
        'MyBase.DrawString(iBultos)


        If item.Sku.Ean13 Is Nothing Then
            Dim sMsg As String = String.Format("falta codi Ean a {0}", item.Sku.NomLlarg.Tradueix(oLang))
            If Not MyBase.exs.Any(Function(x) x.Message = sMsg) Then
                MyBase.exs.Add(New Exception(sMsg))
            End If
        Else
            MyBase.X = textLeftMargin
            MyBase.Y = 291
            MyBase.DrawString(item.Sku.Ean13.Value)
        End If

        If item.Sku.RefCustomer = "" Then
            Dim sMsg As String = String.Format("falta ref. client a {0}", item.Sku.NomLlarg.Tradueix(oLang))
            If Not MyBase.exs.Any(Function(x) x.Message = sMsg) Then
                MyBase.exs.Add(New Exception(sMsg))
            End If
        Else
            MyBase.X = textLeftMargin
            MyBase.Y = 337
            MyBase.DrawString(item.Sku.RefCustomer)
        End If

        MyBase.Y = 337
        MyBase.X = MyBase.MarginsRectangle.Width - 360 - 20 + 20
        MyBase.DrawString(Math.Min(item.Qty, DTOProductSku.InnerPackOrInherited(item.Sku)))

        MyBase.X = textLeftMargin
        MyBase.Y = 383
        MyBase.DrawString(item.Sku.NomLlarg.Esp)

        Dim oImage As Image = My.Resources.RecycleSonae
        Dim rc As New Rectangle(MyBase.MarginsRectangle.Width, 140, 60, 59)
        MyBase.DrawImage(oImage, rc)

        DrawBarcodeImage(item)
    End Sub

    Private Sub DrawBarcodeImage(item As DTODeliveryItem)
        If item.Sku.Ean13 IsNot Nothing Then
            'Dim oBarcode As New BarcodeLib.Barcode()
            'oBarcode.BarWidth = 2
            Dim ImgWidth As Integer = 500
            Dim imgHeight As Integer = MyBase.MarginsRectangle.Height - 426 - 20
            Dim oBarcodeImg As Image = MatHelper.BarcodeHelper.EAN13Image(item.Sku.Ean13.Value, ImgWidth, imgHeight)
            'Dim oBarcodeImg As Image = oBarcode.Encode(BarcodeLib.TYPE.EAN13, item.Sku.Ean13.Value, ImgWidth, imgHeight)

            MyBase.X = MyBase.MarginsRectangle.Left + (MyBase.MarginsRectangle.Width - oBarcodeImg.Width) / 2
            MyBase.Y = MyBase.MarginsRectangle.Bottom - 20 - imgHeight
            Dim rc As New Rectangle(MyBase.X, MyBase.Y, oBarcodeImg.Width, oBarcodeImg.Height)
            MyBase.DrawImage(oBarcodeImg, rc)
        End If
    End Sub

End Class
