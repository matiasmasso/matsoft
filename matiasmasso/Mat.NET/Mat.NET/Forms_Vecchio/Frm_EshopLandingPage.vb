Public Class Frm_EshopLandingPage
    Private _EShopLandingpage As EShopLandingpage
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As EShopLandingpage)
        MyBase.New()
        Me.InitializeComponent()
        _EShopLandingpage = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _EShopLandingpage
            TextBox1.Text = .ToString
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBox1.TextChanged
        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        If EShopLandingpageloader.Update(_EShopLandingpage, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_EShopLandingpage))
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If EShopLandingpageloader.Delete(_EShopLandingpage, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_EShopLandingpage))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


