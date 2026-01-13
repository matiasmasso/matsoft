<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_AeatModel
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
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.TextBoxDsc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxSoloInfo = New System.Windows.Forms.CheckBox()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.ComboBoxPeriodType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_AeatDocs1 = New Winforms.Xl_AeatDocs()
        Me.CheckBoxVisibleBancs = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_AeatDocs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 350)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(330, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(111, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
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
        Me.ButtonOk.Location = New System.Drawing.Point(222, 4)
        Me.ButtonOk.Name = "ButtonOk"
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
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Nom"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(12, 49)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(196, 20)
        Me.TextBoxNom.TabIndex = 43
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDsc.Location = New System.Drawing.Point(11, 95)
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(197, 20)
        Me.TextBoxDsc.TabIndex = 51
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Descripció"
        '
        'CheckBoxSoloInfo
        '
        Me.CheckBoxSoloInfo.AutoSize = True
        Me.CheckBoxSoloInfo.Location = New System.Drawing.Point(11, 131)
        Me.CheckBoxSoloInfo.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxSoloInfo.Name = "CheckBoxSoloInfo"
        Me.CheckBoxSoloInfo.Size = New System.Drawing.Size(188, 17)
        Me.CheckBoxSoloInfo.TabIndex = 52
        Me.CheckBoxSoloInfo.Text = "Declaració informativa sense valor"
        Me.CheckBoxSoloInfo.UseVisualStyleBackColor = True
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Location = New System.Drawing.Point(11, 176)
        Me.ComboBoxCod.Margin = New System.Windows.Forms.Padding(1)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(114, 21)
        Me.ComboBoxCod.TabIndex = 53
        '
        'ComboBoxPeriodType
        '
        Me.ComboBoxPeriodType.FormattingEnabled = True
        Me.ComboBoxPeriodType.Location = New System.Drawing.Point(11, 228)
        Me.ComboBoxPeriodType.Margin = New System.Windows.Forms.Padding(1)
        Me.ComboBoxPeriodType.Name = "ComboBoxPeriodType"
        Me.ComboBoxPeriodType.Size = New System.Drawing.Size(114, 21)
        Me.ComboBoxPeriodType.TabIndex = 54
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 161)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Codi"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 214)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "Periodicitat"
        '
        'Xl_AeatDocs1
        '
        Me.Xl_AeatDocs1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AeatDocs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_AeatDocs1.Location = New System.Drawing.Point(212, 21)
        Me.Xl_AeatDocs1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_AeatDocs1.Name = "Xl_AeatDocs1"
        Me.Xl_AeatDocs1.RowTemplate.Height = 40
        Me.Xl_AeatDocs1.Size = New System.Drawing.Size(114, 325)
        Me.Xl_AeatDocs1.TabIndex = 49
        '
        'CheckBoxVisibleBancs
        '
        Me.CheckBoxVisibleBancs.AutoSize = True
        Me.CheckBoxVisibleBancs.Location = New System.Drawing.Point(12, 277)
        Me.CheckBoxVisibleBancs.Name = "CheckBoxVisibleBancs"
        Me.CheckBoxVisibleBancs.Size = New System.Drawing.Size(122, 17)
        Me.CheckBoxVisibleBancs.TabIndex = 57
        Me.CheckBoxVisibleBancs.Text = "Visible per els bancs"
        Me.CheckBoxVisibleBancs.UseVisualStyleBackColor = True
        '
        'Frm_AeatModel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(330, 381)
        Me.Controls.Add(Me.CheckBoxVisibleBancs)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBoxPeriodType)
        Me.Controls.Add(Me.ComboBoxCod)
        Me.Controls.Add(Me.CheckBoxSoloInfo)
        Me.Controls.Add(Me.TextBoxDsc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_AeatDocs1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_AeatModel"
        Me.Text = "Model de Hisenda"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_AeatDocs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Xl_AeatDocs1 As Xl_AeatDocs
    Friend WithEvents TextBoxDsc As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckBoxSoloInfo As CheckBox
    Friend WithEvents ComboBoxCod As ComboBox
    Friend WithEvents ComboBoxPeriodType As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents CheckBoxVisibleBancs As CheckBox
End Class
