Public Class Frm_PaymentGateway

    Private _PaymentGateway As DTOPaymentGateway
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPaymentGateway)
        MyBase.New()
        Me.InitializeComponent()
        _PaymentGateway = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.PaymentGateway.Load(_PaymentGateway, exs) Then
            With _PaymentGateway
                TextBoxNom.Text = .Nom
                TextBoxMerchantCode.Text = .MerchantCode
                TextBoxSignatureKey.Text = .SignatureKey
                TextBoxSermepaUrl.Text = .SermepaUrl
                TextBoxMerchantUrl.Text = .MerchantURL
                TextBoxUrlOk.Text = .UrlOK
                TextBoxUrlKo.Text = .UrlKO
                TextBoxUserAdmin.Text = .UserAdmin
                TextBoxPwd.Text = .Pwd
                If .FchFrom = Nothing Then .FchFrom = DTO.GlobalVariables.Today()
                DateTimePickerFchFrom.Value = .FchFrom
                If .FchTo = Nothing Then
                    DateTimePickerFchTo.Visible = False
                Else
                    CheckBoxObsolet.Checked = True
                    DateTimePickerFchTo.Value = .FchTo
                End If
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxMerchantCode.TextChanged,
          TextBoxSignatureKey.TextChanged,
           TextBoxSermepaUrl.TextChanged,
            TextBoxMerchantUrl.TextChanged,
             TextBoxUrlOk.TextChanged,
              TextBoxUrlKo.TextChanged,
               TextBoxUserAdmin.TextChanged,
                TextBoxPwd.TextChanged,
                 DateTimePickerFchFrom.ValueChanged,
                  DateTimePickerFchTo.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxObsolet_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxObsolet.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchTo.Visible = CheckBoxObsolet.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _PaymentGateway
            .Nom = TextBoxNom.Text
            .MerchantCode = TextBoxMerchantCode.Text
            .SignatureKey = TextBoxSignatureKey.Text
            .SermepaUrl = TextBoxSermepaUrl.Text
            .MerchantURL = TextBoxMerchantUrl.Text
            .UrlOK = TextBoxUrlOk.Text
            .UrlKO = TextBoxUrlKo.Text
            .UserAdmin = TextBoxUserAdmin.Text
            .Pwd = TextBoxPwd.Text
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxObsolet.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.PaymentGateway.Update(_PaymentGateway, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_PaymentGateway))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar la configuració Sermepa")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.PaymentGateway.Delete(_PaymentGateway, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PaymentGateway))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

End Class


