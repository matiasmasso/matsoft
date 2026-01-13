

Public Class Xl_Bancs_Select
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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(48, 48)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.LargeImageList = Me.ImageList1
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(150, 90)
        Me.ListView1.TabIndex = 1
        '
        'Xl_Bancs_Select
        '
        Me.Controls.Add(Me.ListView1)
        Me.Name = "Xl_Bancs_Select"
        Me.Size = New System.Drawing.Size(150, 90)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _Bancs As List(Of DTOBanc)
    Private _Sprite As Byte()
    Private mAllowEvents As Boolean
    Private mMenuItemObsoletos As New ToolStripMenuItem

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public ReadOnly Property Banc() As DTOBanc
        Get
            Return CurrentBanc()
        End Get
    End Property

    Public Shadows Async Function Load(Optional oSelectedBanc As DTOBanc = Nothing) As Task
        If _Bancs Is Nothing Then
            _Bancs = Await GetBancs(GlobalVariables.Emp)
            refresca(oSelectedBanc)
        End If

        mAllowEvents = True
    End Function

    Private Async Function GetBancs(oEmp As DTOEmp) As Task(Of List(Of DTOBanc))
        Dim retval As New List(Of DTOBanc)
        Dim exs As New List(Of Exception)
        retval = Await FEB.Bancs.AllActive(oEmp, exs)
        If exs.Count = 0 Then
            _Sprite = Await FEB.Bancs.Sprite(Current.Session.Emp, exs)
            If exs.Count > 0 Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
        Return retval
    End Function


    Private Sub refresca(Optional oSelectedBanc As DTOBanc = Nothing)
        ListView1.Items.Clear()
        ListView1.SelectedIndices.Clear()

        For imageIndex As Integer = 0 To _Bancs.Count - 1
            Dim oBanc As DTOBanc = _Bancs(imageIndex)
            If mMenuItemObsoletos.Checked Or (Not oBanc.obsoleto) Then
                Dim oImageBytes = LegacyHelper.SpriteHelper.Extract(_Sprite, imageIndex, _Bancs.Count)
                Dim ms As New IO.MemoryStream(oImageBytes)
                ImageList1.Images.Add(Image.FromStream(ms))
                Dim oListViewItem = ListViewItem(oBanc, imageIndex)
                ListView1.Items.Add(oListViewItem)
                If oBanc.Equals(oSelectedBanc) Then
                    oListViewItem.Selected = True
                    Dim iCount = ListView1.SelectedItems.Count
                End If
            End If
        Next
    End Sub

    Private Function CurrentBanc() As DTOBanc
        Dim retval As DTOBanc = Nothing
        If ListView1.SelectedItems.Count > 0 Then
            retval = ListView1.SelectedItems(0).Tag
        End If
        Return retval
    End Function

    Private Function ListViewItem(oBanc As DTOBanc, imageIndex As Integer) As ListViewItem
        Dim retval As New ListViewItem
        With retval
            .Text = oBanc.Abr
            .ImageIndex = imageIndex
            .Tag = oBanc
        End With
        Return retval
    End Function

    Private Sub MenuItemObsolets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mMenuItemObsoletos.Checked = Not mMenuItemObsoletos.Checked
        refresca()
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedIndices.Count > 0 Then
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If mAllowEvents Then
            RaiseEvent AfterUpdate(sender, e)
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If ListView1.SelectedItems.Count = 1 Then
            Dim oMenu_Banc As New Menu_Banc(CurrentBanc)
            oContextMenu.Items.AddRange(oMenu_Banc.Range)
        End If

        oContextMenu.Items.Add(mMenuItemObsoletos)

        ListView1.ContextMenuStrip = oContextMenu
    End Sub

End Class

