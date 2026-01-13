Public Class Frm_RepComLiquidable
    Private _RepComLiquidable As DTORepComLiquidable
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Public Sub New(oRepComLiquidable As DTORepComLiquidable)
        MyBase.New()
        Me.InitializeComponent()
        _RepComLiquidable = oRepComLiquidable
        With _RepComLiquidable
            TextBoxRep.Text = .Rep.NickName
            TextBoxFra.Text = DTOInvoice.Caption(.Fra)
            If .RepLiq IsNot Nothing Then
                TextBoxRepLiq.Text = "Liquidació " & .RepLiq.Id & " del " & .RepLiq.Fch
            End If
            CheckBoxLiquidable.Checked = .Liquidable
            TextBoxObs.Text = .Obs
            Xl_RepComLiquidableArcs1.LineItmArcs = .Items
        End With
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
         CheckBoxLiquidable.CheckedChanged, _
          TextBoxObs.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _RepComLiquidable
            .Liquidable = CheckBoxLiquidable.Checked
            .Obs = TextBoxObs.Text
            Dim exs As New List(Of Exception)

            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB.RepComLiquidable.Update(_RepComLiquidable, exs) Then
                RaiseEvent AfterUpdate(_RepComLiquidable, EventArgs.Empty)
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs, "No s'ha pogut desar:")
            End If
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

End Class