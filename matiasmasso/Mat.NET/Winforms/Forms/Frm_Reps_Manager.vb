Public Class Frm_Reps_Manager

    Private Enum Tabs
        Conflicts
        Pending
        RepLiqs
    End Enum

    Private Async Sub ButtonSaveNewRepLiq_Click(sender As Object, e As EventArgs) Handles ButtonSaveNewRepLiq.Click
        Dim exs As New List(Of Exception)

        ButtonSaveNewRepLiq.Enabled = False
        Application.DoEvents()

        ProgressBarSave.Visible = True
        Dim oRepLiqs As List(Of DTORepLiq) = Await Xl_RepComLiquidableNewRepLiq1.RepLiqs(exs)

        If exs.Count = 0 Then
            Dim iSuccessCount As Integer
            Dim iFailedCount As Integer
            For Each oRepliq In oRepLiqs
                Dim ex2 As New List(Of Exception)
                oRepliq = Await FEB2.Repliq.Update(ex2, oRepliq, GlobalVariables.Emp)
                If ex2.Count = 0 Then
                    iSuccessCount += 1
                Else
                    If oRepliq Is Nothing Then
                        exs.Add(New Exception("error al desar les liquidacions"))
                    Else
                        exs.Add(New Exception("error al desar la liquidació de " & oRepliq.rep.NicknameOrNom()))
                    End If
                    exs.AddRange(ex2)
                    iFailedCount += 1
                End If
            Next
            ProgressBarSave.Visible = False
            If exs.Count = 0 Then
                MsgBox(oRepLiqs.Count & " noves liquidacions registrades")
                Me.Close()
            Else
                ButtonSaveNewRepLiq.Enabled = True
                exs.Insert(0, (New Exception(String.Format("{0} de {1} liquidacions registrades", iSuccessCount, iFailedCount))))
                UIHelper.WarnError(exs)
            End If
        Else
            ProgressBarSave.Visible = False
            ButtonSaveNewRepLiq.Enabled = True
            UIHelper.WarnError(exs, "error al registrar les liquidacions:")
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
        Dim oRepLiqs = Await FEB2.Repliqs.Headers(Current.Session.Emp, exs)
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


End Class