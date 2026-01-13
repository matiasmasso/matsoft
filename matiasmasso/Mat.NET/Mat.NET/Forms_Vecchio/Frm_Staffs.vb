

Public Class Frm_Staffs
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents MenuItemObsolets As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemZoom As System.Windows.Forms.MenuItem
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.MenuItemObsolets = New System.Windows.Forms.MenuItem
        Me.MenuItemZoom = New System.Windows.Forms.MenuItem
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'MenuItemObsolets
        '
        Me.MenuItemObsolets.Index = 1
        Me.MenuItemObsolets.Text = "Inclou obsolets"
        '
        'MenuItemZoom
        '
        Me.MenuItemZoom.Index = 0
        Me.MenuItemZoom.Text = "Zoom"
        '
        'ListView1
        '
        Me.ListView1.ContextMenu = Me.ContextMenu1
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.LargeImageList = Me.ImageList1
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(292, 273)
        Me.ListView1.TabIndex = 1
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemZoom, Me.MenuItemObsolets})
        '
        'ImageList1
        '
        Me.ImageList1.ImageSize = New System.Drawing.Size(48, 48)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Frm_Staffs
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.ListView1)
        Me.Name = "Frm_Staffs"
        Me.Text = "PERSONAL EN PLANTILLA"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mStaffs As Staffs
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Sub Frm_Staffs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mStaffs = MaxiSrvr.Emp.FromDTOEmp(mEmp).Staffs()
        refresca()
    End Sub

    Private Sub refresca()
        ListView1.Items.Clear()
        Dim oStaff As Staff
        Dim BlShow As Boolean
        Dim oLv As ListViewItem
        Dim Idx As Integer
        For Each oStaff In mStaffs
            BlShow = True
            If oStaff.Obsoleto And (Not MenuItemObsolets.Checked) Then
                BlShow = False
            End If
            If BlShow Then
                oLv = New ListViewItem
                oLv.Text = oStaff.Nom

                ListView1.Items.Add(oLv)
                If Not oStaff.Img48 Is Nothing Then
                    ImageList1.Images.Add(oStaff.Img48)
                    oLv.ImageIndex = Idx
                    Idx = Idx + 1
                End If
            End If
        Next
    End Sub

    Private Sub MenuItemZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemZoom.Click
        Dim oStaff As Staff = mStaffs(ListView1.SelectedIndices(0))
        root.ShowContact(oStaff)
    End Sub

    Private Sub MenuItemObsolets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemObsolets.Click
        MenuItemObsolets.Checked = Not MenuItemObsolets.Checked
        refresca()
    End Sub

End Class

