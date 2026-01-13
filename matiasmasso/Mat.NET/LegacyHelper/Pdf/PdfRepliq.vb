Public Class PdfRepLiq
    Private mRepLiq As DTORepLiq
    Private mPdf As PdfCorpTemplateDiagonal

    Private mPageCount As Integer = 0
    Private X As Integer
    Private Y As Integer
    Private Ymin As Integer = 120
    Private Ymax As Integer
    Private mFont As New Font("Helvetica", 9, FontStyle.Regular)
    Private mBrush As SolidBrush = Brushes.Black
    Private mLeftMargin As Integer = 70
    Private mRightMargin As Integer
    Private mBottomMargin As Integer

    Friend mFontHeader As New Font("Helvetica", 10, FontStyle.Regular)
    Private mLang As DTOLang


    Public Sub New(ByVal oRepLiq As DTORepLiq)
        MyBase.New()
        mRepLiq = oRepLiq
        'BLLRepLiq.Load(mRepLiq) no l'hem de carregar de nou per no sobreescriure les dades esmenades al formulari
    End Sub

    'verificar Rep.Lang, RaoSocialFacturacio, Nom, Nif, Address
    Public Function Stream(exs As List(Of Exception), Optional oSignatureCert As DTOCert = Nothing) As Byte()
        Dim retval As Byte() = Nothing
        mLang = DTORep.RaoSocialFacturacioOrSelf(mRepLiq.Rep).Lang
        mPdf = New PdfCorpTemplateDiagonal(PdfCorpTemplateDiagonal.Templates.invoice)
        mRightMargin = mPdf.PageRectangle.Right - 30
        mBottomMargin = mPdf.PageRectangle.Bottom - 40

        DrawFras()

        Dim oPdf As C1PdfHelper.Document = mPdf
        If oSignatureCert IsNot Nothing Then
            oPdf.Sign(oSignatureCert.Stream, oSignatureCert.Pwd, exs)
        End If

        Dim oMemStream As New IO.MemoryStream
        oPdf.Save(oMemStream)

        retval = oMemStream.ToArray
        Return retval
    End Function

    Private Sub DrawFras()
        Dim oArrastre = DTOAmt.Empty

        For Each oItem As DTORepComLiquidable In mRepLiq.Items
            If IsEndOfPage() Then NewPage(oArrastre)
            DrawFra(oItem)
            oArrastre.Add(oItem.Comisio)
        Next

        If mRepLiq.IVApct <> 0 Or mRepLiq.IRPFpct <> 0 Then
            If IsEndOfPage() Then NewPage(oArrastre)
            DrawRaya()
            DrawTotal("Base imponible", DTORepLiq.GetTotalComisions(mRepLiq).Eur)
            If mRepLiq.IVApct <> 0 Then
                If IsEndOfPage() Then NewPage(oArrastre)
                Dim oIvaAmt As DTOAmt = DTORepLiq.GetIVAAmt(mRepLiq)
                DrawTotal("IVA " & mRepLiq.IVApct & "%", oIvaAmt.Eur)
                oArrastre.Add(oIvaAmt)
            End If
            If mRepLiq.IRPFpct <> 0 Then
                If IsEndOfPage() Then NewPage(oArrastre)
                Dim oIrpfAmt As DTOAmt = DTORepLiq.GetIRPFAmt(mRepLiq)
                DrawTotal("retención IRPF " & mRepLiq.IRPFpct & "%", -oIrpfAmt.Eur)
                oArrastre.Substract(oIrpfAmt)
            End If
        End If

        DrawRaya()
        DrawTotal("total Eur", oArrastre.Eur)
    End Sub

    Private Function IsEndOfPage() As Boolean
        Dim retval As Boolean = (Y = 0)
        If Y > (mBottomMargin - 2 * mFont.Height) Then retval = True
        Return retval
    End Function

    Private Sub NewPage(oArrastre As DTOAmt)
        If mPageCount > 0 Then
            DrawTotal("suma y sigue", oArrastre.Eur)
            mPdf.NewPage()
        End If
        mPageCount += 1
        DrawHeader()
        'Y = Ymin
        If mPageCount > 1 Then
            DrawTotal("suma anterior", oArrastre.Eur)
        End If
    End Sub

    Private Sub DrawHeader()

        Y = 30
        X = mLeftMargin
        'DrawString("MATIAS MASSO, S.A. - NIF A58007857 - Diagonal 403 - 08008 BARCELONA - tel.932.541.522", mRightMargin - mLeftMargin)
        Y = Y + mFontHeader.Height
        'mPdf.DrawLine(Pens.Gray, mLeftMargin, Y, mRightMargin, Y)

        Y = 60
        X = mLeftMargin + 100

        If mPageCount = 1 Then
            PrintHeader()
            Y = Y + 2 * mFontHeader.Height
        Else
            mPdf.DrawPageNoLogo()
        End If


        Dim sTit As String = "LIQUIDACION DE COMISIONES NUM." & mRepLiq.Id & " DEL " & mRepLiq.Fch.ToShortDateString
        DrawString(sTit, 300)

        If mPageCount > 1 Then
            X = mRightMargin - 50
            DrawString(mLang.Tradueix("pag.", "pag.", "page ") & mPageCount, 50, , StringAlignment.Far)
        End If


        Y = Y + 2 * mFontHeader.Height

        DrawColumnHeaders()
    End Sub


    Private Sub PrintHeader()
        'DrawForm()
        Dim exs As New List(Of Exception)
        Dim oContact = DTORep.RaoSocialFacturacioOrSelf(mRepLiq.Rep)
        Dim oArray As New ArrayList
        oArray.Add(oContact.Nom)
        oArray.Add("NIF: " & oContact.PrimaryNifValue())
        oArray.Add(oContact.Address.Text)
        oArray.Add(DTOZip.FullNom(oContact.Address.Zip))
        DrawAdr(oArray)
    End Sub


    Friend Sub DrawAdr(ByVal oAdr As ArrayList)
        Dim oRc As RectangleF
        Dim X As Integer = mLeftMargin
        Y = 120
        Dim s As String
        For Each s In oAdr
            oRc = New RectangleF(X, Y, mPdf.PageRectangle.Width, mFontHeader.Height)
            mPdf.DrawString(s, mFontHeader, Brushes.Black, oRc)
            Y += mFontHeader.GetHeight
        Next
    End Sub


    Private Sub DrawTotal(ByVal sTxt As String, ByVal DblEur As Decimal)
        Dim iWidth As Integer
        X = mLeftMargin + 50 + 50

        iWidth = 180
        DrawString(sTxt, iWidth)

        X = mRightMargin - 50

        iWidth = 50
        DrawAmt(DblEur, iWidth)

        Y = Y + mFont.Height
    End Sub

    Private Sub DrawColumnHeaders()
        Dim iWidth As Integer
        X = mLeftMargin

        iWidth = 50
        DrawString(mLang.Tradueix("factura", "factura", "invoice", "factura"), iWidth)

        iWidth = 50
        DrawString(mLang.Tradueix("fecha", "data", "date", "fecha"), iWidth)

        iWidth = 250
        DrawString(mLang.Tradueix("cliente", "client", "customer", "cliente"), iWidth)

        X = mRightMargin - 100
        iWidth = 50
        DrawString(mLang.Tradueix("base", "base", "base", "base"), iWidth, , StringAlignment.Far)

        X = mRightMargin - 50
        iWidth = 50
        DrawString(mLang.Tradueix("comisión", "comissió", "commission", "comisión"), iWidth, , StringAlignment.Far)

        Y = Y + 2 * mFont.Height
    End Sub

    Private Sub DrawFra(oItem As DTORepComLiquidable)
        Dim iWidth As Integer
        X = mLeftMargin

        Dim oFra As DTOInvoice = oItem.Fra

        iWidth = 50
        DrawString(oFra.Num, iWidth)

        iWidth = 50
        DrawString(oFra.Fch.ToShortDateString, iWidth)

        iWidth = 250
        DrawString(oFra.Customer.FullNom, iWidth)

        X = mRightMargin - 100
        iWidth = 50
        DrawAmt(oItem.baseFras.eur, iWidth)

        X = mRightMargin - 50
        iWidth = 50
        DrawAmt(oItem.Comisio.Val, iWidth)

        Y = Y + mFont.Height
    End Sub

    Private Sub DrawString(ByVal sTxt As String, ByVal iWidth As Integer, Optional ByVal oFont As Font = Nothing, Optional ByVal oAlign As StringAlignment = StringAlignment.Near)
        Dim iStringWidth As Integer
        Dim xPos As Integer
        If oFont Is Nothing Then oFont = mFont

        Select Case oAlign
            Case StringAlignment.Near
                xPos = X
            Case StringAlignment.Center
                iStringWidth = mPdf.MeasureString(sTxt, oFont).Width
                xPos = X + (iWidth - iStringWidth) / 2
            Case StringAlignment.Far
                iStringWidth = mPdf.MeasureString(sTxt, oFont).Width
                xPos = X + iWidth - iStringWidth
        End Select

        Dim rc As New RectangleF(xPos, Y, iWidth, oFont.Height)
        mPdf.DrawString(sTxt, oFont, mBrush, rc)
        X = X + iWidth
    End Sub

    Private Sub DrawAmt(ByVal DblEur As Decimal, ByVal iWidth As Integer, Optional ByVal oFont As Font = Nothing)
        If DblEur = 0 Then Return
        If oFont Is Nothing Then oFont = mFont

        Dim sEur As String = Format(DblEur, "#,##0.00")
        Dim iStringWidth As Integer = mPdf.MeasureString(sEur, oFont).Width
        Dim xPos As Integer = X + iWidth - iStringWidth
        Dim rc As New RectangleF(xPos, Y, iWidth, oFont.Height)
        mPdf.DrawString(sEur, oFont, mBrush, rc)
        X = X + iWidth
    End Sub

    Private Sub DrawRaya()
        Y = Y + mFont.Height / 2
        mPdf.DrawLine(Pens.Black, mRightMargin - 40, Y, mRightMargin, Y)
        Y = Y + mFont.Height / 2
    End Sub
End Class
