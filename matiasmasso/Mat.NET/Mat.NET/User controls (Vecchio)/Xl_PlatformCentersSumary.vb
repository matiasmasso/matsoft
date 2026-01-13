Public Class Xl_PlatformCentersSumary

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event SelectionChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Check
        Centre
        Albarans
        Import
    End Enum

    Public Shadows Sub Load(values As List(Of DTODelivery))

        Dim oUnsorted As New List(Of ControlItem)
        For Each oItem As DTODelivery In values
            Dim oGuid As Guid = oItem.Contact.Guid
            Dim oControlItem As ControlItem = oUnsorted.Find(Function(x) x.Source.Guid.Equals(oGuid)) ' _ControlItems.Find(oItem.Contact.Guid)
            If oControlItem Is Nothing Then
                oControlItem = New ControlItem(oItem.Contact)
                oUnsorted.Add(oControlItem)
            End If

            oControlItem.Albarans += 1
            oControlItem.Import += BLL_Delivery.BaseImponible(oItem).Eur
        Next

        'sort
        _ControlItems = New ControlItems
        For Each oControlItem In oUnsorted.OrderBy(Function(x) x.Centre).ToList()
            _ControlItems.Add(oControlItem)
        Next

        LoadGrid()
    End Sub

    Public ReadOnly Property ItemsChecked As List(Of DTOContact)
        Get
            Dim retval As New List(Of DTOContact)
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

    Public ReadOnly Property Items As List(Of DTOContact)
        Get
            Dim retval As New List(Of DTOContact)
            Dim oBindingSource As BindingSource = DataGridView1.DataSource
            Dim oControlItems As ControlItems = oBindingSource.DataSource
            For Each oControlItem As ControlItem In oControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

    Public Function SelectedItem() As DTOContact
        Dim retval As DTOContact = Nothing
        Dim oControlItem As ControlItem = CurrentItem()
        If oControlItem IsNot Nothing Then
            retval = oControlItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As New ToolStripMenuItem("desmarcar el resto", Nothing, AddressOf Do_UnCheckRest)
        oContextMenu.Items.Add(oMenuItem)
        oMenuItem = New ToolStripMenuItem("marcar todo", Nothing, AddressOf Do_CheckAll)
        oContextMenu.Items.Add(oMenuItem)
        oMenuItem = New ToolStripMenuItem("desmarcar todo", Nothing, AddressOf Do_UnCheckAll)
        oContextMenu.Items.Add(oMenuItem)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_CheckAll()
        For Each oItem As ControlItem In _ControlItems
            oItem.Checked = True
        Next
        DataGridView1.Refresh()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_UnCheckAll()
        For Each oItem As ControlItem In _ControlItems
            oItem.Checked = False
        Next
        DataGridView1.Refresh()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_UnCheckRest()
        Dim oCurrentItem As ControlItem = CurrentItem()
        For Each oItem As ControlItem In _ControlItems
            If oItem Is oCurrentItem Then
                oItem.Checked = True
            Else
                oItem.Checked = False
            End If
        Next
        DataGridView1.Refresh()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
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
            With CType(.Columns(Cols.Check), DataGridViewImageColumn)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Centre)
                .DataPropertyName = "Centre"
                .HeaderText = "Centre"
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
        Public Property Source As DTOContact
        Public Property Checked As Boolean
        Public Property Centre As String
        Public Property Albarans As Integer
        Public Property Import As Decimal

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(oContact As DTOContact)
            MyBase.New()
            _Source = oContact
            With oContact
                _Checked = True
                _Centre = GetNom()
            End With
        End Sub

        Private Function GetNom() As String
            Dim retval As String = _Source.FullNom
            Dim StartPos As Integer = retval.IndexOf("[")
            If StartPos >= 0 Then
                retval = retval.Substring(StartPos + 1)
                Dim EndPos As Integer = retval.IndexOf("]")
                If EndPos >= 0 Then
                    retval = retval.Substring(0, EndPos)
                End If
            End If
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)

        'Public Function Find(oGuid As Guid) As ControlItem
        ' Dim retval As ControlItem = Nothing
        '     For Each oItem As ControlItem In Me
        '         If oItem.Source.Guid.Equals(oGuid) Then
        '             retval = oItem
        '             Exit For
        '         End If
        '     Next
        '     Return retval
        ' End Function
    End Class

End Class
