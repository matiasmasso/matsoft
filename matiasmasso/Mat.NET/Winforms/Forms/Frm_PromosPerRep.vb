Public Class Frm_PromosPerRep
    Private _Promo As DTOPromo
    Private _Orders As List(Of DTOPurchaseOrder)
    Private _AllowEvents As Boolean

    Public Sub New(oPromo As DTOPromo)
        MyBase.New
        InitializeComponent()
        _Promo = oPromo
    End Sub

    Private Sub Frm_PromosPerRep_Load(sender As Object, e As EventArgs) Handles Me.Load
        _Orders = BLLPromo.PurchaseOrders(_Promo, BLLSession.Current.User)
        Xl_PromosPerRep1.Load(_Orders)
        Xl_PurchaseOrders1.Load(_Orders)
        _AllowEvents = True
    End Sub

    Private Sub Xl_PromosPerRep1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_PromosPerRep1.ValueChanged
        If _AllowEvents Then
            Dim oRep As DTORep = e.Argument
            Dim oOrders As List(Of DTOPurchaseOrder) = _Orders.Where(Function(x) x.Items.Any(Function(y) MatchesRep(y.RepCom, oRep))).ToList
            Xl_PurchaseOrders1.Load(oOrders)
        End If
    End Sub

    Private Function MatchesRep(oRepCom As DTORepCom, oRep As DTORep) As Boolean
        Dim retval As Boolean
        If oRepCom IsNot Nothing Then
            If oRepCom.Rep.Equals(oRep) Then
                retval = True
            End If
        End If
        Return retval
    End Function
End Class