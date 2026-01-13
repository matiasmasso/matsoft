Public Class Xl_Contact2
    Public Event OnFileDropEventArgs(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private _Contact As DTOContact
    Private _Dirty As Boolean
    Private _DefaultBackColor As Color
    Private _AllowEvents As Boolean = True

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
            _Contact = Value
            Display()
            _AllowEvents = True
        End Set
    End Property

    Private Sub TextBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
        If _Dirty Then
            e.Cancel = Not ValidateContact()
        End If
    End Sub

    Private Function ValidateContact() As Boolean
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
            _Contact = Finder.FindContact2(BLL.BLLApp.Emp, sKey)
            retval = (_Contact IsNot Nothing)
            ContactFound(_Contact, New System.EventArgs)
        End If
        Return retval
    End Function

    Private Sub ContactFound(ByVal sender As Object, ByVal e As System.EventArgs)
        _Contact = CType(sender, DTOContact)
        Display()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Contact))
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        If sender Is Nothing Then
            _Contact = Nothing
        ElseIf TypeOf (sender) Is Contact Then
            Dim oContact As Contact = CType(sender, Contact)
            _Contact = BLL.BLLContact.Find(oContact.Guid)
        Else
            'refresca el mateix
            Try
                Display()
            Catch ex As Exception

            End Try
        End If
        Display()
    End Sub

    Private Sub Display()
        _AllowEvents = False
        If _Contact Is Nothing Then
            TextBox1.Text = ""
            TextBox1.BackColor = System.Drawing.Color.White
        Else
            TextBox1.Text = _Contact.FullNom

            Dim oContact As New Contact(_Contact.Guid)
            _DefaultBackColor = oContact.BackColor
            TextBox1.BackColor = _DefaultBackColor
            WarnWrongMail(Not Emails.ExistFromContact(oContact))
            Application.DoEvents()
        End If


        SetMenuItems()

        _AllowEvents = True
        _Dirty = False
    End Sub



    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter, Keys.Return
                If Not _Contact Is Nothing Then
                    If _Contact.FullNom = TextBox1.Text And Not (_Contact.Guid = Nothing And _Contact.Id = 0) Then

                        Dim oContact As New Contact(_Contact.Guid)
                        Dim oFrm As New Frm_Contact(oContact)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                        Exit Sub
                    Else
                        ValidateContact()
                    End If
                Else
                    ValidateContact()
                End If
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


    Private Sub Do_Autocomplete()
        Dim oArray As ArrayList = MaxiSrvr.Contacts.AutoCompleteString(BLL.BLLApp.Emp, TextBox1.Text)
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
    End Sub


    Private Sub SetMenuItems()
        Dim oContextMenu As New ContextMenuStrip

        If _Contact IsNot Nothing Then
            Dim oMenuContact As New Menu_Contact(_Contact)
            AddHandler oMenuContact.AfterUpdate, AddressOf RefreshRequest
            With oContextMenu.Items
                .Clear()
                .AddRange(oMenuContact.Range)
            End With
        End If

        TextBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oContact As New Contact(BLL.BLLApp.Emp)
        Dim oFrm As New Frm_Contact(oContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub Xl_Contact_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F3
                AddNew(sender, e)
        End Select
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

    Private Sub AfterFileDropped(ByVal oBigFiles As ArrayList)
        For Each oBigFile As MaxiSrvr.BigFileNew In oBigFiles
            Select Case oBigFile.MimeCod
                Case DTOEnums.MimeCods.Xml
                    Dim oDoc As Xml.XmlDocument = BLL.FileSystemHelper.GetXMLfromByteStream(oBigFile.Stream)
                    ImportedXMLFileManager(oDoc)
                Case Else
                    Dim oContact As New Contact(_Contact.Guid)
                    root.ShowFileImport(oBigFile, oContact)
            End Select
        Next
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
            Dim oContact As New Contact(_Contact.Guid)
            root.ShowFileImport(oDocFiles.First, oContact)
        Else
            MsgBox("error al importar fitxer" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
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
