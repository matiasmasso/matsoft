Public Class Xl_Mems

    Inherits DataGridView

    Private _Values As List(Of DTOMem)
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)


    Private Enum Cols
        ico
        Fch
        Nom
        Txt
        Usr
    End Enum

    Public Shadows Sub Load(values As List(Of DTOMem), Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Values = values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOMem) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOMem In FilteredValues()
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOMem)
        Dim retval As List(Of DTOMem)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) MatchFilter(x)).ToList
        End If
        Return retval
    End Function

    Private Function MatchFilter(src As DTOMem) As Boolean
        Dim retval As Boolean
        If src.Text.ToLower.Contains(_Filter.ToLower) Then
            retval = True
        ElseIf src.UsrLog.usrCreated IsNot Nothing Then
            If DTOUsrLog2.UsrCreatedNom(src.UsrLog).ToLower.Contains(_Filter.ToLower) Then retval = True
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

    Public ReadOnly Property Value As DTOMem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOMem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
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


        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .Visible = Not SingleContact()
            .HeaderText = "Contacte"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Txt)
            .HeaderText = "Text"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Usr)
            .HeaderText = "Usuari"
            .DataPropertyName = "Usr"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
    End Sub

    Private Function SingleContact() As Boolean
        Dim retval As Boolean
        Select Case _Values.Count
            Case 0
                retval = True
            Case 1
                retval = True
            Case Else
                retval = Not _Values.Exists(Function(x) x.Contact.unEquals(_Values.First.Contact))
        End Select
        Return retval
    End Function

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOMem)
        Dim retval As New List(Of DTOMem)
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
            Dim oMenu_Mem As New Menu_Mem(SelectedItems.First)
            AddHandler oMenu_Mem.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Mem.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Zoom()
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOMem = CurrentControlItem.Source
            Dim oFrm As New Frm_Mem(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Do_Zoom()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            Do_Zoom()
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub


    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem IsNot Nothing Then
            Dim oMem As DTOMem = oControlItem.Source
            Select Case oMem.Cod
                Case DTOMem.Cods.Rep
                    oRow.DefaultCellStyle.BackColor = Color.Yellow
                'UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.Yellow)
                Case Else
                    oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
            End Select
        End If
    End Sub

    Private Sub Xl_Datagridview_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oMem As DTOMem = oControlItem.Source
                If oMem.docfiles.Count > 0 Then
                    e.Value = IconHelper.GetIconFromMimeCod(MatHelperStd.MimeCods.Jpg)
                End If
        End Select
    End Sub


    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOMem

        Property Fch As Date
        Property Nom As String
        Property Txt As String
        Property Usr As String

        Public Sub New(value As DTOMem)
            MyBase.New()
            _Source = value
            With value
                _Fch = .Fch
                _Usr = DTOUsrLog2.UsrCreatedNom(.UsrLog)
                _Nom = .Contact.Nom
                _Txt = Microsoft.VisualBasic.Strings.Left(.Text, 100)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


