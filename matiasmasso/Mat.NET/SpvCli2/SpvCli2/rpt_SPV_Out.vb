Imports VB = Microsoft.VisualBasic

Public Class rpt_SPV_Out
    Inherits System.Windows.Forms.Form

    Private _Delivery As DTODelivery

    Public Sub New(oDelivery As DTODelivery)
        MyBase.New()

        InitializeComponent()
        _Delivery = oDelivery
    End Sub

#Region " Windows Form Designer generated code "



    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(rpt_SPV_Out))
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = DirectCast(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.MaximumSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Opacity = 1
        Me.PrintPreviewDialog1.TransparencyKey = System.Drawing.Color.Empty
        Me.PrintPreviewDialog1.Visible = False
        '
        'rpt_SPV_Out
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Name = "rpt_SPV_Out"
        Me.Text = "rpt_SPV_Out"

    End Sub

#End Region

    Dim FontColHdr As Font
    Dim FontNum As Font
    Dim FontStd As Font
    Dim FontItalic As Font
    Dim FontTit As Font

    Dim StringOffset As Integer

    Dim hTabLeft As Integer
    Dim hTabRight As Integer
    Dim hTabHdrAdr As Integer
    Dim hTabHdrAlb As Integer

    Dim vTabLogo As Integer
    Dim vTabHdrTop As Integer
    Dim vTabHdrTit As Integer
    Dim vTabHdrAlb As Integer
    Dim vTabHdrBtm As Integer

    Dim vTabDtlTop As Integer
    Dim vTabDtlTit As Integer
    Dim vTabDtlBtm As Integer

    Dim hTabDtlQty As Integer
    Dim hTabDtlPvp As Integer
    Dim hTabDtlDto As Integer
    Dim hTabDtlAmt As Integer

    Private Const TEXT_LINE_LENGTH = 60


    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal ev As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim X As Integer
        Dim Y As Integer
        Dim tmpStr As String
        Dim FormatString As String
        Dim Base As Decimal
        Dim Total As Decimal

        FormatString = IIf(Year(Today) > 2001, "#,###.00", "#,###")
        DrawTemplate(ev, hTabLeft, 24)

        With ev.Graphics
            'DESTINATARI
            X = hTabLeft + StringOffset
            Y = vTabHdrTop + 3 * FontStd.Height
            .DrawString(_Delivery.Nom & vbCrLf & _Delivery.Address.toMultilineString(_Delivery.Customer.Lang), FontStd, Brushes.Black, X, Y)
            '.DrawString(_Delivery.Nom & vbCrLf & _Delivery.Address.Text & vbCrLf & _Delivery.Address.Zip.Location.Nom, FontStd, Brushes.Black, X, Y)

            'DATA
            tmpStr = Format(_Delivery.Fch, "dd/MM/yy")
            X = hTabHdrAdr + (hTabHdrAlb - hTabHdrAdr - .MeasureString(tmpStr, FontStd).Width()) / 2
            Y = vTabHdrTit + (vTabHdrAlb - vTabHdrTit - .MeasureString(tmpStr, FontStd).Height) / 2
            .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

            'ALBARA
            tmpStr = _Delivery.Id
            X = hTabHdrAlb + (hTabRight - hTabHdrAlb - .MeasureString(tmpStr, FontNum).Width()) / 2
            Y = vTabHdrTit + (vTabHdrAlb - vTabHdrTit - .MeasureString(tmpStr, FontNum).Height) / 2
            .DrawString(tmpStr, FontNum, Brushes.Black, X, Y)

            'OBSERVACIONS
            X = hTabHdrAdr + StringOffset
            Y = vTabHdrAlb + FontStd.Height '2 * FontStd.Height

            If _Delivery.Customer.SuProveedorNum > "" Then
                tmpStr = "Proveedor num.: " & _Delivery.Customer.SuProveedorNum
                .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
                Y = Y + FontStd.Height
            End If

            tmpStr = "bultos: " & _Delivery.Bultos
            .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
            Y = Y + FontStd.Height

            If _Delivery.PortsCod = DTOCustomer.PortsCodes.Reculliran Then
                tmpStr = "transporte: sus medios"
            Else
                tmpStr = "envio por transportista"
            End If
            .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

            Y = vTabDtlTit

            For Each oSpv As DTOSpv In _Delivery.Spvs
                'REPARACIO
                Y = Y + (2 * FontStd.Height)
                X = hTabLeft + StringOffset

                If oSpv.sRef > "" Then
                    tmpStr = "su referencia " & oSpv.sRef
                    .DrawString(tmpStr, FontItalic, Brushes.Black, X, Y)
                    Y = Y + FontStd.Height
                End If

                tmpStr = "reparación num." & oSpv.Id
                If oSpv.SpvIn IsNot Nothing Then
                    tmpStr = tmpStr & " recibida el " & Format(oSpv.SpvIn.Fch, "dd/MM/yy")
                End If
                .DrawString(tmpStr, FontItalic, Brushes.Black, X, Y)

                'ARTICLE / GARANTIA
                Y = Y + FontStd.Height
                X = hTabLeft + 2 * StringOffset


                tmpStr = oSpv.product.Nom
                If oSpv.Garantia = True Then
                    tmpStr += ", reparado en garantía."
                Else
                    tmpStr += ", reparado con cargo."
                End If
                .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)


                'OBSERVACIONS
                Y = Y + 2 * FontStd.Height
                Dim oSizeObs As SizeF = .MeasureString(oSpv.ObsTecnic, FontStd, hTabDtlQty - X)
                Dim oRcObs As New RectangleF(New PointF(X, Y), oSizeObs)
                .DrawString(oSpv.ObsTecnic, FontStd, Brushes.Black, oRcObs)
                Y += oSizeObs.Height
                'Y = Y + 2 * FontStd.Height

                'ITEMS
                Dim items As List(Of DTODeliveryItem) = _Delivery.Items

                For Each Itm As DTODeliveryItem In items
                    Y = Y + FontStd.Height

                    tmpStr = Itm.Sku.NomLlarg
                    X = hTabLeft + 3 * StringOffset
                    .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

                    If Itm.Qty <> 0 Then
                        tmpStr = Itm.Qty
                        X = hTabDtlPvp - StringOffset - .MeasureString(tmpStr, FontStd).Width
                        .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
                    End If

                    If Itm.Price IsNot Nothing And _Delivery.Valorado Then
                        tmpStr = Format(Itm.Price.Eur, FormatString)
                        X = hTabDtlAmt - StringOffset - .MeasureString(tmpStr, FontStd).Width
                        .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

                        tmpStr = Format(Itm.Import.Eur, FormatString)
                        X = hTabRight - StringOffset - .MeasureString(tmpStr, FontStd).Width
                        .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
                        Base = Base + Itm.Import.Eur
                    End If
                Next Itm

                If Base <> 0 And _Delivery.Valorado Then
                    tmpStr = Format(Base, FormatString)
                    Y = Y + 1.5 * (FontStd.Height)
                    X = hTabRight - StringOffset - .MeasureString(tmpStr, FontStd).Width
                    .DrawLine(Pens.Black, X, Y, hTabRight - StringOffset, Y)

                    'SUMA
                    Y = Y + 0.5 * (FontStd.Height)
                    X = hTabRight - StringOffset - .MeasureString(tmpStr, FontStd).Width
                    .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

                    X = hTabLeft + 3 * StringOffset
                    tmpStr = "suma"
                    .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

                End If
            Next

            Total = Base
            If Total <> 0 And _Delivery.Valorado Then

                If Not _Delivery.IvaExempt Then
                    'IVA
                    Dim DcIvaTipus As Decimal = 21 '==================================================================================== OJO, Generalitzar!!!!!
                    Dim DcIVA As Decimal = Math.Round(Base * DcIvaTipus / 100, 2, MidpointRounding.AwayFromZero)
                    tmpStr = Format(DcIVA, FormatString)
                    Y = Y + (FontStd.Height)
                    X = hTabRight - StringOffset - .MeasureString(tmpStr, FontStd).Width
                    .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

                    X = hTabLeft + 3 * StringOffset
                    tmpStr = "IVA " & DcIvaTipus & "%"
                    .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
                    Total = Total + DcIVA

                    If _Delivery.Customer.Req Then
                        'REQ
                        Dim DcReqTipus As Decimal = 5.2 '==================================================================================== OJO, Generalitzar!!!!!
                        Dim DcREQ As Decimal = Math.Round(Base * DcReqTipus / 100, 2, MidpointRounding.AwayFromZero)
                        tmpStr = Format(DcREQ, FormatString)
                        Y = Y + (FontStd.Height)
                        X = hTabRight - StringOffset - .MeasureString(tmpStr, FontStd).Width
                        .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

                        X = hTabLeft + 3 * StringOffset
                        tmpStr = "Recargo equivalencia " & DcReqTipus & "%"
                        .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
                        Total = Total + DcIVA
                    End If
                End If
            End If

            If Total <> 0 And _Delivery.Valorado Then
                tmpStr = Format(Total, FormatString)
                Y = Y + 1.5 * (FontStd.Height)
                X = hTabRight - StringOffset - .MeasureString(tmpStr, FontStd).Width
                .DrawLine(Pens.Black, X, Y, hTabRight - StringOffset, Y)

                'SUMA
                Y = Y + 0.5 * (FontStd.Height)
                X = hTabRight - StringOffset - .MeasureString(tmpStr, FontStd).Width
                .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

                X = hTabLeft + 3 * StringOffset
                tmpStr = "total"
                .DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

            End If

        End With
    End Sub



    Public Sub Print()
        With PrintDocument1
            '.DefaultPageSettings.PaperSize.PaperKind.A2()
            .Print()
        End With
        'PrintPreviewDialog1.ShowDialog()
    End Sub

    Public Sub PrintPreview()
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub DrawTemplate(ByVal ev As System.Drawing.Printing.PrintPageEventArgs, ByVal X As Integer, ByVal Y As Integer)
        Dim tmpStr As String

        FontColHdr = New Font("Arial", 8, FontStyle.Regular)
        FontNum = New Font("Arial", 14, FontStyle.Regular)
        FontStd = New Font("Arial", 10, FontStyle.Regular)
        FontItalic = New Font("Arial", 10, FontStyle.Italic)
        FontTit = New Font("Arial", 8, FontStyle.Regular)

        StringOffset = ev.Graphics.MeasureString("0", FontStd).Width

        hTabLeft = ev.MarginBounds.Left
        hTabRight = ev.MarginBounds.Right
        hTabHdrAdr = hTabLeft + ((hTabRight - hTabLeft) / 2)
        hTabHdrAlb = hTabHdrAdr + (hTabRight - hTabHdrAdr) / 2

        vTabLogo = 24
        vTabHdrTop = ev.MarginBounds.Top + 46
        vTabHdrTit = vTabHdrTop + FontColHdr.GetHeight(ev.Graphics)
        vTabHdrAlb = vTabHdrTit + 48
        vTabHdrBtm = vTabHdrTop + 156

        vTabDtlTop = vTabHdrBtm + 32
        vTabDtlTit = vTabDtlTop + FontColHdr.GetHeight(ev.Graphics)
        vTabDtlBtm = ev.MarginBounds.Bottom

        hTabDtlAmt = hTabRight - (7 * StringOffset)
        hTabDtlPvp = hTabDtlAmt - (6 * StringOffset)
        'hTabDtlPvp = hTabDtlDto - (5 * StringOffset)
        hTabDtlQty = hTabDtlPvp - (3 * StringOffset)

        DrawMembrete(ev, hTabLeft, vTabLogo)
        With ev.Graphics
            'header
            .DrawRectangle(Pens.Black, New Rectangle(hTabLeft, vTabHdrTop, hTabRight - hTabLeft, vTabHdrBtm - vTabHdrTop)) 'SPV nº
            .DrawLine(Pens.Black, hTabHdrAdr, vTabHdrTop, hTabHdrAdr, vTabHdrBtm)
            .DrawLine(Pens.Black, hTabHdrAlb, vTabHdrTop, hTabHdrAlb, vTabHdrAlb)
            .DrawLine(Pens.Black, hTabHdrAdr, vTabHdrTit, hTabRight, vTabHdrTit)
            .DrawLine(Pens.Black, hTabHdrAdr, vTabHdrAlb, hTabRight, vTabHdrAlb)
            tmpStr = "fecha"
            .DrawString(tmpStr, FontTit, Brushes.Black, hTabHdrAdr + (hTabHdrAlb - hTabHdrAdr - .MeasureString(tmpStr, FontTit).Width()) / 2, vTabHdrTop)
            tmpStr = "albarán"
            .DrawString(tmpStr, FontTit, Brushes.Black, hTabHdrAlb + (hTabRight - hTabHdrAlb - .MeasureString(tmpStr, FontTit).Width()) / 2, vTabHdrTop)

            'detail
            .DrawRectangle(Pens.Black, New Rectangle(hTabLeft, vTabDtlTop, hTabRight - hTabLeft, vTabDtlBtm - vTabDtlTop)) 'SPV nº
            .DrawLine(Pens.Black, hTabLeft, vTabDtlTit, hTabRight, vTabDtlTit)
            .DrawLine(Pens.Black, hTabDtlQty, vTabDtlTop, hTabDtlQty, vTabDtlBtm)
            .DrawLine(Pens.Black, hTabDtlPvp, vTabDtlTop, hTabDtlPvp, vTabDtlBtm)
            '.DrawLine(Pens.Black, hTabDtlDto, vTabDtlTop, hTabDtlDto, vTabDtlBtm)
            .DrawLine(Pens.Black, hTabDtlAmt, vTabDtlTop, hTabDtlAmt, vTabDtlBtm)
            tmpStr = "concepto"
            .DrawString(tmpStr, FontTit, Brushes.Black, hTabLeft + StringOffset, vTabDtlTop)
        End With
    End Sub

    Public Sub DrawMembrete(ByVal ev As System.Drawing.Printing.PrintPageEventArgs, ByVal X As Integer, ByVal Y As Integer)

        Dim oLogo As New Logo(70)
        oLogo.BackGroundStyle = Logo.BackGroundStyles.ratlles
        oLogo.DrawMe(ev.Graphics, X, Y)

        Dim tmpStr As String
        Dim FontNom As Font
        Dim FontAdr As Font
        Dim Gap As Integer

        tmpStr = "M"
        FontNom = New Font("Arial", 12, FontStyle.Bold)
        FontAdr = New Font("Arial", 10, FontStyle.Regular)
        Gap = 10
        With ev.Graphics
            X = X + oLogo.width + Gap
            .DrawString("MATIAS MASSO, S.A.", FontNom, Brushes.DarkGray, X, Y)
            Y = Y + FontNom.Height
            .DrawString("SERVICIO TECNICO OFICIAL BRITAX RÖMER Y 4MOMS", FontAdr, Brushes.DarkGray, X, Y)
            Y = Y + FontAdr.Height
            .DrawString("Bertrán 96, 08022 BARCELONA", FontAdr, Brushes.DarkGray, X, Y)
            Y = Y + FontAdr.Height
            .DrawString("www.matiasmasso.es", FontAdr, Brushes.DarkGray, X, Y)

        End With
    End Sub

   
End Class
