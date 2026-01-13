Public Class Menu_EDiversaOrderItem
    Inherits Menu_Base

    Private _EdiversaOrderItem As DTOEdiversaOrderItem


    Public Sub New(ByVal oEdiversaOrderItem As DTOEdiversaOrderItem)
        MyBase.New()
        _EdiversaOrderItem = oEdiversaOrderItem
    End Sub

    Public Shadows Async Function Range() As Task(Of ToolStripMenuItem())
        Dim oMenuItemExceptions = Await MenuItem_Exceptions()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Sku(),
        MenuItem_Preu(),
        oMenuItemExceptions,
        MenuItem_Delete()})

    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        oMenuItem.Enabled = False
        Return oMenuItem
    End Function

    Private Function MenuItem_Sku() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Article..."
        If _EdiversaOrderItem.Sku Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.DropDownItems.AddRange(New Menu_ProductSku(_EdiversaOrderItem.Sku).Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Preu() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "preu"
        oMenuItem.Visible = False
        AddHandler oMenuItem.Click, AddressOf Do_Preu
        Return oMenuItem
    End Function

    Private Async Function MenuItem_Exceptions() As Task(Of ToolStripMenuItem)
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Errors"
        If _EdiversaOrderItem.Exceptions.Count = 0 Then
            oMenuItem.Visible = False
        Else
            oMenuItem.ForeColor = Color.Red
            For Each ex As DTOEdiversaException In _EdiversaOrderItem.Exceptions
                Dim oMenuException As New ToolStripMenuItem(ex.Msg)
                oMenuItem.DropDownItems.Add(oMenuException)
                Dim oMenu_Exceptions As New Menu_EDiversaException(ex)
                AddHandler oMenu_Exceptions.AfterUpdate, AddressOf RefreshRequest
                'For Each item In oMenu_Exceptions.Range
                'oMenuException.DropDownItems.Add(New ToolStripItem(oMenu_Exceptions.Range)
                ' Next
                Dim oRange = Await oMenu_Exceptions.Range
                oMenuException.DropDownItems.AddRange(oRange)
            Next
        End If
        'AddHandler oMenuItem.Click, AddressOf Do_Exceptions
        Return oMenuItem
    End Function

    Private Function Menu_ItemException_SkuNotFound() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "No s'ha trobat l'article. Seleccionar-lo del cataleg..."
        AddHandler oMenuItem.Click, AddressOf Do_SkuNotFound
        Return oMenuItem
    End Function

    Private Function Menu_ItemException_MissingPrice() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Sense preu de tarifa"
        AddHandler oMenuItem.Click, AddressOf Do_MissingPrice
        Return oMenuItem
    End Function

    Private Function Menu_ItemException_WrongPrice() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Preu erroni"
        oMenuItem.DropDownItems.Add(Menu_ItemException_WrongPrice_AcceptIt)
        oMenuItem.DropDownItems.Add(Menu_ItemException_WrongPrice_AmendIt)
        Return oMenuItem
    End Function

    Private Function Menu_ItemException_WrongPrice_AcceptIt() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Accepta'l"
        AddHandler oMenuItem.Click, AddressOf Do_WrongPrice_AcceptIt
        Return oMenuItem
    End Function

    Private Function Menu_ItemException_WrongPrice_AmendIt() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Posa-hi el preu correcte"
        AddHandler oMenuItem.Click, AddressOf Do_WrongPrice_AmendIt
        Return oMenuItem
    End Function

    Private Function Menu_ItemException_WrongDiscount() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Descompte erroni"
        oMenuItem.DropDownItems.Add(Menu_ItemException_WrongDiscount_AcceptIt)
        oMenuItem.DropDownItems.Add(Menu_ItemException_WrongDiscount_AmendIt)
        Return oMenuItem
    End Function

    Private Function Menu_ItemException_WrongDiscount_AcceptIt() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Accepta'l"
        AddHandler oMenuItem.Click, AddressOf Do_WrongPrice_AcceptIt
        Return oMenuItem
    End Function

    Private Function Menu_ItemException_WrongDiscount_AmendIt() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Posa-hi el descompte correcte"
        AddHandler oMenuItem.Click, AddressOf Do_WrongPrice_AmendIt
        Return oMenuItem
    End Function


    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_EdiversaOrderItem(_EdiversaOrderItem)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub
    Private Sub Do_Exceptions(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_EDiversaExceptions(_EdiversaOrderItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_SkuNotFound(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oProduct As DTOProductSku = Nothing
        If IsNumeric(_EdiversaOrderItem.RefProveidor) Then
            oProduct = Await FEB.ProductSku.FromId(exs, Current.Session.Emp, _EdiversaOrderItem.RefProveidor)
            If exs.Count <> 0 Then
                UIHelper.WarnError(exs)
            End If
        End If
        If oProduct Is Nothing And _EdiversaOrderItem.RefClient > "" Then
            Dim oEdiversaOrder As DTOEdiversaOrder = _EdiversaOrderItem.Parent
            Dim oCustomerProducts = Await FEB.CustomerProducts.All(exs, oEdiversaOrder.Comprador,, _EdiversaOrderItem.RefClient)
            If oCustomerProducts.Count > 0 Then
                oProduct = oCustomerProducts(0).Sku
            End If
        End If

        Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectSku)
        AddHandler oFrm.OnItemSelected, AddressOf onSkuSelected
        oFrm.Show()


    End Sub

    Private Sub onSkuSelected(sender As Object, e As MatEventArgs)
        _EdiversaOrderItem.Sku = e.Argument
        RefreshRequest(Me, New MatEventArgs(_EdiversaOrderItem))
    End Sub

    Private Sub Do_MissingPrice(ByVal sender As Object, ByVal e As System.EventArgs)
        MsgBox("sorry, encara no está implementat")
    End Sub

    Private Sub Do_WrongPrice_AcceptIt(ByVal sender As Object, ByVal e As System.EventArgs)
        MsgBox("sorry, encara no está implementat")
    End Sub

    Private Sub Do_WrongPrice_AmendIt(ByVal sender As Object, ByVal e As System.EventArgs)
        MsgBox("sorry, encara no está implementat")
    End Sub

    Private Sub Do_WrongDiscount_AcceptIt(ByVal sender As Object, ByVal e As System.EventArgs)
        MsgBox("sorry, encara no está implementat")
    End Sub

    Private Sub Do_WrongDiscount_AmendIt(ByVal sender As Object, ByVal e As System.EventArgs)
        MsgBox("sorry, encara no está implementat")
    End Sub

    Private Sub Do_Preu(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_SkuPreuJustification(_EdiversaOrderItem.First)
        'oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        _EdiversaOrderItem.Parent.Items.Remove(_EdiversaOrderItem)
        Dim exs As New List(Of Exception)
        If Await FEB.EdiversaOrder.Update(_EdiversaOrderItem.Parent, exs) Then
            MyBase.RefreshRequest(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class

