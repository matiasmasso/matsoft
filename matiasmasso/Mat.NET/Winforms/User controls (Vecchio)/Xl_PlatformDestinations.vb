Public Class Xl_PlatformDestinations
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPlatformDestination))
        _ControlItems = New ControlItems
        For Each oItem As DTOPlatformDestination In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Items As List(Of DTOPlatformDestination)
        Get
            Dim retval As New List(Of DTOPlatformDestination)
            Dim oBindingSource As BindingSource = DataGridView1.DataSource
            Dim oControlItems As ControlItems = oBindingSource.DataSource
            For Each oControlItem As ControlItem In oControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

    Public Function SelectedItem() As DTOPlatformDestination
        Dim retval As DTOPlatformDestination = Nothing
        Dim oControlItem As ControlItem = CurrentItem()
        If oControlItem IsNot Nothing Then
            retval = oControlItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        'Dim oMenuItem As ToolStripMenuItem = _IncludeObsoletsMenuItem
        'oContextMenu.Items.Add(oMenuItem)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow Is Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub LoadGrid()
        _AllowEvents = False

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .ReadOnly = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .RowHeadersVisible = True
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

        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView1_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Nom
                    _AllowEvents = False
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim oItem As ControlItem = oRow.DataBoundItem
                    Dim sKey As String = oRow.Cells(Cols.Nom).Value
                    Dim exs As New List(Of Exception)
                    Dim oContact = Finder.FindContact(exs, Current.Session.User, sKey)
                    If exs.Count = 0 Then
                        If oContact Is Nothing Then
                            oRow.Cells(Cols.Nom).Value = "(no s'ha trobat cap producte per '" & sKey & "')"
                            oItem.Source = Nothing
                        Else
                            Dim oPlatformDestination = DTOPlatformDestination.FromContact(oContact)
                            oItem.Source = oPlatformDestination
                            oItem.Nom = oPlatformDestination.FullNom
                        End If

                        RaiseEvent AfterUpdate(oItem, EventArgs.Empty)
                        _AllowEvents = True
                        SetContextMenu()
                    Else
                        UIHelper.WarnError(exs)
                    End If
            End Select
        End If
    End Sub


    Protected Class ControlItem
        Public Property Source As DTOPlatformDestination
        Public Property Nom As String

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(oPlatformDestination As DTOPlatformDestination)
            MyBase.New()
            _Source = oPlatformDestination
            With oPlatformDestination
                _Nom = .FullNom
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
