Imports DTO.Integracions

Public Class PdfLogisticLabelSideCarrefour
    Private _item As DTOCarrefourItem
    Private _Pdf As _PdfBase

    Private _DinA6WidthMm As Integer = 105 '298 points
    Private _DinA6HeightMm As Integer = 148 '420 ponts
    Private _DinA6WidthPoints As Integer = 298
    Private _DinA6HeightPoints As Integer = 420
    Private _Factor As Decimal = _DinA6HeightPoints / _DinA6HeightMm

    Private _LabelWidth As Integer = 100 * _Factor
    Private _LabelHeight As Integer = 140 * _Factor

    Private _FrameTop As Integer = 0
    Private _FrameHeight As Integer = 147 ' 130
    Private _TextHeight As Integer = 40
    Private _ImgHeight As Integer = 110 ' 94
    Private _Padding As Integer = 0


    Public Sub New(item As DTOCarrefourItem)
        MyBase.New
        _item = item
    End Sub

    Public Sub DrawLabel(oPdf As _PdfBase)
        _Pdf = oPdf
        SetMargins()
        DrawLabel()
        drawDeliveryRef()
    End Sub

    Private Sub SetMargins()
        Dim LeftMargin As Integer = (_DinA6WidthPoints - _LabelWidth) / 2
        Dim TopMargin As Integer = (_DinA6HeightPoints - _LabelHeight) / 2
        Dim BottomMargin As Integer = _DinA6HeightPoints - _LabelHeight - TopMargin
        Dim RightMargin As Integer = _DinA6WidthPoints - _LabelWidth - LeftMargin
        _Pdf.SetMargins(TopMargin, LeftMargin, RightMargin, BottomMargin)
    End Sub

    Private Sub DrawLabel()
        'outer frame
        _Pdf.DrawRectangle(0, _FrameTop, _LabelWidth, _FrameHeight)

        'bar code digits
        _Pdf.Y = _FrameTop
        _Pdf.X = _LabelWidth / 2
        _Pdf.Font = New Font("Helvetica", 32, FontStyle.Bold)
        _Pdf.DrawCenteredString(_item.MasterBarCode)

        'horizontal line
        _Pdf.DrawRectangle(0, _FrameTop + _TextHeight, _LabelWidth, 0)

        'bar code image
        If Not String.IsNullOrEmpty(_item.MasterBarCode) Then
            DrawBarcodeImage()
        End If


    End Sub

    Private Sub DrawBarcodeImage()

        'Dim oBarcodeImg As Image = Barcodes.ITF14.Image(_item.MasterBarCode, _LabelWidth - 2 * _Padding, _ImgHeight - _Padding)

        'Dim oBarcode As New BarcodeLib.Barcode()
        'oBarcode.BarWidth = 3
        Dim ImgWidth As Integer = _LabelWidth - 2 * _Padding
        Dim imgHeight As Integer = _ImgHeight - _Padding
        'Dim oBarcodeImg As Image = oBarcode.Encode(BarcodeLib.TYPE.ITF14, _item.MasterBarCode, ImgWidth, imgHeight)
        Dim oBarcodeImg As Image = MatHelper.BarcodeHelper.EAN13Image(_item.MasterBarCode, ImgWidth, imgHeight, IronBarCode.BarcodeWriterEncoding.ITF)

        _Pdf.X = _Pdf.marginLeft + (_LabelWidth - oBarcodeImg.Width) / 2
        _Pdf.Y = _FrameTop + _TextHeight + _Padding + 9
        Dim rc As New Rectangle(_Pdf.X, _Pdf.Y, oBarcodeImg.Width, oBarcodeImg.Height)
        _Pdf.DrawImage(oBarcodeImg, rc)
    End Sub

    Private Sub drawDeliveryRef()
        _Pdf.Font = New Font("Helvetica", 12, FontStyle.Bold)
        Dim deliveryTop As Integer = 150
        _Pdf.Y = deliveryTop
        _Pdf.X = _LabelWidth / 2
        _Pdf.DrawCenteredString(String.Format("MATIAS MASSO, S.A.  -  Albarán: {0}  Linea: {1}", _item.Albaran, _item.Linea))
    End Sub

End Class
