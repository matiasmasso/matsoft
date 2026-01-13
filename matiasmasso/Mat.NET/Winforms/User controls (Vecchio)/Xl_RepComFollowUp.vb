Public Class Xl_RepComFollowUp
    Private _ControlItems As RepComFollowUps
    Private _AllowEvents As Boolean

    Private Enum Cols
        Pdc
        Fch
        Clx
        Demanat
        Servit
        Facturat
        Liquidat
    End Enum

    Public WriteOnly Property Rep As Rep
        Set(value As Rep)
            _ControlItems = RepComFollowUps.FromRep(value)
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        With DataGridView1
            .AutoGenerateColumns = False
            .Columns.Clear()
            For i As Integer = Cols.Pdc To Cols.Liquidat
                .Columns.Add(New DataGridViewTextBoxColumn)
            Next

            .DataSource = _ControlItems
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.Pdc)
                .HeaderText = "Comanda"
                .DataPropertyName = "Pdc"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .ReadOnly = True
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "Client"
                .DataPropertyName = "Clx"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Demanat)
                .ReadOnly = True
                .HeaderText = "Demanat"
                .DataPropertyName = "Demanat"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Servit)
                .ReadOnly = True
                .HeaderText = "Servit"
                .DataPropertyName = "Servit"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Facturat)
                .ReadOnly = True
                .HeaderText = "Facturat"
                .DataPropertyName = "Facturat"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Liquidat)
                .ReadOnly = True
                .HeaderText = "Liquidat"
                .DataPropertyName = "Liquidat"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function CurrentItem() As RepComFollowUp
        Dim retval As RepComFollowUp = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As RepComFollowUp = CurrentItem()

        If oControlItem IsNot Nothing Then
            Select Case DataGridView1.CurrentCell.ColumnIndex
                Case Cols.Pdc
                    Dim oMenu As New Menu_Pdc(CType(oControlItem.Source, Pdc))
                    oContextMenu.Items.AddRange(oMenu.Range)
                Case Cols.Servit

            End Select

        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

End Class
