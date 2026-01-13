Public Class Xl_MediaResourcesOld
    Inherits ListView

    Private _Product As DTOProduct
    Private _LastClickPoint As Point
    Private _ShowProgress As ProgressBarHandler

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oProduct As DTOProduct, items As List(Of DTOMediaResource), showProgress As ProgressBarHandler)
        _ShowProgress = showProgress

        MyBase.AllowDrop = True
        MyBase.LargeImageList = New ImageList()
        MyBase.LargeImageList.ImageSize = New Size(DTOMediaResource.THUMBWIDTH, DTOMediaResource.THUMBHEIGHT)
        MyBase.Items.Clear()

        For Each oCod In [Enum].GetValues(GetType(DTOMediaResource.Cods))
            Dim oGroup As New ListViewGroup(oCod.ToString)
            oGroup.Tag = oCod
            MyBase.Groups.Add(oGroup)
        Next


        For Each item As DTOMediaResource In items
            Try
                MyBase.LargeImageList.Images.Add(item.Hash, item.Thumbnail)
                Dim oListViewItem As ListViewItem = GetListViewItem(item)
                MyBase.Items.Add(oListViewItem)

            Catch ex As Exception
                Stop
            End Try
        Next

        SetContextMenu()
    End Sub

    Private Function SelectedValues() As List(Of DTOMediaResource)
        Dim retval As New List(Of DTOMediaResource)
        For Each item As ListViewItem In MyBase.SelectedItems
            retval.Add(item.Tag)
        Next
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim items As List(Of DTOMediaResource) = SelectedValues()

        If items.Count > 0 Then
            Dim oMenu_MediaResource As New Menu_MediaResource(items, _ShowProgress)
            AddHandler oMenu_MediaResource.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_MediaResource.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        Dim oGroup As ListViewGroup = CurrentGroup()

        Dim item As New DTOMediaResource
        With item
            .Cod = oGroup.Tag
            .Product = _Product
            .FchCreated = Now
        End With
        Dim oFrm As New Frm_MediaResource(item)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Function CurrentGroup() As ListViewGroup
        'Dim htInfo As ListViewHitTestInfo = MyBase.HitTest(MyBase.PointToClient(_LastClickPoint))
        Dim htInfo As ListViewHitTestInfo = MyBase.HitTest(_LastClickPoint)
        Dim lviSibling As ListViewItem = htInfo.Item
        Dim retval As ListViewGroup = lviSibling.Group
        Return retval
    End Function

    Private Function GetListViewItem(item As DTOMediaResource) As ListViewItem
        Dim retval As New ListViewItem(item.Nom, item.Hash)
        With retval
            .Group = MyBase.Groups(item.Cod)
            .Tag = item
            .ToolTipText = BLLMedia.Features(item)
        End With
        Return retval
    End Function

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_MediaResources_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles Me.ItemSelectionChanged
        SetContextMenu()
    End Sub

    Private Sub Xl_MediaResources_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        DragDropHelper.SetDragEnterFilePresentEffect(e)
    End Sub

    Private Sub Xl_MediaResources_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim exs As New List(Of Exception)

        Dim oMediaResources As New List(Of DTOMediaResource)
        If DragDropHelper.GetDroppedMediaResources(e, oMediaResources, exs) Then
            If oMediaResources.Count > 0 Then
                Dim oFrm As New Frm_MediaResourceCodSelection(oMediaResources)
                AddHandler oFrm.AfterUpdate, AddressOf onCodSelected
                oFrm.Show()
            End If
        Else
            UIHelper.WarnError(exs, "error al importar fitxers")
        End If
    End Sub

    Private Sub onCodSelected(sender As Object, e As MatEventArgs)
        Upload(e.Argument)
    End Sub


    Private Sub Upload(oMediaResources As List(Of DTOMediaResource))
        Dim CancelRequest As Boolean
        _ShowProgress(0, 1000, 0, "pujant " & oMediaResources.Count & " fitxers", CancelRequest)
        Application.DoEvents()

        Dim oGroup As ListViewGroup = CurrentGroup()
        Dim oCod As DTOMediaResource.Cods = oGroup.Tag

        For Each item As DTOMediaResource In oMediaResources
            With item
                .Product = _Product
                .Cod = oCod
                .UsrCreated = BLLSession.Current.User
                .FchCreated = Now
            End With

            Dim exs As New List(Of Exception)
            If BLLMediaResource.Update(item, exs) Then
                Try
                    Dim oFtp As BLL.FTPclient = BLLMediaResources.FtpClient
                    Dim sTargetFilename As String = BLLMediaResource.TargetFilename(item)
                    oFtp.Upload(item.Stream, sTargetFilename)
                    _ShowProgress(0, oMediaResources.Count, oMediaResources.IndexOf(item), "pujant fitxers", CancelRequest)
                    If CancelRequest Then Exit For
                Catch ex As Exception
                    BLLMediaResource.Delete(item, exs)
                    UIHelper.WarnError(ex)
                End Try
            End If

        Next

        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_MediaResources_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        _LastClickPoint = New Point(e.X, e.Y)
    End Sub
End Class
