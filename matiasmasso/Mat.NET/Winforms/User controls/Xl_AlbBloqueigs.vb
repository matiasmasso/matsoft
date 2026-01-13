Public Class Xl_AlbBloqueigs

    Inherits DataGridView

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        CliNom
        UsrNom
        Fch
        Cod
    End Enum

    Public Shadows Sub Load(values As List(Of DTOAlbBloqueig))
        _ControlItems = New ControlItems
        For Each oItem As DTOAlbBloqueig In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOAlbBloqueig
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOAlbBloqueig = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.CliNom)
            .HeaderText = "Client"
            .DataPropertyName = "CliNom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.UsrNom)
            .HeaderText = "Usuari"
            .DataPropertyName = "UserNom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cod)
            .HeaderText = "Codi"
            .DataPropertyName = "Cod"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With


            SetContextMenu()
            _AllowEvents = True
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

    Private Function SelectedItems() As List(Of DTOAlbBloqueig)
        Dim retval As New List(Of DTOAlbBloqueig)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then
            If CurrentControlItem() IsNot Nothing Then
                retval.Add(CurrentControlItem.Source)
            End If
        End If
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
        Dim items As List(Of DTOAlbBloqueig) = SelectedItems()

        If items.Count > 0 Then
            oContextMenu.Items.Add("desbloquejar", Nothing, AddressOf Do_Unbloq)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Async Sub Do_Unbloq()
        Dim exs As New List(Of Exception)
        For Each Item As DTOAlbBloqueig In SelectedItems()
            If Not Await FEB2.AlbBloqueig.Delete(Item, exs) Then
                UIHelper.WarnError(exs)
            End If
        Next

        If exs.Count = 0 Then
            RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOAlbBloqueig

        Property CliNom As String
        Property UserNom As String
        Property Fch As Date
        Property Cod As String

        Public Sub New(value As DTOAlbBloqueig)
            MyBase.New()
            _Source = value
            With value
                If .Contact IsNot Nothing Then _CliNom = .Contact.FullNom
                _UserNom = DTOUser.NicknameOrElse(.User)
                _Fch = .Fch
                _Cod = IIf(.Codi = DTOAlbBloqueig.Codis.PDC, "Comanda", "Albarà")
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
