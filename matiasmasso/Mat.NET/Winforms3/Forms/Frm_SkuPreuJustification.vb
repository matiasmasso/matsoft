Public Class Frm_SkuPreuJustification
    Private _item As DTOPurchaseOrderItem

    Public Sub New(item As DTOPurchaseOrderItem)
        MyBase.New
        InitializeComponent()
        _item = item
    End Sub

    Private Async Sub Frm_SkuPreuJustification_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oOrder As DTOPurchaseOrder = _item.PurchaseOrder
        Dim oCustomer As DTOCustomer = oOrder.Contact
        Dim oSku As DTOProductSku = _item.Sku
        Dim oBrand As DTOProductBrand = oSku.Category.Brand

        TextBoxCustomer.Text = _item.PurchaseOrder.Contact.FullNom
        Dim oContactMenu = Await FEB.ContactMenu.Find(exs, oCustomer)
        Dim oMenu_Contact As New Menu_Contact(oCustomer, oContactMenu)
        TextBoxCustomer.ContextMenuStrip = New ContextMenuStrip
        TextBoxCustomer.ContextMenuStrip.Items.AddRange(oMenu_Contact.Range)

        TextBoxPdc.Text = DTOPurchaseOrder.Title(oOrder)

        TextBoxSku.Text = oSku.NomLlarg.Tradueix(Current.Session.Lang)
        Dim oMenu_Sku As New Menu_ProductSku(oSku)
        TextBoxSku.ContextMenuStrip = New ContextMenuStrip
        TextBoxSku.ContextMenuStrip.Items.AddRange(oMenu_Sku.Range)

        Dim oPricelistItem = Await FEB.PriceListItemCustomer.Search(exs, oSku, oOrder.Fch)
        If exs.Count = 0 Then
            If oPricelistItem IsNot Nothing Then
                Dim oPricelist As DTOPricelistCustomer = oPricelistItem.Parent
                Dim oRetail As DTOAmt = oPricelistItem.Retail

                TextBoxTarifa.Text = DTOPricelistCustomer.FullNom(oPricelist)
                Dim oMenu_Tarifa As New Menu_PriceList_Customer(oPricelist)
                TextBoxTarifa.ContextMenuStrip = New ContextMenuStrip
                TextBoxTarifa.ContextMenuStrip.Items.AddRange(oMenu_Tarifa.Range)

                TextBoxRetail.Text = DTOAmt.CurFormatted(oRetail)

                Dim oDtos = Await FEB.CustomerTarifaDtos.All(exs, oCustomer)
                Dim oDto As DTOCustomerTarifaDto = oDtos.Find(Function(x) oBrand.Equals(x.Product) Or x.Product Is Nothing)
                If oDto IsNot Nothing Then
                    TextBoxDtoText.Text = DTOCustomerTarifaDto.FullNom(oDto)
                    'Dim oMenu_CustomerTarifaDto As New Menu_CustomerTarifaDto(oPricelist)
                    'TextBoxTarifa.ContextMenuStrip = New ContextMenuStrip
                    'TextBoxTarifa.ContextMenuStrip.Items.AddRange(oMenu_Tarifa.Range)

                    Dim DcDto As Decimal = DTOCustomerTarifaDto.ProductDto(oDtos, oSku.Category.Brand)
                    TextBoxDtoPct.Text = DcDto & "%"

                    Dim oCost As DTOAmt = oRetail.Clone
                    oCost.DeductPercent(DcDto)
                    TextBoxCostDistribuidor.Text = DTOAmt.CurFormatted(oCost)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TextBoxDto_DoubleClick(sender As Object, e As EventArgs) Handles _
        TextBoxDtoPct.DoubleClick,
         TextBoxDtoText.DoubleClick

        Dim oFrm As New Frm_CustomerTarifa(_item.PurchaseOrder.Contact, Frm_CustomerTarifa.Tabs.Dto)
        oFrm.Show()
    End Sub


End Class