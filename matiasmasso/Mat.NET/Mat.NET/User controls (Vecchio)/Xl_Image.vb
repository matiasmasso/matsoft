Public Class Xl_Image
    Private mTitle As String
    Private mMaxHeight As Integer
    Private mMaxWidth As Integer
    Private mSizeMode As System.Windows.Forms.PictureBoxSizeMode
    Private mEmptyImageLabelText As String = ""
    Private mFormat As Formats
    Private mStream As Byte()

    Public Property IsDirty As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Enum Formats
        img
        zip
    End Enum

    Public WriteOnly Property Title() As String
        Set(ByVal Value As String)
            mTitle = Value
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
            SetMenuItems()
        End Set
    End Property

    Public ReadOnly Property Format() As Formats
        Get
            Return mFormat
        End Get
    End Property

    Public Property MaxHeight() As Integer
        Get
            Return mMaxHeight
        End Get
        Set(ByVal Value As Integer)
            mMaxHeight = Value
        End Set
    End Property

    Public Property MaxWidth() As Integer
        Get
            Return mMaxWidth
        End Get
        Set(ByVal Value As Integer)
            mMaxWidth = Value
        End Set
    End Property

    Public Property SizeMode() As System.Windows.Forms.PictureBoxSizeMode
        Get
            Return PictureBox1.SizeMode
        End Get
        Set(ByVal Value As System.Windows.Forms.PictureBoxSizeMode)
            PictureBox1.SizeMode = Value
        End Set
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

        Dim sSize As String = "(" & Me.Width & "x" & Me.Height & "px)"
        If Me.EmptyImageLabelText > "" Then
            ImportarToolStripMenuItem.Text = mEmptyImageLabelText
        Else
            'LabelSize.Text = sSize
            ImportarToolStripMenuItem.Text = "importar " & sSize
        End If

        Dim iData As Windows.Forms.IDataObject = Windows.Forms.Clipboard.GetDataObject()
        Dim ImageInClipboard As Boolean = iData.GetDataPresent(Windows.Forms.DataFormats.Bitmap)
        PegarToolStripMenuItem.Enabled = ImageInClipboard
    End Sub


    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim oImg As System.Drawing.Image = PictureBox1.Image
            If Not oImg Is Nothing Then
                PictureBox1.DoDragDrop(oImg, Windows.Forms.DragDropEffects.Copy)
            End If
        End If
    End Sub

    Private Sub PictureBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter
        If (e.Data.GetDataPresent(Windows.Forms.DataFormats.FileDrop)) Then
            e.Effect = Windows.Forms.DragDropEffects.Copy
        Else
            e.Effect = Windows.Forms.DragDropEffects.None
        End If
    End Sub

    Private Sub PictureBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        'PictureBox1.Image = e.Data.GetData(DataFormats.Bitmap)

        Dim FileNames() As String = e.Data.GetData(Windows.Forms.DataFormats.FileDrop)
        Dim sFileName As String = FileNames(0)
        Dim oBitmap As System.Drawing.Bitmap = MaxiSrvr.ImageResize(New System.Drawing.Bitmap(sFileName), mMaxHeight, mMaxWidth)
        mStream = BLL.ImageHelper.GetByteArrayFromImg(oBitmap)
        RaiseEvent AfterUpdate(Me, New MatEventArgs(mStream))
        PictureBox1.Image = oBitmap
        _IsDirty = True
        SetMenuItems()
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        GetImgFromFile()
    End Sub

    Private Sub GetImgFromFile()
        Dim oImg As System.Drawing.Image = Nothing
        Dim oDlg As New Windows.Forms.OpenFileDialog
        Dim oResult As Windows.Forms.DialogResult

        With oDlg
            .Title = mTitle
            .Filter = "Totes les imatges |*.gif;*.jpg;*.jpeg;*.png;*.bmp;*.ico|Tots els fitxers (*.*)|*.*"
            .FilterIndex = 4
            oResult = .ShowDialog
            Select Case oResult
                Case Windows.Forms.DialogResult.OK
                    Dim exs as New List(Of exception)
                    If BLL.FileSystemHelper.GetStreamFromFile(.FileName, mStream, exs) Then
                        Dim sExtension As String = .FileName.Substring(.FileName.LastIndexOf(".")).ToLower
                        Select Case sExtension
                            Case ".jpg", ".jpeg", ".gif", ".png"
                                oImg = MaxiSrvr.ImageResize(System.Drawing.Image.FromFile(.FileName), mMaxHeight, mMaxWidth)
                                mFormat = Formats.img
                                PictureBox1.Image = oImg
                                RaiseEvent AfterUpdate(Me, New MatEventArgs(mStream))
                            Case ".zip"
                                mFormat = Formats.zip
                                PictureBox1.Image = My.Resources.Zip86
                                RaiseEvent AfterUpdate(Me, New MatEventArgs(mStream))
                        End Select
                    Else
                        MsgBox("error al importar la imatge" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                    End If
            End Select
        End With
    End Sub

    Public Sub Clear()
        PictureBox1.Image = Nothing
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_Image_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PictureBox1.AllowDrop = True
    End Sub

    Private Sub ImportarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportarToolStripMenuItem.Click
        GetImgFromFile()
        _IsDirty = True
        SetMenuItems()
    End Sub

    Private Sub CopiarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CopiarToolStripMenuItem.Click
        Dim oImg As System.Drawing.Image = PictureBox1.Image
        Windows.Forms.Clipboard.SetDataObject(oImg, True)
    End Sub

    Private Sub ExportarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExportarToolStripMenuItem.Click
        Dim oImg As System.Drawing.Image = PictureBox1.Image
        Dim oFormat As System.Drawing.Imaging.ImageFormat = oImg.RawFormat
        Dim sImgFilter As String = "imatges format JPG (*.jpg)|*.jpg;*.jpeg|imatges format GIF (*.gif)|*.gif|tots els arxius|*.*" 'IIf(oFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif), "(*.gif)|*.gif|", "(*.jpg)|*.jpg|") & "tots els arxius (*.*)|*.*|"
        Dim sZipFilter As String = "fitxers comprimits (*.zip)|*.zip"
        Dim oDlg As New Windows.Forms.SaveFileDialog
        Dim oResult As Windows.Forms.DialogResult
        With oDlg
            .Title = "Guardar imatge de producte"
            .FileName = mTitle
            .Filter = IIf(mFormat = Formats.zip, sZipFilter, sImgFilter)
            '.FilterIndex = 1
            oResult = .ShowDialog
            Select Case oResult
                Case Windows.Forms.DialogResult.OK
                    oImg.Save(.FileName)
            End Select
        End With
    End Sub

    Private Sub PegarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PegarToolStripMenuItem.Click
        Dim iData As Windows.Forms.IDataObject = Windows.Forms.Clipboard.GetDataObject()
        If iData.GetDataPresent(Windows.Forms.DataFormats.Bitmap) Then
            Dim oBitmap As System.Drawing.Bitmap = CType(iData.GetData(Windows.Forms.DataFormats.Bitmap), System.Drawing.Bitmap)
            Dim sFileName As String = "C:\tmp\tmpimage"
            Try
                oBitmap.Save(sFileName)
            Catch ex As Exception
                MsgBox("Error al grabar la imatge:" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "MAT.NET")
            End Try
            oBitmap = maxisrvr.ImageResize(New System.Drawing.Bitmap(sFileName), mMaxHeight, mMaxWidth)
            PictureBox1.Image = oBitmap
            _IsDirty = True
            SetMenuItems()
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem.Click
        Clear()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub
End Class
