
Public Class Xl_Contact_Logo
    Private _Contact As DTOContact
    Private mEnableChanges As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Shadows Async Function Load(exs As List(Of Exception), value As DTOContact) As Task(Of Boolean)
        _Contact = value
        If _Contact Is Nothing Then
            PictureBox1.Image = My.Resources.DefaultContact
            PictureBox1.ContextMenuStrip = New ContextMenuStrip
        Else
            _Contact.Logo = Await FEB2.Contact.Logo(exs, value)
            If exs.Count = 0 Then
                If _Contact.Logo Is Nothing Then
                    PictureBox1.Image = My.Resources.DefaultContact
                Else
                    PictureBox1.Image = LegacyHelper.ImageHelper.Converter(_Contact.Logo)
                End If
            Else
                PictureBox1.Image = My.Resources.DefaultContact
            End If
            PictureBox1.AllowDrop = True
            SetContextMenu()
        End If
        Return exs.Count = 0
    End Function

    Public Sub Clear()
        PictureBox1.Image = My.Resources.DefaultContact
    End Sub

    Public ReadOnly Property Contact() As DTOContact
        Get
            Return _Contact
        End Get
    End Property

    Public WriteOnly Property EnableChanges() As Boolean
        Set(ByVal value As Boolean)
            mEnableChanges = value
        End Set
    End Property

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItem As ToolStripMenuItem = Nothing

        If _Contact IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("contacte...")
            oContextMenu.Items.Add(oMenuItem)

            Dim oMenuContact As New Menu_Contact(_Contact)
            AddHandler oMenuContact.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenuContact.Range)

            oMenuItem = New ToolStripMenuItem("importar imatge", Nothing, AddressOf UploadImage)
            oContextMenu.Items.Add(oMenuItem)

        End If

        If mEnableChanges Then
            oMenuItem = New ToolStripMenuItem("canviar contacte", Nothing, AddressOf ChangeContact)
            oContextMenu.Items.Add(oMenuItem)
        End If

        PictureBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        PictureBox1.Image = LegacyHelper.ImageHelper.Converter(_Contact.Logo)
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        Dim oFrm As New Frm_Contact(_Contact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub ChangeContact(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sNewKey As String = InputBox("nou contacte:", "CANVIAR CONTACTE")
        Dim exs As New List(Of Exception)
        Dim oContact = Finder.FindContact(exs, Current.Session.User, sNewKey)
        If exs.Count = 0 Then
            Await Me.Load(exs, oContact)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oContact))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub PictureBox1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        Dim fileNames() As String = Nothing
        Dim exs As New List(Of Exception)
        Dim oDocFiles As New List(Of DTODocFile)
        If DragDropHelper.GetDroppedDocFiles(e, oDocFiles, exs) Then
            For Each oDocFile As DTODocFile In oDocFiles
                Select Case oDocFile.Mime
                    Case MimeCods.Xml
                        'Dim oDoc As Xml.XmlDocument = FileSystemHelper.GetXMLfromByteStream(oDocFile.Stream)
                        'ImportedXMLFileManager(oDoc)
                    Case Else
                        ShowFileImport(oDocFile, _Contact)
                End Select
            Next
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub PictureBox1_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
            DirectCast(sender, PictureBox).BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            DirectCast(sender, PictureBox).BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            '    or none of the above
        Else
            DirectCast(sender, PictureBox).BorderStyle = System.Windows.Forms.BorderStyle.None
        End If
    End Sub

    Private Sub PictureBox1_DragLeave(sender As Object, e As System.EventArgs) Handles PictureBox1.DragLeave
        DirectCast(sender, PictureBox).BorderStyle = System.Windows.Forms.BorderStyle.None
    End Sub


    Private Sub PictureBox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If _Contact IsNot Nothing Then
                PictureBox1.DoDragDrop(_Contact, DragDropEffects.Copy)
            End If
        End If
    End Sub



    Private Async Sub UploadImage(sender As Object, e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "importar imatge Gif 150x48 pixels"
            .Filter = "imatge Gif 150x48 pixels (*.Gif)|*.gif"
            If .ShowDialog Then
                Dim oImg As Image = Image.FromFile(.FileName)
                Dim Validate As Boolean = (oImg.Width = 150 And oImg.Height = 48)
                If Validate Then
                    If FEB2.Contact.Load(_Contact, exs) Then
                        _Contact.Logo = LegacyHelper.ImageHelper.Converter(oImg)
                        Dim id = Await FEB2.Contact.Update(exs, _Contact)
                        If exs.Count = 0 Then
                            RefreshRequest(Me, MatEventArgs.Empty)
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    MsgBox("imatge no importada per no ser de les mides correctes." & vbCrLf & "La imatge seleccionada fa " & oImg.Width & " x " & oImg.Height & " pixels." & vbCrLf & "La mida requerida es de 150 x 48 pixels.", MsgBoxStyle.Exclamation, "MAT.NET")
                End If
            End If
        End With
    End Sub
End Class
