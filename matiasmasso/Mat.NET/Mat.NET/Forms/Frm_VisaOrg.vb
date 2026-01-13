Public Class Frm_VisaOrg
    Private _VisaOrg As DTOVisaEmisor
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOVisaEmisor)
        MyBase.New()
        Me.InitializeComponent()
        _VisaOrg = value
        BLL.BLLVisaOrg.Load(_VisaOrg)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _VisaOrg
            TextBoxNom.Text = .Nom
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _VisaOrg
            .Nom = TextBoxNom.Text
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLVisaOrg.Update(_VisaOrg, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_VisaOrg))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLVisaOrg.Delete(_VisaOrg, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_VisaOrg))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


