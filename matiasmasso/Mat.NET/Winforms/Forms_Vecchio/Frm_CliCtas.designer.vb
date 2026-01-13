<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CliCtas
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Exercicis1 = New Winforms.Xl_Exercicis()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Extracte1 = New Winforms.Xl_Extracte()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_Contact_Pnds1 = New Winforms.Xl_Contact_Pnds_Old()
        Me.Xl_Contact_Logo1 = New Winforms.Xl_Contact_Logo()
        Me.Xl_PgcCtas1 = New Winforms.Xl_PgcCtas()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_PgcCtas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 88)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(2245, 618)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(10, 48)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage1.Size = New System.Drawing.Size(2225, 560)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Comptes"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(8, 7)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Exercicis1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(2209, 546)
        Me.SplitContainer1.SplitterDistance = 80
        Me.SplitContainer1.SplitterWidth = 11
        Me.SplitContainer1.TabIndex = 2
        '
        'Xl_Exercicis1
        '
        Me.Xl_Exercicis1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Exercicis1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Exercicis1.Margin = New System.Windows.Forms.Padding(21, 17, 21, 17)
        Me.Xl_Exercicis1.Name = "Xl_Exercicis1"
        Me.Xl_Exercicis1.Size = New System.Drawing.Size(80, 546)
        Me.Xl_Exercicis1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_PgcCtas1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_Extracte1)
        Me.SplitContainer2.Size = New System.Drawing.Size(2118, 546)
        Me.SplitContainer2.SplitterDistance = 173
        Me.SplitContainer2.SplitterWidth = 11
        Me.SplitContainer2.TabIndex = 1
        '
        'Xl_Extracte1
        '
        Me.Xl_Extracte1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Extracte1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Extracte1.Margin = New System.Windows.Forms.Padding(21, 17, 21, 17)
        Me.Xl_Extracte1.Name = "Xl_Extracte1"
        Me.Xl_Extracte1.Size = New System.Drawing.Size(1934, 546)
        Me.Xl_Extracte1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_Contact_Pnds1)
        Me.TabPage2.Location = New System.Drawing.Point(10, 48)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TabPage2.Size = New System.Drawing.Size(2225, 560)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Partides pendents"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_Contact_Pnds1
        '
        Me.Xl_Contact_Pnds1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Pnds1.Location = New System.Drawing.Point(8, 7)
        Me.Xl_Contact_Pnds1.Margin = New System.Windows.Forms.Padding(21, 17, 21, 17)
        Me.Xl_Contact_Pnds1.Name = "Xl_Contact_Pnds1"
        Me.Xl_Contact_Pnds1.Size = New System.Drawing.Size(2209, 546)
        Me.Xl_Contact_Pnds1.TabIndex = 0
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.AllowDrop = True
        Me.Xl_Contact_Logo1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact_Logo1.Contact = Nothing
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(1835, 12)
        Me.Xl_Contact_Logo1.Margin = New System.Windows.Forms.Padding(21, 17, 21, 17)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(400, 114)
        Me.Xl_Contact_Logo1.TabIndex = 3
        Me.Xl_Contact_Logo1.Visible = False
        '
        'Xl_PgcCtas1
        '
        Me.Xl_PgcCtas1.AllowUserToAddRows = False
        Me.Xl_PgcCtas1.AllowUserToDeleteRows = False
        Me.Xl_PgcCtas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PgcCtas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PgcCtas1.Filter = Nothing
        Me.Xl_PgcCtas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PgcCtas1.Name = "Xl_PgcCtas1"
        Me.Xl_PgcCtas1.ReadOnly = True
        Me.Xl_PgcCtas1.RowTemplate.Height = 40
        Me.Xl_PgcCtas1.Size = New System.Drawing.Size(173, 546)
        Me.Xl_PgcCtas1.TabIndex = 0
        '
        'Frm_CliCtas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2251, 708)
        Me.Controls.Add(Me.Xl_Contact_Logo1)
        Me.Controls.Add(Me.TabControl1)
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Name = "Frm_CliCtas"
        Me.Text = "Comptes"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_PgcCtas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Extracte1 As Xl_Extracte
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Exercicis1 As Winforms.Xl_Exercicis
    Friend WithEvents Xl_Contact_Logo1 As Winforms.Xl_Contact_Logo
    Friend WithEvents Xl_Contact_Pnds1 As Winforms.Xl_Contact_Pnds_Old
    Friend WithEvents Xl_PgcCtas1 As Xl_PgcCtas
End Class
