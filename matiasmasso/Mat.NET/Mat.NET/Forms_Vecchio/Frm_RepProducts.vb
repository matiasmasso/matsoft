

Public Class Frm_RepProducts
    Private _Rep As DTORep
    Private _AllowEvents As Boolean

    Public Sub New(oRep As DTORep)
        MyBase.New()
        Me.InitializeComponent()
        _Rep = oRep
        BLL.BLLRep.Load(_Rep)
        Me.Text = _Rep.NickName & " - Cartera de Productes"
    End Sub

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim items As List(Of DTORepProduct) = BLL.BLLRepProducts.All(BLL.BLLApp.Emp, _Rep, True)
        Xl_RepProducts1.Load(items, Xl_RepProducts.Modes.ByRep)
        _AllowEvents = True
    End Sub

End Class