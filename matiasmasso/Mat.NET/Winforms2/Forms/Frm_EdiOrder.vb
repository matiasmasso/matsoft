Public Class Frm_EdiOrder
    Private _EdiOrder As DTOEdiOrder
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOEdiOrder)
        MyBase.New()
        Me.InitializeComponent()
        _EdiOrder = value
    End Sub

    Private Sub Frm_EdiOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.EdiOrder.Load(exs, _EdiOrder) Then
            With _EdiOrder
                ' TextBox1.Text = .Nom
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
        TextBox1.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _EdiOrder
            '.Nom = TextBox1.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.EdiOrder.Update(exs, _EdiOrder) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_EdiOrder))
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
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.EdiOrder.Delete(exs, _EdiOrder) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_EdiOrder))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


