Public Class Frm_PricelistItemCustomer
    Private _Item As DTOPricelistItemCustomer
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
            If .Parent IsNot Nothing Then
                TextBoxPriceList.Text = .Parent.Fch.ToShortDateString & "-" & .Parent.Concepte
            End If

            If .Sku IsNot Nothing Then
                LoadArt(.Sku)

                Dim DcCostNet As Decimal
                If Xl_AmtCostNet.Amt IsNot Nothing Then
                    DcCostNet = Xl_AmtCostNet.Amt.eur
                End If

                If .Retail IsNot Nothing Then
                        Xl_AmtRetail.Amt = .Retail
                        If DcCostNet <> 0 Then
                            Xl_PercentMarginRetail.Value = Math.Round(100 * (.Retail.Val / DcCostNet - 1), 2, MidpointRounding.AwayFromZero)
                        End If
                    End If


                End If
        End With
    End Sub

    Private Async Sub LoadArt(oSku As DTOProductSku)
        Dim exs As New List(Of Exception)
        If oSku IsNot Nothing Then
            Dim oArtMenu As New Menu_ProductSku(oSku)
            Dim oContextMenu As New ContextMenuStrip
            oContextMenu.Items.AddRange(oArtMenu.Range)

            PictureBox1.Image = LegacyHelper.ImageHelper.Converter(Await FEB2.ProductSku.Image(exs, oSku))
            If exs.Count = 0 Then
                PictureBox1.ContextMenuStrip = oContextMenu
            Else
                UIHelper.WarnError(exs)
            End If

            If FEB2.ProductSku.Load(oSku, exs) Then
                Xl_LookupProduct1.Product = oSku
                Dim DcCostNet As Decimal
                Dim oPriceListItem_Supplier = Await FEB2.PriceListItemSupplier.FromSku(exs, oSku)
                If exs.Count = 0 Then
                    If oPriceListItem_Supplier IsNot Nothing Then
                        Dim oPriceList_Supplier As DTOPriceListSupplier = oPriceListItem_Supplier.Parent
                        Dim oCost As DTOAmt = oPriceList_Supplier.Cur.AmtFromDivisa(oPriceListItem_Supplier.Price)
                        Xl_AmtCurCostTarifa.Amt = oCost
                        Xl_AmtCostBrutEur.Amt = DTOAmt.Factory(oCost.Eur) 'Contravalor
                        Xl_PercentDtoOnInvoice.Value = oPriceList_Supplier.Discount_OnInvoice
                        Xl_PercentDtoOffInvoice.Value = oPriceList_Supplier.Discount_OffInvoice
                        DcCostNet = Math.Round(oCost.Eur * (100 - oPriceList_Supplier.Discount_OnInvoice - oPriceList_Supplier.Discount_OffInvoice) / 100, DTOCur.Eur.Decimals, MidpointRounding.AwayFromZero)
                    End If
                    Xl_AmtCostNet.Amt = DTOAmt.Factory(DcCostNet)
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If

    End Sub

    Private Async Function CostNet(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of Decimal)
        Dim retval As Decimal
        If oSku IsNot Nothing Then
            If oSku.RefProveidor = "" Then FEB2.ProductSku.Load(oSku, exs)
            Dim oPriceListItem_Supplier = Await FEB2.PriceListItemSupplier.GetPreusDeCost(exs, oSku) ' oArt.GetPreusDeCost
            If oPriceListItem_Supplier IsNot Nothing Then
                retval = oPriceListItem_Supplier.CostNet()
            End If
        End If
        Return retval
    End Function



    Private Async Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        With _Item
            .Sku = Xl_LookupProduct1.Product
            .Retail = Xl_AmtRetail.Amt
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.PriceListItemCustomer.Update(exs, _Item) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Item))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Async Sub Xl_AmtRetail_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmtRetail.AfterUpdate
        If _allowEvents Then
            Await CalcMarginRetail()
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Async Function CalcMarginRetail() As Task
        Dim exs As New List(Of Exception)
        Dim oSku As DTOProductSku = Xl_LookupProduct1.Product
        Dim DcCostNet = Await CostNet(exs, oSku)
        If exs.Count = 0 Then
            If DcCostNet > 0 Then
                Xl_PercentMarginRetail.Value = Math.Round(100 * (Xl_AmtRetail.Amt.Eur / DcCostNet - 1), 2, MidpointRounding.AwayFromZero)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Product1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        Dim exs As New List(Of Exception)
        If _allowEvents Then
            Dim oSku As DTOProductSku = Xl_LookupProduct1.Product
            Dim DcCostNet As Decimal = Await CostNet(exs, oSku)
            If exs.Count = 0 Then
                LoadArt(oSku)
                ButtonOk.Enabled = True
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub TextBoxPriceList_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxPriceList.DoubleClick
        Dim oFrm As New Frm_PriceList_Customer(_Item.Parent)
        oFrm.Show()
    End Sub

    Private Sub Xl_AmtRetail_TextChanged(sender As Object, e As EventArgs) Handles Xl_AmtRetail.TextChanged
        RaiseEvent AfterUpdate(Me, New MatEventArgs(Xl_AmtRetail.Amt))
    End Sub



    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem el preu de " & _Item.sku.nom.Tradueix(Current.Session.Lang) & " ?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.PriceListItemCustomer.Delete(exs, _Item) Then
                MsgBox("preu eliminat", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, New System.EventArgs)
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub
End Class