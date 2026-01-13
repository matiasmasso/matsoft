<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_AeatDoc
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TextBoxModel = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.NumericUpDownPeriod = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_DocFile1 = New Mat.Net.Xl_DocFile_Old()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 488)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(359, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonDel, "Frm_AeatDoc.htm#ButtonDel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonDel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonDel.Location = New System.Drawing.Point(3, 3)
        Me.ButtonDel.Name = "ButtonDel"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonDel, True)
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 13
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonCancel, "Frm_AeatDoc.htm#ButtonCancel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonCancel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonCancel.Location = New System.Drawing.Point(140, 4)
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
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonOk, "Frm_AeatDoc.htm#ButtonOk")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonOk, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonOk.Location = New System.Drawing.Point(251, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonOk, True)
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TextBoxModel
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxModel, "Frm_AeatDoc.htm#TextBoxModel")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxModel, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxModel.Location = New System.Drawing.Point(6, 10)
        Me.TextBoxModel.Name = "TextBoxModel"
        Me.TextBoxModel.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxModel, True)
        Me.TextBoxModel.Size = New System.Drawing.Size(347, 20)
        Me.TextBoxModel.TabIndex = 45
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "datai:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.HelpProviderHG.SetHelpKeyword(Me.DateTimePicker1, "Frm_AeatDoc.htm#Label3")
        Me.HelpProviderHG.SetHelpNavigator(Me.DateTimePicker1, System.Windows.Forms.HelpNavigator.Topic)
        Me.DateTimePicker1.Location = New System.Drawing.Point(50, 36)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.HelpProviderHG.SetShowHelp(Me.DateTimePicker1, True)
        Me.DateTimePicker1.Size = New System.Drawing.Size(102, 20)
        Me.DateTimePicker1.TabIndex = 48
        '
        'NumericUpDownPeriod
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.NumericUpDownPeriod, "Frm_AeatDoc.htm#NumericUpDownPeriod")
        Me.HelpProviderHG.SetHelpNavigator(Me.NumericUpDownPeriod, System.Windows.Forms.HelpNavigator.Topic)
        Me.NumericUpDownPeriod.Location = New System.Drawing.Point(304, 36)
        Me.NumericUpDownPeriod.Name = "NumericUpDownPeriod"
        Me.HelpProviderHG.SetShowHelp(Me.NumericUpDownPeriod, True)
        Me.NumericUpDownPeriod.Size = New System.Drawing.Size(49, 20)
        Me.NumericUpDownPeriod.TabIndex = 46
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(236, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "periode:"
        '
        'Xl_DocFile1
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_DocFile1, "Frm_AeatDoc.htm#Xl_DocFile")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_DocFile1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(3, 62)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.HelpProviderHG.SetShowHelp(Me.Xl_DocFile1, True)
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 50
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_AeatDoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 519)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.NumericUpDownPeriod)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxModel)
        Me.Controls.Add(Me.Panel1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_AeatDoc.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Frm_AeatDoc"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "DECLARACIO HISENDA"
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBoxModel As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents NumericUpDownPeriod As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_DocFile1 As Mat.Net.Xl_DocFile_Old
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
