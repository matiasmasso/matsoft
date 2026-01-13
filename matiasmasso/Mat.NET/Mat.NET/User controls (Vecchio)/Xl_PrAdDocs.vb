Public Class Xl_PrAdDocs

    Private _ControlItems As ControlItems
    Private _SelectionMode As bll.dEFAULTS.SelectionModes
    Private _AllowEvents As Boolean

    Public Event AfterSelect(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ico
        Size
    End Enum

    Public Shadows Sub Load(oPrAdDocs As PrAdDocs)
        _ControlItems = New ControlItems
        For Each oItem As PrAdDoc In oPrAdDocs
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
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.Ico), DataGridViewImageColumn)
                .HeaderText = ""
                .DataPropertyName = "Ico"
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Size)
                .HeaderText = ""
                .DataPropertyName = "Size"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As PrAdDocs
        Dim retval As New PrAdDocs
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
            Dim oMenu_PrAdDoc As New Menu_PrAdDoc(SelectedItems.First)
            oContextMenu.Items.AddRange(oMenu_PrAdDoc.Range)
        End If

        Dim oMenuItem As New ToolStripMenuItem("noves mides", Nothing, AddressOf AddChild)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub ZoomParent(ByVal sender As Object, ByVal e As EventArgs)
        'Dim oAd As PrAd = CurrentParent()
        'Dim oFrm As New Frm_PrAd(oAd)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest1
        'oFrm.Show()
    End Sub

    Private Sub ZoomChild(ByVal sender As Object, ByVal e As EventArgs)
        'Dim oAd As PrAdDoc = CurrentChild()
        'Dim oFrm As New Frm_PrAddoc(oAd)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest2
        'oFrm.Show()
    End Sub

    Private Sub AddParent(ByVal sender As Object, ByVal e As EventArgs)
        'Dim oAd As New PrAd()
        'Dim oFrm As New Frm_PrAd(oAd)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest1
        'oFrm.Show()
    End Sub

    Private Sub AddChild(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oPrAdDoc As PrAdDoc = CurrentControlItem.Source
        Dim oArgs As New MatEventArgs(oPrAdDoc)
        RaiseEvent AfterSelect(Me, oArgs)
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As PrAdDoc

        Public Property Ico As Image
        Public Property Size As String

        Public Sub New(value As PrAdDoc)
            MyBase.New()
            _Source = value
            With value
                If .DocFile IsNot Nothing Then
                    _Ico = BLL.MediaHelper.GetIconFromMimeCod(.DocFile.Mime)
                End If
                _Size = .FullText
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

