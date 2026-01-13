Public Class Xl_AeatMod347_Items_vecchio
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Private COLOR_DISABLED As System.Drawing.Color = Color.FromArgb(210, 210, 210)

    Private Enum Cols
        Nif
        RaoSocial
        Total
        T1
        T2
        T3
        T4
    End Enum

    Public Shadows Sub Load(value As List(Of AeatMod347_Item))
        _ControlItems = New ControlItems
        For Each oItem As AeatMod347_Item In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As AeatMod347_Item
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As AeatMod347_Item = oControlItem.Source
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

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nif)
                .HeaderText = "NIF"
                .DataPropertyName = "NIF"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.RaoSocial)
                .HeaderText = "Rao Social"
                .DataPropertyName = "RaoSocial"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Total)
                .HeaderText = "Total"
                .DataPropertyName = "Total"
                .Width = 70
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.T1)
                .HeaderText = "T1"
                .DataPropertyName = "T1"
                .Width = 70
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.T2)
                .HeaderText = "T2"
                .DataPropertyName = "T2"
                .Width = 70
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.T3)
                .HeaderText = "T3"
                .DataPropertyName = "T3"
                .Width = 70
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.T4)
                .HeaderText = "T4"
                .DataPropertyName = "T4"
                .Width = 70
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
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

    Private Function SelectedItems() As AeatMod347_Items
        Dim retval As New AeatMod347_Items
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
        Dim oMenuItem As ToolStripMenuItem
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oAeatMod347_Item As AeatMod347_Item = oControlItem.Source
            Dim oContact As Contact = oAeatMod347_Item.Contact

            If oContact IsNot Nothing Then
                oMenuItem = New ToolStripMenuItem("veure detall", Nothing, AddressOf Do_Detall)
                oContextMenu.Items.Add(oMenuItem)

                oMenuItem = New ToolStripMenuItem("client...")
                oContextMenu.Items.Add(oMenuItem)

                oContextMenu.Items.Add("-")
            End If
        End If


        DataGridView1.ContextMenuStrip = oContextMenu

    End Sub



    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Select Case oControlItem.Source.Declarable()
            Case True
                oRow.DefaultCellStyle.BackColor = Color.White
            Case False
                oRow.DefaultCellStyle.BackColor = COLOR_DISABLED
        End Select

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_Detall(sender As Object, e As System.EventArgs)
        Dim oItem As ControlItem = CurrentControlItem()
        Dim oFrm As New Frm_Fiscal_Mod347_2012_detall(oItem.Source.Contact, oItem.Source.Parent.Exercici.Yea, oItem.Source.ClauOp)
        oFrm.Show()
    End Sub

    Protected Class ControlItem
        Public Property Source As AeatMod347_Item

        Public Property Nif As String
        Public Property RaoSocial As String
        Public Property Total As Decimal
        Public Property T1 As Decimal
        Public Property T2 As Decimal
        Public Property T3 As Decimal
        Public Property T4 As Decimal

        Public Sub New(oAeatMod347_Item As AeatMod347_Item)
            MyBase.New()
            _Source = oAeatMod347_Item
            With oAeatMod347_Item
                _Nif = .Contact.NIF
                _RaoSocial = .Contact.Nom
                _Total = .Total.Eur
                _T1 = .T1.Eur
                _T2 = .T2.Eur
                _T3 = .T3.Eur
                _T4 = .T4.Eur
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class
