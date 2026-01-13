<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_App
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
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxLastVersion = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxMinVersion = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxId = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.PanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxNom, "Frm_App.htm#Label1")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxNom, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxNom.Location = New System.Drawing.Point(91, 48)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxNom, True)
        Me.TextBoxNom.Size = New System.Drawing.Size(245, 20)
        Me.TextBoxNom.TabIndex = 57
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 166)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(348, 31)
        Me.PanelButtons.TabIndex = 55
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonCancel, "Frm_App.htm#ButtonCancel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonCancel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonCancel.Location = New System.Drawing.Point(129, 4)
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
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonOk, "Frm_App.htm#ButtonOk")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonOk, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonOk.Location = New System.Drawing.Point(240, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonOk, True)
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonDel, "Frm_App.htm#ButtonDel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonDel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonDel, True)
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Nom"
        '
        'TextBoxLastVersion
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxLastVersion, "Frm_App.htm#Label2")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxLastVersion, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxLastVersion.Location = New System.Drawing.Point(91, 74)
        Me.TextBoxLastVersion.Name = "TextBoxLastVersion"
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxLastVersion, True)
        Me.TextBoxLastVersion.Size = New System.Drawing.Size(121, 20)
        Me.TextBoxLastVersion.TabIndex = 59
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Ultima versió"
        '
        'TextBoxMinVersion
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxMinVersion, "Frm_App.htm#Label3")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxMinVersion, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxMinVersion.Location = New System.Drawing.Point(91, 100)
        Me.TextBoxMinVersion.Name = "TextBoxMinVersion"
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxMinVersion, True)
        Me.TextBoxMinVersion.Size = New System.Drawing.Size(121, 20)
        Me.TextBoxMinVersion.TabIndex = 61
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Mínima versió"
        '
        'ComboBoxId
        '
        Me.ComboBoxId.FormattingEnabled = True
        Me.HelpProviderHG.SetHelpKeyword(Me.ComboBoxId, "Frm_App.htm#Label4")
        Me.HelpProviderHG.SetHelpNavigator(Me.ComboBoxId, System.Windows.Forms.HelpNavigator.Topic)
        Me.ComboBoxId.Location = New System.Drawing.Point(91, 21)
        Me.ComboBoxId.Name = "ComboBoxId"
        Me.HelpProviderHG.SetShowHelp(Me.ComboBoxId, True)
        Me.ComboBoxId.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxId.TabIndex = 62
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(16, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "Id"
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_App
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(348, 197)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBoxId)
        Me.Controls.Add(Me.TextBoxMinVersion)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxLastVersion)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.Label1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_App.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Frm_App"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Apps"
        Me.PanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxLastVersion As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxMinVersion As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBoxId As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
