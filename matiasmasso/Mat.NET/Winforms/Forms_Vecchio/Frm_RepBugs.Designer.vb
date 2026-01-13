<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepBugs
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPagePnc = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.DataGridViewPnc = New System.Windows.Forms.DataGridView
        Me.TabControl1.SuspendLayout()
        Me.TabPagePnc.SuspendLayout()
        CType(Me.DataGridViewPnc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPagePnc)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(662, 311)
        Me.TabControl1.TabIndex = 0
        '
        'TabPagePnc
        '
        Me.TabPagePnc.Controls.Add(Me.DataGridViewPnc)
        Me.TabPagePnc.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePnc.Name = "TabPagePnc"
        Me.TabPagePnc.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePnc.Size = New System.Drawing.Size(654, 285)
        Me.TabPagePnc.TabIndex = 0
        Me.TabPagePnc.Text = "COMANDES NO ASSIGNADES"
        Me.TabPagePnc.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(192, 74)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridViewPnc
        '
        Me.DataGridViewPnc.AllowUserToAddRows = False
        Me.DataGridViewPnc.AllowUserToDeleteRows = False
        Me.DataGridViewPnc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewPnc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewPnc.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewPnc.Name = "DataGridViewPnc"
        Me.DataGridViewPnc.ReadOnly = True
        Me.DataGridViewPnc.Size = New System.Drawing.Size(648, 279)
        Me.DataGridViewPnc.TabIndex = 0
        '
        'Frm_RepBugs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 363)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_RepBugs"
        Me.Text = "Frm_RepBugs"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPagePnc.ResumeLayout(False)
        CType(Me.DataGridViewPnc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPagePnc As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewPnc As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
End Class
