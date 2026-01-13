Public Class Frm_LocalizedString
    Private _LocalizedString As DTOLocalizedString
    Private _locale As String
    Private _allowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oLocalizedString As DTOLocalizedString, locale As String)
        MyBase.New
        InitializeComponent()
        _LocalizedString = oLocalizedString
        _locale = locale
    End Sub

    Private Sub Frm_LocalizedString_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.LocalizedString.Load(exs, _LocalizedString) Then
            With _LocalizedString
                TextBoxKey.Text = .key
                TextBoxLocale.Text = _locale
                Dim item = .items.FirstOrDefault(Function(x) x.locale = _locale)
                If item IsNot Nothing Then
                    TextBoxValue.Text = item.value
                End If
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _allowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxKey.TextChanged,
         TextBoxLocale.TextChanged,
          TextBoxValue.TextChanged

        If _allowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _LocalizedString
            .key = TextBoxKey.Text
            Dim item = _LocalizedString.FindOrAddItem(_locale)
            item.value = TextBoxValue.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.LocalizedString.Update(exs, _LocalizedString) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_LocalizedString))
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
            If Await FEB.LocalizedString.Delete(exs, _LocalizedString) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_LocalizedString))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class

