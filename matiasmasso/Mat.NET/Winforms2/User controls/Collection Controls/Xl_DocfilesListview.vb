Public Class Xl_DocfilesListview
    Inherits ListView

    Private _Docfiles As List(Of DTODocFile)
    Private _ImageList As ImageList
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oDocfiles As List(Of DTODocFile))
        _Docfiles = oDocfiles
        MyBase.View = View.LargeIcon
        _ImageList = New ImageList
        _ImageList.ImageSize = New Size(48, 48)
        refresca()
    End Sub

    Public Function docfiles() As List(Of DTODocFile)
        Dim retval = MyBase.Items.Cast(Of ListViewItem)().Select(Function(x) DirectCast(x.Tag, DTODocFile)).ToList
        Return retval
    End Function

    Private Sub refresca()
        _AllowEvents = False
        MyBase.Items.Clear()
        _ImageList.Images.Clear()

        For i = 0 To _Docfiles.Count - 1
            Dim oThumbnail = LegacyHelper.ImageHelper.FromBytes(_Docfiles(i).Thumbnail)
            Dim oThumb = LegacyHelper.ImageHelper.GetThumbnailToFit(oThumbnail, 48, 48)
            _ImageList.Images.Add(oThumb)
        Next

        MyBase.LargeImageList = _ImageList
        For i = 0 To _ImageList.Images.Count - 1
            Dim item As New ListViewItem()
            item.ImageIndex = i
            item.Tag = _Docfiles(i)
            MyBase.Items.Add(item)
        Next

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        If MyBase.Items.Count > 0 AndAlso Me.SelectedItems.Count > 0 Then
            Dim item As DTODocFile = Me.SelectedItems(0).Tag

            If item IsNot Nothing Then
                oContextMenu.Items.Add("zoom", Nothing, AddressOf Do_Zoom)
                oContextMenu.Items.Add("eliminar", Nothing, AddressOf Do_Delete)
            End If
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Async Sub Do_Zoom()
        Dim exs As New List(Of Exception)
        Dim oDocfile As DTODocFile = Me.SelectedItems(0).Tag
        If Not Await UIHelper.ShowStreamAsync(exs, oDocfile) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Delete()
        For Each item In Me.SelectedItems
            MyBase.Items.Remove(item)
        Next
        RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.docfiles))
    End Sub

    Private Sub Do_AddNew()
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files|*.*"
            .Multiselect = True
            If .ShowDialog = DialogResult.OK Then
                For i = 0 To .FileNames.Count - 1

                    Dim oDocfile = LegacyHelper.DocfileHelper.Factory(.FileNames(i), exs)
                    If exs.Count = 0 Then
                        _Docfiles.Add(oDocfile)
                    End If

                Next
                refresca()
                RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.docfiles))
            End If
        End With
    End Sub

    Private Sub Xl_DocfilesListview_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles Me.ItemSelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub
End Class
