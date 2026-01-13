Public Class Xl_ContactClasses

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOContactClass)
    Private _DefaultValue As DTOContactClass
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOContactClass), Optional oDefaultValue As DTOContactClass = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub



    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOContactClass) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOContactClass In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems

        If Not CouldSelectDefaultValue() Then
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function CouldSelectDefaultValue() As Boolean
        Dim retval As Boolean
        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.FirstOrDefault(Function(x) x.Source.Equals(_DefaultValue))
            If oControlItem IsNot Nothing Then
                Dim idx As Integer = _ControlItems.IndexOf(oControlItem)
                MyBase.CurrentCell = MyBase.Rows(idx).Cells(Cols.Nom)
                retval = True
            End If
        End If
        Return retval
    End Function

    Private Function FilteredValues() As List(Of DTOContactClass)
        Dim retval As List(Of DTOContactClass)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.Esp.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOContactClass
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOContactClass = Nothing
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowContactClass.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = True
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of DTOContactClass)
        Dim retval As New List(Of DTOContactClass)
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
            Dim oMenu_ContactClass As New Menu_ContactClass(SelectedItems.First)
            AddHandler oMenu_ContactClass.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_ContactClass.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOContactClass = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_ContactClass(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
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
    Private Sub DataGridView1_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragOver
        Dim previousAllowedEvents As Boolean = _AllowEvents
        _AllowEvents = False

        If (e.Data.GetDataPresent(GetType(List(Of DTOContact)))) Then
            Dim oPoint = MyBase.PointToClient(New Point(e.X, e.Y))
            Dim hit As DataGridView.HitTestInfo = MyBase.HitTest(oPoint.X, oPoint.Y)
            If hit.Type = DataGridViewHitTestType.Cell Then
                Dim oclickedCell As DataGridViewCell = MyBase.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                MyBase.CurrentCell = oclickedCell
            End If
        Else
            e.Effect = DragDropEffects.None
        End If

        _AllowEvents = previousAllowedEvents
    End Sub

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(GetType(List(Of DTOContact))) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub


    Private Async Sub DataGridView1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop
        Dim previousAllowedEvents As Boolean = _AllowEvents
        Dim sourceClass As DTOContactClass = Nothing
        _AllowEvents = False

        Dim exs As New List(Of Exception)
        Dim oTargetCell As DataGridViewCell = Nothing
        If e.Data.GetDataPresent(GetType(List(Of DTOContact))) Then

            Dim oPoint = sender.PointToClient(New Point(e.X, e.Y))
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(oPoint.X, oPoint.Y)
            If hit.Type = DataGridViewHitTestType.Cell Then
                Dim oTargetRow As DataGridViewRow = sender.Rows(hit.RowIndex)
                Dim oControlitem As ControlItem = oTargetRow.DataBoundItem
                If oControlitem IsNot Nothing Then
                    Dim oClass As DTOContactClass = oControlitem.Source
                    Dim oContacts As List(Of DTOContact) = e.Data.GetData(GetType(List(Of DTOContact)))
                    If oContacts.Count > 0 Then
                        sourceClass = oContacts.First.ContactClass
                        SelectSourceClass(sourceClass)
                        If Await FEB.Contacts.MoveToClass(exs, oClass, oContacts) Then
                            MyBase.RefreshRequest(Me, MatEventArgs.Empty)
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    End If
                End If
            End If
        End If

        _AllowEvents = previousAllowedEvents
    End Sub

    Private Sub SelectSourceClass(oClass As DTOContactClass)
        If oClass IsNot Nothing Then
            Dim idx As Integer = _Values.IndexOf(oClass)
            Dim sourceCell As DataGridViewCell = MyBase.Rows(idx).Cells(Cols.Nom)
            MyBase.CurrentCell = sourceCell
        End If
    End Sub

#End Region


    Protected Class ControlItem
        Property Source As DTOContactClass

        Property Nom As String

        Public Sub New(value As DTOContactClass)
            MyBase.New()
            _Source = value

            With value
                Dim sNom As String = .Nom.Tradueix(Current.Session.Lang)
                If value.Contacts.Count > 0 Then
                    _Nom = String.Format("{0} ({1:#,##0.#})", sNom, .Contacts.Count)
                Else
                    _Nom = sNom
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

