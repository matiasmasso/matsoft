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
        Me.Xl_AreaRegions1 = New Mat.Net.Xl_AreaRegions()
        Me.Xl_AreaProvincias1 = New Mat.Net.Xl_AreaProvincias()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_AreaRegions1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_AreaProvincias1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderHG.SetHelpKeyword(Me.SplitContainer1, "Frm_AreaProvincias.htm#SplitContainer1")
        Me.HelpProviderHG.SetHelpNavigator(Me.SplitContainer1, System.Windows.Forms.HelpNavigator.Topic)
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
        Me.HelpProviderHG.SetShowHelp(Me.SplitContainer1, True)
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
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_AreaRegions1, "Frm_AreaProvincias.htm#Xl_AreaRegions1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_AreaRegions1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_AreaRegions1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_AreaRegions1.MouseIsDown = False
        Me.Xl_AreaRegions1.Name = "Xl_AreaRegions1"
        Me.Xl_AreaRegions1.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.Xl_AreaRegions1, True)
        Me.Xl_AreaRegions1.Size = New System.Drawing.Size(266, 450)
        Me.Xl_AreaRegions1.TabIndex = 0
        '
        'Xl_AreaProvincias1
        '
        Me.Xl_AreaProvincias1.DisplayObsolets = False
        Me.Xl_AreaProvincias1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_AreaProvincias1.Filter = Nothing
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_AreaProvincias1, "Frm_AreaProvincias.htm#Xl_AreaProvincias1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_AreaProvincias1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_AreaProvincias1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_AreaProvincias1.MouseIsDown = False
        Me.Xl_AreaProvincias1.Name = "Xl_AreaProvincias1"
        Me.HelpProviderHG.SetShowHelp(Me.Xl_AreaProvincias1, True)
        Me.Xl_AreaProvincias1.Size = New System.Drawing.Size(530, 450)
        Me.Xl_AreaProvincias1.TabIndex = 0
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_AreaProvincias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_AreaProvincias.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Frm_AreaProvincias"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Provincies de "
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_AreaRegions1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_AreaProvincias1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_AreaRegions1 As Xl_AreaRegions
    Friend WithEvents Xl_AreaProvincias1 As Xl_AreaProvincias
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
