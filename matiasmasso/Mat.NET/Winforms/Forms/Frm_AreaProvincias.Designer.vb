<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AreaProvincias
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
        Me.Xl_AreaRegions1 = New Winforms.Xl_AreaRegions()
        Me.Xl_AreaProvincias1 = New Winforms.Xl_AreaProvincias()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_AreaRegions1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_AreaRegions1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_AreaProvincias1)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 266
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_AreaRegions1
        '
        Me.Xl_AreaRegions1.AllowUserToAddRows = False
        Me.Xl_AreaRegions1.AllowUserToDeleteRows = False
        Me.Xl_AreaRegions1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_AreaRegions1.DisplayObsolets = False
        Me.Xl_AreaRegions1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_AreaRegions1.Filter = Nothing
        Me.Xl_AreaRegions1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_AreaRegions1.MouseIsDown = False
        Me.Xl_AreaRegions1.Name = "Xl_AreaRegions1"
        Me.Xl_AreaRegions1.ReadOnly = True
        Me.Xl_AreaRegions1.Size = New System.Drawing.Size(266, 450)
        Me.Xl_AreaRegions1.TabIndex = 0
        '
        'Xl_AreaProvincias1
        '
        Me.Xl_AreaProvincias1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_AreaProvincias1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_AreaProvincias1.Name = "Xl_AreaProvincias1"
        Me.Xl_AreaProvincias1.Size = New System.Drawing.Size(530, 450)
        Me.Xl_AreaProvincias1.TabIndex = 0
        '
        'Frm_AreaProvincias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_AreaProvincias"
        Me.Text = "Provincies de "
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_AreaRegions1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_AreaRegions1 As Xl_AreaRegions
    Friend WithEvents Xl_AreaProvincias1 As Xl_AreaProvincias
End Class
