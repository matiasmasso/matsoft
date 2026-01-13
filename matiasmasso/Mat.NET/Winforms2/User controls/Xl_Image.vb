

Imports System.Resources.ResXFileRef

Public Class Xl_Image
    Private _Title As String
    Private mMaxHeight As Integer
    Private mMaxWidth As Integer
    Private mSizeMode As System.Windows.Forms.PictureBoxSizeMode
    Private mEmptyImageLabelText As String = ""
    Private mFormat As Formats
    Private mStream As Byte()
    Private _RestrictToWidth As Integer
    Private _RestrictToHeight As Integer
    Private _Tooltip As New ToolTip
    Private _MimeCod As MimeCods
    Public Property IsDirty As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Enum Formats
        img
        zip
    End Enum

    Public Sub New()
        InitializeComponent()
        PictureBox1.AllowDrop = True
    End Sub

    Public Shadows Sub Load(oByteArray As Byte(), Optional restrictToWidth As Integer = 0, Optional restrictToHeight As Integer = 0, Optional Title As String = "", Optional oMime As MimeCods = MimeCods.NotSet)
        If oByteArray IsNot Nothing AndAlso oByteArray.Length > 0 Then
            TextBoxInfo.Visible = False
            _RestrictToWidth = restrictToWidth
            _RestrictToHeight = restrictToHeight
            LabelMetadata.Visible = RestrictedDimensions()
            _Title = Title
            PictureBox1.AllowDrop = True

            'Dim ms As New IO.MemoryStream(oByteArray)
            'PictureBox1.Image = System.Drawing.Image.FromStream(ms)
            PictureBox1.Image = BytesToImage(oByteArray)
            If oMime = MimeCods.NotSet Then
                _MimeCod = LegacyHelper.ImageHelper.GetLegacyImageMimeCod(PictureBox1.Image)
            Else
                _MimeCod = oMime
            End If
            DisplaySize()
            SetMenuItems()
        End If
    End Sub


    Public Shadows Sub LoadAsync(url As String, Optional restrictToWidth As Integer = 0, Optional restrictToHeight As Integer = 0, Optional Title As String = "")
        _RestrictToWidth = restrictToWidth
        _RestrictToHeight = restrictToHeight
        LabelMetadata.Visible = RestrictedDimensions()
        _Title = Title
        PictureBox1.AllowDrop = True
        PictureBox1.LoadAsync(url)
        TextBoxInfo.Visible = False
        DisplaySize()
        SetMenuItems()
    End Sub

    Public Function MimeCod() As MimeCods
        Return _MimeCod
    End Function

    Private Function RestrictedDimensions() As Boolean
        Return _RestrictToWidth <> 0 Or _RestrictToHeight <> 0
    End Function

    Public WriteOnly Property Title() As String
        Set(ByVal Value As String)
            _Title = Value
        End Set
    End Property

    Public Property ZipStream() As Byte()
        Get
            Return mStream
        End Get
        Set(ByVal Value As Byte())
            If Value IsNot Nothing Then
                mStream = Value
                mFormat = Formats.zip
                SetMenuItems()
            End If
        End Set
    End Property


    Public Property Bitmap() As System.Drawing.Bitmap
        Get
            Return PictureBox1.Image
        End Get
        Set(ByVal Value As System.Drawing.Bitmap)
            PictureBox1.Image = Value
            TextBoxInfo.Visible = False
            DisplaySize()
            SetMenuItems()
        End Set
    End Property

    Public ReadOnly Property Format() As Formats
        Get
            Return mFormat
        End Get
    End Property

    Public Property EmptyImageLabelText() As String
        Get
            Return mEmptyImageLabelText
        End Get
        Set(ByVal value As String)
            mEmptyImageLabelText = value
        End Set
    End Property

    Private Sub SetMenuItems()
        Dim BlEmptyImg As Boolean = PictureBox1.Image Is Nothing
        'LabelSize.Visible = BlEmptyImg

        ExportarToolStripMenuItem.Enabled = Not BlEmptyImg
        CopiarToolStripMenuItem.Enabled = Not BlEmptyImg
        EliminarToolStripMenuItem.Enabled = Not BlEmptyImg

        If Me.EmptyImageLabelText > "" Then
            ImportarToolStripMenuItem.Text = mEmptyImageLabelText
        Else
            'LabelSize.Text = sSize
            If RestrictedDimensions() Then
                ImportarToolStripMenuItem.Text = String.Format("importar {0}x{1} px", _RestrictToWidth, _RestrictToHeight)
            Else
                ImportarToolStripMenuItem.Text = "importar"
            End If
        End If

        Dim iData As System.Windows.Forms.IDataObject = System.Windows.Forms.Clipboard.GetDataObject()
        Dim ImageInClipboard As Boolean = iData.GetDataPresent(System.Windows.Forms.DataFormats.Bitmap)
        PegarToolStripMenuItem.Enabled = ImageInClipboard
    End Sub


    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim oImg As System.Drawing.Image = PictureBox1.Image
            If Not oImg Is Nothing Then
                PictureBox1.DoDragDrop(oImg, System.Windows.Forms.DragDropEffects.Copy)
            End If
        End If
    End Sub

    Private Sub PictureBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter
        e.Effect = DragDropHelper.DragEnterFilePresentEffect(e)
    End Sub

    Private Sub PictureBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        Dim exs As New List(Of Exception)

        Dim oImage = DragDropHelper.GetDroppedImage(exs, e)
        _MimeCod = DragDropHelper.GetDroppedMime(exs, e)
        If exs.Count = 0 Then
            ValidateAndLoadImage(oImage)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DisplaySize()
        Dim oImage As System.Drawing.Image = PictureBox1.Image
        With LabelMetadata
            If IsNullImage(oImage) Then
                .Text = String.Format("{0}x{1}", _RestrictToWidth, _RestrictToHeight)
                .ForeColor = System.Drawing.Color.Gray
                .BackColor = System.Drawing.Color.White
                _Tooltip.SetToolTip(PictureBox1, "")
                _Tooltip.SetToolTip(LabelMetadata, "")
            Else
                .Text = String.Format("{0}x{1}", oImage.Width, oImage.Height)
                If ValidateImage(oImage) Then
                    .ForeColor = System.Drawing.Color.Black
                    .BackColor = System.Drawing.Color.White
                    _Tooltip.SetToolTip(PictureBox1, "")
                    _Tooltip.SetToolTip(LabelMetadata, "")
                Else
                    .ForeColor = System.Drawing.Color.White
                    .BackColor = System.Drawing.Color.Red
                    _Tooltip.SetToolTip(PictureBox1, SizeWarningText(oImage))
                    _Tooltip.SetToolTip(LabelMetadata, SizeWarningText(oImage))
                End If
            End If

            .BringToFront()
        End With
    End Sub



    Private Function ValidateImage(oImage As System.Drawing.Image) As Boolean
        Dim retval As Boolean = IsNullImage(oImage) Or ((_RestrictToWidth = 0 Or _RestrictToWidth = oImage.Width) And (_RestrictToHeight = 0 Or _RestrictToHeight = oImage.Height))
        Return retval
    End Function

    Private Function IsNullImage(oImage As System.Drawing.Image) As Boolean
        Return oImage Is Nothing OrElse (oImage.Width = 1 And oImage.Height = 1)
    End Function

    Private Function SizeWarningText(oImage As System.Drawing.Image) As String
        Dim retval As String = ""
        If (_RestrictToWidth <> 0 And _RestrictToHeight <> 0) Then
            retval = String.Format("Mida incorrecta. La imatge ha de ser de {0}x{1} pixels", _RestrictToWidth, _RestrictToHeight)
        ElseIf _RestrictToWidth <> 0 Then
            retval = String.Format("Mida incorrecta. L'amplada de la imatge ha de ser de {0} pixels", _RestrictToWidth)
        ElseIf _RestrictToHeight <> 0 Then
            retval = String.Format("Mida incorrecta. L'alçada de la imatge ha de ser de {0} pixels", _RestrictToHeight)
        End If
        Return retval
    End Function

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick,
        TextBoxInfo.DoubleClick
        GetImgFromFile()
    End Sub

    Private Sub GetImgFromFile()
        Dim oDlg As New System.Windows.Forms.OpenFileDialog
        Dim oResult As System.Windows.Forms.DialogResult

        With oDlg
            .Title = _Title
            .Filter = "Totes les imatges |*.gif;*.jpg;*.jpeg;*.png;*.bmp;*.ico|Tots els fitxers (*.*)|*.*"
            .FilterIndex = 4
            oResult = .ShowDialog
            Select Case oResult
                Case System.Windows.Forms.DialogResult.OK
                    Dim exs As New List(Of Exception)
                    If FileSystemHelper.GetStreamFromFile(.FileName, mStream, exs) Then
                        Dim sExtension As String = .FileName.Substring(.FileName.LastIndexOf(".")).ToLower
                        _MimeCod = MimeHelper.GetMimeFromExtension(sExtension)
                        Select Case sExtension
                            Case ".jpg", ".jpeg", ".gif", ".png"
                                mFormat = Formats.img
                                Dim oImage As System.Drawing.Image = System.Drawing.Image.FromFile(.FileName)
                                ValidateAndLoadImage(oImage)
                            Case ".Zip"
                                mFormat = Formats.zip
                                PictureBox1.Image = My.Resources.Zip86
                                DisplaySize()
                                RaiseEvent AfterUpdate(Me, New MatEventArgs(PictureBox1.Image))
                        End Select
                    Else
                        MsgBox("error al importar la imatge" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
                    End If
            End Select
        End With
    End Sub

    Public Sub Clear()
        PictureBox1.Image = New Bitmap(1, 1)
        TextBoxInfo.Visible = True
        DisplaySize()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(PictureBox1.Image))
    End Sub



    Public Function ByteArray() As Byte()
        Return ImageToByte(PictureBox1.Image)
    End Function
    Private Sub ImportarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportarToolStripMenuItem.Click
        GetImgFromFile()
        _IsDirty = True
        SetMenuItems()
    End Sub

    Private Sub CopiarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CopiarToolStripMenuItem.Click
        Dim oImg As System.Drawing.Image = PictureBox1.Image
        System.Windows.Forms.Clipboard.SetDataObject(oImg, True)
    End Sub

    Public Shared Function ImageToByte(ByVal img As Image) As Byte()
        Dim converter As ImageConverter = New ImageConverter()
        Return CType(converter.ConvertTo(img, GetType(Byte())), Byte())
    End Function

    Private Shared Function BytesToImage(bytes As Byte())
        Dim ms As New IO.MemoryStream(bytes)
        Dim retval As Image = Image.FromStream(ms)
        Return retval
    End Function

    Private Sub ExportarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExportarToolStripMenuItem.Click
        Dim oImg As System.Drawing.Image = PictureBox1.Image
        Dim oFormat As System.Drawing.Imaging.ImageFormat = Imaging.ImageFormat.Jpeg
        Dim sImgFilter As String = "imatges format JPG (*.jpg)|*.jpg;*.jpeg|imatges format GIF (*.gif)|*.gif|tots els arxius|*.*" 'IIf(oFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif), "(*.gif)|*.gif|", "(*.jpg)|*.jpg|") & "tots els arxius (*.*)|*.*|"
        Dim sZipFilter As String = "fitxers comprimits (*.Zip)|*.Zip"
        Dim oDlg As New System.Windows.Forms.SaveFileDialog
        Dim oResult As System.Windows.Forms.DialogResult
        With oDlg
            .Title = "Guardar imatge de producte"
            .FileName = _Title
            .Filter = IIf(mFormat = Formats.zip, sZipFilter, sImgFilter)
            '.FilterIndex = 1
            oResult = .ShowDialog
            Select Case oResult
                Case System.Windows.Forms.DialogResult.OK
                    Try
                        PictureBox1.Image.Save(.FileName)
                        MsgBox("Saved!")
                    Catch ex As Exception
                        MsgBox("error al desar la imatge: " & ex.Message)
                    End Try
            End Select
        End With
    End Sub

    Private Sub PegarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PegarToolStripMenuItem.Click
        Dim iData As System.Windows.Forms.IDataObject = System.Windows.Forms.Clipboard.GetDataObject()
        If iData.GetDataPresent(System.Windows.Forms.DataFormats.Bitmap) Then
            Dim oBitmap As System.Drawing.Bitmap = DirectCast(iData.GetData(System.Windows.Forms.DataFormats.Bitmap), System.Drawing.Bitmap)
            Dim sFileName As String = "C:\tmp\tmpimage"
            Try
                oBitmap.Save(sFileName)
            Catch ex As Exception
                MsgBox("Error al grabar la imatge:" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "MAT.NET")
            End Try
            Dim oImage As System.Drawing.Image = System.Drawing.Image.FromFile(sFileName)
            _MimeCod = LegacyHelper.ImageHelper.GetLegacyImageMimeCod(oImage)
            oBitmap = LegacyHelper.ImageHelper.GetThumbnailToFit(oImage, mMaxWidth, mMaxHeight)
            ValidateAndLoadImage(oBitmap)
        End If
    End Sub

    Private Function ValidateAndLoadImage(oImage As System.Drawing.Image) As Boolean
        If Not ValidateImage(oImage) Then
            Dim rc = MsgBox(SizeWarningText(oImage), MsgBoxStyle.RetryCancel)
            If rc = MsgBoxResult.Cancel Then
                Return False
                Exit Function
            End If
        End If
        PictureBox1.Image = oImage
        TextBoxInfo.Visible = False
        DisplaySize()
        SetMenuItems()
        _IsDirty = True
        RaiseEvent AfterUpdate(Me, New MatEventArgs(oImage))
        Return True
    End Function

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem.Click
        Clear()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub


End Class
