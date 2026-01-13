<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Mgz_Stks
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.RECUENTOToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ButtonRefresca = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridViewTpas = New System.Windows.Forms.DataGridView()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.DataGridViewStps = New System.Windows.Forms.DataGridView()
        Me.DataGridViewArts = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxDias50 = New System.Windows.Forms.TextBox()
        Me.TextBoxDias100 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonSaveCca = New System.Windows.Forms.Button()
        Me.ComboBoxMgz = New System.Windows.Forms.ComboBox()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridViewTpas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataGridViewStps, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewArts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RECUENTOToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1115, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'RECUENTOToolStripButton
        '
        Me.RECUENTOToolStripButton.Image = Global.Mat.NET.My.Resources.Resources.Excel
        Me.RECUENTOToolStripButton.Name = "RECUENTOToolStripButton"
        Me.RECUENTOToolStripButton.Size = New System.Drawing.Size(77, 22)
        Me.RECUENTOToolStripButton.Text = "Recuento"
        '
        'ButtonRefresca
        '
        Me.ButtonRefresca.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRefresca.Location = New System.Drawing.Point(1065, 3)
        Me.ButtonRefresca.Name = "ButtonRefresca"
        Me.ButtonRefresca.Size = New System.Drawing.Size(48, 20)
        Me.ButtonRefresca.TabIndex = 9
        Me.ButtonRefresca.Text = "..."
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(969, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePicker1.TabIndex = 8
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridViewTpas)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1115, 413)
        Me.SplitContainer1.SplitterDistance = 285
        Me.SplitContainer1.TabIndex = 10
        '
        'DataGridViewTpas
        '
        Me.DataGridViewTpas.AllowUserToAddRows = False
        Me.DataGridViewTpas.AllowUserToDeleteRows = False
        Me.DataGridViewTpas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewTpas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewTpas.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewTpas.Name = "DataGridViewTpas"
        Me.DataGridViewTpas.ReadOnly = True
        Me.DataGridViewTpas.Size = New System.Drawing.Size(285, 413)
        Me.DataGridViewTpas.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DataGridViewStps)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataGridViewArts)
        Me.SplitContainer2.Size = New System.Drawing.Size(826, 413)
        Me.SplitContainer2.SplitterDistance = 293
        Me.SplitContainer2.TabIndex = 0
        '
        'DataGridViewStps
        '
        Me.DataGridViewStps.AllowUserToAddRows = False
        Me.DataGridViewStps.AllowUserToDeleteRows = False
        Me.DataGridViewStps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewStps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewStps.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewStps.Name = "DataGridViewStps"
        Me.DataGridViewStps.ReadOnly = True
        Me.DataGridViewStps.Size = New System.Drawing.Size(293, 413)
        Me.DataGridViewStps.TabIndex = 1
        '
        'DataGridViewArts
        '
        Me.DataGridViewArts.AllowUserToAddRows = False
        Me.DataGridViewArts.AllowUserToDeleteRows = False
        Me.DataGridViewArts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewArts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewArts.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewArts.Name = "DataGridViewArts"
        Me.DataGridViewArts.ReadOnly = True
        Me.DataGridViewArts.Size = New System.Drawing.Size(529, 413)
        Me.DataGridViewArts.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(99, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(194, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "desvaloritza 50% stocks antiguitat> dias"
        '
        'TextBoxDias50
        '
        Me.TextBoxDias50.Location = New System.Drawing.Point(296, 2)
        Me.TextBoxDias50.Name = "TextBoxDias50"
        Me.TextBoxDias50.Size = New System.Drawing.Size(31, 20)
        Me.TextBoxDias50.TabIndex = 12
        '
        'TextBoxDias100
        '
        Me.TextBoxDias100.Location = New System.Drawing.Point(549, 2)
        Me.TextBoxDias100.Name = "TextBoxDias100"
        Me.TextBoxDias100.Size = New System.Drawing.Size(31, 20)
        Me.TextBoxDias100.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(352, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(200, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "desvaloritza 100% stocks antiguitat> dias"
        '
        'ButtonSaveCca
        '
        Me.ButtonSaveCca.Location = New System.Drawing.Point(602, -1)
        Me.ButtonSaveCca.Name = "ButtonSaveCca"
        Me.ButtonSaveCca.Size = New System.Drawing.Size(134, 23)
        Me.ButtonSaveCca.TabIndex = 15
        Me.ButtonSaveCca.Text = "registrar assentament"
        Me.ButtonSaveCca.UseVisualStyleBackColor = True
        '
        'ComboBoxMgz
        '
        Me.ComboBoxMgz.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxMgz.FormattingEnabled = True
        Me.ComboBoxMgz.Location = New System.Drawing.Point(743, 2)
        Me.ComboBoxMgz.Name = "ComboBoxMgz"
        Me.ComboBoxMgz.Size = New System.Drawing.Size(220, 21)
        Me.ComboBoxMgz.TabIndex = 16
        '
        'Frm_Mgz_Stks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1115, 438)
        Me.Controls.Add(Me.ComboBoxMgz)
        Me.Controls.Add(Me.ButtonSaveCca)
        Me.Controls.Add(Me.TextBoxDias100)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxDias50)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ButtonRefresca)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Mgz_Stks"
        Me.Text = "STOCKS"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridViewTpas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataGridViewStps, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewArts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents RECUENTOToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ButtonRefresca As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewTpas As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewStps As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewArts As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDias50 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDias100 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonSaveCca As System.Windows.Forms.Button
    Friend WithEvents ComboBoxMgz As System.Windows.Forms.ComboBox
End Class
