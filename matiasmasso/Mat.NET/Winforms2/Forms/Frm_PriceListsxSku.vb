Public Class Frm_PriceListsxSku

    Private _Sku As DTOProductSku

    Private Enum Tabs
        Venda
        Cost
    End Enum

    Public Sub New(oSku As DTOProductSku)
        MyBase.New
        InitializeComponent()

        _Sku = oSku
    End Sub

    Private Async Sub Frm_PriceListsxSku_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.ProductSku.Load(_Sku, exs) Then
            LabelSku.Text = _Sku.NomLlarg.Tradueix(Current.Session.Lang)
            Await RefrescaVenda()

            Dim oUser As DTOUser = Current.Session.User
            Select Case oUser.Rol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts
                Case Else
                    TabControl1.TabPages.RemoveAt(Tabs.Cost)
            End Select
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Cost
                Await RefrescaCosts()
        End Select
    End Sub

    Private Async Function RefrescaCosts() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB.PriceListItemsSupplier.All(exs, _Sku)
        If exs.Count = 0 Then
            Xl_PriceListsSuplierxSku1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function RefrescaVenda() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB.PriceListItemsCustomer.All(exs, _Sku)
        If exs.Count = 0 Then
            Xl_PricelistItems_CustomerXSku1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class