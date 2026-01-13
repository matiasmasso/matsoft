<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Trp_Tarifa
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxCalcM3 = New System.Windows.Forms.TextBox
        Me.ButtonCalc = New System.Windows.Forms.Button
        Me.ListBoxZonasSi = New System.Windows.Forms.ListBox
        Me.ListBoxZonasNo = New System.Windows.Forms.ListBox
        Me.LabelCalcCost = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxTrpNom = New System.Windows.Forms.TextBox
        Me.TextBoxCalcKg = New System.Windows.Forms.TextBox
        Me.TextBoxTrpZon = New System.Windows.Forms.TextBox
        Me.TabPageCosts = New System.Windows.Forms.TabPage
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ButtonRemoveZona = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPageZonas = New System.Windows.Forms.TabPage
        Me.ButtonAddZona = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextBoxCubicatje = New System.Windows.Forms.TextBox
        Me.CheckBoxHeredaCubicatje = New System.Windows.Forms.CheckBox
        Me.CheckBoxActivat = New System.Windows.Forms.CheckBox
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageCosts.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPageZonas.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 276)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "volum m3:"
        '
        'TextBoxCalcM3
        '
        Me.TextBoxCalcM3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCalcM3.Location = New System.Drawing.Point(79, 273)
        Me.TextBoxCalcM3.Name = "TextBoxCalcM3"
        Me.TextBoxCalcM3.Size = New System.Drawing.Size(64, 20)
        Me.TextBoxCalcM3.TabIndex = 6
        Me.TextBoxCalcM3.TabStop = False
        '
        'ButtonCalc
        '
        Me.ButtonCalc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonCalc.Enabled = False
        Me.ButtonCalc.Image = My.Resources.Resources.calcula
        Me.ButtonCalc.Location = New System.Drawing.Point(168, 266)
        Me.ButtonCalc.Name = "ButtonCalc"
        Me.ButtonCalc.Size = New System.Drawing.Size(100, 88)
        Me.ButtonCalc.TabIndex = 9
        Me.ButtonCalc.TabStop = False
        '
        'ListBoxZonasSi
        '
        Me.ListBoxZonasSi.FormattingEnabled = True
        Me.ListBoxZonasSi.Location = New System.Drawing.Point(254, 20)
        Me.ListBoxZonasSi.Name = "ListBoxZonasSi"
        Me.ListBoxZonasSi.Size = New System.Drawing.Size(179, 329)
        Me.ListBoxZonasSi.TabIndex = 18
        '
        'ListBoxZonasNo
        '
        Me.ListBoxZonasNo.FormattingEnabled = True
        Me.ListBoxZonasNo.Location = New System.Drawing.Point(7, 20)
        Me.ListBoxZonasNo.Name = "ListBoxZonasNo"
        Me.ListBoxZonasNo.Size = New System.Drawing.Size(179, 329)
        Me.ListBoxZonasNo.TabIndex = 15
        '
        'LabelCalcCost
        '
        Me.LabelCalcCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelCalcCost.BackColor = System.Drawing.SystemColors.Window
        Me.LabelCalcCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelCalcCost.Location = New System.Drawing.Point(284, 273)
        Me.LabelCalcCost.Name = "LabelCalcCost"
        Me.LabelCalcCost.Size = New System.Drawing.Size(138, 19)
        Me.LabelCalcCost.TabIndex = 10
        Me.LabelCalcCost.Text = "cost:"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 303)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "pes Kg:"
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Location = New System.Drawing.Point(-1, 1)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogo.TabIndex = 19
        Me.PictureBoxLogo.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(162, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "tarifa:"
        '
        'TextBoxTrpNom
        '
        Me.TextBoxTrpNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTrpNom.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxTrpNom.Location = New System.Drawing.Point(162, 1)
        Me.TextBoxTrpNom.Multiline = True
        Me.TextBoxTrpNom.Name = "TextBoxTrpNom"
        Me.TextBoxTrpNom.Size = New System.Drawing.Size(278, 23)
        Me.TextBoxTrpNom.TabIndex = 15
        Me.TextBoxTrpNom.TabStop = False
        '
        'TextBoxCalcKg
        '
        Me.TextBoxCalcKg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCalcKg.Location = New System.Drawing.Point(79, 300)
        Me.TextBoxCalcKg.Name = "TextBoxCalcKg"
        Me.TextBoxCalcKg.Size = New System.Drawing.Size(64, 20)
        Me.TextBoxCalcKg.TabIndex = 8
        Me.TextBoxCalcKg.TabStop = False
        '
        'TextBoxTrpZon
        '
        Me.TextBoxTrpZon.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTrpZon.Location = New System.Drawing.Point(217, 29)
        Me.TextBoxTrpZon.Name = "TextBoxTrpZon"
        Me.TextBoxTrpZon.Size = New System.Drawing.Size(223, 20)
        Me.TextBoxTrpZon.TabIndex = 17
        '
        'TabPageCosts
        '
        Me.TabPageCosts.Controls.Add(Me.DataGridView1)
        Me.TabPageCosts.Controls.Add(Me.LabelCalcCost)
        Me.TabPageCosts.Controls.Add(Me.Label3)
        Me.TabPageCosts.Controls.Add(Me.TextBoxCalcKg)
        Me.TabPageCosts.Controls.Add(Me.Label2)
        Me.TabPageCosts.Controls.Add(Me.TextBoxCalcM3)
        Me.TabPageCosts.Controls.Add(Me.ButtonCalc)
        Me.TabPageCosts.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCosts.Name = "TabPageCosts"
        Me.TabPageCosts.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCosts.Size = New System.Drawing.Size(438, 360)
        Me.TabPageCosts.TabIndex = 1
        Me.TabPageCosts.Text = "COST"
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(3, 1)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(432, 259)
        Me.DataGridView1.TabIndex = 11
        '
        'ButtonRemoveZona
        '
        Me.ButtonRemoveZona.Image = My.Resources.Resources.UNDO
        Me.ButtonRemoveZona.Location = New System.Drawing.Point(186, 91)
        Me.ButtonRemoveZona.Name = "ButtonRemoveZona"
        Me.ButtonRemoveZona.Size = New System.Drawing.Size(68, 62)
        Me.ButtonRemoveZona.TabIndex = 17
        Me.ButtonRemoveZona.Text = "retirar"
        Me.ButtonRemoveZona.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageCosts)
        Me.TabControl1.Controls.Add(Me.TabPageZonas)
        Me.TabControl1.Location = New System.Drawing.Point(-2, 106)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(446, 386)
        Me.TabControl1.TabIndex = 18
        Me.TabControl1.TabStop = False
        '
        'TabPageZonas
        '
        Me.TabPageZonas.Controls.Add(Me.ButtonRemoveZona)
        Me.TabPageZonas.Controls.Add(Me.ButtonAddZona)
        Me.TabPageZonas.Controls.Add(Me.ListBoxZonasSi)
        Me.TabPageZonas.Controls.Add(Me.ListBoxZonasNo)
        Me.TabPageZonas.Location = New System.Drawing.Point(4, 22)
        Me.TabPageZonas.Name = "TabPageZonas"
        Me.TabPageZonas.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageZonas.Size = New System.Drawing.Size(438, 360)
        Me.TabPageZonas.TabIndex = 0
        Me.TabPageZonas.Text = "ZONAS"
        '
        'ButtonAddZona
        '
        Me.ButtonAddZona.Image = My.Resources.Resources.REDO
        Me.ButtonAddZona.Location = New System.Drawing.Point(186, 22)
        Me.ButtonAddZona.Name = "ButtonAddZona"
        Me.ButtonAddZona.Size = New System.Drawing.Size(68, 62)
        Me.ButtonAddZona.TabIndex = 16
        Me.ButtonAddZona.Text = "afegir"
        Me.ButtonAddZona.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 494)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(452, 31)
        Me.Panel1.TabIndex = 34
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(233, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(344, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 3
        Me.ButtonOk.Text = "ACCEPTAR"
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
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(162, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "cubicatje:"
        '
        'TextBoxCubicatje
        '
        Me.TextBoxCubicatje.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCubicatje.Location = New System.Drawing.Point(217, 56)
        Me.TextBoxCubicatje.Name = "TextBoxCubicatje"
        Me.TextBoxCubicatje.ReadOnly = True
        Me.TextBoxCubicatje.Size = New System.Drawing.Size(53, 20)
        Me.TextBoxCubicatje.TabIndex = 36
        '
        'CheckBoxHeredaCubicatje
        '
        Me.CheckBoxHeredaCubicatje.AutoSize = True
        Me.CheckBoxHeredaCubicatje.Checked = True
        Me.CheckBoxHeredaCubicatje.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxHeredaCubicatje.Location = New System.Drawing.Point(277, 59)
        Me.CheckBoxHeredaCubicatje.Name = "CheckBoxHeredaCubicatje"
        Me.CheckBoxHeredaCubicatje.Size = New System.Drawing.Size(136, 17)
        Me.CheckBoxHeredaCubicatje.TabIndex = 37
        Me.CheckBoxHeredaCubicatje.Text = "hereda del transportista"
        Me.CheckBoxHeredaCubicatje.UseVisualStyleBackColor = True
        '
        'CheckBoxActivat
        '
        Me.CheckBoxActivat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxActivat.AutoSize = True
        Me.CheckBoxActivat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxActivat.Checked = True
        Me.CheckBoxActivat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxActivat.Location = New System.Drawing.Point(382, 95)
        Me.CheckBoxActivat.Name = "CheckBoxActivat"
        Me.CheckBoxActivat.Size = New System.Drawing.Size(58, 17)
        Me.CheckBoxActivat.TabIndex = 38
        Me.CheckBoxActivat.Text = "activat"
        Me.CheckBoxActivat.UseVisualStyleBackColor = True
        '
        'Frm_Trp_Tarifa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 525)
        Me.Controls.Add(Me.CheckBoxActivat)
        Me.Controls.Add(Me.CheckBoxHeredaCubicatje)
        Me.Controls.Add(Me.TextBoxCubicatje)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxTrpNom)
        Me.Controls.Add(Me.TextBoxTrpZon)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Trp_Tarifa"
        Me.Text = "TARIFA TRANSPORT"
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageCosts.ResumeLayout(False)
        Me.TabPageCosts.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageZonas.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCalcM3 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCalc As System.Windows.Forms.Button
    Friend WithEvents ListBoxZonasSi As System.Windows.Forms.ListBox
    Friend WithEvents ListBoxZonasNo As System.Windows.Forms.ListBox
    Friend WithEvents LabelCalcCost As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTrpNom As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxCalcKg As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxTrpZon As System.Windows.Forms.TextBox
    Friend WithEvents TabPageCosts As System.Windows.Forms.TabPage
    Friend WithEvents ButtonRemoveZona As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageZonas As System.Windows.Forms.TabPage
    Friend WithEvents ButtonAddZona As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCubicatje As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxHeredaCubicatje As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxActivat As System.Windows.Forms.CheckBox
End Class
