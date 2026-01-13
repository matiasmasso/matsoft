Imports System.drawing


Public Class Rpt_DocsOld
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Rpt_DocsOld))
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Location = New System.Drawing.Point(110, 145)
        Me.PrintPreviewDialog1.MinimumSize = New System.Drawing.Size(375, 250)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.TransparencyKey = System.Drawing.Color.Empty
        Me.PrintPreviewDialog1.Visible = False
        '
        'Rpt_Docs
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Name = "Rpt_Docs"
        Me.Text = "Rpt_Docs"

    End Sub

#End Region

    Private mDocRpt As MaxiSrvr.DocRpt

    Private mEv As System.Drawing.Printing.PrintPageEventArgs
    Private mEnsobrado As DTODocRpt.Ensobrados
    'Private mOmrScan As New OMRscan()

    Private DocIndex As Integer
    Private ItmIndex As Integer
    Private CurrentDoc As MaxiSrvr.Doc
    Private CurrentItm As MaxiSrvr.DocItm
    Private CurrentPage As Integer
    Private CurrentLine As Integer
    Private TopLines As Integer
    Private Y As Integer
    Private mTot As Decimal

    Private FontStd As Font = New Font("Arial", 10)
    Private FontNum As Font = New Font("Arial", 10, FontStyle.Bold)
    Private FontTit As Font = New Font("Arial", 8, FontStyle.Bold)

    Private mOMR As OMR
    Private mDataSetEnsobrado As DataSet

    Private StringOffset As Integer = 13 'mEv.Graphics.MeasureString("0", FontStd).Width
    Private Gap As Integer = StringOffset / 2
    Private EndLinePadChars As Integer = 6

    'Dim hTabLeft As Integer = 100 '= mEv.MarginBounds.Left
    Dim hTabLeft As Integer = 60 '= mEv.MarginBounds.Left
    Dim hTabRight As Integer = 727 '719 '= mEv.MarginBounds.Left
    Dim hTabHdrAdr As Integer = 413 ' = hTabLeft + ((hTabRight - hTabLeft) / 2)
    '(finestra esquerra) Dim hTabHdrAlb As Integer = 570 ' = hTabHdrAdr + (hTabRight - hTabHdrAdr) / 2
    'Dim hTabHdrAlb As Integer = hTabLeft + ((hTabHdrAdr - hTabLeft) / 2) ' 256 ' (finestra dreta) = hTabLeft + ((hTabHdrAdr - hTabLeft) / 2)
    Dim hTabHdrNum As Integer = hTabLeft + 80 ' = hTabLeft + ((hTabRight - hTabLeft) / 2)
    Dim hTabHdrFch As Integer = hTabHdrNum + 80 ' = hTabLeft + ((hTabRight - hTabLeft) / 2)


    Dim vTabLogo As Integer = 24
    Dim vTabHdrTop As Integer = 146 '= mEv.MarginBounds.Top + 46
    Dim vTabHdrTit As Integer = 160 '= vTabHdrTop + FontColHdr.GetHeight(mEv.Graphics)
    Dim vTabHdrAlb As Integer = 208 '= vTabHdrTit + 48
    Dim vTabHdrBtm As Integer = 302 '= vTabHdrTop + 156

    Dim vTabDtlTop As Integer = 334 '= vTabHdrBtm + 32
    Dim vTabDtlTit As Integer = 342 '=vTabDtlTop + FontColHdr.GetHeight(mEv.Graphics)
    Dim vTabDtlBtm As Integer = 1069 '= mEv.MarginBounds.Bottom

    Dim ColPuntsWidth As Integer = 3 * StringOffset
    Dim ColAmtWidth As Integer = 7 * StringOffset
    Dim ColDtoWidth As Integer = 3 * StringOffset
    Dim ColPvpWidth As Integer = 5 * StringOffset
    Dim ColQtyWidth As Integer = 3 * StringOffset

    Dim hTabDtlPunts As Integer = hTabRight
    Dim hTabDtlAmt As Integer = hTabRight
    Dim hTabDtlDto As Integer = hTabDtlAmt - (ColAmtWidth * StringOffset)
    Dim hTabDtlPvp As Integer = hTabDtlDto - (ColDtoWidth * StringOffset)
    Dim hTabDtlQty As Integer = hTabDtlPvp - (ColPvpWidth * StringOffset)
    Dim hTabDtlTxt As Integer = hTabDtlQty - (ColQtyWidth * StringOffset)

    Dim oPenGray As New Pen(Color.Gray, 2)
    Dim oBrushTit As Brush = Brushes.Gray

    Public Event AfterPrintDoc(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum SumConcepts
        SumaAnterior
        SumaySigue
        Total
        SumaDeImportes
        SumaParcial
        BaseImponible
    End Enum

    Public WriteOnly Property DocRpt() As MaxiSrvr.DocRpt
        Set(ByVal Value As MaxiSrvr.DocRpt)
            mDocRpt = Value
        End Set
    End Property

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim oItm As DocItm

        If mOMR Is Nothing Then
            mOMR = New OMR(OMR.FeedDirections.BottomScanning)
        End If
        mEv = e

        If ItmIndex = 0 Then
            CurrentDoc = mDocRpt.Docs(DocIndex)
            CurrentPage = 1
            CurrentLine = 0
            mTot = 0
            DocStart()
            Y = vTabDtlTit + (FontStd.Height / 2)
        Else
            CurrentPage = CurrentPage + 1
            CurrentLine = 0
            DocContinua()
            Y = vTabDtlTit + (FontStd.Height / 2)
            DrawSum(SumConcepts.SumaAnterior)
        End If

        Do
            If ItmIndex >= CurrentDoc.Itms.Count Then Exit Do
            oItm = CurrentDoc.Itms(ItmIndex)
            CurrentLine = CurrentLine + 1
            If CurrentLine > TopLines - oItm.MinLinesBeforeEndPage Then
                DrawSum(SumConcepts.SumaySigue)
                If mEnsobrado Then Ensobra(CurrentPage = 1 And CurrentDoc.BOC = True, False)
                mEv.HasMorePages = True
                Exit Sub
            End If
            DoClientne(oItm)
            ItmIndex = ItmIndex + 1
        Loop

        If DocEnd() = False Then
            DrawSum(SumConcepts.SumaySigue)
            If mEnsobrado Then Ensobra(CurrentPage = 1 And CurrentDoc.BOC = True, False)
            mEv.HasMorePages = True
            Exit Sub
        End If

        If mEnsobrado Then Ensobra(CurrentPage = 1 And CurrentDoc.BOC = True, CurrentDoc.EOC)

        DocIndex = DocIndex + 1
        If DocIndex >= mDocRpt.Docs.Count Then Exit Sub

        RaiseEvent AfterPrintDoc(DocIndex, New System.EventArgs)
        ItmIndex = 0
        mEv.HasMorePages = True
    End Sub


    Private Sub Ensobra(ByVal BOC As Boolean, ByVal EOC As Boolean)
        Static PagesPerDoc As Integer
        Dim SelFeed1 As Boolean = False

        If BOC Then PagesPerDoc = 0
        PagesPerDoc = PagesPerDoc + 1
        If EOC Then
            SelFeed1 = CurrentDoc.selfeed1
            EnsobraLog(CurrentDoc.Dest(0), CurrentDoc.Num, CurrentDoc.Fch, PagesPerDoc)
        End If

        With mOMR
            .BOC = BOC
            .EOC = EOC
            .SelFeed1 = SelFeed1
            .NextPage(mEv)
        End With
    End Sub

    Private Sub EnsobraLog(ByVal pNom As String, ByVal LngNum As Long, ByVal pFch As Date, ByVal pPags As Integer)
        Dim oTb As DataTable
        Dim oRow As DataRow
        If mDataSetEnsobrado Is Nothing Then
            mDataSetEnsobrado = New DataSet
            oTb = New DataTable()
            oTb.Columns.Add("Num", System.Type.GetType("System.Int32"))
            oTb.Columns.Add("Fch", System.Type.GetType("System.String"))
            oTb.Columns.Add("Nom", System.Type.GetType("System.String"))
            oTb.Columns.Add("Pags", System.Type.GetType("System.Int32"))
            mDataSetEnsobrado.Tables.Add(oTb)
        Else
            oTb = mDataSetEnsobrado.Tables(0)
        End If
        oRow = oTb.NewRow()
        oRow("Num") = LngNum
        oRow("Nom") = pNom
        oRow("Fch") = pFch.ToShortDateString
        oRow("Pags") = pPags
        oTb.Rows.Add(oRow)
    End Sub

    Private Sub DocStart()
        DrawTemplate()
        DrawDestinatari(CurrentDoc.Dest)
        DrawFch(CurrentDoc.Fch)
        DrawNum(CurrentDoc.Num)
        DrawObs(CurrentDoc.Obs)
    End Sub

    Private Sub DocContinua()
        DrawTemplate()
        DrawDestinatari(CurrentDoc.Dest)
        DrawPage(CurrentPage)
        DrawNum(CurrentDoc.Num)
    End Sub

    Private Sub DoClientne(ByVal oItm As DocItm)
        Dim DblImport As Decimal = oItm.Import(CurrentDoc.Cur.Decimals)
        If oItm.Text > "" Then DrawText(oItm.Text, oItm.FontStyle, oItm.LeftPadChars, IIf(oItm.Qty = 0, False, True))
        If oItm.Qty <> 0 Then DrawQty(oItm.Qty)
        If oItm.Preu <> 0 Then DrawPreu(oItm.Preu)
        If oItm.Dto <> 0 Then DrawItmDto(oItm.Dto)
        If DblImport <> 0 Then
            DrawImport(oItm.Import)
            mTot = mTot + DblImport
        End If
        If CurrentDoc.DisplayPunts And oItm.Punts > 0 Then
            DrawItmPunts(oItm.Punts)
        End If
        Y = Y + FontStd.Height
    End Sub

    Private Function DocEnd() As Boolean
        Dim DblBase As Decimal
        Dim DblTmp As Decimal
        Dim Decimals As Integer = CurrentDoc.Cur.Decimals


        If mTot <> 0 Then
            If CurrentLine > TopLines - EndLinesCount() Then
                Return False
                Exit Function
            End If

            If CurrentDoc.Descomptes Then
                DrawSum(SumConcepts.SumaDeImportes)
                If CurrentDoc.Dto <> 0 Then
                    DblTmp = Math.Round(mTot * CurrentDoc.Dto / 100, Decimals, MidpointRounding.AwayFromZero)
                    DrawGralDto(CurrentDoc.Dto, DblTmp)
                    mTot = mTot - DblTmp
                End If
                If CurrentDoc.PuntsTipus <> 0 Then
                    DblTmp = CurrentDoc.PuntsBase.Percent(CurrentDoc.PuntsTipus).Eur
                    DrawPunts(CurrentDoc.PuntsQty, CurrentDoc.PuntsBase.Eur, CurrentDoc.PuntsTipus, DblTmp)
                    mTot = mTot - DblTmp
                End If
                If CurrentDoc.Dpp <> 0 Then
                    If CurrentDoc.Dto <> 0 Then DrawSum(SumConcepts.SumaParcial)
                    DblTmp = Math.Round(mTot * CurrentDoc.Dpp / 100, Decimals, MidpointRounding.AwayFromZero)
                    DrawDpp(CurrentDoc.Dpp, DblTmp)
                    mTot = mTot - DblTmp
                End If
            End If

            If CurrentDoc.Impostos Then
                DblBase = mTot
                DrawSum(SumConcepts.BaseImponible)

                Dim DcBaseImponible As Decimal = mTot
                For Each oQuota As IvaBaseQuota In CurrentDoc.IvaBaseQuotas
                    DblTmp = DrawIVA(oQuota.Iva.Tipus, oQuota.Base, oQuota.Base.Eur <> DcBaseImponible)
                    mTot = mTot + DblTmp
                    If CurrentDoc.RecarrecEquivalencia Then
                        DblTmp = DrawREQ(oQuota.Iva.RecarrecEquivalencia.Tipus, oQuota.Base, oQuota.Base.Eur <> DcBaseImponible)
                        mTot = mTot + DblTmp
                    End If
                Next
            End If
            DrawSum(SumConcepts.Total)
        End If

        For Each s As String In CurrentDoc.Coletillas
            Y = Y + FontStd.Height
            DrawBigText(s, FontStyle.Italic, 0, False)
        Next
        Return True
    End Function

    Private Sub DrawDestinatari(ByVal oArrayList As ArrayList)
        '(finestra esquerra) Dim X As Integer = hTabLeft + StringOffset
        Dim X As Integer = hTabHdrAdr + StringOffset
        Dim MitadEspacioLibre As Integer = (vTabHdrBtm - vTabHdrTop) / 2
        Dim MitadEspacioOcupado As Integer = oArrayList.Count * FontStd.Height / 2
        Dim Y As Integer = vTabHdrTop + MitadEspacioLibre - MitadEspacioOcupado
        Dim tmpStr As String
        If CurrentPage > 1 Then Y = vTabHdrTit + (vTabHdrAlb - vTabHdrTit - mEv.Graphics.MeasureString(oArrayList(0), FontStd).Height) / 2
        Dim itm As String
        For Each itm In oArrayList
            '(finestra esquerra) tmpStr = CutString(itm, hTabHdrAdr - X, FontStd, mEv)
            tmpStr = itm '(no tallis la adreça) CutString(itm, hTabRight - X, FontStd, mEv)
            mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
            If CurrentPage > 1 Then Exit For
            Y = Y + mEv.Graphics.MeasureString(oArrayList(0), FontStd).Height ' FontStd.Height
        Next
    End Sub

    Private Sub DrawPage(ByVal IntPage As Integer)
        Dim tmpStr As String = IntPage
        '(finestra esquerra) Dim X As Integer = hTabHdrAdr + (hTabHdrAlb - hTabHdrAdr - mEv.Graphics.MeasureString(tmpStr, FontStd).Width()) / 2
        Dim X As Integer = hTabHdrNum + (hTabHdrFch - hTabHdrNum - mEv.Graphics.MeasureString(tmpStr, FontStd).Width()) / 2
        Dim Y As Integer = vTabHdrTit + (vTabHdrAlb - vTabHdrTit - mEv.Graphics.MeasureString(tmpStr, FontStd).Height) / 2
        mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
    End Sub

    Private Sub DrawFch(ByVal DtFch As Date)
        If DtFch = Nothing Then Exit Sub
        Dim tmpStr As String = Format(DtFch, "dd/MM/yy")
        '(finestra esquerra) Dim X As Integer = hTabHdrAdr + (hTabHdrAlb - hTabHdrAdr - mEv.Graphics.MeasureString(tmpStr, FontStd).Width()) / 2
        Dim X As Integer = hTabHdrNum + (hTabHdrFch - hTabHdrNum - mEv.Graphics.MeasureString(tmpStr, FontStd).Width()) / 2
        Dim Y As Integer = vTabHdrTit + (vTabHdrAlb - vTabHdrTit - mEv.Graphics.MeasureString(tmpStr, FontStd).Height) / 2
        mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
    End Sub

    Private Sub DrawNum(ByVal sNum As String)
        Dim tmpStr As String = sNum
        '(finestra esquerra) Dim X As Integer = hTabHdrAlb + (hTabRight - hTabHdrAlb - mEv.Graphics.MeasureString(tmpStr, FontNum).Width()) / 2
        Dim X As Integer = hTabLeft + (hTabHdrNum - hTabLeft - mEv.Graphics.MeasureString(tmpStr, FontNum).Width()) / 2
        Dim Y As Integer = vTabHdrTit + (vTabHdrAlb - vTabHdrTit - mEv.Graphics.MeasureString(tmpStr, FontNum).Height) / 2
        mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
    End Sub

    Private Sub DrawObs(ByVal oArrayList As ArrayList)
        '(finestra esquerra) Dim X As Integer = hTabHdrAdr + StringOffset
        Dim X As Integer = hTabLeft + StringOffset
        Dim MitadEspacioLibre As Integer = (vTabHdrBtm - vTabHdrAlb) / 2
        Dim MitadEspacioOcupado As Integer = oArrayList.Count * FontStd.Height / 2
        Dim Y As Integer = vTabHdrAlb + MitadEspacioLibre - MitadEspacioOcupado
        Dim tmpStr As String
        Dim itm As String
        For Each itm In oArrayList
            '(finestra esquerra) tmpStr = CutString(itm, hTabRight - X, FontStd, mEv)
            tmpStr = CutString(itm, hTabHdrAdr - X, FontStd, mEv).Replace(vbLf, "")
            mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
            Y = Y + FontStd.Height
        Next
    End Sub

    Private Sub DrawText(ByVal txt As String, ByVal oFontStyle As FontStyle, Optional ByVal Offset As Integer = 0, Optional ByVal CortaTexto As Boolean = False)
        Dim X As Integer
        Dim tmpStr As String
        If oFontStyle = Nothing Then oFontStyle = FontStyle.Regular
        Dim oFont As New Font(FontStd.FontFamily, FontStd.Size, oFontStyle)
        Do While txt > ""
            Dim CrLfPos As Integer = txt.IndexOf(Chr(13))
            If CrLfPos >= 0 Then
                tmpStr = txt.Substring(0, CrLfPos)
                txt = txt.Substring(CrLfPos + 2)
            Else
                tmpStr = txt
                txt = ""
            End If

            X = hTabLeft + Offset * StringOffset + Gap
            If CortaTexto Then
                tmpStr = CutString(tmpStr, hTabDtlTxt - X, oFont, mEv)
            End If
            mEv.Graphics.DrawString(tmpStr, oFont, Brushes.Black, X, Y)
            If CrLfPos >= 0 Then Y = Y + FontStd.Height
        Loop
    End Sub

    Private Sub DrawBigText(ByVal txt As String, ByVal oFontStyle As FontStyle, Optional ByVal Offset As Integer = 0, Optional ByVal CortaTexto As Boolean = False)
        Dim X As Integer
        Dim tmpStr As String
        If oFontStyle = Nothing Then oFontStyle = FontStyle.Regular
        Dim oFont As New Font(FontStd.FontFamily, FontStd.Size, oFontStyle)
        Do While txt > ""
            Dim CrLfPos As Integer = txt.IndexOf(Chr(13))
            If CrLfPos >= 0 Then
                tmpStr = txt.Substring(0, CrLfPos)
                txt = txt.Substring(CrLfPos + 2)
            Else
                tmpStr = txt
                txt = ""
            End If

            X = hTabLeft + Offset * StringOffset + Gap
            If CortaTexto Then
                tmpStr = CutString(tmpStr, hTabDtlTxt - X, oFont, mEv)
            End If
            mEv.Graphics.DrawString(tmpStr, oFont, Brushes.Black, X, Y)
            If CrLfPos >= 0 Then Y = Y + FontStd.Height
        Loop
    End Sub

    Private Sub DrawQty(ByVal IntQty As Integer)
        Dim tmpStr As String = Format(IntQty, "#,###")
        Dim X As Integer = hTabDtlQty - mEv.Graphics.MeasureString(tmpStr, FontStd).Width
        mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
    End Sub

    Private Sub DrawPreu(ByVal DblPreu As Decimal)
        Dim tmpStr As String = Format(DblPreu, CurrentDoc.Cur.FormatString)
        Dim X As Integer = hTabDtlPvp - mEv.Graphics.MeasureString(tmpStr, FontStd).Width
        mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
    End Sub

    Private Sub DrawItmDto(ByVal SngDto As Decimal)
        Dim tmpStr As String = CStr(SngDto).Trim & "%"
        Dim X As Integer = hTabDtlDto - mEv.Graphics.MeasureString(tmpStr, FontStd).Width
        mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
    End Sub

    Private Sub DrawItmPunts(ByVal SngPunts As Decimal)
        Dim tmpStr As String = Format(SngPunts, "0.00")
        Dim X As Integer = hTabDtlPunts - mEv.Graphics.MeasureString(tmpStr, FontStd).Width
        mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
    End Sub

    Private Sub DrawImport(ByVal DblImport As Decimal)
        Dim tmpStr As String = Format(DblImport, CurrentDoc.Cur.FormatString)
        Dim X As Integer = hTabDtlAmt - mEv.Graphics.MeasureString(tmpStr, FontStd).Width
        mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)
    End Sub

    Private Sub DrawSum(ByVal Concept As SumConcepts)
        Dim tmpStr As String
        Select Case Concept
            Case SumConcepts.SumaAnterior
                tmpStr = CurrentDoc.Lang.Tradueix("Suma anterior", "Suma anterior", "Last Amt")
                DrawEndLine(tmpStr, mTot, False)
            Case SumConcepts.SumaySigue
                tmpStr = CurrentDoc.Lang.Tradueix("Suma y sigue", "Suma y segueix", "To follow")
                DrawEndLine(tmpStr, mTot, False)
            Case SumConcepts.Total
                Select Case CurrentDoc.Incoterm
                    Case Doc.incoterms.CIF
                        tmpStr = "CIF " & CurrentDoc.Cur.Tag
                    Case Else
                        tmpStr = CurrentDoc.Lang.Tradueix("Total", "Total", "Total") & " " & BLLCur.NomOrTag(CurrentDoc.Cur, CurrentDoc.Lang)
                End Select
                DrawEndLine(tmpStr, mTot, True)
            Case SumConcepts.SumaParcial
                tmpStr = CurrentDoc.Lang.Tradueix("Suma parcial", "Suma parcial", "parcial sum")
                DrawEndLine(tmpStr, mTot, True)
            Case SumConcepts.SumaDeImportes
                tmpStr = CurrentDoc.Lang.Tradueix("Suma de importes", "Suma d'imports", "sum")
                DrawEndLine(tmpStr, mTot, True)
            Case SumConcepts.BaseImponible
                tmpStr = CurrentDoc.Lang.Tradueix("Base imponible", "Base imponible", "Tax base")
                DrawEndLine(tmpStr, mTot, True)
        End Select
    End Sub

    Private Sub DrawGralDto(ByVal SngDto As Decimal, ByVal DblImporte As Decimal)
        Dim tmpStr As String
        tmpStr = CurrentDoc.Lang.Tradueix("Descuento", "Descompte", "Discount")
        tmpStr = tmpStr & " " & SngDto & " %"
        DrawEndLine(tmpStr, DblImporte, False)
    End Sub

    Private Sub DrawPunts(ByVal IntPunts As Decimal, ByVal DblBase As Decimal, ByVal SngDto As Decimal, ByVal DblImporte As Decimal)
        Dim tmpStr As String
        Dim IntMes As Integer = CurrentDoc.Fch.Month
        tmpStr = CurrentDoc.Lang.Tradueix("Descuento", "Descompte", "Discount") & " "
        tmpStr = tmpStr & SngDto & "% " & CurrentDoc.Lang.Tradueix("por", "per", "for") & " "
        tmpStr = tmpStr & IntPunts & " " & CurrentDoc.Lang.Tradueix("puntos sobre Eur ", "punts sobre Eur ", "bonus points over Eur ")
        tmpStr = tmpStr & " " & Format(DblBase, "#,###.00")
        tmpStr = tmpStr & " " & CurrentDoc.Lang.Tradueix("mes de", "mes de", "month") & " "
        tmpStr = tmpStr & maxisrvr.mesFullName(IntMes, CurrentDoc.Lang)

        DrawEndLine(tmpStr, DblImporte, False)
    End Sub

    Private Sub DrawDpp(ByVal SngDto As Decimal, ByVal DblImporte As Decimal)
        Dim tmpStr As String
        tmpStr = CurrentDoc.Lang.Tradueix("Descuento prontopago", "Descompte prontopago", "Discount quick payment")
        tmpStr = tmpStr & " " & SngDto & " %"
        DrawEndLine(tmpStr, DblImporte, False)
    End Sub

    Private Function DrawIVA(ByVal DcIva As Decimal, ByVal oBase As DTOAmt, ByVal BlPrintBase As Boolean) As Decimal
        Dim tmpStr As String
        Dim sTipus As String = Format(DcIVA, IIf(Decimal.Truncate(DcIVA) = DcIVA, "0", "0.00"))
        tmpStr = "IVA " & sTipus & "% "
        If BlPrintBase Then
            tmpStr = tmpStr & CurrentDoc.Lang.Tradueix("sobre", "sobre", "over") & " "
            tmpStr = tmpStr & oBase.CurFormatted
        End If
        Dim oAmt As DTOAmt = oBase.Percent(DcIva)
        Dim DblAmt As Decimal = oAmt.Val
        DrawEndLine(tmpStr, DblAmt, False)
        Return DblAmt
    End Function

    Private Function DrawREQ(ByVal DcREQ As Decimal, ByVal oBase As DTOAmt, ByVal BlPrintBase As Boolean) As Decimal
        Dim sTipus As String = Format(DcREQ, IIf(Decimal.Truncate(DcREQ) = DcREQ, "0", "0.00"))
        Dim tmpStr As String
        tmpStr = CurrentDoc.Lang.Tradueix("Recargo de equivalencia", "Recarrec d'equivalència", "Tax complement") & " "
        tmpStr = tmpStr & sTipus & "% "
        If BlPrintBase Then
            tmpStr = tmpStr & CurrentDoc.Lang.Tradueix("sobre", "sobre", "over") & " "
            tmpStr = tmpStr & oBase.CurFormatted
        End If
        Dim oAmt As DTOAmt = oBase.Percent(DcREQ)
        Dim DblAmt As Decimal = oAmt.Val
        DrawEndLine(tmpStr, DblAmt, False)
        Return DblAmt
    End Function

    Private Sub DrawEndLine(ByVal tmpStr As String, ByVal DblImporte As Decimal, ByVal Raya As Boolean)
        Dim X As Integer
        If Raya Then
            X = hTabDtlAmt - (6 * StringOffset)
            Y = Y + FontStd.Height / 2
            mEv.Graphics.DrawLine(Pens.Black, X, Y, hTabDtlAmt - Gap, Y)
            Y = Y + FontStd.Height / 2
        End If

        X = hTabLeft + EndLinePadChars * StringOffset
        mEv.Graphics.DrawString(tmpStr, FontStd, Brushes.Black, X, Y)

        DrawImport(DblImporte)
        Y = Y + FontStd.Height
    End Sub

    Private Sub DrawTemplate()
        Dim tmpStr As String
        Dim X As Integer
        Dim oLang as DTOLang = CurrentDoc.Lang
        Dim sConcept As String = ""

        'DrawMembrete(hTabLeft, vTabLogo)

        Select Case mDocRpt.Estilo
            Case DTODocRpt.Estilos.Comanda
                sConcept = oLang.Tradueix("pedido", "comanda", "order")
            Case DTODocRpt.Estilos.Albara
                sConcept = oLang.Tradueix("albarán", "albará", "packing list")
            Case DTODocRpt.Estilos.Factura
                sConcept = oLang.Tradueix("factura", "factura", "invoice")
            Case DTODocRpt.Estilos.Proforma
                sConcept = oLang.Tradueix("proforma", "proforma", "proforma")
        End Select

        If CurrentPage = 1 Then
            vTabHdrBtm = vTabHdrTop + 156
            vTabDtlTop = vTabHdrBtm + 32
            'vTabDtlTit = vTabDtlTop + FontTit.GetHeight(mEv.Graphics)
            vTabDtlTit = vTabDtlTop + FontTit.Height
            If CurrentDoc.WriteTemplate Then
                With mEv.Graphics
                    '.DrawRectangle(Pens.Black, New Rectangle(hTabLeft, vTabHdrTop, hTabRight - hTabLeft, vTabHdrBtm - vTabHdrTop))
                    '.DrawLine(Pens.Black, hTabHdrAdr, vTabHdrTop, hTabHdrAdr, vTabHdrBtm)
                    '.DrawLine(Pens.Black, hTabLeft, vTabHdrTop, hTabLeft, vTabHdrTit)
                    .DrawLine(Pens.Black, hTabHdrNum, vTabHdrTop, hTabHdrNum, vTabHdrTit)
                    .DrawLine(Pens.Black, hTabHdrFch, vTabHdrTop, hTabHdrFch, vTabHdrTit)
                    '(finestra esquerra) .DrawLine(Pens.Black, hTabHdrAdr, vTabHdrTit, hTabRight, vTabHdrTit)
                    '(finestra esquerra) .DrawLine(Pens.Black, hTabHdrAdr, vTabHdrAlb, hTabRight, vTabHdrAlb)
                    '.DrawLine(Pens.Black, hTabLeft, vTabHdrTop, hTabHdrAdr, vTabHdrTop)
                    .DrawLine(Pens.Black, hTabLeft, vTabHdrTit, hTabHdrFch, vTabHdrTit)
                    '.DrawLine(Pens.Black, hTabLeft, vTabHdrAlb, hTabHdrAdr, vTabHdrAlb)

                    tmpStr = oLang.Tradueix("fecha", "data", "date")
                    '(finestra esquerra) .DrawString(tmpStr, FontTit, Brushes.Black, hTabHdrAdr + (hTabHdrAlb - hTabHdrAdr - .MeasureString(tmpStr, FontTit).Width()) / 2, vTabHdrTop)
                    .DrawString(tmpStr, FontTit, oBrushTit, hTabHdrNum + (hTabHdrFch - hTabHdrNum - .MeasureString(tmpStr, FontTit).Width()) / 2, vTabHdrTop)

                    tmpStr = sConcept
                    '(finestra esquerra) .DrawString(tmpStr, FontTit, Brushes.Black, hTabHdrAlb + (hTabRight - hTabHdrAlb - .MeasureString(tmpStr, FontTit).Width()) / 2, vTabHdrTop)
                    .DrawString(tmpStr, FontTit, oBrushTit, hTabLeft + (hTabHdrNum - hTabLeft - .MeasureString(tmpStr, FontTit).Width()) / 2, vTabHdrTop)
                End With
            End If
        Else
            vTabHdrBtm = vTabHdrAlb
            vTabDtlTop = vTabHdrBtm + 32
            vTabDtlTit = vTabDtlTop + FontTit.Height
            If CurrentDoc.WriteTemplate Then
                With mEv.Graphics
                    '.DrawRectangle(Pens.Black, New Rectangle(hTabLeft, vTabHdrTop, hTabRight - hTabLeft, vTabHdrBtm - vTabHdrTop))
                    '.DrawLine(Pens.Black, hTabHdrAdr, vTabHdrTop, hTabHdrAdr, vTabHdrBtm)
                    .DrawLine(Pens.Black, hTabHdrNum, vTabHdrTop, hTabHdrNum, vTabHdrTit)
                    .DrawLine(Pens.Black, hTabHdrFch, vTabHdrTop, hTabHdrFch, vTabHdrTit)
                    '.DrawLine(Pens.Black, hTabHdrAlb, vTabHdrTop, hTabHdrAlb, vTabHdrAlb)
                    '(finestra esquerra) .DrawLine(Pens.Black, hTabHdrAdr, vTabHdrTit, hTabRight, vTabHdrTit)
                    .DrawLine(Pens.Black, hTabLeft, vTabHdrTit, hTabHdrFch, vTabHdrTit)
                    '(finestra esquerra) .DrawLine(Pens.Black, hTabHdrAdr, vTabHdrAlb, hTabRight, vTabHdrAlb)
                    '.DrawLine(Pens.Black, hTabLeft, vTabHdrAlb, hTabHdrAdr, vTabHdrAlb)

                    tmpStr = oLang.Tradueix("página", "pàgina", "page")
                    '(finestra esquerra) .DrawString(tmpStr, FontTit, Brushes.Black, hTabHdrAdr + (hTabHdrAlb - hTabHdrAdr - .MeasureString(tmpStr, FontTit).Width()) / 2, vTabHdrTop)
                    .DrawString(tmpStr, FontTit, oBrushTit, hTabHdrNum + (hTabHdrFch - hTabHdrNum - .MeasureString(tmpStr, FontTit).Width()) / 2, vTabHdrTop)

                    tmpStr = sConcept
                    '(finestra esquerra) .DrawString(tmpStr, FontTit, Brushes.Black, hTabHdrAlb + (hTabRight - hTabHdrAlb - .MeasureString(tmpStr, FontTit).Width()) / 2, vTabHdrTop)
                    .DrawString(tmpStr, FontTit, oBrushTit, hTabLeft + (hTabHdrNum - hTabLeft - .MeasureString(tmpStr, FontTit).Width()) / 2, vTabHdrTop)
                End With
            End If
        End If

        TopLines = (vTabDtlBtm - vTabDtlTit) / FontStd.Height - 2

        If CurrentDoc.DisplayPunts Then
            hTabDtlAmt = hTabDtlAmt - ColPuntsWidth
        End If

        If CurrentDoc.DtoColumnDisplay Then
            hTabDtlDto = hTabDtlAmt - (ColAmtWidth)
            hTabDtlPvp = hTabDtlDto - (ColDtoWidth)
        Else
            hTabDtlPvp = hTabDtlAmt - (ColAmtWidth)
        End If
        hTabDtlQty = hTabDtlPvp - (ColPvpWidth)
        hTabDtlTxt = hTabDtlQty - (ColQtyWidth)

        If CurrentDoc.WriteTemplate Then
            With mEv.Graphics
                'detail
                '.DrawRectangle(Pens.Black, New Rectangle(hTabLeft, vTabDtlTop, hTabRight - hTabLeft, vTabDtlBtm - vTabDtlTop)) 'SPV nº
                '.DrawLine(Pens.Black, hTabLeft, vTabDtlTop, hTabRight, vTabDtlTop)
                .DrawLine(Pens.Black, hTabLeft, vTabDtlTit, hTabRight, vTabDtlTit)
                '.DrawLine(Pens.Black, hTabLeft, vTabDtlTop, hTabLeft, vTabDtlTit)
                .DrawLine(Pens.Black, hTabDtlTxt, vTabDtlTop, hTabDtlTxt, vTabDtlTit)
                .DrawLine(Pens.Black, hTabDtlQty, vTabDtlTop, hTabDtlQty, vTabDtlTit)
                .DrawLine(Pens.Black, hTabDtlPvp, vTabDtlTop, hTabDtlPvp, vTabDtlTit)
                .DrawLine(Pens.Black, hTabDtlDto, vTabDtlTop, hTabDtlDto, vTabDtlTit)
                .DrawLine(Pens.Black, hTabRight, vTabDtlTop, hTabRight, vTabDtlTit)

                '.DrawLine(Pens.Black, hTabDtlTxt, vTabDtlTop, hTabDtlTxt, vTabDtlBtm)
                '.DrawLine(Pens.Black, hTabDtlQty, vTabDtlTop, hTabDtlQty, vTabDtlBtm)
                '.DrawLine(Pens.Black, hTabDtlPvp, vTabDtlTop, hTabDtlPvp, vTabDtlBtm)
                '.DrawLine(Pens.Black, hTabDtlDto, vTabDtlTop, hTabDtlDto, vTabDtlBtm)


                tmpStr = BLL.BLLLang.GetResourceByKey(oLang, "CONCEPTO")
                X = hTabLeft + Gap
                .DrawString(tmpStr, FontTit, oBrushTit, X, vTabDtlTop)

                tmpStr = oLang.Tradueix("cant", "quant", "qty")
                X = hTabDtlQty - Gap - mEv.Graphics.MeasureString(tmpStr, FontTit).Width
                .DrawString(tmpStr, FontTit, oBrushTit, X, vTabDtlTop)

                tmpStr = oLang.Tradueix("precio", "preu", "price")
                X = hTabDtlPvp - Gap - mEv.Graphics.MeasureString(tmpStr, FontTit).Width
                .DrawString(tmpStr, FontTit, oBrushTit, X, vTabDtlTop)

                If CurrentDoc.DtoColumnDisplay Then
                    tmpStr = oLang.Tradueix("dto", "dte", "dct")
                    X = hTabDtlDto - Gap - mEv.Graphics.MeasureString(tmpStr, FontTit).Width
                    .DrawString(tmpStr, FontTit, oBrushTit, X, vTabDtlTop)
                End If

                tmpStr = oLang.Tradueix("importe", "import", "Amt")
                X = hTabDtlAmt - Gap - mEv.Graphics.MeasureString(tmpStr, FontTit).Width
                .DrawString(tmpStr, FontTit, oBrushTit, X, vTabDtlTop)

                If CurrentDoc.DisplayPunts Then
                    tmpStr = oLang.Tradueix("puntos", "punts", "points")
                    X = hTabDtlPunts - ColPuntsWidth / 2 - mEv.Graphics.MeasureString(tmpStr, FontTit).Width / 2
                    .DrawString(tmpStr, FontTit, oBrushTit, X, vTabDtlTop)
                    '.DrawLine(Pens.Black, hTabDtlPunts - ColPuntsWidth, vTabDtlTop, hTabDtlPunts - ColPuntsWidth, vTabDtlBtm)
                    .DrawLine(oPenGray, hTabDtlPunts - ColPuntsWidth, vTabDtlTop, hTabDtlPunts - ColPuntsWidth, vTabDtlTit)
                End If

                DrawDadesRegistrals()
            End With
        End If
    End Sub

    Private Sub DrawDadesRegistrals()
        Dim s As String = "Reg.Mercantil de Barcelona, Tomo 6403, Libro 5689, Sección 2ª, Folio 167, Hoja 76326, Inscripción 1ª, NIF ES-A58007857"
        Dim iStringWidth As Integer = mEv.Graphics.MeasureString(s, FontTit).Width
        Dim oRc As New RectangleF(-1169, 0, 1169, 20)
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Center
        sF.LineAlignment = StringAlignment.Center
        'sF.FormatFlags = StringFormatFlags.DirectionRightToLeft
        'sF.FormatFlags = StringFormatFlags.DirectionVertical
        'mEv.Graphics.TranslateTransform(10, 10)
        mEv.Graphics.RotateTransform(-90)
        mEv.Graphics.DrawString(s, FontTit, oBrushTit, oRc, sF)
        mEv.Graphics.RotateTransform(90)
    End Sub

    Private Function EndLinesCount() As Integer
        Dim IntCount As Integer = 2 'total i ratlla
        With CurrentDoc
            If .Descomptes Then IntCount = IntCount + 3 'ratlla, suma  i dto
            If .Dto <> 0 And .Dpp <> 0 Then IntCount = IntCount + 3 'suma parcial, ratlla i dto
            If .Impostos Then
                IntCount = IntCount + .IvaBaseQuotas.Count 'ratlla, base imponible i IVAs
                For Each oQuota As MaxiSrvr.IvaBaseQuota In .IvaBaseQuotas
                    If .RecarrecEquivalencia Then IntCount += 1
                Next
            End If
        End With
        Return IntCount
    End Function

    Public Sub Printpreview()
        ItmIndex = 0
        mEnsobrado = 1
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Public Sub print(ByVal oKind As DTODocRpt.FuentePapel, ByVal eEnsobrado As DTODocRpt.Ensobrados, Optional ByVal CopiasExtra As Integer = 0)
        'Public Function print(ByVal oKind As DocRpt.FuentePapel, ByVal eEnsobrado As DocRpt.Ensobrados, Optional ByVal CopiasExtra As Integer = 0) As DataSet
        ItmIndex = 0
        mEnsobrado = eEnsobrado
        Dim oSrc As Drawing.Printing.PaperSource = PrintDocument1.DefaultPageSettings.PaperSource
        Select Case oKind
            Case DTODocRpt.FuentePapel.Original
                For Each oSrc In PrintDocument1.PrinterSettings.PaperSources
                    Select Case oSrc.SourceName
                        Case "Preprinted", "Preimprès", "Preimpreso", "Tray 3"

                            'MsgBox("oSrc.Sourcename:" & oSrc.SourceName)

                            PrintDocument1.DefaultPageSettings.PaperSource = oSrc
                            'PrintDocument1.DefaultPageSettings.PaperSource = PrintDocument1.PrinterSettings.PaperSources("Preprinted")
                            Exit For
                    End Select
                Next
            Case Else
                'oKind = Drawing.Printing.PaperSourceKind.Upper
                'oFrm.print(oKind, oDocRpt.Ensobrados.Sencillo, CopiasExtra)
        End Select

        'oKind as Drawing.Printing.PaperSourceKind
        'Dim oSrc As Drawing.Printing.PaperSource = PrintDocument1.DefaultPageSettings.PaperSource
        'For Each oSrc In PrintDocument1.PrinterSettings.PaperSources
        'If oSrc.Kind = oKind Then
        'PrintDocument1.DefaultPageSettings.PaperSource = oSrc
        'PrintDocument1.DefaultPageSettings.PaperSource = PrintDocument1.PrinterSettings.PaperSources("Preprinted")
        'Exit For
        'End If
        'Next
        PrintDocument1.PrinterSettings.Copies = CopiasExtra + 1
        'PrintDocument1.PrinterSettings.PrintToFile() = True
        PrintDocument1.Print()
        'mDataSetEnsobrado.WriteXml("E:\FRASDATASET.xml")
        'Return mDataSetEnsobrado
    End Sub
End Class
