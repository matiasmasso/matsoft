Public Class Frm_SkuSwitch
    Private _Value As DTOProductSku.Switch
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOProductSku.Switch)
        MyBase.New()
        Me.InitializeComponent()
        _Value = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.ProductSkuSwitch.Load(exs, _Value) Then
            With _Value
                DateTimePicker1.Value = .Fch
                Xl_LookupProductFrom.Load(.SkuFrom, DTOProduct.SelectionModes.SelectSku)
                If RadioButtonSwitch.Checked Then
                    .SkuTo = Xl_LookupProductTo.Value
                Else
                    .SkuTo = Nothing
                End If
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        DateTimePicker1.ValueChanged,
           RadioButtonSwitch.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Value
            .Fch = DateTimePicker1.Value
            .SkuFrom = Xl_LookupProductFrom.Value
            If RadioButtonSwitch.Checked Then
                .SkuTo = Xl_LookupProductTo.Value
            Else
                .SkuTo = Nothing
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB2.ProductSkuSwitch.Update(exs, _Value) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Value))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.ProductSkuSwitch.Delete(exs, _Value) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Value))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


    Private Sub RadioButtonSwitch_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSwitch.CheckedChanged
        Xl_LookupProductTo.Enabled = RadioButtonSwitch.Checked
    End Sub
End Class