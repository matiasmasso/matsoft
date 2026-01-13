Public Class Xl_CliProductsBlocked
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCliProductBlocked))
        _ControlItems = New ControlItems
        For Each oItem As DTOCliProductBlocked In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOCliProductBlocked
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCliProductBlocked = oControlItem.Source
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

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.ico), DataGridViewImageColumn)
                .DataPropertyName = "Ico"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "Nom"
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

    Private Function SelectedItems() As List(Of DTOCliProductBlocked)
        Dim retval As New List(Of DTOCliProductBlocked)
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
            Dim oMenu_CliProductBlocked As New Menu_CliProductBlocked(SelectedItems)
            AddHandler oMenu_CliProductBlocked.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_CliProductBlocked.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOCliProductBlocked = CurrentControlItem.Source
        Dim oFrm As New Frm_CliProductBlocked(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOCliProductBlocked

        Property ico As Image
        Property Nom As String

        Public Sub New(oCliProductBlocked As DTOCliProductBlocked)
            MyBase.New()
            _Source = oCliProductBlocked
            With oCliProductBlocked
                Select Case .Cod
                    Case DTOCliProductBlocked.Codis.Standard
                        Select Case FEB2.Product.BrandCodDist(_Source.Product)
                            Case DTOProductBrand.CodDists.DistribuidorsOficials
                                _ico = My.Resources.wrong
                            Case DTOProductBrand.CodDists.Free
                                _ico = My.Resources.Ok
                        End Select
                    Case DTOCliProductBlocked.Codis.DistribuidorOficial
                        _ico = My.Resources.star_green
                    Case DTOCliProductBlocked.Codis.Exclusiva
                        _ico = My.Resources.star
                    Case DTOCliProductBlocked.Codis.NoAplicable
                        _ico = My.Resources.NoPark
                    Case DTOCliProductBlocked.Codis.Exclos
                        _ico = My.Resources.wrong
                    Case DTOCliProductBlocked.Codis.AltresEnExclusiva
                        _ico = My.Resources.warn
                End Select
                _Nom = DTOProduct.GetNom(.Product)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

