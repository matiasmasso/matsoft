<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AeatModels
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
        Me.Xl_AeatModels1 = New Mat.Net.Xl_AeatModels()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        CType(Me.Xl_AeatModels1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_AeatModels1
        '
        Me.Xl_AeatModels1.AllowUserToAddRows = False
        Me.Xl_AeatModels1.AllowUserToDeleteRows = False
        Me.Xl_AeatModels1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_AeatModels1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_AeatModels1.Filter = Nothing
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_AeatModels1, "Frm_AeatModels.htm#Xl_AeatModels1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_AeatModels1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_AeatModels1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_AeatModels1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Xl_AeatModels1.Name = "Xl_AeatModels1"
        Me.Xl_AeatModels1.ReadOnly = True
        Me.Xl_AeatModels1.RowTemplate.Height = 40
        Me.HelpProviderHG.SetShowHelp(Me.Xl_AeatModels1, True)
        Me.Xl_AeatModels1.Size = New System.Drawing.Size(376, 240)
        Me.Xl_AeatModels1.TabIndex = 0
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_AeatModels
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(376, 240)
        Me.Controls.Add(Me.Xl_AeatModels1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_AeatModels.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Name = "Frm_AeatModels"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Models d'Hisenda i altres oficials"
        CType(Me.Xl_AeatModels1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_AeatModels1 As Xl_AeatModels
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
