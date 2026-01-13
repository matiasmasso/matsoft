Public Class Xl_PlatformAlbsSumary
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event SelectionChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Check
        Plataforma
        Albarans
        Import
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCustomerPlatform))
        _ControlItems = New ControlItems
        For Each oItem As DTOCustomerPlatform In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property ItemsChecked As List(Of DTOCustomerPlatform)
        Get
            Dim retval As New List(Of DTOCustomerPlatform)
            Dim oBindingSource As BindingSource = DataGridView1.DataSource
            Dim oControlItems As ControlItems = oBindingSource.DataSource
            For Each oControlItem As ControlItem In oControlItems
                If oControlItem.Checked Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property Items As List(Of DTOCustomerPlatform)
        Get
            Dim retval As New List(Of DTOCustomerPlatform)
            Dim oBindingSource As BindingSource = DataGridView1.DataSource
            Dim oControlItems As ControlItems = oBindingSource.DataSource
            For Each oControlItem As ControlItem In oControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

    Public Function SelectedItem() As DTOCustomerPlatform
        Dim retval As DTOCustomerPlatform = Nothing
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
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub LoadGrid()
        _AllowEvents = False

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .ReadOnly = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .RowHeadersVisible = False
            .ColumnHeadersVisible = True
            .AutoGenerateColumns = False
            .Columns.Clear()

            Dim oBindingSource As New BindingSource
            oBindingSource.AllowNew = True
            oBindingSource.DataSource = _ControlItems

            .DataSource = oBindingSource


            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Check), DataGridViewImageColumn)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Plataforma)
                .DataPropertyName = "Plataforma"
                .HeaderText = "Plataforma"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Albarans)
                .DataPropertyName = "Albarans"
                .HeaderText = "Albarans"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Import)
                .DataPropertyName = "Import"
                .HeaderText = "Import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

        End With

        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                oControlItem.Checked = Not oControlItem.Checked
                DataGridView1.Refresh()
                RaiseEvent AfterUpdate(Me, New MatEventArgs(oControlItem.Source))

        End Select
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oControlItem As ControlItem = DataGridView1.Rows(e.RowIndex).DataBoundItem
                'If oControlItem IsNot Nothing Then
                If oControlItem.Checked = True Then
                    e.Value = My.Resources.Checked13
                Else
                    e.Value = My.Resources.UnChecked13
                End If
                'End If
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent SelectionChanged(Me, New MatEventArgs(CurrentItem.Source))
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Public Property Source As DTOCustomerPlatform
        Public Property Checked As Boolean
        Public Property Plataforma As String
        Public Property Albarans As Integer
        Public Property Import As Decimal

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(oPlatformDestination As DTOCustomerPlatform)
            MyBase.New()
            _Source = oPlatformDestination

            Dim sNom = oPlatformDestination.nom.Replace("EL CORTE INGLES ", "")
            If sNom.StartsWith(Chr(34)) Then
                sNom = sNom.Replace(Chr(34), "")
            ElseIf sNom.StartsWith("(") Then
                sNom = sNom.Substring(1)
                Dim iPos = sNom.IndexOf(")")
                If iPos >= 0 Then
                    sNom = sNom.Substring(0, iPos) & sNom.Substring(iPos + 1)
                End If
            ElseIf sNom.StartsWith("[") Then
                sNom = sNom.Substring(1)
                Dim iPos = sNom.IndexOf("]")
                If iPos >= 0 Then
                    sNom = sNom.Substring(0, iPos) & sNom.Substring(iPos + 1)
                End If
            End If


            With oPlatformDestination
                _Checked = True
                _Plataforma = sNom
                _Albarans = .Deliveries.Count
                _Import = DTODelivery.BaseImponible(.Deliveries).eur
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
