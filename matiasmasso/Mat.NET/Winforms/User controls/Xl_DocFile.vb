Imports System.IO

Public Class Xl_DocFile
    Private _DocFile As DTODocFile
    Property IsDirty As Boolean
    Property IsInedit As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        PictureBox1.AllowDrop = True
    End Sub

    Public Shadows Async function Load(oDocFile As DTODocFile) as task
        _DocFile = oDocFile
        SetContextMenu()
        Try
            If _DocFile Is Nothing Then
                PictureBox1.Image = Nothing
                TextboxFeatures.Clear()
                SetContextMenu()
            Else
                Dim exs As New List(Of Exception)
                If oDocFile.Hash > "" Then
                    If oDocFile.Thumbnail Is Nothing Then
                        Dim oImage = FEB2.DocFile.Thumbnail(exs, oDocFile.Hash)
                        If exs.Count = 0 Then
                            PictureBox1.Image = LegacyHelper.ImageHelper.Converter(Await FEB2.DocFile.Thumbnail(exs, oDocFile.hash))
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        PictureBox1.Image = LegacyHelper.ImageHelper.Converter(oDocFile.Thumbnail)
                    End If

                    Dim sFeatures = DTODocFile.Features(_DocFile)
                    If sFeatures.isNotEmpty Then
                        TextboxFeatures.Text = DTODocFile.Features(_DocFile)
                        TextboxFeatures.Visible = True
                    Else
                        TextboxFeatures.Visible = False
                    End If
                End If
                SetContextMenu()
            End If
        Catch ex As Exception
            UIHelper.WarnError(ex)
        End Try
    End function

    Public ReadOnly Property Value
        Get
            Return _DocFile
        End Get
    End Property

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        If _DocFile IsNot Nothing Then
            Dim oMenu As New Menu_DocFile(_DocFile)
            oContextMenu.Items.AddRange(oMenu.Range)
            oContextMenu.Items.Add("Clear", Nothing, AddressOf Do_Clear)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("importar", Nothing, AddressOf Do_ImportaFileAsync)

        PictureBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Async Sub Do_Clear()
        Await Load(Nothing)
        _IsDirty = True
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Browse()
        UIHelper.ShowPdf(_DocFile)
    End Sub


    Private Async Sub Do_ImportaFileAsync()
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "documents pdf (*.pdf)|*.pdf|tots els documents|*.*"
            If .ShowDialog = DialogResult.OK Then
                ProgressBar1.Visible = True
                TextboxFeatures.Visible = False
                Try
                    Dim oByteArray As Byte() = System.IO.File.ReadAllBytes(.FileName)
                    Dim oMime = MimeHelper.GetMimeFromExtension(.FileName)
                    _DocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray, oMime)
                    If exs.Count = 0 Then
                        Await DisplayFileOrServerCopy()
                        ProgressBar1.Visible = False
                        TextboxFeatures.Visible = True
                    Else
                        ProgressBar1.Visible = False
                        TextboxFeatures.Visible = True
                        UIHelper.WarnError(exs)
                    End If

                Catch ex As Exception
                    ProgressBar1.Visible = False
                    TextboxFeatures.Visible = True
                    UIHelper.WarnError(ex)
                End Try
                'If Await ImportaStreamAsync(oByteArray, exs) Then
                'RaiseEvent AfterUpdate(Me, New MatEventArgs(_DocFile))
                'Else
                'UIHelper.WarnError(exs, "error al importar fitxer")
                'End If
            End If
        End With
    End Sub

    Private Async Function ImportaStreamAsync(oByteArray As Byte(), exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean

        Dim sHash As String = CryptoHelper.HashMD5(oByteArray)

        Dim oDocfile = Await FEB2.DocFile.Find(sHash, exs)
        If exs.Count = 0 Then
            If oDocfile Is Nothing Then
                oDocfile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray, MimeCods.NotSet)
                If exs.Count = 0 Then
                    _DocFile = oDocfile
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_DocFile))
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                _DocFile = oDocfile
                Await Load(_DocFile)
                retval = True
            End If
        End If

        Return retval
    End Function


    Private Sub PictureBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(DTODocFile))) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Async Sub PictureBox1_DragDropAsync(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        Dim exs As New List(Of Exception)
        Dim oByteArray As Byte() = Nothing
        Dim sMime As String = ""
        If sMime = ".lnk" Then
            UIHelper.WarnError("Estàs arrossegant un enllaç en lloc del document")
        Else
            ProgressBar1.Visible = True
            TextboxFeatures.Visible = False
            If GetDroppedFile(e, oByteArray, sMime, exs) Then
                Dim oMime = MimeHelper.GetMimeFromExtension("." & sMime)
                _DocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray, oMime)
                If exs.Count = 0 Then
                    Await DisplayFileOrServerCopy()
                    ProgressBar1.Visible = False
                    TextboxFeatures.Visible = True
                Else
                    ProgressBar1.Visible = False
                    TextboxFeatures.Visible = True
                    UIHelper.WarnError(exs)
                End If

                'If Await ImportaStreamAsync(oByteArray, exs) Then
                'RaiseEvent AfterUpdate(Me, New MatEventArgs(_DocFile))
                'Else
                'UIHelper.WarnError(exs, "error al arrossegar fitxer")
                'End If
            Else
                UIHelper.WarnError(exs, "error al arrossegar fitxer")
            End If
        End If
    End Sub

    Private Async Function DisplayFileOrServerCopy() As Task
        Dim exs As New List(Of Exception)
        Dim oServerCopy As DTODocFile = Await FEB2.DocFile.Find(_DocFile.Hash, exs)
        If exs.Count = 0 Then
            If oServerCopy Is Nothing Then
                _IsInedit = True
                TextboxFeatures.BackColor = Color.LightYellow
                PictureBox1.Image = LegacyHelper.ImageHelper.Converter(_DocFile.Thumbnail)
                TextboxFeatures.Text = DTODocFile.Features(_DocFile)
                SetContextMenu()
            Else
                _IsInedit = False
                _DocFile.FchCreated = oServerCopy.FchCreated
                TextboxFeatures.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption)
                Await Load(_DocFile)
            End If

            _IsDirty = True
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_DocFile))
        Else
            UIHelper.WarnError(exs, "Error al verificar si al servidor ja hi ha una copia d'aquest document")
        End If
    End Function

    Shared Function GetDroppedFile(ByVal e As System.Windows.Forms.DragEventArgs, ByRef oByteArray As Byte(), ByRef sMime As String, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
                Dim sFilenames As String() = e.Data.GetData(DataFormats.FileDrop)
                Dim sFilename As String = sFilenames(0)
                sMime = System.IO.Path.GetExtension(sFilename)
                oByteArray = File.ReadAllBytes(sFilename)
                retval = True

            ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
                '
                Dim theStream As System.IO.MemoryStream = e.Data.GetData("FileGroupDescriptor")
                Dim fileGroupDescriptor(512) As Byte
                theStream.Read(fileGroupDescriptor, 0, 512)

                ' used to build the filename from the FileGroupDescriptor block
                Dim sFilename As String = ""
                For i As Integer = 76 To 512
                    If fileGroupDescriptor(i) = 0 Then Exit For
                    sFilename = sFilename & Convert.ToChar(fileGroupDescriptor(i))
                Next
                theStream.Close()
                sMime = System.IO.Path.GetExtension(sFilename)

                '
                ' get the actual raw file into memory
                Dim oMemStream As System.IO.MemoryStream = e.Data.GetData("FileContents", True)

                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oMemStream)
                oByteArray = oBinaryReader.ReadBytes(oMemStream.Length)
                oBinaryReader.Close()
                retval = True

            Else
                exs.Add(New Exception("l'element arrossegat no ha estat identificat com a fitxer"))
                retval = False
            End If

        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function



End Class
