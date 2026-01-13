Public Class Frm_User_Old
    Private _User As User
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(oUser As User)
        MyBase.New()
        Me.InitializeComponent()
        _User = oUser
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()

    End Sub


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class