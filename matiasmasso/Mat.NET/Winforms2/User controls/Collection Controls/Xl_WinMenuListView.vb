Public Class Xl_WinMenuListView
    Inherits ListView

    Private _PropertiesSet As Boolean
    Private _WinMenuParent As DTOWinMenuItem
    Private _WinMenuItems As List(Of DTOWinMenuItem)
    Private _MenuItem_StaffObsolets As ToolStripMenuItem
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Shadows Async Sub Load(oItems As List(Of DTOWinMenuItem))
        If Not _PropertiesSet Then
            SetProperties()
            _PropertiesSet = True
        End If

        'save previous layout
        Dim SelectedItemPosition As Point = Nothing
        Dim SelectedItemIdx As Integer
        If MyBase.SelectedIndices.Count > 0 Then
            Dim oSelectedItem As ListViewItem = MyBase.Items(MyBase.SelectedIndices(0))
            SelectedItemIdx = oSelectedItem.Index
            SelectedItemPosition = oSelectedItem.Position
        End If

        MyBase.Items.Clear()
        MyBase.LargeImageList = New ImageList()
        MyBase.LargeImageList.Images.Clear()
        MyBase.LargeImageList.ImageSize = New System.Drawing.Size(48, 48)

        Dim ImgIdx As Integer
        Dim oNoImg As New Bitmap(48, 48)
        Using gr As Graphics = Graphics.FromImage(oNoImg)
            gr.Clear(Color.White)
        End Using


        For Each item In oItems
            Dim oLv As New ListViewItem(item.langText.Tradueix(Current.Session.Lang))
            With oLv
                .Tag = item
                If item.icon Is Nothing Then
                    LargeImageList.Images.Add(oNoImg)
                Else
                    Dim ms As New System.IO.MemoryStream(item.icon)
                    Dim icon = Image.FromStream(ms)
                    'If item.Mime <> MimeCods.Jpg Then
                    '    Dim jpgFormat As Imaging.ImageFormat = Imaging.ImageFormat.Jpeg
                    '    icon.Save(ms, jpgFormat)
                    '    icon = Image.FromStream(ms)
                    'End If

                    LargeImageList.Images.Add(icon)
                End If
                .ImageIndex = ImgIdx
                ImgIdx = ImgIdx + 1
            End With
            MyBase.Items.Add(oLv)
        Next

        'restore previous layout
        If SelectedItemPosition <> Nothing And SelectedItemIdx < MyBase.Items.Count Then
            MyBase.Items(SelectedItemIdx).Selected = True
            MyBase.Items(SelectedItemIdx).Position = SelectedItemPosition
            MyBase.Items(SelectedItemIdx).EnsureVisible()
        End If

        Await SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub SetProperties()
        _MenuItem_StaffObsolets = New ToolStripMenuItem("mostrar obsolets")
        With _MenuItem_StaffObsolets
            .CheckOnClick = True
            .Checked = False
        End With
        AddHandler _MenuItem_StaffObsolets.CheckedChanged, AddressOf Do_ShowStaffObsolets
    End Sub

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

    Private Async Sub Xl_WinMenuListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Me.SelectedIndexChanged
        Await SetContextMenu()
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

    Private Async Function SetContextMenu() As Task
        Dim exs As New List(Of Exception)
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
                    oContextMenu.Items.Add("-")

                    Select Case Current.Session.Rol.id
                        Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                            oContextMenu.Items.Add("Excel tots els empleats", My.Resources.Excel, AddressOf Do_ShowStaffExcel)
                    End Select

                    oContextMenu.Items.Add("veure obsolets", Nothing, AddressOf Do_ShowStaffObsolets)

                Case DTOWinMenuItem.CustomTargets.reps
                    Dim oContactMenu = Await FEB.ContactMenu.Find(exs, item.tag)
                    Dim oMenu_Rep As New Menu_Rep(item.tag, oContactMenu)
                    AddHandler oMenu_Rep.AfterUpdate, AddressOf Refreshrequest
                    oContextMenu.Items.AddRange(oMenu_Rep.Range)
                    oContextMenu.Items.Add("-")
                    oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_RepsExcel)
                Case Else
                    Dim oMenu_WinMenuItem As New Menu_WinMenuItem(item)
                    AddHandler oMenu_WinMenuItem.AfterUpdate, AddressOf Refreshrequest
                    AddHandler oMenu_WinMenuItem.AfterDelete, AddressOf AfterDelete
                    AddHandler oMenu_WinMenuItem.RequestToToggleProgressBar, AddressOf ToggleProgressBarRequest
                    oContextMenu.Items.AddRange(oMenu_WinMenuItem.Range)
            End Select
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Function

    Private Sub ToggleProgressBarRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToToggleProgressBar(Me, e)
    End Sub

    Private Async Sub Do_RepsExcel()
        Dim exs As New List(Of Exception)
        Dim oRepProducts = Await FEB.RepProducts.RepsxAreaWithMobiles(exs)
        If exs.Count = 0 Then
            Dim oSheet = DTORepProduct.Excel(oRepProducts)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ShowStaffExcel()
        Dim exs As New List(Of Exception)
        Dim oExercici = DTOExercici.Current(GlobalVariables.Emp)
        Dim oStaffs = Await FEB.Staffs.All(exs, oExercici)
        If exs.Count = 0 Then
            Dim oSheet = DTOStaff.Excel(oStaffs, Current.Session.Lang)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
    Private Sub Do_ShowStaffObsolets()
        If _AllowEvents Then
            RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub AfterDelete(sender As Object, e As MatEventArgs)
        Dim oDeletedItem As DTOWinMenuItem = e.Argument
        If oDeletedItem.Parent IsNot Nothing Then
            SaveSetting(DTOSession.Settings.Last_Menu_Selection, oDeletedItem.Parent.Guid.ToString())
        End If
        Refreshrequest(sender, e)
    End Sub

    Private Sub Refreshrequest(sender As Object, e As MatEventArgs)
        RaiseEvent afterupdate(Me, e)
    End Sub
End Class
