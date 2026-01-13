<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_ConsumerTicketReturn
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
        Me.LabelIvaTipus = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Xl_DocFile1 = New Xl_DocFile()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ImportarPdfToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.TextBoxOriginal = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_TextBoxNumBaseImponible = New Xl_TextBoxNum()
        Me.Xl_TextBoxNumIva = New Xl_TextBoxNum()
        Me.Xl_TextBoxNumReembolso = New Xl_TextBoxNum()
        Me.Xl_TextBoxNumComision = New Xl_TextBoxNum()
        Me.MenuStrip1.SuspendLayout()
        Me.PanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelIvaTipus
        '
        Me.LabelIvaTipus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelIvaTipus.AutoSize = True
        Me.LabelIvaTipus.Location = New System.Drawing.Point(15, 304)
        Me.LabelIvaTipus.Name = "LabelIvaTipus"
        Me.LabelIvaTipus.Size = New System.Drawing.Size(62, 13)
        Me.LabelIvaTipus.TabIndex = 78
        Me.LabelIvaTipus.Text = "IVA 21,00%"
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 278)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 13)
        Me.Label10.TabIndex = 76
        Me.Label10.Text = "Base imponible"
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.IsInedit = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(351, 27)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 75
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 330)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 73
        Me.Label9.Text = "Reembolso"
        '
        'ImportarPdfToolStripMenuItem
        '
        Me.ImportarPdfToolStripMenuItem.Name = "ImportarPdfToolStripMenuItem"
        Me.ImportarPdfToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.ImportarPdfToolStripMenuItem.Text = "Importar Pdf"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportarPdfToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(705, 24)
        Me.MenuStrip1.TabIndex = 72
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(486, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(597, 4)
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
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 451)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(705, 31)
        Me.PanelButtons.TabIndex = 68
        '
        'TextBoxOriginal
        '
        Me.TextBoxOriginal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxOriginal.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxOriginal.Location = New System.Drawing.Point(12, 27)
        Me.TextBoxOriginal.Multiline = True
        Me.TextBoxOriginal.Name = "TextBoxOriginal"
        Me.TextBoxOriginal.Size = New System.Drawing.Size(316, 177)
        Me.TextBoxOriginal.TabIndex = 65
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 252)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "Data devolució"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(178, 249)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePicker1.TabIndex = 54
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 376)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 80
        Me.Label2.Text = "Comisió a retrocedir"
        '
        'Xl_TextBoxNumBaseImponible
        '
        Me.Xl_TextBoxNumBaseImponible.Location = New System.Drawing.Point(178, 275)
        Me.Xl_TextBoxNumBaseImponible.Mat_FormatString = ""
        Me.Xl_TextBoxNumBaseImponible.Name = "Xl_TextBoxNumBaseImponible"
        Me.Xl_TextBoxNumBaseImponible.ReadOnly = False
        Me.Xl_TextBoxNumBaseImponible.Size = New System.Drawing.Size(99, 20)
        Me.Xl_TextBoxNumBaseImponible.TabIndex = 81
        Me.Xl_TextBoxNumBaseImponible.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_TextBoxNumIva
        '
        Me.Xl_TextBoxNumIva.Location = New System.Drawing.Point(178, 301)
        Me.Xl_TextBoxNumIva.Mat_FormatString = ""
        Me.Xl_TextBoxNumIva.Name = "Xl_TextBoxNumIva"
        Me.Xl_TextBoxNumIva.ReadOnly = False
        Me.Xl_TextBoxNumIva.Size = New System.Drawing.Size(99, 20)
        Me.Xl_TextBoxNumIva.TabIndex = 82
        Me.Xl_TextBoxNumIva.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_TextBoxNumReembolso
        '
        Me.Xl_TextBoxNumReembolso.Location = New System.Drawing.Point(178, 327)
        Me.Xl_TextBoxNumReembolso.Mat_FormatString = ""
        Me.Xl_TextBoxNumReembolso.Name = "Xl_TextBoxNumReembolso"
        Me.Xl_TextBoxNumReembolso.ReadOnly = False
        Me.Xl_TextBoxNumReembolso.Size = New System.Drawing.Size(99, 20)
        Me.Xl_TextBoxNumReembolso.TabIndex = 83
        Me.Xl_TextBoxNumReembolso.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_TextBoxNumComision
        '
        Me.Xl_TextBoxNumComision.Location = New System.Drawing.Point(178, 369)
        Me.Xl_TextBoxNumComision.Mat_FormatString = ""
        Me.Xl_TextBoxNumComision.Name = "Xl_TextBoxNumComision"
        Me.Xl_TextBoxNumComision.ReadOnly = False
        Me.Xl_TextBoxNumComision.Size = New System.Drawing.Size(99, 20)
        Me.Xl_TextBoxNumComision.TabIndex = 84
        Me.Xl_TextBoxNumComision.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Frm_ConsumerTicketReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 482)
        Me.Controls.Add(Me.Xl_TextBoxNumComision)
        Me.Controls.Add(Me.Xl_TextBoxNumReembolso)
        Me.Controls.Add(Me.Xl_TextBoxNumIva)
        Me.Controls.Add(Me.Xl_TextBoxNumBaseImponible)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LabelIvaTipus)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.TextBoxOriginal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Name = "Frm_ConsumerTicketReturn"
        Me.Text = "Devolució ticket de consumidor"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.PanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelIvaTipus As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Xl_DocFile1 As Xl_DocFile
    Friend WithEvents Label9 As Label
    Friend WithEvents ImportarPdfToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents TextBoxOriginal As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_TextBoxNumBaseImponible As Xl_TextBoxNum
    Friend WithEvents Xl_TextBoxNumIva As Xl_TextBoxNum
    Friend WithEvents Xl_TextBoxNumReembolso As Xl_TextBoxNum
    Friend WithEvents Xl_TextBoxNumComision As Xl_TextBoxNum
End Class
