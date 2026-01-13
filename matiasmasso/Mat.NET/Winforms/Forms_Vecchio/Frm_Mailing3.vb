Public Class Frm_Mailing3

    Private _AllowEvents As Boolean

    Private Sub Frm_Mailing2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oRols As List(Of DTORol) = BLL_Rols.All
        Xl_Rols_CheckList1.Load(oRols)
        _AllowEvents = True
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oRols As List(Of DTORol) = Xl_Rols_CheckList1.CheckedValues
        Dim oProducts As New List(Of Product)
        Dim oUsuaris As List(Of DTOUsuari) = BLL_MailingDestinataris.All(oRols, oProducts)

        If CheckBoxIncludeInfo.Checked Then
            Dim oInfo As DTOUsuari = BLL_Usuari.FromEmail("info@matiasmasso.es")
            oUsuaris.Add(oInfo)
        End If

        If CheckBoxExclouWordpress.Checked Then
            BLL_MailingDestinataris.ExcludeWordPressBlogSubscriptors(oUsuaris)
        End If


        Dim oCsv As DTOCsv = BLL_MailingDestinataris.CsvFile(oUsuaris)
        If UIHelper.SaveCsvDialog(oCsv, "desar fitxer destinataris") Then
            Me.Close()
        Else
            UIHelper.WarnError("error al desar el fitxer")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        Xl_Rols_CheckList1.AfterUpdate, _
         CheckBoxIncludeInfo.CheckedChanged, _
          CheckBoxExclouWordpress.CheckedChanged, _
           CheckBoxFchFrom.CheckedChanged, _
            CheckBoxMinVolume.CheckedChanged, _
             CheckBoxProduct.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub
End Class