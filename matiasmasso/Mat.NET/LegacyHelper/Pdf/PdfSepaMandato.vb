Imports C1.C1Pdf

Public Class PdfSepaMandato

    Private _Iban As DTOIban
    Private _Swift As String
    Private _Lang As DTOLang
    Private _Org As DTOContact

    Private mRectangleMain As Rectangle
    Private mRectangleHeader As Rectangle
    Private mRectangleTitle As Rectangle
    Private mRectangleLogo As Rectangle
    Private mRectangleSubHeader As Rectangle
    Private mRectangleBody As Rectangle
    Private mRectangleFooter As Rectangle

    Private mCasillaX As Integer
    Private mCasillaWidth As Integer
    Private mCasillaSingleLineHeight As Integer = 13

    Private mFontStandard As New Font("Helvetica", 8, FontStyle.Regular)
    Private mFontTitle As New Font("Helvetica", 13, FontStyle.Bold)
    Private mFontSubtitle As New Font("Helvetica", 6, FontStyle.Italic)

    Private Enum texts
        Title
        SubTitle
        SubHeader1
        SubHeader2
        SubHeader3 'renuncia devolucions
        DeutorNom
        DeutorAdr
        DeutorZipyCit
        DeutorPais
        Iban
        Swift
        CreditorNom
        CreditorId
        CreditorAdr
        CreditorZipyCit
        CreditorPais
        TipoDePago
        SigLocation
        Signature
        DetailsInfo
        DeutorId
        DeutorBehalf
        DeutorBehalfName
        LeaveBlankOnYourBehalf
        DebtorReferencePartyId
        CreditorBehalfParty
        CreditorRefPartyNom
        CreditorRefPartyId
        InRespectOfTheContract
        ContractId
        ContractDsc
        ReturnAdr
        CreditorsUse
    End Enum

    Public Sub New(oEmp As DTOEmp, oIban As DTOIban, sSwift As String, Optional oLang As DTOLang = Nothing)
        MyBase.New()
        _Iban = oIban
        _Swift = sSwift
        If oLang Is Nothing Then oLang = _Iban.titular.lang
        _Lang = oLang
        _Org = oEmp.Org
    End Sub

    Public ReadOnly Property Iban As DTOIban
        Get
            Return _Iban
        End Get
    End Property

    Public Function Pdf(oSepaTexts As List(Of DTOSepaText)) As Byte()
        Dim oMemoryStream As New IO.MemoryStream
        C1Pdf(oSepaTexts).Save(oMemoryStream)

        Dim oPdfStream As Byte() = oMemoryStream.ToArray
        Return oPdfStream
    End Function

    Private Function C1Pdf(oSepaTexts As List(Of DTOSepaText)) As C1PdfDocument

        Dim oPdf As New C1.C1Pdf.C1PdfDocument(System.Drawing.Printing.PaperKind.A4)
        Dim oPenRectangles As New Pen(Color.DarkGray, 1)
        Dim iHeaderSide As Integer = 100

        mRectangleMain = New Rectangle(50, 20, oPdf.PageRectangle.Width - 60, oPdf.PageRectangle.Height - 20)
        mRectangleHeader = New Rectangle(mRectangleMain.X, mRectangleMain.Y, mRectangleMain.Width, iHeaderSide)
        mRectangleTitle = New Rectangle(mRectangleHeader.X + iHeaderSide, mRectangleHeader.Y, mRectangleHeader.Width - 200, mRectangleHeader.Height)
        mRectangleLogo = New Rectangle(mRectangleHeader.X + mRectangleHeader.Width - iHeaderSide, mRectangleHeader.Y, iHeaderSide, iHeaderSide)

        Dim sSubHeaderText As String = oSepaTexts(texts.SubHeader1).LangText.Tradueix(_Lang) & vbCrLf & oSepaTexts(texts.SubHeader3).LangText.Tradueix(_Lang) ' & vbCrLf & oSepaTexts(texts.SubHeader2)
        sSubHeaderText = sSubHeaderText.Replace("@01", _Org.Nom)
        Dim oSubHeaderSize As SizeF = oPdf.MeasureString(sSubHeaderText, mFontStandard, mRectangleMain.Width - 20)

        mRectangleSubHeader = New Rectangle(mRectangleHeader.X, mRectangleHeader.Y + mRectangleHeader.Height, mRectangleHeader.Width, oSubHeaderSize.Height + 20)
        'mRectangleSubheaderText = New Rectangle(mRectangleSubHeader.X + 10, mRectangleSubHeader.Y + 10, mRectangleSubHeader.Width - 20, mRectangleSubHeader.Height - 10)
        mRectangleBody = New Rectangle(mRectangleMain.X, mRectangleSubHeader.Bottom, mRectangleMain.Width, mRectangleMain.Bottom - mRectangleSubHeader.Bottom - 30)
        mRectangleFooter = New Rectangle(mRectangleBody.Left, mRectangleBody.Bottom, mRectangleBody.Width, 10)

        mCasillaX = mRectangleHeader.X + 180
        mCasillaWidth = mRectangleHeader.Right - 40 - mCasillaX

        Dim oBackgroundColor As Color = Color.FromArgb(255, 204, 153)
        oPdf.FillRectangle(New SolidBrush(oBackgroundColor), mRectangleSubHeader)
        oPdf.FillRectangle(New SolidBrush(oBackgroundColor), mRectangleBody)
        oPdf.FillRectangle(New SolidBrush(Color.LightBlue), mRectangleFooter)

        oPdf.DrawRectangle(oPenRectangles, mRectangleHeader)
        oPdf.DrawRectangle(oPenRectangles, mRectangleTitle)
        oPdf.DrawRectangle(oPenRectangles, mRectangleSubHeader)
        oPdf.DrawRectangle(oPenRectangles, mRectangleBody)
        oPdf.DrawRectangle(oPenRectangles, mRectangleFooter)

        Dim oLogo As New LogoMmO(oPdf)
        Dim oLogoCenter As New Point(mRectangleLogo.X + iHeaderSide / 2, mRectangleLogo.Y + iHeaderSide / 2)
        oLogo.Draw(oLogoCenter, 75, True)

        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center
        oPdf.DrawString(oSepaTexts(texts.Title).LangText.Tradueix(_Lang), mFontTitle, Brushes.Black, mRectangleTitle, sF)

        sF.Alignment = StringAlignment.Center
        Dim oSubTitleRectangle As New Rectangle(mRectangleHeader.X, (mRectangleHeader.Y + mRectangleHeader.Height) / 2 + mFontTitle.Height, mRectangleHeader.Width, mFontSubtitle.Height)
        oPdf.DrawString(oSepaTexts(texts.SubTitle).LangText.Tradueix(_Lang), mFontSubtitle, Brushes.DarkGray, oSubTitleRectangle, sF)

        sF.Alignment = StringAlignment.Center
        sF.LineAlignment = StringAlignment.Center
        Dim oFontGuid As New Font("Helvetica", 12, FontStyle.Bold)
        oPdf.DrawString(_Iban.Guid.ToString, mFontTitle, Brushes.Black, mRectangleTitle, sF)

        sF.Alignment = StringAlignment.Near
        sF.LineAlignment = StringAlignment.Center
        Dim oSubHeaderTextRectangle As New Rectangle(mRectangleSubHeader.X + 10, mRectangleSubHeader.Y + 10, oSubHeaderSize.Width, oSubHeaderSize.Height)
        oPdf.DrawString(sSubHeaderText, mFontStandard, Brushes.Black, oSubHeaderTextRectangle, sF)

        Dim iY As Integer = mRectangleSubHeader.Bottom + 10

        Dim ss() As String = oSepaTexts(texts.DeutorNom).LangText.Tradueix(_Lang).Split(vbLf)
        iY = iY + DrawCasilla(oPdf, True, iY, ss(0), ss(1), _Iban.Titular.Nom, 1)

        ss = oSepaTexts(texts.DeutorAdr).LangText.Tradueix(_Lang).Split(vbLf)
        iY = iY + DrawCasilla(oPdf, True, iY, ss(0), ss(1), _Iban.Titular.Address.Text, 2)

        ss = oSepaTexts(texts.DeutorZipyCit).LangText.Tradueix(_Lang).Split(vbLf)
        DrawCasilla(oPdf, True, iY, , ss(0), _Iban.Titular.Address.Zip.ZipCod, , , 65)
        'iY = iY + DrawCasilla(oPdf, True, iY, , ss(1), _Iban.Contact.Adr.Zip.Location.Nom, 3, mCasillaX + 75, mCasillaWidth - 75)
        iY = iY + DrawCasilla(oPdf, True, iY, , ss(1), _Iban.Titular.Address.Zip.Location.Nom, 3, mCasillaX, mCasillaWidth)

        iY = iY + DrawCasilla(oPdf, True, iY, , oSepaTexts(texts.DeutorPais).LangText.Tradueix(_Lang), _Iban.Titular.Address.CountryNom(_Lang), 4)

        ss = oSepaTexts(texts.Iban).LangText.Tradueix(_Lang).Split(vbLf)
        iY = iY + DrawCasilla(oPdf, True, iY, ss(0), ss(1), DTOIban.Formated(_Iban), 5)

        iY = iY + DrawCasilla(oPdf, True, iY, , oSepaTexts(texts.Swift).LangText.Tradueix(_Lang), _Swift, 6)

        iY = iY + DrawCasilla(oPdf, True, iY, oSepaTexts(texts.CreditorNom).LangText.Tradueix(_Lang), oSepaTexts(texts.CreditorNom).LangText.Tradueix(_Lang), _Org.Nom, 7)

        Dim sPresentador_Id As String = IdentificadorDelInterviniente(DTOAddress.Country(_Org.Address).ISO, "000", _Org.PrimaryNifValue())
        iY = iY + DrawCasilla(oPdf, True, iY, , oSepaTexts(texts.CreditorId).LangText.Tradueix(_Lang), sPresentador_Id, 8)

        iY = iY + DrawCasilla(oPdf, True, iY, , oSepaTexts(texts.CreditorAdr).LangText.Tradueix(_Lang), _Org.Address.Text, 9)

        ss = oSepaTexts(texts.CreditorZipyCit).LangText.Tradueix(_Lang).Split(vbLf)
        DrawCasilla(oPdf, True, iY, , ss(0), _Org.Address.Zip.ZipCod, , , 65)
        iY = iY + DrawCasilla(oPdf, True, iY, , ss(1), DTOAddress.Location(_Org.Address).Nom, 10, mCasillaX + 75, mCasillaWidth - 75)

        iY = iY + DrawCasilla(oPdf, True, iY, , oSepaTexts(texts.CreditorPais).LangText.Tradueix(_Lang), DTOAddress.Country(_Org.address).LangNom.Tradueix(_Lang), 11)

        ss = oSepaTexts(texts.TipoDePago).LangText.Tradueix(_Lang).Split(vbLf)
        Dim iShortLen As Integer = 16
        DrawCasilla(oPdf, True, iY, ss(0), ss(1), "X", , , iShortLen)
        iY = iY + DrawCasilla(oPdf, True, iY, , ss(2), , 12, mCasillaX + 75, iShortLen)

        ss = oSepaTexts(texts.SigLocation).LangText.Tradueix(_Lang).Split(vbLf)
        DrawCasilla(oPdf, False, iY, ss(0), ss(1), _Iban.Titular.Address.Zip.Location.Nom, , , mCasillaWidth - 75)
        iY = iY + DrawCasilla(oPdf, True, iY, , ss(2), DTO.GlobalVariables.Today(), 13, mCasillaX + mCasillaWidth - 65, 65)

        Dim oSignature As New C1.C1Pdf.PdfSignature
        With oSignature
            .Visibility = FieldVisibility.VisibleNotPrintable
            .Font = New System.Drawing.Font("Arial", 0.5)
            '.BackColor = Color.GreenYellow
            '.BorderStyle = FieldBorderStyle.Dashed
            '.BorderColor = Color.Red
            '.BorderWidth = FieldBorderWidth.Thick
        End With
        oPdf.AddField(oSignature, New RectangleF(mCasillaX, iY, mCasillaWidth, 35))


        ss = oSepaTexts(texts.Signature).LangText.Tradueix(_Lang).Split(vbLf)
        iY = iY + DrawCasilla(oPdf, True, iY, ss(0), ss(1), , , , , 45)

        Dim sbs As New System.Text.StringBuilder
        sbs.Append(_Lang.Tradueix("Firmado por ", "Signat per ", "Signed by "))
        sbs.Append(_Iban.PersonNom)
        sbs.Append(_Lang.Tradueix(" con DNI ", " amb DNI ", " with Id number "))
        sbs.Append(_Iban.PersonDni)
        oPdf.DrawString(sbs.ToString, mFontSubtitle, Brushes.Navy, New PointF(mCasillaX + 10, iY - 20))



        oPdf.DrawLine(oPenRectangles, mRectangleBody.Left, iY, mRectangleBody.Right, iY)

        iY = iY + 10
        sF.Alignment = StringAlignment.Near
        Dim oPoint As New PointF(mRectangleSubHeader.X + 10, iY)
        oPdf.DrawString(oSepaTexts(texts.DetailsInfo).LangText.Tradueix(_Lang), mFontStandard, Brushes.Black, oPoint, sF)
        iY = iY + 18

        ss = oSepaTexts(texts.DeutorId).LangText.Tradueix(_Lang).Split(vbLf)
        iY = iY + DrawCasilla(oPdf, False, iY, ss(0), ss(1))

        Dim s As String = oSepaTexts(texts.DeutorBehalfName).LangText.Tradueix(_Lang) & oSepaTexts(texts.LeaveBlankOnYourBehalf).LangText.Tradueix(_Lang)
        s = s.Replace("@01", _Org.Nom)
        iY = iY + DrawCasilla(oPdf, False, iY, oSepaTexts(texts.DeutorBehalf).LangText.Tradueix(_Lang), s)

        iY = iY + DrawCasilla(oPdf, False, iY, , oSepaTexts(texts.DebtorReferencePartyId).LangText.Tradueix(_Lang))

        iY = iY + DrawCasilla(oPdf, False, iY, oSepaTexts(texts.CreditorBehalfParty).LangText.Tradueix(_Lang), oSepaTexts(texts.CreditorRefPartyNom).LangText.Tradueix(_Lang))

        iY = iY + DrawCasilla(oPdf, False, iY, , oSepaTexts(texts.CreditorRefPartyId).LangText.Tradueix(_Lang))

        iY = iY + DrawCasilla(oPdf, False, iY, oSepaTexts(texts.InRespectOfTheContract).LangText.Tradueix(_Lang), oSepaTexts(texts.ContractId).LangText.Tradueix(_Lang))

        iY = iY + DrawCasilla(oPdf, False, iY, , oSepaTexts(texts.ContractDsc).LangText.Tradueix(_Lang))


        Dim sb As New System.Text.StringBuilder
        sb.Append(oSepaTexts(texts.ReturnAdr).LangText.Tradueix(_Lang) & ": ")
        sb.Append("email " & "cuentas@matiasmasso.es")
        sb.Append(" ") 'pad right

        iY = mRectangleBody.Bottom
        sF.Alignment = StringAlignment.Far
        oPdf.DrawString(sb.ToString, mFontStandard, Brushes.Black, mRectangleFooter, sF)


        Return oPdf
    End Function

    Private Function DrawCasilla(oPdf As C1PdfDocument, BlObligatori As Boolean, iY As Integer, Optional sHeader As String = "", Optional sSubHeader As String = "", Optional sValue As String = "", Optional iOrdre As Integer = 0, Optional iX As Integer = 0, Optional iWidth As Integer = 0, Optional iCasillaHeight As Integer = 0) As Integer
        Dim iLineHeight As Integer = 17
        If iX = 0 Then iX = mCasillaX
        If iWidth = 0 Then iWidth = mCasillaWidth
        If iCasillaHeight = 0 Then iCasillaHeight = mCasillaSingleLineHeight

        If sHeader > "" Then
            Dim sF As New StringFormat()
            sF.Alignment = StringAlignment.Near
            'Dim oPoint As New PointF(mRectangleSubheaderText.X, iY)
            Dim iTextWidth As Integer = iX - (mRectangleSubHeader.X + 10) - 10
            Dim oTextSizeF As SizeF = oPdf.MeasureString(sHeader, mFontStandard, iTextWidth)
            Dim oRectangle As New Rectangle(mRectangleSubHeader.X + 10, iY, oTextSizeF.Width, oTextSizeF.Height)
            oPdf.DrawString(sHeader, mFontStandard, Brushes.Black, oRectangle, sF)
        End If

        If BlObligatori Then
            Dim sF As New StringFormat()
            sF.Alignment = StringAlignment.Center
            sF.LineAlignment = StringAlignment.Center
            Dim oFontAsterisk As New Font("Helvetia", 12, FontStyle.Bold)
            Dim oRectangleAsterisk As New Rectangle(iX - 12, iY, 12, 12)
            oPdf.DrawString("*", oFontAsterisk, Brushes.DarkGray, oRectangleAsterisk, sF)
        End If

        Dim oCasilla As New Rectangle(iX, iY, iWidth, iCasillaHeight)
        oPdf.FillRectangle(Brushes.White, oCasilla)
        oPdf.DrawRectangle(Pens.DarkGray, oCasilla)
        iLineHeight += (iCasillaHeight - mCasillaSingleLineHeight)

        If sValue > "" Then
            Dim sF As New StringFormat()
            sF.Alignment = StringAlignment.Near
            sF.LineAlignment = StringAlignment.Center
            Dim oRectangle As New Rectangle(oCasilla.X + 5, oCasilla.Y, oCasilla.Width - 6, oCasilla.Height)
            oPdf.DrawString(sValue, mFontStandard, Brushes.Navy, oRectangle, sF)
        End If

        If sSubHeader > "" Then
            Dim sF As New StringFormat()
            sF.Alignment = StringAlignment.Near
            Dim oRectangle As Rectangle
            Dim oBrush As SolidBrush
            Dim oTextSizeF As SizeF = oPdf.MeasureString(sSubHeader, mFontSubtitle, mCasillaWidth - 5)
            If iCasillaHeight = mCasillaSingleLineHeight Then
                oRectangle = New Rectangle(oCasilla.X + 5, iY + iCasillaHeight, oTextSizeF.Width, oTextSizeF.Height) 'texte sota la casella
                oBrush = Brushes.Black
            Else
                oRectangle = New Rectangle(oCasilla.X + 5, iY, oTextSizeF.Width, oTextSizeF.Height) 'texte sota la casella
                oBrush = Brushes.DarkGray
            End If
            oPdf.DrawString(sSubHeader, mFontSubtitle, oBrush, oRectangle, sF)
            iLineHeight += oTextSizeF.Height
        End If

        If iOrdre <> 0 Then
            Dim iOrdreWidth As Integer = 30
            Dim iRightMargin As Integer = 20
            Dim sF As New StringFormat()
            sF.Alignment = StringAlignment.Far
            Dim oRectangle As New Rectangle(mRectangleMain.Right - iOrdreWidth - iRightMargin, iY, iOrdreWidth, mFontStandard.Height)
            oPdf.DrawString(iOrdre.ToString, mFontStandard, Brushes.Black, oRectangle, sF)
        End If

        Return iLineHeight
    End Function

    Shared Function IdentificadorDelInterviniente(ByVal sIsoPais As String, ByVal sSufijo As String, ByVal sNif As String) As String
        sIsoPais = IIf(sIsoPais.Length < 2, sIsoPais.PadRight(2), Left(sIsoPais, 2))
        sSufijo = IIf(sSufijo.Length < 3, sSufijo.PadLeft(3, "0"), Left(sSufijo, 3))
        sNif = IIf(sNif.Length < 9, sNif.PadRight(9), Left(sNif, 9))
        Dim sDC As String = "00"

        Dim sb As New System.Text.StringBuilder
        sb.Append(sIsoPais)
        sb.Append(sDC)
        sb.Append(sSufijo)
        sb.Append(sNif)

        sDC = DigitsDeControlInterviniente(sb.ToString())

        sb = New System.Text.StringBuilder
        sb.Append(sIsoPais)
        sb.Append(sDC)
        sb.Append(sSufijo)
        sb.Append(sNif)

        Dim retval As String = sb.ToString
        retval = retval.PadRight(35)
        Return retval
    End Function

    Shared Function DigitsDeControlInterviniente(sSource As String) As String
        'Excluir las posiciones 5 a 7 de esta referencia
        'Entre las posiciones 8 y 35, eliminar todos los espacios y caracteres no alfanuméricos. Esto es: “/ - ? : ( ) . , ' +”.
        'Añadir el código ISO del país, y ‘00’ a la derecha, y
        'Convertir las letras en dígitos, de acuerdo a la tabla de conversión 1
        'Aplicar el sistema de dígitos de control MOD 97-10.

        'Excluir las posiciones 5 a 7 de esta referencia (les del sufixe)
        '(=seleccionar las primeras 5 posiciones, justo antes de los dígitos de control que intentamos calcular)
        Dim tmp1 As String = sSource.Substring(0, 4)

        'Entre las posiciones 8 y 35, eliminar todos los espacios y caracteres no alfanuméricos. Esto es: “/ - ? : ( ) . , ' +”.
        Dim sAllowedChars As String = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789"
        Dim sb2 As New System.Text.StringBuilder
        'sb2.Append(tmp1)
        For i As Integer = 7 To sSource.Length - 1
            Dim sChar As String = sSource.Substring(i, 1)
            If sAllowedChars.Contains(sChar) Then
                sb2.Append(sChar)
            End If
        Next
        Dim tmp2 As String = sb2.ToString

        'Añadir el código ISO del país, y ‘00’ a la derecha
        Dim sIsoPais As String = sSource.Substring(0, 2)
        Dim tmp3 As String = tmp2 & sIsoPais & "00"

        'Convertir las letras en dígitos, de acuerdo a la tabla de conversión 1
        Dim sb4 As New System.Text.StringBuilder
        Dim sLetras As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        For i As Integer = 0 To tmp3.Length - 1
            Dim sChar As String = tmp3.Substring(i, 1)
            If sLetras.Contains(sChar) Then
                Dim iDigit As Integer = sLetras.IndexOf(sChar) + 10
                sb4.Append(iDigit.ToString())
            Else
                sb4.Append(sChar)
            End If
        Next
        Dim tmp4 As String = sb4.ToString

        'Aplicar el sistema de dígitos de control MOD 97-10 (ISO 7604).
        Dim dblTmp4 As Double = CDbl(tmp4)
        Dim iMod97 As Integer = dblTmp4 Mod 97
        'Restamos el módulo obtenido de la cifra 98
        Dim iDiferencia As Integer = 98 - iMod97
        'Si da un solo digito le ponemos un cero delante
        Dim retval As String = Format(iDiferencia, "00")

        Return retval
    End Function



    Public Function Filename() As String
        Dim s As String = DTOIban.Formated(_Iban) & ".pdf"
        Return s
    End Function

End Class


