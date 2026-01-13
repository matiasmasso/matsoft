<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PgcClassesCtas
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Xl_PgcClassTree1 = New Xl_PgcClassTree()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_PgcCtas1 = New Xl_PgcCtas()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_LookupPgcPlan1 = New Xl_LookupPgcPlan()
        Me.TextBoxSearch = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_PgcCtas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_PgcClassTree1
        '
        Me.Xl_PgcClassTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PgcClassTree1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PgcClassTree1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Xl_PgcClassTree1.Name = "Xl_PgcClassTree1"
        Me.Xl_PgcClassTree1.Size = New System.Drawing.Size(285, 342)
        Me.Xl_PgcClassTree1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(1, 1)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_PgcClassTree1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_PgcCtas1)
        Me.SplitContainer1.Size = New System.Drawing.Size(498, 342)
        Me.SplitContainer1.SplitterDistance = 285
        Me.SplitContainer1.SplitterWidth = 2
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_PgcCtas1
        '
        Me.Xl_PgcCtas1.AllowUserToAddRows = False
        Me.Xl_PgcCtas1.AllowUserToDeleteRows = False
        Me.Xl_PgcCtas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PgcCtas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PgcCtas1.Filter = Nothing
        Me.Xl_PgcCtas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PgcCtas1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Xl_PgcCtas1.Name = "Xl_PgcCtas1"
        Me.Xl_PgcCtas1.ReadOnly = True
        Me.Xl_PgcCtas1.RowTemplate.Height = 40
        Me.Xl_PgcCtas1.Size = New System.Drawing.Size(211, 342)
        Me.Xl_PgcCtas1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(2, 29)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(508, 370)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage1.Size = New System.Drawing.Size(500, 344)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Classificació comptes"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_LookupPgcPlan1
        '
        Me.Xl_LookupPgcPlan1.IsDirty = False
        Me.Xl_LookupPgcPlan1.Location = New System.Drawing.Point(6, 5)
        Me.Xl_LookupPgcPlan1.Name = "Xl_LookupPgcPlan1"
        Me.Xl_LookupPgcPlan1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPgcPlan1.PgcPlan = Nothing
        Me.Xl_LookupPgcPlan1.Size = New System.Drawing.Size(221, 20)
        Me.Xl_LookupPgcPlan1.TabIndex = 3
        Me.Xl_LookupPgcPlan1.Value = Nothing
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSearch.Location = New System.Drawing.Point(294, 5)
        Me.TextBoxSearch.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.Size = New System.Drawing.Size(190, 20)
        Me.TextBoxSearch.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = Global.Mat.Net.My.Resources.Resources.Lupa
        Me.PictureBox1.Location = New System.Drawing.Point(481, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 29)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'Frm_PgcClassesCtas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 400)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TextBoxSearch)
        Me.Controls.Add(Me.Xl_LookupPgcPlan1)
        Me.Controls.Add(Me.TabControl1)
        Me.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Name = "Frm_PgcClassesCtas"
        Me.Text = "Pla Comptable"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_PgcCtas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_PgcClassTree1 As Xl_PgcClassTree
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_PgcCtas1 As Xl_PgcCtas
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Xl_LookupPgcPlan1 As Xl_LookupPgcPlan
    Friend WithEvents TextBoxSearch As TextBox
    Friend WithEvents PictureBox1 As PictureBox
End Class
