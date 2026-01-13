<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Intrastat
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TextBoxEurs = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxKgs = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxUnits = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxPartides = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxCsv = New System.Windows.Forms.TextBox()
        Me.NumericUpDownOrd = New System.Windows.Forms.NumericUpDown()
        Me.ComboBoxFlujo = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_YearMonth1 = New Xl_YearMonth()
        Me.Xl_DocFile1 = New Xl_DocFile_Old()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_IntrastatPartidas1 = New Xl_IntrastatPartidas()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesarFitxerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownOrd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_IntrastatPartidas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(1, 34)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(669, 488)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxEurs)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.TextBoxKgs)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxUnits)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxPartides)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.TextBoxCsv)
        Me.TabPage1.Controls.Add(Me.NumericUpDownOrd)
        Me.TabPage1.Controls.Add(Me.ComboBoxFlujo)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Xl_YearMonth1)
        Me.TabPage1.Controls.Add(Me.Xl_DocFile1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage1.Size = New System.Drawing.Size(661, 462)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TextBoxEurs
        '
        Me.TextBoxEurs.Location = New System.Drawing.Point(60, 227)
        Me.TextBoxEurs.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TextBoxEurs.Name = "TextBoxEurs"
        Me.TextBoxEurs.ReadOnly = True
        Me.TextBoxEurs.Size = New System.Drawing.Size(157, 20)
        Me.TextBoxEurs.TabIndex = 50
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 228)
        Me.Label8.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "Import:"
        '
        'TextBoxKgs
        '
        Me.TextBoxKgs.Location = New System.Drawing.Point(60, 204)
        Me.TextBoxKgs.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TextBoxKgs.Name = "TextBoxKgs"
        Me.TextBoxKgs.ReadOnly = True
        Me.TextBoxKgs.Size = New System.Drawing.Size(157, 20)
        Me.TextBoxKgs.TabIndex = 48
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 205)
        Me.Label7.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(28, 13)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Pes:"
        '
        'TextBoxUnits
        '
        Me.TextBoxUnits.Location = New System.Drawing.Point(60, 179)
        Me.TextBoxUnits.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TextBoxUnits.Name = "TextBoxUnits"
        Me.TextBoxUnits.ReadOnly = True
        Me.TextBoxUnits.Size = New System.Drawing.Size(157, 20)
        Me.TextBoxUnits.TabIndex = 46
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 180)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 13)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "Unitats:"
        '
        'TextBoxPartides
        '
        Me.TextBoxPartides.Location = New System.Drawing.Point(60, 155)
        Me.TextBoxPartides.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TextBoxPartides.Name = "TextBoxPartides"
        Me.TextBoxPartides.ReadOnly = True
        Me.TextBoxPartides.Size = New System.Drawing.Size(157, 20)
        Me.TextBoxPartides.TabIndex = 44
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 156)
        Me.Label5.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Partides:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(1, 430)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(659, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(440, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(551, 4)
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
        'TextBoxCsv
        '
        Me.TextBoxCsv.Location = New System.Drawing.Point(60, 130)
        Me.TextBoxCsv.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TextBoxCsv.Name = "TextBoxCsv"
        Me.TextBoxCsv.Size = New System.Drawing.Size(157, 20)
        Me.TextBoxCsv.TabIndex = 8
        '
        'NumericUpDownOrd
        '
        Me.NumericUpDownOrd.Location = New System.Drawing.Point(60, 104)
        Me.NumericUpDownOrd.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.NumericUpDownOrd.Name = "NumericUpDownOrd"
        Me.NumericUpDownOrd.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownOrd.TabIndex = 7
        '
        'ComboBoxFlujo
        '
        Me.ComboBoxFlujo.Enabled = False
        Me.ComboBoxFlujo.FormattingEnabled = True
        Me.ComboBoxFlujo.Items.AddRange(New Object() {"Introducció", "Expedició"})
        Me.ComboBoxFlujo.Location = New System.Drawing.Point(60, 76)
        Me.ComboBoxFlujo.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.ComboBoxFlujo.Name = "ComboBoxFlujo"
        Me.ComboBoxFlujo.Size = New System.Drawing.Size(157, 21)
        Me.ComboBoxFlujo.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 132)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Csv:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 104)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Ordre:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 78)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Fluxe:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Data:"
        '
        'Xl_YearMonth1
        '
        Me.Xl_YearMonth1.Location = New System.Drawing.Point(64, 23)
        Me.Xl_YearMonth1.Name = "Xl_YearMonth1"
        Me.Xl_YearMonth1.Size = New System.Drawing.Size(150, 19)
        Me.Xl_YearMonth1.TabIndex = 1
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(309, 4)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_IntrastatPartidas1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage2.Size = New System.Drawing.Size(661, 462)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Partides"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_IntrastatPartidas1
        '
        Me.Xl_IntrastatPartidas1.AllowUserToAddRows = False
        Me.Xl_IntrastatPartidas1.AllowUserToDeleteRows = False
        Me.Xl_IntrastatPartidas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_IntrastatPartidas1.DisplayObsolets = False
        Me.Xl_IntrastatPartidas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_IntrastatPartidas1.Filter = Nothing
        Me.Xl_IntrastatPartidas1.Location = New System.Drawing.Point(1, 1)
        Me.Xl_IntrastatPartidas1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Xl_IntrastatPartidas1.MouseIsDown = False
        Me.Xl_IntrastatPartidas1.Name = "Xl_IntrastatPartidas1"
        Me.Xl_IntrastatPartidas1.ReadOnly = True
        Me.Xl_IntrastatPartidas1.RowTemplate.Height = 40
        Me.Xl_IntrastatPartidas1.Size = New System.Drawing.Size(659, 460)
        Me.Xl_IntrastatPartidas1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(2, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(670, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DesarFitxerToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 22)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'DesarFitxerToolStripMenuItem
        '
        Me.DesarFitxerToolStripMenuItem.Name = "DesarFitxerToolStripMenuItem"
        Me.DesarFitxerToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.DesarFitxerToolStripMenuItem.Text = "Desar fitxer"
        '
        'Frm_Intrastat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(670, 522)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Name = "Frm_Intrastat"
        Me.Text = "Intrastat"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownOrd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_IntrastatPartidas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Xl_DocFile1 As Xl_DocFile_Old
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TextBoxCsv As TextBox
    Friend WithEvents NumericUpDownOrd As NumericUpDown
    Friend WithEvents ComboBoxFlujo As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_YearMonth1 As Xl_YearMonth
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_IntrastatPartidas1 As Xl_IntrastatPartidas
    Friend WithEvents TextBoxEurs As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxKgs As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxUnits As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxPartides As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DesarFitxerToolStripMenuItem As ToolStripMenuItem
End Class
