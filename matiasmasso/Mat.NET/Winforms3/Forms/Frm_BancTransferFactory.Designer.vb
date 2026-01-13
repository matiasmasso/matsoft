<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BancTransferFactory
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_BancTransferBeneficiarisFactory1 = New Mat.Net.Xl_BancTransferBeneficiarisFactory()
        Me.Xl_BancsComboBox1 = New Mat.Net.Xl_BancsComboBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LabelTot = New System.Windows.Forms.Label()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_BancTransferBeneficiarisFactory1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(153, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Banc emissor:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.HelpProviderHG.SetHelpKeyword(Me.DateTimePicker1, "Frm_BancTransferFactory.htm#LabelTot")
        Me.HelpProviderHG.SetHelpNavigator(Me.DateTimePicker1, System.Windows.Forms.HelpNavigator.Topic)
        Me.DateTimePicker1.Location = New System.Drawing.Point(700, 4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.HelpProviderHG.SetShowHelp(Me.DateTimePicker1, True)
        Me.DateTimePicker1.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePicker1.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 333)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(801, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonCancel, "Frm_BancTransferFactory.htm#ButtonCancel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonCancel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonCancel.Location = New System.Drawing.Point(582, 4)
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
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonOk, "Frm_BancTransferFactory.htm#ButtonOk")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonOk, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonOk.Location = New System.Drawing.Point(693, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonOk, True)
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_BancTransferBeneficiarisFactory1
        '
        Me.Xl_BancTransferBeneficiarisFactory1.AllowUserToAddRows = False
        Me.Xl_BancTransferBeneficiarisFactory1.AllowUserToDeleteRows = False
        Me.Xl_BancTransferBeneficiarisFactory1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BancTransferBeneficiarisFactory1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BancTransferBeneficiarisFactory1.DisplayObsolets = False
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_BancTransferBeneficiarisFactory1, "Frm_BancTransferFactory.htm#Xl_BancTransferBeneficiarisFactory1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_BancTransferBeneficiarisFactory1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_BancTransferBeneficiarisFactory1.Location = New System.Drawing.Point(0, 30)
        Me.Xl_BancTransferBeneficiarisFactory1.MouseIsDown = False
        Me.Xl_BancTransferBeneficiarisFactory1.Name = "Xl_BancTransferBeneficiarisFactory1"
        Me.Xl_BancTransferBeneficiarisFactory1.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.Xl_BancTransferBeneficiarisFactory1, True)
        Me.Xl_BancTransferBeneficiarisFactory1.Size = New System.Drawing.Size(801, 301)
        Me.Xl_BancTransferBeneficiarisFactory1.TabIndex = 43
        '
        'Xl_BancsComboBox1
        '
        Me.Xl_BancsComboBox1.FormattingEnabled = True
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_BancsComboBox1, "Frm_BancTransferFactory.htm#Xl_BancsComboBox1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_BancsComboBox1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_BancsComboBox1.Location = New System.Drawing.Point(232, 3)
        Me.Xl_BancsComboBox1.Name = "Xl_BancsComboBox1"
        Me.HelpProviderHG.SetShowHelp(Me.Xl_BancsComboBox1, True)
        Me.Xl_BancsComboBox1.Size = New System.Drawing.Size(163, 21)
        Me.Xl_BancsComboBox1.TabIndex = 44
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(801, 24)
        Me.MenuStrip1.TabIndex = 45
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'LabelTot
        '
        Me.LabelTot.AutoSize = True
        Me.LabelTot.Location = New System.Drawing.Point(441, 7)
        Me.LabelTot.Name = "LabelTot"
        Me.LabelTot.Size = New System.Drawing.Size(34, 13)
        Me.LabelTot.TabIndex = 46
        Me.LabelTot.Text = "Total:"
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_BancTransferFactory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(801, 364)
        Me.Controls.Add(Me.LabelTot)
        Me.Controls.Add(Me.Xl_BancsComboBox1)
        Me.Controls.Add(Me.Xl_BancTransferBeneficiarisFactory1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_BancTransferFactory.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_BancTransferFactory"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Transferencies"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_BancTransferBeneficiarisFactory1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents Xl_BancTransferBeneficiarisFactory1 As Xl_BancTransferBeneficiarisFactory
    Friend WithEvents Xl_BancsComboBox1 As Xl_BancsComboBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LabelTot As Label
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
