Public Class Xl_PrInsercions

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterInsercioDropped(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Revista
        Anunci
        Tarifa
        Net
        FraIco
    End Enum

    Public Shadows Sub Load(oPrInsercions As PrInsercions)
        _ControlItems = New ControlItems
        For Each oItem As PrInsercio In oPrInsercions
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
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
            With .Columns(Cols.Revista)
                .HeaderText = "Revista"
                .DataPropertyName = "Revista"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Anunci)
                .HeaderText = "Anunci"
                .DataPropertyName = "Anunci"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Tarifa)
                .HeaderText = "Tarifat"
                .DataPropertyName = "Tarifa"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Net)
                .HeaderText = "Cost net"
                .DataPropertyName = "Net"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.FraIco), DataGridViewImageColumn)
                .HeaderText = "factura"
                .DataPropertyName = "FraIco"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.NullValue = Nothing
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

    Private Function SelectedItems() As PrInsercions
        Dim retval As New PrInsercions
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval.Add(oControlItem.Source)
            End If
        End If
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
            Dim oMenu_PrInsercio As New Menu_PrInsercio(SelectedItems)
            AddHandler oMenu_PrInsercio.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PrInsercio.Range)
        End If

        Dim oMenuItem As New ToolStripMenuItem("afegir inserció", My.Resources.clip, AddressOf AddNew)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub AddNew(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oPrInsercio As PrInsercio = CurrentControlItem.Source
        Dim oFrm As New Frm_PrInsercio(oPrInsercio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Dim oDragDropReult As DragDropEffects
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim oSelectedItems As PrInsercions = SelectedItems()
            If oSelectedItems.Count = 0 Then
                SetContextMenu()
            Else
                Dim oInsercio As PrInsercio = oSelectedItems.First
                If oInsercio IsNot Nothing Then
                    oDragDropReult = DataGridView1.DoDragDrop(oInsercio, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        If (e.Data.GetDataPresent(GetType(PrInsercio))) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
            'PictureBox1.BackColor = Color.SeaShell
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragOver
        Dim oPoint = DataGridView1.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridView1.CurrentCell = oclickedCell
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        Dim oPoint = DataGridView1.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            'Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            'DataGridView1.CurrentCell = oclickedCell
        ElseIf hit.Type = DataGridViewHitTestType.None Then
            Dim oInsercio As PrInsercio = e.Data.GetData(GetType(PrInsercio))
            RaiseEvent AfterInsercioDropped(oInsercio, EventArgs.Empty)
        End If
        RefreshRequest(sender, e)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub
    Protected Class ControlItem
        Public Property Source As PrInsercio

        Public Property Revista As String
        Public Property Anunci As String
        Public Property Tarifa As Decimal
        Public Property Net As Decimal
        Public Property FraIco As Image

        Public Sub New(oPrInsercio As PrInsercio)
            MyBase.New()
            _Source = oPrInsercio
            With oPrInsercio
                _Revista = .FullText
                If .Document IsNot Nothing Then
                    If .Document.Ad IsNot Nothing Then
                        _Anunci = .Document.FullText
                    End If
                End If
                If .Tarifa IsNot Nothing Then
                    _Tarifa = .Tarifa.Eur
                End If
                If .Cost IsNot Nothing Then
                    _Net = .Cost.Eur
                End If
                _FraIco = IIf(.Cca Is Nothing, Nothing, My.Resources.pdf)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
