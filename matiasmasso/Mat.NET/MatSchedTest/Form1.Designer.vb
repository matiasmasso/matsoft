<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ButtonImportaInbox = New System.Windows.Forms.Button()
        Me.TextBoxLog = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ButtonTest = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.Button1, "Form1.htm#Button1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Button1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Button1.Location = New System.Drawing.Point(105, 77)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.HelpProviderHG.SetShowHelp(Me.Button1, True)
        Me.Button1.Size = New System.Drawing.Size(220, 35)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Timer elapsed"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ButtonImportaInbox
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonImportaInbox, "Form1.htm#ButtonImportaInbox")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonImportaInbox, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonImportaInbox.Location = New System.Drawing.Point(64, 32)
        Me.ButtonImportaInbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ButtonImportaInbox.Name = "ButtonImportaInbox"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonImportaInbox, True)
        Me.ButtonImportaInbox.Size = New System.Drawing.Size(144, 35)
        Me.ButtonImportaInbox.TabIndex = 1
        Me.ButtonImportaInbox.Text = "importa inbox"
        Me.ButtonImportaInbox.UseVisualStyleBackColor = True
        '
        'TextBoxLog
        '
        Me.TextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxLog, "Form1.htm#TextBoxLog")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxLog, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxLog.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxLog.Multiline = True
        Me.TextBoxLog.Name = "TextBoxLog"
        Me.TextBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxLog, True)
        Me.TextBoxLog.Size = New System.Drawing.Size(471, 225)
        Me.TextBoxLog.TabIndex = 2
        '
        'Button2
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.Button2, "Form1.htm#Button2")
        Me.HelpProviderHG.SetHelpNavigator(Me.Button2, System.Windows.Forms.HelpNavigator.Topic)
        Me.Button2.Location = New System.Drawing.Point(216, 32)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button2.Name = "Button2"
        Me.HelpProviderHG.SetShowHelp(Me.Button2, True)
        Me.Button2.Size = New System.Drawing.Size(144, 35)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "exporta outbox"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TextBoxLog)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 125)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(471, 260)
        Me.Panel1.TabIndex = 4
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 225)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(471, 35)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        Me.ProgressBar1.Visible = False
        '
        'ButtonTest
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonTest, "Form1.htm#Button2")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonTest, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonTest.Location = New System.Drawing.Point(327, 77)
        Me.ButtonTest.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ButtonTest.Name = "ButtonTest"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonTest, True)
        Me.ButtonTest.Size = New System.Drawing.Size(144, 35)
        Me.ButtonTest.TabIndex = 5
        Me.ButtonTest.Text = "test"
        Me.ButtonTest.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(471, 386)
        Me.Controls.Add(Me.ButtonTest)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ButtonImportaInbox)
        Me.Controls.Add(Me.Button1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Form1.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Form1"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "MatSchedService debugger"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents ButtonImportaInbox As Button
    Friend WithEvents TextBoxLog As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents HelpProviderHG As HelpProvider
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ButtonTest As Button
End Class
