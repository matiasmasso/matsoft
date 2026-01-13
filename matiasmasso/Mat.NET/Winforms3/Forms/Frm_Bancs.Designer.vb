<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Bancs
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
        Me.Xl_Bancs1 = New Mat.Net.Xl_Bancs()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        CType(Me.Xl_Bancs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Bancs1
        '
        Me.Xl_Bancs1.AllowUserToAddRows = False
        Me.Xl_Bancs1.AllowUserToDeleteRows = False
        Me.Xl_Bancs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Bancs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_Bancs1, "Frm_Bancs.htm#Xl_Bancs1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_Bancs1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_Bancs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Bancs1.Name = "Xl_Bancs1"
        Me.Xl_Bancs1.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.Xl_Bancs1, True)
        Me.Xl_Bancs1.Size = New System.Drawing.Size(589, 261)
        Me.Xl_Bancs1.TabIndex = 0
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_Bancs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(589, 261)
        Me.Controls.Add(Me.Xl_Bancs1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_Bancs.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Frm_Bancs"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Bancs"
        CType(Me.Xl_Bancs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Bancs1 As Xl_Bancs
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
