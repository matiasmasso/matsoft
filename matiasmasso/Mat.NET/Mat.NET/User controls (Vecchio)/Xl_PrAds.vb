Public Class Xl_PrAds

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterSelect(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As EventArgs)

    Private Enum Cols
        Product
        Nom
    End Enum

    Public Shadows Sub Load(value As PrAds)
        _ControlItems = New ControlItems
        For Each oItem As PrAd In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As PrAd
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As PrAd = oControlItem.Source
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
            With .Columns(Cols.Product)
                .HeaderText = "Producte"
                .DataPropertyName = "Product"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "Anunci"
                .DataPropertyName = "Nom"
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

    Private Function SelectedItems() As PrAds
        Dim retval As New PrAds
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
            Dim oMenu_PrAd As New Menu_PrAd(SelectedItems.First)
            oContextMenu.Items.AddRange(oMenu_PrAd.Range)
        End If

        Dim oMenuItem As ToolStripMenuItem = Nothing

        oMenuItem = New ToolStripMenuItem("afegir mides", Nothing, AddressOf AddChild)
        'oMenuItem.Enabled = CurrentParent() IsNot Nothing
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("afegir nou", Nothing, AddressOf AddParent)
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
        'Dim oAd As PrAd = CurrentParent()
        'Dim oAdDoc As New PrAdDoc(oAd)
        'Dim oFrm As New Frm_PrAdDoc(oAdDoc)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest2
        'oFrm.Show()
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oPrAd As PrAd = CurrentControlItem.Source
        Dim oFrm As New Frm_PrAd(oPrAd)
        AddHandler oFrm.AfterUpdate, AddressOf refreshrequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
            Dim oArgs As New MatEventArgs(CurrentControlItem.Source)
            RaiseEvent AfterSelect(Me, oArgs)
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, EventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Public Property Source As PrAd

        Public Property Product As String
        Public Property Nom As String

        Public Sub New(value As PrAd)
            MyBase.New()
            _Source = value
            With value
                _Product = .Product.Text
                _Nom = .Nom
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

