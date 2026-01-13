Public Class Xl_Skus_Selection
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Private _ForSelectionOnly As Boolean = True

    Public Event onItemSelected(sender As Object, e As MatEventArgs)


    Private Enum Cols
        Sku
        Nom
    End Enum

    Public Shadows Sub Load(value As List(Of DTOProductSku))
        _ControlItems = New ControlItems
        For Each oSku As DTOProductSku In value
            Dim oItem As New ControlItem(oSku)
            _ControlItems.Add(oItem)
        Next

        LoadGrid()
    End Sub


    Public Function CurrentItem() As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oItem As ControlItem = oRow.DataBoundItem
            retval = oItem.Source
        End If
        Return retval
    End Function


    Private Sub Sortida()
        Dim oEventArgs As MatEventArgs = Nothing
        oEventArgs = New MatEventArgs(CurrentItem)
        RaiseEvent onItemSelected(Me, oEventArgs)
    End Sub


#Region "Grid"

    Private Sub LoadGrid()
        With DataGridView1

            .AutoGenerateColumns = False
            .Columns.Clear()

            .Columns.Add(New DataGridViewTextBoxColumn)
            .Columns.Add(New DataGridViewTextBoxColumn)

            .DataSource = _ControlItems
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            '.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True


            With .Columns(Cols.Sku)
                .DataPropertyName = "Sku"
                .Width = 42
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        DataGridView1.ClearSelection()
        _AllowEvents = True
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Sortida()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                Sortida()
                e.Handled = True
        End Select
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Nom
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oItem As ControlItem = oRow.DataBoundItem
                e.CellStyle.BackColor = LegacyHelper.ImageHelper.Converter(DTOProductSku.BackColor(oItem.Source))
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            DataGridView1.ClearSelection()
        End If
    End Sub

#End Region

#Region "Sizes"

    Public Function WidthAdjustment() As Integer
        Dim oGraphics As Graphics = DataGridView1.CreateGraphics()
        Dim iMaxWidth As Integer
        For Each oItem As ControlItem In _ControlItems
            Dim iWidth As Integer = DataGridViewCell.MeasureTextWidth(oGraphics, oItem.Nom, DataGridView1.Font, DataGridView1.RowTemplate.Height, TextFormatFlags.Left)
            If iWidth > iMaxWidth Then iMaxWidth = iWidth
        Next

        Dim iOriginalColWidth As Integer = DataGridView1.Columns(Cols.Nom).Width
        Dim retval As Integer = iMaxWidth - iOriginalColWidth
        Return retval
    End Function

    Public Function AdjustedHeight() As Integer
        Dim MaxVisibleRows As Integer = 16
        Dim VisibleRows As Integer = 0
        If _ControlItems.Count <= MaxVisibleRows Then
            VisibleRows = _ControlItems.Count
        Else
            VisibleRows = MaxVisibleRows
        End If

        Dim retval As Integer = DataGridView1.RowTemplate.Height * VisibleRows + 3
        Return retval
    End Function
#End Region

#Region "ControlItem"

    Protected Class ControlItem
        Public Property Source As DTOProductSku
        Public Property Sku As Integer
        Public Property Nom As String
        Public Property BackColor As Color

        Public Sub New(oSku As DTOProductSku)
            MyBase.New()
            _Source = oSku
            With _Source
                _Sku = .id
                _Nom = .nomLlarg.Tradueix(Current.Session.Lang)
            End With
        End Sub


    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


#End Region


End Class