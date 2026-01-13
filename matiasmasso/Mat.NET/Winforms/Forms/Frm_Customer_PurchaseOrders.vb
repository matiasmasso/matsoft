Public Class Frm_Customer_PurchaseOrders

    Private _Mode As Modes
    Private _Codi As DTOPurchaseOrder.Codis
    Private _Contact As DTOContact
    Private _PurchaseOrders As List(Of DTOPurchaseOrder)
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Modes
        All
        SingleContact
        SomeOrders
    End Enum

    Public Sub New(oContact As DTOContact, oCodi As DTOPurchaseOrder.Codis, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        InitializeComponent()
        _Contact = oContact
        _Mode = Modes.SingleContact
        _Codi = oCodi
        _SelectionMode = oSelectionMode
    End Sub

    Public Sub New(oOrders As List(Of DTOPurchaseOrder))
        MyBase.New()
        InitializeComponent()
        _PurchaseOrders = oOrders
        If oOrders.Count > 0 Then _Codi = oOrders.First.Cod
        _Mode = Modes.SomeOrders
    End Sub

    Public Sub New(oCodi As DTOPurchaseOrder.Codis, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        InitializeComponent()
        _Mode = Modes.All
        _SelectionMode = oSelectionMode
        _Codi = oCodi
    End Sub

    Public WriteOnly Property Orders As List(Of DTOPurchaseOrder)
        Set(value As List(Of DTOPurchaseOrder))
            _PurchaseOrders = value
            If _PurchaseOrders.Count > 0 Then _Codi = _PurchaseOrders.First.Cod
            _Mode = Modes.SomeOrders
            refresca()
        End Set
    End Property

    Private Sub Frm_Customer_PurchaseOrders_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = GetTitle()
        refresca()
    End Sub

    Private Function GetTitle() As String
        Dim retval As String = ""
        Select Case _Codi
            Case DTOPurchaseOrder.Codis.Client
                retval = "Comandes de client"
            Case DTOPurchaseOrder.Codis.Proveidor
                retval = "Comandes de proveidor"
        End Select

        Select Case _Mode
            Case Modes.All
                Dim iYeas As List(Of Integer) = BLL.BLLPurchaseOrders.Years(BLL.BLLApp.Emp, _Codi, Nothing)
                Xl_Years1.Load(iYeas)
                Xl_Years1.Visible = True
            Case Modes.SingleContact
                BLL.BLLContact.Load(_Contact)
                retval = "Comandes de " & _Contact.FullNom
        End Select
        Return retval
    End Function

    Private Sub refresca(Optional oDefaultPurchaseOrder As DTOPurchaseOrder = Nothing)
        Select Case _Mode
            Case Modes.All
                _PurchaseOrders = BLL.BLLPurchaseOrders.Headers(Cod:=_Codi, Year:=Xl_Years1.Exercici.Year)
            Case Modes.SingleContact
                _PurchaseOrders = BLL.BLLPurchaseOrders.Headers(Cod:=_Codi, Contact:=_Contact)
            Case Modes.SomeOrders
        End Select

        Xl_PurchaseOrders1.Load(_PurchaseOrders, oDefaultPurchaseOrder, Xl_PurchaseOrders.Modes.SingleCustomer, _SelectionMode)
    End Sub


    Private Sub Xl_PurchaseOrders1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrders1.RequestToRefresh
        refresca(e.Argument)
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_PurchaseOrders1.Filter = e.Argument
    End Sub

    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        refresca()
    End Sub

    Private Sub Xl_PurchaseOrders1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrders1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub
End Class