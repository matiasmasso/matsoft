Public Class Frm_StaffCategory

    Private _StaffCategory As DTOStaffCategory
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOStaffCategory)
        MyBase.New()
        Me.InitializeComponent()
        _StaffCategory = value

    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.StaffCategory.Load(exs, _StaffCategory) Then
            With _StaffCategory
                TextBox1.Text = .Nom
                Xl_LookupSegSocialGrup1.Load(.SegSocialGrup)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBox1.TextChanged,
         Xl_LookupSegSocialGrup1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _StaffCategory
            .Nom = TextBox1.Text
            .SegSocialGrup = Xl_LookupSegSocialGrup1.SegSocialGrup
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.StaffCategory.Update(exs, _StaffCategory) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_StaffCategory))
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
            If Await FEB2.StaffCategory.Delete(exs, _StaffCategory) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_StaffCategory))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class