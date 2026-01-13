

Public Class Frm_TntTracking
    Private mAlb As Alb

    Public Sub New(oAlb As Alb)
        MyBase.New()
        Me.InitializeComponent()
        mAlb = oAlb
        refresca()
    End Sub

    Private Sub refresca()
        Dim oTracking As New TntWebTracking(mAlb.Formatted)

    End Sub
End Class