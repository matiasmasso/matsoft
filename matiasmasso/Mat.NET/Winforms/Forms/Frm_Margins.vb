Public Class Frm_Margins

    Private _Customer As DTOCustomer
    Private _Proveidor As DTOProveidor
    Private _Mode As Modes
    Private _Values As List(Of DTODeliveryItem)
    Private _AllowEvents As Boolean

    Private Enum Modes
        Customer
        Proveidor
    End Enum

    Public Sub New(Optional oCustomer As DTOCustomer = Nothing)
        MyBase.New
        InitializeComponent()
        If oCustomer IsNot Nothing Then
            Dim exs As New List(Of Exception)
            FEB2.Customer.Load(oCustomer, exs)
            _Customer = FEB2.Customer.CcxOrMe(exs, oCustomer)
        End If
        _Mode = Modes.Customer
    End Sub

    Public Sub New(oProveidor As DTOProveidor)
        MyBase.New
        InitializeComponent()
        _Proveidor = oProveidor
        _Mode = Modes.Proveidor
    End Sub

    Private Async Sub Frm_CustomerPmcs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Select Case _Mode
            Case Modes.Customer
                _Values = Await FEB2.DeliveryItems.All(exs, _Customer, GlobalVariables.Emp.Mgz)
            Case Modes.Proveidor
                _Values = Await FEB2.DeliveryItems.All(exs, _Proveidor, GlobalVariables.Emp.Mgz)
        End Select
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            If _Values.Count = 0 Then
                MsgBox("sense dades registrades", MsgBoxStyle.Exclamation)
            Else
                Dim DtFchMin As Date = _Values.Min(Function(x) x.Delivery.Fch)
                DateTimePickerFchFrom.MinDate = DtFchMin
                DateTimePickerFchTo.MinDate = DtFchMin
                DateTimePickerFchFrom.MaxDate = Today
                DateTimePickerFchTo.MaxDate = Today.AddDays(1)

                Dim DtYearAgo As Date = Today.AddYears(-1)
                If DtYearAgo > DtFchMin Then
                    DateTimePickerFchFrom.Value = DtYearAgo
                Else
                    DateTimePickerFchFrom.Value = DtFchMin
                End If
                DateTimePickerFchTo.Value = _Values.Max(Function(x) x.Delivery.Fch)

                refresca()
                _AllowEvents = True
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refresca()
        Dim FchMin As Date = DateTimePickerFchFrom.Value
        Dim FchMax As Date = DateTimePickerFchTo.Value
        Dim items As List(Of DTODeliveryItem) = _Values.Where(Function(x) x.Delivery.Fch >= FchMin And x.Delivery.Fch <= FchMax).ToList
        Xl_Marges1.Load(items, Xl_Marges.Modes.Brands)
        Xl_Marges2.Load(items, Xl_Marges.Modes.Categories)
        Xl_Marges3.Load(items, Xl_Marges.Modes.Customers)
        Xl_CustomerPmcs1.Load(items)
    End Sub

    Private Sub Xl_Marges1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Marges1.ValueChanged
        If _AllowEvents Then
            Dim oBrandGuid As Guid = e.Argument
            Dim oBrand As New DTOProductBrand(oBrandGuid)
            Dim filteredValues As List(Of DTODeliveryItem) = _Values.Where(Function(x) x.Sku.Category.Brand.Equals(oBrand)).ToList
            Xl_Marges2.Load(filteredValues, Xl_Marges.Modes.Categories)
            Xl_Marges3.Load(filteredValues, Xl_Marges.Modes.Customers)
            Xl_CustomerPmcs1.Load(filteredValues)
        End If
    End Sub

    Private Sub Xl_Marges2_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Marges2.ValueChanged
        If _AllowEvents Then
            Dim oCategoryGuid As Guid = e.Argument
            Dim oCategory As New DTOProductBrand(oCategoryGuid)
            Dim filteredValues As List(Of DTODeliveryItem) = _Values.Where(Function(x) x.Sku.Category.Equals(oCategory)).ToList
            Xl_Marges3.Load(filteredValues, Xl_Marges.Modes.Customers)
            Xl_CustomerPmcs1.Load(filteredValues)
        End If
    End Sub

    Private Sub DateTimePickerFchFrom_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerFchFrom.ValueChanged
        refresca()
    End Sub

    Private Sub DateTimePickerFchTo_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerFchTo.ValueChanged
        refresca()
    End Sub
End Class