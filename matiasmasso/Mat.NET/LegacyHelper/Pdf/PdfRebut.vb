Public Class PdfRebut
    Inherits C1PdfHelper.Document

    Private _Rebut As DTORebut

    Private mRcPage As RectangleF
    Private mRcRebut As New RectangleF

    Private mLeftMargin As Integer
    Private mTopMargin As Integer
    Private mEtqWidth As Integer
    Private mEtqHeight As Integer
    Private mBottomMargin As Integer
    Private mRightMargin As Integer
    Private mFont As New Font("Helvetica", 10, FontStyle.Regular)
    Private mFontEpg As New Font("Helvetica", 8, FontStyle.Regular)

    Private mBrush As SolidBrush = Brushes.Black

    Public Sub New(ByVal oRebut As DTORebut)
        MyBase.New()
        _Rebut = oRebut
    End Sub

    Public Overloads Function Stream(exs As List(Of Exception)) As Byte()
        DrawTemplate()
        Return MyBase.Stream(exs)
    End Function

    Private Sub DrawTemplate()
        mRcPage = MyBase.PageRectangle
        mRcRebut = New RectangleF(mRcPage.Left, mRcPage.Top, mRcPage.Right - mRcPage.Left, (mRcPage.Bottom - mRcPage.Top) / 3)
        MyBase.DrawRectangle(Pens.Black, mRcRebut)


        'capçalera
        Dim LeftMargin As Integer = mRcRebut.Left + 150
        Dim RightMargin As Integer = mRcRebut.Right - 50
        Dim CellWidth As Integer = 100
        Dim CellHeight As Integer = mFontEpg.Height + mFont.Height

        Dim oRcHeader As New Rectangle(LeftMargin, 10, RightMargin - LeftMargin, CellHeight * 2)
        MyBase.DrawRectangle(Pens.Black, oRcHeader)

        Dim iMidHeader As Integer = oRcHeader.Left + (oRcHeader.Right - oRcHeader.Left) / 2
        Dim YHeaderEpgs As Integer = oRcHeader.Y + 1

        If _Rebut.Lang Is Nothing Then _Rebut.Lang = DTOLang.Default
        Dim s As String = _Rebut.Lang.Tradueix("recibo nº", "rebut nº", "receipt #")
        DrawEpg(s, oRcHeader.X + 2, YHeaderEpgs)

        s = _Rebut.Lang.Tradueix("localidad de expedición", "localitat d'expedició", "emission")
        DrawEpg(s, oRcHeader.X + CellWidth + 2, YHeaderEpgs)

        s = _Rebut.Lang.Tradueix("importe", "import", "Amt")
        DrawEpg(s, oRcHeader.Right - CellWidth + 2, YHeaderEpgs)

        Dim YHeaderValues As Integer = YHeaderEpgs + mFontEpg.Height
        s = _Rebut.Id
        Dim iLen As Integer = MyBase.MeasureString(s, mFont).Width
        DrawVal(s, oRcHeader.X + CellWidth - iLen - 2, YHeaderValues)

        s = "BARCELONA"
        iLen = MyBase.MeasureString(s, mFont).Width
        DrawVal(s, iMidHeader - iLen / 2, YHeaderValues)

        s = DTOAmt.CurFormatted(_Rebut.Amt)
        iLen = MyBase.MeasureString(s, mFont).Width
        DrawVal(s, oRcHeader.Right - iLen - 2, YHeaderValues)

        MyBase.DrawLine(Pens.DimGray, oRcHeader.Left, oRcHeader.Top + CellHeight, oRcHeader.Right, oRcHeader.Top + CellHeight)
        MyBase.DrawLine(Pens.DimGray, oRcHeader.Left + CellWidth, oRcHeader.Top, oRcHeader.Left + CellWidth, oRcHeader.Top + CellHeight)
        MyBase.DrawLine(Pens.DimGray, oRcHeader.Right - CellWidth, oRcHeader.Top, oRcHeader.Right - CellWidth, oRcHeader.Top + CellHeight)
        MyBase.DrawLine(Pens.DimGray, iMidHeader, oRcHeader.Top + CellHeight, iMidHeader, oRcHeader.Top + CellHeight + CellHeight)

        YHeaderEpgs = oRcHeader.Y + CellHeight + 1
        s = _Rebut.Lang.Tradueix("fecha de expedición", "data d'expedició", "emission date")
        DrawEpg(s, oRcHeader.X + 2, YHeaderEpgs)

        s = _Rebut.Lang.Tradueix("vencimiento", "venciment", "due date")
        DrawEpg(s, iMidHeader + 2, YHeaderEpgs)

        YHeaderValues = YHeaderEpgs + mFontEpg.Height

        s = DTO.GlobalVariables.Today().ToShortDateString
        iLen = MyBase.MeasureString(s, mFont).Width
        DrawVal(s, iMidHeader - iLen - 2, YHeaderValues)

        s = _Rebut.Vto.ToShortDateString
        iLen = MyBase.MeasureString(s, mFont).Width
        DrawVal(s, oRcHeader.Right - iLen - 2, YHeaderValues)

        If _Rebut.Concepte > "" Then
            YHeaderValues += 2 * mFont.Height
            s = _Rebut.Lang.Tradueix("Concepto: ", "Concepte: ", "Concept: ")
            s = s & _Rebut.Concepte
            DrawVal(s, oRcHeader.Left, YHeaderValues)
        End If

        If _Rebut.IbanDigits > "" Then
            YHeaderValues += 2 * mFont.Height
            s = _Rebut.Lang.Tradueix("Domiciliación bancaria: ", "Domiciliació bancaria: ", "Bank account: ")
            s = s & DTOIban.Formated(_Rebut.IbanDigits)
            DrawVal(s, oRcHeader.Left, YHeaderValues)
        End If

        'dades client
        Dim Y As Integer = 140
        Dim X As Integer = oRcHeader.Left
        MyBase.DrawRectangle(Pens.DimGray, X, Y, 300, 3 * mFont.Height + 5)
        Y += 5
        X += 5

        DrawVal(_Rebut.Nom, X, Y)
        Y += mFont.Height
        DrawVal(_Rebut.Adr, X, Y)
        Y += mFont.Height
        DrawVal(_Rebut.Cit, X, Y)

        'Horitzontal per mecanitzat
        Dim iHeight As Integer = 50
        MyBase.DrawLine(Pens.DimGray, mRcRebut.Left, mRcRebut.Bottom - iHeight, mRcRebut.Right, mRcRebut.Bottom - iHeight)

        'logo
        X = 50
        Dim oFormatVertical As New StringFormat(StringFormatFlags.DirectionVertical)
        Dim oFont As New Font("Helvetica", 18, FontStyle.Bold)
        MyBase.DrawString("MATIAS MASSO, S.A.", oFont, Brushes.DarkGray, New RectangleF(X, oRcHeader.Top, oFont.Height, 190), oFormatVertical)
        X += oFont.Height * 0.8
        oFont = New Font("Helvetica", 10, FontStyle.Regular)
        MyBase.DrawString("Diagonal 403 - 08008 BARCELONA", oFont, Brushes.DarkGray, New RectangleF(X, oRcHeader.Top, oFont.Height, 190), oFormatVertical)
        X += oFont.Height * 0.8
        MyBase.DrawString("Tel.: 932.541.522 - fax 932.541.521", oFont, Brushes.DarkGray, New RectangleF(X, oRcHeader.Top, oFont.Height, 190), oFormatVertical)
        X += oFont.Height * 0.8
        MyBase.DrawString("info@matiasmasso.es", oFont, Brushes.DarkGray, New RectangleF(X, oRcHeader.Top, oFont.Height, 190), oFormatVertical)

    End Sub

    Private Sub DrawEpg(ByVal Source As String, ByVal iX As Integer, ByVal iY As Integer)
        MyBase.DrawString(Source, mFontEpg, Brushes.DimGray, New RectangleF(iX, iY, MyBase.MeasureString(Source, mFontEpg).Width, mFontEpg.Height))
    End Sub

    Private Sub DrawVal(ByVal Source As String, ByVal iX As Integer, ByVal iY As Integer)
        MyBase.DrawString(Source, mFont, Brushes.Black, New RectangleF(iX, iY, MyBase.MeasureString(Source, mFont).Width, mFont.Height))
    End Sub


End Class
