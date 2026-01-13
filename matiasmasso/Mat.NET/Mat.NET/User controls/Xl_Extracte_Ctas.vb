Public Class Xl_Extracte_Ctas
    Inherits DataGridView

    Private _ControlItems As ControlItems
    Private _LastValue As DTOPgcCta
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Cta
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPgcCta), Optional defaultCta As DTOPgcCta = Nothing, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        Dim LastControlItem As ControlItem = CurrentControlItem()

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _ControlItems = New ControlItems
        For Each oItem As DTOPgcCta In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        _AllowEvents = False

        MyBase.DataSource = _ControlItems
        Dim oNewControlItem As ControlItem = Nothing
        If defaultCta IsNot Nothing Then
            oNewControlItem = _ControlItems.ToList.Find(Function(x) x.Cta.Equals(defaultCta))
        ElseIf LastControlItem IsNot Nothing Then
            oNewControlItem = _ControlItems.ToList.Find(Function(x) x.Cta.Equals(LastControlItem.Cta))
        End If

        If oNewControlItem IsNot Nothing Then
            Dim iRowIndex As Integer = _ControlItems.IndexOf(oNewControlItem)
            MyBase.CurrentCell = MyBase.Rows(iRowIndex).Cells(Cols.Cta)
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOPgcCta
        Get
            Dim retval As DTOPgcCta = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

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
        With MyBase.Columns(Cols.Cta)
            .HeaderText = "Compte"
            .DataPropertyName = "Cta"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        If MyBase.Rows.Count > 0 Then
            MyBase.CurrentCell = MyBase.Rows(0).Cells(Cols.Cta)
        End If

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

    Private Function SelectedItems() As List(Of DTOPgcCta)
        Dim retval As New List(Of DTOPgcCta)
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

        If oControlItem Is Nothing Then
            _LastValue = Nothing
        Else
            _LastValue = SelectedItems.First
            Dim oMenu_PgcCta As New Menu_Cta(_LastValue)
            AddHandler oMenu_PgcCta.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PgcCta.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        RaiseEvent onItemSelected(Me, New MatEventArgs(CurrentControlItem.Source))
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            Dim oCta As DTOPgcCta = CurrentControlItem.Source
            RaiseEvent onItemSelected(Me, New MatEventArgs(oCta))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOPgcCta

        Property Cta As String

        Public Sub New(value As DTOPgcCta)
            MyBase.New()
            _Source = value
            With value
                _Cta = BLL.BLLPgcCta.FullNom(value, BLL.BLLSession.Current.User.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


