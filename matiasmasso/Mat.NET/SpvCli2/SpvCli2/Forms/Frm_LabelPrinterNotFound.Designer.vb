<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_LabelPrinterNotFound
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
        Me.ListBoxPrinters = New System.Windows.Forms.ListBox()
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
        Me.Panel1.Location = New System.Drawing.Point(0, 233)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(432, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonCancel, "Frm_LabelPrinterNotFound.htm#ButtonCancel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonCancel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonCancel.Location = New System.Drawing.Point(213, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonCancel, True)
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonOk, "Frm_LabelPrinterNotFound.htm#ButtonOk")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonOk, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonOk.Location = New System.Drawing.Point(324, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonOk, True)
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ListBoxPrinters
        '
        Me.ListBoxPrinters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBoxPrinters.FormattingEnabled = True
        Me.HelpProviderHG.SetHelpKeyword(Me.ListBoxPrinters, "Frm_LabelPrinterNotFound.htm#ListBoxPrinters")
        Me.HelpProviderHG.SetHelpNavigator(Me.ListBoxPrinters, System.Windows.Forms.HelpNavigator.Topic)
        Me.ListBoxPrinters.Location = New System.Drawing.Point(0, 0)
        Me.ListBoxPrinters.Name = "ListBoxPrinters"
        Me.HelpProviderHG.SetShowHelp(Me.ListBoxPrinters, True)
        Me.ListBoxPrinters.Size = New System.Drawing.Size(432, 233)
        Me.ListBoxPrinters.TabIndex = 43
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_LabelPrinterNotFound
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(432, 264)
        Me.Controls.Add(Me.ListBoxPrinters)
        Me.Controls.Add(Me.Panel1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_LabelPrinterNotFound.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Frm_LabelPrinterNotFound"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "SELECCIONAR IMPRESORA ETIQUETES"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ListBoxPrinters As System.Windows.Forms.ListBox
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
