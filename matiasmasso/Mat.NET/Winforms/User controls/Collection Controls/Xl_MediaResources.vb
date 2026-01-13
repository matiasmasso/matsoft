Public Class Xl_MediaResources
    Private _Product As DTOProduct
    Private _items As List(Of DTOMediaResource)
    Private _menuItemObsolets As ToolStripMenuItem
    Private _CancelRequest As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oProduct As DTOProduct, items As List(Of DTOMediaResource))
        _Product = oProduct
        _items = items
        _menuItemObsolets = New ToolStripMenuItem("inclou obsolets")
        Xl_ProgressBar1.Visible = False

        AddHandler _menuItemObsolets.CheckedChanged, AddressOf onMenuitemObsolets_CheckedChanged
        With _menuItemObsolets
            .CheckOnClick = True
            .CheckState = CheckState.Unchecked
        End With

        With ListView1
            .AllowDrop = True
            .LargeImageList = New ImageList()
            .LargeImageList.ImageSize = New Size(DTOMediaResource.THUMBWIDTH, DTOMediaResource.THUMBHEIGHT)
            .Items.Clear()
        End With

        For Each oCod In [Enum].GetValues(GetType(DTOMediaResource.Cods))
            Dim oGroup As New ListViewGroup(oCod.ToString())
            oGroup.Tag = oCod
            ListView1.Groups.Add(oGroup)
        Next

        For Each item As DTOMediaResource In items
            If item.Thumbnail Is Nothing Then item.Thumbnail = LegacyHelper.ImageHelper.Converter(My.Resources.NoImg140)
            ListView1.LargeImageList.Images.Add(item.Guid.ToString, LegacyHelper.ImageHelper.Converter(item.Thumbnail))
        Next

        refresca()
    End Sub

    Private Sub onMenuitemObsolets_CheckedChanged()
        refresca()
    End Sub

    Private Sub refresca()
        'Xl_ProgressBar1.Visible = True
        ListView1.Items.Clear()
        For Each item As DTOMediaResource In _items
            If _menuItemObsolets.Checked Or item.Obsolet = False Then
                Dim oListViewItem As ListViewItem = GetListViewItem(item)
                ListView1.Items.Add(oListViewItem)
            End If
            'Xl_ProgressBar1.ShowProgress(0, _items.Count, _items.IndexOf(item) / _items.Count, item.Nom, _CancelRequest)
            If _CancelRequest Then Exit For
        Next
        SetContextMenu()
        'Xl_ProgressBar1.Visible = False

    End Sub

    Private Function SelectedValues() As List(Of DTOMediaResource)
        Dim retval As New List(Of DTOMediaResource)
        For Each item As ListViewItem In ListView1.SelectedItems
            retval.Add(item.Tag)
        Next
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim items As List(Of DTOMediaResource) = SelectedValues()

        If items.Count > 0 Then
            Dim oMenu_MediaResource As New Menu_MediaResource(items, AddressOf Xl_ProgressBar1.ShowProgress)
            AddHandler oMenu_MediaResource.AfterUpdate, AddressOf RefreshRequest
            AddHandler oMenu_MediaResource.RequestToToggleProgressBar, AddressOf ToggleProgressBar
            oContextMenu.Items.AddRange(oMenu_MediaResource.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add(_menuItemObsolets)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        ListView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub ToggleProgressBar(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToToggleProgressBar(Me, e)
    End Sub

    Private Sub Do_AddNew()
        Dim item = DTOMediaResource.Factory(Current.Session.User, _Product)
        Dim oFrm As New Frm_MediaResource(item)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Function GetListViewItem(item As DTOMediaResource) As ListViewItem
        Dim retval As New ListViewItem(item.Nom, item.Guid.ToString())
        With retval
            .Group = ListView1.Groups(item.Cod)
            .Tag = item
            .ToolTipText = DTODocFile.Features(item)
        End With
        Return retval
    End Function

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_MediaResources_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        SetContextMenu()
    End Sub

    Private Sub Xl_MediaResources_DragEnter(sender As Object, e As DragEventArgs) Handles ListView1.DragEnter
        e.Effect = DragDropHelper.DragEnterFilePresentEffect(e)
    End Sub

    Private Sub Xl_MediaResources_DragDrop(sender As Object, e As DragEventArgs) Handles ListView1.DragDrop
        Dim exs As New List(Of Exception)

        Dim oMediaResources As New List(Of DTOMediaResource)
        If DragDropHelper.GetDroppedMediaResources(e, oMediaResources, Current.Session.User, exs) Then
            If oMediaResources.Count > 0 Then
                Dim oFrm As New Frm_MediaResourceCodSelection(oMediaResources)
                AddHandler oFrm.AfterUpdate, AddressOf onCodSelected
                oFrm.Show()
            End If
        Else
            UIHelper.WarnError(exs, "error al importar fitxers")
        End If
    End Sub

    Private Async Sub onCodSelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim o = Await Upload(exs, e.Argument)
        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Function Upload(exs As List(Of Exception), oMediaResources As List(Of DTOMediaResource)) As Task(Of Boolean)
        Dim sLabel As String = "pujant " & oMediaResources.Count & " fitxers"
        Xl_ProgressBar1.ShowStart(sLabel)
        Application.DoEvents()

        For Each item As DTOMediaResource In oMediaResources
            With item
                .Product = _Product
                .UsrLog.usrLastEdited = Current.Session.User
            End With

            Await FEB2.MediaResource.Update(item, exs)
        Next

        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
        Xl_ProgressBar1.Visible = False
        Return exs.Count = 0
    End Function


End Class
