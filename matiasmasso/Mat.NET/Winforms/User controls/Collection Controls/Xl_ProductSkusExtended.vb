Imports MatHelperStd

Public Class Xl_ProductSkusExtended
    Inherits DataGridView

    Private _Values As List(Of DTOProductSku)
    Private _DefaultValue As DTOVisaEmisor
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _MouseIsDown As Boolean
    Private _ShowRealStocks As Boolean
    Property ShowObsolets As Boolean

    Private _Filter As String
    Private _Related As DTOProductSku.Relateds
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNewSku(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNewPack(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNewBundle(sender As Object, e As MatEventArgs)
    Public Event RequestToRemove(sender As Object, e As MatEventArgs)
    Public Event RequestToToggleObsoletos(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Nom
        LastProductionIco
        Stk
        Pn2
        Pn3
        RRPP
        ImgIco
        Pn1
    End Enum

    Public Shadows Sub Load(values As List(Of DTOProductSku), Optional oDefaultValue As DTOProductSku = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse, Optional oRelated As DTOProductSku.Relateds = DTOProductSku.Relateds.NotSet)
        _Values = values
        _Related = oRelated

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOProductSku)
        Get
            Return _Values
        End Get
    End Property

    Public WriteOnly Property ShowRealStocks As Boolean
        Set(value As Boolean)
            _ShowRealStocks = value
            Refresca()
        End Set
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOProductSku) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOProductSku In oFilteredValues
            If _ShowObsolets Or oItem.obsoleto = False Or oItem.stock > 0 Or oItem.clients > 0 Or oItem.proveidors > 0 Then
                If oItem.bundleSkus.Count > 0 Then
                    Dim oControlItem As New ControlItem(oItem, _ShowRealStocks, _Related, DTOProductSku.BundleCods.parent)
                    _ControlItems.Add(oControlItem)
                    For Each oBundleChild In oItem.bundleSkus
                        oControlItem = New ControlItem(oBundleChild.Sku, _ShowRealStocks, _Related, DTOProductSku.BundleCods.child)
                        _ControlItems.Add(oControlItem)
                    Next
                Else
                    Dim oControlItem As New ControlItem(oItem, _ShowRealStocks, _Related, DTOProductSku.BundleCods.none)
                    _ControlItems.Add(oControlItem)
                End If
            End If
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        For i = 0 To MyBase.Rows.Count - 1
            Dim oProductSku As DTOProductSku = RowSku(i)
            If oProductSku.bundleSkus.Count > 0 Then
                Dim oRow As DataGridViewRow = MyBase.Rows(i)
                oRow.Height = MyBase.Font.Height * 2
            End If
        Next

        'If _ControlItems.Count > 0 Then
        'MyBase.CurrentCell = MyBase.FirstDisplayedCell
        'End If

        'If _DefaultValue IsNot Nothing Then
        'Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
        'Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
        'If rowIdx >= 0 Then
        'MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
        'End If
        'End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Sub Clear()
        _ControlItems = New ControlItems
    End Sub
    Public ReadOnly Property Value As DTOProductSku
        Get

            Dim retval As DTOProductSku = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        With MyBase.RowTemplate
            .Height = MyBase.Font.Height * 1.3
            .DefaultCellStyle.BackColor = Color.Transparent
        End With

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = True
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Id)
            .DataPropertyName = "Id"
            .HeaderText = "ref."
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .DataPropertyName = "Nom"
            .HeaderText = "nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.LastProductionIco), DataGridViewImageColumn)
            .HeaderText = ""
            .Width = 18
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Stk)
            .DataPropertyName = "stock"
            .HeaderText = "stock"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#"
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn2)
            .DataPropertyName = "clients"
            .HeaderText = "clients"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#"
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn3)
            .DataPropertyName = "pot"
            .HeaderText = "prog"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#"
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.RRPP)
            .HeaderText = "PVP"
            .DataPropertyName = "RRPP"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.ImgIco), DataGridViewImageColumn)
            .HeaderText = ""
            .Width = 18
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn1)
            .DataPropertyName = "proveidors"
            .HeaderText = "prov"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#"
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With

    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim previousAllowEvents = _AllowEvents
        _AllowEvents = False
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_ProductSku As New Menu_ProductSku(SelectedItems)
            AddHandler oMenu_ProductSku.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_ProductSku.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim oMenuItemObsolets As New ToolStripMenuItem("inclou obsolets")
        oMenuItemObsolets.CheckOnClick = True
        AddHandler oMenuItemObsolets.CheckedChanged, AddressOf onMenuItemObsoletsCheckedChanged
        oContextMenu.Items.Add(oMenuItemObsolets)

        oMenuItemObsolets.Checked = _ShowObsolets
        _AllowEvents = previousAllowEvents

        oContextMenu.Items.Add("afegir article", Nothing, AddressOf Do_AddNewSku)
        oContextMenu.Items.Add("afegir bundle", Nothing, AddressOf Do_AddNewBundle)

        Select Case _Related
            Case DTOProduct.Relateds.NotSet
                oContextMenu.Items.Add("afegir pack", Nothing, AddressOf Do_AddNewPack)
            Case DTOProduct.Relateds.Accessories, DTOProduct.Relateds.Spares
                oContextMenu.Items.Add("retirar d'aquesta llista", Nothing, AddressOf Do_Remove)
        End Select

        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
        _AllowEvents = previousAllowEvents
    End Sub

    Private Sub onMenuItemObsoletsCheckedChanged(sender As Object, e As EventArgs)
        If _AllowEvents Then
            Dim oMenuItem As ToolStripMenuItem = sender
            _ShowObsolets = oMenuItem.Checked
            RaiseEvent RequestToToggleObsoletos(Me, MatEventArgs.Empty)
            Refresca()
        End If
    End Sub

    Private Sub Do_AddNewSku()
        RaiseEvent RequestToAddNewSku(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_AddNewBundle()
        RaiseEvent RequestToAddNewBundle(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Excel()
        Dim oLang = Current.Session.Lang
        Dim oDomain = DTOWebDomain.Factory(oLang, True)
        Dim oSheet As New ExcelHelper.Sheet("Stocks")
        With oSheet
            .addColumn("ref", ExcelHelper.Sheet.NumberFormats.W50)
            .addColumn("nom", ExcelHelper.Sheet.NumberFormats.W50)
            .addColumn("stock", ExcelHelper.Sheet.NumberFormats.Integer)
            .addColumn("retail", ExcelHelper.Sheet.NumberFormats.Euro)
        End With
        For Each item As DTOProductSku In SelectedItems()
            Dim oRow As ExcelHelper.Row = oSheet.addRow
            oRow.addCell(item.id, item.GetUrl(oLang))
            oRow.addCell(item.NomLlarg.Tradueix(oLang), item.GetUrl(oLang))
            oRow.addCell(item.stock)
            oRow.AddCellAmt(item.rrpp)
        Next

        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Remove()
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem IsNot Nothing Then
            Dim oSku As DTOProductSku = oControlItem.Source
            RaiseEvent RequestToRemove(Me, New MatEventArgs(oSku))
        End If
    End Sub

    Private Sub Do_AddNewPack()
        RaiseEvent RequestToAddNewPack(Me, MatEventArgs.Empty)
    End Sub

    Private Function MenuItem_Obsolets() As ToolStripMenuItem
        Dim retval As New ToolStripMenuItem
        With retval
            .Text = "Inclou obsolets"
            .CheckOnClick = True
            .Checked = _ShowObsolets
        End With
        AddHandler retval.Click, AddressOf Refresca
        Return retval
    End Function


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOProductSku = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_ProductSku(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                'RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
                SetContextMenu()
            End If
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Function RowSku(RowIndex As Integer) As DTOProductSku
        Dim oRow As DataGridViewRow = MyBase.Rows(RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim retval As DTOProductSku = oControlItem.Source
        Return retval
    End Function

    Private Sub Xl_ProductSkusExtended_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Pn1
                Dim oProductSku As DTOProductSku = RowSku(e.RowIndex)
                If oProductSku.proveidors = 0 Then
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.SelectionBackColor = Color.White
                Else
                    If oProductSku.previsions = 0 Then
                        e.CellStyle.BackColor = Color.White
                        e.CellStyle.SelectionBackColor = Color.White
                    ElseIf oProductSku.previsions > oProductSku.clients - oProductSku.stock Then
                        e.CellStyle.BackColor = Color.LightGreen
                        e.CellStyle.SelectionBackColor = Color.LightGreen
                    ElseIf oProductSku.previsions = oProductSku.clients Then
                        e.CellStyle.BackColor = Color.Yellow
                        e.CellStyle.SelectionBackColor = Color.Yellow
                    Else
                        e.CellStyle.BackColor = Color.LightSalmon
                        e.CellStyle.SelectionBackColor = Color.LightSalmon
                    End If
                End If

            Case Cols.LastProductionIco
                Dim oProductSku As DTOProductSku = RowSku(e.RowIndex)
                If oProductSku.lastProduction Then
                    e.Value = My.Resources.wrong
                Else
                    Select Case DTOProductSku.Moq(oProductSku)
                        Case 0, 1
                            e.Value = My.Resources.empty
                        Case 2
                            e.Value = My.Resources.dau2_17x17
                        Case 3
                            e.Value = My.Resources.dau3_17x17
                        Case 4
                            e.Value = My.Resources.dau4_17x17
                        Case 5
                            e.Value = My.Resources.dau5_17x17
                        Case Else
                            e.Value = My.Resources.dau6_17x17
                    End Select
                End If
            Case Cols.ImgIco
                Dim oProductSku As DTOProductSku = RowSku(e.RowIndex)
                If oProductSku.imageExists Then
                    e.Value = My.Resources.img_16
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub Xl_ProductSkusExtended_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oProductSku As DTOProductSku = RowSku(e.RowIndex)
        If oProductSku.bundleSkus.Count > 0 Then
            UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.White)

        Else
            Select Case oProductSku.stock
                Case <= 0
                    If oProductSku.obsoleto Then
                        UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.LightGray)
                    Else
                        UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.LightSalmon)
                    End If
                Case <= oProductSku.clients - oProductSku.clientsAlPot - oProductSku.clientsEnProgramacio
                    UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.Yellow)
                Case Else
                    UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.LightGreen)
            End Select
        End If

    End Sub

    Private Sub Xl_ProductSkusExtended_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Me.CellPainting
        If e.RowIndex >= 0 Then
            Dim oProductSku As DTOProductSku = RowSku(e.RowIndex)
            If oProductSku.bundleSkus.Count > 0 Then
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None
            End If
        End If
    End Sub

    Private Sub Xl_ProductSkusExtended_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            If oRow IsNot Nothing Then
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem IsNot Nothing Then
                    Dim oSku As DTOProductSku = oControlItem.Source
                    e.ToolTipText = oSku.nomLlarg.Tradueix(Current.Session.Lang)
                End If
            End If
        End If
    End Sub


#Region "DragDrop"

    Private mLastMouseDownRectangle As System.Drawing.Rectangle

    Private Sub PictureBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        e.Effect = DragDropHelper.DragEnterFilePresentEffect(e)
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                mLastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not mLastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(mLastMouseDownRectangle.X, mLastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    Dim oSkus As List(Of DTOProductSku) = SelectedItems()
                    sender.DoDragDrop(oSkus, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub

    Private Async Sub Xl_ProductSkusExtended_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim exs As New List(Of Exception)
        If e.Data.GetDataPresent(DataFormats.FileDrop, False) Or (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            Dim oImage = DragDropHelper.GetDroppedImage(exs, e)
            If exs.Count = 0 Then
                Dim oSkus = SelectedItems()
                If oSkus.Count = 0 Then
                    UIHelper.WarnError("No hi ha cap producte seleccionat")
                Else
                    Dim oSku = oSkus.First
                    If FEB2.ProductSku.Load(oSku, exs) Then
                        oSku.image = LegacyHelper.ImageHelper.Converter(oImage)
                        If Await FEB2.ProductSku.Update(oSku, exs) Then
                            RaiseEvent RequestToRefresh(Me, New MatEventArgs(oSku))
                        Else
                            UIHelper.WarnError(exs, "error al desar la imatge al producte")
                        End If

                    Else
                        UIHelper.WarnError(exs, "error al llegir les dades del producte")
                    End If
                End If
            Else
                UIHelper.WarnError(exs, "error al importar el document a l'article")
            End If
        Else
            UIHelper.WarnError("l'element arrossegat no ha estat identificat com a document valid per aquesta operació")
        End If

    End Sub



#End Region



    Protected Class ControlItem
        Property Source As DTOProductSku

        Property Id As Integer
        Property Nom As String
        Property Stock As Integer
        Property Clients As Integer
        Property Pot As Integer
        Property RRPP As Decimal
        Property Proveidors As Integer
        Property BundleCod As DTOProductSku.BundleCods

        Public Sub New(value As DTOProductSku, ShowRealStocks As Boolean, oRelated As DTOProduct.Relateds, oBundleCod As DTOProductSku.BundleCods)
            MyBase.New()
            _Source = value
            With value
                _Id = .id
                If oRelated = DTOProduct.Relateds.NotSet Then
                    _Nom = .nom.Tradueix(Current.Session.Lang)
                Else
                    _Nom = .nomLlarg.Tradueix(Current.Session.Lang)
                End If
                If .rrpp IsNot Nothing Then
                    _RRPP = .rrpp.eur
                End If

                _BundleCod = oBundleCod
                Select Case oBundleCod
                    Case DTOProductSku.BundleCods.none, DTOProductSku.BundleCods.child
                        _Stock = .stock
                        _Pot = .clientsAlPot + .clientsEnProgramacio
                        _Clients = .clients - _Pot
                        _Proveidors = .proveidors

                        If Not ShowRealStocks Then
                            _Stock -= .clientsBlockStock
                            _Clients -= .clientsBlockStock
                            _Proveidors -= .clientsBlockStock
                        End If
                End Select
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


