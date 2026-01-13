Public Class PdfAlb2
    Private mPdf As Object
    Private mBaseImponible As DTOAmt

    Private mDetallTop As Integer = 260
    Private mGapSegonesPlanes As Integer = 90
    Private mBackBrush = System.Drawing.Brushes.Gray

    Private mPen As Pen = Pens.Gray
    Private mFont As New Font("Arial", 10, FontStyle.Regular)
    Private mLineHeight As Integer = 13
    Private mFontEpigrafe As New Font("Arial", 9, FontStyle.Regular)
    Private mLineHeightEpigrafe As Integer = 11
    Private mLeft As Integer = 60
    Private mRight As Integer
    Private mBottom As Integer

    Private mTabs(12) As Integer
    Private mLang As DTOLang
    Private mArrastre As DTOAmt
    'Private mCur As DTOCur
    Private mCurrentDoc As DTODoc
    Private mFraPageCount As Integer
    Private mPageType As PageTypes
    Private mFirstPage As Boolean = True
    Private mFileName As String
    Private mLabelTachado As Boolean
    Private mLabelExport As Boolean
    Private mLabelRectificativa As Boolean
    Private mDrawPijama As Boolean = True

    Private mSigned As Boolean

    Dim Y As Integer


    Public Enum PageTypes
        Portada
        SegonesPlanes
    End Enum

    Public Enum EndRows
        Sumaysigue
        SumaAnterior
        Suma
        Dto
        Parcial
        DtoPunts
        Dpp
        BaseImponible
        IVAstd
        IVAred
        IVAsuperRed
        REQstd
        REQred
        REQsuperRed
        Total
        Custom
    End Enum

    Private Enum TabCols
        Ref0
        RefF
        Dsc0
        DscF
        Qty0
        QtyF
        Pvp0
        PvpF
        Dto0
        DtoF
        Amt0
        AmtF
    End Enum

    Public Sub New(ByVal oPurchaseOrders As List(Of DTOPurchaseOrder), Optional ByVal BlSigned As Boolean = False, Optional ByVal BlProforma As Boolean = False, Optional ByVal SaveAsFileName As String = "")
        'mPdf = New C1.C1Pdf.C1PdfDocument(Printing.PaperKind.A4)
        Dim exs As New List(Of Exception)
        If oPurchaseOrders.Count > 0 Then
            Dim oFirstOrder As DTOPurchaseOrder = oPurchaseOrders.First
            If oFirstOrder.fch < CDate("1/11/2007") Then
                mPdf = New PdfCorpTemplateStGervasi(PdfCorpTemplateStGervasi.Templates.invoice)
            Else
                mPdf = New PdfCorpTemplateDiagonal(PdfCorpTemplateStGervasi.Templates.invoice)
            End If

            mRight = mPdf.PageRectangle.Width - 20
            mBottom = mPdf.PageRectangle.Bottom - 20
            mSigned = BlSigned


            'Dim oDoc As Doc

        End If
        'mFileName = Save(SaveAsFileName, BlSigned)
    End Sub




    Public Function Stream(exs As List(Of Exception), Optional oCert As DTOCert = Nothing) As Byte()
        'C1PdfHelper.Stream necessita Ghostscript
        Dim retval As Byte() = Nothing
        If oCert IsNot Nothing Then
            If exs.Count = 0 Then
                retval = DirectCast(mPdf, C1PdfHelper.Document).SignedStream(exs, oCert.Stream, oCert.Pwd)
            End If
        Else
            retval = DirectCast(mPdf, C1PdfHelper.Document).Stream(exs)
        End If
        Return retval
    End Function


    Public ReadOnly Property FileName() As String
        Get
            Return mFileName
        End Get
    End Property

    Public Sub PrintDoc(ByVal oCurrentDoc As DTODoc, Optional iCopias As Integer = 0)
        mCurrentDoc = oCurrentDoc

        SetDetailTabWidths()

        Dim oLegacyBackColor = oCurrentDoc.BackColor
        mBackBrush = New Pen(oLegacyBackColor).Brush
        mLang = mCurrentDoc.Lang

        If mLabelExport Then
            mCurrentDoc.Estilo = DTODoc.Estilos.Proforma
            'mCurrentDoc.Incoterm = Doc.incoterms.CIF
            If iCopias = 0 Then iCopias = 5
        Else
            'mCurrentDoc.Estilo = DTODoc.Estilos.Albara
            If iCopias = 0 Then iCopias = 1
        End If

        Dim iCopia As Integer
        For iCopia = 1 To iCopias
            mFraPageCount = 0
            mPageType = PageTypes.Portada
            NewPage()
            mArrastre = DTOAmt.Empty(oCurrentDoc.Cur)
            PrintDetall()
            If mArrastre.Eur <> 0 Or mArrastre.Val <> 0 Then
                PrintTotals()
            End If
            PrintCustom()
        Next
    End Sub


    Private Sub PrintDetall()
        Dim X As Integer = 75
        Dim oImport As DTOAmt
        Dim mTot As Decimal
        Dim sTxt As String

        Dim oItm As DTODocItm
        Dim iLine As Integer = 0
        For Each oItm In mCurrentDoc.Itms
            CheckEndPage(1)
            oImport = DTOAmt.Import(oItm.Qty, oItm.Preu, oItm.Dto)
            mArrastre.Add(oImport)

            If oItm.Ref > "" Or oItm.Qty > 0 Then
                Select Case mCurrentDoc.Estilo
                    Case DTODoc.Estilos.Comanda ', DTODoc.Estilos.Proforma
                        If oItm.Qty > 0 Then
                            iLine += 1
                            DrawRef(Format(iLine, "000"))
                        End If

                    Case Else
                        DrawRef(oItm.Ref)
                End Select
            End If

            sTxt = oItm.Text
            If sTxt > "" Then
                Select Case oItm.Ref
                    Case "6395"
                        sTxt = mLang.Tradueix("mano de obra", "ma d'obra", "handwork")
                    Case "14165"
                        sTxt = mLang.Tradueix("portes recogida y/o entrega", "ports recollida i/o entrega", "transport way and/or way back")
                End Select
                DrawText(sTxt, oItm.FontStyle, oItm.LeftPadChars, IIf(oItm.Qty = 0, False, (oItm.Qty <> 0)))
            End If

            If oItm.Qty <> 0 Then DrawQty(oItm.Qty)
            If oItm.Preu IsNot Nothing Then DrawPreu(oItm.Preu)
            If oItm.Dto <> 0 Then DrawItmDto(oItm.Dto)

            If oImport IsNot Nothing Then
                If oImport.IsNotZero Then
                    DrawImport(oImport)
                    mTot = mTot + oImport.val
                End If
            End If
            If mCurrentDoc.DisplayPunts And oItm.Punts > 0 Then
                DrawItmPunts(oItm.Punts)
            End If
            Y += mLineHeight
        Next
    End Sub

    Private Sub PrintTotals()

        If mCurrentDoc.Dto <> 0 Or mCurrentDoc.Dpp <> 0 Or mCurrentDoc.PuntsTipus <> 0 Then
            CheckEndPage(3)
            DrawRayaSum()
            DrawRowEnd(EndRows.Suma)
            If mCurrentDoc.Dto <> 0 Then
                DrawRowEnd(EndRows.Dto, mCurrentDoc.Dto)
                If mCurrentDoc.Dpp <> 0 Or mCurrentDoc.PuntsTipus <> 0 Then
                    DrawRayaSum()
                    DrawRowEnd(EndRows.Parcial)
                End If
            End If

            If mCurrentDoc.PuntsTipus <> 0 Then
                DrawRowEnd(EndRows.DtoPunts, mCurrentDoc.PuntsTipus, mCurrentDoc.PuntsQty, mCurrentDoc.PuntsBase.Val, mCurrentDoc.Fch.Month)
                If mCurrentDoc.Dpp <> 0 Then
                    DrawRayaSum()
                    DrawRowEnd(EndRows.Parcial)
                End If
            End If
            If mCurrentDoc.Dpp <> 0 Then
                DrawRowEnd(EndRows.Dpp, mCurrentDoc.Dpp)
            End If
        End If

        If mCurrentDoc.IvaBaseQuotas.Count > 0 Then
            CheckEndPage(3)
            mBaseImponible = mArrastre.Clone
            DrawRayaSum()
            DrawRowEnd(EndRows.BaseImponible)

            For Each oQuota As DTOTaxBaseQuota In mCurrentDoc.IvaBaseQuotas
                DrawRowEnd(oQuota)
            Next
        End If


        CheckEndPage(1)
        DrawRayaSum()
        DrawRowEnd(EndRows.Total)
    End Sub

    Private Sub PrintCustom()
        If mCurrentDoc.CustomLines IsNot Nothing Then
            For Each s As String In mCurrentDoc.CustomLines
                DrawRowEnd(EndRows.Custom, s)
            Next
        End If
    End Sub


    Private Sub DrawRayaSum()
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far
        Dim s As String = "_______"
        Dim oRc As New RectangleF(mTabs(TabCols.Amt0), Y - 8, mTabs(TabCols.AmtF) - mTabs(TabCols.Amt0), mFont.Height)
        mPdf.DrawString(s, mFont, Brushes.Black, oRc, sF)
        Y += mLineHeight / 2
    End Sub

    Private Sub PrintHeader()
        'membrete i fondo
        'DrawLogo(60, 20)
        'DrawNom(190, 20)
        'DrawDadesRegistrals(100, 100)

        If mCurrentDoc.WriteTemplate Then
            DrawHeaderOutline(100)
            DrawDetailOutline(IIf(mPageType = PageTypes.Portada, mDetallTop, mDetallTop - mGapSegonesPlanes), True)
        End If


        Dim s As String

        'adreça
        Dim oRc As RectangleF
        Dim X As Integer = mLeft + 10
        Y = 120
        If mPageType = PageTypes.Portada Then
            For Each s In mCurrentDoc.Dest
                oRc = New RectangleF(X, Y, mPdf.PageRectangle.Width, mFont.Height)
                mPdf.DrawString(s, mFont, Brushes.Black, oRc)
                Y += mLineHeight
            Next
        Else
            oRc = New RectangleF(X, Y, mPdf.PageRectangle.Width, mFont.Height)
            mPdf.DrawString(mCurrentDoc.Dest(0), mFont, Brushes.Black, oRc)
        End If

        Y = 120
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center

        'data
        s = Format(mCurrentDoc.Fch, "dd/MM/yy")
        X = mLeft + CInt((mRight - mLeft) / 2)
        oRc = New RectangleF(X, Y, CInt((mRight - mLeft) / 4), mFont.Height)
        mPdf.DrawString(s, mFont, Brushes.Black, oRc, sF)

        'numero
        s = mCurrentDoc.Num
        If mFraPageCount > 1 Then s = s & " (pag." & mFraPageCount & ")"
        X += CInt((mRight - mLeft) / 4)
        oRc = New RectangleF(X, Y, CInt((mRight - mLeft) / 4), mFont.Height)
        mPdf.DrawString(s, mFont, Brushes.Black, oRc, sF)

        'observacions
        If mPageType = PageTypes.Portada Then
            Y = 150
            X = mLeft + CInt((mRight - mLeft) / 2) + 10
            For Each s In mCurrentDoc.Obs
                oRc = New RectangleF(X, Y, (mRight - mLeft / 2), mFont.Height)
                mPdf.DrawString(s, mFont, Brushes.Black, oRc)
                Y += mLineHeight
            Next
        End If

    End Sub

    Public Function CheckEndPage(ByVal iMinLinesToEnd As Integer) As Boolean
        Dim retval As Boolean
        Dim iLinesToEnd As Integer = (mBottom - Y) / mLineHeight
        Dim BlValorat As Boolean = mArrastre.Eur <> 0
        If iLinesToEnd <= (iMinLinesToEnd + 1) Then 'iMinLinesToEnd trepitja la ratlla
            If BlValorat Then DrawRowEnd(EndRows.Sumaysigue)
            NewPage()
            retval = True
            If BlValorat Then DrawRowEnd(EndRows.SumaAnterior)
        End If
        Return retval
    End Function

    Private Sub NewPage()
        If mFirstPage Then
            mFirstPage = False
        Else
            mPdf.NewPage()
            mPdf.drawpage()
        End If

        If mFraPageCount = 0 Then
            mPageType = PageTypes.Portada
        Else
            mPageType = PageTypes.SegonesPlanes
        End If

        mFraPageCount += 1

        PrintHeader()

        Y = mDetallTop + 20
        If mPageType = PageTypes.SegonesPlanes Then Y -= mGapSegonesPlanes
        If mLabelTachado Then TachaPage()
        If mLabelExport Then LabelExport()

        If mCurrentDoc.SideLabel <> DTODoc.SideLabels.None Then SideLabel()
    End Sub

    Private Sub TachaPage()
        Dim oPen As New Pen(Color.Red, 3)
        Dim oRc As RectangleF = mPdf.PageRectangle
        mPdf.DrawLine(oPen, oRc.Left, oRc.Top, oRc.Right, oRc.Bottom)
    End Sub

    Private Sub SideLabel()
        Dim s As String = ""
        Dim oBrush As Brush = mBackBrush
        Select Case mCurrentDoc.SideLabel
            Case DTODoc.SideLabels.FacturaRectificativa
                s = "factura rectificativa"
                oBrush = Brushes.Red
        End Select
        Dim oFont As New Font("Arial", 16, FontStyle.Bold)

        Dim iX As Integer = mPdf.PageRectangle.Right - 21
        Dim oRc As New RectangleF(iX, mDetallTop, 16, mBottom - mDetallTop)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Near
        sF.FormatFlags = StringFormatFlags.DirectionVertical
        mPdf.DrawString(s, oFont, oBrush, oRc, sF)
    End Sub

    Private Sub LabelExport()
        Dim s As String = "exportación"
        Dim oFont As New Font("Arial", 16, FontStyle.Bold)
        Dim iX As Integer = mPdf.PageRectangle.Right - 21
        Dim oRc As New RectangleF(iX, mDetallTop, 16, mBottom - mDetallTop)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Near
        sF.FormatFlags = StringFormatFlags.DirectionVertical
        mPdf.DrawString(s, oFont, mBackBrush, oRc, sF)
    End Sub

    Private Sub DrawRowEnd(ByVal oQuota As DTOTaxBaseQuota)
        Select Case oQuota.Tax.Codi
            Case DTOTax.Codis.Iva_Standard
                DrawRowEnd(EndRows.IVAstd, DTOTax.ClosestTipus(DTOTax.Codis.Iva_Standard), oQuota.baseImponible.Eur)
            Case DTOTax.Codis.Iva_Reduit
                DrawRowEnd(EndRows.IVAred, DTOTax.ClosestTipus(DTOTax.Codis.Iva_Reduit), oQuota.baseImponible.Eur)
            Case DTOTax.Codis.Iva_SuperReduit
                DrawRowEnd(EndRows.IVAsuperRed, DTOTax.ClosestTipus(DTOTax.Codis.Iva_SuperReduit), oQuota.baseImponible.Eur)
            Case DTOTax.Codis.Recarrec_Equivalencia_Standard
                DrawRowEnd(EndRows.REQstd, DTOTax.ClosestTipus(DTOTax.Codis.Recarrec_Equivalencia_Standard), oQuota.baseImponible.Eur)
            Case DTOTax.Codis.Recarrec_Equivalencia_Reduit
                DrawRowEnd(EndRows.REQred, DTOTax.ClosestTipus(DTOTax.Codis.Recarrec_Equivalencia_Reduit), oQuota.baseImponible.Eur)
            Case DTOTax.Codis.Recarrec_Equivalencia_SuperReduit
                DrawRowEnd(EndRows.REQsuperRed, DTOTax.ClosestTipus(DTOTax.Codis.Recarrec_Equivalencia_SuperReduit), oQuota.baseImponible.Eur)
        End Select
    End Sub


    Public Sub DrawRowEnd(ByVal oEndRow As EndRows, ByVal ParamArray sStrings() As String)
        Dim sText As String = ""
        Dim oAmt As DTOAmt = mArrastre.Clone
        Select Case oEndRow
            Case EndRows.SumaAnterior
                sText = mLang.Tradueix("Suma anterior", "Suma anterior", "Above sum")
            Case EndRows.Sumaysigue
                sText = mLang.Tradueix("Suma y sigue", "Suma i segueix", "Sum and follow")
            Case EndRows.Suma
                sText = mLang.Tradueix("Importe", "Import", "Amt")
            Case EndRows.Parcial
                sText = mLang.Tradueix("Suma parcial", "Suma parcial", "Parcial Amt")
            Case EndRows.BaseImponible
                sText = mLang.Tradueix("Base Imponible", "Base Imponible", "Tax base")
            Case EndRows.Dto
                Dim DcDto As Decimal = sStrings(0)
                sText = mLang.Tradueix("Descuento", "Descompte", "Discount")
                sText = sText & " " & DcDto & "%"
                oAmt = mArrastre.Percent(-DcDto)
                mArrastre.Add(oAmt)
            Case EndRows.DtoPunts
                Dim DcPuntsDto As Decimal = sStrings(0)
                Dim DcPuntsQty As Decimal = sStrings(1)
                Dim oPuntsBas = DTOAmt.Factory(CDbl(sStrings(2)), mArrastre.Cur, CDbl(sStrings(2)))
                Dim IntMes As Integer = sStrings(3)
                sText = mLang.Tradueix("Dto", "Dte", "Dct") & " "
                sText = sText & DcPuntsDto & "% " & mLang.Tradueix("por", "per", "for") & " "
                sText = sText & DcPuntsQty & " " & mLang.Tradueix("puntos s/Eur", "punts sobre Eur", "bonus points over Eur")
                sText = sText & " " & oPuntsBas.Formatted & " "
                sText = sText & DTOLang.Mes(mLang, IntMes)
                oAmt = oPuntsBas.Percent(-DcPuntsDto)
                mArrastre.Add(oAmt)
            Case EndRows.Dpp
                Dim DcDpp As Decimal = sStrings(0)
                sText = mLang.Tradueix("Descuento prontopago", "Descompte prontopago", "Discount cash")
                sText = sText & " " & DcDpp & "%"
                oAmt = mArrastre.Percent(-DcDpp)
                mArrastre.Add(oAmt)
            Case EndRows.IVAstd
                Dim DcIVA As Decimal = sStrings(0)
                Dim sTipus As String = Format(DcIVA, IIf(Decimal.Truncate(DcIVA) = DcIVA, "0", "0.00"))
                Dim oBase = DTOAmt.Factory(sStrings(1), mArrastre.Cur, sStrings(1))
                sText = mLang.Tradueix("IVA", "IVA", "VAT")
                sText = sText & " " & sTipus & "%"
                If oBase.Val <> mBaseImponible.Val Then
                    sText = sText & " " & mLang.Tradueix("sobre", "sobre", "on") & " "
                    sText = sText & DTOAmt.CurFormatted(oBase)
                End If
                oAmt = oBase.Percent(DcIVA)
                mArrastre.Add(oAmt)
            Case EndRows.IVAred
                Dim DcIVA As Decimal = sStrings(0)
                Dim sTipus As String = Format(DcIVA, IIf(Decimal.Truncate(DcIVA) = DcIVA, "0", "0.00"))
                Dim oBase = DTOAmt.Factory(sStrings(1), mArrastre.Cur, sStrings(1))
                sText = mLang.Tradueix("IVA reducido", "IVA reduit", "reduced VAT")
                sText = sText & " " & sTipus & "%"
                If oBase.Val <> mBaseImponible.Val Then
                    sText = sText & " " & mLang.Tradueix("sobre", "sobre", "on") & " "
                    sText = sText & DTOAmt.CurFormatted(oBase)
                End If
                oAmt = oBase.Percent(DcIVA)
                mArrastre.Add(oAmt)
            Case EndRows.IVAsuperRed
                Dim DcIVA As Decimal = sStrings(0)
                Dim sTipus As String = Format(DcIVA, IIf(Decimal.Truncate(DcIVA) = DcIVA, "0", "0.00"))
                Dim oBase = DTOAmt.Factory(sStrings(1), mArrastre.Cur, sStrings(1))
                sText = mLang.Tradueix("IVA super reducido", "IVA super reduit", "super reduced VAT")
                sText = sText & " " & sTipus & "%"
                If oBase.Val <> mBaseImponible.Val Then
                    sText = sText & " " & mLang.Tradueix("sobre", "sobre", "on") & " "
                    sText = sText & DTOAmt.CurFormatted(oBase)
                End If
                oAmt = oBase.Percent(DcIVA)
                mArrastre.Add(oAmt)
            Case EndRows.REQstd, EndRows.REQred, EndRows.REQsuperRed
                Dim DcREQ As Decimal = sStrings(0)
                Dim sTipus As String = Format(DcREQ, IIf(Decimal.Truncate(DcREQ) = DcREQ, "0", "0.00"))
                Dim oBase = DTOAmt.Factory(sStrings(1), mArrastre.Cur, sStrings(1))
                sText = mLang.Tradueix("Recargo de equivalencia", "Recarrec d'equivalencia", "VAT add-on")
                sText = sText & " " & sTipus & "%"
                If oBase.Val <> mBaseImponible.Val Then
                    sText = sText & " " & mLang.Tradueix("sobre", "sobre", "on") & " "
                    sText = sText & DTOAmt.CurFormatted(oBase)
                End If
                oAmt = oBase.Percent(DcREQ)
                mArrastre.Add(oAmt)
            Case EndRows.Total
                If mCurrentDoc.incoterm Is Nothing Then
                    sText = "Total"
                Else
                    sText = String.Format("Total {0}", mCurrentDoc.incoterm.Id.ToString())
                End If

                sText = sText & " " & mArrastre.cur.tag

                If mCurrentDoc.IvaBaseQuotas.Count = 0 And oAmt.IsNotZero Then
                    sText = sText & " " & mLang.Tradueix("Exento de IVA", "Exempt de IVA", "VAT exempt", "Exento de IVA")
                End If
            Case EndRows.Custom
                sText = sStrings(0)
                oAmt = DTOAmt.Empty
        End Select

        'Dim sAmt As String
        'If Not oAmt Is Nothing Then sAmt = IIf(oAmt.Val = 0, "", oAmt.Formatted)
        DrawText(sText, FontStyle.Regular, 4)
        If Not (oEndRow = EndRows.Custom And oAmt.IsZero) Then
            DrawImport(oAmt.val)
        End If
        Y += mLineHeight
        'Dim DcIndent As Decimal = mIndentLevelRowEnd * mDcIndentGap
        'MyBase.Sections(Secs.body).DrawRow(DcIndent, sText, "", "", "", sAmt)
    End Sub



    Private Sub DrawRef(ByVal sText As String)
        Dim oRc As New RectangleF(mTabs(TabCols.Ref0), Y, mTabs(TabCols.RefF) - mTabs(TabCols.Ref0), mFont.Height)
        mPdf.DrawString(sText, mFont, Brushes.Black, oRc)
    End Sub


    Private Sub DrawText(ByVal sText As String, ByVal oFontStyle As FontStyle, Optional ByVal Offset As Integer = 0, Optional ByVal CortaTexto As Boolean = False)
        Dim oFont As New Font(mFont.Name, mFont.Size, oFontStyle)

        Dim iWidth As Integer
        If CortaTexto Then
            iWidth = mTabs(TabCols.DscF) - mTabs(TabCols.Dsc0)
        Else
            iWidth = mTabs(TabCols.AmtF) - mTabs(TabCols.Dsc0)
        End If

        Dim oRc As New RectangleF(mTabs(TabCols.Dsc0), Y, iWidth, mFont.Height)
        If Offset > 0 Then sText = New String(" ", Offset) & sText
        mPdf.DrawString(sText, oFont, Brushes.Black, oRc)
    End Sub

    Private Sub DrawQty(ByVal IntQty As Integer)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far
        Dim s As String = Format(IntQty, "#,###")
        Dim iQty0 As Integer = mTabs(TabCols.Qty0)
        Dim iQtyF As Integer = mTabs(TabCols.QtyF)
        If Not mCurrentDoc.DtoColumnDisplay Then
            Dim iDtoWidth As Integer = mTabs(TabCols.DtoF) - mTabs(TabCols.Dto0)
            iQty0 += iDtoWidth
            iQtyF += iDtoWidth
        End If
        Dim oRc As New RectangleF(iQty0, Y, iQtyF - iQty0, mFont.Height)
        mPdf.DrawString(s, mFont, Brushes.Black, oRc, sF)
    End Sub

    Private Sub DrawPreu(ByVal oPreu As DTOAmt)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far
        Dim s As String = oPreu.Formatted
        Dim iPvp0 As Integer = mTabs(TabCols.Pvp0)
        Dim iPvpF As Integer = mTabs(TabCols.PvpF)
        If Not mCurrentDoc.DtoColumnDisplay Then
            Dim iDtoWidth As Integer = mTabs(TabCols.DtoF) - mTabs(TabCols.Dto0)
            iPvp0 += iDtoWidth
            iPvpF += iDtoWidth
        End If
        Dim oRc As New RectangleF(iPvp0, Y, iPvpF - iPvp0, mFont.Height)
        mPdf.DrawString(s, mFont, Brushes.Black, oRc, sF)
    End Sub

    Private Sub DrawPreu(ByVal DblPreu As Decimal)
        'to deprecate? substituit per oAmt
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far
        Dim s As String = Format(DblPreu, "#,##0.00")
        Dim iPvp0 As Integer = mTabs(TabCols.Pvp0)
        Dim iPvpF As Integer = mTabs(TabCols.PvpF)
        If Not mCurrentDoc.DtoColumnDisplay Then
            Dim iDtoWidth As Integer = mTabs(TabCols.DtoF) - mTabs(TabCols.Dto0)
            iPvp0 += iDtoWidth
            iPvpF += iDtoWidth
        End If
        Dim oRc As New RectangleF(iPvp0, Y, iPvpF - iPvp0, mFont.Height)
        mPdf.DrawString(s, mFont, Brushes.Black, oRc, sF)
    End Sub

    Private Sub DrawItmDto(ByVal SngDto As Decimal)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far

        Dim s As String = ""
        If SngDto = Fix(SngDto) Then
            s = Fix(SngDto) & "%"
        Else
            s = SngDto.ToString & "%"
        End If

        Dim oRc As New RectangleF(mTabs(TabCols.Dto0), Y, mTabs(TabCols.DtoF) - mTabs(TabCols.Dto0), mFont.Height)
        mPdf.DrawString(s, mFont, Brushes.Black, oRc, sF)
    End Sub

    Private Sub DrawItmPunts(ByVal SngPunts As Decimal)
    End Sub

    Private Sub DrawImport(ByVal oImport As DTOAmt)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far
        Dim s As String = oImport.Formatted
        Dim oRc As New RectangleF(mTabs(TabCols.Amt0), Y, mTabs(TabCols.AmtF) - mTabs(TabCols.Amt0), mFont.Height)
        mPdf.DrawString(s, mFont, Brushes.Black, oRc, sF)
    End Sub

    Private Sub DrawImport(ByVal DblImport As Decimal)
        'to deprecate? substituit per Amt
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far
        Dim s As String = Format(DblImport, "#,##0.00")
        Dim oRc As New RectangleF(mTabs(TabCols.Amt0), Y, mTabs(TabCols.AmtF) - mTabs(TabCols.Amt0), mFont.Height)
        mPdf.DrawString(s, mFont, Brushes.Black, oRc, sF)
    End Sub


    Private Sub DrawColumnaDetall(ByVal s As String, ByVal iDesde As Integer, ByVal iHasta As Integer, ByVal oAlign As StringAlignment)
        Dim iY As Integer = GetDetallTop()
        If iDesde <> mTabs(0) Then
            Dim iX As Integer = iDesde - 2
            'mPdf.DrawLine(mPen, iX, iY, iX, mBottom)
            mPdf.DrawLine(Pens.White, iX, iY, iX, mBottom)
        End If

        Dim oRc As New Rectangle(iDesde, iY, iHasta - iDesde, mFontEpigrafe.Height)

        Dim sF As New StringFormat()
        sF.Alignment = oAlign

        mPdf.DrawString(s, mFontEpigrafe, Brushes.Black, oRc, sF)
    End Sub

    Private Function GetDetallTop() As Integer
        Select Case mPageType
            Case PageTypes.Portada
                Return mDetallTop
            Case Else
                Return mDetallTop - mGapSegonesPlanes
        End Select
    End Function


    Private Sub DrawHeaderOutline(ByVal iRectangleTop As Integer)
        Dim iPortadaHeight As Integer = 130
        Dim iSegonesPlanesHeight As Integer = 40

        Dim iHeight As Integer = IIf(mPageType = PageTypes.Portada, iPortadaHeight, iSegonesPlanesHeight)

        'tot el rectangle header
        Dim oRc As New RectangleF(mLeft, iRectangleTop, mRight - mLeft, iHeight)
        mPdf.DrawRectangle(mPen, oRc)

        'linia mitja vertical
        Dim iMidWidth As Integer = CInt(oRc.Width / 2)
        mPdf.DrawLine(mPen, mRight - iMidWidth, oRc.Y, mRight - iMidWidth, oRc.Bottom)

        'linia mitja horitzontal
        If mPageType = PageTypes.Portada Then
            mPdf.DrawLine(mPen, mRight - iMidWidth, oRc.Y + iSegonesPlanesHeight, mRight, oRc.Y + iSegonesPlanesHeight)
        End If

        'epigraf cuadre
        Dim oRectEpigrafe As New RectangleF(mRight - iMidWidth + 1, oRc.Y + 1, iMidWidth - 2, mLineHeightEpigrafe)
        mPdf.FillRectangle(mBackBrush, oRectEpigrafe)

        'separador vertical data/numero de document
        Dim iQuarterWidth As Integer = CInt(iMidWidth / 2)
        mPdf.DrawLine(mPen, mRight - iQuarterWidth, oRc.Y, mRight - iQuarterWidth, oRc.Y + iSegonesPlanesHeight)

        'epigraf titols
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center
        Dim s As String = mCurrentDoc.Lang.Tradueix("Fecha", "Data", "Date", "Data")
        Dim oRcFch As New Rectangle(oRectEpigrafe.X, oRectEpigrafe.Y, oRectEpigrafe.Width / 2, oRectEpigrafe.Height)
        mPdf.DrawString(s, mFontEpigrafe, Brushes.Black, oRcFch, sF)
        s = LabelNum(mCurrentDoc)
        Dim oRcNum As New Rectangle(oRectEpigrafe.X + oRectEpigrafe.Width / 2, oRectEpigrafe.Y, oRectEpigrafe.Width / 2, oRectEpigrafe.Height)
        mPdf.DrawString(s, mFontEpigrafe, Brushes.Black, oRcNum, sF)

    End Sub

    Shared Function LabelNum(oDoc As DTODoc) As String
        Dim sRetVal As String = ""
        Select Case oDoc.Estilo
            Case DTODoc.Estilos.Comanda
                sRetVal = oDoc.Lang.Tradueix("Pedido", "Comanda", "Order", "Encomenda")
            Case DTODoc.Estilos.Albara
                sRetVal = oDoc.Lang.Tradueix("Albarán", "Albarà", "Delivery", "Alvará")
            Case DTODoc.Estilos.Factura
                Select Case oDoc.SideLabel
                    Case DTODoc.SideLabels.FacturaRectificativa
                        sRetVal = oDoc.Lang.Tradueix("Factura Rectificativa", "Factura Rectificativa", "Corrective Invoice", "Fatura Rectificativa")
                    Case Else
                        sRetVal = oDoc.Lang.Tradueix("Factura", "Factura", "Invoice", "Fatura")
                End Select
            Case DTODoc.Estilos.Proforma
                sRetVal = "Proforma"
        End Select
        Return sRetVal
    End Function

    Private Sub DrawDetailOutline(ByVal iRectangleTop As Integer, ByVal BlDescompte As Boolean)
        If mDrawPijama Then
            DrawPijama()
        End If

        Dim oRc As New RectangleF(mLeft, iRectangleTop, mRight - mLeft, mBottom - iRectangleTop)
        mPdf.DrawRectangle(Pens.Gray, oRc)

        Dim oRectEpigrafe As New RectangleF(mLeft + 1, oRc.Y + 1, oRc.Width - 2, mLineHeightEpigrafe)
        mPdf.FillRectangle(mBackBrush, oRectEpigrafe)

        Dim s As String = ""
        Select Case mCurrentDoc.Estilo
            Case DTODoc.Estilos.Comanda ', DTODoc.Estilos.Proforma
                s = "Lin"
            Case Else
                s = mLang.Tradueix("n/ref", "n/ref", "our ref")
        End Select
        DrawColumnaDetall(s, mTabs(TabCols.Ref0), mTabs(TabCols.RefF), StringAlignment.Near)


        Dim iDtoWidthSuplement As Integer = 0
        If Not mCurrentDoc.DtoColumnDisplay Then
            iDtoWidthSuplement = mTabs(TabCols.DtoF) - mTabs(TabCols.Dto0)
        End If

        s = mCurrentDoc.Lang.Tradueix("Concepto", "Concepte", "Concept", "Designaçao")
        DrawColumnaDetall(s, mTabs(TabCols.Dsc0), mTabs(TabCols.DscF) + iDtoWidthSuplement, StringAlignment.Near)
        s = mCurrentDoc.Lang.Tradueix("Cant", "Quant", "Qty", "Quant")
        DrawColumnaDetall(s, mTabs(TabCols.Qty0) + iDtoWidthSuplement, mTabs(TabCols.QtyF) + iDtoWidthSuplement, StringAlignment.Far)
        s = mCurrentDoc.Lang.Tradueix("Precio", "Preu", "Price", "Preço")
        DrawColumnaDetall(s, mTabs(TabCols.Pvp0) + iDtoWidthSuplement, mTabs(TabCols.PvpF) + iDtoWidthSuplement, StringAlignment.Far)

        If mCurrentDoc.DtoColumnDisplay Then
            s = mCurrentDoc.Lang.Tradueix("Dto", "Dte", "Dsct", "Dtos")
            DrawColumnaDetall(s, mTabs(TabCols.Dto0), mTabs(TabCols.DtoF), StringAlignment.Far)
        End If
        s = mCurrentDoc.Lang.Tradueix("Importe", "Import", "Amount", "Montante")
        DrawColumnaDetall(s, mTabs(TabCols.Amt0), mTabs(TabCols.AmtF), StringAlignment.Far)

    End Sub

    Public Sub DirectSave(ByVal sSaveAsFileName As String)
        mPdf.Save(sSaveAsFileName)
    End Sub

    Private Sub DrawPijama()
        Dim Yn As Integer = mDetallTop + 20
        If mPageType = PageTypes.SegonesPlanes Then Yn -= mGapSegonesPlanes
        Dim oBrush As New SolidBrush(Color.FromArgb(230, 230, 230))
        Do While Yn < mBottom - mLineHeight
            mPdf.fillRectangle(oBrush, New RectangleF(mLeft, Yn, mRight - mLeft, mLineHeight))
            Yn += 2 * mLineHeight
        Loop
    End Sub

    Private Sub SetDetailTabWidths(Optional BlMostrarEANenFactura As Boolean = False)
        mTabs(TabCols.Ref0) = 65
        mTabs(TabCols.RefF) = 97
        mTabs(TabCols.Dsc0) = 103
        mTabs(TabCols.DscF) = 397
        mTabs(TabCols.Qty0) = 400
        mTabs(TabCols.QtyF) = 429
        mTabs(TabCols.Pvp0) = 432
        mTabs(TabCols.PvpF) = 482
        mTabs(TabCols.Dto0) = 490
        mTabs(TabCols.DtoF) = 515
        mTabs(TabCols.Amt0) = 520
        'mTabs(TabCols.AmtF) = 575
        mTabs(TabCols.AmtF) = 572

        If BlMostrarEANenFactura Then
            '13 digits en lloc de 5: 51 punts adicionals
            mTabs(TabCols.RefF) = 97 + 42
            mTabs(TabCols.Dsc0) = 103 + 42
        End If
    End Sub
End Class
