Imports System.Drawing

Public Class Xl_DocFile_Old
    Private _DocFile As DTODocFile

    Public Property IsDirty As Boolean
    Public Event AfterFileDropped(sender As Object, oArgs As MatEventArgs)

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        PictureBox1.AllowDrop = True
        SetContextMenu()
    End Sub

    Public Shadows Sub Load(oDocFile As DTODocFile)
        _DocFile = oDocFile
        If _DocFile IsNot Nothing Then
            If _DocFile.Thumbnail Is Nothing Then
                Dim exs As New List(Of Exception)
                If Not FEB.DocFile.Load(exs, _DocFile, LoadThumbnail:=True) Then
                    UIHelper.WarnError(exs)
                End If
            End If
        End If
        refresca()
    End Sub

    Public ReadOnly Property Value As DTODocFile
        Get
            Return _DocFile
        End Get
    End Property

    Private Sub Do_Delete()
        _DocFile = Nothing
        IsDirty = True
        refresca()
        RaiseEvent AfterFileDropped(Me, New MatEventArgs(_DocFile))
    End Sub

    Private Sub onFileUpdated(sender As Object, e As MatEventArgs)
        _DocFile = e.Argument
        IsDirty = True
        refresca()
        RaiseEvent AfterFileDropped(Me, e)
    End Sub

    Private Sub refresca()
        If _DocFile Is Nothing Then
            PictureBox1.Image = Nothing
            TextBox1.Clear()
            'ElseIf _DocFile.PendingOp = DTODocFile.PendingOps.Delete Then
            'PictureBox1.Image = Nothing
            'TextBox1.Clear()
        Else
            SetPortadaFromSpecificMime()
            Dim ms As New IO.MemoryStream(_DocFile.Thumbnail)
            PictureBox1.Image = Image.FromStream(ms)
            TextBox1.Text = DTODocFile.Features(_DocFile)
            If FEB.DocFile.Exists(_DocFile.hash, _DocFile.fch) Then
                TextBox1.BackColor = System.Drawing.Color.FromKnownColor(KnownColor.GradientInactiveCaption)
            Else
                TextBox1.BackColor = System.Drawing.Color.LightYellow
            End If
        End If

        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If _DocFile IsNot Nothing Then
            Dim oMenuRange As New Menu_DocFile(_DocFile)
            AddHandler oMenuRange.AfterUpdate, AddressOf onFileUpdated
            oContextMenu.Items.AddRange(oMenuRange.Range)
            oContextMenu.Items.Add("-")
            oContextMenu.Items.Add("eliminar", Nothing, AddressOf Do_Delete)
        End If
        oContextMenu.Items.Add("importar", Nothing, AddressOf Do_Import)
        PictureBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Import()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "importar document"
            .Filter = "documents pdf (*.pdf)|*.pdf| tots els documents (*.*)|*.*"
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim exs As New List(Of Exception)

                _DocFile = LegacyHelper.DocfileHelper.Factory(.FileName, exs)
                If exs.Count = 0 Then
                    Dim DtFch As Date = Nothing
                    If FEB.DocFile.Exists(_DocFile.hash, DtFch) Then
                        'recupera data si ja estava registrat
                        _DocFile.fch = DtFch
                    End If
                    _IsDirty = True
                    refresca()

                    Dim oDocFiles As New List(Of DTODocFile)
                    oDocFiles.Add(_DocFile)
                    Dim oArgs As New MatEventArgs(oDocFiles)
                    RaiseEvent AfterFileDropped(Me, oArgs)
                Else
                    UIHelper.WarnError(exs, "error al importar document")
                End If
            End If
        End With
    End Sub

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

    Private Sub PictureBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        Dim oDocFiles As List(Of DTODocFile) = Nothing
        Dim exs As New List(Of Exception)
        If DragDropHelper.GetDroppedDocFiles(e, oDocFiles, exs) Then
            _DocFile = oDocFiles.First
            Dim DtFch As Date = Nothing
            If FEB.DocFile.Exists(_DocFile.hash, DtFch) Then
                'recupera data si ja estava registrat
                _DocFile.fch = DtFch
            End If
            _IsDirty = True
            refresca()

            Dim oArgs As New MatEventArgs(oDocFiles)
            RaiseEvent AfterFileDropped(Me, oArgs)
        Else
            UIHelper.WarnError(exs, "error al importar fitxer")
        End If
    End Sub


    Private Sub SetPortadaFromSpecificMime()
        Dim exs As New List(Of Exception)
        If _DocFile.Thumbnail Is Nothing Then
            Select Case _DocFile.mime
                Case MimeCods.Docx
                    _DocFile.Thumbnail = LegacyHelper.WordHelper.GetImgFromWordFirstPage(_DocFile.Stream, exs)
                Case MimeCods.Xlsx
                    Dim iExcelRows As Integer = 0
                    Dim iExcelCols As Integer = 0
                    With _DocFile
                        '.Thumbnail = MatHelper.Excel.GetImgFromExcelFirstPage(_DocFile.Stream, iExcelCols, iExcelRows)
                        '.Thumbnail = My.Resources.Excel_Big

                        .Thumbnail = MatHelper.Excel.Rasterizer.GetExcelThumbnail(_DocFile.Stream, exs)
                        '.Thumbnail = LegacyHelper.ExcelThumbnail.GetExcelThumbnail(_DocFile.Stream, exs)
                        .Size = New System.Drawing.Size(iExcelCols, iExcelRows)
                    End With
                Case MimeCods.Pptx

            End Select
        End If
    End Sub


End Class
