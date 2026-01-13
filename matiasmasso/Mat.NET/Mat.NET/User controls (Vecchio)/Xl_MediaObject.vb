Public Class Xl_MediaObject

    Private _MediaObject As MediaObject
    Private _FileDocument As FileDocument

    Public Property IsDirty As Boolean

    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        PictureBox1.AllowDrop = True
        SetContextMenu()
    End Sub

    Public Shadows Sub Load(oFileDocument As FileDocument)
        _FileDocument = oFileDocument
        If _FileDocument IsNot Nothing Then
            _MediaObject = _FileDocument.MediaObject
        End If
        Refresca()
    End Sub

    Public Shadows Sub Load(oMediaObject As MediaObject, Optional BlSetDirty As Boolean = False)
        _MediaObject = oMediaObject
        If BlSetDirty Then _IsDirty = True
        Refresca()
    End Sub


    Public ReadOnly Property MediaObject As MediaObject
        Get
            Return _MediaObject
        End Get
    End Property

    Private Sub Refresca()
        If _MediaObject Is Nothing Then
            PictureBox1.Image = Nothing
            TextBox1.Clear()
        Else
            PictureBox1.Image = _MediaObject.Thumbnail
            TextBox1.Text = _MediaObject.ToString
        End If
        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        If _MediaObject IsNot Nothing Then
            oContextMenu.Items.Add("zoom", Nothing, AddressOf Do_Zoom)
            oContextMenu.Items.Add("exportar", Nothing, AddressOf Do_Export)
        End If
        If _FileDocument IsNot Nothing Then
            oContextMenu.Items.Add("copiar enllaç", Nothing, AddressOf Do_CopyLink)
        End If
        oContextMenu.Items.Add("importar", Nothing, AddressOf Do_Import)
        oContextMenu.Items.Add("borrar", Nothing, AddressOf Do_Clear)

        PictureBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Zoom(sender As Object, e As EventArgs)
        root.ShowStream(_MediaObject)
    End Sub

    Private Sub Do_CopyLink(sender As Object, e As EventArgs)
        Clipboard.SetDataObject(_FileDocument.RoutingUrl(True), True)
    End Sub

    Private Sub Do_Clear(sender As Object, e As EventArgs)
        _MediaObject = Nothing
        _IsDirty = True
        RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        Refresca()
    End Sub

    Private Sub Do_Import(sender As Object, e As EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "importar document"
            .Filter = "documents pdf (*.pdf)|*.pdf| tots els documents (*.*)|*.*"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim exs As New List(Of exception)
                _MediaObject = MaxiSrvr.MediaObject.FromFile(.FileName, exs)
                If exs.Count = 0 Then
                    Refresca()
                    _IsDirty = True
                    RaiseEvent AfterUpdate(Me, EventArgs.Empty)
                Else
                    MsgBox("error al importar document" & Environment.NewLine & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            End If
        End With
    End Sub

    Private Sub Do_Export(sender As Object, e As EventArgs)
        Dim sMime As String = _MediaObject.Mime.ToString
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "exportar document"
            .Filter = "documents " & sMime & " (*." & sMime & ")|*." & sMime & "| tots els documents (*.*)|*.*"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then

                Dim exs As New List(Of exception)
                If Not BLL.FileSystemHelper.SaveStream(_MediaObject.Stream, exs, .FileName) Then
                    UIHelper.WarnError(exs, "error al desar el fitxer")
                End If

            End If
        End With
    End Sub



    Private Sub PictureBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(maxisrvr.BigFileNew))) Then
            e.Effect = DragDropEffects.Copy
        Else
            '    or none of the above
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub PictureBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        Dim oMediaObjects As MediaObjects = Nothing
        Dim oStream As Byte() = Nothing
        Dim exs As New List(Of exception)
        If DragDropHelper.GetFileDropMediaObjects(e, oMediaObjects, exs) Then
            _MediaObject = oMediaObjects.First
            Refresca()
            IsDirty = True
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        Else
            MsgBox("error al importar fitxer" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub



End Class
