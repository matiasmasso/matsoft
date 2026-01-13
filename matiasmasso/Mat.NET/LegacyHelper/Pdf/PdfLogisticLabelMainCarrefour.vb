Imports DTO.Integracions

Public Class PdfLogisticLabelMainCarrefour
    Private _item As DTOCarrefourItem
    Private _Pdf As _PdfBase

    Private _DataFont = New Font("Helvetica", 12, FontStyle.Bold)
    Private _TitleFont = New Font("Arial", 8, FontStyle.Regular)

    Private _DinA6WidthMm As Integer = 105 '298 points
    Private _DinA6HeightMm As Integer = 148 '420 ponts
    Private _DinA6WidthPoints As Integer = 298
    Private _DinA6HeightPoints As Integer = 420
    Private _Factor As Decimal = _DinA6HeightPoints / _DinA6HeightMm '2.84 pts/mm

    Private _LabelWidth As Integer = 102 * _Factor
    Private _LabelHeight As Integer = 140 * _Factor

    Private _HRowHeader As Integer = 16 * _Factor
    Private _HRowDsc As Integer = _HRowHeader + 44 * _Factor
    Private _HRowMid As Integer = _HRowDsc + 16 * _Factor

    'invaim la casella de talles per fer el codi de barres mes alt a veure si així el validen
    Private _HRowSizes As Integer = _HRowMid
    Private _HRowBarcode As Integer = _HRowSizes + (32 + 16) * _Factor

    Private _WCellLogo As Integer = 23 * _Factor
    Private _WCellSupplierCode As Integer = _WCellLogo + 23 * _Factor
    Private _WCellSection As Integer = _WCellSupplierCode + 13 * _Factor
    Private _WCellImplantation As Integer = _WCellSection + 19 * _Factor
    Private _WCellMadeIn As Integer = _WCellImplantation + 22 * _Factor

    Private _WCellRef As Integer = 33 * _Factor
    Private _WCellColor As Integer = _WCellRef + 33 * _Factor
    Private _WCellMasterBox As Integer = _WCellColor + 17 * _Factor
    Private _WCellInnerBox As Integer = _WCellMasterBox + 17 * _Factor

    Private _WCellDimensions As Integer = 60 * _Factor
    Private _WCellWeight As Integer = _WCellDimensions + 23 * _Factor
    Private _WCellQC As Integer = _WCellWeight + 17 * _Factor

    Public Sub New(item As DTOCarrefourItem)
        MyBase.New
        _item = item
    End Sub

    Public Sub DrawLabel(oPdf As _PdfBase)
        _Pdf = oPdf
        SetMargins()
        DrawTemplate()
        DrawDades()
    End Sub

    Private Sub SetMargins()
        Dim LeftMargin As Integer = (_DinA6WidthPoints - _LabelWidth) / 2
        Dim TopMargin As Integer = (_DinA6HeightPoints - _LabelHeight) / 2
        Dim BottomMargin As Integer = _DinA6HeightPoints - _LabelHeight - TopMargin
        Dim RightMargin As Integer = _DinA6WidthPoints - _LabelWidth - LeftMargin
        _Pdf.SetMargins(TopMargin, LeftMargin, RightMargin, BottomMargin)
    End Sub

    Private Sub DrawTemplate()

        'Graphics.PageUnit you get in your PrintPage event handler is GraphicsUnit.Display 100 pixels/inch 
        '(1 pixel = 0,254mm / 1mm= 3.937*0.75=2.95 px )

        _Pdf.Font = New System.Drawing.Font("Helvetica", 8)
        _Pdf.X = 0
        _Pdf.Y = 0

        'codi de barres
        If Not String.IsNullOrEmpty(_item.MasterBarCode) Then
            DrawBarcode()
        End If

        _Pdf.Pen.Width = 1
        _Pdf.DrawPageRectangle()


        'linies horitzontals
        _Pdf.DrawRectangle(0, _HRowHeader, _LabelWidth, 0)
        _Pdf.DrawRectangle(0, _HRowDsc, _LabelWidth, 0)
        _Pdf.DrawRectangle(0, _HRowMid, _LabelWidth, 0)
        _Pdf.DrawRectangle(0, _HRowSizes, _LabelWidth, 0)
        _Pdf.DrawRectangle(0, _HRowBarcode, _LabelWidth, 0)

        'linies verticals
        Dim rowHeight As Integer = _HRowHeader
        _Pdf.DrawRectangle(_WCellLogo, 0, 0, rowHeight)
        _Pdf.DrawRectangle(_WCellSupplierCode, 0, 0, rowHeight)
        _Pdf.DrawRectangle(_WCellSection, 0, 0, rowHeight)
        _Pdf.DrawRectangle(_WCellImplantation, 0, 0, rowHeight)

        rowHeight = _HRowMid - _HRowDsc
        _Pdf.DrawRectangle(_WCellRef, _HRowDsc, 0, rowHeight)
        _Pdf.DrawRectangle(_WCellColor, _HRowDsc, 0, rowHeight)
        _Pdf.DrawRectangle(_WCellMasterBox, _HRowDsc, 0, rowHeight)

        rowHeight = _LabelHeight - _HRowBarcode - 2 * _Pdf.Pen.Width
        _Pdf.DrawRectangle(_WCellDimensions, _HRowBarcode, 0, rowHeight)
        _Pdf.DrawRectangle(_WCellWeight, _HRowBarcode, 0, rowHeight)

        'Logo
        Dim oImg = My.Resources.Carrefour ' Image.FromFile(String.Format("{0}\carrefour.png", My.Computer.FileSystem.SpecialDirectories.MyDocuments))
        Dim wImgPadding As Integer = 16
        Dim hImgPadding As Integer = 16
        Dim rc As New Rectangle(wImgPadding / 2 + _Pdf.marginLeft, hImgPadding / 2 + _Pdf.marginTop, _WCellLogo - wImgPadding, _HRowHeader - hImgPadding)
        _Pdf.DrawImage(oImg, rc)


        _Pdf.Font = _TitleFont
        _Pdf.Brush = New SolidBrush(Color.Gray)

        Dim titleTopPadding As Integer = 5
        _Pdf.Y = titleTopPadding
        _Pdf.X = _WCellLogo + (_WCellSupplierCode - _WCellLogo) / 2
        _Pdf.DrawCenteredString("Supplier Code")
        _Pdf.X = _WCellSupplierCode + (_WCellSection - _WCellSupplierCode) / 2
        _Pdf.DrawCenteredString("Section")
        _Pdf.X = _WCellSection + (_WCellImplantation - _WCellSection) / 2
        _Pdf.DrawCenteredString("Implantation")
        _Pdf.X = _WCellImplantation + (_WCellMadeIn - _WCellImplantation) / 2
        _Pdf.DrawCenteredString("Made in")

        _Pdf.Y = _HRowDsc + titleTopPadding
        _Pdf.X = _WCellRef / 2
        _Pdf.DrawCenteredString("Reference")
        _Pdf.X = _WCellRef + (_WCellColor - _WCellRef) / 2
        _Pdf.DrawCenteredString("Color")
        _Pdf.X = _WCellColor + (_WCellMasterBox - _WCellColor) / 2
        _Pdf.DrawCenteredString("Units/PCB")
        _Pdf.X = _WCellMasterBox + (_WCellInnerBox - _WCellMasterBox) / 2
        _Pdf.DrawCenteredString("SPCB")

        _Pdf.Y = _HRowBarcode + titleTopPadding
        _Pdf.X = _WCellDimensions / 2
        _Pdf.DrawCenteredString("Box Dimension L x W x H")
        _Pdf.X = _WCellDimensions + (_WCellWeight - _WCellDimensions) / 2
        _Pdf.DrawCenteredString("Gross Weight")


    End Sub

    Private Sub DrawDades()
        _Pdf.Font = _DataFont
        _Pdf.Brush = New SolidBrush(Color.Navy)

        Dim dataTopPadding As Integer = 20

        'Dades

        _Pdf.Brush = New SolidBrush(Color.Black)
        _Pdf.Y = dataTopPadding
        _Pdf.X = _WCellLogo + (_WCellSupplierCode - _WCellLogo) / 2
        _Pdf.DrawCenteredString(_item.SupplierCode)
        _Pdf.X = _WCellSupplierCode + (_WCellSection - _WCellSupplierCode) / 2
        _Pdf.DrawCenteredString(_item.Section)
        _Pdf.X = _WCellSection + (_WCellImplantation - _WCellSection) / 2
        _Pdf.DrawCenteredString(_item.Implantation)
        _Pdf.X = _WCellImplantation + (_WCellMadeIn - _WCellImplantation) / 2
        _Pdf.DrawCenteredString(_item.MadeIn)

        _Pdf.Y = _HRowHeader + (_HRowDsc - _HRowHeader - _Pdf.Font.Height) / 2
        _Pdf.X = _LabelWidth / 2
        _Pdf.DrawCenteredString(_item.SkuDsc)

        _Pdf.Y = _HRowDsc + dataTopPadding
        _Pdf.X = _WCellRef / 2
        _Pdf.DrawCenteredString(_item.SkuCustomRef)
        _Pdf.X = _WCellRef + (_WCellColor - _WCellRef) / 2
        _Pdf.DrawCenteredString(_item.SkuColor)
        _Pdf.X = _WCellColor + (_WCellMasterBox - _WCellColor) / 2
        _Pdf.DrawCenteredString(_item.UnitsPerMasterBox)
        _Pdf.X = _WCellMasterBox + (_WCellInnerBox - _WCellMasterBox) / 2
        _Pdf.DrawCenteredString(_item.UnitsPerInnerBox)




        _Pdf.Y = _HRowBarcode + dataTopPadding
        _Pdf.X = _WCellDimensions / 2

        _Pdf.DrawCenteredString(_item.Dimensions)
        _Pdf.X = _WCellDimensions + (_WCellWeight - _WCellDimensions) / 2
        _Pdf.DrawCenteredString(String.Format("{0} Kg", _item.Weight))


    End Sub

    Private Sub DrawBarcode()
        Dim barcodeHorizontalPadding As Integer = 0 ' 1
        Dim barcodeLabelHeight As Integer = 20

        'Dim oBarcode As New BarcodeLib.Barcode()
        'oBarcode.BarWidth = 3
        Dim ImgWidth As Integer = _LabelWidth - 2 * barcodeHorizontalPadding
        Dim imgHeight As Integer = _HRowBarcode - _HRowSizes - barcodeLabelHeight
        'Dim oBarcodeImg As Image = oBarcode.Encode(BarcodeLib.TYPE.ITF14, _item.MasterBarCode, ImgWidth, imgHeight)
        Dim oBarcodeImg As Image = MatHelper.BarcodeHelper.EAN13Image(_item.MasterBarCode, ImgWidth, imgHeight, IronBarCode.BarcodeWriterEncoding.ITF)


        _Pdf.X = _Pdf.marginLeft + (_LabelWidth - oBarcodeImg.Width) / 2
        _Pdf.Y = _Pdf.marginTop + _HRowSizes
        Dim rc As New Rectangle(_Pdf.X, _Pdf.Y, oBarcodeImg.Width, oBarcodeImg.Height)
        _Pdf.DrawImage(oBarcodeImg, rc)

        _Pdf.Font = _DataFont
        Dim iWidth As Integer = _Pdf.Pdf.MeasureString(_item.MasterBarCode, _Pdf.Font).Width
        _Pdf.Y += oBarcodeImg.Height - 10
        _Pdf.X = _Pdf.marginLeft + (_LabelWidth - iWidth) / 2
        _Pdf.DrawString(_item.MasterBarCode)
    End Sub


End Class
