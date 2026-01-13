Public Class Xl_Ctas

    Private _PropertiesSet As Boolean
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPgcCta))
        If Not _PropertiesSet Then SetProperties()
        DataGridView1.DataSource = values
        SetContextMenu()
    End Sub

    Public Property SelectedObject As DTOPgcCta
        Get
            Dim retval As DTOPgcCta = CurrentItem()
            Return retval
        End Get
        Set(value As DTOPgcCta)
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                Dim oItem As PgcCta = oRow.DataBoundItem
                If oItem.Equals(value) Then
                    DataGridView1.CurrentCell = oRow.Cells(0)
                    Exit For
                End If
            Next
        End Set
    End Property

    Private Sub SetProperties()
        With DataGridView1

            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            For i As Integer = Cols.Id To Cols.Nom
                .Columns.Add(New DataGridViewTextBoxColumn)
            Next

            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.Id)
                .DataPropertyName = "Id"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(Cols.Nom)
                .DataPropertyName = "Cat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
        _PropertiesSet = True
        _AllowEvents = True
    End Sub


    Private Function CurrentItem() As DTOPgcCta
        Dim retval As DTOPgcCta = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As DTOPgcCta = CurrentItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu As New Menu_Cta(oControlItem)
            oContextMenu.Items.AddRange(oMenu.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Sortida()
        Dim oEventArgs As New MatEventArgs(CurrentItem)
        RaiseEvent onItemSelected(Me, oEventArgs)
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

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oItem As DTOPgcCta = oRow.DataBoundItem
        'Select Case oItem.Updated
        '    Case True
        'oRow.DefaultCellStyle.BackColor = Color.LightGray
        '    Case Else
        'oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        'End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub



    Public Function WidthAdjustment() As Integer
        Dim oLang As DTOLang = BLLSession.Current.Lang
        Dim oGraphics As Graphics = DataGridView1.CreateGraphics()
        Dim iMaxWidth As Integer
        For Each oItem As DTOPgcCta In DataGridView1.DataSource
            Dim sNom As String = BLL.BLLPgcCta.Nom(oItem, oLang)
            Dim iWidth As Integer = DataGridViewCell.MeasureTextWidth(oGraphics, sNom, DataGridView1.Font, DataGridView1.RowTemplate.Height, TextFormatFlags.Left)
            If iWidth > iMaxWidth Then iMaxWidth = iWidth
        Next

        Dim iOriginalColWidth As Integer = DataGridView1.Columns(Cols.Nom).Width
        Dim retval As Integer = iMaxWidth - iOriginalColWidth
        Return retval
    End Function

    Public Function AdjustedHeight() As Integer
        Dim MaxVisibleRows As Integer = 16
        Dim VisibleRows As Integer = 0
        If DataGridView1.DataSource.Count <= MaxVisibleRows Then
            VisibleRows = DataGridView1.DataSource.Count
        Else
            VisibleRows = MaxVisibleRows
        End If

        Dim retval As Integer = DataGridView1.RowTemplate.Height * VisibleRows + 3
        Return retval
    End Function

End Class

