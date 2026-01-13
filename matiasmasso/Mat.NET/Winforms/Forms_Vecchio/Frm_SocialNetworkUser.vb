

Public Class Frm_SocialNetworkUser
    Private mSocialNetwork As SocialNetworkUser.Networks
    Private mNSUser As SocialNetworkUser
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oNSUser As SocialNetworkUser)
        MyBase.new()
        Me.InitializeComponent()
        mNSUser = oNSUser
        mSocialNetwork = mNSUser.Network
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mNSUser
            TextBoxNet.Text = .Network.ToString
            TextBoxEmail.Text = .Email
            ButtonDel.Enabled = .Exists
            TextBoxId.ReadOnly = .Exists
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxId.TextChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mNSUser
            .Image = PictureBox1.Image
            .Update()
         End With
        RaiseEvent AfterUpdate(mNSUser, System.EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        'If mNSUser.allowDelete Then
        ' mNSUser.delete()
        ' Me.Close()
        ' End If
    End Sub

    Private Sub ButtonDownload_Click(sender As Object, e As System.EventArgs) Handles ButtonDownload.Click
        If Not mNSUser.Exists Then
            mNSUser.UserId = TextBoxId.Text
        End If

        Dim sUrl As String = mNSUser.ProfileImageUrl

        If sUrl > "" Then
            PictureBox1.WaitOnLoad = True
            PictureBox1.LoadAsync(sUrl)
        End If
    End Sub
End Class