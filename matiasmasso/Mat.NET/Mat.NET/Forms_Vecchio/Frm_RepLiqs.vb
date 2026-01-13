
Public Class Frm_RepLiqs

    Public Sub New(oRep As DTORep)
        MyBase.New()
        Me.InitializeComponent()
        Me.Text = "Liquidacions de comisions a " & oRep.NickName
        Xl_RepRepLiqs1.Rep = oRep
    End Sub
End Class