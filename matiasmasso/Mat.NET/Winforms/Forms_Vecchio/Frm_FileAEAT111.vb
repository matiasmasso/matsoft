Public Class Frm_FileAEAT111

    Private mFile As MaxiSrvr.MatFileAEAT111
    Private mAllowEvents As Boolean

    Public WriteOnly Property File() As MaxiSrvr.MatFileAEAT111
        Set(ByVal value As MaxiSrvr.MatFileAEAT111)
            mFile = value
            Refresca()
            Calcula()
            mAllowEvents = True
        End Set
    End Property

    Private Sub Refresca()
        With mFile.Regs(0)
            TextBoxMod.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a00_ConceptoFiscal).Value
            TextBoxNIF.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a01_NIF).Value
            TextBoxYea.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a02_Ejercicio).Value
            TextBoxPeriod.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a03_Periodo).Value

            TextBoxTreballCashPerceptors.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a04_Treball_Cash_Perceptors).FormattedValue
            TextBoxTreballCashBase.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a05_Treball_Cash_Base).FormattedValue
            TextBoxTreballCashQuota.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a06_Treball_Cash_Quota).FormattedValue
            TextBoxTreballSpecPerceptors.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a07_Treball_Espc_Perceptors).FormattedValue
            TextBoxTreballSpecBase.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a08_Treball_Espc_Base).FormattedValue
            TextBoxTreballSpecQuota.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a09_Treball_Espc_Quota).FormattedValue

            TextBoxProCashPerceptors.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a10_ActEcon_Cash_Perceptors).FormattedValue
            TextBoxProCashBase.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a11_ActEcon_Cash_Base).FormattedValue
            TextBoxProCashQuota.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a12_ActEcon_Cash_Quota).FormattedValue
            TextBoxProSpecPerceptors.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a13_ActEcon_Espc_Perceptors).FormattedValue
            TextBoxProSpecBase.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a14_ActEcon_Espc_Base).FormattedValue
            TextBoxProSpecQuota.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a15_ActEcon_Espc_Quota).FormattedValue

            TextBoxPremCashPerceptors.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a16_Premios_Cash_Perceptors).FormattedValue
            TextBoxPremCashBase.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a17_Premios_Cash_Base).FormattedValue
            TextBoxPremCashQuota.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a18_Premios_Cash_Quota).FormattedValue
            TextBoxPremSpecPerceptors.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a19_Premios_Espc_Perceptors).FormattedValue
            TextBoxPremSpecBase.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a20_Premios_Espc_Base).FormattedValue
            TextBoxPremSpecQuota.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a21_Premios_Espc_Quota).FormattedValue

            TextBoxForestCashPerceptors.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a22_Forestl_Cash_Perceptors).FormattedValue
            TextBoxForestCashBase.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a23_Forestl_Cash_Base).FormattedValue
            TextBoxForestCashQuota.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a24_Forestl_Cash_Quota).FormattedValue
            TextBoxForestSpecPerceptors.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a25_Forestl_Espc_Perceptors).FormattedValue
            TextBoxForestSpecBase.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a26_Forestl_Espc_Base).FormattedValue
            TextBoxForestSpecQuota.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a27_Forestl_Espc_Quota).FormattedValue

            TextBoxImgCashPerceptors.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a28_DretImg_Espc_Perceptors).FormattedValue
            TextBoxImgCashBase.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a29_DretImg_Espc_Base).FormattedValue
            TextBoxImgCashQuota.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a30_DretImg_Espc_Quota).FormattedValue

            TextBoxSumaQuotas.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a31_Suma_Quotes).FormattedValue
            TextBoxComplementaria.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a32_Quotes_Decl_Anteriors).FormattedValue
            TextBoxTotal.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a33_Resultat_a_Ingresar).FormattedValue

            ' .Fields(maxisrvr.MatFileAEAT111reg.Flds.a34_Codi_Decl_anterior).FormattedValue
            TextBoxContact.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a35_PersonaDeContacto).FormattedValue
            TextBoxTel.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a36_TelefonoDeContacto).FormattedValue
            TextBoxObs.Text = .Fields(maxisrvr.MatFileAEAT111reg.Flds.a37_Observaciones).FormattedValue

        End With


    End Sub


    Private Sub ToolStripButtonSaveFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonSaveFile.Click
        SetFileFromForm()
        Dim oDlg As New SaveFileDialog
        With oDlg
            .FileName = mFile.DefaultFileName
            .DefaultExt = ".TXT"
            .Filter = "fitxers ASCII (*.TXT)|*.TXT|(tots els fitxers)|*.*"
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                mFile.SaveAs(.FileName)
            End If
        End With
    End Sub

    Public Sub SetFileFromForm()
        With mFile.Regs(0)
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a00_ConceptoFiscal).Value = TextBoxMod.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a01_NIF).Value = TextBoxNIF.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a02_Ejercicio).Value = TextBoxYea.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a03_Periodo).Value = TextBoxPeriod.Text

            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a04_Treball_Cash_Perceptors).Value = TextBoxTreballCashPerceptors.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a05_Treball_Cash_Base).Value = TextBoxTreballCashBase.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a06_Treball_Cash_Quota).Value = TextBoxTreballCashQuota.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a07_Treball_Espc_Perceptors).Value = TextBoxTreballSpecPerceptors.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a08_Treball_Espc_Base).Value = TextBoxTreballSpecBase.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a09_Treball_Espc_Quota).Value = TextBoxTreballSpecQuota.Text

            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a10_ActEcon_Cash_Perceptors).Value = TextBoxProCashPerceptors.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a11_ActEcon_Cash_Base).Value = TextBoxProCashBase.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a12_ActEcon_Cash_Quota).Value = TextBoxProCashQuota.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a13_ActEcon_Espc_Perceptors).Value = TextBoxProSpecPerceptors.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a14_ActEcon_Espc_Base).Value = TextBoxProSpecBase.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a15_ActEcon_Espc_Quota).Value = TextBoxProSpecQuota.Text

            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a16_Premios_Cash_Perceptors).Value = TextBoxPremCashPerceptors.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a17_Premios_Cash_Base).Value = TextBoxPremCashBase.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a18_Premios_Cash_Quota).Value = TextBoxPremCashQuota.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a19_Premios_Espc_Perceptors).Value = TextBoxPremSpecPerceptors.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a20_Premios_Espc_Base).Value = TextBoxPremSpecBase.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a21_Premios_Espc_Quota).Value = TextBoxPremSpecQuota.Text

            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a22_Forestl_Cash_Perceptors).Value = TextBoxForestCashPerceptors.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a23_Forestl_Cash_Base).Value = TextBoxForestCashBase.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a24_Forestl_Cash_Quota).Value = TextBoxForestCashQuota.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a25_Forestl_Espc_Perceptors).Value = TextBoxForestSpecPerceptors.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a26_Forestl_Espc_Base).Value = TextBoxForestSpecBase.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a27_Forestl_Espc_Quota).Value = TextBoxForestSpecQuota.Text

            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a28_DretImg_Espc_Perceptors).Value = TextBoxImgCashPerceptors.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a29_DretImg_Espc_Base).Value = TextBoxImgCashBase.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a30_DretImg_Espc_Quota).Value = TextBoxImgCashQuota.Text

            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a31_Suma_Quotes).Value = TextBoxSumaQuotas.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a32_Quotes_Decl_Anteriors).Value = TextBoxComplementaria.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a33_Resultat_a_Ingresar).Value = TextBoxTotal.Text

            '.Fields(maxisrvr.MatFileAEAT111reg.Flds.a34_Codi_Decl_anterior).Value 
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a35_PersonaDeContacto).Value = TextBoxContact.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a36_TelefonoDeContacto).Value = TextBoxTel.Text
            .Fields(maxisrvr.MatFileAEAT111reg.Flds.a37_Observaciones).Value = TextBoxObs.Text

        End With
    End Sub

    Private Sub Calcula()
        mAllowEvents = False
        Dim DblQuota As Decimal = 0
        DblQuota += GetTextBoxValue(TextBoxTreballCashQuota)
        DblQuota += GetTextBoxValue(TextBoxTreballSpecQuota)
        DblQuota += GetTextBoxValue(TextBoxProCashQuota)
        DblQuota += GetTextBoxValue(TextBoxProSpecQuota)
        DblQuota += GetTextBoxValue(TextBoxPremCashQuota)
        DblQuota += GetTextBoxValue(TextBoxPremSpecQuota)
        DblQuota += GetTextBoxValue(TextBoxForestCashQuota)
        DblQuota += GetTextBoxValue(TextBoxForestSpecQuota)
        DblQuota += GetTextBoxValue(TextBoxImgCashQuota)
        SetTextBoxValue(TextBoxSumaQuotas, DblQuota)

        DblQuota += GetTextBoxValue(TextBoxComplementaria)
        SetTextBoxValue(TextBoxTotal, DblQuota)

        mAllowEvents = True
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
        TextBoxTreballCashQuota.TextChanged, _
        TextBoxTreballSpecQuota.TextChanged, _
        TextBoxProCashQuota.TextChanged, _
        TextBoxProSpecQuota.TextChanged, _
        TextBoxPremCashQuota.TextChanged, _
        TextBoxPremSpecQuota.TextChanged, _
        TextBoxForestCashQuota.TextChanged, _
        TextBoxForestSpecQuota.TextChanged, _
        TextBoxImgCashQuota.TextChanged, _
        TextBoxComplementaria.TextChanged


        If mAllowEvents Then
            Calcula()
        End If
    End Sub
End Class