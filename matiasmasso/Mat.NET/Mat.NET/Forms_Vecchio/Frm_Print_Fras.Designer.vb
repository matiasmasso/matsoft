<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Print_Fras
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DescartarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.ComboBoxFchs = New System.Windows.Forms.ComboBox
        Me.CheckBoxEfras = New System.Windows.Forms.CheckBox
        Me.CheckBoxPrint = New System.Windows.Forms.CheckBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ButtonCheckAll = New System.Windows.Forms.Button
        Me.ButtonCheckNone = New System.Windows.Forms.Button
        Me.CheckBoxCliFilter = New System.Windows.Forms.CheckBox
        Me.Xl_Contact1 = New Xl_Contact
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.LabelStatus = New System.Windows.Forms.Label
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DescartarToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(124, 26)
        '
        'DescartarToolStripMenuItem
        '
        Me.DescartarToolStripMenuItem.Name = "DescartarToolStripMenuItem"
        Me.DescartarToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.DescartarToolStripMenuItem.Text = "Descartar"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.LabelStatus)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 221)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(556, 45)
        Me.Panel1.TabIndex = 1
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Image = My.Resources.Resources.del
        Me.ButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCancel.Location = New System.Drawing.Point(3, 6)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(92, 34)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Image = My.Resources.Resources.printer
        Me.ButtonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonOk.Location = New System.Drawing.Point(420, 6)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(133, 34)
        Me.ButtonOk.TabIndex = 0
        Me.ButtonOk.Text = "ENVIAR/IMPRIMIR"
        Me.ButtonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonOk.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "data de impresió/enviament"
        '
        'ComboBoxFchs
        '
        Me.ComboBoxFchs.FormattingEnabled = True
        Me.ComboBoxFchs.Location = New System.Drawing.Point(12, 20)
        Me.ComboBoxFchs.Name = "ComboBoxFchs"
        Me.ComboBoxFchs.Size = New System.Drawing.Size(153, 21)
        Me.ComboBoxFchs.TabIndex = 6
        '
        'CheckBoxEfras
        '
        Me.CheckBoxEfras.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxEfras.AutoSize = True
        Me.CheckBoxEfras.Checked = True
        Me.CheckBoxEfras.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxEfras.Location = New System.Drawing.Point(420, 24)
        Me.CheckBoxEfras.Name = "CheckBoxEfras"
        Me.CheckBoxEfras.Size = New System.Drawing.Size(112, 17)
        Me.CheckBoxEfras.TabIndex = 9
        Me.CheckBoxEfras.Text = "a enviar per e-mail"
        Me.CheckBoxEfras.UseVisualStyleBackColor = True
        '
        'CheckBoxPrint
        '
        Me.CheckBoxPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxPrint.AutoSize = True
        Me.CheckBoxPrint.Location = New System.Drawing.Point(420, 7)
        Me.CheckBoxPrint.Name = "CheckBoxPrint"
        Me.CheckBoxPrint.Size = New System.Drawing.Size(78, 17)
        Me.CheckBoxPrint.TabIndex = 8
        Me.CheckBoxPrint.Text = "per imprimir"
        Me.CheckBoxPrint.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(3, 72)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(550, 149)
        Me.DataGridView1.TabIndex = 10
        '
        'ButtonCheckAll
        '
        Me.ButtonCheckAll.Location = New System.Drawing.Point(188, 5)
        Me.ButtonCheckAll.Name = "ButtonCheckAll"
        Me.ButtonCheckAll.Size = New System.Drawing.Size(104, 21)
        Me.ButtonCheckAll.TabIndex = 11
        Me.ButtonCheckAll.Text = "Seleccionar tot"
        Me.ButtonCheckAll.UseVisualStyleBackColor = True
        '
        'ButtonCheckNone
        '
        Me.ButtonCheckNone.Location = New System.Drawing.Point(291, 5)
        Me.ButtonCheckNone.Name = "ButtonCheckNone"
        Me.ButtonCheckNone.Size = New System.Drawing.Size(104, 21)
        Me.ButtonCheckNone.TabIndex = 12
        Me.ButtonCheckNone.Text = "Deseleccionar tot"
        Me.ButtonCheckNone.UseVisualStyleBackColor = True
        '
        'CheckBoxCliFilter
        '
        Me.CheckBoxCliFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxCliFilter.AutoSize = True
        Me.CheckBoxCliFilter.Location = New System.Drawing.Point(13, 46)
        Me.CheckBoxCliFilter.Name = "CheckBoxCliFilter"
        Me.CheckBoxCliFilter.Size = New System.Drawing.Size(79, 17)
        Me.CheckBoxCliFilter.TabIndex = 13
        Me.CheckBoxCliFilter.Text = "Filtrar client"
        Me.CheckBoxCliFilter.UseVisualStyleBackColor = True
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Location = New System.Drawing.Point(92, 43)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.Size = New System.Drawing.Size(461, 20)
        Me.Xl_Contact1.TabIndex = 14
        Me.Xl_Contact1.Visible = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(98, 20)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(455, 18)
        Me.ProgressBar1.TabIndex = 2
        Me.ProgressBar1.Visible = False
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Location = New System.Drawing.Point(98, 4)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(10, 13)
        Me.LabelStatus.TabIndex = 3
        Me.LabelStatus.Text = " "
        '
        'Frm_Print_Fras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(556, 266)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Controls.Add(Me.CheckBoxCliFilter)
        Me.Controls.Add(Me.ButtonCheckNone)
        Me.Controls.Add(Me.ButtonCheckAll)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.CheckBoxEfras)
        Me.Controls.Add(Me.CheckBoxPrint)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxFchs)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Print_Fras"
        Me.Text = "FACTURES PENDENTS DE ENVIAR"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxFchs As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxEfras As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPrint As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DescartarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonCheckAll As System.Windows.Forms.Button
    Friend WithEvents ButtonCheckNone As System.Windows.Forms.Button
    Friend WithEvents CheckBoxCliFilter As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_Contact1 As Xl_Contact
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents LabelStatus As System.Windows.Forms.Label
End Class
