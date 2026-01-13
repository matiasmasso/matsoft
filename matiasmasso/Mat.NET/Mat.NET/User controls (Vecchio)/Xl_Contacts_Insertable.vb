Public Class Xl_Contacts_Insertable

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private _DirtyCell As Boolean
    Private _LastValidatedObject As DTOContact

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event RequestToRemove(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(oContacts As List(Of DTOContact))
        _ControlItems = New ControlItems
        For Each oContact As DTOContact In oContacts
            Dim oItem As New ControlItem(oContact)
            _ControlItems.Add(oItem)
        Next

        LoadGrid()
    End Sub

    Public ReadOnly Property Contacts As List(Of DTOContact)
        Get
            Dim retval As New List(Of DTOContact)
            Dim oBindingSource As BindingSource = DataGridView1.DataSource
            Dim oControlItems As ControlItems = oBindingSource.DataSource
            For Each oControlItem As ControlItem In _ControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

    Public Property SelectedObject As DTOContact
        Get
            Dim retval As DTOContact = CurrentItem()
            Return retval
        End Get
        Set(value As DTOContact)
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                Dim oItem As DTOContact = oRow.DataBoundItem
                If oItem.Equals(value) Then
                    DataGridView1.CurrentCell = oRow.Cells(0)
                    Exit For
                End If
            Next
        End Set
    End Property


    Private Sub Sortida()
        Dim oEventArgs As New MatEventArgs(CurrentItem)
        RaiseEvent onItemSelected(Me, oEventArgs)
    End Sub

    Private Function CurrentItem() As DTOContact
        Dim retval As DTOContact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oItem As ControlItem = oRow.DataBoundItem
            If oItem IsNot Nothing Then
                retval = oItem.Source
            End If
        End If
        Return retval
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As DTOContact = CurrentItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu As New Menu_Contact(oControlItem)
            oContextMenu.Items.AddRange(oMenu.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

#Region "Grid"

    Private Sub LoadGrid()
        With DataGridView1

            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .ReadOnly = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .RowHeadersVisible = False
            .ColumnHeadersVisible = False
            .AutoGenerateColumns = False
            .Columns.Clear()

            Dim oBindingSource As New BindingSource
            oBindingSource.AllowNew = True
            oBindingSource.DataSource = _ControlItems
            .DataSource = oBindingSource

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Sortida()
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'Select Case e.ColumnIndex
        '    Case Cols.Ico
        'Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        'Dim oItem As ControlItem = oRow.DataBoundItem
        'Select Case oItem.Ico
        'Case ControlItem.Icons.None
        '    e.Value = My.Resources.empty
        'Case ControlItem.Icons.Botiga
        '    e.Value = My.Resources.Basket
        'Case ControlItem.Icons.Obsolet
        '    e.Value = My.Resources.del
        'End Select
        'End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oItem As ControlItem = oRow.DataBoundItem
        'If oItem.Ico = ControlItem.Icons.Obsolet Then
        'oRow.DefaultCellStyle.BackColor = Color.LightGray
        'End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub
#End Region

#Region "Insertion"
    Private Sub DataGridView1_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        _DirtyCell = True
    End Sub

    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If _DirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            Select Case e.ColumnIndex
                Case Cols.Nom
                    If e.FormattedValue = "" Then
                        _LastValidatedObject = Nothing
                    Else
                        Dim oContact As DTOContact = Nothing
                        Dim oControlItem As ControlItem = oRow.DataBoundItem
                        Dim valueChanged As Boolean = True
                        If oControlItem IsNot Nothing Then
                            oContact = oControlItem.Source
                            If oContact IsNot Nothing Then
                                valueChanged = (oContact.FullNom <> e.FormattedValue)
                            End If
                        End If

                        If valueChanged Then
                            oContact = Finder.FindContact2(BLL.BLLApp.Emp, e.FormattedValue)
                            If oContact Is Nothing Then
                                e.Cancel = True
                            Else
                                _LastValidatedObject = oContact
                            End If
                        Else
                            _LastValidatedObject = oContact
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        If _DirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Select Case e.ColumnIndex
                Case Cols.Nom
                    Dim oContact As DTOContact = _LastValidatedObject
                    Dim oItem As ControlItem = oRow.DataBoundItem
                    oItem.Source = oContact
                    oItem.Nom = oContact.FullNom
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                    'Stop
                    'oRow.Cells(Cols.Id).Value = oContact.Id
                    'oRow.Cells(Cols.Nom).Value = oContact.Clx
                    'SetDirty()
            End Select

            _DirtyCell = False
        End If
    End Sub


    Private Sub Datagridview1_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridView1.UserDeletedRow
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Datagridview1_UserAddedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridView1.UserAddedRow
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub
#End Region


    Protected Class ControlItem
        Public Property Source As DTOContact
        Public Property Ico As Icons
        Public Property Nom As String

        Public Enum Icons
            None
            Botiga
            Obsolet
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(oContact As DTOContact)
            MyBase.New()
            _Source = oContact
            With _Source
                _Nom = .FullNom
            End With
        End Sub

    End Class


    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete
                Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
                If oRow IsNot Nothing Then
                    _ControlItems.Remove(oRow.DataBoundItem)
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                End If
        End Select
    End Sub
End Class



