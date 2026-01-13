Public Class PdfCorpTemplateStGervasi
    Inherits C1PdfHelper.Document

    Private mColor As Color = Color.FromArgb(255, 51, 51) 'FF3333
    Private mLogoBrush As New SolidBrush(mColor)
    Private mFontTemplate As New Font("Arial", 8, FontStyle.Regular)
    Private mTextBrush As Brush = Brushes.Gray
    Private mTemplate As Templates
    Private mLeft As Integer = 60
    Private mRight As Integer


    Public Enum Templates
        logo
        invoice
    End Enum

    Public Sub New(ByVal oTemplate As Templates)
        MyBase.New(System.Drawing.Printing.PaperKind.A4)
        mTemplate = oTemplate
        DrawPage()
    End Sub


    Public Sub DrawPage()
        mRight = MyBase.PageRectangle.Width - 20
        DrawLogo(60, 20)
        DrawNom(190, 20)
        DrawDadesRegistrals(100, 100)
    End Sub

    Private Sub DrawDadesRegistrals(ByVal NomX As Integer, ByVal NomY As Integer)
        Dim s As String = "Domicilio Social: Hurtado 21, 08023 Barcelona - Reg.Mercantil de Barcelona, Tomo 6403, Libro 5689, Sección 2ª, Folio 167, Hoja 76326, Inscripción 1ª, NIF ES-A58007857"
        Dim oFont As New Font("Arial", 8, FontStyle.Regular)
        Dim iStringWidth As Integer = MyBase.MeasureString(s, oFont).Width
        Dim oRc As RectangleF = MyBase.PageRectangle
        oRc.Width = 10
        oRc.X = 10
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center
        sF.FormatFlags = StringFormatFlags.DirectionVertical
        MyBase.DrawString(s, oFont, Brushes.Gray, oRc, sF)
    End Sub

    Private Sub DrawNom(ByVal NomX As Integer, ByVal NomY As Integer)
        Dim iY As Integer = NomY - 6
        Dim sF As New StringFormat
        sF.Alignment = StringAlignment.Center

        Dim s As String = "MATIAS MASSO, S.A."
        Dim oFont As New Font("Arial", 24, FontStyle.Bold)
        Dim oRc As New Rectangle(mLeft, iY, mRight - mLeft, oFont.Height)
        MyBase.DrawString(s, oFont, Brushes.Gray, oRc, sF)
        iY = iY + 28

        s = "Pg. Sant Gervasi, 50 - 08022 BARCELONA"
        oFont = New Font("Arial", 8, FontStyle.Regular)
        oRc = New Rectangle(mLeft, iY, mRight - mLeft, oFont.Height)
        MyBase.DrawString(s, oFont, Brushes.Gray, oRc, sF)
        iY = iY + 10

        s = "tel. 932.541.522 fax 932.541.521 info@matiasmasso.es"
        oFont = New Font("Arial", 8, FontStyle.Regular)
        oRc = New Rectangle(mLeft, iY, mRight - mLeft, oFont.Height)
        MyBase.DrawString(s, oFont, Brushes.Gray, oRc, sF)
        iY = iY + 10

        s = "www.matiasmasso.es"
        oFont = New Font("Arial", 8, FontStyle.Regular)
        oRc = New Rectangle(mLeft, iY, mRight - mLeft, oFont.Height)
        MyBase.DrawString(s, oFont, Brushes.Gray, oRc, sF)

    End Sub

    Private Sub DrawLogo(ByVal LogoX As Integer, ByVal LogoY As Integer)
        Dim iRectangleWidth As Integer = 33
        Dim iRectangleGap As Integer = 7
        Dim oFont As New Font("Arial", 36, FontStyle.Bold)

        Dim iX As Integer = LogoX
        Dim oRc1 As New Rectangle(iX, LogoY, iRectangleWidth, iRectangleWidth)

        iX = LogoX + iRectangleWidth + iRectangleGap
        Dim oRc2 As New Rectangle(iX, LogoY, iRectangleWidth, iRectangleWidth)

        iX = LogoX + 2 * (iRectangleWidth + iRectangleGap)
        Dim oRc3 As New Rectangle(iX, LogoY, iRectangleWidth, iRectangleWidth)

        MyBase.DrawRectangle(Pens.Black, oRc1.X + 1, oRc1.Y + 1, iRectangleWidth, iRectangleWidth)
        MyBase.DrawRectangle(Pens.Black, oRc2.X + 1, oRc2.Y + 1, iRectangleWidth, iRectangleWidth)
        MyBase.DrawRectangle(Pens.Black, oRc3.X + 1, oRc3.Y + 1, iRectangleWidth, iRectangleWidth)

        MyBase.FillRectangle(Brushes.Red, oRc1)
        MyBase.FillRectangle(Brushes.Red, oRc2)
        MyBase.FillRectangle(Brushes.Red, oRc3)

        oRc1.X += 1
        oRc1.Y -= 7
        MyBase.DrawString("M", oFont, Brushes.Black, oRc1)

        oRc1.X += 1
        oRc1.Y += 1
        MyBase.DrawString("M", oFont, Brushes.White, oRc1)

        oRc2.X += 6
        oRc2.Y -= 7
        MyBase.DrawString("+", oFont, Brushes.Black, oRc2)

        oRc2.X += 1
        oRc2.Y += 1
        MyBase.DrawString("+", oFont, Brushes.White, oRc2)

        oRc3.X += 2
        oRc3.Y -= 7
        MyBase.DrawString("O", oFont, Brushes.Black, oRc3)

        oRc3.X += 1
        oRc3.Y += 1
        MyBase.DrawString("O", oFont, Brushes.White, oRc3)

    End Sub


End Class


