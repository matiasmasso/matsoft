Imports DTO.Integracions

Public Class Xl_CarrefourLogisticLabels
    Inherits TabStopDataGridView

    Private _Values As List(Of DTOCarrefourItem)
    Private _DefaultValue As DTOCarrefourItem
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        alb
        lin
        implantation
        MasterBarCode
        SkuCustomRef
        SkuDsc
        SkuColor
        Width
        Height
        Length
        UnitsPerMasterBox
        UnitsPerInnerBox
        madeIn
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCarrefourItem), Optional oDefaultValue As DTOCarrefourItem = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Public Function Values() As List(Of DTOCarrefourItem)
        For Each oControlItem As ControlItem In _ControlItems
            Dim value As DTOCarrefourItem = _Values.Find(Function(x) x.Albaran = oControlItem.alb And x.Linea = oControlItem.lin)
            With value
                .MasterBarCode = oControlItem.MasterBarCode
                .Implantation = oControlItem.implantation
                .MadeIn = oControlItem.madeIn
                .SkuDsc = oControlItem.SkuDsc
                .SkuColor = oControlItem.SkuColor
                .SkuCustomRef = oControlItem.SkuCustomRef
            End With
        Next
        Return _Values
    End Function

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOCarrefourItem) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOCarrefourItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        Dim oBindingSource As New BindingSource
        oBindingSource.AllowNew = True
        oBindingSource.DataSource = _ControlItems
        MyBase.DataSource = oBindingSource
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOCarrefourItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCarrefourItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowCarrefourItem.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New _Xl_ReadOnlyDatagridview.DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.alb)
            .HeaderText = "Albarà"
            .DataPropertyName = "Alb"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.lin)
            .HeaderText = "Linia"
            .DataPropertyName = "Lin"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.implantation)
            .HeaderText = "Implantació"
            .DataPropertyName = "Implantation"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.MasterBarCode)
            .HeaderText = "MasterBarCode"
            .DataPropertyName = "MasterBarCode"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SkuCustomRef)
            .HeaderText = "Ref"
            .DataPropertyName = "SkuCustomRef"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SkuDsc)
            .HeaderText = "Descripció"
            .DataPropertyName = "SkuDsc"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SkuColor)
            .HeaderText = "Color"
            .DataPropertyName = "SkuColor"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Width)
            .HeaderText = "Ample"
            .DataPropertyName = "Width"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Height)
            .HeaderText = "Alt"
            .DataPropertyName = "Height"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Length)
            .HeaderText = "Llarg"
            .DataPropertyName = "Length"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.UnitsPerMasterBox)
            .HeaderText = "Master box"
            .DataPropertyName = "UnitsPerMasterBox"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.UnitsPerInnerBox)
            .HeaderText = "Inner box"
            .DataPropertyName = "UnitsPerInnerBox"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.madeIn)
            .HeaderText = "Made In"
            .DataPropertyName = "madeIn"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
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

    Private Function SelectedItems() As List(Of DTOCarrefourItem)
        Dim retval As New List(Of DTOCarrefourItem)
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
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            'Dim oMenu_CarrefourItem As New Menu_CarrefourItem(SelectedItems.First)
            'AddHandler oMenu_CarrefourItem.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_CarrefourItem.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOCarrefourItem = CurrentControlItem.Source
            'Dim oFrm As New Frm_CarrefourItem(oSelectedValue)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            ' oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oCarrefourItem As DTOCarrefourItem = oControlItem.Source

                '                If oDocFile IsNot Nothing Then
                '                e.Value = iconHelper.GetIconFromMimeCod(oDocFile.Mime)
                '                End If
        End Select
    End Sub


    Protected Class ControlItem
        Property Source As DTOCarrefourItem

        Property alb As Integer
        Property lin As Integer
        Property implantation As String
        Property MasterBarCode As String
        Property SkuCustomRef As String
        Property SkuDsc As String
        Property SkuColor As String
        Property Width As Integer
        Property Height As Integer
        Property Length As Integer
        Property UnitsPerMasterBox As Integer
        Property UnitsPerInnerBox As Integer
        Property madeIn As String


        Public Sub New(value As DTOCarrefourItem)
            MyBase.New()
            _Source = value
            With value
                _alb = .Albaran
                _lin = .Linea
                _implantation = .Implantation
                _MasterBarCode = .MasterBarCode
                _SkuCustomRef = .SkuCustomRef
                _SkuDsc = .SkuDsc
                _SkuColor = .SkuColor
                _Width = .Width
                _Height = .Height
                _Length = .Length
                _UnitsPerMasterBox = .UnitsPerMasterBox
                _UnitsPerInnerBox = .UnitsPerInnerBox
                _madeIn = .MadeIn
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


