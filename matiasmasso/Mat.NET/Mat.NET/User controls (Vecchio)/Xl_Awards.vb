Public Class Xl_Awards
    Private _ControlItems As ControlItems
    Private _Mode As Modes
    Private _AllowEvents As Boolean

    Public Enum Modes
        NotSet
        FromProduct
        FromOrganisation
    End Enum

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Organisation
        Product
        Year
        Caduca
    End Enum

    Public Shadows Sub Load(value As List(Of Award), oMode As Modes)
        _Mode = oMode
        _ControlItems = New ControlItems
        For Each oItem As Award In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As Award
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As Award = oControlItem.Source
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

            If _Mode <> Modes.FromOrganisation Then
                .Columns.Add(New DataGridViewTextBoxColumn)
                With .Columns(Cols.Organisation)
                    .HeaderText = "Organització"
                    .DataPropertyName = "Organisation"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
            End If

            If _Mode <> Modes.FromProduct Then
                .Columns.Add(New DataGridViewTextBoxColumn)
                With .Columns(Cols.Product)
                    .HeaderText = "Producte"
                    .DataPropertyName = "Product"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
            End If

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Year)
                .HeaderText = "Any"
                .DataPropertyName = "Year"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Caduca)
                .HeaderText = "Caducitat"
                .DataPropertyName = "Caduca"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
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

    Private Function SelectedItems() As List(Of Award)
        Dim retval As New List(Of Award)
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
            Dim oMenu_Award As New Menu_Award(SelectedItems.First)
            AddHandler oMenu_Award.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Award.Range)
        End If

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As Award = CurrentControlItem.Source
        Dim oFrm As New Frm_Award(oSelectedValue)
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

    End Sub

    Protected Class ControlItem
        Public Property Source As Award

        Property Organisation As String
        Property Product As String
        Property Year As Integer
        Property Caduca As Date

        Public Sub New(oAward As Award)
            MyBase.New()
            _Source = oAward
            With oAward
                _Organisation = .Organisation.Clx
                _Product = BLL.BLLProduct.Nom(.Product)
                _Year = .Year
                _Caduca = .Caduca
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class
