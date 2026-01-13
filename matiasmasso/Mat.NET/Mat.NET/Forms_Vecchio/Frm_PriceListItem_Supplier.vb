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
            Xl_AmtCost.Amt = New Amt(.Price)
            Xl_AmtRRPP.Amt = New Amt(.Retail)
        End With
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Item
            .Ref = TextBoxRef.Text
            .EAN = TextBoxEAN.Text
            .Description = TextBoxNom.Text
            .Price = Xl_AmtCost.Amt.Val
            .Retail = Xl_AmtRRPP.Amt.Val
            '.Parent.UpdateItem(_Item)
        End With

        Dim exs As New List(Of exception)
        If BLL.BLLPriceListItem_Supplier.Update(_Item, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Item))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TextBoxRef_TextChanged(sender As Object, e As EventArgs) Handles _
        TextBoxRef.TextChanged, _
         TextBoxEAN.TextChanged, _
          TextBoxNom.TextChanged, _
           Xl_AmtCost.AfterUpdate, _
            Xl_AmtRRPP.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonBrowseProduct_Click(sender As Object, e As EventArgs) Handles ButtonBrowseProduct.Click
        'Dim oFrm As New Frm_Arts(Frm_Arts.SelModes.SelectArt)
        Dim oFrm As New Frm_Products(, Frm_Products.SelModes.SelectProductSku)
        AddHandler oFrm.onItemSelected, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(sender As Object, e As MatEventArgs)
        Dim oSku As DTOProductSku = e.Argument
        BLL.BLLProductSku.Load(oSku)
        With _Item
            .Sku = oSku
            If oSku.Ean13 IsNot Nothing Then
                .EAN = oSku.Ean13.Value
            End If
            .Description = BLL.BLLProductSku.nomPrvOrMyd(oSku)
            .Ref = oSku.RefProveidor
            If oSku.TarifaCost IsNot Nothing Then
                .Price = oSku.TarifaCost.Eur
            End If
            If oSku.InnerPack <> 0 Then
                .InnerPack = oSku.InnerPack
            End If
        End With
        refresca()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest preu de la tarifa?", MsgBoxStyle.YesNo)
        If rc = MsgBoxResult.Yes Then
            Dim exs As New List(Of Exception)
            If BLL.BLLPriceListItem_Supplier.delete(_Item, exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar el preu")
            End If
        End If
    End Sub
End Class