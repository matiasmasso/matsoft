Public Class Frm_CustomerClusters
    Private _Holding As DTOHolding
    Private _Customer As DTOCustomer
    Private _DefaultValue As DTOCustomerCluster
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Selection

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oHolding As DTOHolding, Optional oDefaultValue As DTOCustomerCluster = Nothing)
        MyBase.New()
        Me.InitializeComponent()

        _Holding = oHolding
        _DefaultValue = oDefaultValue
    End Sub


    Public Sub New(oCustomer As DTOCustomer, Optional oDefaultValue As DTOCustomerCluster = Nothing)
        MyBase.New()
        Me.InitializeComponent()


        _Customer = oCustomer
        _DefaultValue = oDefaultValue
    End Sub


    Private Async Sub Frm_CustomerClusters_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Holding Is Nothing Then
            Dim oCcx = FEB2.Customer.CcxOrMe(exs, _Customer)
            _Holding = oCcx.Holding
        End If

        If exs.Count = 0 Then
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_CustomerClusters1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_CustomerClusters1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_CustomerClusters1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CustomerClusters1.RequestToAddNew
        Dim oCustomerCluster = DTOCustomerCluster.Factory(_Holding)
        Dim oFrm As New Frm_CustomerCluster(oCustomerCluster)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_CustomerClusters1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CustomerClusters1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oClusters = Await FEB2.CustomerClusters.All(exs, _Holding)
        If exs.Count = 0 Then
            Xl_CustomerClusters1.Load(oClusters, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class