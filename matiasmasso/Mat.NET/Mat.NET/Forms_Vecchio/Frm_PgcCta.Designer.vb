<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PgcCta
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPageGral = New System.Windows.Forms.TabPage
        Me.Label9 = New System.Windows.Forms.Label
        Me.Xl_Cta_PGC_next = New Xl_Cta
        Me.Label8 = New System.Windows.Forms.Label
        Me.Xl_Cta_PGC_previous = New Xl_Cta
        Me.Label5 = New System.Windows.Forms.Label
        Me.ComboBoxSigne = New System.Windows.Forms.ComboBox
        Me.TextBoxGrup4 = New System.Windows.Forms.TextBox
        Me.TextBoxGrup3 = New System.Windows.Forms.TextBox
        Me.TextBoxGrup2 = New System.Windows.Forms.TextBox
        Me.TextBoxGrup1 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox
        Me.TextBoxPgcPlan = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBoxDsc = New System.Windows.Forms.TextBox
        Me.TextBoxEng = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TextBoxCat = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TextBoxEsp = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxId = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPageCcds = New System.Windows.Forms.TabPage
        Me.CheckBoxOcultarComptesSaldats = New System.Windows.Forms.CheckBox
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton
        Me.DataGridViewCcds = New System.Windows.Forms.DataGridView
        Me.TabPageDetall = New System.Windows.Forms.TabPage
        Me.DataGridViewSpecial = New System.Windows.Forms.DataGridView
        Me.ComboBoxCcdYeas = New System.Windows.Forms.ComboBox
        Me.CheckBoxIsBaseImponibleIVA = New System.Windows.Forms.CheckBox
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        Me.TabPageCcds.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridViewCcds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageDetall.SuspendLayout()
        CType(Me.DataGridViewSpecial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 509)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(573, 31)
        Me.Panel1.TabIndex = 66
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(354, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 13
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(465, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 12
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
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageCcds)
        Me.TabControl1.Controls.Add(Me.TabPageDetall)
        Me.TabControl1.Location = New System.Drawing.Point(6, 23)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(563, 480)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.CheckBoxIsBaseImponibleIVA)
        Me.TabPageGral.Controls.Add(Me.Label9)
        Me.TabPageGral.Controls.Add(Me.Xl_Cta_PGC_next)
        Me.TabPageGral.Controls.Add(Me.Label8)
        Me.TabPageGral.Controls.Add(Me.Xl_Cta_PGC_previous)
        Me.TabPageGral.Controls.Add(Me.Label5)
        Me.TabPageGral.Controls.Add(Me.ComboBoxSigne)
        Me.TabPageGral.Controls.Add(Me.TextBoxGrup4)
        Me.TabPageGral.Controls.Add(Me.TextBoxGrup3)
        Me.TabPageGral.Controls.Add(Me.TextBoxGrup2)
        Me.TabPageGral.Controls.Add(Me.TextBoxGrup1)
        Me.TabPageGral.Controls.Add(Me.Label4)
        Me.TabPageGral.Controls.Add(Me.ComboBoxCod)
        Me.TabPageGral.Controls.Add(Me.TextBoxPgcPlan)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.TextBoxDsc)
        Me.TabPageGral.Controls.Add(Me.TextBoxEng)
        Me.TabPageGral.Controls.Add(Me.Label7)
        Me.TabPageGral.Controls.Add(Me.TextBoxCat)
        Me.TabPageGral.Controls.Add(Me.Label6)
        Me.TabPageGral.Controls.Add(Me.TextBoxEsp)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Controls.Add(Me.TextBoxId)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGral.Size = New System.Drawing.Size(555, 454)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "GENERAL"
        Me.TabPageGral.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(188, 426)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(175, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "correspondencia amb PGC següent"
        '
        'Xl_Cta_PGC_next
        '
        Me.Xl_Cta_PGC_next.Cta = Nothing
        Me.Xl_Cta_PGC_next.Location = New System.Drawing.Point(383, 421)
        Me.Xl_Cta_PGC_next.Name = "Xl_Cta_PGC_next"
        Me.Xl_Cta_PGC_next.Size = New System.Drawing.Size(136, 20)
        Me.Xl_Cta_PGC_next.TabIndex = 22
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(188, 402)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(172, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "correspondencia amb PGC anterior"
        '
        'Xl_Cta_PGC_previous
        '
        Me.Xl_Cta_PGC_previous.Cta = Nothing
        Me.Xl_Cta_PGC_previous.Location = New System.Drawing.Point(383, 395)
        Me.Xl_Cta_PGC_previous.Name = "Xl_Cta_PGC_previous"
        Me.Xl_Cta_PGC_previous.Size = New System.Drawing.Size(136, 20)
        Me.Xl_Cta_PGC_previous.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(194, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "signe"
        '
        'ComboBoxSigne
        '
        Me.ComboBoxSigne.FormattingEnabled = True
        Me.ComboBoxSigne.Items.AddRange(New Object() {"(seleccionar)", "deutor", "creditor"})
        Me.ComboBoxSigne.Location = New System.Drawing.Point(197, 167)
        Me.ComboBoxSigne.Name = "ComboBoxSigne"
        Me.ComboBoxSigne.Size = New System.Drawing.Size(132, 21)
        Me.ComboBoxSigne.TabIndex = 18
        '
        'TextBoxGrup4
        '
        Me.TextBoxGrup4.Location = New System.Drawing.Point(107, 110)
        Me.TextBoxGrup4.Name = "TextBoxGrup4"
        Me.TextBoxGrup4.ReadOnly = True
        Me.TextBoxGrup4.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxGrup4.TabIndex = 17
        Me.TextBoxGrup4.TabStop = False
        '
        'TextBoxGrup3
        '
        Me.TextBoxGrup3.Location = New System.Drawing.Point(107, 84)
        Me.TextBoxGrup3.Name = "TextBoxGrup3"
        Me.TextBoxGrup3.ReadOnly = True
        Me.TextBoxGrup3.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxGrup3.TabIndex = 16
        Me.TextBoxGrup3.TabStop = False
        '
        'TextBoxGrup2
        '
        Me.TextBoxGrup2.Location = New System.Drawing.Point(107, 58)
        Me.TextBoxGrup2.Name = "TextBoxGrup2"
        Me.TextBoxGrup2.ReadOnly = True
        Me.TextBoxGrup2.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxGrup2.TabIndex = 15
        Me.TextBoxGrup2.TabStop = False
        '
        'TextBoxGrup1
        '
        Me.TextBoxGrup1.Location = New System.Drawing.Point(107, 32)
        Me.TextBoxGrup1.Name = "TextBoxGrup1"
        Me.TextBoxGrup1.ReadOnly = True
        Me.TextBoxGrup1.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxGrup1.TabIndex = 14
        Me.TextBoxGrup1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(332, 150)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Codi"
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Location = New System.Drawing.Point(335, 166)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(184, 21)
        Me.ComboBoxCod.TabIndex = 12
        '
        'TextBoxPgcPlan
        '
        Me.TextBoxPgcPlan.Location = New System.Drawing.Point(107, 6)
        Me.TextBoxPgcPlan.Name = "TextBoxPgcPlan"
        Me.TextBoxPgcPlan.ReadOnly = True
        Me.TextBoxPgcPlan.Size = New System.Drawing.Size(64, 20)
        Me.TextBoxPgcPlan.TabIndex = 1
        Me.TextBoxPgcPlan.TabStop = False
        Me.TextBoxPgcPlan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(36, 286)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "descripció"
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Location = New System.Drawing.Point(107, 286)
        Me.TextBoxDsc.Multiline = True
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(412, 76)
        Me.TextBoxDsc.TabIndex = 11
        '
        'TextBoxEng
        '
        Me.TextBoxEng.Location = New System.Drawing.Point(107, 245)
        Me.TextBoxEng.MaxLength = 20
        Me.TextBoxEng.Name = "TextBoxEng"
        Me.TextBoxEng.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxEng.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(36, 248)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Anglés"
        '
        'TextBoxCat
        '
        Me.TextBoxCat.Location = New System.Drawing.Point(107, 219)
        Me.TextBoxCat.MaxLength = 20
        Me.TextBoxCat.Name = "TextBoxCat"
        Me.TextBoxCat.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxCat.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(36, 222)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Catalá"
        '
        'TextBoxEsp
        '
        Me.TextBoxEsp.Location = New System.Drawing.Point(107, 193)
        Me.TextBoxEsp.MaxLength = 20
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxEsp.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 196)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Espanyol"
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(107, 167)
        Me.TextBoxId.MaxLength = 5
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.Size = New System.Drawing.Size(50, 20)
        Me.TextBoxId.TabIndex = 3
        Me.TextBoxId.TabStop = False
        Me.TextBoxId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 170)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Compte nº"
        '
        'TabPageCcds
        '
        Me.TabPageCcds.Controls.Add(Me.CheckBoxOcultarComptesSaldats)
        Me.TabPageCcds.Controls.Add(Me.ToolStrip1)
        Me.TabPageCcds.Controls.Add(Me.DataGridViewCcds)
        Me.TabPageCcds.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCcds.Name = "TabPageCcds"
        Me.TabPageCcds.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCcds.Size = New System.Drawing.Size(555, 454)
        Me.TabPageCcds.TabIndex = 1
        Me.TabPageCcds.Text = "SUBCOMPTES"
        Me.TabPageCcds.UseVisualStyleBackColor = True
        '
        'CheckBoxOcultarComptesSaldats
        '
        Me.CheckBoxOcultarComptesSaldats.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxOcultarComptesSaldats.AutoSize = True
        Me.CheckBoxOcultarComptesSaldats.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxOcultarComptesSaldats.Location = New System.Drawing.Point(410, 4)
        Me.CheckBoxOcultarComptesSaldats.Name = "CheckBoxOcultarComptesSaldats"
        Me.CheckBoxOcultarComptesSaldats.Size = New System.Drawing.Size(139, 17)
        Me.CheckBoxOcultarComptesSaldats.TabIndex = 3
        Me.CheckBoxOcultarComptesSaldats.Text = "Ocultar comptes saldats"
        Me.CheckBoxOcultarComptesSaldats.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonExcel})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(549, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "exportar a Excel"
        '
        'DataGridViewCcds
        '
        Me.DataGridViewCcds.AllowUserToAddRows = False
        Me.DataGridViewCcds.AllowUserToDeleteRows = False
        Me.DataGridViewCcds.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewCcds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCcds.Location = New System.Drawing.Point(3, 27)
        Me.DataGridViewCcds.Name = "DataGridViewCcds"
        Me.DataGridViewCcds.ReadOnly = True
        Me.DataGridViewCcds.RowHeadersVisible = False
        Me.DataGridViewCcds.Size = New System.Drawing.Size(549, 424)
        Me.DataGridViewCcds.TabIndex = 1
        '
        'TabPageDetall
        '
        Me.TabPageDetall.Controls.Add(Me.DataGridViewSpecial)
        Me.TabPageDetall.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDetall.Name = "TabPageDetall"
        Me.TabPageDetall.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDetall.Size = New System.Drawing.Size(555, 454)
        Me.TabPageDetall.TabIndex = 2
        Me.TabPageDetall.Text = "DETALL"
        Me.TabPageDetall.UseVisualStyleBackColor = True
        '
        'DataGridViewSpecial
        '
        Me.DataGridViewSpecial.AllowUserToAddRows = False
        Me.DataGridViewSpecial.AllowUserToDeleteRows = False
        Me.DataGridViewSpecial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSpecial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewSpecial.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewSpecial.Name = "DataGridViewSpecial"
        Me.DataGridViewSpecial.ReadOnly = True
        Me.DataGridViewSpecial.Size = New System.Drawing.Size(549, 448)
        Me.DataGridViewSpecial.TabIndex = 2
        '
        'ComboBoxCcdYeas
        '
        Me.ComboBoxCcdYeas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCcdYeas.FormattingEnabled = True
        Me.ComboBoxCcdYeas.Location = New System.Drawing.Point(489, 13)
        Me.ComboBoxCcdYeas.Name = "ComboBoxCcdYeas"
        Me.ComboBoxCcdYeas.Size = New System.Drawing.Size(76, 21)
        Me.ComboBoxCcdYeas.TabIndex = 0
        '
        'CheckBoxIsBaseImponibleIVA
        '
        Me.CheckBoxIsBaseImponibleIVA.AutoSize = True
        Me.CheckBoxIsBaseImponibleIVA.Location = New System.Drawing.Point(107, 369)
        Me.CheckBoxIsBaseImponibleIVA.Name = "CheckBoxIsBaseImponibleIVA"
        Me.CheckBoxIsBaseImponibleIVA.Size = New System.Drawing.Size(143, 17)
        Me.CheckBoxIsBaseImponibleIVA.TabIndex = 24
        Me.CheckBoxIsBaseImponibleIVA.Text = "es base imponible de Iva"
        Me.CheckBoxIsBaseImponibleIVA.UseVisualStyleBackColor = True
        '
        'Frm_PgcCta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 540)
        Me.Controls.Add(Me.ComboBoxCcdYeas)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_PgcCta"
        Me.Text = "COMPTE"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        Me.TabPageCcds.ResumeLayout(False)
        Me.TabPageCcds.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridViewCcds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageDetall.ResumeLayout(False)
        CType(Me.DataGridViewSpecial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDsc As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxEng As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCat As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEsp As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxId As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPgcPlan As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCod As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxGrup2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxGrup1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxGrup4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxGrup3 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxSigne As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Xl_Cta_PGC_next As Xl_Cta
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Xl_Cta_PGC_previous As Xl_Cta
    Friend WithEvents TabPageCcds As System.Windows.Forms.TabPage
    Friend WithEvents ComboBoxCcdYeas As System.Windows.Forms.ComboBox
    Friend WithEvents TabPageDetall As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewSpecial As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewCcds As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents CheckBoxOcultarComptesSaldats As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxIsBaseImponibleIVA As System.Windows.Forms.CheckBox
End Class
