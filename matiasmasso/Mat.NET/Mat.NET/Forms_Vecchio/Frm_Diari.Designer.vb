<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Diari
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
        Me.Xl_Years1 = New Mat.NET.Xl_Years()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Diari_Months = New Mat.NET.Xl_Diari()
        Me.Xl_Diari_Days = New Mat.NET.Xl_Diari()
        Me.Xl_Diari_Pdcs = New Mat.NET.Xl_Diari()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(12, 12)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 0
        Me.Xl_Years1.Value = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 41)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Diari_Months)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1226, 421)
        Me.SplitContainer1.SplitterDistance = 158
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_Diari_Days)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_Diari_Pdcs)
        Me.SplitContainer2.Size = New System.Drawing.Size(1226, 259)
        Me.SplitContainer2.SplitterDistance = 131
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_Diari_Months
        '
        Me.Xl_Diari_Months.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Diari_Months.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Diari_Months.Name = "Xl_Diari_Months"
        Me.Xl_Diari_Months.Size = New System.Drawing.Size(1226, 158)
        Me.Xl_Diari_Months.TabIndex = 0
        '
        'Xl_Diari_Days
        '
        Me.Xl_Diari_Days.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Diari_Days.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Diari_Days.Name = "Xl_Diari_Days"
        Me.Xl_Diari_Days.Size = New System.Drawing.Size(1226, 131)
        Me.Xl_Diari_Days.TabIndex = 1
        '
        'Xl_Diari_Pdcs
        '
        Me.Xl_Diari_Pdcs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Diari_Pdcs.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Diari_Pdcs.Name = "Xl_Diari_Pdcs"
        Me.Xl_Diari_Pdcs.Size = New System.Drawing.Size(1226, 124)
        Me.Xl_Diari_Pdcs.TabIndex = 2
        '
        'Frm_Diari
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1228, 462)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Name = "Frm_Diari"
        Me.Text = "Frm_Diari"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Years1 As Mat.NET.Xl_Years
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Diari_Months As Mat.NET.Xl_Diari
    Friend WithEvents Xl_Diari_Days As Mat.NET.Xl_Diari
    Friend WithEvents Xl_Diari_Pdcs As Mat.NET.Xl_Diari
End Class
