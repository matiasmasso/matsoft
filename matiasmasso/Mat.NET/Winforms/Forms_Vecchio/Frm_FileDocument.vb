Public Class Frm_FileDocument
    Private _Value As FileDocument

    Public Event AfterUpdate(sender As Object, e As AfterUpdateEventArgs)

    Public Sub New(oValue As FileDocument)
        MyBase.New()
        Me.InitializeComponent()
        _Value = oValue
        Xl_MediaObject1.Load(_Value.MediaObject)
        Dim sHash As String = BLL.CryptoHelper.HashMD5(_Value.MediaObject.Stream)
        TextBox1.Text = sHash & " (" & sHash.Length & " bytes)"
        If oValue.Srcs.Count = 0 Then
            ButtonDel.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class