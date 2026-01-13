<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AmortizationItem
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxImmobilitzat = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxBaixa = New System.Windows.Forms.CheckBox()
        Me.Xl_PercentTipus = New Mat.Net.Xl_Percent()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_AmountCur1 = New Mat.Net.Xl_AmountCur()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.HelpProviderHG.SetHelpKeyword(Me.DateTimePicker1, "Frm_AmortizationItem.htm#Label2")
        Me.HelpProviderHG.SetHelpNavigator(Me.DateTimePicker1, System.Windows.Forms.HelpNavigator.Topic)
        Me.DateTimePicker1.Location = New System.Drawing.Point(77, 80)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.HelpProviderHG.SetShowHelp(Me.DateTimePicker1, True)
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 52
        '
        'TextBoxImmobilitzat
        '
        Me.TextBoxImmobilitzat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxImmobilitzat, "Frm_AmortizationItem.htm#Label1")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxImmobilitzat, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxImmobilitzat.Location = New System.Drawing.Point(77, 58)
        Me.TextBoxImmobilitzat.Name = "TextBoxImmobilitzat"
        Me.TextBoxImmobilitzat.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxImmobilitzat, True)
        Me.TextBoxImmobilitzat.Size = New System.Drawing.Size(283, 20)
        Me.TextBoxImmobilitzat.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Inmobilitzat"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 230)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(372, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonCancel, "Frm_AmortizationItem.htm#ButtonCancel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonCancel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonCancel.Location = New System.Drawing.Point(153, 4)
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
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonOk, "Frm_AmortizationItem.htm#ButtonOk")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonOk, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonOk.Location = New System.Drawing.Point(264, 4)
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
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonDel, "Frm_AmortizationItem.htm#ButtonDel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonDel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonDel, True)
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Data"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Import"
        '
        'CheckBoxBaixa
        '
        Me.CheckBoxBaixa.AutoSize = True
        Me.HelpProviderHG.SetHelpKeyword(Me.CheckBoxBaixa, "Frm_AmortizationItem.htm#CheckBoxBaixa")
        Me.HelpProviderHG.SetHelpNavigator(Me.CheckBoxBaixa, System.Windows.Forms.HelpNavigator.Topic)
        Me.CheckBoxBaixa.Location = New System.Drawing.Point(77, 152)
        Me.CheckBoxBaixa.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxBaixa.Name = "CheckBoxBaixa"
        Me.HelpProviderHG.SetShowHelp(Me.CheckBoxBaixa, True)
        Me.CheckBoxBaixa.Size = New System.Drawing.Size(65, 17)
        Me.CheckBoxBaixa.TabIndex = 56
        Me.CheckBoxBaixa.Text = "es baixa"
        Me.CheckBoxBaixa.UseVisualStyleBackColor = True
        '
        'Xl_PercentTipus
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_PercentTipus, "Frm_AmortizationItem.htm#Xl_PercentTipus")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_PercentTipus, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_PercentTipus.Location = New System.Drawing.Point(77, 104)
        Me.Xl_PercentTipus.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_PercentTipus.Name = "Xl_PercentTipus"
        Me.HelpProviderHG.SetShowHelp(Me.Xl_PercentTipus, True)
        Me.Xl_PercentTipus.Size = New System.Drawing.Size(83, 20)
        Me.Xl_PercentTipus.TabIndex = 57
        Me.Xl_PercentTipus.Text = "0 %"
        Me.Xl_PercentTipus.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Tipus"
        '
        'Xl_AmountCur1
        '
        Me.Xl_AmountCur1.Amt = Nothing
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_AmountCur1, "Frm_AmortizationItem.htm#Xl_AmtCur")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_AmountCur1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_AmountCur1.Location = New System.Drawing.Point(77, 128)
        Me.Xl_AmountCur1.Name = "Xl_AmountCur1"
        Me.HelpProviderHG.SetShowHelp(Me.Xl_AmountCur1, True)
        Me.Xl_AmountCur1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmountCur1.TabIndex = 59
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_AmortizationItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(372, 261)
        Me.Controls.Add(Me.Xl_AmountCur1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_PercentTipus)
        Me.Controls.Add(Me.CheckBoxBaixa)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxImmobilitzat)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_AmortizationItem.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Frm_AmortizationItem"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Amortització"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents TextBoxImmobilitzat As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents CheckBoxBaixa As CheckBox
    Friend WithEvents Xl_PercentTipus As Xl_Percent
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_AmountCur1 As Xl_AmountCur
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
