Public Class Frm_Computer
    Private _Computer As DTOComputer
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOComputer)
        MyBase.New()
        Me.InitializeComponent()
        _Computer = value
    End Sub

    Private Sub Frm_Computer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Computer.Load(exs, _Computer) Then
            With _Computer
                TextBoxNom.Text = .Nom
                If .FchFrom > DateTimePickerFchFrom.MinDate Then
                    CheckBoxFchFrom.Checked = True
                    DateTimePickerFchFrom.Visible = True
                    DateTimePickerFchFrom.Value = .FchFrom
                End If
                If .FchTo > DateTimePickerFchTo.MinDate Then
                    CheckBoxFchTo.Checked = True
                    DateTimePickerFchTo.Visible = True
                    DateTimePickerFchTo.Value = .FchTo
                End If
                RichTextBox1.Text = .Text
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
        TextBoxNom.TextChanged,
        RichTextBox1.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Computer
            .Nom = TextBoxNom.Text
            .FchFrom = If(CheckBoxFchFrom.Checked, DateTimePickerFchFrom.Value, Nothing)
            .FchTo = If(CheckBoxFchTo.Checked, DateTimePickerFchTo.Value, Nothing)
            .Text = RichTextBox1.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.Computer.Update(exs, _Computer) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Computer))
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
            If Await FEB.Computer.Delete(exs, _Computer) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Computer))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub CopyUrlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyUrlToolStripMenuItem.Click
        UIHelper.CopyToClipboard(_Computer.Url)
    End Sub

    Private Sub CheckBoxFchFrom_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchFrom.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchFrom.Visible = CheckBoxFchFrom.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxFchTo_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchTo.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchTo.Visible = CheckBoxFchTo.Checked
            ButtonOk.Enabled = True
        End If
    End Sub
End Class


