Public Class Frm_WebMenuGroup
    Private _WebMenuGroup As DTOWebMenuGroup
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOWebMenuGroup)
        MyBase.New()
        Me.InitializeComponent()
        _WebMenuGroup = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.WebMenuGroup.Load(exs, _WebMenuGroup) Then
            With _WebMenuGroup
                TextBoxEsp.Text = .LangText.Text(DTOLang.ESP)
                TextBoxCat.Text = .LangText.Text(DTOLang.CAT)
                TextBoxEng.Text = .LangText.Text(DTOLang.ENG)
                TextBoxPor.Text = .LangText.Text(DTOLang.POR)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
          TextBoxEsp.TextChanged,
           TextBoxCat.TextChanged,
            TextBoxEng.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _WebMenuGroup
            .LangText = New DTOLangText(TextBoxEsp.Text, TextBoxCat.Text, TextBoxEng.Text, TextBoxPor.Text)
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.WebMenuGroup.Update(exs, _WebMenuGroup) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebMenuGroup))
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
            If Await FEB2.WebMenuGroup.Delete(exs, _WebMenuGroup) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebMenuGroup))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub



End Class


