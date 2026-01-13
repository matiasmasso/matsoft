Public Partial Class Frm_UsrEvents
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.ButtonAlbsDel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.ButtonAlbsAdd = New System.Windows.Forms.Button
        Me.Xl_ContactAlbs = New Xl_Contact
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(13, 13)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(413, 249)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Controls.Add(Me.ButtonAlbsDel)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.ButtonAlbsAdd)
        Me.TabPage1.Controls.Add(Me.Xl_ContactAlbs)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(405, 223)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "ALBARANS"
        '
        'ButtonAlbsDel
        '
        Me.ButtonAlbsDel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAlbsDel.Image = My.Resources.Resources.minus
        Me.ButtonAlbsDel.Location = New System.Drawing.Point(361, 31)
        Me.ButtonAlbsDel.Margin = New System.Windows.Forms.Padding(1, 3, 3, 1)
        Me.ButtonAlbsDel.Name = "ButtonAlbsDel"
        Me.ButtonAlbsDel.Size = New System.Drawing.Size(38, 20)
        Me.ButtonAlbsDel.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(322, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Enviam un e-mail quan surti un albará per els següents destinataris:"
        '
        'ButtonAlbsAdd
        '
        Me.ButtonAlbsAdd.Enabled = False
        Me.ButtonAlbsAdd.Image = My.Resources.Resources.clip
        Me.ButtonAlbsAdd.Location = New System.Drawing.Point(320, 30)
        Me.ButtonAlbsAdd.Margin = New System.Windows.Forms.Padding(3, 3, 2, 1)
        Me.ButtonAlbsAdd.Name = "ButtonAlbsAdd"
        Me.ButtonAlbsAdd.Size = New System.Drawing.Size(38, 20)
        Me.ButtonAlbsAdd.TabIndex = 4
        '
        'Xl_ContactAlbs
        '
        Me.Xl_ContactAlbs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactAlbs.Location = New System.Drawing.Point(4, 31)
        Me.Xl_ContactAlbs.Name = "Xl_ContactAlbs"
        Me.Xl_ContactAlbs.Size = New System.Drawing.Size(309, 20)
        Me.Xl_ContactAlbs.TabIndex = 3
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(343, 269)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(78, 28)
        Me.ButtonOk.TabIndex = 2
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(13, 269)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(78, 28)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(6, 57)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(393, 160)
        Me.DataGridView1.TabIndex = 7
        '
        'Frm_UsrEvents
        '
        Me.ClientSize = New System.Drawing.Size(437, 304)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_UsrEvents"
        Me.Text = "EVENTS DE USUARI"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonAlbsAdd As System.Windows.Forms.Button
    Friend WithEvents Xl_ContactAlbs As Xl_Contact
    Friend WithEvents ButtonAlbsDel As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
