Public Class Frm_Credencial
    Private _Credencial As DTOCredencial
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCredencial)
        MyBase.New()
        Me.InitializeComponent()
        _Credencial = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Credencial
            TextBoxContact.Text = .Contact.FullNom
            TextBoxReferencia.Text = .Referencia
            TextBoxUrl.Text = .Url
            TextBoxUsuari.Text = .Usuari
            TextBoxPwd.Text = .Password
            TextBoxObs.Text = .Obs
            Xl_Rols_Allowed1.Load(.Rols)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
 _
        TextBoxReferencia.TextChanged, _
        TextBoxUsuari.TextChanged, _
        TextBoxPwd.TextChanged, _
        TextBoxObs.TextChanged, _
        Xl_Rols_Allowed1.AfterUpdate

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        With _Credencial
            .Referencia = TextBoxReferencia.Text
            .Url = TextBoxUrl.Text
            .Usuari = TextBoxUsuari.Text
            .Password = TextBoxPwd.Text
            .Obs = TextBoxObs.Text
            .Rols = Xl_Rols_Allowed1.Rols
        End With
        If BLL.BLLCredencial.Update(_Credencial, BLL.BLLSession.Current.User, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Credencial))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar les credencials")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLCredencial.Delete(_Credencial, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Credencial))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class