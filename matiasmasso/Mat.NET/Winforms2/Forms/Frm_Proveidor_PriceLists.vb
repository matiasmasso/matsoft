

Public Class Frm_Proveidor_PriceLists

    Private _Proveidor As DTOProveidor
    Private _DirtyVigent As Boolean = True
    Private _tarifaVigent As List(Of DTOPriceListItem_Supplier)
    Private _DirtyHistoric As Boolean = True
    Private _AllowEvents As Boolean

    Public Enum Tabs
        Historic
        Vigent
    End Enum

    Public Sub New(oProveidor As DTOProveidor)
        MyBase.New()
        Me.InitializeComponent()
        _Proveidor = oProveidor
    End Sub

    Private Async Sub Frm_Proveidor_PriceLists_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Proveidor.Load(_Proveidor, exs) Then
            Me.Text = _Proveidor.NomComercialOrDefault() & ": Tarifa de preus"
            Await refrescaHistoric()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub refresca()
        _DirtyVigent = True
        _DirtyHistoric = True
        Select Case TabControl1.SelectedIndex
            Case Tabs.Vigent
                Await RefrescaVigent()
            Case Tabs.Historic
                Await refrescaHistoric()
        End Select
    End Sub


    Private Async Function RefrescaVigent() As Task
        Dim exs As New List(Of Exception)
        If _DirtyVigent Then
            ProgressBar1.Visible = True
            _tarifaVigent = Await FEB.PriceListItemsSupplier.Vigent(exs, _Proveidor)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                Xl_TarifaProveidor1.Load(_tarifaVigent)
                _DirtyVigent = False
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Async Function refrescaHistoric() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oPriceLists = Await FEB.PriceListsSupplier.FromProveidor(exs, _Proveidor)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_PriceLists_Suppliers1.Load(oPriceLists)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_PriceLists_Suppliers1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Suppliers1.RequestToAddNew
        Dim oPriceList = DTOPriceListSupplier.Factory(_Proveidor)
        Dim oFrm As New Frm_PriceList_Supplier(oPriceList)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_PriceLists_Suppliers1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Suppliers1.RequestToRefresh
        Await refrescaHistoric()
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Vigent
                Await RefrescaVigent()
            Case Tabs.Historic
                Await refrescaHistoric()
        End Select
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_TarifaProveidor1.Filter = e.Argument
    End Sub

    Private Sub Xl_TextboxSearchHistorial_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearchHistorial.AfterUpdate
        Xl_PriceLists_Suppliers1.Filter = e.Argument
    End Sub

    Private Async Sub ExcelTarifaVigentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelTarifaVigentToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        If _DirtyVigent Or _tarifaVigent Is Nothing Then
            ProgressBar1.Visible = True
            _tarifaVigent = Await FEB.PriceListItemsSupplier.Vigent(exs, _Proveidor)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                _DirtyVigent = False
            Else
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If

        Dim oSheet = DTOPriceListSupplier.ExcelTarifaVigent(_tarifaVigent)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class