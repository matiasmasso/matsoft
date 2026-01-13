Public Class Frm_FileAEAT115

    Private mFile As MaxiSrvr.MatFileAEAT115
    Private mAllowEvents As Boolean

    Public WriteOnly Property File() As MaxiSrvr.MatFileAEAT115
        Set(ByVal value As MaxiSrvr.MatFileAEAT115)
            mFile = value
            Refresca()
            Calcula()
            mAllowEvents = True
        End Set
    End Property

    Private Sub Refresca()
        With mFile.Regs(0)
            TextBoxMod.Text = .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a00_Modelo).Value
            TextBoxNIF.Text = .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a05_Id_NIF).Value
            TextBoxYea.Text = .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a19_Devengo_Ejercicio).Value
            TextBoxPeriod.Text = .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a20_Devengo_Periodo).Value

            'TextBoxTreballCashPerceptors.Text = .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a04_Treball_Cash_Perceptors).FormattedValue
            'TextBoxTreballCashBase.Text = .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a05_Treball_Cash_Base).FormattedValue
            'TextBoxTreballCashQuota.Text = .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a06_Treball_Cash_Quota).FormattedValue

            TextBoxContact.Text = .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a28_Contacte_Nom).FormattedValue
            TextBoxTel.Text = .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a29_Contacte_Tel).FormattedValue
            TextBoxObs.Text = .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a30_Obs).FormattedValue

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
            .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a00_Modelo).Value = TextBoxMod.Text
            .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a05_Id_NIF).Value = TextBoxNIF.Text
            .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a19_Devengo_Ejercicio).Value = TextBoxYea.Text
            .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a20_Devengo_Periodo).Value = TextBoxPeriod.Text

            '.Fields(Maxisrvr.MatFileAEAT115reg.Flds.a04_Treball_Cash_Perceptors).Value = TextBoxTreballCashPerceptors.Text
            '.Fields(Maxisrvr.MatFileAEAT115reg.Flds.a05_Treball_Cash_Base).Value = TextBoxTreballCashBase.Text
            '.Fields(Maxisrvr.MatFileAEAT115reg.Flds.a06_Treball_Cash_Quota).Value = TextBoxTreballCashQuota.Text

            .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a28_Contacte_Nom).Value = TextBoxContact.Text
            .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a29_Contacte_Tel).Value = TextBoxTel.Text
            .Fields(Maxisrvr.MatFileAEAT115reg.Flds.a30_Obs).Value = TextBoxObs.Text

        End With
    End Sub

    Private Sub Calcula()
        mAllowEvents = False
        Dim DblQuota As Decimal = 0

        'DblQuota += GetTextBoxValue(TextBoxTreballCashQuota)
        'SetTextBoxValue(TextBoxSumaQuotas, DblQuota)


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


    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles _
        'TextBoxTreballCashQuota.TextChanged

        If mAllowEvents Then
            Calcula()
        End If
    End Sub
End Class