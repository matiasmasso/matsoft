Public Class Frm_Img
    Inherits System.Windows.Forms.Form

    Private _Image As Image
    Private _Caption As String

    Public Sub New(oImage As Image, Optional sCaption As String = "")
        MyBase.New()

        _Image = oImage
        _Caption = sCaption

        InitializeComponent()

    End Sub

#Region " Windows Form Designer generated code "



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
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopy As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MenuItemCopy = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.ContextMenu = Me.ContextMenu1
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(292, 266)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopy})
        '
        'MenuItemCopy
        '
        Me.MenuItemCopy.Index = 0
        Me.MenuItemCopy.Text = "Copiar"
        '
        'Frm_Img
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Frm_Img"
        Me.Text = "IMATGE"
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub MenuItemCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopy.Click
        System.Windows.Forms.Clipboard.SetDataObject(_Image, True)
    End Sub

    Private Sub Frm_Img_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Size = _Image.Size
        PictureBox1.Image = _Image
        Me.Text = _Caption
    End Sub
End Class
