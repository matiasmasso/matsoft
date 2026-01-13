Public Class Frm_StaffPos
    Private _StaffPos As DTOStaffPos
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOStaffPos)
        MyBase.New()
        Me.InitializeComponent()
        _StaffPos = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.StaffPos.Load(exs, _StaffPos) Then
            With _StaffPos
                TextBoxNomEsp.Text = .LangNom.Esp
                TextBoxNomCat.Text = .LangNom.Cat
                TextBoxNomEng.Text = .LangNom.Eng
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNomEsp.TextChanged,
         TextBoxNomCat.TextChanged,
          TextBoxNomEng.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        _StaffPos.LangNom = New DTOLangText(TextBoxNomEsp.Text, TextBoxNomCat.Text, TextBoxNomEng.Text)

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.StaffPos.Update(exs, _StaffPos) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_StaffPos))
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
            If Await FEB.StaffPos.Delete(exs, _StaffPos) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_StaffPos))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


