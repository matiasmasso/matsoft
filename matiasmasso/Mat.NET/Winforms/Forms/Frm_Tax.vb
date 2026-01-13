

Public Class Frm_Tax
    Private _Tax As DTOTax
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oObject As Object)
        MyBase.new()
        Me.InitializeComponent()
        _Tax = oObject
        'Me.Text = _Tax.ToString
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        With _Tax
            TextBoxCodi.Text = _Tax.Codi.ToString.Replace("_", " ")
            DateTimePicker1.Value = .Fch
            Xl_PercentTipus.Value = .Tipus
            If Not .IsNew Then
                ButtonDel.Enabled = True
            End If
        End With
    End Sub

    Private Sub DateTimePicker1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DateTimePicker1.Validating
        If _AllowEvents Then
            Dim DtFch As Date = DateTimePicker1.Value
            If DTOApp.Current.Taxes.Any(Function(x) x.Codi = _Tax.Codi And x.Fch = DtFch) Then
                UIHelper.WarnError("ja existeix una entrada amb aquest codi i data.")
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         DateTimePicker1.ValueChanged, _
          Xl_PercentTipus.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Tax
            .Fch = DateTimePicker1.Value
            .Tipus = Xl_PercentTipus.Value
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Tax.Update(_Tax, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Tax))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, True)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & DTOTax.Nom(_Tax.Codi) & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Tax.Delete(_Tax, exs) Then
                RaiseEvent AfterUpdate(Me, EventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar l'impost")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class