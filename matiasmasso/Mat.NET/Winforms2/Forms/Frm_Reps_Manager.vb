Public Class Frm_Reps_Manager
    Dim oProgressBar As Xl_ProgressBar

    Private Enum Tabs
        Conflicts
        Pending
        RepLiqs
    End Enum

    Private Async Sub ButtonSaveNewRepLiq_Click(sender As Object, e As EventArgs) Handles ButtonSaveNewRepLiq.Click
        Dim exs As New List(Of Exception)

        oProgressBar = New Xl_ProgressBar()
        oProgressBar.Dock = DockStyle.Fill
        oProgressBar.BringToFront()
        Panel1.Controls.Add(oProgressBar)
        ButtonCancel.Visible = False
        ButtonSaveNewRepLiq.Visible = False

        Dim cancelRequest As Boolean
        Dim msg As String = Current.Session.Tradueix("Generando las liquidaciones...", "Generant les liquidacions...", "Building commission statements...")
        oProgressBar.ShowProgress(0, 100, 0, msg, cancelRequest)
        Dim oRepLiqs As List(Of DTORepLiq) = Await Xl_RepComLiquidableNewRepLiq1.RepLiqs(exs)

        If Await FEB.Repliqs.Update(exs, oRepLiqs, Current.Session.User, AddressOf oProgressBar.ShowProgress) Then
            MsgBox(oRepLiqs.Count & " noves liquidacions registrades", MsgBoxStyle.Information)
            Me.Close()
        Else
            Panel1.Controls.Remove(oProgressBar)
            ButtonCancel.Visible = True
            ButtonSaveNewRepLiq.Visible = True
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.RepLiqs
                Await RefrescaRepLiqs()
        End Select
    End Sub

    Private Async Sub Xl_RepLiqs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RepLiqs1.RequestToRefresh
        Await RefrescaRepLiqs()
    End Sub

    Private Async Function RefrescaRepLiqs() As Task
        Dim exs As New List(Of Exception)

        ProgressBarRepLiqs.Visible = True
        Dim oRepLiqs = Await FEB.Repliqs.Headers(Current.Session.Emp, exs)
        ProgressBarRepLiqs.Visible = False

        If exs.Count = 0 Then
            Xl_RepLiqs1.Load(oRepLiqs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Buttonransferencies_Click(sender As Object, e As EventArgs) Handles Buttonransferencies.Click
        Dim oFrm As New Frm_BancTransferFactory(Frm_BancTransferFactory.Modes.Reps)
        oFrm.Show()
    End Sub

    Private Sub o(sender As Object, e As MatEventArgs) Handles Xl_RepComLiquidableNewRepLiq1.ToggleProggressBar
        UIHelper.ToggleProggressBar(Panel1, e.Argument)
    End Sub


End Class