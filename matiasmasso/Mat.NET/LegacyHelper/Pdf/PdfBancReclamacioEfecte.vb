Public Class PdfBancReclamacioEfecte
    Private mPdf As C1.C1Pdf.C1PdfDocument

    Private mCsb As DTOCsb = Nothing
    Private mFch As Date = DTO.GlobalVariables.Today()
    Private mMail As DTOCorrespondencia = Nothing

    Private mPageCount As Integer = 0
    Private X As Integer
    Private Y As Integer
    Private Yheader As Integer = 120

    Private Ymax As Integer
    Private mFont As New Font("Helvetica", 10, FontStyle.Regular)
    Private mFontEpg As New Font("Tahoma", 9, FontStyle.Regular)
    Private mLeftMargin As Integer = 90
    Private mRightMargin As Integer
    Private mBottomMargin As Integer

    Private Enum HorizontalAlignments
        Left
        Center
        Right
    End Enum
    Public Sub New(ByVal oCsb As DTOCsb, ByVal oMail As DTOCorrespondencia)
        MyBase.New()
        mCsb = oCsb
        Dim exs As New List(Of Exception)
        mMail = oMail
    End Sub


    Public Function Stream(exs As List(Of Exception), Optional ByVal BlSigned As Boolean = False) As Byte()
        mPdf = New PdfCorpTemplateDiagonal(PdfCorpTemplateDiagonal.Templates.invoice)
        mRightMargin = mPdf.PageRectangle.Right - 30
        mBottomMargin = mPdf.PageRectangle.Bottom - 40
        DrawDocument(exs)
        Dim oMemStream As New IO.MemoryStream
        mPdf.Save(oMemStream)
        Dim oBuffer As Byte() = oMemStream.ToArray
        'mCsb.Csa.emp.Org.Cert.SignStream(oBuffer)
        Return oBuffer
    End Function


    Public Sub DrawDocument(exs As List(Of Exception))
        If mPageCount > 0 Then
            mPdf.NewPage()
        End If
        mPageCount += 1

        Dim oBanc As DTOBanc = mCsb.Csa.Banc

        Dim iWidth As Integer = 300
        X = mLeftMargin
        Y = Yheader
        DrawString(oBanc.Nom, iWidth)

        X = mLeftMargin
        Y = Y + mFont.Height
        DrawString(oBanc.Address.Text, iWidth)

        X = mLeftMargin
        Y = Y + mFont.Height
        DrawString(DTOAddress.ZipyCit(oBanc.Address), iWidth)

        X = mRightMargin - 100
        Y = Y + 3 * mFont.Height
        DrawString("n/ref." & mMail.id, 100, HorizontalAlignments.Right)


        X = mLeftMargin + 120
        Y = Y + 5 * mFont.Height
        Dim s As String = "RECLAMACIO DE EFECTE " & mCsb.FormattedId()
        DrawString(s, mRightMargin - mLeftMargin)
        Y = Y + 2 * mFont.Height

        X = mLeftMargin
        Y = Y + mFont.Height
        DrawString("Senyors,", iWidth)
        Y = Y + mFont.Height

        X = mLeftMargin
        Y = Y + mFont.Height
        DrawString("Agrairem  reclamin el següent efecte amb càrrec al nostre compte corrent:", iWidth)
        Y = Y + mFont.Height

        AddRow("import", DTOAmt.CurFormatted(mCsb.Amt))
        AddRow("venciment", mCsb.Vto.ToShortDateString)
        AddRow("lliurat", mCsb.Contact.Nom)
        AddRow("compte del lliurat", DTOIban.Formated(mCsb.Iban))
        AddRow("data circulació", mCsb.Csa.Fch)
        AddRow("compte de carrec", DTOIban.Formated(mCsb.Csa.Banc.Iban))

        X = mLeftMargin
        Y = Y + 2 * mFont.Height
        DrawString("Cordialment,", iWidth)
        'Y = Y + 2 * mFont.Height
    End Sub

    Private Sub AddRow(ByVal sConcepte As String, ByVal sValue As String)
        Dim iWidth As Integer = 90
        X = mLeftMargin + 60
        Y = Y + mFont.Height
        DrawString(sConcepte, iWidth)

        iWidth = 200
        X = mLeftMargin + 150
        DrawString(sValue, iWidth)
    End Sub


    Private Sub DrawString(ByVal sTxt As String, ByVal iWidth As Integer, Optional ByVal oAlign As HorizontalAlignments = HorizontalAlignments.Left, Optional ByVal oFont As Font = Nothing, Optional ByVal oBrush As SolidBrush = Nothing)
        If oFont Is Nothing Then oFont = mFont
        If oBrush Is Nothing Then oBrush = Brushes.Black


        Dim xPos As Integer
        Dim iStringWidth As Integer
        Select Case oAlign
            Case HorizontalAlignments.Left
                xPos = X
                iStringWidth = iWidth
            Case HorizontalAlignments.Center
                iStringWidth = mPdf.MeasureString(sTxt, oFont).Width
                xPos = X + (iWidth - iStringWidth) / 2
            Case HorizontalAlignments.Right
                iStringWidth = mPdf.MeasureString(sTxt, oFont).ToSize.Width
                xPos = X + iWidth - iStringWidth
        End Select

        Dim oRc As New Rectangle(xPos, Y, iStringWidth, oFont.Height)
        Dim rc As New RectangleF(xPos, Y, iWidth, oFont.Height)
        mPdf.DrawString(sTxt, oFont, oBrush, rc)
        X = X + iWidth
    End Sub

End Class
