Public Class Xl_Banners
    Private _Values As List(Of DTOBanner)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Image
        Txt
    End Enum

    Public Shadows Sub Load(values As List(Of DTOBanner))
        _Values = values
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOBanner) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOBanner In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(DataGridView1)
        DataGridView1.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(DataGridView1, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOBanner
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOBanner = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub SetProperties()
        With DataGridView1
            With .RowTemplate
                .Height = 48
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Image), DataGridViewImageColumn)
                .DataPropertyName = "Image"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 105
                .DefaultCellStyle.NullValue = Nothing
                .ImageLayout = DataGridViewImageCellLayout.Zoom
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Txt)
                .DataPropertyName = "Txt"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.WrapMode = DataGridViewTriState.True
            End With
        End With
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

    Private Function SelectedItems() As List(Of DTOBanner)
        Dim retval As New List(Of DTOBanner)
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
            Dim oMenu_Banner As New Menu_Banner(SelectedItems.First)
            AddHandler oMenu_Banner.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Banner.Range)
        End If

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOBanner = CurrentControlItem.Source
        Dim oFrm As New Frm_Banner(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oBanner = oControlItem.Source

        If Not oBanner.isActive Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Public Property Source As DTOBanner

        Public Property Image As System.Drawing.Image
        Public Property Txt As String

        Public Sub New(oBanner As DTOBanner)
            MyBase.New()
            _Source = oBanner
            With oBanner
                _Image = LegacyHelper.ImageHelper.Converter(.Thumbnail)
                _Txt = MultilineText(oBanner)
            End With
        End Sub

        Private Function MultilineText(oBanner As DTOBanner) As String
            Dim sb As New Text.StringBuilder
            If oBanner.FchFrom <> Nothing Then
                sb.Append(oBanner.FchFrom.ToShortDateString)
            End If
            If oBanner.FchTo <> Nothing Then
                sb.Append(" - " & oBanner.FchTo.ToShortDateString)
            End If
            If sb.Length > 0 Then sb.AppendLine()

            sb.AppendLine(oBanner.Nom)

            If oBanner.Lang IsNot Nothing Then
                sb.AppendLine(oBanner.Lang.Tag)
            End If
            Return sb.ToString
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

