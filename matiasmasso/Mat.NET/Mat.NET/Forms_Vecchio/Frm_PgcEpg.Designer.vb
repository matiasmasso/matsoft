<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PgcEpg
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxEsp = New System.Windows.Forms.TextBox()
        Me.TextBoxLevel = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ContextMenuStripCtas = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItemZoom = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemCtasAddNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemCtasRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxPlan = New System.Windows.Forms.ComboBox()
        Me.TextBoxCod = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ButtonEditCtas = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TabPageCtas = New System.Windows.Forms.TabPage()
        Me.TextBoxCat = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxEng = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStripCtas.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 482)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(559, 31)
        Me.Panel1.TabIndex = 67
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(340, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(451, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 1
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
        'TextBoxEsp
        '
        Me.TextBoxEsp.Location = New System.Drawing.Point(112, 45)
        Me.TextBoxEsp.MaxLength = 60
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.Size = New System.Drawing.Size(299, 20)
        Me.TextBoxEsp.TabIndex = 0
        '
        'TextBoxLevel
        '
        Me.TextBoxLevel.Location = New System.Drawing.Point(112, 9)
        Me.TextBoxLevel.Name = "TextBoxLevel"
        Me.TextBoxLevel.ReadOnly = True
        Me.TextBoxLevel.Size = New System.Drawing.Size(66, 20)
        Me.TextBoxLevel.TabIndex = 68
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 69
        Me.Label1.Text = "Nivell:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(53, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "Espanyol:"
        '
        'ContextMenuStripCtas
        '
        Me.ContextMenuStripCtas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemZoom, Me.ToolStripMenuItemCtasAddNew, Me.ToolStripMenuItemCtasRemove})
        Me.ContextMenuStripCtas.Name = "ContextMenuStripCtas"
        Me.ContextMenuStripCtas.Size = New System.Drawing.Size(113, 70)
        '
        'ToolStripMenuItemZoom
        '
        Me.ToolStripMenuItemZoom.Image = My.Resources.Resources.prismatics
        Me.ToolStripMenuItemZoom.Name = "ToolStripMenuItemZoom"
        Me.ToolStripMenuItemZoom.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripMenuItemZoom.Text = "Zoom"
        '
        'ToolStripMenuItemCtasAddNew
        '
        Me.ToolStripMenuItemCtasAddNew.Image = My.Resources.Resources.clip
        Me.ToolStripMenuItemCtasAddNew.Name = "ToolStripMenuItemCtasAddNew"
        Me.ToolStripMenuItemCtasAddNew.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripMenuItemCtasAddNew.Text = "Afegir"
        '
        'ToolStripMenuItemCtasRemove
        '
        Me.ToolStripMenuItemCtasRemove.Image = My.Resources.Resources.del
        Me.ToolStripMenuItemCtasRemove.Name = "ToolStripMenuItemCtasRemove"
        Me.ToolStripMenuItemCtasRemove.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripMenuItemCtasRemove.Text = "Treurer"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageCtas)
        Me.TabControl1.Location = New System.Drawing.Point(6, 38)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(541, 427)
        Me.TabControl1.TabIndex = 71
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.TextBoxEng)
        Me.TabPageGral.Controls.Add(Me.Label6)
        Me.TabPageGral.Controls.Add(Me.TextBoxCat)
        Me.TabPageGral.Controls.Add(Me.Label5)
        Me.TabPageGral.Controls.Add(Me.Label4)
        Me.TabPageGral.Controls.Add(Me.ComboBoxPlan)
        Me.TabPageGral.Controls.Add(Me.TextBoxCod)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.ButtonEditCtas)
        Me.TabPageGral.Controls.Add(Me.DataGridView1)
        Me.TabPageGral.Controls.Add(Me.TextBoxEsp)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Controls.Add(Me.TextBoxLevel)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGral.Size = New System.Drawing.Size(533, 401)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "GENERAL"
        Me.TabPageGral.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(244, 159)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "Pla Comptable:"
        '
        'ComboBoxPlan
        '
        Me.ComboBoxPlan.FormattingEnabled = True
        Me.ComboBoxPlan.Location = New System.Drawing.Point(325, 154)
        Me.ComboBoxPlan.Name = "ComboBoxPlan"
        Me.ComboBoxPlan.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxPlan.TabIndex = 75
        '
        'TextBoxCod
        '
        Me.TextBoxCod.Location = New System.Drawing.Point(399, 9)
        Me.TextBoxCod.Name = "TextBoxCod"
        Me.TextBoxCod.ReadOnly = True
        Me.TextBoxCod.Size = New System.Drawing.Size(128, 20)
        Me.TextBoxCod.TabIndex = 73
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(348, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "Codi:"
        '
        'ButtonEditCtas
        '
        Me.ButtonEditCtas.Location = New System.Drawing.Point(452, 154)
        Me.ButtonEditCtas.Name = "ButtonEditCtas"
        Me.ButtonEditCtas.Size = New System.Drawing.Size(75, 23)
        Me.ButtonEditCtas.TabIndex = 72
        Me.ButtonEditCtas.Text = "cuentas..."
        Me.ButtonEditCtas.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(3, 183)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(524, 212)
        Me.DataGridView1.TabIndex = 71
        '
        'TabPageCtas
        '
        Me.TabPageCtas.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCtas.Name = "TabPageCtas"
        Me.TabPageCtas.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCtas.Size = New System.Drawing.Size(533, 401)
        Me.TabPageCtas.TabIndex = 1
        Me.TabPageCtas.Text = "COMPTES"
        Me.TabPageCtas.UseVisualStyleBackColor = True
        '
        'TextBoxCat
        '
        Me.TextBoxCat.Location = New System.Drawing.Point(112, 71)
        Me.TextBoxCat.MaxLength = 60
        Me.TextBoxCat.Name = "TextBoxCat"
        Me.TextBoxCat.Size = New System.Drawing.Size(299, 20)
        Me.TextBoxCat.TabIndex = 77
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(53, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 78
        Me.Label5.Text = "Catalá:"
        '
        'TextBoxEng
        '
        Me.TextBoxEng.Location = New System.Drawing.Point(112, 97)
        Me.TextBoxEng.MaxLength = 60
        Me.TextBoxEng.Name = "TextBoxEng"
        Me.TextBoxEng.Size = New System.Drawing.Size(299, 20)
        Me.TextBoxEng.TabIndex = 79
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(53, 100)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 80
        Me.Label6.Text = "Anglés:"
        '
        'Frm_PgcEpg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(559, 513)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_PgcEpg"
        Me.Text = "EPIGRAFE CONTABLE"
        Me.Panel1.ResumeLayout(False)
        Me.ContextMenuStripCtas.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxEsp As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxLevel As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStripCtas As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItemZoom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemCtasAddNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemCtasRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCtas As System.Windows.Forms.TabPage
    Friend WithEvents ButtonEditCtas As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBoxCod As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxPlan As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxEng As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCat As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
