<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Inventory
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.RECUENTOToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.Xl_MatDateTimePicker1 = New Xl_MatDateTimePicker()
        Me.ComboBoxMgz = New System.Windows.Forms.ComboBox()
        Me.ButtonSaveCca = New System.Windows.Forms.Button()
        Me.TextBoxDias100 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxDias50 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonRefresca = New System.Windows.Forms.Button()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_InventoryCategories = New Xl_Inventory2()
        Me.Xl_InventorySkus = New Xl_Inventory2()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_InventoryBrands = New Xl_Inventory2()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_InventoryCategories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_InventorySkus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_InventoryBrands, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RECUENTOToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1071, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'RECUENTOToolStripButton
        '
        Me.RECUENTOToolStripButton.Image = Global.Mat.Net.My.Resources.Resources.Excel
        Me.RECUENTOToolStripButton.Name = "RECUENTOToolStripButton"
        Me.RECUENTOToolStripButton.Size = New System.Drawing.Size(77, 22)
        Me.RECUENTOToolStripButton.Text = "Recuento"
        '
        'Xl_MatDateTimePicker1
        '
        Me.Xl_MatDateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_MatDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Xl_MatDateTimePicker1.IsDroppedDown = False
        Me.Xl_MatDateTimePicker1.Location = New System.Drawing.Point(921, 4)
        Me.Xl_MatDateTimePicker1.Name = "Xl_MatDateTimePicker1"
        Me.Xl_MatDateTimePicker1.Size = New System.Drawing.Size(90, 20)
        Me.Xl_MatDateTimePicker1.TabIndex = 25
        '
        'ComboBoxMgz
        '
        Me.ComboBoxMgz.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxMgz.FormattingEnabled = True
        Me.ComboBoxMgz.Location = New System.Drawing.Point(739, 3)
        Me.ComboBoxMgz.Name = "ComboBoxMgz"
        Me.ComboBoxMgz.Size = New System.Drawing.Size(176, 21)
        Me.ComboBoxMgz.TabIndex = 24
        '
        'ButtonSaveCca
        '
        Me.ButtonSaveCca.Location = New System.Drawing.Point(598, 0)
        Me.ButtonSaveCca.Name = "ButtonSaveCca"
        Me.ButtonSaveCca.Size = New System.Drawing.Size(134, 23)
        Me.ButtonSaveCca.TabIndex = 23
        Me.ButtonSaveCca.Text = "registrar assentament"
        Me.ButtonSaveCca.UseVisualStyleBackColor = True
        '
        'TextBoxDias100
        '
        Me.TextBoxDias100.Location = New System.Drawing.Point(545, 3)
        Me.TextBoxDias100.Name = "TextBoxDias100"
        Me.TextBoxDias100.Size = New System.Drawing.Size(31, 20)
        Me.TextBoxDias100.TabIndex = 22
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(348, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(200, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "desvaloritza 100% stocks antiguitat> dias"
        '
        'TextBoxDias50
        '
        Me.TextBoxDias50.Location = New System.Drawing.Point(292, 3)
        Me.TextBoxDias50.Name = "TextBoxDias50"
        Me.TextBoxDias50.Size = New System.Drawing.Size(31, 20)
        Me.TextBoxDias50.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(95, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(194, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "desvaloritza 50% stocks antiguitat> dias"
        '
        'ButtonRefresca
        '
        Me.ButtonRefresca.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRefresca.Location = New System.Drawing.Point(1017, 4)
        Me.ButtonRefresca.Name = "ButtonRefresca"
        Me.ButtonRefresca.Size = New System.Drawing.Size(48, 20)
        Me.ButtonRefresca.TabIndex = 18
        Me.ButtonRefresca.Text = "..."
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_InventoryCategories)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_InventorySkus)
        Me.SplitContainer2.Size = New System.Drawing.Size(794, 390)
        Me.SplitContainer2.SplitterDistance = 281
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_InventoryCategories
        '
        Me.Xl_InventoryCategories.AllowUserToAddRows = False
        Me.Xl_InventoryCategories.AllowUserToDeleteRows = False
        Me.Xl_InventoryCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InventoryCategories.DisplayObsolets = False
        Me.Xl_InventoryCategories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_InventoryCategories.Location = New System.Drawing.Point(0, 0)
        Me.Xl_InventoryCategories.MouseIsDown = False
        Me.Xl_InventoryCategories.Name = "Xl_InventoryCategories"
        Me.Xl_InventoryCategories.ReadOnly = True
        Me.Xl_InventoryCategories.Size = New System.Drawing.Size(281, 390)
        Me.Xl_InventoryCategories.TabIndex = 1
        '
        'Xl_InventorySkus
        '
        Me.Xl_InventorySkus.AllowUserToAddRows = False
        Me.Xl_InventorySkus.AllowUserToDeleteRows = False
        Me.Xl_InventorySkus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InventorySkus.DisplayObsolets = False
        Me.Xl_InventorySkus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_InventorySkus.Location = New System.Drawing.Point(0, 0)
        Me.Xl_InventorySkus.MouseIsDown = False
        Me.Xl_InventorySkus.Name = "Xl_InventorySkus"
        Me.Xl_InventorySkus.ReadOnly = True
        Me.Xl_InventorySkus.Size = New System.Drawing.Size(509, 390)
        Me.Xl_InventorySkus.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_InventoryBrands)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1071, 390)
        Me.SplitContainer1.SplitterDistance = 273
        Me.SplitContainer1.TabIndex = 26
        '
        'Xl_InventoryBrands
        '
        Me.Xl_InventoryBrands.AllowUserToAddRows = False
        Me.Xl_InventoryBrands.AllowUserToDeleteRows = False
        Me.Xl_InventoryBrands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InventoryBrands.DisplayObsolets = False
        Me.Xl_InventoryBrands.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_InventoryBrands.Location = New System.Drawing.Point(0, 0)
        Me.Xl_InventoryBrands.MouseIsDown = False
        Me.Xl_InventoryBrands.Name = "Xl_InventoryBrands"
        Me.Xl_InventoryBrands.ReadOnly = True
        Me.Xl_InventoryBrands.Size = New System.Drawing.Size(273, 390)
        Me.Xl_InventoryBrands.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 415)
        Me.ProgressBar1.MarqueeAnimationSpeed = 800
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1071, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 27
        '
        'Frm_Inventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1071, 438)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Xl_MatDateTimePicker1)
        Me.Controls.Add(Me.ComboBoxMgz)
        Me.Controls.Add(Me.ButtonSaveCca)
        Me.Controls.Add(Me.TextBoxDias100)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxDias50)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonRefresca)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Inventory"
        Me.Text = "Frm_Inventory"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_InventoryCategories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_InventorySkus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_InventoryBrands, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents RECUENTOToolStripButton As ToolStripButton
    Friend WithEvents Xl_MatDateTimePicker1 As Xl_MatDateTimePicker
    Friend WithEvents ComboBoxMgz As ComboBox
    Friend WithEvents ButtonSaveCca As Button
    Friend WithEvents TextBoxDias100 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxDias50 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ButtonRefresca As Button
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Xl_InventoryCategories As Xl_Inventory2
    Friend WithEvents Xl_InventorySkus As Xl_Inventory2
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_InventoryBrands As Xl_Inventory2
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
