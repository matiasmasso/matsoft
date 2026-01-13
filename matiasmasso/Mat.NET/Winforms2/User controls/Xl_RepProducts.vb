Public Class Xl_RepProducts
    Private _Mode As Modes
    Private _RowIdx As Integer
    Private _FirstDisplayedScrollingRowIndex As Integer

    Private _values As List(Of DTORepProduct)
    Private _ControlItems As ControlItems
    Private _ObsoletsHidden As Boolean = True
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Enum Modes
        FullMode
        ByRep
        ByProduct
        ByArea
        ByChannel
    End Enum

    Private Enum Cols
        Ico
        Rep
        Product
        Area
        Channel
        FchFrom
        FchTo
        ComStd
    End Enum

    Public Shadows Sub Load(values As List(Of DTORepProduct), oMode As Modes)
        _Mode = oMode
        _values = values
        _ControlItems = New ControlItems
        LoadControlItems()
        LoadGrid()
    End Sub

    Private Sub LoadControlItems()
        _ControlItems = New ControlItems
        For Each oItem As DTORepProduct In _values
            If DTORepProduct.IsActive(oItem) Or Not _ObsoletsHidden Then
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            End If
        Next
    End Sub

    Public ReadOnly Property SelectedValues As List(Of DTORepProduct)
        Get
            Dim retval As New List(Of DTORepProduct)
            For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

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

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Ico), DataGridViewImageColumn)
                .DataPropertyName = "Ico"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Rep)
                .DataPropertyName = "Rep"
                .HeaderText = "Rep"
                .Visible = _Mode <> Modes.ByRep
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Product)
                .DataPropertyName = "Product"
                .HeaderText = "Producte"
                .Visible = _Mode <> Modes.ByProduct
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Area)
                .DataPropertyName = "Area"
                .HeaderText = "Zona"
                .Visible = _Mode <> Modes.ByArea
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Channel)
                .DataPropertyName = "Channel"
                .HeaderText = "Canal"
                .Visible = _Mode <> Modes.ByChannel
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FchFrom)
                .DataPropertyName = "FchFrom"
                .HeaderText = "des de"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            If Not _ObsoletsHidden Then
                .Columns.Add(New DataGridViewTextBoxColumn)
                With .Columns(Cols.FchTo)
                    .DataPropertyName = "FchTo"
                    .HeaderText = "fins a"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 70
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.Format = "dd/MM/yy"
                End With
            End If

            If .Rows.Count > 0 Then
                If _ControlItems.Count > _RowIdx Then
                    Dim oPreviousRow As DataGridViewRow = .Rows(_RowIdx)
                    Dim oPreviousRowVisibleCell As DataGridViewCell = oPreviousRow.Cells(Cols.FchFrom)
                    .CurrentCell = oPreviousRowVisibleCell
                    .FirstDisplayedScrollingRowIndex = _FirstDisplayedScrollingRowIndex
                Else
                    .CurrentCell = .Rows(.Rows.Count - 1).Cells(.FirstDisplayedScrollingColumnIndex)
                End If
            End If
        End With


        SetContextMenu()
        _AllowEvents = True
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

    Private Function SelectedItems() As List(Of DTORepProduct)
        Dim retval As New List(Of DTORepProduct)
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
            Dim oMenu_RepProduct As New Menu_RepProduct(SelectedItems)
            AddHandler oMenu_RepProduct.AfterUpdate, AddressOf refreshRequest
            oContextMenu.Items.AddRange(oMenu_RepProduct.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim oMenuItemObsolets As New ToolStripMenuItem
        With oMenuItemObsolets
            .Text = "oculta obsolets"
            .Checked = _ObsoletsHidden
            AddHandler oMenuItemObsolets.Click, AddressOf Do_ToggleObsolets
        End With
        oContextMenu.Items.Add(oMenuItemObsolets)

        oContextMenu.Items.Add("excel", Nothing, AddressOf Do_Excel)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf refreshrequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub refreshrequest()
        _RowIdx = DataGridView1.CurrentRow.Index
        _FirstDisplayedScrollingRowIndex = DataGridView1.FirstDisplayedScrollingRowIndex
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_ToggleObsolets()
        _ObsoletsHidden = Not _ObsoletsHidden
        If DataGridView1.CurrentRow IsNot Nothing Then
            _RowIdx = DataGridView1.CurrentRow.Index
            _FirstDisplayedScrollingRowIndex = DataGridView1.FirstDisplayedScrollingRowIndex
        End If
        LoadControlItems()
        LoadGrid()
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub
    Private Sub Do_Excel()
        Dim oSheet = DTORepProduct.Excel(_values)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oRepProduct As DTORepProduct = oControlItem.Source
        If oRepProduct.FchTo <> Nothing And oRepProduct.FchTo < DTO.GlobalVariables.Today() Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        ElseIf oRepProduct.FchFrom > DTO.GlobalVariables.Today() Then
            oRow.DefaultCellStyle.BackColor = Color.LightYellow
        Else
            oRow.DefaultCellStyle.BackColor = Color.White
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Public Property Source As DTORepProduct

        Public Property Ico As Image
        Public Property Rep As String
        Public Property Product As String
        Public Property Area As String
        Public Property Channel As String
        Public Property FchFrom As Date
        Public Property FchTo As Nullable(Of Date)

        Public Sub New(oRepProduct As DTORepProduct)
            MyBase.New()
            _Source = oRepProduct

            With oRepProduct
                Select Case _Source.Cod
                    Case DTORepProduct.Cods.Included
                        _Ico = My.Resources.vb
                    Case DTORepProduct.Cods.Excluded
                        _Ico = My.Resources.del
                End Select
                _Rep = .Rep.NickName
                _Product = DTOProduct.GetNom(.Product)
                _Area = DTOArea.nomOrDefault(.area)
                _Channel = .distributionChannel.langText.Tradueix(Current.Session.Lang)
                _FchFrom = .fchFrom
                If .FchTo <> Nothing Then
                    _FchTo = .FchTo
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class