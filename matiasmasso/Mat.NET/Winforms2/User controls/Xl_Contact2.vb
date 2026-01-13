Public Class Xl_Contact2
    Public Event OnFileDropEventArgs(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event ValueSet(sender As Object, e As MatEventArgs) 'per us intern i evitar 
    Public Event RequestToToggleProgressBar(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private _Contact As DTOContact
    Private _ContactMenu As DTOContactMenu
    Private _Emp As DTOEmp
    Private _Dirty As Boolean
    Private _SearchBy As DTOContact.SearchBy
    Private _PersistSearchBy As Boolean
    Private _DefaultBackColor As Color
    Private _AllowEvents As Boolean = True

    Public Shadows Async Function Load(exs As List(Of Exception), oContact As DTOContact) As Task(Of Boolean)
        _Contact = oContact
        If _Contact IsNot Nothing Then
            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
            _ContactMenu = Await FEB.ContactMenu.Find(exs, oContact.Guid)
            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
            Display()
        End If
        _AllowEvents = True
        Return True
    End Function


    Public Property Emp As DTOEmp
        Get
            If _Emp Is Nothing And Current.Session IsNot Nothing Then
                _Emp = Current.Session.Emp
            End If
            Return _Emp
        End Get
        Set(value As DTOEmp)
            _Emp = value
        End Set
    End Property

    Public Shadows Property [ReadOnly] As Boolean
        Get
            Return TextBox1.ReadOnly
        End Get
        Set(value As Boolean)
            TextBox1.ReadOnly = value
        End Set
    End Property

    Public Property Contact() As DTOContact
        Get
            Return _Contact
        End Get
        Set(ByVal Value As DTOContact)
            If Value Is Nothing And _Contact Is Nothing Then
                'prevent from triggering events
            Else
                _Contact = Value
                RaiseEvent ValueSet(Me, New MatEventArgs(_Contact))
            End If
        End Set
    End Property

    Private Async Sub Xl_Contact2_ValueSet(sender As Object, e As MatEventArgs) Handles Me.ValueSet
        _AllowEvents = False
        Dim exs As New List(Of Exception)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
        _ContactMenu = Await FEB.ContactMenu.Find(exs, _Contact)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
        Display()
        _AllowEvents = True
    End Sub



    Public ReadOnly Property Customer As DTOCustomer
        Get
            Dim retval As DTOCustomer = DTOCustomer.FromContact(_Contact)
            Return retval
        End Get
    End Property

    Public ReadOnly Property Mgz As DTOMgz
        Get
            Dim retval As DTOMgz = DTOMgz.FromContact(_Contact)
            Return retval
        End Get
    End Property

    Private Async Sub TextBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
        If _Dirty Then
            Dim exs As New List(Of Exception)
            e.Cancel = Not Await ValidateContact(exs, Emp)
            If exs.Count > 0 Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Function ValidateContact(exs As List(Of Exception), Optional Emp As DTOEmp = Nothing) As Task(Of Boolean)
        Dim retval As Boolean
        Dim sKey As String = TextBox1.Text

        Dim BlContactUnchanged As Boolean
        If _Contact IsNot Nothing Then
            If _Contact.FullNom = sKey Then
                BlContactUnchanged = True
            End If
        End If

        If BlContactUnchanged Then
            retval = True
        ElseIf sKey = "" Then
            _Contact = Nothing
            retval = True
        Else
            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
            _Contact = Await Finder.FindContactAsync(exs, Current.Session.User, sKey, _SearchBy, AddressOf toggleProgressBar)
            If exs.Count = 0 Then
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
                _ContactMenu = Await FEB.ContactMenu.Find(exs, _Contact)
                ContactFound(_Contact, New System.EventArgs)
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
            Else
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                UIHelper.WarnError(exs)
            End If
        End If
        Return exs.Count = 0
    End Function

    Private Sub toggleProgressBar(visible As Boolean)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(visible))
    End Sub

    Private Sub ContactFound(ByVal sender As Object, ByVal e As System.EventArgs)
        _Contact = DirectCast(sender, DTOContact)
        Display()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Contact))
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        If sender Is Nothing Then
            _Contact = Nothing
            Display()
        Else
            'refresca el mateix
            Try
                Display()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Display()
        SetMenuItems()

        _AllowEvents = False
        If _Contact Is Nothing Then
            TextBox1.Text = ""
            TextBox1.BackColor = System.Drawing.Color.White
        Else
            TextBox1.Text = _Contact.FullNom
            If TextBox1.Text = "" Then TextBox1.Text = _Contact.Nom
            If _ContactMenu IsNot Nothing Then
                _DefaultBackColor = _ContactMenu.BackColor()
                TextBox1.BackColor = _DefaultBackColor
                WarnWrongMail(_ContactMenu.Emails.Count = 0)
            End If
            Application.DoEvents()
        End If

        _AllowEvents = True
        _Dirty = False
    End Sub



    Private Async Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Dim exs As New List(Of Exception)
        Select Case e.KeyCode
            Case Keys.Enter, Keys.Return
                If Not _Contact Is Nothing Then
                    If _Contact.FullNom = TextBox1.Text And Not (_Contact.Guid = Nothing And _Contact.Id = 0) Then
                        Dim oFrm As New Frm_Contact(_Contact)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                        Exit Sub
                    Else
                        If Not Await ValidateContact(exs) Then
                            UIHelper.WarnError(exs)
                        End If
                    End If
                Else
                    If Not Await ValidateContact(exs) Then
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Keys.F1
                Do_AdvancedSearch()
            Case Keys.F3
                AddNew(sender, e)
        End Select
    End Sub

    Private Sub Xl_CLI_Nom_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        MyBase.Height = TextBox1.Height
        TextBox1.Width = MyBase.Width
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If _AllowEvents Then
            'Do_Autocomplete
            _Dirty = True
        End If
    End Sub


    Private Async Sub Do_Autocomplete()
        Dim exs As New List(Of Exception)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
        Dim oArray = Await FEB.Contacts.AutoCompleteString(exs, Current.Session.Emp, TextBox1.Text)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
        If exs.Count = 0 Then
            If oArray.Count < 5 Then
                Dim oAutoCompleteStringCollection As New System.Windows.Forms.AutoCompleteStringCollection
                For Each s As String In oArray
                    oAutoCompleteStringCollection.Add(s)
                Next
                TextBox1.AutoCompleteCustomSource = oAutoCompleteStringCollection
            Else
                TextBox1.AutoCompleteCustomSource = Nothing
                'TextBox1.AutoCompleteSource = AutoCompleteSource.None
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Function SetMenuItemsAsync() As Task
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip

        If _Contact IsNot Nothing Then
            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
            Dim oContactMenu = Await FEB.ContactMenu.Find(exs, _Contact.Guid)
            RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
            Dim oMenuContact As New Menu_Contact(_Contact, oContactMenu)
            AddHandler oMenuContact.AfterUpdate, AddressOf RefreshRequest
            With oContextMenu.Items
                .Clear()
                .AddRange(oMenuContact.Range)
            End With
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add("obrir nou contacte", Nothing, AddressOf AddNew)

        TextBox1.ContextMenuStrip = oContextMenu
    End Function

    Private Sub SetMenuItems()
        Dim oContextMenu As New ContextMenuStrip
        Dim exs As New List(Of Exception)

        If _Contact IsNot Nothing Then
            Dim oMenuContact As New Menu_Contact(_Contact, _ContactMenu)
            AddHandler oMenuContact.AfterUpdate, AddressOf RefreshRequest
            With oContextMenu.Items
                .Clear()
                .AddRange(oMenuContact.Range)
            End With
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add("obrir nou contacte", Nothing, AddressOf AddNew)

        TextBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oContact As New Contact(Current.session.emp)
        Dim oFrm As New Frm_CreateContact()
        AddHandler oFrm.AfterUpdate, AddressOf AfterCreate
        oFrm.Show()
    End Sub

    Private Sub AfterCreate(sender As Object, e As MatEventArgs)
        Me.Contact = e.Argument
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub Xl_Contact_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                Do_AdvancedSearch()
            Case Keys.F3
                AddNew(sender, e)
        End Select
    End Sub

    Private Sub Do_AdvancedSearch()
        Dim oFrm As New Frm_ContactAdvancedSearch(TextBox1.Text)
        AddHandler oFrm.AfterUpdate, AddressOf onSearchAdvance
        oFrm.Show()
    End Sub

    Private Async Sub onSearchAdvance(sender As Object, e As MatEventArgs)
        Dim oContacts As List(Of DTOContact) = e.Argument
        Dim exs As New List(Of Exception)
        If Not Await ValidateContact(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub WarnWrongMail(ByVal BlWrongMail As Boolean)
        If BlWrongMail Then
            With TextBox1
                .Dock = DockStyle.None
                .Left = PictureBoxWrongMail.Width
                .Top = 0
                .Width = MyBase.Width - PictureBoxWrongMail.Width
            End With
            PictureBoxWrongMail.Visible = True
        Else
            TextBox1.Dock = DockStyle.Fill
            PictureBoxWrongMail.Visible = False
        End If
    End Sub



    Public Sub Clear()
        _Contact = Nothing
        TextBox1.Clear()
    End Sub

#Region "DragDrop"

    Private Sub TextBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TextBox1.DragDrop
        Dim exs as New List(Of exception)
        Dim oDocFiles As List(Of DTODocFile) = Nothing
        Dim oTargetCell As DataGridViewCell = Nothing
        If DragDropHelper.GetDroppedDocFiles(e, oDocFiles, exs) Then
            root.ShowFileImport(oDocFiles.First, _Contact)
        Else
            MsgBox("error al importar fitxer" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
        End If
        TextBox1.BackColor = _DefaultBackColor
    End Sub

    Private Sub TextBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TextBox1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
            TextBox1.BackColor = Color.SeaShell
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            TextBox1.BackColor = Color.LemonChiffon
            '    or none of the above
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub TextBox1_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DragLeave
        TextBox1.BackColor = _DefaultBackColor
    End Sub



#End Region

End Class
