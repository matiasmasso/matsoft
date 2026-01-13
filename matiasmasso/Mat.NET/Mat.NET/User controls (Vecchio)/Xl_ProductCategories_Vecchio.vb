Public Class Xl_ProductCategories_Vecchio
    Private _ControlItems As ControlItems
    Private _SelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of ProductCategory), Optional oSelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        _SelectionMode = oSelectionMode
        _ControlItems = New ControlItems
        For Each oItem As ProductCategory In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As ProductCategory
        Get
            Dim retval As ProductCategory = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        _AllowEvents = False
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
            With .Columns(Cols.Nom)
                .HeaderText = BLL.BLLSession.Current.Lang.Tradueix("Categorías", "Categories", "Categories")
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

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
            'Dim oCategory As ProductCategory = oControlItem.Source
            'Dim oMenu_Location As New Menu_Location(oCategory)
            'AddHandler oMenu_Location.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Location.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As ProductCategory = CurrentControlItem.Source
        Select Case _SelectionMode
            Case BLL.Defaults.SelectionModes.Browse
                'Dim oFrm As New Frm_Location(oSelectedValue)
                'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                'oFrm.Show()
            Case bll.dEFAULTS.SelectionModes.Selection
                RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()

    End Sub

    Protected Class ControlItem
        Public Property Source As ProductCategory

        Public Property Nom As String

        Public Sub New(oCategory As ProductCategory)
            MyBase.New()
            _Source = oCategory
            With oCategory
                _Nom = .Nom
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class



