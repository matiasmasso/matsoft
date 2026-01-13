Public Class PdfTemplateForm
    Inherits PdfTemplateMembrete

    Private mEmp As DTOEmp
    Private mBaseImponible As DTOAmt

    Private mDetallTop As Integer = 260
    Private mGapSegonesPlanes As Integer = 90
    Private mBackBrush As Brush = Brushes.Gray

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
    Private mCur As DTOCur
    Private mCurrentDoc As DTODoc
    Private mFraPageCount As Integer
    Private mPageType As PageTypes
    Private mFirstPage As Boolean = True
    Private mFileName As String
    Private mDrawPijama As Boolean = True

    Private mSigned As Boolean


    'Dim Y As Integer

    Property ForceValorat As Boolean
    Property LabelTachado As Boolean
    Property LabelExport As Boolean

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

    Public Sub New(oLang As DTOLang)
        MyBase.New(oLang)
        mRight = MyBase.PageRectangle.Width - 20
        mBottom = MyBase.PageRectangle.Bottom - 20
    End Sub

    Protected Sub PrintDoc(ByVal oCurrentDoc As DTODoc, Optional iCopias As Integer = 0)
        mCurrentDoc = oCurrentDoc
        Dim oLegacyColor = mCurrentDoc.BackColor
        mBackBrush = New Pen(oLegacyColor).Brush
        mLang = mCurrentDoc.Lang

        If _LabelExport Then
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
            mArrastre = DTOAmt.empty(mCurrentDoc.Cur)
            PrintDetall()

            If oCurrentDoc.Valorat Or _ForceValorat Or mCurrentDoc.DisplayTotalLogistic Then
                If mArrastre.IsNotZero Or mCurrentDoc.DisplayTotalLogistic Then
                    PrintTotals()
                End If
            End If
        Next
    End Sub

    Private Sub PrintDetall()
        Dim X As Integer = 75
        Dim oImport As DTOAmt = Nothing
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

            Dim iLineHeight As Integer = mLineHeight
            sTxt = oItm.Text
            If sTxt > "" Then
                Select Case oItm.Ref
                    Case "6395"
                        sTxt = mLang.Tradueix("mano de obra", "ma d'obra", "handwork")
                    Case "14165"
                        sTxt = mLang.Tradueix("portes recogida y/o entrega", "ports recollida i/o entrega", "transport way and/or way back")
                End Select
                iLineHeight = DrawText(sTxt, oItm.FontStyle, oItm.LeftPadChars, IIf(oItm.Qty = 0, False, (oItm.Qty <> 0)))
            End If
            If oItm.Qty <> 0 Then DrawQty(oItm.Qty)

            If mCurrentDoc.Valorat Or _ForceValorat Then
                If oItm.Preu IsNot Nothing Then DrawPreu(oItm.Preu)
                If oItm.Dto <> 0 Then DrawItmDto(oItm.Dto)
                If oImport IsNot Nothing Then
                    DrawImport(oImport)
                End If

            End If
            If mCurrentDoc.DisplayPunts And oItm.Punts > 0 Then
                DrawItmPunts(oItm.Punts)
            End If

            Y += iLineHeight
        Next
    End Sub

    Private Sub PrintTotals()

        If mCurrentDoc.Valorat And (mCurrentDoc.Dto <> 0 Or mCurrentDoc.Dpp <> 0 Or mCurrentDoc.PuntsTipus <> 0) Then
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

        If mArrastre.IsNotZero And mCurrentDoc.Valorat And mCurrentDoc.IvaBaseQuotas.Count > 0 Then
            CheckEndPage(3)
            mBaseImponible = mArrastre.Clone
            DrawRayaSum()
            DrawRowEnd(EndRows.BaseImponible)

            For Each oQuota As DTOTaxBaseQuota In mCurrentDoc.IvaBaseQuotas
                DrawRowEnd(oQuota)
            Next
        End If


        CheckEndPage(1)
        If mCurrentDoc.Valorat Then DrawRayaSum()
        DrawRowEnd(EndRows.Total)
    End Sub

    Private Sub DrawRayaSum()
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far
        Dim s As String = "_______"
        Dim oRc As New RectangleF(mTabs(TabCols.Amt0), Y - 8, mTabs(TabCols.AmtF) - mTabs(TabCols.Amt0), mFont.Height)
        Dim oBrush As SolidBrush = IIf(mCurrentDoc.Valorat, Brushes.Black, Brushes.Gray)
        MyBase.DrawString(s, mFont, oBrush, oRc, sF)
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
                oRc = New RectangleF(X, Y, MyBase.PageRectangle.Width, mFont.Height)
                MyBase.DrawString(s, mFont, Brushes.Black, oRc)
                Y += mLineHeight
            Next
        Else
            oRc = New RectangleF(X, Y, MyBase.PageRectangle.Width, mFont.Height)
            MyBase.DrawString(mCurrentDoc.Dest(0), mFont, Brushes.Black, oRc)
        End If

        Y = 120
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center

        'data
        's = mCurrentDoc.Fch.ToShortDateString
        s = Format(mCurrentDoc.Fch, "dd/MM/yy")

        X = mLeft + CInt((mRight - mLeft) / 2)
        oRc = New RectangleF(X, Y, CInt((mRight - mLeft) / 4), mFont.Height)
        MyBase.DrawString(s, mFont, Brushes.Black, oRc, sF)

        'numero
        If mCurrentDoc.Num <> "0" Then
            s = mCurrentDoc.Num
            If mFraPageCount > 1 Then s = s & " (pag." & mFraPageCount & ")"
            X += CInt((mRight - mLeft) / 4)
            oRc = New RectangleF(X, Y, CInt((mRight - mLeft) / 4), mFont.Height)
            MyBase.DrawString(s, mFont, Brushes.Black, oRc, sF)
        End If

        'observacions
        If mPageType = PageTypes.Portada And mCurrentDoc.Estilo <> DTODoc.Estilos.Proforma Then
            Y = 150
            X = mLeft + CInt((mRight - mLeft) / 2) + 10
            For Each s In mCurrentDoc.Obs
                oRc = New RectangleF(X, Y, (mRight - mLeft / 2), mFont.Height)
                MyBase.DrawString(s, mFont, Brushes.Black, oRc)
                Y += mLineHeight
            Next
        End If

    End Sub

    Public Sub CheckEndPage(ByVal iMinLinesToEnd As Integer)
        Dim iLinesToEnd As Integer = (mBottom - Y) / mLineHeight
        Dim BlValorat As Boolean = mArrastre.Eur <> 0
        If iLinesToEnd <= (iMinLinesToEnd + 1) Then 'iMinLinesToEnd trepitja la ratlla
            If BlValorat Then DrawRowEnd(EndRows.Sumaysigue)
            NewPage()
            If BlValorat Then DrawRowEnd(EndRows.SumaAnterior)
        End If
    End Sub

    Private Shadows Sub NewPage()
        If mFirstPage Then
            mFirstPage = False
        Else
            MyBase.NewPage()
            MyBase.DrawPage()
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
        If _LabelTachado Then TachaPage()
        If _LabelExport Then SetLabelExport()
    End Sub

    Private Sub TachaPage()
        Dim oPen As New Pen(Color.Red, 3)
        Dim oRc As RectangleF = MyBase.PageRectangle
        MyBase.DrawLine(oPen, oRc.Left, oRc.Top, oRc.Right, oRc.Bottom)
    End Sub

    Private Sub SetLabelExport()
        Dim s As String = "exportación"
        Dim oFont As New Font("Arial", 16, FontStyle.Bold)
        Dim iX As Integer = MyBase.PageRectangle.Right - 21
        Dim oRc As New RectangleF(iX, mDetallTop, 16, mBottom - mDetallTop)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Near
        sF.FormatFlags = StringFormatFlags.DirectionVertical
        MyBase.DrawString(s, oFont, mBackBrush, oRc, sF)
    End Sub

    Private Sub DrawRowEnd(ByVal oQuota As DTOTaxBaseQuota)
        Select Case oQuota.Tax.Codi
            Case DTOTax.Codis.iva_Standard
                DrawRowEnd(EndRows.IVAstd, oQuota.tax.tipus, oQuota.baseImponible.Eur)
                If mCurrentDoc.recarrecEquivalencia Then
                    Dim oReq As DTOTax = DTOTax.closest(DTOTax.Codis.recarrec_Equivalencia_Standard, oQuota.tax.fch)
                    DrawRowEnd(EndRows.REQstd, oReq.tipus, oQuota.baseImponible.Eur)
                End If
            Case DTOTax.Codis.iva_Reduit
                DrawRowEnd(EndRows.IVAred, oQuota.tax.tipus, oQuota.baseImponible.Eur)
                If mCurrentDoc.recarrecEquivalencia Then
                    Dim oReq As DTOTax = DTOTax.closest(DTOTax.Codis.recarrec_Equivalencia_Reduit, oQuota.tax.fch)
                    DrawRowEnd(EndRows.REQred, oReq.tipus, oQuota.baseImponible.Eur)
                End If
            Case DTOTax.Codis.iva_SuperReduit
                DrawRowEnd(EndRows.IVAsuperRed, oQuota.tax.tipus, oQuota.baseImponible.Eur)
                If mCurrentDoc.recarrecEquivalencia Then
                    Dim oReq As DTOTax = DTOTax.closest(DTOTax.Codis.recarrec_Equivalencia_SuperReduit, oQuota.tax.fch)
                    DrawRowEnd(EndRows.REQsuperRed, oReq.tipus, oQuota.baseImponible.Eur)
                End If
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
                Dim oPuntsBas As DTOAmt = DTOAmt.Factory(mArrastre.Cur, CDec(sStrings(2)))
                Dim IntMes As Integer = sStrings(3)
                sText = mLang.Tradueix("Dto", "Dte", "Dct") & " "
                sText = sText & DcPuntsDto & "% " & mLang.Tradueix("por", "per", "for") & " "
                sText = sText & DcPuntsQty & " " & mLang.Tradueix("puntos s/Eur", "punts sobre Eur", "bonus points over Eur")
                sText = sText & " " & oPuntsBas.Formatted & " "
                sText = sText & mLang.Mes(IntMes)
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
                Dim oBase = DTOAmt.Factory(mArrastre.Cur, CDec(sStrings(1)))
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
                Dim oBase = DTOAmt.Factory(mArrastre.Cur, CDec(sStrings(1)))
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
                Dim oBase = DTOAmt.Factory(mArrastre.Cur, CDec(sStrings(1)))
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
                Dim oBase = DTOAmt.Factory(mArrastre.Cur, CDec(sStrings(1)))
                sText = mLang.Tradueix("Recargo de equivalencia", "Recarrec d'equivalencia", "VAT add-on")
                sText = sText & " " & sTipus & "%"
                If oBase.Val <> mBaseImponible.Val Then
                    sText = sText & " " & mLang.Tradueix("sobre", "sobre", "on") & " "
                    sText = sText & DTOAmt.CurFormatted(oBase)
                End If
                oAmt = oBase.Percent(DcREQ)
                mArrastre.Add(oAmt)
            Case EndRows.Total
                If mCurrentDoc.valorat Then
                    If mCurrentDoc.incoterm Is Nothing Then
                        sText = String.Format("Total {0}", mArrastre.Cur.Tag)
                    Else
                        sText = String.Format("Total {0} {1}", mCurrentDoc.incoterm.Id.ToString, mArrastre.Cur.Tag)
                    End If
                ElseIf mCurrentDoc.displayTotalLogistic Then
                    sText = "Total"
                End If

                If mCurrentDoc.DisplayTotalLogistic Then
                    Dim boxes As Integer = mCurrentDoc.Itms.Sum(Function(x) x.boxes)
                    Dim m3 As Decimal = mCurrentDoc.Itms.Sum(Function(x) x.m3)
                    Dim kg As Integer = mCurrentDoc.Itms.Sum(Function(x) x.kg)
                    sText = sText & String.Format(" {0:N0} {1} {2:N2}m3 {3:N0}Kg", boxes, mLang.Tradueix("cajas", "caixes", "boxes"), m3, kg)
                End If

        End Select

        'Dim sAmt As String
        'If Not oAmt Is Nothing Then sAmt = IIf(oAmt.Val = 0, "", oAmt.Formatted)
        DrawText(sText, FontStyle.Regular, 4)

        If mCurrentDoc.Valorat Then
            DrawImport(oAmt)
        End If
        Y += mLineHeight
        'Dim DcIndent As Decimal = mIndentLevelRowEnd * mDcIndentGap
        'MyBase.Sections(Secs.body).DrawRow(DcIndent, sText, "", "", "", sAmt)
    End Sub



    Private Sub DrawRef(ByVal sText As String)
        Dim oRc As New RectangleF(mTabs(TabCols.Ref0), Y, mTabs(TabCols.RefF) - mTabs(TabCols.Ref0), mFont.Height)
        MyBase.DrawString(sText, mFont, Brushes.Black, oRc)
    End Sub


    Private Function DrawText(ByVal sText As String, ByVal oFontStyle As FontStyle, Optional ByVal Offset As Integer = 0, Optional ByVal CortaTexto As Boolean = False) As Integer
        Dim oFont As New Font(mFont.Name, mFont.Size, oFontStyle)

        Dim iWidth As Integer
        If CortaTexto Then
            iWidth = mTabs(TabCols.DscF) - mTabs(TabCols.Dsc0)
        Else
            iWidth = mTabs(TabCols.AmtF) - mTabs(TabCols.Dsc0)
        End If

        Dim oSize = MeasureString(sText, oFont, iWidth)
        Dim oRc As New RectangleF(mTabs(TabCols.Dsc0), Y, iWidth, oSize.Height) 'mFont.Height)
        If Offset > 0 Then sText = New String(" ", Offset) & sText
        MyBase.DrawString(sText, oFont, Brushes.Black, oRc)
        Return oSize.Height
    End Function

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
        MyBase.DrawString(s, mFont, Brushes.Black, oRc, sF)
    End Sub

    Private Sub DrawPreu(ByVal oPreu As DTOAmt)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far
        Dim s As String = Format(oPreu.Val, "#,##0.00")
        Dim iPvp0 As Integer = mTabs(TabCols.Pvp0)
        Dim iPvpF As Integer = mTabs(TabCols.PvpF)
        If Not mCurrentDoc.DtoColumnDisplay Then
            Dim iDtoWidth As Integer = mTabs(TabCols.DtoF) - mTabs(TabCols.Dto0)
            iPvp0 += iDtoWidth
            iPvpF += iDtoWidth
        End If
        Dim oRc As New RectangleF(iPvp0, Y, iPvpF - iPvp0, mFont.Height)
        Dim oBrush As SolidBrush = IIf(mCurrentDoc.Valorat, Brushes.Black, Brushes.Gray)
        MyBase.DrawString(s, mFont, oBrush, oRc, sF)
    End Sub

    Private Sub DrawItmDto(ByVal SngDto As Decimal)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far
        Dim s As String = SngDto & "%"
        Dim oRc As New RectangleF(mTabs(TabCols.Dto0), Y, mTabs(TabCols.DtoF) - mTabs(TabCols.Dto0), mFont.Height)
        Dim oBrush As SolidBrush = IIf(mCurrentDoc.Valorat, Brushes.Black, Brushes.Gray)
        MyBase.DrawString(s, mFont, oBrush, oRc, sF)
    End Sub

    Private Sub DrawItmPunts(ByVal SngPunts As Decimal)
    End Sub

    Private Sub DrawImport(ByVal oImport As DTOAmt)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Far
        Dim s As String = Format(oImport.Val, "#,##0.00")
        Dim oRc As New RectangleF(mTabs(TabCols.Amt0), Y, mTabs(TabCols.AmtF) - mTabs(TabCols.Amt0), mFont.Height)
        Dim oBrush As SolidBrush = IIf(mCurrentDoc.Valorat, Brushes.Black, Brushes.Gray)
        MyBase.DrawString(s, mFont, oBrush, oRc, sF)
    End Sub


    Private Sub DrawColumnaDetall(ByVal s As String, ByVal iDesde As Integer, ByVal iHasta As Integer, ByVal oAlign As StringAlignment)
        Dim iY As Integer = GetDetallTop()
        If iDesde <> mTabs(0) Then
            Dim iX As Integer = iDesde - 2
            'mybase.DrawLine(mPen, iX, iY, iX, mBottom)
            MyBase.DrawLine(Pens.White, iX, iY, iX, mBottom)
        End If

        Dim oRc As New Rectangle(iDesde, iY, iHasta - iDesde, mFontEpigrafe.Height)

        Dim sF As New StringFormat()
        sF.Alignment = oAlign

        MyBase.DrawString(s, mFontEpigrafe, Brushes.Black, oRc, sF)
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
        MyBase.DrawRectangle(mPen, oRc)

        'linia mitja vertical
        Dim iMidWidth As Integer = CInt(oRc.Width / 2)
        MyBase.DrawLine(mPen, mRight - iMidWidth, oRc.Y, mRight - iMidWidth, oRc.Bottom)

        'linia mitja horitzontal
        If mPageType = PageTypes.Portada Then
            MyBase.DrawLine(mPen, mRight - iMidWidth, oRc.Y + iSegonesPlanesHeight, mRight, oRc.Y + iSegonesPlanesHeight)
        End If

        'epigraf cuadre
        Dim oRectEpigrafe As New RectangleF(mRight - iMidWidth + 1, oRc.Y + 1, iMidWidth - 2, mLineHeightEpigrafe)
        MyBase.FillRectangle(mBackBrush, oRectEpigrafe)

        'separador vertical data/numero de document
        Dim iQuarterWidth As Integer = CInt(iMidWidth / 2)
        MyBase.DrawLine(mPen, mRight - iQuarterWidth, oRc.Y, mRight - iQuarterWidth, oRc.Y + iSegonesPlanesHeight)

        'epigraf titols
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center
        Dim s As String = mCurrentDoc.Lang.Tradueix("fecha", "data", "date")
        Dim oRcFch As New Rectangle(oRectEpigrafe.X, oRectEpigrafe.Y, oRectEpigrafe.Width / 2, oRectEpigrafe.Height)
        MyBase.DrawString(s, mFontEpigrafe, Brushes.Black, oRcFch, sF)

        Select Case mCurrentDoc.Estilo
            Case DTODoc.Estilos.Comanda
                's = mCurrentDoc.Lang.Tradueix("número", "número", "number")
                s = mCurrentDoc.Lang.Tradueix("pedido", "comanda", "order #")
            Case DTODoc.Estilos.Albara
                s = mCurrentDoc.Lang.Tradueix("albarán", "albará", "delivery note")
            Case DTODoc.Estilos.Factura
                s = mCurrentDoc.Lang.Tradueix("factura", "factura", "invoice")
            Case DTODoc.Estilos.Proforma
                s = "proforma"
        End Select
        Dim oRcNum As New Rectangle(oRectEpigrafe.X + oRectEpigrafe.Width / 2, oRectEpigrafe.Y, oRectEpigrafe.Width / 2, oRectEpigrafe.Height)
        MyBase.DrawString(s, mFontEpigrafe, Brushes.Black, oRcNum, sF)

    End Sub

    Private Sub DrawDetailOutline(ByVal iRectangleTop As Integer, ByVal BlDescompte As Boolean)
        If mDrawPijama Then
            DrawPijama()
        End If

        Dim oRc As New RectangleF(mLeft, iRectangleTop, mRight - mLeft, mBottom - iRectangleTop)
        MyBase.DrawRectangle(Pens.Gray, oRc)

        Dim oRectEpigrafe As New RectangleF(mLeft + 1, oRc.Y + 1, oRc.Width - 2, mLineHeightEpigrafe)
        MyBase.FillRectangle(mBackBrush, oRectEpigrafe)

        Dim s As String = ""
        Select Case mCurrentDoc.Estilo
            Case DTODoc.Estilos.Comanda ', DTODoc.Estilos.Proforma
                s = mCurrentDoc.Lang.Tradueix("linea", "linia", "line#")
            Case Else
                s = mCurrentDoc.Lang.Tradueix("n/ref", "n/ref", "o/ref")
        End Select
        DrawColumnaDetall(s, mTabs(TabCols.Ref0), mTabs(TabCols.RefF), StringAlignment.Near)


        Dim iDtoWidthSuplement As Integer = 0
        If Not mCurrentDoc.DtoColumnDisplay Then
            iDtoWidthSuplement = mTabs(TabCols.DtoF) - mTabs(TabCols.Dto0)
        End If

        s = mCurrentDoc.Lang.Tradueix("concepto", "concepte", "concept")
        DrawColumnaDetall(s, mTabs(TabCols.Dsc0), mTabs(TabCols.DscF) + iDtoWidthSuplement, StringAlignment.Near)
        s = mCurrentDoc.Lang.Tradueix("cant.", "quant.", "qty")
        DrawColumnaDetall(s, mTabs(TabCols.Qty0) + iDtoWidthSuplement, mTabs(TabCols.QtyF) + iDtoWidthSuplement, StringAlignment.Far)
        s = mCurrentDoc.Lang.Tradueix("precio", "preu", "price")
        DrawColumnaDetall(s, mTabs(TabCols.Pvp0) + iDtoWidthSuplement, mTabs(TabCols.PvpF) + iDtoWidthSuplement, StringAlignment.Far)

        If mCurrentDoc.DtoColumnDisplay Then
            s = mCurrentDoc.Lang.Tradueix("dto", "dte", "dct")
            DrawColumnaDetall(s, mTabs(TabCols.Dto0), mTabs(TabCols.DtoF), StringAlignment.Far)
        End If
        s = mCurrentDoc.Lang.Tradueix("importe", "import", "amount")
        DrawColumnaDetall(s, mTabs(TabCols.Amt0), mTabs(TabCols.AmtF), StringAlignment.Far)

    End Sub

    Shared Function PlataformaPage(oPlataforma As DTOCustomerPlatform, ByVal oDeliveries As List(Of DTODelivery)) As DTODoc
        Dim oFirstAlb As DTODelivery = oDeliveries.First
        Dim oClient As DTOCustomer = oFirstAlb.customer
        Dim oLang As DTOLang = oClient.lang
        Dim retval As New DTODoc(DTODoc.Estilos.Factura, oLang, DTOCur.Eur, False)
        Dim tmp As String
        With retval
            .dest.Add(oPlataforma.NomComercialOrDefault)
            For Each s As String In oPlataforma.Address.Text.Split(vbCrLf)
                If s > "" Then .dest.Add(s)
            Next
            .dest.Add(oPlataforma.Address.Zip.FullNom(oLang))
            .fch = DTO.GlobalVariables.Today()

            tmp = ""
            .itms.Add(New DTODocItm(tmp, FontStyle.Regular))


            tmp = "ALBARAN DE ENTREGA"
            .itms.Add(New DTODocItm(tmp, FontStyle.Underline))


            tmp = ""
            .itms.Add(New DTODocItm(tmp, FontStyle.Regular))

            tmp = "entregamos la presente mercancía para el siguiente destinatario:"
            .itms.Add(New DTODocItm(tmp, FontStyle.Regular))

            tmp = oClient.Nom
            .itms.Add(New DTODocItm(tmp, FontStyle.Regular))

            'Dim oShippingAddress As DTOAddress = febl.customer.ShippingAddressOrDefault(oClient)
            Dim oShippingAddress As DTOAddress = oFirstAlb.Address

            tmp = oShippingAddress.Text
            For Each s As String In tmp.Split(vbCrLf)
                If s > "" Then .itms.Add(New DTODocItm(s, FontStyle.Regular))
            Next

            tmp = oShippingAddress.Zip.FullNom(oLang)
            .itms.Add(New DTODocItm(tmp, FontStyle.Regular))

            tmp = ""
            .itms.Add(New DTODocItm(tmp, FontStyle.Regular))

            tmp = "contenido segun albaranes:"
            .itms.Add(New DTODocItm(tmp, FontStyle.Regular))

            For Each oDelivery As DTODelivery In oDeliveries
                tmp = "Albarán nº: " & oDelivery.Id
                .itms.Add(New DTODocItm(tmp, FontStyle.Regular))
            Next

        End With
        Return retval
    End Function


    Private Sub DrawPijama()
        Dim Yn As Integer = mDetallTop + 20
        If mPageType = PageTypes.SegonesPlanes Then Yn -= mGapSegonesPlanes
        Dim oBrush As New SolidBrush(Color.FromArgb(230, 230, 230))
        Do While Yn < mBottom - mLineHeight
            MyBase.FillRectangle(oBrush, New RectangleF(mLeft, Yn, mRight - mLeft, mLineHeight))
            Yn += 2 * mLineHeight
        Loop
    End Sub

    Protected Sub SetDetailTabWidths(Optional BlMostrarEAN As Boolean = False)
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

        If BlMostrarEAN Then
            '13 digits en lloc de 5: 51 punts adicionals
            mTabs(TabCols.RefF) = 97 + 42
            mTabs(TabCols.Dsc0) = 103 + 42
        End If
    End Sub


End Class
