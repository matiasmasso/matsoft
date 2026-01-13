Public Class Xl_Proveidor_Entrades

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTODelivery)
    Private _DefaultValue As DTODelivery
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Fch
        Remesa
        Eur
    End Enum

    Public Shadows Sub Load(values As List(Of DTODelivery), Optional oDefaultValue As DTODelivery = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTODelivery) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTODelivery In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTODelivery
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTODelivery = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowDelivery.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Id)
            .HeaderText = "Albará"
            .DataPropertyName = "Id"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Remesa)
            .HeaderText = "Remesa"
            .DataPropertyName = "Remesa"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_Delivery As New Menu_Delivery(SelectedItems)
            AddHandler oMenu_Delivery.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Delivery.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("assignar a remesa d'importació", Nothing, AddressOf Do_SetImportacio)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_SetImportacio()
        If SelectedControlItems.Count > 0 Then
            Dim oDelivery As DTODelivery = SelectedItems.First
            Dim oProveidor As DTOProveidor = oDelivery.Proveidor
            Dim oFrm As New Frm_Importacions(oProveidor, DTO.Defaults.SelectionModes.Selection)
            AddHandler oFrm.ItemSelected, AddressOf onImportacioSelected
            oFrm.Show()
        End If
    End Sub

    Private Async Sub onImportacioSelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oImportacio As DTOImportacio = e.Argument
        If FEB.Importacio.Load(exs, oImportacio) Then
            Dim oItemsToAdd As New List(Of DTOImportacioItem)
            Dim oDeliveries As List(Of DTODelivery) = SelectedItems()
            For Each oDelivery As DTODelivery In oDeliveries
                Dim oItemToAdd As New DTOImportacioItem(oDelivery.Guid)
                oItemToAdd.Parent = oImportacio
                oItemToAdd.SrcCod = DTOImportacioItem.SourceCodes.Alb
                oItemsToAdd.Add(oItemToAdd)
            Next

            oImportacio.Items.AddRange(oItemsToAdd)

            Dim id = Await FEB.Importacio.Update(oImportacio, exs)
            If exs.Count = 0 Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
                For Each item As DTOImportacioItem In oItemsToAdd
                    oImportacio.Items.Remove(item)
                Next
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTODelivery = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oDelivery As DTODelivery = Me.Value
                    Dim oCustomer = oDelivery.Contact
                    Dim exs As New List(Of Exception)
                    If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
                        Dim oFrm As New Frm_AlbNew2(oDelivery)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

#Region "DragDrop"


    Private Sub Xl_Proveidor_Entrades_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If MyBase.MouseIsDown Then
            MyBase.DoDragDrop(Me.SelectedItems, DragDropEffects.Copy)
        End If
        MyBase.MouseIsDown = False
    End Sub


#End Region


    Protected Class ControlItem
        Property Source As DTODelivery

        Property Id As Integer
        Property Fch As Date
        Property Remesa As Integer
        Property Eur As Decimal


        Public Sub New(value As DTODelivery)
            MyBase.New()
            _Source = value
            With value
                _Id = .Id
                _Fch = .Fch
                If .Importacio IsNot Nothing Then
                    _Remesa = .Importacio.Id
                End If
                _Eur = .Import.Eur
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


