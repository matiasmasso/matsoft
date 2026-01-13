Public Class Frm_PriceLists_Customers
    Private _SelectionMode As BLL.Defaults.SelectionModes
    Private _AllowEvents As Boolean

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Vigent
        Historic
    End Enum

    Public Sub New(Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
        If oSelectionMode = BLL.Defaults.SelectionModes.Selection Then
            TabControl1.SelectedIndex = Tabs.Historic
        End If

    End Sub

    Private Sub Frm_Eventos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        Dim values As List(Of DTOPricelistCustomer) = BLL.BLLPricelistsCustomer.All()
        Xl_PriceLists_Customers1.Load(values, _SelectionMode)
    End Sub

    Private Sub Xl_PriceLists_Customers1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Customers1.RequestToAddNew
        Dim oItem As New DTOPricelistCustomer()

        Dim oFrm As New Frm_PriceList_Customer(oItem)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_PriceLists_Customers1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Customers1.RequestToRefresh
        refresca()
    End Sub

    Private Sub Xl_PriceLists_Customers1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Customers1.OnItemSelected
        RaiseEvent OnItemSelected(Me, e)
        Me.Close()
    End Sub
End Class