Public Class Frm_Ibans
    Private _AllowEvents As Boolean

    Private Sub Frm_Ibans_Load(sender As Object, e As EventArgs) Handles Me.Load
        UIHelper.LoadComboFromEnum(ComboBoxFormat, GetType(DTOIban.Formats), "(tots)")
        ComboBoxStatus.SelectedIndex = DTOIban.StatusEnum.All
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        Dim oStatus As DTOIban.StatusEnum = ComboBoxStatus.SelectedIndex

        LabelCount.Text = ""
        Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Dim oIbans As List(Of DTOIban) = BLL.BLLIbans.All(oStatus)
        LabelCount.Text = Format(oIbans.Count, "#,###") & " mandats"
        Xl_Ibans1.Load(oIbans)
        Cursor = Cursors.Default
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
  ComboBoxStatus.SelectedIndexChanged, _
       ComboBoxFormat.SelectedIndexChanged

        If _AllowEvents Then refresca()
    End Sub
End Class