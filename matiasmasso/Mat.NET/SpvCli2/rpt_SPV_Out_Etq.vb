Public Class rpt_SPV_Out_Etq
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(rpt_SPV_Out_Etq))
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
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Location = New System.Drawing.Point(66, 87)
        Me.PrintPreviewDialog1.MaximumSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Opacity = 1
        Me.PrintPreviewDialog1.TransparencyKey = System.Drawing.Color.Empty
        Me.PrintPreviewDialog1.Visible = False
        '
        'rpt_SPV_Out_Etq
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Name = "rpt_SPV_Out_Etq"
        Me.Text = "rpt_SPV_Out_Etq"

    End Sub

#End Region
    Private mRow As DataRow
    Private mev As System.Drawing.Printing.PrintPageEventArgs
    Private FontStd As New Font("Arial", 10, FontStyle.Bold)
    Private FontBig As New Font("Impact", 24, FontStyle.Regular)
    Private BrushStd As Brush = Brushes.Black
    Private X As Integer
    Private Y As Integer

    Public WriteOnly Property DataRow() As DataRow
        Set(ByVal Value As DataRow)
            mRow = Value
        End Set
    End Property

    Public Sub Print()
        With PrintDocument1
            .DefaultPageSettings.Landscape = True
            .PrinterSettings.PrinterName = "ZEBRA S400"
            .Print()
        End With
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal ev As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        mev = ev
        PrintRemitente()
        PrintDestinatario()
        PrintReferencia()
        PrintServicioYFecha()
        If mRow("PORTSCOD") <> 2 Then
            PrintBarras() 'omite si recogerán por sus medios
        End If
        PrintBultos()
        PrintDetalles()
    End Sub

    Private Sub PrintRemitente()
        X = 375
        Y = 40
        '100pts=25mm
        'X = mev.MarginBounds.Left + 40
        'Y = mev.MarginBounds.Top + 40
        PrintLine("MATIAS MASSO, S.A.")
        PrintLine("SERVICIO TECNICO BRITAX RÖMER Y 4MOMS")
        PrintLine("Bertran, 96")
        PrintLine("08022 BARCELONA")
        PrintLine("tel.: 932.541.522")
    End Sub

    Private Sub PrintDestinatario()
        Y = 160
        Dim sNom As String = mRow("CLINOM")
        Dim iPos As Integer = sNom.IndexOf(" '")
        Dim sCom As String

        If iPos > -1 Then
            sCom = sNom.Substring(iPos + 1)
            sNom = sNom.Substring(0, iPos)
            PrintLine(sNom)
            PrintLine(sCom)
        Else
            PrintLine(sNom)
        End If

        PrintLine(mRow("CLIADR"))
        PrintLine(mRow("CLICIT"))
        PrintLine("tel.: " & mRow("CLITEL"))
    End Sub

    Private Sub PrintReferencia()
        Y = 270
        PrintLine(mRow("REF1"))
    End Sub

    Private Sub PrintServicioYFecha()
        X = 680
        Y = 20
        If mRow("PORTSCOD") = 2 Then
            PrintLine("recogerán")
        Else
            PrintLine("SEUR 24")
        End If
        PrintLine(Today.ToShortDateString)
    End Sub

    Private Sub PrintBarras()
        Dim sPlazaDestino As String = ""
        If Not IsDBNull(mRow("PLAZA")) Then
            sPlazaDestino = mRow("PLAZA")
        End If
        PrintLine("")
        PrintLine("de BARCELONA a " & sPlazaDestino)
        PrintLine(sPlazaDestino, FontBig)
        Y = 130
        Dim t As String = mRow("ECB")
        PrintBitmap(InterlacedBitmap(I25Encode(t), 100, 2))
        Y = 250
        Dim s As String = t.Substring(0, 2) & "  " & t.Substring(2, 2) & "  " & t.Substring(4, 2) & "  " & t.Substring(6, 7) & "  " & t.Substring(13, 1)
        PrintLine(s)
    End Sub

    Private Sub PrintBultos()
        Y = 280
        PrintLine("Bultos: " & mRow("BULTOS"))
        PrintLine("Peso..: " & mRow("KILOS"))
    End Sub

    Private Sub PrintDetalles()
        Y = 300
        X = 880
        Select Case mRow("PORTSCOD")
            Case 1
                PrintLine("P.PAGADOS")
            Case 2
                PrintLine("P.DEBIDOS")
            Case 3
                PrintLine("SUS MEDIOS")
        End Select
    End Sub

    Private Sub PrintLine(ByVal s As String, Optional ByVal oFont As Font = Nothing)
        If oFont Is Nothing Then oFont = FontStd
        With mev.Graphics
            .DrawString(s, oFont, BrushStd, X, Y)
            Y = Y + oFont.Height
        End With
    End Sub

    Private Sub PrintBitmap(ByVal oBmp As Bitmap)
        With mev.Graphics
            .DrawImage(oBmp, X, Y)
            Y = Y + oBmp.Height
        End With
    End Sub

    Function I25Encode(ByVal StringNumber)
        Dim asPattern(), sSTART, sSTOP

        ReDim asPattern(10)

        ' start and stop patterns can be found in fig. 3
        sSTART = "NNNN"
        sSTOP = "WNN"

        ' these patterns can be found in fig. 1
        asPattern(0) = "NNWWN"
        asPattern(1) = "WNNNW"
        asPattern(2) = "NWNNW"
        asPattern(3) = "WWNNN"
        asPattern(4) = "NNWNW"
        asPattern(5) = "WNWNN"
        asPattern(6) = "NWWNN"
        asPattern(7) = "NNNWW"
        asPattern(8) = "WNNWN"
        asPattern(9) = "NWNWN"

        If (Len(StringNumber) Mod 2) <> 0 Then
            ' the number of characters in the
            ' argument must be odd
            I25Encode = ""
            Exit Function
        End If


        If Not IsNumeric(StringNumber) Then
            ' argument must be numeric
            I25Encode = ""
            Exit Function
        Else
            If (InStr(StringNumber, ".") <> 0) Or _
               (InStr(StringNumber, ",") <> 0) Then
                ' argument is numeric but contains invalid
                ' characters to us
                I25Encode = ""
                Exit Function
            End If
        End If

        Dim sEncodedSTR, sUnit
        Dim iCharRead, sDigit1, sDigit2, i

        sEncodedSTR = ""
        For iCharRead = 1 To Len(StringNumber) Step 2
            sDigit1 = asPattern(Asc( _
                      Mid(StringNumber, iCharRead, 1)) - 48)
            sDigit2 = asPattern(Asc(Mid( _
                      StringNumber, iCharRead + 1, 1)) - 48)

            sUnit = ""

            For i = 1 To 5
                sUnit = sUnit & Mid(sDigit1, i, 1) & _
                        Mid(sDigit2, i, 1)
            Next

            sEncodedSTR = sEncodedSTR & sUnit
        Next

        I25Encode = sSTART & sEncodedSTR & sSTOP

    End Function

    Public Function InterlacedBitmap(ByVal DataToEncode As String, Optional ByVal IntHeight As Integer = 8, Optional ByVal LineWidth As Integer = 1) As Bitmap
        If IntHeight = 0 Then IntHeight = 150
        Dim IntChars As Integer = Len(DataToEncode)
        Dim IntWidth As Integer = IntChars * LineWidth * 1.5
        'MsgBox(IntWidth)
        Dim xPos As System.Int32 = 1


        Dim oBitmap As New Bitmap(IntWidth, IntHeight)
        Dim oGraphics As Graphics = Graphics.FromImage(oBitmap)

        Dim oPen As Pen
        Dim PenBlack As New Pen(Color.Black, LineWidth)
        Dim PenWhite As New Pen(Color.White, LineWidth)
        Dim i As Integer
        Dim NarrowBar As Integer = 1 * LineWidth
        Dim WideBar As Integer = 2 * LineWidth
        Dim BarWidth As Integer
        Dim BlackBar As Boolean = True
        Dim j As Integer
        oPen = PenBlack
        For i = 0 To IntChars - 1
            oPen = IIf(BlackBar, PenBlack, PenWhite)
            BarWidth = IIf(DataToEncode.Substring(i, 1) = "N", NarrowBar, WideBar)
            For j = 1 To BarWidth
                xPos = xPos + 1
                oGraphics.DrawLine(oPen, xPos, 0, xPos, IntHeight)
                'xPos = xPos + 1
            Next
            BlackBar = Not BlackBar
        Next
        Return oBitmap
    End Function
End Class
