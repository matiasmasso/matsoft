Public Class Xl_ContactsOld
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Private _ForSelectionOnly As Boolean = True
    Private _IsDTOContact As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event RequestToRemove(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ico
        Nom
    End Enum

    Public Shadows Sub Load(oContacts As List(Of DTOContact))
        _IsDTOContact = True
        _ControlItems = New ControlItems
        For Each oContact As DTOContact In oContacts
            Dim oItem As New ControlItem(oContact)
            _ControlItems.Add(oItem)
        Next

        LoadGrid()
    End Sub

    Public WriteOnly Property DataSource As Contacts
        Set(value As Contacts)

            _ControlItems = New ControlItems
            For Each oContact As Contact In value
                Dim oItem As New ControlItem(oContact)
                _ControlItems.Add(oItem)
            Next

            LoadGrid()
        End Set
    End Property


    Public Property SelectedObject2 As DTOContact
        Get
            Dim retval As DTOContact = CurrentItem2()
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

    Public Property SelectedObject As Contact
        Get
            Dim retval As Contact = CurrentItem()
            Return retval
        End Get
        Set(value As Contact)
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                Dim oItem As Contact = oRow.DataBoundItem
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

    Private Function CurrentItem2() As DTOContact
        Dim retval As DTOContact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oItem As ControlItem = oRow.DataBoundItem
            retval = oItem.Source2
        End If
        Return retval
    End Function

    Private Function CurrentItem() As Contact
        Dim retval As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oItem As ControlItem = oRow.DataBoundItem
            retval = oItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As Contact = CurrentItem()

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

            .AutoGenerateColumns = False
            .Columns.Clear()

            .Columns.Add(New DataGridViewImageColumn)
            .Columns.Add(New DataGridViewTextBoxColumn)

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.Ico)
                .DataPropertyName = "Ico"
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

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

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                Sortida()
                e.Handled = True
            Case Keys.Delete
                RaiseEvent RequestToRemove(Me, New MatEventArgs(CurrentItem2))
        End Select
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oItem As ControlItem = oRow.DataBoundItem
                Select Case oItem.Ico
                    Case ControlItem.Icons.None
                        e.Value = My.Resources.empty
                    Case ControlItem.Icons.Botiga
                        e.Value = My.Resources.Basket
                    Case ControlItem.Icons.Obsolet
                        e.Value = My.Resources.del
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oItem As ControlItem = oRow.DataBoundItem
        If oItem.Ico = ControlItem.Icons.Obsolet Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents And Not _ForSelectionOnly Then
            SetContextMenu()
        End If
    End Sub
#End Region

#Region "Sizes"

    Public Function WidthAdjustment() As Integer
        Dim oGraphics As Graphics = DataGridView1.CreateGraphics()
        Dim iMaxWidth As Integer
        For Each oItem As ControlItem In _ControlItems
            Dim iWidth As Integer = DataGridViewCell.MeasureTextWidth(oGraphics, oItem.Nom, DataGridView1.Font, DataGridView1.RowTemplate.Height, TextFormatFlags.Left)
            If iWidth > iMaxWidth Then iMaxWidth = iWidth
        Next

        Dim iOriginalColWidth As Integer = DataGridView1.Columns(Cols.Nom).Width
        Dim retval As Integer = iMaxWidth - iOriginalColWidth
        Return retval
    End Function

    Public Function AdjustedHeight() As Integer
        Dim MaxVisibleRows As Integer = 16
        Dim VisibleRows As Integer = 0
        If _ControlItems.Count <= MaxVisibleRows Then
            VisibleRows = _ControlItems.Count
        Else
            VisibleRows = MaxVisibleRows
        End If

        Dim retval As Integer = DataGridView1.RowTemplate.Height * VisibleRows + 3
        Return retval
    End Function
#End Region

#Region "ControlItem"

    Protected Class ControlItem
        Public Property Source As Contact
        Public Property Source2 As DTOContact
        Public Property Ico As Icons
        Public Property Nom As String

        Public Enum Icons
            None
            Botiga
            Obsolet
        End Enum

        Public Sub New(oContact As Contact)
            MyBase.New()
            _Source = oContact
            With _Source
                _Nom = .Clx
                If .Obsoleto Then
                    _Ico = Icons.Obsolet
                ElseIf .Botiga Then
                    _Ico = Icons.Botiga
                End If
            End With
        End Sub

        Public Sub New(oContact As DTOContact)
            MyBase.New()
            _Source2 = oContact
            With _Source2
                _Nom = .FullNom
                If .Obsoleto Then
                    _Ico = Icons.Obsolet
                ElseIf .Botiga Then
                    _Ico = Icons.Botiga
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.Collections.CollectionBase

        Public Sub Add(ByVal NewObjMember As ControlItem)
            List.Add(NewObjMember)
        End Sub

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ControlItem
            Get
                Item = List.Item(vntIndexKey)
            End Get
        End Property

    End Class


#End Region

End Class


