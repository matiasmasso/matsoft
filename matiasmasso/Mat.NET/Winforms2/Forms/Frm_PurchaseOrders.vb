Public Class Frm_PurchaseOrders
    Private _Orders As List(Of DTOPurchaseOrder)
    Private _Contact As DTOContact
    Private _ContactMode As ContactModes
    Private _Cod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.notSet
    Private _SelectionMode As DTO.Defaults.SelectionModes

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum ContactModes
        None
        Customer
        Ccx
        Proveidor
        Rep
    End Enum

    Public Sub New(oCustomer As DTOCustomer, Optional includeGroupSalePoints As Boolean = False)
        MyBase.New()
        Me.InitializeComponent()

        _Contact = oCustomer
        _ContactMode = IIf(includeGroupSalePoints, ContactModes.Ccx, ContactModes.Customer)
        _Cod = DTOPurchaseOrder.Codis.client
        Me.Text = IIf(includeGroupSalePoints, String.Format("Comandes del Grup de {0}", _Contact.FullNom), String.Format("Comandes de " & _Contact.FullNom))
    End Sub

    Public Sub New(oProveidor As DTOProveidor, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()

        _Contact = oProveidor
        _ContactMode = ContactModes.Proveidor
        _Cod = DTOPurchaseOrder.Codis.proveidor
        _SelectionMode = oSelectionMode
        Me.Text = String.Format("Comandes de {0}", _Contact.FullNom)
    End Sub

    Public Sub New(oRep As DTORep)
        MyBase.New()
        Me.InitializeComponent()

        _Contact = oRep
        _ContactMode = ContactModes.Rep
        _Cod = DTOPurchaseOrder.Codis.client
        Me.Text = String.Format("Comandes de {0}", _Contact.FullNom)
    End Sub

    Public Sub New(Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()

        _ContactMode = ContactModes.None
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_PurchaseOrders_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)

        If _ContactMode = ContactModes.None Then
            Dim iYeas = Await FEB.PurchaseOrders.Years(exs, Current.Session.Emp, DTOPurchaseOrder.Codis.Client, Nothing)
            If exs.Count = 0 Then
                Xl_Years1.Load(iYeas)
                Await Refresca()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            Xl_Years1.Visible = False
            Await Refresca()
        End If

    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Select Case _ContactMode
            Case ContactModes.None
                _Orders = Await FEB.PurchaseOrders.Headers(exs, Current.Session.Emp, Cod:=DTOPurchaseOrder.Codis.Client, Contact:=_Contact, Year:=Xl_Years1.Value)
            Case ContactModes.Customer
                _Orders = Await FEB.PurchaseOrders.Headers(exs, Current.Session.Emp, Cod:=DTOPurchaseOrder.Codis.Client, Contact:=_Contact)
            Case ContactModes.Ccx
                _Orders = Await FEB.PurchaseOrders.Headers(exs, Current.Session.Emp, Cod:=DTOPurchaseOrder.Codis.Client, Ccx:=_Contact)
            Case ContactModes.Proveidor
                _Orders = Await FEB.PurchaseOrders.Headers(exs, Current.Session.Emp, Cod:=DTOPurchaseOrder.Codis.Proveidor, Contact:=_Contact)
            Case ContactModes.Rep
                _Orders = Await FEB.PurchaseOrders.Headers(exs, Current.Session.Emp, Cod:=DTOPurchaseOrder.Codis.Client, Rep:=_Contact)
        End Select

        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_PurchaseOrders1.Load(_Orders, Nothing, IIf(_ContactMode = ContactModes.None, Xl_PurchaseOrders.Modes.MultipleCustomers, Xl_PurchaseOrders.Modes.SingleCustomer), _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Await Refresca()
    End Sub

    Private Async Sub Xl_PurchaseOrders1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrders1.RequestToRefresh
        Await Refresca()
    End Sub

    Private Sub Xl_PurchaseOrders1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrders1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_PurchaseOrders1.Filter = e.Argument
    End Sub

    Private Sub Xl_PurchaseOrders1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrders1.RequestToToggleProgressBar
        ProgressBar1.Visible = e.Argument
    End Sub
End Class