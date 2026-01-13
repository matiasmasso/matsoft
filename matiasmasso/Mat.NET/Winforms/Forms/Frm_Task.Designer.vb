<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Task
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
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.LabelTimeSpan = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxNextRunFch = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxLastLogFch = New System.Windows.Forms.TextBox()
        Me.PictureBoxLastLogResult = New System.Windows.Forms.PictureBox()
        Me.DateTimePickerNotAfter = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerNotBefore = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxNotAfter = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNotBefore = New System.Windows.Forms.CheckBox()
        Me.CheckBoxEnabled = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.TextBoxDsc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TextBoxMsg = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_TaskSchedules1 = New Winforms.Xl_TaskSchedules()
        Me.Xl_TaskLogs1 = New Winforms.Xl_TaskLogs()
        Me.TextBoxResult = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.PictureBoxLastLogResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_TaskSchedules1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_TaskLogs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(103, 51)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(278, 20)
        Me.TextBoxNom.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Nom"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 346)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(398, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(179, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(290, 4)
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
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(1, 27)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(396, 319)
        Me.TabControl1.TabIndex = 53
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxResult)
        Me.TabPage1.Controls.Add(Me.LabelTimeSpan)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxNextRunFch)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.TextBoxLastLogFch)
        Me.TabPage1.Controls.Add(Me.PictureBoxLastLogResult)
        Me.TabPage1.Controls.Add(Me.DateTimePickerNotAfter)
        Me.TabPage1.Controls.Add(Me.DateTimePickerNotBefore)
        Me.TabPage1.Controls.Add(Me.CheckBoxNotAfter)
        Me.TabPage1.Controls.Add(Me.CheckBoxNotBefore)
        Me.TabPage1.Controls.Add(Me.CheckBoxEnabled)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.ComboBoxCod)
        Me.TabPage1.Controls.Add(Me.TextBoxDsc)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(388, 293)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'LabelTimeSpan
        '
        Me.LabelTimeSpan.AutoSize = True
        Me.LabelTimeSpan.Location = New System.Drawing.Point(206, 156)
        Me.LabelTimeSpan.Name = "LabelTimeSpan"
        Me.LabelTimeSpan.Size = New System.Drawing.Size(44, 13)
        Me.LabelTimeSpan.TabIndex = 75
        Me.LabelTimeSpan.Text = "d'aqui a"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 156)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 13)
        Me.Label6.TabIndex = 73
        Me.Label6.Text = "Propera execució"
        '
        'TextBoxNextRunFch
        '
        Me.TextBoxNextRunFch.Location = New System.Drawing.Point(103, 153)
        Me.TextBoxNextRunFch.Name = "TextBoxNextRunFch"
        Me.TextBoxNextRunFch.ReadOnly = True
        Me.TextBoxNextRunFch.Size = New System.Drawing.Size(88, 20)
        Me.TextBoxNextRunFch.TabIndex = 72
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 13)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Darrera execució"
        '
        'TextBoxLastLogFch
        '
        Me.TextBoxLastLogFch.Location = New System.Drawing.Point(103, 127)
        Me.TextBoxLastLogFch.Name = "TextBoxLastLogFch"
        Me.TextBoxLastLogFch.Size = New System.Drawing.Size(88, 20)
        Me.TextBoxLastLogFch.TabIndex = 1
        '
        'PictureBoxLastLogResult
        '
        Me.PictureBoxLastLogResult.Location = New System.Drawing.Point(197, 130)
        Me.PictureBoxLastLogResult.Name = "PictureBoxLastLogResult"
        Me.PictureBoxLastLogResult.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxLastLogResult.TabIndex = 0
        Me.PictureBoxLastLogResult.TabStop = False
        '
        'DateTimePickerNotAfter
        '
        Me.DateTimePickerNotAfter.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerNotAfter.Location = New System.Drawing.Point(284, 218)
        Me.DateTimePickerNotAfter.Name = "DateTimePickerNotAfter"
        Me.DateTimePickerNotAfter.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePickerNotAfter.TabIndex = 61
        Me.DateTimePickerNotAfter.Visible = False
        '
        'DateTimePickerNotBefore
        '
        Me.DateTimePickerNotBefore.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerNotBefore.Location = New System.Drawing.Point(284, 196)
        Me.DateTimePickerNotBefore.Name = "DateTimePickerNotBefore"
        Me.DateTimePickerNotBefore.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePickerNotBefore.TabIndex = 60
        Me.DateTimePickerNotBefore.Visible = False
        '
        'CheckBoxNotAfter
        '
        Me.CheckBoxNotAfter.AutoSize = True
        Me.CheckBoxNotAfter.Location = New System.Drawing.Point(102, 223)
        Me.CheckBoxNotAfter.Name = "CheckBoxNotAfter"
        Me.CheckBoxNotAfter.Size = New System.Drawing.Size(130, 17)
        Me.CheckBoxNotAfter.TabIndex = 59
        Me.CheckBoxNotAfter.Text = "No activar després de"
        Me.CheckBoxNotAfter.UseVisualStyleBackColor = True
        '
        'CheckBoxNotBefore
        '
        Me.CheckBoxNotBefore.AutoSize = True
        Me.CheckBoxNotBefore.Location = New System.Drawing.Point(102, 199)
        Me.CheckBoxNotBefore.Name = "CheckBoxNotBefore"
        Me.CheckBoxNotBefore.Size = New System.Drawing.Size(122, 17)
        Me.CheckBoxNotBefore.TabIndex = 58
        Me.CheckBoxNotBefore.Text = "No activar abans de"
        Me.CheckBoxNotBefore.UseVisualStyleBackColor = True
        '
        'CheckBoxEnabled
        '
        Me.CheckBoxEnabled.AutoSize = True
        Me.CheckBoxEnabled.Location = New System.Drawing.Point(102, 246)
        Me.CheckBoxEnabled.Name = "CheckBoxEnabled"
        Me.CheckBoxEnabled.Size = New System.Drawing.Size(62, 17)
        Me.CheckBoxEnabled.TabIndex = 57
        Me.CheckBoxEnabled.Text = "habilitat"
        Me.CheckBoxEnabled.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "Codi"
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Location = New System.Drawing.Point(103, 24)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(278, 21)
        Me.ComboBoxCod.TabIndex = 55
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDsc.Location = New System.Drawing.Point(103, 77)
        Me.TextBoxDsc.Multiline = True
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(278, 44)
        Me.TextBoxDsc.TabIndex = 54
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Descripció"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_TaskSchedules1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(388, 293)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Agenda"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.SplitContainer1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(388, 293)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Log"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_TaskLogs1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxMsg)
        Me.SplitContainer1.Size = New System.Drawing.Size(388, 293)
        Me.SplitContainer1.SplitterDistance = 151
        Me.SplitContainer1.TabIndex = 0
        '
        'TextBoxMsg
        '
        Me.TextBoxMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxMsg.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxMsg.Multiline = True
        Me.TextBoxMsg.Name = "TextBoxMsg"
        Me.TextBoxMsg.Size = New System.Drawing.Size(233, 293)
        Me.TextBoxMsg.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(398, 24)
        Me.MenuStrip1.TabIndex = 54
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'Xl_TaskSchedules1
        '
        Me.Xl_TaskSchedules1.AllowUserToAddRows = False
        Me.Xl_TaskSchedules1.AllowUserToDeleteRows = False
        Me.Xl_TaskSchedules1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_TaskSchedules1.DisplayObsolets = False
        Me.Xl_TaskSchedules1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_TaskSchedules1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_TaskSchedules1.MouseIsDown = False
        Me.Xl_TaskSchedules1.Name = "Xl_TaskSchedules1"
        Me.Xl_TaskSchedules1.ReadOnly = True
        Me.Xl_TaskSchedules1.Size = New System.Drawing.Size(382, 287)
        Me.Xl_TaskSchedules1.TabIndex = 0
        '
        'Xl_TaskLogs1
        '
        Me.Xl_TaskLogs1.AllowUserToAddRows = False
        Me.Xl_TaskLogs1.AllowUserToDeleteRows = False
        Me.Xl_TaskLogs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_TaskLogs1.DisplayObsolets = False
        Me.Xl_TaskLogs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_TaskLogs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_TaskLogs1.MouseIsDown = False
        Me.Xl_TaskLogs1.Name = "Xl_TaskLogs1"
        Me.Xl_TaskLogs1.ReadOnly = True
        Me.Xl_TaskLogs1.Size = New System.Drawing.Size(151, 293)
        Me.Xl_TaskLogs1.TabIndex = 0
        '
        'TextBoxResult
        '
        Me.TextBoxResult.BackColor = System.Drawing.SystemColors.Window
        Me.TextBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxResult.Location = New System.Drawing.Point(219, 130)
        Me.TextBoxResult.Name = "TextBoxResult"
        Me.TextBoxResult.ReadOnly = True
        Me.TextBoxResult.Size = New System.Drawing.Size(162, 13)
        Me.TextBoxResult.TabIndex = 77
        '
        'Frm_Task
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 377)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Task"
        Me.Text = "Tasca"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.PictureBoxLastLogResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_TaskSchedules1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_TaskLogs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents DateTimePickerNotAfter As DateTimePicker
    Friend WithEvents DateTimePickerNotBefore As DateTimePicker
    Friend WithEvents CheckBoxNotAfter As CheckBox
    Friend WithEvents CheckBoxNotBefore As CheckBox
    Friend WithEvents CheckBoxEnabled As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBoxCod As ComboBox
    Friend WithEvents TextBoxDsc As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxLastLogFch As TextBox
    Friend WithEvents PictureBoxLastLogResult As PictureBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_TaskLogs1 As Xl_TaskLogs
    Friend WithEvents Xl_TaskSchedules1 As Xl_TaskSchedules
    Friend WithEvents LabelTimeSpan As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxNextRunFch As TextBox
    Friend WithEvents TextBoxMsg As TextBox
    Friend WithEvents TextBoxResult As TextBox
End Class
