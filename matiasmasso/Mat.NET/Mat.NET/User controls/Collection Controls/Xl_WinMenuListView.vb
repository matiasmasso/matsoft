Public Class Xl_WinMenuListView
    Inherits ListView

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(items As List(Of DTOWinMenuItem))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        MyBase.Items.Clear()
        MyBase.LargeImageList = New ImageList()
        MyBase.LargeImageList.Images.Clear()
        MyBase.LargeImageList.ImageSize = New System.Drawing.Size(48, 48)

        Dim ImgIdx As Integer
        For Each item As DTOWinMenuItem In items.FindAll(Function(x) x.Cod = DTOWinMenuItem.Cods.Item)
            Dim oLv As New ListViewItem(item.Nom)
            With oLv
                .Tag = item
                If Not item.Icon Is Nothing Then
                    LargeImageList.Images.Add(item.Icon)
                    .ImageIndex = ImgIdx
                    ImgIdx = ImgIdx + 1
                End If
            End With
            MyBase.Items.Add(oLv)
        Next

        SetContextmenu()
        _AllowEvents = True
    End Sub

    Private Sub SetProperties()

    End Sub

    Private Function GetListViewItem(item As DTOWinMenuItem) As ListViewItem
        Dim retval As New ListViewItem(item.Nom)
        If item.Icon IsNot Nothing Then
            LargeImageList.Images.Add(item.Icon)
            retval.ImageIndex = LargeImageList.Images.Count - 1
        End If
        Return retval
    End Function

    Private Sub Xl_WinMenuListView_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Dim oListViewItems As ListView.SelectedListViewItemCollection = MyBase.SelectedItems
        If oListViewItems.Count > 0 Then
            Dim oListViewItem As ListViewItem = oListViewItems(0)
            Dim item As DTOWinMenuItem = oListViewItem.Tag
            If item IsNot Nothing Then
                RaiseEvent onItemSelected(Me, New MatEventArgs(item))
            End If
        End If
    End Sub

    Private Sub Xl_WinMenuListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Me.SelectedIndexChanged
        setcontextmenu()
    End Sub

    Private Function CurrentItm() As DTOWinMenuItem
        Dim retval As DTOWinMenuItem = Nothing
        Dim oListViewItems As ListView.SelectedListViewItemCollection = MyBase.SelectedItems
        If oListViewItems.Count > 0 Then
            Dim oListViewItem As ListViewItem = oListViewItems(0)
            retval = oListViewItem.Tag
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim item As DTOWinMenuItem = CurrentItm()

        If item IsNot Nothing Then
            Select Case item.CustomTarget
                Case DTOWinMenuItem.CustomTargets.Bancs
                    Dim oMenu_Banc As New Menu_Banc(item.Tag)
                    AddHandler oMenu_Banc.AfterUpdate, AddressOf Refreshrequest
                    oContextMenu.Items.AddRange(oMenu_Banc.Range)
                Case DTOWinMenuItem.CustomTargets.Staff
                    Dim oMenu_Staff As New Menu_Staff(item.Tag)
                    AddHandler oMenu_Staff.AfterUpdate, AddressOf Refreshrequest
                    oContextMenu.Items.AddRange(oMenu_Staff.Range)
                Case DTOWinMenuItem.CustomTargets.Reps
                    Dim oMenu_Rep As New Menu_Rep(item.Tag)
                    AddHandler oMenu_Rep.AfterUpdate, AddressOf Refreshrequest
                    oContextMenu.Items.AddRange(oMenu_Rep.Range)
                Case Else
                    Dim oMenu_WinMenuItem As New Menu_WinMenuItem(item)
                    AddHandler oMenu_WinMenuItem.AfterUpdate, AddressOf Refreshrequest
                    oContextMenu.Items.AddRange(oMenu_WinMenuItem.Range)
            End Select
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Refreshrequest(sender As Object, e As MatEventArgs)
        RaiseEvent afterupdate(Me, e)
    End Sub
End Class
