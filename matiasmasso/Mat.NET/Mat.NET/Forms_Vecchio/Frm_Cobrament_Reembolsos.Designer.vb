Public Partial Class Frm_Cobrament_Reembolsos
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPagePndSel = New System.Windows.Forms.TabPage
        Me.Xl_AlbsReembols1 = New Xl_AlbsReembols
        Me.TabPageFpg = New System.Windows.Forms.TabPage
        Me.Xl_Cobrament1 = New Xl_Cobrament
        Me.TabPageEnd = New System.Windows.Forms.TabPage
        Me.TextBoxEnd = New System.Windows.Forms.TextBox
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox
        Me.ButtonPrevious = New System.Windows.Forms.Button
        Me.ButtonEnd = New System.Windows.Forms.Button
        Me.ButtonNext = New System.Windows.Forms.Button
        Me.ComboBoxTrp = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
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
        Me.TabControl1.Location = New System.Drawing.Point(12, 36)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(676, 354)
        Me.TabControl1.TabIndex = 39
        '
        'TabPagePndSel
        '
        Me.TabPagePndSel.Controls.Add(Me.Xl_AlbsReembols1)
        Me.TabPagePndSel.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePndSel.Name = "TabPagePndSel"
        Me.TabPagePndSel.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePndSel.Size = New System.Drawing.Size(668, 328)
        Me.TabPagePndSel.TabIndex = 0
        Me.TabPagePndSel.Text = "SELECCIO"
        '
        'Xl_AlbsReembols1
        '
        Me.Xl_AlbsReembols1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AlbsReembols1.Location = New System.Drawing.Point(7, 7)
        Me.Xl_AlbsReembols1.Name = "Xl_AlbsReembols1"
        Me.Xl_AlbsReembols1.Size = New System.Drawing.Size(658, 315)
        Me.Xl_AlbsReembols1.TabIndex = 0
        '
        'TabPageFpg
        '
        Me.TabPageFpg.Controls.Add(Me.Xl_Cobrament1)
        Me.TabPageFpg.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFpg.Name = "TabPageFpg"
        Me.TabPageFpg.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageFpg.Size = New System.Drawing.Size(668, 328)
        Me.TabPageFpg.TabIndex = 1
        Me.TabPageFpg.Text = "MODALITAT"
        '
        'Xl_Cobrament1
        '
        Me.Xl_Cobrament1.Location = New System.Drawing.Point(131, 70)
        Me.Xl_Cobrament1.Name = "Xl_Cobrament1"
        Me.Xl_Cobrament1.Size = New System.Drawing.Size(400, 200)
        Me.Xl_Cobrament1.TabIndex = 0
        '
        'TabPageEnd
        '
        Me.TabPageEnd.Controls.Add(Me.TextBoxEnd)
        Me.TabPageEnd.Location = New System.Drawing.Point(4, 22)
        Me.TabPageEnd.Name = "TabPageEnd"
        Me.TabPageEnd.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEnd.Size = New System.Drawing.Size(668, 328)
        Me.TabPageEnd.TabIndex = 2
        Me.TabPageEnd.Text = "FINAL"
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
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(12, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(91, 20)
        Me.DateTimePicker1.TabIndex = 45
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLogo.Location = New System.Drawing.Point(538, 3)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogo.TabIndex = 43
        Me.PictureBoxLogo.TabStop = False
        '
        'ButtonPrevious
        '
        Me.ButtonPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrevious.Enabled = False
        Me.ButtonPrevious.Location = New System.Drawing.Point(9, 395)
        Me.ButtonPrevious.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(96, 24)
        Me.ButtonPrevious.TabIndex = 40
        Me.ButtonPrevious.Text = "< ENRERA"
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.Enabled = False
        Me.ButtonEnd.Location = New System.Drawing.Point(593, 395)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(96, 24)
        Me.ButtonEnd.TabIndex = 41
        Me.ButtonEnd.Text = "FI >>"
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.Enabled = False
        Me.ButtonNext.Location = New System.Drawing.Point(489, 395)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(96, 24)
        Me.ButtonNext.TabIndex = 42
        Me.ButtonNext.Text = "SEGÜENT >"
        '
        'ComboBoxTrp
        '
        Me.ComboBoxTrp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxTrp.FormattingEnabled = True
        Me.ComboBoxTrp.Location = New System.Drawing.Point(330, 3)
        Me.ComboBoxTrp.Name = "ComboBoxTrp"
        Me.ComboBoxTrp.Size = New System.Drawing.Size(201, 21)
        Me.ComboBoxTrp.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(253, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "transportista:"
        '
        'Frm_Cobrament_Reembolsos
        '
        Me.ClientSize = New System.Drawing.Size(705, 421)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxTrp)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.ButtonPrevious)
        Me.Controls.Add(Me.ButtonEnd)
        Me.Controls.Add(Me.ButtonNext)
        Me.Name = "Frm_Cobrament_Reembolsos"
        Me.Text = "COBRAMENT REEMBOLSOS"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPagePndSel.ResumeLayout(False)
        Me.TabPageFpg.ResumeLayout(False)
        Me.TabPageEnd.ResumeLayout(False)
        Me.TabPageEnd.PerformLayout()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPagePndSel As System.Windows.Forms.TabPage
    Friend WithEvents TabPageFpg As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Cobrament1 As Xl_Cobrament
    Friend WithEvents TabPageEnd As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxEnd As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents PictureBoxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonPrevious As System.Windows.Forms.Button
    Friend WithEvents ButtonEnd As System.Windows.Forms.Button
    Friend WithEvents ButtonNext As System.Windows.Forms.Button
    Friend WithEvents ComboBoxTrp As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_AlbsReembols1 As Xl_AlbsReembols
End Class
