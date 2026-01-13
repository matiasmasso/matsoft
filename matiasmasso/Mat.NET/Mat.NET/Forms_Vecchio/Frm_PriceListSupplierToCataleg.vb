Public Class Frm_PriceListSupplierToCataleg
    Private _Item As DTOPriceListItem_Supplier
    Private _DirtySearchKey As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oItem As DTOPriceListItem_Supplier, Optional oLastPriceListCustomer As DTOPricelistCustomer = Nothing, Optional oLastCategory As DTOProductCategory = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Item = oItem
        Refresca(oLastPriceListCustomer, oLastCategory)
        _AllowEvents = True
    End Sub

    Private Sub Refresca(Optional oLastPriceListCustomer As DTOPricelistCustomer = Nothing, Optional oLastCategory As DTOProductCategory = Nothing)

        With _Item
            TextBoxParent.Text = BLL.BLLContact.NomComercialOrDefault(.Parent.Proveidor) & " - " & .Parent.Concepte
            TextBoxRefProveidor.Text = .Ref
            TextBoxNomProveidor.Text = .Description

            Xl_PriceList_Customer1.PriceList_Customer = oLastPriceListCustomer
            Xl_LookupProduct1.Product = oLastCategory

            Dim oSku As DTOProductSku = BLL.BLLProductSku.FromProveidor(_Item.Parent.Proveidor, _Item.Ref)
            If oSku Is Nothing Then
                TextBoxNomCurt.Text = .Description
                TextBoxNom.Text = BLL.BLLPriceListItem_Supplier.Ref_And_Description(_Item)
            Else
                TextBoxNomCurt.Text = oSku.NomCurt
                TextBoxNom.Text = oSku.NomLlarg
                Xl_LookupProduct1.Value = oSku.Category
            End If

            NumericUpDownInnerPack.Value = .InnerPack


            Dim oCur As Cur = App.Current.GetOldCur(.Parent.Cur.Id.ToString)
            Xl_AmtCurCostTarifa.Amt = New Amt(, oCur, .Price)
            Xl_PercentDtoOnInvoice.Value = .Parent.Discount_OnInvoice
            Xl_PercentDtoOffInvoice.Value = .Parent.Discount_OffInvoice

            If oCur.Equals(Cur.Eur) Then
                TextBoxExchange.Text = "1"
                TextBoxExchange.Enabled = False
                Xl_AmtCostBrutEur.Amt = New Amt(, oCur, .Price)
                Xl_AmtCurCost.Amt = Xl_AmtCostBrutEur.Amt
            Else
                TextBoxExchange.Text = oCur.Euros
                If IsNumeric(TextBoxExchange.Text) Then
                    Dim DcExchange As Decimal = CDec(TextBoxExchange.Text)
                    Xl_AmtCostBrutEur.Amt = New Amt(.Price * DcExchange)
                    Xl_AmtCurCost.Amt = Xl_AmtCostBrutEur.Amt
                End If
            End If

            Dim oCommercialMargin As DTOCommercialMargin = BLL.BLLProveidor.CommercialMargin(.Parent.Proveidor)
            oCommercialMargin.CostNet = BLL.BLLPriceList_Supplier.CostNet(_Item)

            Xl_PercentCostDto.Value = .Parent.Discount_OnInvoice

            'If .TarifaA = 0 Then
            'Xl_AmtCurTarifaA.Amt = New Amt(oCommercialMargin.GetTarifaA())
            'Xl_AmtCurTarifaB.Amt = New Amt(oCommercialMargin.GetTarifaB())
            'Else
            'Xl_AmtCurTarifaA.Amt = New Amt(.TarifaA)
            'Xl_AmtCurTarifaB.Amt = New Amt(oCommercialMargin.GetTarifaB(.TarifaA))
            'End If

            If .Retail = 0 Then
                Xl_AmtCurRetail.Amt = New Amt(oCommercialMargin.GetRetail())
            Else
                Xl_AmtCurRetail.Amt = New Amt(.Retail)
            End If

            'Xl_PercentTarifaA.Value = Math.Round(100 * (Xl_AmtCurTarifaA.Amt.Eur / BLL.BLLPriceList_Supplier.CostNet(_Item) - 1), 2)
            'Xl_PercentTarifaB.Value = Math.Round(100 * (Xl_AmtCurTarifaB.Amt.Eur / Xl_AmtCurTarifaA.Amt.Eur - 1), 2)
            Dim DcCost As Decimal = BLL.BLLPriceList_Supplier.CostNet(_Item)
            If DcCost <> 0 Then
                Xl_PercentRetail.Value = Math.Round(100 * (Xl_AmtCurRetail.Amt.Eur / DcCost - 1), 2)
            End If

        End With
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oCategory As DTOProductCategory = Xl_LookupProduct1.Product
        If oCategory Is Nothing Then
            MsgBox("Cal triar una categoría de producte", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim oPriceList_Customer AS DTOPricelistCustomer = Xl_PriceList_Customer1.PriceList_Customer
        If oPriceList_Customer Is Nothing Then
            MsgBox("Cal assignar el preu a una tarifa de venda", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim oSku As DTOProductSku = BLL.BLLProductSku.FromProveidor(_Item.Parent.Proveidor, _Item.Ref)
        If oSku Is Nothing Then
            Dim sNomCurt As String = TextBoxNomCurt.Text
            Dim oExistingSku As DTOProductSku = BLL.BLLProductSku.FromNomCurt(oCategory, sNomCurt)
            If oExistingSku IsNot Nothing Then
                MsgBox("Ja existeix un article '" & sNomCurt & "' dins la categoría '" & oCategory.Nom & "'", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            oSku = New DTOProductSku
            With oSku
                .Category = oCategory
                .RefProveidor = TextBoxRefProveidor.Text
                .NomProveidor = TextBoxNomProveidor.Text
                .Ean13 = BLL.bllean13.fromDigits(_Item.EAN)
                .Hereda = oCategory.Dsc_PropagateToChildren
                .NomCurt = sNomCurt
                .NomLlarg = TextBoxNom.Text
                .Keys = GetArrayListFromSplitCharSeparatedString(TextBoxSearchKey.Text)
                .InnerPack = NumericUpDownInnerPack.Value
            End With


            Dim exs As New List(Of Exception)

            If BLL.BLLProductSku.Update(oSku, exs) Then
                Dim oDTOPricelistItemCustomer As New DTOPricelistItemCustomer(oPriceList_Customer)
                With oDTOPricelistItemCustomer
                    .Sku = oSku
                    .Retail = Xl_AmtCurRetail.Amt2
                End With

                If BLL.BLLPricelistItemCustomer.Update(oDTOPricelistItemCustomer, exs) Then
                    Me.Close()
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(oDTOPricelistItemCustomer))
                Else
                    UIHelper.WarnError(exs, "error al desar el preu")
                End If
            Else
                UIHelper.WarnError(exs, "error al desar l'article")
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_Stp1_AfterUpdate(sender As Object, e As EventArgs)
        SetKeys()
    End Sub

    Private Sub SetKeys()
        Dim oCategoria As DTOProductCategory = Xl_LookupProduct1.Value
        Dim sb As New System.Text.StringBuilder
        sb.Append(TextBoxRefProveidor.Text)
        If oCategoria IsNot Nothing Then
            sb.Append(",")
            sb.Append(oCategoria.Nom & " " & TextBoxNomCurt.Text)
        End If
        Dim sSearchKeys As String = sb.ToString
        TextBoxSearchKey.Text = sSearchKeys
    End Sub

    Private Sub TextBoxNomCurt_TextChanged(sender As Object, e As EventArgs) Handles TextBoxNomCurt.TextChanged
        SetKeys()
    End Sub

    Private Sub TextBoxSearchKey_Validated(sender As Object, e As EventArgs) Handles TextBoxSearchKey.Validated
        _DirtySearchKey = True
    End Sub

    Private Sub TextBoxExchange_TextChanged(sender As Object, e As EventArgs) Handles TextBoxExchange.TextChanged
        If IsNumeric(TextBoxExchange.Text) Then
            Dim DcExchange As Decimal = CDec(TextBoxExchange.Text)
            Xl_AmtCurCost.Amt = New Amt(_Item.Price * DcExchange)
        End If

    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        Xl_PriceList_Customer1.AfterUpdate
        If _allowevents Then
            ButtonOk.Enabled = True
        End If
    End Sub
End Class