Public Class Frm_CustomerPlatform
    Private _CustomerPlatform As DTOCustomerPlatform
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCustomerPlatform)
        MyBase.New()
        Me.InitializeComponent()
        _CustomerPlatform = value
    End Sub

    Private Sub Frm_CustomerPlatform_Load(sender As Object, e As EventArgs) Handles Me.Load
        CustomerPlatformloader.Load(_CustomerPlatform)
        With _CustomerPlatform
            TextBoxCustomer.Text = .Customer.FullNom
            LabelPlatform.Text = .Nom & vbCrLf & BLL.BLLAddress.FullText(.Address)
            Xl_PlatformDestinations1.Load(.Destinations)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_PlatformDestinations1.AfterUpdate
        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _CustomerPlatform
            .Destinations = Xl_PlatformDestinations1.Items
        End With
        Dim exs as New List(Of exception)
        If CustomerPlatformloader.Update(_CustomerPlatform, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CustomerPlatform))
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
            If CustomerPlatformloader.Delete(_CustomerPlatform, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_CustomerPlatform))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub
End Class

