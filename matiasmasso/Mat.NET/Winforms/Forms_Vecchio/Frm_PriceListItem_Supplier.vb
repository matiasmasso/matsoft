Public Class Frm_PriceListItem_Supplier
    Private _Item As DTOPriceListItem_Supplier
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(oItem As DTOPriceListItem_Supplier)
        MyBase.New()
        Me.InitializeComponent()
        _Item = oItem
        If Not _Item.IsNew Then ButtonDel.Enabled = True
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        With _Item
            TextBoxParent.Text = .Parent.Fch.ToShortDateString & " - " & .Parent.Concepte
            TextBoxRef.Text = .Ref
            TextBoxEAN.Text = .EAN
            TextBoxNom.Text = .Description
            Xl_AmtCost.Amt = DTOAmt.Factory(.Price)
            Xl_AmtRRPP.Amt = DTOAmt.Factory(.Retail)
        End With
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        If _Item.Parent.IsNew Then
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB2.PriceListSupplier.Update(exs, _Item.Parent) Then
                UIHelper.ToggleProggressBar(Panel1, False)
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If


        With _Item
            .Ref = TextBoxRef.Text
            .EAN = TextBoxEAN.Text
            .Description = TextBoxNom.Text
            .Price = Xl_AmtCost.Amt.Val
            .Retail = Xl_AmtRRPP.Amt.Val
        End With

        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.PriceListItemSupplier.Update(exs, _Item) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Item))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TextBoxRef_TextChanged(sender As Object, e As EventArgs) Handles _
        TextBoxRef.TextChanged,
         TextBoxEAN.TextChanged,
          TextBoxNom.TextChanged,
           Xl_AmtCost.AfterUpdate,
            Xl_AmtRRPP.AfterUpdate, TextBoxRef.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonBrowseProduct_Click(sender As Object, e As EventArgs) Handles ButtonBrowseProduct.Click
        Dim exs As New List(Of Exception)
        Dim oProduct = Await DefaultProductToBrowse(exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectSku, oProduct)
            AddHandler oFrm.onItemSelected, AddressOf onProductSelected
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function DefaultProductToBrowse(exs As List(Of Exception)) As Task(Of DTOProduct)
        Dim retval As DTOProduct = Nothing
        Dim oPriceList As DTOPriceListSupplier = _Item.Parent
        If oPriceList.Items.Count = 0 Then
            retval = Await DefaultBrandToBrowse(exs)
        Else
            Dim item As DTOPriceListItem_Supplier = oPriceList.Items.Last
            If item.Sku Is Nothing Then
                retval = Await DefaultBrandToBrowse(exs)
            Else
                FEB2.ProductSku.Load(item.Sku, exs) 'per carregar categoria i marca
                retval = item.Sku
            End If
        End If
        Return retval
    End Function

    Private Async Function DefaultBrandToBrowse(exs As List(Of Exception)) As Task(Of DTOProduct)
        Dim retval As DTOProduct = Nothing
        Dim oPriceList As DTOPriceListSupplier = _Item.Parent
        Dim oProveidor = oPriceList.Proveidor
        Dim oBrands As New List(Of DTOProductBrand)
        If oProveidor IsNot Nothing Then
            oBrands = Await FEB2.ProductBrands.FromProveidor(exs, oProveidor)
        End If
        If oBrands.Count > 0 Then
            retval = oBrands.First
        End If
        Return retval
    End Function


    Private Sub onProductSelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oSku As DTOProductSku = e.Argument
        If FEB2.ProductSku.Load(oSku, exs) Then
            With _Item
                .Sku = oSku
                .SkuGuid = oSku.Guid
                If oSku.Ean13 IsNot Nothing Then
                    .EAN = oSku.Ean13.Value
                End If
                .Description = oSku.NomPrvOrMyd
                .Ref = oSku.RefProveidor
                If oSku.Cost IsNot Nothing Then
                    .Price = oSku.Cost.Eur
                End If
                If oSku.InnerPack <> 0 Then
                    .InnerPack = oSku.InnerPack
                End If
            End With
            refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest preu de la tarifa?", MsgBoxStyle.YesNo)
        If rc = MsgBoxResult.Yes Then
            Dim exs As New List(Of Exception)
            If Await FEB2.PriceListItemSupplier.Delete(exs, _Item) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar el preu")
            End If
        End If
    End Sub
End Class