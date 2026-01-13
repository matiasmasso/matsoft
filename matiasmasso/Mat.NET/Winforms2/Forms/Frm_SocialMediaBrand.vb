Public Class Frm_SocialMediaWidget
    Private _SocialMediaWidget As DTOSocialMediaWidget
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOSocialMediaWidget)
        MyBase.New()
        Me.InitializeComponent()
        _SocialMediaWidget = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.SocialMediaWidget.Load(exs, _SocialMediaWidget) Then
            With _SocialMediaWidget
                ComboBoxPlatform.SelectedIndex = .Platform
                Xl_LookupProductBrand1.Load(.Brand)
                TextBoxTitular.Text = .Titular
                TextBoxWidgetId.Text = .WidgetId
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            ComboBoxPlatform.SelectedIndexChanged,
             Xl_LookupProductBrand1.AfterUpdate,
              TextBoxTitular.TextChanged,
               TextBoxWidgetId.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _SocialMediaWidget
            .Platform = ComboBoxPlatform.SelectedIndex
            .Brand = Xl_LookupProductBrand1.Brand
            .Titular = TextBoxTitular.Text
            .WidgetId = TextBoxWidgetId.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.SocialMediaWidget.Update(exs, _SocialMediaWidget) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_SocialMediaWidget))
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
            If Await FEB.SocialMediaWidget.Delete(exs, _SocialMediaWidget) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_SocialMediaWidget))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class

