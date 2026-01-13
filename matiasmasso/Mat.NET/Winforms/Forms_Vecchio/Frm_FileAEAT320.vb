Public Class Frm_FileAEAT303
    Private mFile As Maxisrvr.MatFileAEAT303
    Private mSngDefaultIVA As Decimal = 16
    Private mAllowEvents As Boolean

    Public WriteOnly Property File() As Maxisrvr.MatFileAEAT303
        Set(ByVal value As Maxisrvr.MatFileAEAT303)
            mFile = value
            Refresca()
            Calcula()
            mAllowEvents = True
        End Set
    End Property

    Private Sub Refresca()
        With mFile.Regs(0)
            TextBoxMod.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a02_Identificador_Modelo).Value
            TextBoxNIF.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a07_NIF).Value
            TextBoxYea.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a11_Ejercicio).Value
            TextBoxPeriod.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a12_Periodo).Value

            TextBoxDev1Base.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a13_IVA_Devengado_RegGral1_BaseImponible).FormattedValue
            TextBoxDev1Tip.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a14_IVA_Devengado_RegGral1_Tipo).FormattedValue
            TextBoxDev1Quot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a15_IVA_Devengado_RegGral1_Cuota).FormattedValue
            TextBoxDev2Base.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a16_IVA_Devengado_RegGral2_BaseImponible).FormattedValue
            TextBoxDev2Tip.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a17_IVA_Devengado_RegGral2_Tipo).FormattedValue
            TextBoxDev2Quot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a18_IVA_Devengado_RegGral2_Cuota).FormattedValue
            TextBoxDev3Base.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a19_IVA_Devengado_RegGral3_BaseImponible).FormattedValue
            TextBoxDev3Tip.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a20_IVA_Devengado_RegGral3_Tipo).FormattedValue
            TextBoxDev3Quot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a21_IVA_Devengado_RegGral3_Cuota).FormattedValue

            TextBoxReq1Base.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a22_IVA_Devengado_RecargoEquivalencia1_BaseImponible).FormattedValue
            TextBoxReq1Tip.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a23_IVA_Devengado_RecargoEquivalencia1_Tipo).FormattedValue
            TextBoxReq1Quot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a24_IVA_Devengado_RecargoEquivalencia1_Cuota).FormattedValue
            TextBoxReq2Base.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a25_IVA_Devengado_RecargoEquivalencia2_BaseImponible).FormattedValue
            TextBoxReq2Tip.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a26_IVA_Devengado_RecargoEquivalencia2_Tipo).FormattedValue
            TextBoxReq2Quot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a27_IVA_Devengado_RecargoEquivalencia2_Cuota).FormattedValue
            TextBoxReq3Base.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a28_IVA_Devengado_RecargoEquivalencia3_BaseImponible).FormattedValue
            TextBoxReq3Tip.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a29_IVA_Devengado_RecargoEquivalencia3_Tipo).FormattedValue
            TextBoxReq3Quot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a30_IVA_Devengado_RecargoEquivalencia3_Cuota).FormattedValue

            TextBoxDevECEBase.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a31_IVA_Devengado_Intracomunitario_BaseImponible).FormattedValue
            TextBoxDevECEQuot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a32_IVA_Devengado_Intracomunitario_Cuota).FormattedValue
            TextBoxDevTot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a33_IVA_Devengado_TotalCuota).FormattedValue

            TextBoxSopIntBase.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a34_IVA_Soportado_OperacionesInterioresCorrientes_BaseImponible).FormattedValue
            TextBoxSopIntQuot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a35_IVA_Soportado_OperacionesInterioresCorrientes_Cuota).FormattedValue
            TextBoxSopImpBase.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a38_IVA_Soportado_ImportacionesCorrientes_BaseImponible).FormattedValue
            TextBoxSopImpQuot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a39_IVA_Soportado_ImportacionesCorrientes_Cuota).FormattedValue
            TextBoxSopECEBase.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a42_IVA_Soportado_IntracomunitarioCorriente_BaseImponible).FormattedValue
            TextBoxSopECEQuot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a43_IVA_Soportado_IntracomunitarioCorriente_Cuota).FormattedValue
            TextBoxSopAGP.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a46_IVA_Deducible_Compensaciones_AGP_Cuota).FormattedValue
            TextBoxSopInv.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a47_IVA_Deducible_Regularizacion_Inversiones_Cuota).FormattedValue
            TextBoxSopTot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a49_IVA_Deducible_TotalCuota).FormattedValue


            TextBoxDif.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a50_Diferencia).FormattedValue
            TextBoxDifEstatPct.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a51_AtribuibleAdministracion_Tipo).FormattedValue
            TextBoxDifEstatQuot.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a52_AtribuibleAdministracion_Cuota).FormattedValue
            TextBoxDifCompensar.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a53_CuotasACompensar_PeriodosAnteriores).FormattedValue
            TextBoxDifIntracomunitari.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a54_Entregas_Intracomunitarias).FormattedValue
            TextBoxDifExportacions.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a55_Exportaciones).FormattedValue
            TextBoxDifExents.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a56_OtrasOperacionesExentas).FormattedValue

            TextBoxRes.Text = .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a58_Resultado).FormattedValue
            Xl_DropdownList_BancsNostres1.Banc = New Banc(BLLBanc.BancToReceiveTransfers.Guid)
        End With


    End Sub


    Private Sub ToolStripButtonSaveFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonSaveFile.Click
        SetFileFromForm()
        Dim oDlg As New SaveFileDialog
        With oDlg
            .FileName = mFile.DefaultFileName ' "AEAT." & TextBoxMod.Text & "." & TextBoxYea.Text & "." & TextBoxPeriod.Text & ".TXT"
            .DefaultExt = ".TXT"
            .Filter = "fitxers ASCII (*.TXT)|*.TXT|(tots els fitxers)|*.*"
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                mFile.SaveAs(.FileName)
            End If
        End With
    End Sub

    Public Sub SetFileFromForm()
        With mFile.Regs(0)
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a02_Identificador_Modelo).Value = TextBoxMod.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a07_NIF).Value = TextBoxNIF.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a11_Ejercicio).Value = TextBoxYea.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a12_Periodo).Value = TextBoxPeriod.Text

            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a13_IVA_Devengado_RegGral1_BaseImponible).Value = TextBoxDev1Base.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a14_IVA_Devengado_RegGral1_Tipo).Value = TextBoxDev1Tip.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a15_IVA_Devengado_RegGral1_Cuota).Value = TextBoxDev1Quot.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a16_IVA_Devengado_RegGral2_BaseImponible).Value = TextBoxDev2Base.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a17_IVA_Devengado_RegGral2_Tipo).Value = TextBoxDev2Tip.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a18_IVA_Devengado_RegGral2_Cuota).Value = TextBoxDev2Quot.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a19_IVA_Devengado_RegGral3_BaseImponible).Value = TextBoxDev3Base.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a20_IVA_Devengado_RegGral3_Tipo).Value = TextBoxDev3Tip.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a21_IVA_Devengado_RegGral3_Cuota).Value = TextBoxDev3Quot.Text

            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a22_IVA_Devengado_RecargoEquivalencia1_BaseImponible).Value = TextBoxReq1Base.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a23_IVA_Devengado_RecargoEquivalencia1_Tipo).Value = TextBoxReq1Tip.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a24_IVA_Devengado_RecargoEquivalencia1_Cuota).Value = TextBoxReq1Quot.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a25_IVA_Devengado_RecargoEquivalencia2_BaseImponible).Value = TextBoxReq2Base.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a26_IVA_Devengado_RecargoEquivalencia2_Tipo).Value = TextBoxReq2Tip.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a27_IVA_Devengado_RecargoEquivalencia2_Cuota).Value = TextBoxReq2Quot.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a28_IVA_Devengado_RecargoEquivalencia3_BaseImponible).Value = TextBoxReq3Base.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a29_IVA_Devengado_RecargoEquivalencia3_Tipo).Value = TextBoxReq3Tip.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a30_IVA_Devengado_RecargoEquivalencia3_Cuota).Value = TextBoxReq3Quot.Text

            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a31_IVA_Devengado_Intracomunitario_BaseImponible).Value = TextBoxDevECEBase.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a32_IVA_Devengado_Intracomunitario_Cuota).Value = TextBoxDevECEQuot.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a33_IVA_Devengado_TotalCuota).Value = TextBoxDevTot.Text

            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a34_IVA_Soportado_OperacionesInterioresCorrientes_BaseImponible).Value = TextBoxSopIntBase.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a35_IVA_Soportado_OperacionesInterioresCorrientes_Cuota).Value = TextBoxSopIntQuot.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a38_IVA_Soportado_ImportacionesCorrientes_BaseImponible).Value = TextBoxSopImpBase.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a39_IVA_Soportado_ImportacionesCorrientes_Cuota).Value = TextBoxSopImpQuot.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a42_IVA_Soportado_IntracomunitarioCorriente_BaseImponible).Value = TextBoxSopECEBase.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a43_IVA_Soportado_IntracomunitarioCorriente_Cuota).Value = TextBoxSopECEQuot.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a46_IVA_Deducible_Compensaciones_AGP_Cuota).Value = TextBoxSopAGP.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a47_IVA_Deducible_Regularizacion_Inversiones_Cuota).Value = TextBoxSopInv.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a49_IVA_Deducible_TotalCuota).Value = TextBoxSopTot.Text


            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a50_Diferencia).Value = TextBoxDif.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a51_AtribuibleAdministracion_Tipo).Value = TextBoxDifEstatPct.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a52_AtribuibleAdministracion_Cuota).Value = TextBoxDifEstatQuot.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a53_CuotasACompensar_PeriodosAnteriores).Value = TextBoxDifCompensar.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a54_Entregas_Intracomunitarias).Value = TextBoxDifIntracomunitari.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a55_Exportaciones).Value = TextBoxDifExportacions.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a56_OtrasOperacionesExentas).Value = TextBoxDifExents.Text

            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a58_Resultado).Value = TextBoxRes.Text
            .Fields(Maxisrvr.MatFileAEAT303Reg.Flds.a60_ResultadoLiquidacion).Value = TextBoxRes.Text

            Dim oBanc As MaxiSrvr.Banc = Xl_DropdownList_BancsNostres1.Banc
            .Fields(MaxiSrvr.MatFileAEAT303Reg.Flds.a70_Ingreso_Ccc_Entidad).Value = BLL.BLLIban.BankId(oBanc.Iban)
            .Fields(MaxiSrvr.MatFileAEAT303Reg.Flds.a71_Ingreso_Ccc_Oficina).Value = BLL.BLLIban.BranchId(oBanc.Iban)
            .Fields(MaxiSrvr.MatFileAEAT303Reg.Flds.a72_Ingreso_Ccc_DC).Value = oBanc.Iban.Digits.Substring(12, 2)
            .Fields(MaxiSrvr.MatFileAEAT303Reg.Flds.a73_Ingreso_Ccc_Cta).Value = oBanc.Iban.Digits.Substring(14, 10)

        End With
    End Sub

    Private Sub Calcula()
        mAllowEvents = False
        Dim DblDevengat As Decimal = 0
        DblDevengat += GetTextBoxValue(TextBoxDev1Quot)
        DblDevengat += GetTextBoxValue(TextBoxDev2Quot)
        DblDevengat += GetTextBoxValue(TextBoxDev3Quot)
        DblDevengat += GetTextBoxValue(TextBoxReq1Quot)
        DblDevengat += GetTextBoxValue(TextBoxReq2Quot)
        DblDevengat += GetTextBoxValue(TextBoxReq3Quot)
        DblDevengat += GetTextBoxValue(TextBoxDevECEQuot)
        SetTextBoxValue(TextBoxDevTot, DblDevengat)

        Dim DblSoportat As Decimal = 0
        DblSoportat += GetTextBoxValue(TextBoxSopIntQuot)
        DblSoportat += GetTextBoxValue(TextBoxSopImpQuot)
        DblSoportat += GetTextBoxValue(TextBoxSopECEQuot)
        DblSoportat += GetTextBoxValue(TextBoxSopAGP)
        DblSoportat += GetTextBoxValue(TextBoxSopInv)
        SetTextBoxValue(TextBoxSopTot, DblSoportat)

        Dim DblDif As Decimal = DblDevengat - DblSoportat
        SetTextBoxValue(TextBoxDif, DblDif)

        Dim SngEstatPct As Decimal = GetTextBoxValue(TextBoxDifEstatPct)
        Dim DblEstat As Decimal = Math.Round(DblDif * SngEstatPct / 100, 2, MidpointRounding.AwayFromZero)
        SetTextBoxValue(TextBoxDifEstatQuot, DblEstat)

        Dim DblRes As Decimal = DblEstat
        DblRes += GetTextBoxValue(TextBoxDifCompensar)
        'DblRes += GetTextBoxValue(TextBoxResRegAnual)
        SetTextBoxValue(TextBoxRes, DblRes)

        CheckPct(TextBoxDev1Base, TextBoxDev1Tip, TextBoxDev1Quot, PictureBoxWarn03)
        CheckPct(TextBoxDev2Base, TextBoxDev2Tip, TextBoxDev2Quot, PictureBoxWarn06)
        CheckPct(TextBoxDev3Base, TextBoxDev3Tip, TextBoxDev3Quot, PictureBoxWarn09)
        CheckPct(TextBoxReq1Base, TextBoxReq1Tip, TextBoxReq1Quot, PictureBoxWarn12)
        CheckPct(TextBoxReq2Base, TextBoxReq2Tip, TextBoxReq2Quot, PictureBoxWarn15)
        CheckPct(TextBoxReq3Base, TextBoxReq3Tip, TextBoxReq3Quot, PictureBoxWarn18)
        CheckPct(TextBoxDevECEBase, mSngDefaultIVA, TextBoxDevECEQuot, PictureBoxWarn20)
        CheckPct(TextBoxSopECEBase, mSngDefaultIVA, TextBoxSopECEQuot, PictureBoxWarn27)
        mAllowEvents = True
    End Sub

    Private Sub CheckPct(ByVal oTextBoxBase As TextBox, ByVal oTextBoxPct As TextBox, ByVal oTextBoxQuot As TextBox, ByVal oPictureboxWarning As PictureBox)
        Dim SngPct As Decimal = GetTextBoxValue(oTextBoxPct)
        CheckPct(oTextBoxBase, SngPct, oTextBoxQuot, oPictureboxWarning)
    End Sub

    Private Sub CheckPct(ByVal oTextBoxBase As TextBox, ByVal SngPct As Decimal, ByVal oTextBoxQuot As TextBox, ByVal oPictureboxWarning As PictureBox)
        Dim DblBase As Decimal = GetTextBoxValue(oTextBoxBase)
        Dim QuotFromBase As Decimal = Math.Round(DblBase * SngPct / 100, 2, MidpointRounding.AwayFromZero)
        Dim QuotFromTextBox As Decimal = GetTextBoxValue(oTextBoxQuot)
        oPictureboxWarning.Visible = (QuotFromBase <> QuotFromTextBox)
    End Sub

    Private Sub SetTextBoxValue(ByVal oTextBox As TextBox, ByVal DblVal As Decimal)
        oTextBox.Text = Format(DblVal, "#,##0.00;-#,##0.00;#")
    End Sub

    Private Function GetTextBoxValue(ByVal oTextBox As TextBox) As Decimal
        Dim sVal As String = oTextBox.Text
        Dim DblVal As Decimal = 0
        If IsNumeric(sVal) Then
            sVal = sVal.Replace(".", "") 'treu separador milers
            DblVal = CDbl(sVal)
        End If
        Return DblVal
    End Function


    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxDev1Base.TextChanged, _
        TextBoxDev1Tip.TextChanged, _
        TextBoxDev1Quot.TextChanged, _
        TextBoxDev2Base.TextChanged, _
        TextBoxDev2Tip.TextChanged, _
        TextBoxDev2Quot.TextChanged, _
        TextBoxDev3Base.TextChanged, _
        TextBoxDev3Tip.TextChanged, _
        TextBoxDev3Quot.TextChanged, _
        TextBoxReq1Base.TextChanged, _
        TextBoxReq1Tip.TextChanged, _
        TextBoxReq1Quot.TextChanged, _
        TextBoxReq2Base.TextChanged, _
        TextBoxReq2Tip.TextChanged, _
        TextBoxReq2Quot.TextChanged, _
        TextBoxReq3Base.TextChanged, _
        TextBoxReq3Tip.TextChanged, _
        TextBoxReq3Quot.TextChanged, _
        TextBoxDevECEBase.TextChanged, _
        TextBoxDevECEQuot.TextChanged, _
        TextBoxSopIntBase.TextChanged, _
        TextBoxSopIntQuot.TextChanged, _
        TextBoxSopImpBase.TextChanged, _
        TextBoxSopImpQuot.TextChanged, _
        TextBoxSopECEBase.TextChanged, _
        TextBoxSopECEQuot.TextChanged, _
        TextBoxSopAGP.TextChanged, _
        TextBoxSopInv.TextChanged, _
        TextBoxDifEstatPct.TextChanged, _
        TextBoxDifEstatQuot.TextChanged, _
        TextBoxDifCompensar.TextChanged, _
        TextBoxDifIntracomunitari.TextChanged, _
        TextBoxDifExportacions.TextChanged, _
        TextBoxDifExents.TextChanged

        If mAllowEvents Then
            Calcula()
        End If
    End Sub
End Class