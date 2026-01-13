<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Zips
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
        Me.Xl_Zonas1 = New Mat.NET.Xl_Zonas()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Countries1 = New Mat.NET.Xl_Countries()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Locations1 = New Mat.NET.Xl_Locations()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Countries1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(962, 261)
        Me.SplitContainer1.SplitterDistance = 223
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_Zonas1
        '
        Me.Xl_Zonas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Zonas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Zonas1.Name = "Xl_Zonas1"
        Me.Xl_Zonas1.Size = New System.Drawing.Size(290, 261)
        Me.Xl_Zonas1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_Zonas1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(735, 261)
        Me.SplitContainer2.SplitterDistance = 290
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_Countries1
        '
        Me.Xl_Countries1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Countries1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Countries1.Name = "Xl_Countries1"
        Me.Xl_Countries1.Size = New System.Drawing.Size(223, 261)
        Me.Xl_Countries1.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.Xl_Locations1)
        Me.SplitContainer3.Size = New System.Drawing.Size(441, 261)
        Me.SplitContainer3.SplitterDistance = 305
        Me.SplitContainer3.TabIndex = 0
        '
        'Xl_Locations1
        '
        Me.Xl_Locations1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Locations1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Locations1.Name = "Xl_Locations1"
        Me.Xl_Locations1.Size = New System.Drawing.Size(305, 261)
        Me.Xl_Locations1.TabIndex = 1
        '
        'Frm_Zips
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(962, 261)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_Zips"
        Me.Text = "Frm_Zips"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Countries1 As Mat.NET.Xl_Countries
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Zonas1 As Mat.NET.Xl_Zonas
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Locations1 As Mat.NET.Xl_Locations
End Class
