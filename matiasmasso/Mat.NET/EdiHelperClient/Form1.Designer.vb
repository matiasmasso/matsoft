<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ButtonImport = New System.Windows.Forms.Button()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBox1, "Form1_1.htm#TextBox1")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBox1, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBox1.Location = New System.Drawing.Point(35, 44)
        Me.TextBox1.Name = "TextBox1"
        Me.HelpProviderHG.SetShowHelp(Me.TextBox1, True)
        Me.TextBox1.Size = New System.Drawing.Size(272, 20)
        Me.TextBox1.TabIndex = 0
        '
        'ButtonImport
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonImport, "Form1_1.htm#ButtonImport")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonImport, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonImport.Location = New System.Drawing.Point(313, 42)
        Me.ButtonImport.Name = "ButtonImport"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonImport, True)
        Me.ButtonImport.Size = New System.Drawing.Size(75, 23)
        Me.ButtonImport.TabIndex = 1
        Me.ButtonImport.Text = "Importar"
        Me.ButtonImport.UseVisualStyleBackColor = True
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 175)
        Me.Controls.Add(Me.ButtonImport)
        Me.Controls.Add(Me.TextBox1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Form1_1.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Form1"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ButtonImport As Button
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
