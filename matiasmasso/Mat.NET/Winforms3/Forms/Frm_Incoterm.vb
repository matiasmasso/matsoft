Public Class Frm_Incoterm
    Private _Incoterm As DTOIncoterm
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOIncoterm)
        MyBase.New()
        Me.InitializeComponent()
        _Incoterm = value
    End Sub

    Private Sub Frm_Incoterm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Incoterm.Load(exs, _Incoterm) Then
            With _Incoterm
                TextBoxId.Text = .Id
                TextBoxDsc.Text = .Nom
            End With
            ButtonDel.Enabled = True
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxId.TextChanged,
        TextBoxDsc.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Incoterm
            .Id = TextBoxId.Text
            .Nom = TextBoxDsc.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.Incoterm.Update(exs, _Incoterm) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Incoterm))
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
            If Await FEB.Incoterm.Delete(exs, _Incoterm) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Incoterm))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


