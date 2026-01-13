<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TpvRedsys
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
        Me.TabPageLogs = New System.Windows.Forms.TabPage()
        Me.TabPageConfig = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_TpvRedsysLogs1 = New Xl_TpvRedsysLogs()
        Me.Xl_TpvRedsysConfigs1 = New Xl_TpvRedsysConfigs()
        Me.TabControl1.SuspendLayout()
        Me.TabPageLogs.SuspendLayout()
        Me.TabPageConfig.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_TpvRedsysLogs1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_TpvRedsysConfigs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageLogs)
        Me.TabControl1.Controls.Add(Me.TabPageConfig)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(560, 211)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageLogs
        '
        Me.TabPageLogs.Controls.Add(Me.Xl_TpvRedsysLogs1)
        Me.TabPageLogs.Location = New System.Drawing.Point(4, 22)
        Me.TabPageLogs.Name = "TabPageLogs"
        Me.TabPageLogs.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageLogs.Size = New System.Drawing.Size(552, 185)
        Me.TabPageLogs.TabIndex = 1
        Me.TabPageLogs.Text = "Logs"
        Me.TabPageLogs.UseVisualStyleBackColor = True
        '
        'TabPageConfig
        '
        Me.TabPageConfig.Controls.Add(Me.Xl_TpvRedsysConfigs1)
        Me.TabPageConfig.Location = New System.Drawing.Point(4, 22)
        Me.TabPageConfig.Name = "TabPageConfig"
        Me.TabPageConfig.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageConfig.Size = New System.Drawing.Size(433, 185)
        Me.TabPageConfig.TabIndex = 0
        Me.TabPageConfig.Text = "Configuracions"
        Me.TabPageConfig.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(1, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(560, 234)
        Me.Panel1.TabIndex = 1
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 211)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(560, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'Xl_TpvRedsysLogs1
        '
        Me.Xl_TpvRedsysLogs1.AllowUserToAddRows = False
        Me.Xl_TpvRedsysLogs1.AllowUserToDeleteRows = False
        Me.Xl_TpvRedsysLogs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_TpvRedsysLogs1.DisplayObsolets = False
        Me.Xl_TpvRedsysLogs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_TpvRedsysLogs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_TpvRedsysLogs1.MouseIsDown = False
        Me.Xl_TpvRedsysLogs1.Name = "Xl_TpvRedsysLogs1"
        Me.Xl_TpvRedsysLogs1.ReadOnly = True
        Me.Xl_TpvRedsysLogs1.Size = New System.Drawing.Size(546, 179)
        Me.Xl_TpvRedsysLogs1.TabIndex = 0
        '
        'Xl_TpvRedsysConfigs1
        '
        Me.Xl_TpvRedsysConfigs1.AllowUserToAddRows = False
        Me.Xl_TpvRedsysConfigs1.AllowUserToDeleteRows = False
        Me.Xl_TpvRedsysConfigs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_TpvRedsysConfigs1.DisplayObsolets = False
        Me.Xl_TpvRedsysConfigs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_TpvRedsysConfigs1.Filter = Nothing
        Me.Xl_TpvRedsysConfigs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_TpvRedsysConfigs1.MouseIsDown = False
        Me.Xl_TpvRedsysConfigs1.Name = "Xl_TpvRedsysConfigs1"
        Me.Xl_TpvRedsysConfigs1.ReadOnly = True
        Me.Xl_TpvRedsysConfigs1.Size = New System.Drawing.Size(427, 179)
        Me.Xl_TpvRedsysConfigs1.TabIndex = 0
        '
        'Frm_TpvRedsys
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 267)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_TpvRedsys"
        Me.Text = "Tpv Redsys"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageLogs.ResumeLayout(False)
        Me.TabPageConfig.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_TpvRedsysLogs1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_TpvRedsysConfigs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageConfig As TabPage
    Friend WithEvents TabPageLogs As TabPage
    Friend WithEvents Xl_TpvRedsysConfigs1 As Xl_TpvRedsysConfigs
    Friend WithEvents Xl_TpvRedsysLogs1 As Xl_TpvRedsysLogs
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
