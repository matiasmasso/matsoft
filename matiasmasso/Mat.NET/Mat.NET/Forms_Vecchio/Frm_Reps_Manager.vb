Public Class Frm_Reps_Manager

    Private Enum Tabs
        Conflicts
        Pending
        RepLiqs
    End Enum

    Private Sub ButtonSaveNewRepLiq_Click(sender As Object, e As EventArgs) Handles ButtonSaveNewRepLiq.Click
        Dim exs As New List(Of Exception)
        Dim oRepLiqs As List(Of DTO.DTORepLiq) = Xl_RepComLiquidableNewRepLiq1.RepLiqs(exs)

        If exs.Count = 0 Then
            If BLL_RepLiqs.Update(oRepLiqs, exs) Then
                MsgBox(oRepLiqs.Count & " noves liquidacions registrades")
            End If
        End If

        If exs.Count > 0 Then
            MsgBox("error al registrar les liquidacions:" & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.RepLiqs
                RefrescaRepLiqs()
        End Select
    End Sub

    Private Sub Xl_RepLiqs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RepLiqs1.RequestToRefresh
        RefrescaRepLiqs()
    End Sub

    Private Sub RefrescaRepLiqs()
        Dim oRepLiqs As List(Of DTORepLiq) = BLL_RepLiqs.All(Today.Year)
        Xl_RepLiqs1.Load(oRepLiqs)
    End Sub

    Private Sub Buttonransferencies_Click(sender As Object, e As EventArgs) Handles Buttonransferencies.Click
        Dim oFrm As New Frm_Rep_Transfers
        With oFrm
            .NewTransferCod = Frm_Rep_Transfers.NewTransferCods.Reps
            .Show()
        End With
    End Sub

End Class