Public Class Frm_PriceListSupplierToCataleg
    Private _Item As DTOPriceListItem_Supplier
    Private _DirtySearchKey As Boolean
    Private _LastPriceListCustomer As DTOPricelistCustomer
    Private _LastCategory As DTOProductCategory
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oItem As DTOPriceListItem_Supplier, Optional oLastPriceListCustomer As DTOPricelistCustomer = Nothing, Optional oLastCategory As DTOProductCategory = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Item = oItem
        _LastPriceListCustomer = oLastPriceListCustomer
        _LastCategory = oLastCategory
    End Sub

    Private Async Sub Frm_PriceListSupplierToCataleg_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await Refresca(_LastPriceListCustomer, _LastCategory)
        _AllowEvents = True
    End Sub

    Private Async Function Refresca(Optional oLastPriceListCustomer As DTOPricelistCustomer = Nothing, Optional oLastCategory As DTOProductCategory = Nothing) As Task
        Dim exs As New List(Of Exception)
        With _Item
            TextBoxParent.Text = .Parent.Proveidor.NomComercialOrDefault() & " - " & .Parent.Concepte
            TextBoxRefProveidor.Text = .Ref
            TextBoxNomProveidor.Text = .Description

            Xl_PriceList_Customer1.PriceList_Customer = oLastPriceListCustomer
            Xl_LookupProduct1.Load(oLastCategory, DTOProduct.SelectionModes.SelectCategory)

            Dim oSku = Await FEB2.ProductSku.FromProveidor(exs, _Item.Parent.Proveidor, _Item.Ref)
            If exs.Count = 0 Then
                If oSku Is Nothing Then
                    TextBoxNomCurt.Text = .Description
                    TextBoxNom.Text = DTOPriceListItem_Supplier.Ref_And_Description(_Item)
                Else
                    TextBoxNomCurt.Text = oSku.nom.Tradueix(Current.Session.Lang)
                    TextBoxNom.Text = oSku.nomLlarg.Tradueix(Current.Session.Lang)
                    Xl_LookupProduct1.Value = oSku.Category
                End If

                NumericUpDownInnerPack.Value = .InnerPack

                Dim oCur As DTOCur = .Parent.Cur
                Xl_AmtCurCostTarifa.Amt = DTOAmt.Factory(oCur, .Price)
                Xl_PercentDtoOnInvoice.Value = .Parent.Discount_OnInvoice
                Xl_PercentDtoOffInvoice.Value = .Parent.Discount_OffInvoice

                If oCur.Tag = DTOCur.Eur.Tag Then
                    TextBoxExchange.Text = "1"
                    TextBoxExchange.Enabled = False
                    Xl_AmtCostBrutEur.Amt = DTOAmt.Factory(oCur, .Price)
                    Xl_AmtCurCost.Amt = Xl_AmtCostBrutEur.Amt
                Else
                    Dim oCurExchangeRate = Await FEB2.CurExchangeRate.Closest(oCur, Today, exs)
                    If exs.Count = 0 Then
                        TextBoxExchange.Text = oCurExchangeRate.Rate
                        If IsNumeric(TextBoxExchange.Text) Then
                            Dim DcExchange As Decimal = CDec(TextBoxExchange.Text)
                            Xl_AmtCostBrutEur.Amt = DTOAmt.Factory(.Price * DcExchange)
                            Xl_AmtCurCost.Amt = Xl_AmtCostBrutEur.Amt
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If

                Dim oCommercialMargin As DTOCommercialMargin = DTOProveidor.GetCommercialMargin(.Parent.Proveidor)
                oCommercialMargin.CostNet = _Item.CostNet()

                Xl_PercentCostDto.Value = .Parent.Discount_OnInvoice

                'If .TarifaA = 0 Then
                'Xl_AmtCurTarifaA.Amt = New DTOAmt(oCommercialMargin.GetTarifaA())
                'Xl_AmtCurTarifaB.Amt = New DTOAmt(oCommercialMargin.GetTarifaB())
                'Else
                'Xl_AmtCurTarifaA.Amt = New DTOAmt(.TarifaA)
                'Xl_AmtCurTarifaB.Amt = New DTOAmt(oCommercialMargin.GetTarifaB(.TarifaA))
                'End If

                If .Retail = 0 Then
                    Xl_AmtCurRetail.Amt = DTOAmt.Factory(oCommercialMargin.GetRetail())
                Else
                    Xl_AmtCurRetail.Amt = DTOAmt.Factory(.Retail)
                End If

                'Xl_PercentTarifaA.Value = Math.Round(100 * (Xl_AmtCurTarifaA.Amt.Eur / _Item.CostNet() - 1), 2, MidpointRounding.AwayFromZero)
                'Xl_PercentTarifaB.Value = Math.Round(100 * (Xl_AmtCurTarifaB.Amt.Eur / Xl_AmtCurTarifaA.Amt.Eur - 1), 2, MidpointRounding.AwayFromZero)
                Dim DcCost As Decimal = _Item.CostNet()
                If DcCost <> 0 Then
                    Xl_PercentRetail.Value = Math.Round(100 * (Xl_AmtCurRetail.Amt.Eur / DcCost - 1), 2, MidpointRounding.AwayFromZero)
                End If

                If Xl_PriceList_Customer1.PriceList_Customer Is Nothing Then
                    CheckBoxHideUntil.Text = "Ocultar a consumidors i professionals fins que la tarifa sigui vigent"
                Else
                    CheckBoxHideUntil.Text = String.Format("Ocultar a consumidors i professionals fins el {0:dd/MM/yy}", Xl_PriceList_Customer1.PriceList_Customer.Fch)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End With
    End Function

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim oCategory As DTOProductCategory = Xl_LookupProduct1.Product
        If oCategory Is Nothing Then
            MsgBox("Cal triar una categoría de producte", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim oPriceList_Customer As DTOPricelistCustomer = Xl_PriceList_Customer1.PriceList_Customer
        If oPriceList_Customer Is Nothing Then
            MsgBox("Cal assignar el preu a una tarifa de venda", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        UIHelper.ToggleProggressBar(Panel1, True)
        Dim oSku = Await FEB2.ProductSku.FromProveidor(exs, _Item.Parent.Proveidor, _Item.Ref)
        If exs.Count = 0 Then
            If oSku Is Nothing Then
                Dim sNomCurt As String = TextBoxNomCurt.Text
                Dim oExistingSku = Await FEB2.ProductSku.FromNom(exs, oCategory, sNomCurt)
                If exs.Count = 0 Then
                    If oExistingSku IsNot Nothing Then
                        UIHelper.ToggleProggressBar(Panel1, False)
                        MsgBox("Ja existeix un article '" & sNomCurt & "' dins la categoría '" & oCategory.nom.Tradueix(Current.Session.Lang) & "'", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If

                    oSku = New DTOProductSku
                    With oSku
                        .Category = oCategory
                        .RefProveidor = TextBoxRefProveidor.Text
                        .NomProveidor = TextBoxNomProveidor.Text
                        .Ean13 = DTOEan.Factory(_Item.EAN)
                        .Hereda = oCategory.dsc_PropagateToChildren
                        .nom.Esp = sNomCurt
                        .nom = New DTOLangText(sNomCurt)
                        .nomLlarg.Esp = TextBoxNom.Text
                        .Keys = TextHelper.GetArrayListFromSplitCharSeparatedString(TextBoxSearchKey.Text)
                        .InnerPack = NumericUpDownInnerPack.Value
                        .IvaCod = DTOTax.Codis.Iva_Standard
                        .UsrLog = DTOUsrLog.Factory(Current.Session.User)

                        If CheckBoxHideUntil.Checked Then
                            If Today < Xl_PriceList_Customer1.PriceList_Customer.Fch Then
                                .HideUntil = Xl_PriceList_Customer1.PriceList_Customer.Fch
                            End If
                        End If
                    End With

                    If Await FEB2.ProductSku.Update(oSku, exs) Then
                        Dim oDTOPricelistItemCustomer As New DTOPricelistItemCustomer(oPriceList_Customer)
                        With oDTOPricelistItemCustomer
                            .Sku = oSku
                            .Retail = Xl_AmtCurRetail.Amt
                        End With

                        If Await FEB2.PriceListItemCustomer.Update(exs, oDTOPricelistItemCustomer) Then
                            Me.Close()
                            RaiseEvent AfterUpdate(Me, New MatEventArgs(oDTOPricelistItemCustomer))
                        Else
                            UIHelper.ToggleProggressBar(Panel1, False)
                            UIHelper.WarnError(exs, "error al desar el preu")
                        End If
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs, "error al desar l'article")
                    End If
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
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
            sb.Append(oCategoria.nom.Tradueix(Current.Session.Lang) & " " & TextBoxNomCurt.Text)
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
            Xl_AmtCurCost.Amt = DTOAmt.Factory(_Item.Price * DcExchange)
        End If

    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        Xl_PriceList_Customer1.AfterUpdate
        If _allowevents Then
            ButtonOk.Enabled = True
        End If
    End Sub


End Class