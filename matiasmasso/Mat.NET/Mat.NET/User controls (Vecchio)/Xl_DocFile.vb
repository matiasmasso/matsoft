Public Class Xl_DocFile
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
            If _DocFile.IsPersisted = MaxiSrvr.TriState.NotSet Then
                BLL.BLLDocFile.Load(_DocFile)
            End If
        End If
        refresca()
    End Sub

    Public ReadOnly Property Value As DTODocFile
        Get
            Return _DocFile
        End Get
    End Property

    Private Sub onDeleteRequest(sender As Object, e As MatEventArgs)
        _DocFile.PendingOp = DTODocFile.PendingOps.Delete
        IsDirty = True
        refresca()
        RaiseEvent AfterFileDropped(Me, New MatEventArgs(_DocFile))
    End Sub

    Private Sub onFileUpdated(sender As Object, e As MatEventArgs)
        _DocFile = e.Argument
        _DocFile.PendingOp = DTODocFile.PendingOps.Update
        IsDirty = True
        refresca()
        RaiseEvent AfterFileDropped(Me, e)
    End Sub

    Private Sub refresca()
        If _DocFile Is Nothing Then
            PictureBox1.Image = Nothing
            TextBox1.Clear()
        ElseIf _DocFile.PendingOp = DTODocFile.PendingOps.Delete Then
            PictureBox1.Image = Nothing
            TextBox1.Clear()
        Else
            SetPortadaFromSpecificMime()
            PictureBox1.Image = _DocFile.Thumbnail
            TextBox1.Text = BLL_DocFile.Features(_DocFile)
            If _DocFile.Fch = Nothing Then
                TextBox1.BackColor = Color.LightYellow
            Else
                TextBox1.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption)
            End If
        End If

        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuRange As New Menu_DocFile(_DocFile)
        AddHandler oMenuRange.AfterUpdate, AddressOf onFileUpdated
        AddHandler oMenuRange.RequestToDelete, AddressOf onDeleteRequest
        oContextMenu.Items.AddRange(oMenuRange.Range)
        PictureBox1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub PictureBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(maxisrvr.BigFileNew))) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub PictureBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        Dim oDocFiles As List(Of DTODocFile) = Nothing
        Dim exs As New List(Of exception)
        If DragDropHelper.GetDroppedDocFiles(e, oDocFiles, exs) Then
            _DocFile = oDocFiles.First
            _DocFile.PendingOp = DTODocFile.PendingOps.Update
            _IsDirty = True
            refresca()

            Dim oArgs As New MatEventArgs(oDocFiles)
            RaiseEvent AfterFileDropped(Me, oArgs)
        Else
            MsgBox("error al importar fitxer" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub


    Private Sub SetPortadaFromSpecificMime()
        If _DocFile.Thumbnail Is Nothing Then
            Select Case _DocFile.Mime
                Case DTOEnums.MimeCods.Docx
                    _DocFile.Thumbnail = WordHelper.GetImgFromWordFirstPage(_DocFile.Stream)
                Case DTOEnums.MimeCods.Xlsx
                    Dim iExcelRows As Integer = 0
                    Dim iExcelCols As Integer = 0
                    With _DocFile
                        .Thumbnail = ExcelHelper.GetImgFromExcelFirstPage(_DocFile.Stream, iExcelCols, iExcelRows)
                        .Size = New Size(iExcelCols, iExcelRows)
                    End With
                Case DTOEnums.MimeCods.Pptx

            End Select
        End If
    End Sub

End Class
