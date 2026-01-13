Public Class Xl_ArtWiths
    Private _DataSource As ArtWiths
    Private _AllowEvents As Boolean
    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Private Enum Cols
        Qty
        Nom
    End Enum

    Public Shadows Sub Load(oDataSource As ArtWiths, Optional oDefaultArtWith As ArtWith = Nothing)
        _DataSource = oDataSource
        LoadGrid()
    End Sub

    Public ReadOnly Property Items As ArtWiths
        Get
            Dim retval As New ArtWiths
            Dim oBindingSource As BindingSource = DataGridView1.DataSource
            Dim oControlItems As ControlItems = oBindingSource.DataSource
            For Each oControlItem As ControlItem In oControlItems
                If oControlItem.Qty > 0 Then
                    Dim oArtWith As ArtWith = oControlItem.Source
                    oArtWith.Qty = oControlItem.Qty
                    retval.Add(oArtWith)
                End If
            Next
            Return retval
        End Get
    End Property

    Public Function SelectedItem() As ArtWith
        Dim retval As ArtWith = Nothing
        Dim oControlItem As ControlItem = CurrentItem()
        If oControlItem IsNot Nothing Then
            retval = oControlItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        'Dim oMenuItem As ToolStripMenuItem = _IncludeObsoletsMenuItem
        'oContextMenu.Items.Add(oMenuItem)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow Is Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub LoadGrid()
        _AllowEvents = False

        Dim oControlItems As New ControlItems
        For Each oArtWith As ArtWith In _DataSource
            Dim oControlItem As New ControlItem(oArtWith)
            oControlItems.Add(oControlItem)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .ReadOnly = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .RowHeadersVisible = True
            .ColumnHeadersVisible = True
            .AutoGenerateColumns = False
            .Columns.Clear()

            Dim oBindingSource As New BindingSource
            oBindingSource.AllowNew = True
            oBindingSource.DataSource = oControlItems
            .DataSource = oBindingSource


            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Qty)
                .DataPropertyName = "Qty"
                .HeaderText = "Unitats"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .DataPropertyName = "Nom"
                .HeaderText = "Producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With

        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView1_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Nom
                    _AllowEvents = False
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim oItem As ControlItem = oRow.DataBoundItem
                    Dim sKey As String = oRow.Cells(Cols.Nom).Value

                    Dim oSku As DTOProductSku = Finder.FindSku(sKey, BLL.BLLApp.Mgz)
                    If oSku Is Nothing Then
                        oRow.Cells(Cols.Nom).Value = "(no s'ha trobat cap producte per '" & sKey & "')"
                        oItem.Source = Nothing
                    Else
                        Dim oArt As New Art(oSku.Guid)
                        Dim oArtWith As New ArtWith()
                        oArtWith.Child = oArt
                        oItem.Source = oArtWith
                        oItem.Nom = oArt.Nom_ESP
                    End If

                    RaiseEvent AfterUpdate(oItem, EventArgs.Empty)
                    _AllowEvents = True
                    SetContextMenu()
            End Select
        End If
    End Sub


    Protected Class ControlItem
        Public Property Source As ArtWith
        Public Property Qty As Integer
        Public Property Nom As String

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(oArtWith As ArtWith)
            MyBase.New()
            _Source = oArtWith
            With oArtWith
                _Qty = .Qty
                _Nom = .Child.Nom_ESP
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
