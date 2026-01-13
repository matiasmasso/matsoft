Public Class Frm_OnlineVendors

    Private _DefaultValue As DTOOnlineVendor
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOOnlineVendor = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_OnlineVendors_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_OnlineVendors1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_OnlineVendors1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_OnlineVendors1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_OnlineVendors1.RequestToAddNew
        Dim oOnlineVendor As New DTOOnlineVendor
        Dim oFrm As New Frm_OnlineVendor(oOnlineVendor)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_OnlineVendors1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_OnlineVendors1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        'Dim oOnlineVendors As List(Of DTOOnlineVendor) = BLL.BLLOnlineVendors.All
        'Xl_OnlineVendors1.Load(oOnlineVendors, _DefaultValue, _SelectionMode)
    End Sub
End Class