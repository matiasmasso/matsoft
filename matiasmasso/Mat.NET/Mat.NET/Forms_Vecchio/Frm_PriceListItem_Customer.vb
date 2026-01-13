Public Class Frm_PricelistItemCustomer
    Private _Item As DTOPricelistItemCustomer
    Private _CurExchangeRate As DTOCurExchangeRate
    Private _allowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Public Sub New(oItem As DTOPricelistItemCustomer)
        MyBase.New()
        Me.InitializeComponent()
        _Item = oItem
        Refresca()
        _allowEvents = True
    End Sub

    Private Sub Refresca()
        With _Item
            TextBoxPriceList.Text = .Parent.Fch.ToShortDateString & "-" & .Parent.Concepte
            If .Sku IsNot Nothing Then
                LoadArt(.Sku)
                Dim DcCostNet As Decimal = Xl_AmtCostNet.Amt2.Eur

                If .TarifaA IsNot Nothing Then
                    Xl_AmtTarifaA.Amt2 = .TarifaA
                    If DcCostNet <> 0 Then
                        Xl_PercentMarginTarifaA.Value = Math.Round(100 * (.TarifaA.Eur / DcCostNet - 1), 2)
                    End If
                End If
                If .TarifaB IsNot Nothing Then
                    Xl_AmtTarifaB.Amt2 = .TarifaB
                    If .TarifaA.Eur <> 0 Then
                        Xl_PercentMarginTarifaB.Value = Math.Round(100 * (.TarifaB.Eur / .TarifaA.Eur - 1), 2)
                    End If
                End If
                If .Retail IsNot Nothing Then
                    Xl_AmtRetail.Amt2 = .Retail
                    If DcCostNet <> 0 Then
                        Xl_PercentMarginRetail.Value = Math.Round(100 * (.Retail.Eur / DcCostNet - 1), 2)
                    End If
                End If

            End If
        End With
    End Sub

    Private Sub LoadArt(oSku As DTOProductSku)
        If oSku IsNot Nothing Then
            Dim oArtMenu As New Menu_Art(oSku)
            Dim oContextMenu As New ContextMenuStrip
            oContextMenu.Items.AddRange(oArtMenu.Range)

            Dim oArt As New Art(oSku.Guid)
            PictureBox1.Image = oArt.Image
            PictureBox1.ContextMenuStrip = oContextMenu
            Xl_LookupProduct1.Product = oSku
            Dim DcCostNet As Decimal
            Dim oPriceListItem_Supplier As DTOPriceListItem_Supplier = oArt.GetPreuDeCost
            If oPriceListItem_Supplier IsNot Nothing Then
                Dim oPriceList_Supplier As DTOPriceList_Supplier = oPriceListItem_Supplier.Parent
                _CurExchangeRate = BLL.BLLCurExchangeRate.Closest(oPriceList_Supplier.Cur)
                Dim oCost As DTOAmt = BLL.BLLCurExchangeRate.Amt(oPriceListItem_Supplier.Price, _CurExchangeRate)
                Xl_AmtCurCostTarifa.Amt2 = oCost
                Xl_AmtCostBrutEur.Amt2 = New DTOAmt(oCost.Eur) 'Contravalor
                Xl_PercentDtoOnInvoice.Value = oPriceList_Supplier.Discount_OnInvoice
                Xl_PercentDtoOffInvoice.Value = oPriceList_Supplier.Discount_OffInvoice
                DcCostNet = Math.Round(oCost.Eur * (100 - oPriceList_Supplier.Discount_OnInvoice - oPriceList_Supplier.Discount_OffInvoice) / 100, DTOCur.Eur.Decimals)
            End If
            Xl_AmtCostNet.Amt2 = New DTOAmt(DcCostNet)
        End If

    End Sub

    Private Function CostNet(oSku As DTOProductSku) As Decimal
        Dim retval As Decimal
        If oSku IsNot Nothing Then
            If oSku.RefProveidor = "" Then BLL.BLLProductSku.Load(oSku)
            Dim oPriceListItem_Supplier As DTO.DTOPriceListItem_Supplier = BLL.BLLPriceListItem_Supplier.GetPreusDeCost(oSku) ' oArt.GetPreusDeCost
            If oPriceListItem_Supplier IsNot Nothing Then
                retval = BLL.BLLPriceList_Supplier.CostNet(oPriceListItem_Supplier)
            End If
        End If
        Return retval
    End Function



    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        With _Item
            .Sku = Xl_LookupProduct1.Product
            If Xl_AmtTarifaA.Amt IsNot Nothing Then
                .TarifaA = New DTOAmt(Xl_AmtTarifaA.Amt.Eur)
            End If
            If Xl_AmtTarifaB.Amt IsNot Nothing Then
                .TarifaB = New DTOAmt(Xl_AmtTarifaB.Amt.Eur)
            End If
            .Retail = New DTOAmt(Xl_AmtRetail.Amt.Eur)
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLPricelistItemCustomer.Update(_Item, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Item))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub Xl_AmtTarifaA_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmtTarifaA.AfterUpdate
        If _allowEvents Then
            CalcMarginTarifaA()
            CalcMarginTarifaB()
            CalcMarginRetail()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_AmtTarifaB_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmtTarifaB.AfterUpdate
        If _allowEvents Then
            CalcMarginTarifaB()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_AmtRetail_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmtRetail.AfterUpdate
        If _allowEvents Then
            CalcMarginRetail()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CalcMarginTarifaA()
        Dim oSku As DTOProductSku = Xl_LookupProduct1.Product
        Dim DcCostNet As Decimal = CostNet(oSku)
        If DcCostNet <> 0 Then
            Xl_PercentMarginTarifaA.Value = Math.Round(100 * (Xl_AmtTarifaA.Amt.Eur / DcCostNet - 1), 2)
        End If
    End Sub

    Private Sub CalcMarginTarifaB()
        If Xl_AmtTarifaA.Amt.Eur <> 0 Then
            If Xl_AmtTarifaB.Amt IsNot Nothing Then
                Xl_PercentMarginTarifaB.Value = Math.Round(100 * (Xl_AmtTarifaB.Amt.Eur / Xl_AmtTarifaA.Amt.Eur - 1), 2)
            End If
        End If
    End Sub

    Private Sub CalcMarginRetail()
        If Xl_AmtTarifaA.Amt IsNot Nothing Then
            If Xl_AmtTarifaA.Amt.Eur <> 0 Then
                If Xl_AmtRetail.Amt IsNot Nothing Then
                    Xl_PercentMarginRetail.Value = Math.Round(100 * (Xl_AmtRetail.Amt.Eur / Xl_AmtTarifaA.Amt.Eur - 1), 2)
                End If
            End If
        End If
    End Sub

    Private Sub Xl_Product1_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_LookupProduct1.AfterUpdate
        If _allowEvents Then
            Dim oSku As DTOProductSku = Xl_LookupProduct1.Product
            Dim DcCostNet As Decimal = CostNet(oSku)
            LoadArt(oSku)

            ButtonOk.Enabled = True
        End If
    End Sub
End Class