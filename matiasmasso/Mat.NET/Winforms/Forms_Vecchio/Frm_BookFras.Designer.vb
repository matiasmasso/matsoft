<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BookFras
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
        Me.ComboBoxBookFraMode = New System.Windows.Forms.ComboBox()
        Me.ComboBoxMes = New System.Windows.Forms.ComboBox()
        Me.CheckBoxMes = New System.Windows.Forms.CheckBox()
        Me.Xl_BookFras1 = New Winforms.Xl_BookFras()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGridViewCtas = New System.Windows.Forms.DataGridView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.DataGridView347 = New System.Windows.Forms.DataGridView()
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown()
        Me.ButtonExcel = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridViewCtas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.DataGridView347, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 22)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1070, 373)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ButtonExcel)
        Me.TabPage1.Controls.Add(Me.ComboBoxBookFraMode)
        Me.TabPage1.Controls.Add(Me.ComboBoxMes)
        Me.TabPage1.Controls.Add(Me.CheckBoxMes)
        Me.TabPage1.Controls.Add(Me.Xl_BookFras1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1062, 347)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Factures"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ComboBoxBookFraMode
        '
        Me.ComboBoxBookFraMode.FormattingEnabled = True
        Me.ComboBoxBookFraMode.Items.AddRange(New Object() {"totes les factures", "nomes les factures amb IVA", "nomes les factures amb IRPF"})
        Me.ComboBoxBookFraMode.Location = New System.Drawing.Point(3, 6)
        Me.ComboBoxBookFraMode.Name = "ComboBoxBookFraMode"
        Me.ComboBoxBookFraMode.Size = New System.Drawing.Size(247, 21)
        Me.ComboBoxBookFraMode.TabIndex = 5
        '
        'ComboBoxMes
        '
        Me.ComboBoxMes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxMes.FormattingEnabled = True
        Me.ComboBoxMes.Location = New System.Drawing.Point(995, 6)
        Me.ComboBoxMes.Name = "ComboBoxMes"
        Me.ComboBoxMes.Size = New System.Drawing.Size(59, 21)
        Me.ComboBoxMes.TabIndex = 4
        '
        'CheckBoxMes
        '
        Me.CheckBoxMes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxMes.AutoSize = True
        Me.CheckBoxMes.Checked = True
        Me.CheckBoxMes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxMes.Location = New System.Drawing.Point(875, 8)
        Me.CheckBoxMes.Name = "CheckBoxMes"
        Me.CheckBoxMes.Size = New System.Drawing.Size(114, 17)
        Me.CheckBoxMes.TabIndex = 3
        Me.CheckBoxMes.Text = "Nomes les del mes"
        Me.CheckBoxMes.UseVisualStyleBackColor = True
        '
        'Xl_BookFras1
        '
        Me.Xl_BookFras1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BookFras1.Location = New System.Drawing.Point(3, 33)
        Me.Xl_BookFras1.Name = "Xl_BookFras1"
        Me.Xl_BookFras1.Size = New System.Drawing.Size(1056, 314)
        Me.Xl_BookFras1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridViewCtas)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1062, 347)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Comptes"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridViewCtas
        '
        Me.DataGridViewCtas.AllowUserToAddRows = False
        Me.DataGridViewCtas.AllowUserToDeleteRows = False
        Me.DataGridViewCtas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCtas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewCtas.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewCtas.Name = "DataGridViewCtas"
        Me.DataGridViewCtas.ReadOnly = True
        Me.DataGridViewCtas.Size = New System.Drawing.Size(1056, 341)
        Me.DataGridViewCtas.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.DataGridView347)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1062, 347)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "347"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'DataGridView347
        '
        Me.DataGridView347.AllowUserToAddRows = False
        Me.DataGridView347.AllowUserToDeleteRows = False
        Me.DataGridView347.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView347.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView347.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView347.Name = "DataGridView347"
        Me.DataGridView347.ReadOnly = True
        Me.DataGridView347.Size = New System.Drawing.Size(1062, 347)
        Me.DataGridView347.TabIndex = 2
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Location = New System.Drawing.Point(1010, 12)
        Me.NumericUpDownYea.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        Me.NumericUpDownYea.Minimum = New Decimal(New Integer() {1985, 0, 0, 0})
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.NumericUpDownYea.Size = New System.Drawing.Size(48, 20)
        Me.NumericUpDownYea.TabIndex = 1
        Me.NumericUpDownYea.Value = New Decimal(New Integer() {2000, 0, 0, 0})
        '
        'ButtonExcel
        '
        Me.ButtonExcel.Image = Global.Winforms.My.Resources.Resources.Excel
        Me.ButtonExcel.Location = New System.Drawing.Point(256, 4)
        Me.ButtonExcel.Name = "ButtonExcel"
        Me.ButtonExcel.Size = New System.Drawing.Size(24, 24)
        Me.ButtonExcel.TabIndex = 6
        Me.ButtonExcel.UseVisualStyleBackColor = True
        '
        'Frm_BookFras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1070, 395)
        Me.Controls.Add(Me.NumericUpDownYea)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_BookFras"
        Me.Text = "llibre de factures rebudes"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridViewCtas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.DataGridView347, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewCtas As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView347 As System.Windows.Forms.DataGridView
    Friend WithEvents NumericUpDownYea As System.Windows.Forms.NumericUpDown
    Friend WithEvents Xl_BookFras1 As Winforms.Xl_BookFras
    Friend WithEvents ComboBoxMes As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxMes As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxBookFraMode As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonExcel As System.Windows.Forms.Button
End Class
