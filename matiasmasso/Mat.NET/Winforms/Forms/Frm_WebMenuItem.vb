Public Class Frm_WebMenuItem
    Private _WebMenuItem As DTOWebMenuItem
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOWebMenuItem)
        MyBase.New()
        Me.InitializeComponent()
        _WebMenuItem = value
        _AllowEvents = True
    End Sub

    Private Async Sub Frm_WebMenuItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.WebMenuItem.Load(exs, _WebMenuItem) Then
            Dim allRols = Await FEB2.Rols.All(exs)
            If exs.Count = 0 Then
                With _WebMenuItem
                    LabelGroup.Text = DTOWebMenuGroup.Nom(.Group, Current.Session.User.Lang)
                    If .LangText IsNot Nothing Then
                        TextBoxEsp.Text = .LangText.Text(DTOLang.ESP)
                        TextBoxCat.Text = .LangText.Text(DTOLang.CAT)
                        TextBoxEng.Text = .LangText.Text(DTOLang.ENG)
                        TextBoxPor.Text = .LangText.Text(DTOLang.POR)
                    End If
                    TextBoxUrl.Text = .Url
                    CheckBoxActiu.Checked = .Actiu
                    Xl_RolsAllowed1.Load(allRols, .Rols)
                    ButtonOk.Enabled = .IsNew
                    ButtonDel.Enabled = Not .IsNew
                End With
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        End If

    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxEsp.TextChanged,
           TextBoxCat.TextChanged,
            TextBoxEng.TextChanged,
            TextBoxPor.TextChanged,
             TextBoxUrl.TextChanged,
              CheckBoxActiu.CheckedChanged,
              Xl_RolsAllowed1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _WebMenuItem
            .LangText = New DTOLangText(TextBoxEsp.Text, TextBoxCat.Text, TextBoxEng.Text, TextBoxPor.Text)
            .Url = TextBoxUrl.Text
            .Actiu = CheckBoxActiu.Checked
            .Rols = Xl_RolsAllowed1.CheckedValues
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.WebMenuItem.Update(exs, _WebMenuItem) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebMenuItem))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
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
            If Await FEB2.WebMenuItem.Delete(exs, _WebMenuItem) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebMenuItem))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


