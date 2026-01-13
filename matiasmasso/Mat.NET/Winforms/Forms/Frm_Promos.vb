Public Class Frm_Promos
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
    End Sub

    Private Sub Frm_Promos_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Promos1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Promos1.RequestToAddNew
        Dim oPromo As New DTOPromo
        Dim oFrm As New Frm_Promo(oPromo)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Promos1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Promos1.RequestToRefresh
        refresca()
    End Sub

    Private Sub Xl_Promos1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Promos1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub refresca()
        Dim BlDisplayObsolets As Boolean = Xl_Promos1.DisplayObsolets
        Dim oPromos As List(Of DTOPromo) = BLL.BLLPromos.All(BLL.BLLSession.Current.User, BlDisplayObsolets)
        Xl_Promos1.Load(oPromos, Nothing, _SelectionMode)
    End Sub
End Class