Imports System.Drawing.Printing
Imports FEB

Public Class Etiqueta
    Private mPd As PrintDocument
    Private mNom As String
    Private mAdr As String
    Private mCit As String
    Private mAlb As Integer
    Private mSpv As Integer
    Private mBultos As Integer
    Private mKg As Integer
    Private mM3 As Decimal

    Private Const ORGNOM As String = "MATIAS MASSO, S.A."
    Private Const ORGADR As String = "Diagonal 403 - 08008 Barcelona - tel.: 932.541.522"

    Public Sub New()
        mPd = New PrintDocument
        mPd.PrinterSettings.PrinterName = GetLabelPrinter()
        AddHandler mPd.PrintPage, AddressOf PrintPage
    End Sub

    Public WriteOnly Property Nom() As String
        Set(ByVal value As String)
            mNom = value
        End Set
    End Property

    Public WriteOnly Property Adr() As String
        Set(ByVal value As String)
            mAdr = value
        End Set
    End Property

    Public WriteOnly Property Cit() As String
        Set(ByVal value As String)
            mCit = value
        End Set
    End Property

    Public WriteOnly Property Alb() As Integer
        Set(ByVal value As Integer)
            mAlb = value
        End Set
    End Property

    Public WriteOnly Property Spv() As Integer
        Set(ByVal value As Integer)
            mSpv = value
        End Set
    End Property

    Public WriteOnly Property Bultos() As Integer
        Set(ByVal value As Integer)
            mBultos = value
        End Set
    End Property

    Public WriteOnly Property Kg() As Integer
        Set(ByVal value As Integer)
            mKg = value
        End Set
    End Property

    Public WriteOnly Property M3() As Decimal
        Set(ByVal value As Decimal)
            mM3 = value
        End Set
    End Property


    Public Function Print() As Boolean
        mPd.Print()
        Return True
    End Function

    Private Sub PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        PrintForm(1, e)
        PrintForm(2, e)
    End Sub

    Private Sub PrintForm(copy As Integer, ByVal e As PrintPageEventArgs)

        Dim copyGap = 550 'distancia entre les dues copies

        Dim leftMargin = 20
        Dim topMargin = (copy - 1) * copyGap + 20
        Dim overallWidth = 800
        Dim overallHeight = 500

        Dim oPenV As New Pen(Color.Gray, 1)
        Dim oPenH As New Pen(Color.Gray, 2)

        'marges
        e.Graphics.DrawLine(oPenH, leftMargin, topMargin, leftMargin + overallWidth, topMargin)
        e.Graphics.DrawLine(oPenH, leftMargin, topMargin + overallHeight, leftMargin + overallWidth, topMargin + overallHeight)
        e.Graphics.DrawLine(oPenV, leftMargin, topMargin, leftMargin, topMargin + overallHeight)
        e.Graphics.DrawLine(oPenV, leftMargin + overallWidth, topMargin, leftMargin + overallWidth, topMargin + overallHeight)

        'horizontal lines
        Dim headerBottom = topMargin + 86
        Dim detailsTop = topMargin + overallHeight - 100
        Dim detailsBottom = detailsTop + 30
        e.Graphics.DrawLine(oPenH, leftMargin, headerBottom, leftMargin + overallWidth, headerBottom)
        e.Graphics.DrawLine(oPenH, leftMargin, detailsTop, leftMargin + overallWidth, detailsTop)
        e.Graphics.DrawLine(oPenH, leftMargin, detailsBottom, leftMargin + overallWidth, detailsBottom)

        'detail vertical lines
        Dim gap As Single = overallWidth / 5
        e.Graphics.DrawLine(oPenV, leftMargin + 1 * gap, detailsTop, leftMargin + 1 * gap, topMargin + overallHeight)
        e.Graphics.DrawLine(oPenV, leftMargin + 2 * gap, detailsTop, leftMargin + 2 * gap, topMargin + overallHeight)
        e.Graphics.DrawLine(oPenV, leftMargin + 3 * gap, detailsTop, leftMargin + 3 * gap, topMargin + overallHeight)
        e.Graphics.DrawLine(oPenV, leftMargin + 4 * gap, detailsTop, leftMargin + 4 * gap, topMargin + overallHeight)

        'Header
        Dim X As Integer = leftMargin + 20
        Dim Y As Integer = topMargin + 20

        Dim S As String = ORGNOM
        Dim oFont As New Font("System", 14, FontStyle.Bold)
        Dim oBrush As New SolidBrush(Color.Black)
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        oFont = New Font("System", 12, FontStyle.Bold)
        S = ORGADR
        Y += oFont.Height + 10
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        oFont = New Font("System", 12, FontStyle.Bold)
        Dim iLen As Integer

        Y = detailsTop + (detailsBottom - detailsTop - oFont.Height) / 2

        S = "reparación"
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = leftMargin + 0 * gap + (gap - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        S = "albarán"
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = leftMargin + 1 * gap + (gap - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        S = "bultos"
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = leftMargin + 2 * gap + (gap - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        S = "peso"
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = leftMargin + 3 * gap + (gap - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        S = "volumen"
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = leftMargin + 4 * gap + (gap - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)


        'datos
        oFont = New Font("System", 20, FontStyle.Bold)

        Dim iMaxLen As Integer = GetAdrLen(20, oFont, e.Graphics)
        If iMaxLen > overallWidth Then iMaxLen = GetAdrLen(16, oFont, e.Graphics)
        If iMaxLen > overallWidth Then iMaxLen = GetAdrLen(12, oFont, e.Graphics)

        X = leftMargin + (overallWidth - iMaxLen) / 2
        If X < 20 Then X = 20

        'destination name
        Y = headerBottom + 80
        S = mNom
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)
        Y += oFont.Height * 1.2
        S = mAdr
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)
        Y += oFont.Height * 1.2
        S = mCit
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)


        oFont = New Font("System", 14, FontStyle.Bold)

        Y = detailsBottom + 20

        S = mSpv.ToString
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = leftMargin + 0 * gap + (gap - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        S = mAlb.ToString
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = leftMargin + 1 * gap + (gap - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        S = mBultos.ToString
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = leftMargin + 2 * gap + (gap - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        If mKg > 0 Then
            S = mKg.ToString & " Kg"
            iLen = e.Graphics.MeasureString(S, oFont).Width
            X = leftMargin + 3 * gap + (gap - iLen) / 2
            e.Graphics.DrawString(S, oFont, oBrush, X, Y)
        End If

        If mM3 > 0 Then
            S = mM3 & " m3"
            iLen = e.Graphics.MeasureString(S, oFont).Width
            X = leftMargin + 4 * gap + (gap - iLen) / 2
            e.Graphics.DrawString(S, oFont, oBrush, X, Y)
        End If
        'e.PageSettings.PrinterSettings.PrintRange
    End Sub


    Private Function GetAdrLen(ByVal iFontHeight As Integer, ByRef oFont As Font, ByVal eV As Graphics) As Integer
        oFont = New Font("System", iFontHeight, FontStyle.Bold)
        Dim iLenNom As Integer = eV.MeasureString(mNom, oFont).Width
        Dim iLenAdr As Integer = eV.MeasureString(mAdr, oFont).Width
        Dim iLenCit As Integer = eV.MeasureString(mCit, oFont).Width

        Dim iMaxLen As Integer = iLenNom
        If iLenAdr > iMaxLen Then iMaxLen = iLenAdr
        If iLenCit > iMaxLen Then iMaxLen = iLenCit
        Return iMaxLen
    End Function

    Private Function GetLabelPrinter() As String
        Dim sLabelPrinter As String = GetSetting("MatSoft", "SpvCli", "LabelPrinter")

        For Each Impresoras In PrinterSettings.InstalledPrinters
            If Impresoras.ToString = sLabelPrinter Then
                Return sLabelPrinter
                Exit Function
            End If
        Next

        MsgBox("impresora de etiquetes no instalada" & vbCrLf & "seleccioneu una impresora en la propera finestra", MsgBoxStyle.Exclamation, "MATIAS MASSO, S.A.")
        Dim oFrm As New Frm_LabelPrinterNotFound
        oFrm.ShowDialog()
        Return GetSetting("MatSoft", "SpvCli", "LabelPrinter")
    End Function
End Class
