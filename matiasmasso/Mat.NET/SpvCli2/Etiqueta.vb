Imports System.Drawing.Printing

Public Class Etiqueta
    Private mPd As PrintDocument
    Private mNom As String
    Private mAdr As String
    Private mCit As String
    Private mAlb As Integer
    Private mBultos As Integer
    Private mKg As Integer
    Private mM3 As Decimal

    Private Const ORGNOM As String = "MATIAS MASSO, S.A."
    Private Const ORGADR As String = "Bertran, 96 - 08023 Barcelona - tel.: 932.541.522"

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
        Dim X As Integer = 10
        Dim Y As Integer = 0
        Dim iWidth As Integer = 360
        Dim iHeight As Integer = 255
        Dim oPenV As New Pen(Color.Black, 1)
        Dim oPenH As New Pen(Color.Black, 2)

        e.Graphics.DrawLine(oPenH, X, Y, X + iWidth, Y)
        e.Graphics.DrawLine(oPenH, X, Y + iHeight, X + iWidth, Y + iHeight)
        e.Graphics.DrawLine(oPenV, X, Y, X, Y + iHeight)
        e.Graphics.DrawLine(oPenV, X + iWidth, Y, X + iWidth, Y + iHeight)

        e.Graphics.DrawLine(oPenH, X, Y + 43, X + iWidth, Y + 43)
        e.Graphics.DrawLine(oPenH, X, Y + 200, X + iWidth, Y + 200)
        e.Graphics.DrawLine(oPenH, X, Y + 220, X + iWidth, Y + 220)

        e.Graphics.DrawLine(oPenV, X + 90, Y + 200, X + 90, Y + 260)
        e.Graphics.DrawLine(oPenV, X + 180, Y + 200, X + 180, Y + 260)
        e.Graphics.DrawLine(oPenV, X + 270, Y + 200, X + 270, Y + 260)

        X = 20
        Y += 6
        Dim S As String = ORGNOM
        Dim oFont As New Font("System", 12, FontStyle.Bold)
        Dim oBrush As New SolidBrush(Color.Black)
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        Y += 4
        oFont = New Font("System", 10, FontStyle.Bold)
        S = ORGADR
        Y += oFont.Height
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        oFont = New Font("System", 10, FontStyle.Bold)
        Dim iLen As Integer
        Y = 200

        S = "albarán"
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = 10 + (90 - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        S = "bultos"
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = 10 + 90 + (90 - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        S = "peso"
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = 10 + 180 + (90 - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        S = "volumen"
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = 10 + 270 + (90 - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)


        'datos
        oFont = New Font("System", 12, FontStyle.Bold)

        Dim iMaxLen As Integer = GetAdrLen(12, oFont, e.Graphics)
        If iMaxLen > 360 Then iMaxLen = GetAdrLen(11, oFont, e.Graphics)
        If iMaxLen > 360 Then iMaxLen = GetAdrLen(10, oFont, e.Graphics)

        X = (360 - iMaxLen) / 2
        If X < 20 Then X = 20

        Y = 80
        S = mNom
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)
        Y += 20
        S = mAdr
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)
        Y += 20
        S = mCit
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        Y = 230

        S = mAlb.ToString
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = 10 + (90 - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        S = mBultos.ToString
        iLen = e.Graphics.MeasureString(S, oFont).Width
        X = 10 + 90 + (90 - iLen) / 2
        e.Graphics.DrawString(S, oFont, oBrush, X, Y)

        If mKg > 0 Then
            S = mKg.ToString & " Kg"
            iLen = e.Graphics.MeasureString(S, oFont).Width
            X = 10 + 180 + (90 - iLen) / 2
            e.Graphics.DrawString(S, oFont, oBrush, X, Y)
        End If

        If mM3 > 0 Then
            S = mM3 & " m3"
            iLen = e.Graphics.MeasureString(S, oFont).Width
            X = 10 + 270 + (90 - iLen) / 2
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
