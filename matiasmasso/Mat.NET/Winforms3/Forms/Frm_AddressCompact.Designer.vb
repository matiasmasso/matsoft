<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AddressCompact
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_LookupZip1 = New Mat.Net.Xl_LookupZip()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 116)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonCancel, "Frm_AddressCompact.htm#ButtonCancel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonCancel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonCancel.Location = New System.Drawing.Point(581, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonCancel, True)
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonOk, "Frm_AddressCompact.htm#ButtonOk")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonOk, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonOk.Location = New System.Drawing.Point(692, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonOk, True)
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_LookupZip1
        '
        Me.Xl_LookupZip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_LookupZip1, "Frm_AddressCompact.htm#Xl_LookupTextboxButton")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_LookupZip1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_LookupZip1.IsDirty = False
        Me.Xl_LookupZip1.Location = New System.Drawing.Point(81, 27)
        Me.Xl_LookupZip1.Name = "Xl_LookupZip1"
        Me.Xl_LookupZip1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupZip1.ReadOnlyLookup = False
        Me.HelpProviderHG.SetShowHelp(Me.Xl_LookupZip1, True)
        Me.Xl_LookupZip1.Size = New System.Drawing.Size(707, 20)
        Me.Xl_LookupZip1.TabIndex = 43
        Me.Xl_LookupZip1.Value = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Població"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Adreça"
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBox1, "Frm_AddressCompact.htm#Label2")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBox1, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBox1.Location = New System.Drawing.Point(81, 54)
        Me.TextBox1.Name = "TextBox1"
        Me.HelpProviderHG.SetShowHelp(Me.TextBox1, True)
        Me.TextBox1.Size = New System.Drawing.Size(707, 20)
        Me.TextBox1.TabIndex = 46
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_AddressCompact
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 147)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_LookupZip1)
        Me.Controls.Add(Me.Panel1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_AddressCompact.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Frm_AddressCompact"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Frm_AddressCompact"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents Xl_LookupZip1 As Xl_LookupZip
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
