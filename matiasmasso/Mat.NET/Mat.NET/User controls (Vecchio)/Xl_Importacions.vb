Public Class Xl_Importacions
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Fch
        Amt
        TrpCost
        Bultos
        Kg
        M3
        IncoTerms
        CodiMercancia
        PaisOrigen
        Intrastat
        Prv
    End Enum

    Public Shadows Sub Load(value As List(Of Importacio))
        _ControlItems = New ControlItems
        For Each oItem As Importacio In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
        SetContextMenu()
    End Sub

    Public ReadOnly Property SelectedValues As List(Of Importacio)
        Get
            Dim retval As New List(Of Importacio)
            For Each oItem As ControlItem In _ControlItems
                retval.Add(oItem.Source)
            Next
            Return retval
        End Get
    End Property

    Private Sub LoadGrid()
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Id

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = True
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Id)
                .DataPropertyName = "Id"
                .HeaderText = "Num"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .DataPropertyName = "Fch"
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Amt)
                .DataPropertyName = "Amt"
                .HeaderText = "Valor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.TrpCost)
                .DataPropertyName = "TrpCost"
                .HeaderText = "Transport"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Bultos)
                .DataPropertyName = "Bultos"
                .HeaderText = "Bultos"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Kg)
                .DataPropertyName = "Kg"
                .HeaderText = "Pes brut"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 Kg;-#,##0.00 Kg;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.M3)
                .DataPropertyName = "M3"
                .HeaderText = "Volum"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 m3;-#,##0.00 m3;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.IncoTerms)
                .DataPropertyName = "IncoTerms"
                .HeaderText = "ICT"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.CodiMercancia)
                .DataPropertyName = "CodiMercancia"
                .HeaderText = "Mercancia"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.PaisOrigen)
                .DataPropertyName = "PaisOrigen"
                .HeaderText = "Origen"
                .DefaultCellStyle.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Intrastat)
                .DataPropertyName = "Intrastat"
                .HeaderText = "Intrastat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Prv)
                .DataPropertyName = "Prv"
                .HeaderText = "Proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            If .CurrentRow Is Nothing Then
            Else
                .FirstDisplayedScrollingRowIndex() = iFirstRow

                If i > .Rows.Count - 1 Then
                    .CurrentCell = .Rows(.Rows.Count - 1).Cells(j)
                Else
                    .CurrentCell = .Rows(i).Cells(j)
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

    Private Function SelectedItems() As Importacions
        Dim retval As New Importacions
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
            Dim oMenu_Importacio As New Menu_Importacio(SelectedItems(0))
            AddHandler oMenu_Importacio.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Importacio.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.PaisOrigen
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.PaisOrigen = "CN" Then
                    e.CellStyle.BackColor = Color.LightGray
                    Dim oCellIntrastat As DataGridViewCell = oRow.Cells(Cols.Intrastat)
                    oCellIntrastat.Style.BackColor = Color.LightGray
                End If
            Case Cols.TrpCost
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim DcDivisa As Decimal = oControlItem.Source.Amt.Eur
                If e.Value <> 0 And DcDivisa <> 0 Then
                    Dim DcTransport As Decimal = e.Value
                    Dim DcRate As Decimal = DcTransport / DcDivisa
                    If DcRate > (5 / 100) Then
                        e.CellStyle.BackColor = Color.LightSalmon
                    End If
                End If

        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oImportacio As Importacio = oControlItem.Source
        ImportacioLoader.Load(oImportacio)
        Dim oFrm As New Frm_Importacio(oImportacio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToRefresh(Me, e)
    End Sub


#Region "DragDrop"

    Private mLastMouseDownRectangle As System.Drawing.Rectangle

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
            'PictureBox1.BackColor = Color.SeaShell
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            'PictureBox1.BackColor = Color.LemonChiffon
            '    or none of the above
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
        Dim oDocFiles As New List(Of DTODocFile)
        Dim exs as New List(Of exception)
        Dim oTargetCell As DataGridViewCell = Nothing
        If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
            Dim oImportacio As Importacio = CurrentControlItem.Source
            Dim oDocFile As DTODocFile = oDocFiles.First
            Dim oFrm As New Frm_ImportacioItem(oImportacio, oDocFile)
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                mLastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If Not mLastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(mLastMouseDownRectangle.X, mLastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    DataGridView1.CurrentCell = sender.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                    Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
                    Dim oImportacio As Importacio = CurrentControlItem.Source
                    sender.DoDragDrop(oImportacio, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub

#End Region



    Protected Class ControlItem
        Public Property Source As Importacio

        Public Property Yea As Integer
        Public Property Id As Integer
        Public Property Fch As Date
        Public Property Amt As String
        Public Property TrpCost As Decimal
        Public Property Bultos As Integer
        Public Property Kg As Decimal
        Public Property M3 As Decimal
        Public Property IncoTerms As String
        Public Property CodiMercancia As String
        Public Property PaisOrigen As String
        Public Property Intrastat As String
        Public Property Prv As String

        Public Sub New(oImportacio As Importacio)
            MyBase.New()
            _Source = oImportacio

            With oImportacio
                _Yea = .Yea
                _Id = .Id
                _Fch = .Fch
                If .Amt.Eur <> 0 Then
                    _Amt = .Amt.CurFormat
                End If
                _TrpCost = .TrpCost.Eur
                _Bultos = .Bultos
                _Kg = .Kg
                _M3 = .M3
                _IncoTerms = .IncoTerm.Id
                _CodiMercancia = .CodiMercancia.Id
                _PaisOrigen = .CountryOrigen.ISO
                _Prv = .Proveidor.Nom

                Dim s As String = ""
                For Each oItm As ImportacioItem In .Items
                    If oItm.Intrastat IsNot Nothing Then
                        s = oItm.Intrastat.ToString
                        Exit For
                    End If
                Next
                _Intrastat = s
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class


End Class
