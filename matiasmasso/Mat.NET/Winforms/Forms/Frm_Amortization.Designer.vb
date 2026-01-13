<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Amortization
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_LookupPgcCta1 = New Winforms.Xl_LookupPgcCta()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_AmountCur1 = New Winforms.Xl_AmountCur()
        Me.Xl_LookupCcaAlta = New Winforms.Xl_LookupCca()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_LookupCcaBaixa = New Winforms.Xl_LookupCca()
        Me.CheckBoxBaixa = New System.Windows.Forms.CheckBox()
        Me.Xl_Percent1 = New Winforms.Xl_Percent()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_AmortizationItems1 = New Winforms.Xl_AmortizationItems()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_AmortizationItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nom"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(73, 44)
        Me.TextBoxNom.MaxLength = 60
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(334, 20)
        Me.TextBoxNom.TabIndex = 1
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(73, 71)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(103, 20)
        Me.DateTimePicker1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Adquisició"
        '
        'Xl_LookupPgcCta1
        '
        Me.Xl_LookupPgcCta1.IsDirty = False
        Me.Xl_LookupPgcCta1.Location = New System.Drawing.Point(73, 98)
        Me.Xl_LookupPgcCta1.Name = "Xl_LookupPgcCta1"
        Me.Xl_LookupPgcCta1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPgcCta1.PgcCta = Nothing
        Me.Xl_LookupPgcCta1.Size = New System.Drawing.Size(334, 20)
        Me.Xl_LookupPgcCta1.TabIndex = 4
        Me.Xl_LookupPgcCta1.Value = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Compte"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Import"
        '
        'Xl_AmountCur1
        '
        Me.Xl_AmountCur1.Amt = Nothing
        Me.Xl_AmountCur1.Location = New System.Drawing.Point(73, 124)
        Me.Xl_AmountCur1.Name = "Xl_AmountCur1"
        Me.Xl_AmountCur1.Size = New System.Drawing.Size(103, 20)
        Me.Xl_AmountCur1.TabIndex = 7
        '
        'Xl_LookupCcaAlta
        '
        Me.Xl_LookupCcaAlta.Cca = Nothing
        Me.Xl_LookupCcaAlta.IsDirty = False
        Me.Xl_LookupCcaAlta.Location = New System.Drawing.Point(73, 204)
        Me.Xl_LookupCcaAlta.Name = "Xl_LookupCcaAlta"
        Me.Xl_LookupCcaAlta.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCcaAlta.Size = New System.Drawing.Size(334, 20)
        Me.Xl_LookupCcaAlta.TabIndex = 8
        Me.Xl_LookupCcaAlta.Value = Nothing
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 207)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(25, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Alta"
        '
        'Xl_LookupCcaBaixa
        '
        Me.Xl_LookupCcaBaixa.Cca = Nothing
        Me.Xl_LookupCcaBaixa.IsDirty = False
        Me.Xl_LookupCcaBaixa.Location = New System.Drawing.Point(73, 230)
        Me.Xl_LookupCcaBaixa.Name = "Xl_LookupCcaBaixa"
        Me.Xl_LookupCcaBaixa.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCcaBaixa.Size = New System.Drawing.Size(334, 20)
        Me.Xl_LookupCcaBaixa.TabIndex = 10
        Me.Xl_LookupCcaBaixa.Value = Nothing
        Me.Xl_LookupCcaBaixa.Visible = False
        '
        'CheckBoxBaixa
        '
        Me.CheckBoxBaixa.AutoSize = True
        Me.CheckBoxBaixa.Location = New System.Drawing.Point(13, 232)
        Me.CheckBoxBaixa.Name = "CheckBoxBaixa"
        Me.CheckBoxBaixa.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxBaixa.TabIndex = 11
        Me.CheckBoxBaixa.Text = "Baixa"
        Me.CheckBoxBaixa.UseVisualStyleBackColor = True
        '
        'Xl_Percent1
        '
        Me.Xl_Percent1.Location = New System.Drawing.Point(73, 151)
        Me.Xl_Percent1.Name = "Xl_Percent1"
        Me.Xl_Percent1.Size = New System.Drawing.Size(74, 20)
        Me.Xl_Percent1.TabIndex = 12
        Me.Xl_Percent1.Text = "0,00 %"
        Me.Xl_Percent1.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 154)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Tipus"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(153, 154)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(179, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "(percentatge a amortitzar anualment)"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 545)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(419, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(200, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(311, 4)
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
        'Xl_AmortizationItems1
        '
        Me.Xl_AmortizationItems1.AllowUserToAddRows = False
        Me.Xl_AmortizationItems1.AllowUserToDeleteRows = False
        Me.Xl_AmortizationItems1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AmortizationItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_AmortizationItems1.DisplayObsolets = False
        Me.Xl_AmortizationItems1.Location = New System.Drawing.Point(0, 277)
        Me.Xl_AmortizationItems1.Name = "Xl_AmortizationItems1"
        Me.Xl_AmortizationItems1.ReadOnly = True
        Me.Xl_AmortizationItems1.Size = New System.Drawing.Size(419, 266)
        Me.Xl_AmortizationItems1.TabIndex = 43
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(419, 24)
        Me.MenuStrip1.TabIndex = 44
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'Frm_Amortization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(419, 576)
        Me.Controls.Add(Me.Xl_AmortizationItems1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_Percent1)
        Me.Controls.Add(Me.CheckBoxBaixa)
        Me.Controls.Add(Me.Xl_LookupCcaBaixa)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_LookupCcaAlta)
        Me.Controls.Add(Me.Xl_AmountCur1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_LookupPgcCta1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Amortization"
        Me.Text = "Amortització Inmobilitzat"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_AmortizationItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_LookupPgcCta1 As Xl_LookupPgcCta
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_AmountCur1 As Xl_AmountCur
    Friend WithEvents Xl_LookupCcaAlta As Xl_LookupCca
    Friend WithEvents Label5 As Label
    Friend WithEvents Xl_LookupCcaBaixa As Xl_LookupCca
    Friend WithEvents CheckBoxBaixa As CheckBox
    Friend WithEvents Xl_Percent1 As Xl_Percent
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_AmortizationItems1 As Xl_AmortizationItems
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
End Class
