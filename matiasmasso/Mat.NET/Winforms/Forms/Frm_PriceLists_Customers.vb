Public Class Frm_PriceLists_Customers
    Private _SelectionMode As DTO.Defaults.SelectionModes
    Private _AllowEvents As Boolean

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Vigent
        Historic
    End Enum

    Public Sub New(Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
        If oSelectionMode = DTO.Defaults.SelectionModes.Selection Then
            TabControl1.SelectedIndex = Tabs.Historic
        End If

    End Sub

    Private Sub Do_CopyLink()
        Dim sUrl As String = FEB2.UrlHelper.Factory(True, "tarifas")
        'If DtFch <> Today Then
        'sUrl = FEB2.UrlHelper.Factory(True, "tarifa", DtFch.ToFileTime)
        'End If
        UIHelper.CopyLink(sUrl)
    End Sub

    Private Async Sub Frm_Eventos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ArxiuToolStripMenuItem.DropDownItems.Add("Copiar enllaç", Nothing, AddressOf Do_CopyLink)
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Async Sub refresca(ByVal sender As Object, ByVal e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBarVigent.Visible = True
        Dim items = Await FEB2.PriceListItemsCustomer.Vigent(exs)
        If exs.Count = 0 Then
            Xl_PriceList_Customers_Vigent1.Load(items) 'Today)
            Dim values = Await FEB2.PriceListsCustomer.All(exs)
            ProgressBarVigent.Visible = False
            If exs.Count = 0 Then
                ProgressBarHistoric.Visible = True
                Xl_PriceLists_Customers1.Load(values, _SelectionMode)
                ProgressBarHistoric.Visible = False
            Else
                ProgressBarVigent.Visible = False
                UIHelper.WarnError(exs)
            End If
        Else
            ProgressBarVigent.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_PriceLists_Customers1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Customers1.RequestToAddNew
        Dim oItem As New DTOPricelistCustomer()

        Dim oFrm As New Frm_PriceList_Customer(oItem)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_PriceLists_Customers1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Customers1.RequestToRefresh
        Await refresca()
    End Sub

    Private Sub Xl_PriceLists_Customers1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Customers1.OnItemSelected
        RaiseEvent OnItemSelected(Me, e)
        Me.Close()
    End Sub
End Class