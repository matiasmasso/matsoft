<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CnapsStats
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
        Me.components = New System.ComponentModel.Container()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_CnapsCheckTree1 = New Winforms.Xl_CnapsCheckTree()
        Me.Xl_Stats1 = New Winforms.Xl_Stats()
        Me.Xl_Years1 = New Winforms.Xl_Years()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 101)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_CnapsCheckTree1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_Stats1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1709, 416)
        Me.SplitContainer1.SplitterDistance = 300
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_CnapsCheckTree1
        '
        Me.Xl_CnapsCheckTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CnapsCheckTree1.IgnoreClickAction = 0
        Me.Xl_CnapsCheckTree1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CnapsCheckTree1.Name = "Xl_CnapsCheckTree1"
        Me.Xl_CnapsCheckTree1.Size = New System.Drawing.Size(300, 416)
        Me.Xl_CnapsCheckTree1.TabIndex = 0
        '
        'Xl_Stats1
        '
        Me.Xl_Stats1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Stats1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Stats1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Stats1.Name = "Xl_Stats1"
        Me.Xl_Stats1.Size = New System.Drawing.Size(1405, 416)
        Me.Xl_Stats1.TabIndex = 0
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(17, 26)
        Me.Xl_Years1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(435, 55)
        Me.Xl_Years1.TabIndex = 1
        Me.Xl_Years1.Value = 0
        '
        'Frm_CnapsStats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1709, 518)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_CnapsStats"
        Me.Text = "Vendes per Cnap"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_CnapsCheckTree1 As Xl_CnapsCheckTree
    Friend WithEvents Xl_Stats1 As Xl_Stats
    Friend WithEvents Xl_Years1 As Xl_Years
End Class
