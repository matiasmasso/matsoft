Public Class Frm_Subscripcio
    Dim _Subscripcio As DTOSubscription

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(oSubscripcio As DTOSubscription)
        MyBase.New()
        Me.InitializeComponent()
        _Subscripcio = oSubscripcio
        Xl_Subscripcio1.Subscripcio = _Subscripcio
    End Sub

    Private Sub Xl_Subscripcio1_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_Subscripcio1.AfterUpdate
        RaiseEvent AfterUpdate(sender, e)
        Me.Close()
    End Sub
End Class