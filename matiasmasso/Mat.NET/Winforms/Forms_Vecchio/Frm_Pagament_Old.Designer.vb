Public Partial Class Frm_Pagament_Old
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPagePndSel = New System.Windows.Forms.TabPage()
        Me.xl_pnd_select1 = New Winforms.Xl_Pnd_Select()
        Me.TabPageFpg = New System.Windows.Forms.TabPage()
        Me.LabelContraValor = New System.Windows.Forms.Label()
        Me.Xl_AmtCurContraValor = New Winforms.Xl_AmountCur()
        Me.LabelDifCanvi = New System.Windows.Forms.Label()
        Me.Xl_AmtCurSelEur = New Winforms.Xl_AmountCur()
        Me.Xl_AmtCurSel = New Winforms.Xl_AmountCur()
        Me.LabelLiq = New System.Windows.Forms.Label()
        Me.Xl_AmtCurLiq = New Winforms.Xl_AmountCur()
        Me.CheckBoxGts = New System.Windows.Forms.CheckBox()
        Me.Xl_AmtCurGts = New Winforms.Xl_AmountCur()
        Me.Xl_AmtCurDifCanvi = New Winforms.Xl_AmountCur()
        Me.Label1 = New System.Windows.Forms.Label()
        'Me.Xl_Pagaments1 = New Winforms.Xl_Pagaments()
        Me.TabPageEnd = New System.Windows.Forms.TabPage()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.ComboBoxMails = New System.Windows.Forms.ComboBox()
        Me.CheckBoxEmail = New System.Windows.Forms.CheckBox()
        Me.TextBoxEnd = New System.Windows.Forms.TextBox()
        Me.ButtonPrevious = New System.Windows.Forms.Button()
        Me.ButtonEnd = New System.Windows.Forms.Button()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile()
        Me.TabControl1.SuspendLayout()
        Me.TabPagePndSel.SuspendLayout()
        Me.TabPageFpg.SuspendLayout()
        Me.TabPageEnd.SuspendLayout()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPagePndSel)
        Me.TabControl1.Controls.Add(Me.TabPageFpg)
        Me.TabControl1.Controls.Add(Me.TabPageEnd)
        Me.TabControl1.Location = New System.Drawing.Point(358, 39)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(576, 369)
        Me.TabControl1.TabIndex = 1
        '
        'TabPagePndSel
        '
        Me.TabPagePndSel.Controls.Add(Me.xl_pnd_select1)
        Me.TabPagePndSel.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePndSel.Name = "TabPagePndSel"
        Me.TabPagePndSel.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePndSel.Size = New System.Drawing.Size(568, 343)
        Me.TabPagePndSel.TabIndex = 0
        Me.TabPagePndSel.Text = "SELECCIO"
        '
        'xl_pnd_select1
        '
        Me.xl_pnd_select1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xl_pnd_select1.Location = New System.Drawing.Point(3, 3)
        Me.xl_pnd_select1.Name = "xl_pnd_select1"
        Me.xl_pnd_select1.Size = New System.Drawing.Size(562, 337)
        Me.xl_pnd_select1.TabIndex = 0
        '
        'TabPageFpg
        '
        Me.TabPageFpg.Controls.Add(Me.LabelContraValor)
        Me.TabPageFpg.Controls.Add(Me.Xl_AmtCurContraValor)
        Me.TabPageFpg.Controls.Add(Me.LabelDifCanvi)
        Me.TabPageFpg.Controls.Add(Me.Xl_AmtCurSelEur)
        Me.TabPageFpg.Controls.Add(Me.Xl_AmtCurSel)
        Me.TabPageFpg.Controls.Add(Me.LabelLiq)
        Me.TabPageFpg.Controls.Add(Me.Xl_AmtCurLiq)
        Me.TabPageFpg.Controls.Add(Me.CheckBoxGts)
        Me.TabPageFpg.Controls.Add(Me.Xl_AmtCurGts)
        Me.TabPageFpg.Controls.Add(Me.Xl_AmtCurDifCanvi)
        Me.TabPageFpg.Controls.Add(Me.Label1)
        'Me.TabPageFpg.Controls.Add(Me.Xl_Pagaments1)
        Me.TabPageFpg.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFpg.Name = "TabPageFpg"
        Me.TabPageFpg.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageFpg.Size = New System.Drawing.Size(568, 343)
        Me.TabPageFpg.TabIndex = 1
        Me.TabPageFpg.Text = "MODALITAT"
        '
        'LabelContraValor
        '
        Me.LabelContraValor.AutoSize = True
        Me.LabelContraValor.Location = New System.Drawing.Point(23, 82)
        Me.LabelContraValor.Name = "LabelContraValor"
        Me.LabelContraValor.Size = New System.Drawing.Size(135, 13)
        Me.LabelContraValor.TabIndex = 27
        Me.LabelContraValor.Text = "contravalor pagat en Euros"
        '
        'Xl_AmtCurContraValor
        '
        Me.Xl_AmtCurContraValor.Amt = Nothing
        Me.Xl_AmtCurContraValor.Location = New System.Drawing.Point(63, 100)
        Me.Xl_AmtCurContraValor.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.Xl_AmtCurContraValor.Name = "Xl_AmtCurContraValor"
        Me.Xl_AmtCurContraValor.Size = New System.Drawing.Size(160, 20)
        Me.Xl_AmtCurContraValor.TabIndex = 28
        Me.Xl_AmtCurContraValor.Visible = False
        '
        'LabelDifCanvi
        '
        Me.LabelDifCanvi.AutoSize = True
        Me.LabelDifCanvi.Location = New System.Drawing.Point(23, 126)
        Me.LabelDifCanvi.Name = "LabelDifCanvi"
        Me.LabelDifCanvi.Size = New System.Drawing.Size(155, 13)
        Me.LabelDifCanvi.TabIndex = 29
        Me.LabelDifCanvi.Text = "diferencies de canvi (positives):"
        '
        'Xl_AmtCurSelEur
        '
        Me.Xl_AmtCurSelEur.Amt = Nothing
        Me.Xl_AmtCurSelEur.Enabled = False
        Me.Xl_AmtCurSelEur.Location = New System.Drawing.Point(63, 59)
        Me.Xl_AmtCurSelEur.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Xl_AmtCurSelEur.Name = "Xl_AmtCurSelEur"
        Me.Xl_AmtCurSelEur.Size = New System.Drawing.Size(160, 20)
        Me.Xl_AmtCurSelEur.TabIndex = 26
        Me.Xl_AmtCurSelEur.TabStop = False
        Me.Xl_AmtCurSelEur.Visible = False
        '
        'Xl_AmtCurSel
        '
        Me.Xl_AmtCurSel.Amt = Nothing
        Me.Xl_AmtCurSel.Location = New System.Drawing.Point(63, 40)
        Me.Xl_AmtCurSel.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Xl_AmtCurSel.Name = "Xl_AmtCurSel"
        Me.Xl_AmtCurSel.Size = New System.Drawing.Size(160, 20)
        Me.Xl_AmtCurSel.TabIndex = 25
        Me.Xl_AmtCurSel.TabStop = False
        '
        'LabelLiq
        '
        Me.LabelLiq.AutoSize = True
        Me.LabelLiq.Location = New System.Drawing.Point(23, 210)
        Me.LabelLiq.Name = "LabelLiq"
        Me.LabelLiq.Size = New System.Drawing.Size(34, 13)
        Me.LabelLiq.TabIndex = 33
        Me.LabelLiq.Text = "liquid:"
        Me.LabelLiq.Visible = False
        '
        'Xl_AmtCurLiq
        '
        Me.Xl_AmtCurLiq.Amt = Nothing
        Me.Xl_AmtCurLiq.Enabled = False
        Me.Xl_AmtCurLiq.Location = New System.Drawing.Point(63, 230)
        Me.Xl_AmtCurLiq.Name = "Xl_AmtCurLiq"
        Me.Xl_AmtCurLiq.Size = New System.Drawing.Size(160, 20)
        Me.Xl_AmtCurLiq.TabIndex = 34
        Me.Xl_AmtCurLiq.Visible = False
        '
        'CheckBoxGts
        '
        Me.CheckBoxGts.AutoSize = True
        Me.CheckBoxGts.Location = New System.Drawing.Point(23, 170)
        Me.CheckBoxGts.Margin = New System.Windows.Forms.Padding(3, 2, 3, 1)
        Me.CheckBoxGts.Name = "CheckBoxGts"
        Me.CheckBoxGts.Size = New System.Drawing.Size(74, 17)
        Me.CheckBoxGts.TabIndex = 31
        Me.CheckBoxGts.Text = "despeses:"
        '
        'Xl_AmtCurGts
        '
        Me.Xl_AmtCurGts.Amt = Nothing
        Me.Xl_AmtCurGts.Location = New System.Drawing.Point(63, 190)
        Me.Xl_AmtCurGts.Name = "Xl_AmtCurGts"
        Me.Xl_AmtCurGts.Size = New System.Drawing.Size(160, 20)
        Me.Xl_AmtCurGts.TabIndex = 32
        Me.Xl_AmtCurGts.Visible = False
        '
        'Xl_AmtCurDifCanvi
        '
        Me.Xl_AmtCurDifCanvi.Amt = Nothing
        Me.Xl_AmtCurDifCanvi.Enabled = False
        Me.Xl_AmtCurDifCanvi.Location = New System.Drawing.Point(63, 144)
        Me.Xl_AmtCurDifCanvi.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.Xl_AmtCurDifCanvi.Name = "Xl_AmtCurDifCanvi"
        Me.Xl_AmtCurDifCanvi.Size = New System.Drawing.Size(160, 20)
        Me.Xl_AmtCurDifCanvi.TabIndex = 30
        Me.Xl_AmtCurDifCanvi.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "seleccionat:"
        '
        'Xl_Pagaments1
        '
        'Me.Xl_Pagaments1.Location = New System.Drawing.Point(232, 31)
        'Me.Xl_Pagaments1.Name = "Xl_Pagaments1"
        'Me.Xl_Pagaments1.Size = New System.Drawing.Size(333, 241)
        'Me.Xl_Pagaments1.TabIndex = 35
        '
        'TabPageEnd
        '
        Me.TabPageEnd.Controls.Add(Me.TextBoxEmail)
        Me.TabPageEnd.Controls.Add(Me.ComboBoxMails)
        Me.TabPageEnd.Controls.Add(Me.CheckBoxEmail)
        Me.TabPageEnd.Controls.Add(Me.TextBoxEnd)
        Me.TabPageEnd.Location = New System.Drawing.Point(4, 22)
        Me.TabPageEnd.Name = "TabPageEnd"
        Me.TabPageEnd.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEnd.Size = New System.Drawing.Size(568, 343)
        Me.TabPageEnd.TabIndex = 2
        Me.TabPageEnd.Text = "FINAL"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Location = New System.Drawing.Point(207, 252)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(251, 20)
        Me.TextBoxEmail.TabIndex = 3
        '
        'ComboBoxMails
        '
        Me.ComboBoxMails.FormattingEnabled = True
        Me.ComboBoxMails.Location = New System.Drawing.Point(207, 238)
        Me.ComboBoxMails.Name = "ComboBoxMails"
        Me.ComboBoxMails.Size = New System.Drawing.Size(251, 21)
        Me.ComboBoxMails.TabIndex = 2
        '
        'CheckBoxEmail
        '
        Me.CheckBoxEmail.AutoSize = True
        Me.CheckBoxEmail.Checked = True
        Me.CheckBoxEmail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxEmail.Location = New System.Drawing.Point(207, 215)
        Me.CheckBoxEmail.Name = "CheckBoxEmail"
        Me.CheckBoxEmail.Size = New System.Drawing.Size(123, 17)
        Me.CheckBoxEmail.TabIndex = 1
        Me.CheckBoxEmail.Text = "notificar per e-mail a "
        Me.CheckBoxEmail.UseVisualStyleBackColor = True
        '
        'TextBoxEnd
        '
        Me.TextBoxEnd.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxEnd.Location = New System.Drawing.Point(207, 152)
        Me.TextBoxEnd.Name = "TextBoxEnd"
        Me.TextBoxEnd.Size = New System.Drawing.Size(251, 20)
        Me.TextBoxEnd.TabIndex = 0
        Me.TextBoxEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ButtonPrevious
        '
        Me.ButtonPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrevious.Enabled = False
        Me.ButtonPrevious.Location = New System.Drawing.Point(364, 414)
        Me.ButtonPrevious.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(96, 24)
        Me.ButtonPrevious.TabIndex = 25
        Me.ButtonPrevious.Text = "< ENRERA"
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.Enabled = False
        Me.ButtonEnd.Location = New System.Drawing.Point(838, 414)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(96, 24)
        Me.ButtonEnd.TabIndex = 26
        Me.ButtonEnd.Text = "FI >>"
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.Enabled = False
        Me.ButtonNext.Location = New System.Drawing.Point(734, 414)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(96, 24)
        Me.ButtonNext.TabIndex = 27
        Me.ButtonNext.Text = "SEGÜENT >"
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLogo.Location = New System.Drawing.Point(784, 5)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogo.TabIndex = 28
        Me.PictureBoxLogo.TabStop = False
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.BackColor = System.Drawing.SystemColors.MenuBar
        Me.TextBoxNom.Location = New System.Drawing.Point(457, 5)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.ReadOnly = True
        Me.TextBoxNom.Size = New System.Drawing.Size(321, 20)
        Me.TextBoxNom.TabIndex = 29
        Me.TextBoxNom.TabStop = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(358, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(91, 20)
        Me.DateTimePicker1.TabIndex = 31
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(4, 5)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 32
        '
        'Frm_Pagament
        '
        Me.ClientSize = New System.Drawing.Size(939, 440)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.ButtonPrevious)
        Me.Controls.Add(Me.ButtonEnd)
        Me.Controls.Add(Me.ButtonNext)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Pagament"
        Me.Text = "PAGAMENT A PROVEIDOR"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPagePndSel.ResumeLayout(False)
        Me.TabPageFpg.ResumeLayout(False)
        Me.TabPageFpg.PerformLayout()
        Me.TabPageEnd.ResumeLayout(False)
        Me.TabPageEnd.PerformLayout()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPagePndSel As System.Windows.Forms.TabPage
    Friend WithEvents TabPageFpg As System.Windows.Forms.TabPage
    Friend WithEvents TabPageEnd As System.Windows.Forms.TabPage
    Friend WithEvents ButtonPrevious As System.Windows.Forms.Button
    Friend WithEvents ButtonEnd As System.Windows.Forms.Button
    Friend WithEvents ButtonNext As System.Windows.Forms.Button
    Friend WithEvents xl_pnd_select1 As Xl_Pnd_Select
    'Friend WithEvents Xl_Pagaments1 As Xl_Pagaments
    Friend WithEvents PictureBoxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxEnd As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCurGts As Xl_AmountCur
    Friend WithEvents Xl_AmtCurDifCanvi As Xl_AmountCur
    Friend WithEvents Xl_AmtCurSelEur As Xl_AmountCur
    Friend WithEvents Xl_AmtCurSel As Xl_AmountCur
    Friend WithEvents LabelLiq As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCurLiq As Xl_AmountCur
    Friend WithEvents CheckBoxGts As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxMails As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxEmail As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents LabelDifCanvi As System.Windows.Forms.Label
    Friend WithEvents LabelContraValor As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCurContraValor As Xl_AmountCur
    Friend WithEvents Xl_DocFile1 As Winforms.Xl_DocFile
End Class
