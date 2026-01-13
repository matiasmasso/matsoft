Public Class Frm_EDiversaOrders

    Private _AllBrands As List(Of DTOProductBrand)
    Private _AllOrders As List(Of DTOEdiversaOrder)
    Private _AllowEvents As Boolean

    Private Enum Tabs
        Destinataris
        Productes
        ConfirmationPending
        Confirmations
    End Enum

    Private Async Sub Frm_EDiversaOrders_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadYears()

        Dim exs As New List(Of Exception)

        _AllOrders = Await FEB.EdiversaOrders.OpenFiles(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            If _AllOrders Is Nothing Then
                UIHelper.WarnError("Error al consultar les comandes Edi")
            Else
                _AllBrands = AllBrands(_AllOrders)
                'BLLEDiversaOrders.Validate(_AllOrders)
                Dim oFilteredOrders As List(Of DTOEdiversaOrder) = _AllOrders.FindAll(Function(x) x.Result Is Nothing)
                Dim oSortedOrders As List(Of DTOEdiversaOrder) = oFilteredOrders.OrderByDescending(Function(x) x.FchDoc).ToList
                Xl_EdiversaOrders1.Load(oSortedOrders)
                RefrescaTotals()
                RefrescaBrands()
            End If
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs, "Error al consultar les comandes Edi")
        End If
    End Sub

    Private Async Sub Xl_Brands_CheckList1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Brands_CheckList1.AfterUpdate
        If _AllowEvents Then
            Dim exs As New List(Of Exception)
            _AllOrders = Await FEB.EdiversaOrders.OpenFiles(exs)
            If exs.Count = 0 Then
                refrescaOrders()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Function AllBrands(oOrders As List(Of DTOEdiversaOrder)) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        For Each oOrder As DTOEdiversaOrder In oOrders
            For Each item As DTOEdiversaOrderItem In oOrder.Items
                If item.Sku IsNot Nothing Then
                    If Not retval.Exists(Function(x) x.Equals(item.Sku.Category.Brand)) Then
                        retval.Add(item.Sku.Category.Brand)
                    End If
                End If
            Next
        Next
        Return retval
    End Function


    Private Sub refrescaOrders()
        Dim oOrders As List(Of DTOEdiversaOrder)
        If CheckBoxOnlyOpenOrders.Checked Then
            oOrders = New List(Of DTOEdiversaOrder)
            _AllBrands = AllBrands(_AllOrders)
            'BLLEDiversaOrders.Validate(_AllOrders)

            Dim oBrands As List(Of DTOProductBrand) = Xl_Brands_CheckList1.selectedBrands
            Dim oFilteredOrders As List(Of DTOEdiversaOrder) = _AllOrders.FindAll(Function(x) x.Result Is Nothing)
            If oBrands.Count = _AllBrands.Count Then
                oOrders = oFilteredOrders
            Else
                For Each oOrder As DTOEdiversaOrder In oFilteredOrders
                    For Each item As DTOEdiversaOrderItem In oOrder.Items
                        If item.Sku IsNot Nothing Then
                            If oBrands.Exists(Function(x) x.Equals(item.Sku.Category.Brand)) Then
                                oOrders.Add(oOrder)
                                Exit For
                            End If
                        End If
                    Next
                Next
            End If
        Else
            oOrders = _AllOrders
        End If

        Dim oSortedOrders As List(Of DTOEdiversaOrder) = oOrders.OrderByDescending(Function(x) x.FchDoc).ToList
        Xl_EdiversaOrders1.Load(oSortedOrders)
        RefrescaTotals()
    End Sub

    Private Sub RefrescaTotals()
        Dim DcTot As Decimal = Xl_EdiversaOrders1.Total
        LabelTotals.Text = "total " & Xl_EdiversaOrders1.Count & " comandes total " & DTOAmt.CurFormatted(DTOAmt.Factory(DcTot))
    End Sub

    Private Sub RefrescaBrands()
        _AllBrands = AllBrands(_AllOrders)
        Xl_Brands_CheckList1.Load(_AllBrands, _AllBrands)
    End Sub

    Private Async Sub CheckBoxOnlyOpenOrders_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxOnlyOpenOrders.CheckedChanged
        If _AllowEvents Then
            Xl_Years1.Visible = Not CheckBoxOnlyOpenOrders.Checked
            Application.DoEvents()
            Dim exs As New List(Of Exception)
            If CheckBoxOnlyOpenOrders.Checked Then
                _AllOrders = Await FEB.EdiversaOrders.OpenFiles(exs) '(CheckBoxOnlyOpenOrders.Checked)
                Xl_Brands_CheckList1.Visible = True
                SplitContainer1.Panel2Collapsed = False
            Else
                _AllOrders = Await FEB.EdiversaOrders.Headers(exs, GlobalVariables.Emp, Xl_Years1.Value)
                Xl_Brands_CheckList1.Visible = False
                SplitContainer1.Panel2Collapsed = True
            End If

            If exs.Count = 0 Then
                Xl_Brands_CheckList1.Load(_AllBrands, _AllBrands)
                refrescaOrders()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub LoadYears()
        Dim oYears As New List(Of Integer)
        For i As Integer = DTO.GlobalVariables.Today().Year To 2009 Step -1
            oYears.Add(i)
        Next
        Xl_Years1.Load(oYears)
    End Sub

    Private Async Sub Xl_EdiversaOrders1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_EdiversaOrders1.RequestToRefresh
        If TypeOf e.Argument Is DTOEdiversaOrder Then
            Dim oUpdatedOrder As DTOEdiversaOrder = e.Argument
            Dim oDirtyOrder As DTOEdiversaOrder = _AllOrders.Find(Function(x) x.Guid.Equals(oUpdatedOrder.Guid))
            oDirtyOrder = oUpdatedOrder
        ElseIf TypeOf e.Argument Is List(Of DTOEdiversaOrder) Then
        End If

        Dim exs As New List(Of Exception)
        _AllOrders = Await FEB.EdiversaOrders.OpenFiles(exs)
        If exs.Count = 0 Then
            If _AllOrders Is Nothing Then
                UIHelper.WarnError("Error al descarregar comandes pendents")
            Else
                refrescaOrders()
                RefrescaBrands()
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Destinataris
            Case Tabs.Productes
                Dim oStocks = Await FEB.ProductStocks.Skus(exs, GlobalVariables.Emp.Mgz)
                If exs.Count = 0 Then
                    Xl_EdiversaOrdersSkus1.Load(_AllOrders, oStocks)
                Else
                    UIHelper.WarnError(exs)
                End If
            Case Tabs.ConfirmationPending
                Dim oEdiversaOrders As List(Of DTOEdiversaOrder) = Await FEB.EdiversaOrders.ConfirmationPending(exs)
                Xl_EdiversaOrdersConfirmationPending.Load(oEdiversaOrders)
            Case Tabs.Confirmations
                Dim oOrdrSps = Await FEB.EdiversaOrdrSps.Headers(exs)
                If exs.Count = 0 Then
                Else
                    UIHelper.WarnError(exs)
                End If
                Xl_EdiversaOrdrSps1.Load(oOrdrSps)
        End Select

    End Sub

    Private Sub Xl_EdiversaOrders1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_EdiversaOrders1.RequestToToggleProgressBar
        Dim progressBarVisible As Boolean = e.Argument
        ProgressBar1.Visible = progressBarVisible
    End Sub

    Private Async Sub ImportEdiversaFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportEdiversaFileToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar fitxer pla Ediversa"
            .Filter = "fitxers de text (*.pla;*.txt)|*.pla;*.txt|(tots els fitxers)|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim src = FileSystemHelper.GetStringContentFromFile(.FileName)
                Dim oEdiversaOrder = Await FEB.EdiversaOrder.LoadFromSrc(exs, src)
                If exs.Count = 0 Then
                    Dim oFrm As New Frm_EDiversaOrder(oEdiversaOrder)
                    AddHandler oFrm.AfterUpdate, AddressOf refrescaOrders
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_EdiversaOrders1.Filter = e.Argument
    End Sub
End Class