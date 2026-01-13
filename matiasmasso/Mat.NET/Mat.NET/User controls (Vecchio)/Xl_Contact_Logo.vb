

Public Class Xl_Contact_Logo
    Private mContact As Contact = Nothing
    Private _Contact As DTOContact
    Private mEnableChanges As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Shadows Sub Load(value As DTOContact)
        _Contact = value
        mContact = New Contact(value.Guid)
        If _Contact Is Nothing Then
            PictureBox1.Image = My.Resources.DefaultContact
            PictureBox1.ContextMenuStrip = New ContextMenuStrip
        Else
            If _Contact.Logo Is Nothing Then
                PictureBox1.Image = My.Resources.DefaultContact
            Else
                PictureBox1.Image = _Contact.Logo
            End If
            PictureBox1.AllowDrop = True
            SetContextMenu()
        End If
    End Sub

    Public Property Contact() As Contact
        Get
            Return mContact
        End Get
        Set(ByVal value As Contact)
            If value IsNot Nothing Then
                mContact = value
                _Contact = New DTOContact(value.Guid)
                If mContact Is Nothing Then
                    PictureBox1.Image = My.Resources.DefaultContact
                    PictureBox1.ContextMenuStrip = New ContextMenuStrip
                Else
                    If mContact.Img48 Is Nothing Then 'If mContact.Img48 Is Nothing Then
                        PictureBox1.Image = My.Resources.DefaultContact
                    Else
                        PictureBox1.Image = mContact.Img48
                    End If
                    PictureBox1.AllowDrop = True
                    SetContextMenu()
                End If
            End If
        End Set
    End Property

    Public WriteOnly Property EnableChanges() As Boolean
        Set(ByVal value As Boolean)
            mEnableChanges = value
        End Set
    End Property

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItem As ToolStripMenuItem = Nothing



        If mContact IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("contacte...")
            oContextMenu.Items.Add(oMenuItem)

            Dim oMenuContact As New Menu_Contact(mContact)
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

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        PictureBox1.Image = mContact.Img48
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        Dim oFrm As New Frm_Contact(mContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub ChangeContact(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sNewKey As String = InputBox("nou contacte:", "CANVIAR CONTACTE")
        Dim oContact As Contact = Finder.FindContact(mContact.Emp, sNewKey)
        Me.Contact = oContact
        RaiseEvent AfterUpdate(oContact, EventArgs.Empty)
    End Sub

    Private Sub PictureBox1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        Dim fileNames() As String = Nothing
        Dim oBigFiles As New ArrayList

        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
                fileNames = e.Data.GetData(DataFormats.FileDrop)
                For Each sFilename As String In fileNames
                    ' get the actual raw file into memory
                    Dim oFileStream As New System.IO.FileStream(sFilename, IO.FileMode.Open)
                    ' allocate enough bytes to hold the raw data
                    Dim oBinaryReader As New IO.BinaryReader(oFileStream)
                    Dim oStream As Byte() = oBinaryReader.ReadBytes(oFileStream.Length)
                    oBinaryReader.Close()
                    oFileStream.Close()

                    Dim oBigFile As New maxisrvr.BigFileNew()
                    root.LoadBigFile(oBigFile, oStream, sFilename)
                    oBigFiles.Add(oBigFile)
                Next

            ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
                '
                ' the first step here is to get the filename
                ' of the attachment and
                ' build a full-path name so we can store it 
                ' in the temporary folder
                '
                ' set up to obtain the FileGroupDescriptor 
                ' and extract the file name
                Dim theStream As System.IO.MemoryStream = e.Data.GetData("FileGroupDescriptor")
                Dim fileGroupDescriptor(512) As Byte
                theStream.Read(fileGroupDescriptor, 0, 512)

                ' used to build the filename from the FileGroupDescriptor block
                Dim sfilename As String = ""
                For i As Integer = 76 To 512
                    If fileGroupDescriptor(i) = 0 Then Exit For
                    sfilename = sfilename & Convert.ToChar(fileGroupDescriptor(i))
                Next
                theStream.Close()
                '
                ' Second step:  we have the file name.  
                ' Now we need to get the actual raw
                ' data for the attached file .
                '

                ' get the actual raw file into memory
                Dim oMemStream As System.IO.MemoryStream = e.Data.GetData("FileContents", True)
                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oMemStream)
                Dim oStream As Byte() = oBinaryReader.ReadBytes(oMemStream.Length)
                oBinaryReader.Close()
                Dim oBigFile As New maxisrvr.BigFileNew()
                root.LoadBigFile(oBigFile, oStream, sfilename)
                oBigFiles.Add(oBigFile)
            Else
                MsgBox("format desconegut")
                Exit Sub
            End If


            AfterFileDropped(oBigFiles)


        Catch ex As Exception
            MsgBox("Error in DragDrop function: " + ex.Message)
        Finally
            CType(sender, PictureBox).BorderStyle = Windows.Forms.BorderStyle.None
            'TextBox1.BackColor = mDefaultBackColor
        End Try
    End Sub

    Private Sub PictureBox1_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
            CType(sender, PictureBox).BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            CType(sender, PictureBox).BorderStyle = Windows.Forms.BorderStyle.Fixed3D
            '    or none of the above
        Else
            CType(sender, PictureBox).BorderStyle = Windows.Forms.BorderStyle.None
        End If
    End Sub

    Private Sub PictureBox1_DragLeave(sender As Object, e As System.EventArgs) Handles PictureBox1.DragLeave
        CType(sender, PictureBox).BorderStyle = Windows.Forms.BorderStyle.None
    End Sub


    Private Sub PictureBox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            PictureBox1.DoDragDrop(mContact, DragDropEffects.Copy)
        End If
    End Sub

    Private Sub AfterFileDropped(ByVal oBigFiles As ArrayList)
        For Each oBigFile As maxisrvr.BigFileNew In oBigFiles
            Select Case oBigFile.MimeCod
                Case DTOEnums.MimeCods.Xml
                    Dim oDoc As Xml.XmlDocument = BLL.FileSystemHelper.GetXMLfromByteStream(oBigFile.Stream)
                    ImportedXMLFileManager(oDoc)
                Case Else
                    ShowFileImport(oBigFile, mContact)
            End Select
        Next
    End Sub

    Private Sub UploadImage(sender As Object, e As System.EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "importar imatge Gif 150x48 pixels"
            .Filter = "imatge Gif 150x48 pixels (*.Gif)|*.gif"
            If .ShowDialog Then
                Dim oImg As Image = Image.FromFile(.FileName)
                Dim Validate As Boolean = (oImg.Width = 150 And oImg.Height = 48)
                If Validate Then
                    Dim exs as New List(Of exception)
                    mContact.Img48 = oImg
                    mContact.UpdateClx( exs)
                    RefreshRequest(sender, e)
                Else
                    MsgBox("imatge no importada per no ser de les mides correctes." & vbCrLf & "La imatge seleccionada fa " & oImg.Width & " x " & oImg.Height & " pixels." & vbCrLf & "La mida requerida es de 150 x 48 pixels.", MsgBoxStyle.Exclamation, "MAT.NET")
                End If
            End If
        End With
    End Sub
End Class
