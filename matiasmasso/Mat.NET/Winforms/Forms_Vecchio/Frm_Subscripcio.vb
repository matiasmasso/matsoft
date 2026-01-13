Public Class Frm_Subscripcio
    Dim _Subscripcio As DTOSubscription

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(oSubscripcio As DTOSubscription)
        MyBase.New()
        Me.InitializeComponent()
        _Subscripcio = oSubscripcio
    End Sub

    Private Async Sub Frm_Subscripcio_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await Xl_Subscripcio1.Load(_Subscripcio)
    End Sub

    Private Sub Xl_Subscripcio1_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_Subscripcio1.AfterUpdate
        RaiseEvent AfterUpdate(sender, e)
        Me.Close()
    End Sub


End Class