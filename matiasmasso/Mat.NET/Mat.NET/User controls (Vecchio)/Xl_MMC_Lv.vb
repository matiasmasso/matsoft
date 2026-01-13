

Public Class Xl_MMC_Lv
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents LargeImageList As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.LargeImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.LargeImageList = Me.LargeImageList
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(150, 150)
        Me.ListView1.SmallImageList = Me.LargeImageList
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'LargeImageList
        '
        Me.LargeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.LargeImageList.ImageSize = New System.Drawing.Size(48, 48)
        Me.LargeImageList.TransparentColor = System.Drawing.Color.Transparent
        '
        'Xl_MMC_Lv
        '
        Me.Controls.Add(Me.ListView1)
        Me.Name = "Xl_MMC_Lv"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _MMC As MMC

    Public Shadows Sub Load(oMmc As MMC)
        _MMC = oMmc
        Select Case _MMC.Tag
            Case "REPS"
                refresca_reps()
            Case "BANCS"
                refresca_bancs()
            Case "STAFF"
                refresca_Staff()
            Case Else
                refresca()
                If BLL.BLLSession.Current.User.Rol.id = DTORol.Ids.SuperUser Then
                    'MenuItemMaintenance.Visible = True
                End If
        End Select
    End Sub

    Private Sub refresca()
        Dim oMMC As MMC
        Dim itm As ListViewItem
        Dim ImgIdx As Integer

        ListView1.Items.Clear()
        'SmallImageList.Images.Clear()
        LargeImageList.Images.Clear()

        For Each oMMC In _MMC.Children
            If oMMC.Cod = MMC.Cods.Item Then
                itm = New MaxiSrvr.ListViewItmObj(oMMC.Nom, oMMC)
                With itm
                    If Not oMMC.ImgBig Is Nothing Then
                        'SmallImageList.Images.Add(oMMC.ImgSmall)
                        LargeImageList.Images.Add(oMMC.ImgBig)
                        .ImageIndex = ImgIdx
                        ImgIdx = ImgIdx + 1
                    End If
                End With
                ListView1.Items.Add(itm)
            End If
        Next
    End Sub

    Private Sub refresca_reps()
        Dim oReps As List(Of DTORep) = BLL.BLLReps.All(True)
        Dim itm As ListViewItem
        Dim ImgIdx As Integer

        ListView1.Items.Clear()
        'SmallImageList.Images.Clear()
        LargeImageList.Images.Clear()
        For Each oRep As DTORep In oReps
            itm = New MaxiSrvr.ListViewItmObj(oRep.NickName, oRep)
            With itm
                If Not oRep.Img48 Is Nothing Then
                    'SmallImageList.Images.Add(oRep.Img48)
                    LargeImageList.Images.Add(oRep.Img48)
                    .ImageIndex = ImgIdx
                    ImgIdx = ImgIdx + 1
                End If
            End With
            ListView1.Items.Add(itm)

        Next
    End Sub

    Private Sub refresca_bancs()
        Dim oBancs As List(Of DTOBanc) = BLL.BLLBancs.All()
        Dim itm As ListViewItem
        Dim ImgIdx As Integer

        ListView1.Items.Clear()
        'SmallImageList.Images.Clear()
        LargeImageList.Images.Clear()
        For Each oBanc As DTOBanc In oBancs
            Dim oImg As Image = Nothing
            itm = New MaxiSrvr.ListViewItmObj(oBanc.Abr, oBanc)
            With itm
                If oBanc.Iban IsNot Nothing Then
                    Dim oBank As DTOBank = BLL.BLLIban.Bank(oBanc.Iban)
                    If oBank IsNot Nothing Then
                        oImg = oBank.Logo
                    End If
                    If oImg Is Nothing Then oImg = oBanc.logo
                    If Not oImg Is Nothing Then
                        'SmallImageList.Images.Add(oImg)
                        LargeImageList.Images.Add(oImg)
                        .ImageIndex = ImgIdx
                        ImgIdx = ImgIdx + 1
                    End If
                End If
            End With
            ListView1.Items.Add(itm)

        Next
    End Sub

    Private Sub refresca_Staff()
        Dim oStaffs As Staffs = App.Current.emp.Staffs()
        Dim oStaff As Staff
        Dim itm As ListViewItem
        Dim ImgIdx As Integer

        ListView1.Items.Clear()
        'SmallImageList.Images.Clear()
        LargeImageList.Images.Clear()
        For Each oStaff In oStaffs
            itm = New MaxiSrvr.ListViewItmObj(oStaff.Nom, oStaff)
            With itm
                If Not oStaff.Img48 Is Nothing Then
                    'SmallImageList.Images.Add(oStaff.Img48)
                    LargeImageList.Images.Add(oStaff.Img48)
                    .ImageIndex = ImgIdx
                    ImgIdx = ImgIdx + 1
                End If
            End With
            ListView1.Items.Add(itm)

        Next
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        'Dim oMmc As MMC = oitm.Obj
        Select Case _MMC.Tag
            Case "REPS", "BANCS", "STAFF"
                'Dim Idx As Integer = ListView1.SelectedIndices(0)
                'Dim oItm As maxisrvr.ListViewItmObj = ListView1.Items(Idx)
                'Dim oContact As Contact = CType(oItm.Obj, Contact)
                Dim oItm As MaxiSrvr.ListViewItmObj = ListView1.SelectedItems(0)
                Dim oContact As Contact = CType(oItm.Obj, Contact)
                root.ShowContact(oContact)
            Case Else
                _MMC = CurrentMMC()
                If _MMC.Action > "" Then
                    CallByName(Me.FindForm(), _MMC.Action, CallType.Method)
                End If
        End Select

    End Sub

    Private Function CurrentMMC() As MMC
        Dim idx As Integer = ListView1.SelectedIndices(0)
        Dim oItem As MaxiSrvr.ListViewItmObj = ListView1.Items(idx)
        Dim oMmc As MMC = oItem.Obj
        Return oMmc
    End Function

    Private Sub MMCZoom(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oMMC As MMC = CurrentMMC()
        Dim oFrm As New Frm_MMC(oMMC)
        oFrm.Show()
    End Sub

    Private Sub MMCDel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("eliminem l'accés directe " & _MMC.Nom & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            _MMC.Delete()
            MsgBox("Accés eliminat", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information, "MAT.NET")
        End If
    End Sub

    Private Sub MenuItemRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        refresca()
    End Sub


    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        SetMenu()
    End Sub

    Private Sub SetMenu()
        If ListView1.SelectedItems.Count > 0 Then
            Dim oMenu As New ContextMenuStrip

            Dim oItm As MaxiSrvr.ListViewItmObj = ListView1.SelectedItems(0)
            Select Case _MMC.Tag
                Case "REPS"
                    Dim oRep As DTORep = CType(oItm.Obj, DTORep)
                    Dim oMenu_Rep As New Menu_Rep(oRep)
                    oMenu.Items.AddRange(oMenu_Rep.Range)

                Case "BANCS"
                    Dim oBanc As DTOBanc = CType(oItm.Obj, DTOBanc)
                    oMenu = root.ContextMenu_Banc(oBanc)

                Case "STAFF"
                    Dim oStaff As Staff = CType(oItm.Obj, Staff)
                    oMenu.Items.AddRange(New Menu_Contact(oStaff).Range)

                    If BLL.BLLSession.Current.User.Rol.IsAdmin Or BLL.BLLSession.Current.User.Rol.Id = Rol.Ids.Accounts Then
                        oMenu.Items.Add("-")
                        oMenu.Items.Add("models 145", Nothing, AddressOf Do_Models145)
                    End If


                Case Else
                    If BLL.BLLSession.Current.User.Rol.id = Rol.Ids.SuperUser Then

                        Dim oMenuIte_MMCZoom As New ToolStripMenuItem
                        oMenuIte_MMCZoom.Text = "editar"
                        oMenuIte_MMCZoom.Image = My.Resources.prismatics
                        AddHandler oMenuIte_MMCZoom.Click, AddressOf MMCZoom

                        Dim oMenuIte_MMCDel As New ToolStripMenuItem
                        oMenuIte_MMCDel.Text = "eliminar"
                        oMenuIte_MMCDel.Image = My.Resources.del
                        AddHandler oMenuIte_MMCDel.Click, AddressOf MMCDel

                        oMenu.Items.Add(oMenuIte_MMCZoom)
                        oMenu.Items.Add(oMenuIte_MMCDel)

                    End If
            End Select
            ListView1.ContextMenuStrip = oMenu
        End If
    End Sub

    Private Sub Do_Models145()
        Dim oItm As MaxiSrvr.ListViewItmObj = ListView1.SelectedItems(0)
        Dim oStaff As Staff = CType(oItm.Obj, Staff)
        Dim oFrm As New Frm_Fiscal_Mod145(oStaff)
        oFrm.Show()
    End Sub



End Class
