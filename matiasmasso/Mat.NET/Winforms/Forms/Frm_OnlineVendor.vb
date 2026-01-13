Public Class Frm_OnlineVendor

    Private _OnlineVendor As DTOOnlineVendor
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOOnlineVendor)
        MyBase.New()
        Me.InitializeComponent()
        _OnlineVendor = value
        BLL.BLLOnlineVendor.Load(_OnlineVendor, True)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _OnlineVendor
            TextBoxNom.Text = .Nom
            TextBoxUrl.Text = .Url
            TextBoxLandingPage.Text = .LandingPage
            TextBoxObs.Text = .Obs
            Xl_Contact21.Contact = .Customer
            Xl_Image1.Bitmap = .Logo
            CheckBoxIsActive.Checked = .IsActive
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxUrl.TextChanged,
          TextBoxLandingPage.TextChanged,
           TextBoxObs.TextChanged,
            CheckBoxIsActive.CheckedChanged,
             Xl_Contact21.AfterUpdate,
              Xl_Image1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _OnlineVendor
            .Nom = TextBoxNom.Text
            .Url = TextBoxUrl.Text
            .LandingPage = TextBoxLandingPage.Text
            .Obs = TextBoxObs.Text
            .Customer = DTOCustomer.FromContact(Xl_Contact21.Contact)
            .IsActive = CheckBoxIsActive.Checked
            .Logo = Xl_Image1.Bitmap
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLOnlineVendor.Update(_OnlineVendor, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_OnlineVendor))
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
            If BLL.BLLOnlineVendor.Delete(_OnlineVendor, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_OnlineVendor))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


