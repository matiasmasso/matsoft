<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_FlatFile
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_FlatRegs1 = New Mat.NET.Xl_FlatRegs()
        Me.Xl_FlatFields1 = New Mat.NET.Xl_FlatFields()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_FlatRegs1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_FlatFields1)
        Me.SplitContainer1.Size = New System.Drawing.Size(588, 261)
        Me.SplitContainer1.SplitterDistance = 196
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_FlatRegs1
        '
        Me.Xl_FlatRegs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_FlatRegs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_FlatRegs1.Name = "Xl_FlatRegs1"
        Me.Xl_FlatRegs1.Size = New System.Drawing.Size(196, 261)
        Me.Xl_FlatRegs1.TabIndex = 0
        '
        'Xl_FlatFields1
        '
        Me.Xl_FlatFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_FlatFields1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_FlatFields1.Name = "Xl_FlatFields1"
        Me.Xl_FlatFields1.Size = New System.Drawing.Size(388, 261)
        Me.Xl_FlatFields1.TabIndex = 0
        '
        'Frm_FlatFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(588, 261)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_FlatFile"
        Me.Text = "Frm_FlatFile"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_FlatRegs1 As Mat.NET.Xl_FlatRegs
    Friend WithEvents Xl_FlatFields1 As Mat.NET.Xl_FlatFields
End Class
