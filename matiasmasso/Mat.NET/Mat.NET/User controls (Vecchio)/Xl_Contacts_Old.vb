Public Class Xl_Contacts_Old
    Private _AllowEvents As Boolean
    Private _Items As Items
    Private _Contacts As Contacts

    Public Event SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event RequestToDelete(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Ico
        Nom
    End Enum

    Public ReadOnly Property Contact() As Contact
        Get
            Return CurrentContact()
        End Get
    End Property

    Public Property Contacts() As Contacts
        Get
            Dim retval As New Contacts
            If _Items IsNot Nothing Then
                For Each oItem As Item In _Items
                    retval.Add(oItem.Source)
                Next
            End If
            Return retval
        End Get
        Set(ByVal Value As Contacts)
            _Items = GetItemsFromContacts(Value)
            LoadGrid()
        End Set
    End Property

    Private Function CurrentContact() As Contact
        Dim retval As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oItem As Item = oRow.DataBoundItem
            retval = oItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentContact()

        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oContact)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function GetItemsFromContacts(oContacts As Contacts) As Items
        Dim retval As New Items
        For Each oContact As Contact In oContacts
            Dim oItem As New Item(oContact)
            retval.Add(oItem)
        Next
        Return retval
    End Function

    Private Sub LoadGrid()

        With DataGridView1
            .DataSource = _Items
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(New DataGridViewImageColumn())
            .Columns.Add(New DataGridViewTextBoxColumn())
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            With .Columns(Cols.Ico)
                .DataPropertyName = "Icon"
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .DataPropertyName = "Text"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oItem As Item = oRow.DataBoundItem
                If oItem.Icon = Item.Icons.Botiga Then
                    e.Value = My.Resources.Basket
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.Nom
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oItem As Item = oRow.DataBoundItem
                If oItem.Obsolet Then
                    e.CellStyle.BackColor = Color.LightGray
                End If
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Sortida()
    End Sub

    Private Sub RefreshRequest()
        LoadGrid()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                Sortida()
                e.Handled = True
        End Select
    End Sub

    Private Sub Sortida()
        RaiseEvent SelectedItemChanged(CurrentContact, New System.EventArgs)
    End Sub

    Protected Class Item
        Public Property Source As Contact
        Public Property Text As String
        Public Property Icon As Icons = Icons.None
        Public Property Obsolet As Boolean

        Public Enum Icons
            None
            Botiga
        End Enum

        Public Sub New(oContact As Contact)
            MyBase.New()
            _Source = oContact
            _Text = oContact.Clx
            If oContact.Client.Botiga Then
                _Icon = Item.Icons.Botiga
            End If
            _Obsolet = oContact.Obsoleto
        End Sub
    End Class

    Protected Class Items
        Inherits System.Collections.CollectionBase

        Public Sub Add(ByVal NewObjMember As Item)
            List.Add(NewObjMember)
        End Sub

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Item
            Get
                Item = List.Item(vntIndexKey)
            End Get
        End Property

    End Class


End Class
