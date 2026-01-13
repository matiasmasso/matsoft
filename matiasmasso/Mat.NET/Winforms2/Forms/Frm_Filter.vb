Public Class Frm_Filter
    Private _Filter As DTOFilter
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOFilter)
        MyBase.New()
        Me.InitializeComponent()
        _Filter = value
    End Sub

    Private Sub Frm_Filter_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Filter.Load(exs, _Filter) Then
            With _Filter
                With .langText
                    TextBoxEsp.Text = .Esp
                    TextBoxCat.Text = .Cat
                    TextBoxEng.Text = .Eng
                    TextBoxPor.Text = .Por
                End With

                Xl_FilterItems1.Load(_Filter.items)
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
        TextBoxEsp.TextChanged,
        TextBoxCat.TextChanged,
        TextBoxEng.TextChanged,
        TextBoxPor.TextChanged,
         Xl_FilterItems1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Filter
            With .langText
                .Esp = TextBoxEsp.Text
                .Cat = TextBoxCat.Text
                .Eng = TextBoxEng.Text
                .Por = TextBoxPor.Text
            End With
            .items = Xl_FilterItems1.Values
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.Filter.Update(exs, _Filter) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Filter))
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
            If Await FEB.Filter.Delete(exs, _Filter) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Filter))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


