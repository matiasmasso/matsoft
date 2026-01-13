Public Class Xl_PrvCliNums
    Inherits TabStopDataGridView

    Private _Values As List(Of DTOPrvCliNum)
    Private _DefaultValue As DTOPrvCliNum
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Num
    End Enum


    Public Shadows Sub Load(Values As List(Of DTOPrvCliNum))
        _Values = Values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then SetProperties()

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOPrvCliNum) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOPrvCliNum In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        Dim oBindingSource As New BindingSource
        oBindingSource.AllowNew = True
        oBindingSource.DataSource = _ControlItems
        MyBase.DataSource = oBindingSource
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPrvCliNum)
        Dim retval As List(Of DTOPrvCliNum)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Customer.FullNom.ToLower.Contains(_Filter.ToLower) Or x.CliNum.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOPrvCliNum
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPrvCliNum = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowPrvCliNum.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Num)
            .HeaderText = "Numero"
            .DataPropertyName = "Num"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
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

    Private Function SelectedItems() As List(Of DTOPrvCliNum)
        Dim retval As New List(Of DTOPrvCliNum)
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
            Dim oMenu_Client As New Menu_Client(SelectedItems.First.Customer)
            oContextMenu.Items.AddRange(oMenu_Client.Range)
            oContextMenu.Items.Add("-")
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPrvCliNum = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Contact(oSelectedValue.Customer)
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent OnItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem IsNot Nothing Then
            Dim oPrvCliNum As DTOPrvCliNum = oControlItem.Source

            If oPrvCliNum.Customer.Obsoleto Then
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If

        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOPrvCliNum

        Property Nom As String
        Property Num As String

        Public Sub New(value As DTOPrvCliNum)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Customer.FullNom
                _Num = .CliNum
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

