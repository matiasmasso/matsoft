Public Class Xl_ProductStocks
    Private _Values As List(Of DTO.DTOStock)
    Private _ControlItems As ControlItems
    Private _MenuItem_Obsolets As ToolStripMenuItem
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Nom
        LastProductionIco
        Stk
        Pn2
        Pn3
        ImgIco
        Pn1
    End Enum

    Public Shadows Sub Load(values As List(Of DTO.DTOStock))
        _Values = values
        _MenuItem_Obsolets = MenuItem_Obsolets()
        LoadControlItems()
    End Sub

    Public Sub Clear()
        _ControlItems = New ControlItems
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTO.DTOStock
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTO.DTOStock = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub LoadControlItems()
        _ControlItems = New ControlItems

        For Each oItem As DTO.DTOStock In _Values
            If _MenuItem_Obsolets.Checked Or oItem.Sku.Obsoleto = False Then
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            End If
        Next

        LoadGrid()
    End Sub



    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True


            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Id)
                .DataPropertyName = "Id"
                .HeaderText = "ref."
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .DataPropertyName = "Nom"
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With .Columns(Cols.LastProductionIco)
                .HeaderText = ""
                .Width = 18
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Stk)
                .DataPropertyName = "UnitsInStock"
                .HeaderText = "stock"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
                .DefaultCellStyle.SelectionForeColor = Color.Black
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Pn2)
                .DataPropertyName = "UnitsInClients"
                .HeaderText = "clients"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
                .DefaultCellStyle.SelectionForeColor = Color.Black
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Pn3)
                .DataPropertyName = "UnitsInPot"
                .HeaderText = "pot"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
                .DefaultCellStyle.SelectionForeColor = Color.Black
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With .Columns(Cols.ImgIco)
                .HeaderText = ""
                .Width = 18
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Pn1)
                .DataPropertyName = "UnitsInProveidor"
                .HeaderText = "prov"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
                .DefaultCellStyle.SelectionForeColor = Color.Black
            End With


        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'Exit Sub
        Select Case e.ColumnIndex
            Case Cols.Pn1
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oStock As DTO.DTOStock = oControlItem.Source
                If oStock.UnitsInProveidor = 0 Then
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.SelectionBackColor = Color.White
                Else
                    Dim iStk As Integer = oStock.UnitsInStock
                    Dim iPn2 As Integer = oStock.UnitsInClients
                    Dim iPn1 As Integer = oStock.UnitsInProveidor
                    Dim iPrv As Integer = oStock.UnitsInPrevisio
                    If iPrv = 0 Then
                        e.CellStyle.BackColor = Color.White
                        e.CellStyle.SelectionBackColor = Color.White
                    ElseIf iPrv >= iPn2 - iStk Then
                        e.CellStyle.BackColor = Color.LightGreen
                        e.CellStyle.SelectionBackColor = Color.LightGreen
                    Else
                        If iPrv = iPn1 Then
                            e.CellStyle.BackColor = Color.Yellow
                            e.CellStyle.SelectionBackColor = Color.Yellow
                        Else
                            e.CellStyle.BackColor = Color.LightSalmon
                            e.CellStyle.SelectionBackColor = Color.LightSalmon
                        End If
                    End If
                End If

            Case Cols.LastProductionIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow IsNot Nothing Then
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oStock As DTO.DTOStock = oControlItem.Source
                    e.Value = IIf(oStock.Sku.LastProduction, My.Resources.wrong, My.Resources.empty)
                End If
            Case Cols.ImgIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow IsNot Nothing Then
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oStock As DTO.DTOStock = oControlItem.Source
                    If oStock.Sku.ImageExists Then
                        e.Value = My.Resources.img_16
                    Else
                        e.Value = My.Resources.empty
                    End If
                End If
        End Select
    End Sub

    Private Sub DataGridViewArts_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        If oRow IsNot Nothing Then

            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Dim oStock As DTO.DTOStock = oControlItem.Source

            If oStock.UnitsInStock <= 0 Then
                PaintGradientRowBackGround(DataGridView1, e, Color.LightSalmon)
            Else
                Dim iStk As Integer = oStock.UnitsInStock
                Dim iPn2 As Integer = oStock.UnitsInClients

                Select Case iStk
                    Case Is > 0
                        Select Case iStk - iPn2
                            Case Is > 0
                                PaintGradientRowBackGround(DataGridView1, e, Color.LightGreen)
                            Case Else
                                PaintGradientRowBackGround(DataGridView1, e, Color.Yellow)
                        End Select
                    Case Else
                        Select Case oStock.Sku.Obsoleto
                            Case True
                                PaintGradientRowBackGround(DataGridView1, e, Color.LightGray)
                            Case False
                                PaintGradientRowBackGround(DataGridView1, e, Color.LightSalmon)
                        End Select

                End Select
            End If

        End If

    End Sub



    Private Sub PaintGradientRowBackGround(ByVal oGrid As DataGridView, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)
        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke
        'If (e.State And DataGridViewElementStates.Selected) = _
        'DataGridViewElementStates.Selected Then
        'oBgColor = DataGridView1.DefaultCellStyle.SelectionBackColor
        'End If

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            oGrid.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            oGrid.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        'System.Drawing.Drawing2D.LinearGradientBrush(rowBounds, _
        'e.InheritedRowStyle.BackColor, _
        'oColor, _
        'System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTO.DTOStock)
        Dim retval As New List(Of DTO.DTOStock)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_Sku As New Menu_ProductSku(SelectedItems.First.Sku)
            AddHandler oMenu_Sku.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Sku.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add(_MenuItem_Obsolets)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function MenuItem_Obsolets() As ToolStripMenuItem
        Dim retval As New ToolStripMenuItem
        With retval
            .Text = "Inclou obsolets"
            .CheckOnClick = True
        End With
        AddHandler retval.Click, AddressOf LoadControlItems
        Return retval
    End Function

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTO.DTOStock = CurrentControlItem.Source
        Dim oFrm As New Frm_ProductSku(oSelectedValue.Sku)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTO.DTOStock

        Property Id As Integer
        Property Nom As String
        Property UnitsInStock As Integer
        Property UnitsInClients As Integer
        Property UnitsInPot As Integer
        Property UnitsInProveidor As Integer

        Public Sub New(oStock As DTO.DTOStock)
            MyBase.New()
            _Source = oStock
            With oStock
                _Id = .Sku.Id
                _Nom = .Sku.NomCurt
                _UnitsInStock = .UnitsInStock
                _UnitsInClients = .UnitsInClients
                _UnitsInPot = .UnitsInPot
                _UnitsInProveidor = .UnitsInProveidor
            End With
        End Sub

    End Class

    Protected Class ControlItems
        'Inherits SortableBindingList(Of ControlItem)
        Inherits System.ComponentModel.BindingList(Of ControlItem)

    End Class


End Class

