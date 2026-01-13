<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Geo
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
        Me.SplitContainerCountries = New System.Windows.Forms.SplitContainer()
        Me.Xl_Countries1 = New Mat.NET.Xl_Countries()
        Me.SplitContainerZonas = New System.Windows.Forms.SplitContainer()
        Me.Xl_Zonas1 = New Mat.NET.Xl_Zonas()
        Me.Xl_Locations1 = New Mat.NET.Xl_Locations()
        Me.SplitContainerLocations = New System.Windows.Forms.SplitContainer()
        Me.Xl_Zips1 = New Mat.NET.Xl_Zips()
        CType(Me.SplitContainerCountries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerCountries.Panel1.SuspendLayout()
        Me.SplitContainerCountries.Panel2.SuspendLayout()
        Me.SplitContainerCountries.SuspendLayout()
        CType(Me.Xl_Countries1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerZonas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerZonas.Panel1.SuspendLayout()
        Me.SplitContainerZonas.Panel2.SuspendLayout()
        Me.SplitContainerZonas.SuspendLayout()
        CType(Me.Xl_Zonas1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Locations1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerLocations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerLocations.Panel1.SuspendLayout()
        Me.SplitContainerLocations.Panel2.SuspendLayout()
        Me.SplitContainerLocations.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainerCountries
        '
        Me.SplitContainerCountries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerCountries.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerCountries.Name = "SplitContainerCountries"
        '
        'SplitContainerCountries.Panel1
        '
        Me.SplitContainerCountries.Panel1.Controls.Add(Me.Xl_Countries1)
        '
        'SplitContainerCountries.Panel2
        '
        Me.SplitContainerCountries.Panel2.Controls.Add(Me.SplitContainerZonas)
        Me.SplitContainerCountries.Size = New System.Drawing.Size(861, 261)
        Me.SplitContainerCountries.SplitterDistance = 200
        Me.SplitContainerCountries.TabIndex = 0
        '
        'Xl_Countries1
        '
        Me.Xl_Countries1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Countries1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Countries1.Name = "Xl_Countries1"
        Me.Xl_Countries1.Size = New System.Drawing.Size(200, 261)
        Me.Xl_Countries1.TabIndex = 0
        '
        'SplitContainerZonas
        '
        Me.SplitContainerZonas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerZonas.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerZonas.Name = "SplitContainerZonas"
        '
        'SplitContainerZonas.Panel1
        '
        Me.SplitContainerZonas.Panel1.Controls.Add(Me.Xl_Zonas1)
        '
        'SplitContainerZonas.Panel2
        '
        Me.SplitContainerZonas.Panel2.Controls.Add(Me.SplitContainerLocations)
        Me.SplitContainerZonas.Size = New System.Drawing.Size(657, 261)
        Me.SplitContainerZonas.SplitterDistance = 260
        Me.SplitContainerZonas.TabIndex = 0
        '
        'Xl_Zonas1
        '
        Me.Xl_Zonas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Zonas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Zonas1.Name = "Xl_Zonas1"
        Me.Xl_Zonas1.Size = New System.Drawing.Size(260, 261)
        Me.Xl_Zonas1.TabIndex = 0
        '
        'Xl_Locations1
        '
        Me.Xl_Locations1.AllowUserToResizeColumns = False
        Me.Xl_Locations1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Locations1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Locations1.Name = "Xl_Locations1"
        Me.Xl_Locations1.Size = New System.Drawing.Size(320, 261)
        Me.Xl_Locations1.TabIndex = 0
        '
        'SplitContainerLocations
        '
        Me.SplitContainerLocations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerLocations.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerLocations.IsSplitterFixed = True
        Me.SplitContainerLocations.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerLocations.Name = "SplitContainerLocations"
        '
        'SplitContainerLocations.Panel1
        '
        Me.SplitContainerLocations.Panel1.Controls.Add(Me.Xl_Locations1)
        '
        'SplitContainerLocations.Panel2
        '
        Me.SplitContainerLocations.Panel2.Controls.Add(Me.Xl_Zips1)
        Me.SplitContainerLocations.Size = New System.Drawing.Size(393, 261)
        Me.SplitContainerLocations.SplitterDistance = 320
        Me.SplitContainerLocations.TabIndex = 1
        '
        'Xl_Zips1
        '
        Me.Xl_Zips1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Zips1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Zips1.Name = "Xl_Zips1"
        Me.Xl_Zips1.Size = New System.Drawing.Size(69, 261)
        Me.Xl_Zips1.TabIndex = 0
        '
        'Frm_Locations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(861, 261)
        Me.Controls.Add(Me.SplitContainerCountries)
        Me.Name = "Frm_Locations"
        Me.Text = "Poblacions"
        Me.SplitContainerCountries.Panel1.ResumeLayout(False)
        Me.SplitContainerCountries.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerCountries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerCountries.ResumeLayout(False)
        CType(Me.Xl_Countries1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerZonas.Panel1.ResumeLayout(False)
        Me.SplitContainerZonas.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerZonas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerZonas.ResumeLayout(False)
        CType(Me.Xl_Zonas1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Locations1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerLocations.Panel1.ResumeLayout(False)
        Me.SplitContainerLocations.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerLocations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerLocations.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerCountries As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Countries1 As Mat.NET.Xl_Countries
    Friend WithEvents SplitContainerZonas As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Zonas1 As Mat.NET.Xl_Zonas
    Friend WithEvents Xl_Locations1 As Mat.NET.Xl_Locations
    Friend WithEvents SplitContainerLocations As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Zips1 As Mat.NET.Xl_Zips
End Class
