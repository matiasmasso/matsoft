Public Class Frm_Rol

    Private _Rol As DTORol
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTORol)
        MyBase.New()
        Me.InitializeComponent()
        _Rol = value
        BLL.BLLRol.Load(_Rol)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Rol
            TextBoxId.Text = CInt(_Rol.Id)
            TextBoxIdNom.Text = _Rol.Id.ToString
            TextBoxNomEsp.Text = .NomEsp
            TextBoxNomCat.Text = .NomCat
            TextBoxNomEng.Text = .NomEng
            TextBoxDsc.Text = .Dsc
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = BLL.BLLSession.Current.User.Rol.Id = Rol.Ids.SuperUser
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNomEsp.TextChanged, _
         TextBoxNomCat.TextChanged, _
          TextBoxNomEng.TextChanged, _
           TextBoxDsc.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Rol
            .NomEsp = TextBoxNomEsp.Text
            .NomCat = TextBoxNomCat.Text
            .NomEng = TextBoxNomEng.Text
            .Dsc = TextBoxDsc.Text
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLRol.Update(_Rol, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Rol))
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
            If BLL.BLLRol.Delete(_Rol, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Rol))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


