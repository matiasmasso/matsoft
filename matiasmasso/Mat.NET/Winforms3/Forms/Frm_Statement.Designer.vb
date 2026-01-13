<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Statement
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
        Me.Xl_StatementYears1 = New Mat.Net.Xl_StatementYears()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_StatementCtas1 = New Mat.Net.Xl_StatementCtas()
        Me.Xl_StatementItems1 = New Mat.Net.Xl_StatementItems()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_StatementYears1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_StatementCtas1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_StatementItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(585, 218)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(577, 192)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_StatementYears1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(571, 186)
        Me.SplitContainer1.SplitterDistance = 88
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_StatementYears1
        '
        Me.Xl_StatementYears1.AllowUserToAddRows = False
        Me.Xl_StatementYears1.AllowUserToDeleteRows = False
        Me.Xl_StatementYears1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_StatementYears1.DisplayObsolets = False
        Me.Xl_StatementYears1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_StatementYears1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_StatementYears1.MouseIsDown = False
        Me.Xl_StatementYears1.Name = "Xl_StatementYears1"
        Me.Xl_StatementYears1.ReadOnly = True
        Me.Xl_StatementYears1.Size = New System.Drawing.Size(88, 186)
        Me.Xl_StatementYears1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_StatementCtas1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_StatementItems1)
        Me.SplitContainer2.Size = New System.Drawing.Size(479, 186)
        Me.SplitContainer2.SplitterDistance = 160
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_StatementCtas1
        '
        Me.Xl_StatementCtas1.AllowUserToAddRows = False
        Me.Xl_StatementCtas1.AllowUserToDeleteRows = False
        Me.Xl_StatementCtas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_StatementCtas1.DisplayObsolets = False
        Me.Xl_StatementCtas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_StatementCtas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_StatementCtas1.MouseIsDown = False
        Me.Xl_StatementCtas1.Name = "Xl_StatementCtas1"
        Me.Xl_StatementCtas1.ReadOnly = True
        Me.Xl_StatementCtas1.Size = New System.Drawing.Size(160, 186)
        Me.Xl_StatementCtas1.TabIndex = 0
        '
        'Xl_StatementItems1
        '
        Me.Xl_StatementItems1.AllowUserToAddRows = False
        Me.Xl_StatementItems1.AllowUserToDeleteRows = False
        Me.Xl_StatementItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_StatementItems1.DisplayObsolets = False
        Me.Xl_StatementItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_StatementItems1.Filter = Nothing
        Me.Xl_StatementItems1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_StatementItems1.MouseIsDown = False
        Me.Xl_StatementItems1.Name = "Xl_StatementItems1"
        Me.Xl_StatementItems1.ReadOnly = True
        Me.Xl_StatementItems1.Size = New System.Drawing.Size(315, 186)
        Me.Xl_StatementItems1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(578, 258)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(1, 41)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(585, 241)
        Me.Panel1.TabIndex = 1
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 218)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(585, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_Statement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 284)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Statement"
        Me.Text = "Frm_Statement"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_StatementYears1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_StatementCtas1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_StatementItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_StatementYears1 As Xl_StatementYears
    Friend WithEvents Xl_StatementCtas1 As Xl_StatementCtas
    Friend WithEvents Xl_StatementItems1 As Xl_StatementItems
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
