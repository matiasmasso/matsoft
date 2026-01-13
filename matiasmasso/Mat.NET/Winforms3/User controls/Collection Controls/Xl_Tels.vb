Public Class Xl_Tels
    Inherits _Xl_ReadOnlyDatagridview

    Private _Contact As DTOContact
    Private _DefaultValue As DTOBaseTel
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Num
        Obs
    End Enum

    Public Shadows Sub Load(oContact As DTOContact, Optional oDefaultValue As DTOBaseTel = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Contact = oContact
        _SelectionMode = oSelectionMode
        Refresca(_Contact.Tels, _Contact.Emails)
    End Sub

    Public ReadOnly Property Values As List(Of DTOBaseTel)
        Get
            Dim retval As New List(Of DTOBaseTel)
            For Each oControlItem In _ControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property Tels As List(Of DTOContactTel)
        Get
            Dim retval As New List(Of DTOContactTel)
            For Each oControlItem In _ControlItems
                If TypeOf oControlItem.Source Is DTOContactTel Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property Emails As List(Of DTOUser)
        Get
            Dim retval As New List(Of DTOUser)
            For Each oControlItem In _ControlItems
                If TypeOf oControlItem.Source Is DTOUser Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property


    Private Sub Refresca(oTels As List(Of DTOContactTel), oEmails As List(Of DTOUser))
        _AllowEvents = False
        _ControlItems = New ControlItems

        For Each oTel In oTels
            Dim oControlItem As New ControlItem(oTel)
            _ControlItems.Add(oControlItem)
        Next

        For Each oEmail In oEmails
            Dim oControlItem As New ControlItem(oEmail)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        MyBase.ClearSelection()
        SetContextMenu()

        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOBaseTel
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOBaseTel = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = True
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 20
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Num)
            .HeaderText = "Numero"
            .DataPropertyName = "Num"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Obs)
            .HeaderText = "Observacions"
            .DataPropertyName = "Obs"
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

    Private Function SelectedItems() As List(Of DTOBaseTel)
        Dim retval As New List(Of DTOBaseTel)
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

        If MyBase.SelectedRows.Count > 0 Then
            Dim oControlItem As ControlItem = CurrentControlItem()

            If oControlItem IsNot Nothing Then
                If TypeOf oControlItem.Source Is DTOContactTel Then
                    Dim oTel As DTOContactTel = oControlItem.Source
                    Dim oMenu As New Menu_Tel(oTel)
                    AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu.Range)
                    oContextMenu.Items.Add("eliminar", My.Resources.aspa, AddressOf Do_DeleteTel)
                    oContextMenu.Items.Add("-")
                ElseIf TypeOf oControlItem.Source Is DTOUser Then
                    Dim oMenu As New Menu_User(oControlItem.Source)
                    AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu.Range)
                    oContextMenu.Items.Add("-")
                    oContextMenu.Items.Add("desvincular d'aquest contacte", Nothing, AddressOf Do_EmailDrop)
                End If
            End If

        End If
        Dim oAddMenuItem = New ToolStripMenuItem("afegir")
        oContextMenu.Items.Add(oAddMenuItem)
        oAddMenuItem.DropDownItems.Add("telèfon", Nothing, AddressOf Do_AddNewTel)
        oAddMenuItem.DropDownItems.Add("mobil", Nothing, AddressOf Do_AddNewMobil)
        oAddMenuItem.DropDownItems.Add("email", Nothing, AddressOf Do_AddNewEmail)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Protected Shadows Sub RefreshRequest(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        If TypeOf e.Argument Is DTOContactTel Then
            Dim oTel As DTOContactTel = e.Argument

        Else

        End If
        Refresca(Me.Tels, Me.Emails)
        'Dim oContact = Await FEB.Contact.Find(_Contact.Guid, exs)
        'If exs.Count = 0 Then
        'Load(oContact)
        MyBase.RefreshRequest(sender, e)
        'Else
        'UIHelper.WarnError(exs)
        'End If
    End Sub

    Private Sub Do_DeleteTel()
        Dim oControlItem As ControlItem = CurrentControlItem()
        _ControlItems.Remove(oControlItem)
        RaiseEvent AfterUpdate(Me, New MatEventArgs)
    End Sub

    Public Sub Do_AddNewTel()
        Dim oTel = DTOContactTel.Factory(_Contact, DTOContactTel.Cods.tel)
        Dim oFrm As New Frm_Tel(oTel)
        AddHandler oFrm.AfterUpdate, AddressOf onTelAdded
        oFrm.Show()
    End Sub

    Public Sub Do_AddNewMobil()
        Dim oTel = DTOContactTel.Factory(_Contact, DTOContactTel.Cods.movil)
        Dim oFrm As New Frm_Tel(oTel)
        AddHandler oFrm.AfterUpdate, AddressOf onTelAdded
        oFrm.Show()
    End Sub

    Public Sub Do_AddNewEmail()
        Dim oUser = DTOUser.Factory(Current.Session.Emp, _Contact)
        Do_AddNewEmail(oUser)
    End Sub

    Public Sub Do_AddNewEmail(oUser As DTOUser)
        Dim oFrm As New Frm_Email(oUser)
        AddHandler oFrm.AfterUpdate, AddressOf onEmailAdded
        oFrm.Show()
    End Sub

    Private Sub Do_EmailDrop()
        Dim oControlItem As ControlItem = CurrentControlItem()
        _ControlItems.Remove(oControlItem)
        RaiseEvent AfterUpdate(Me, New MatEventArgs)
    End Sub

    Private Sub onTelAdded(sender As Object, e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
        Dim oEmails = Me.Emails
        Dim oTels = Me.Tels
        oTels.Add(e.Argument)
        oTels.OrderBy(Function(x) x.Privat).OrderBy(Function(y) y.Ord).OrderBy(Function(x) x.Cod)
        Refresca(oTels, oEmails)
    End Sub

    Private Sub onEmailAdded(sender As Object, e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
        Dim oEmails = Me.Emails
        Dim oTels = Me.Tels
        oEmails.Add(e.Argument)
        Refresca(oTels, oEmails)
    End Sub

    Private Sub Xl_StringList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Delete Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            _ControlItems.Remove(oControlItem)
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOBaseTel = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    If TypeOf oSelectedValue Is DTOContactTel Then
                        Dim oContactTel As DTOContactTel = oSelectedValue
                        Dim oFrm As New Frm_Tel(oContactTel)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    ElseIf TypeOf oSelectedValue Is DTOUser Then
                        Dim oUser As DTOUser = oSelectedValue
                        Dim oFrm As New Frm_User(oUser)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
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

    Private Sub Xl_Tels_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim value As DTOBaseTel = oControlItem.Source
                Select Case oControlItem.Icon
                    Case ControlItem.Icons.tel
                        e.Value = My.Resources.tel
                    Case ControlItem.Icons.fax
                        e.Value = My.Resources.fax
                    Case ControlItem.Icons.movil
                        e.Value = My.Resources.movil
                    Case ControlItem.Icons.email
                        e.Value = My.Resources.MailSobreGroc
                    Case ControlItem.Icons.PrivateTel
                        e.Value = My.Resources.tel_red
                    Case ControlItem.Icons.PrivateMovil
                        e.Value = My.Resources.movil_red
                    Case ControlItem.Icons.emailBadMail
                        e.Value = My.Resources.wrong
                    Case ControlItem.Icons.emailNoMailings
                        e.Value = My.Resources.NoPark
                    Case ControlItem.Icons.emailEfras
                        e.Value = My.Resources.MailSobreGrocBlau
                    Case ControlItem.Icons.Obsolet
                        e.Value = My.Resources.del
                End Select


        End Select
    End Sub


#Region "DragDrop"

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.StringFormat) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub


    Private Sub DataGridView1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop
        Dim previousAllowedEvents As Boolean = _AllowEvents
        Dim sourceClass As DTOContactClass = Nothing
        _AllowEvents = False

        Dim exs As New List(Of Exception)
        Dim oTargetCell As DataGridViewCell = Nothing
        If e.Data.GetDataPresent(DataFormats.StringFormat) Then
            Dim src As String = e.Data.GetData(DataFormats.StringFormat)
            'RaiseEvent RequestToAddNewTel(Me, New MatEventArgs(src))
        End If

        _AllowEvents = previousAllowedEvents
    End Sub


#End Region


    Protected Class ControlItem
        Property Source As DTOBaseTel

        Property Num As String
        Property Obs As String
        Public Property Icon As Icons

        Public Enum Icons
            NotSet
            tel
            fax
            movil
            email
            PrivateTel
            PrivateMovil
            emailBadMail
            emailNoMailings
            emailEfras
            Obsolet
        End Enum

        Public Sub New(value As DTOContactTel)
            MyBase.New()
            _Source = value
            With value
                _Num = DTOContactTel.Formatted(value)
                _Obs = value.Obs
                _Icon = Icons.tel
                If .Privat Then
                    If .Cod = DTOContactTel.Cods.tel Then _Icon = Icons.PrivateTel
                    If .cod = DTOContactTel.Cods.movil Then _Icon = Icons.PrivateMovil
                Else
                    _Icon = .cod
                End If
            End With

        End Sub

        Public Sub New(value As DTOUser)
            MyBase.New()
            _Source = value
            With value
                _Num = .EmailAddress
                _Obs = .DisplayObs
                _Icon = Icons.email

                If .Obsoleto Then
                    _Icon = Icons.Obsolet
                ElseIf .BadMail IsNot Nothing Then
                    _Icon = Icons.emailBadMail
                ElseIf .EFras Then
                    _Icon = Icons.emailEfras
                ElseIf .NoNews Then
                    _Icon = Icons.emailNoMailings
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


