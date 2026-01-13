Public Class Frm_RepComsFollowUp
    Private _Rep As DTORep

    Public Sub New(oRep As DTORep)
        MyBase.New
        InitializeComponent()
        _Rep = oRep
    End Sub

    Private Sub Frm_RepComsFollowUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        BLLRep.Load(_Rep)
        Me.Text = "Seguiment comisions " & _Rep.NickName
        Dim oOrders As List(Of DTOPurchaseOrder) = BLLRep.Archive(_Rep)
        Xl_RepComsFollowUp1.Load(oOrders)
    End Sub
End Class