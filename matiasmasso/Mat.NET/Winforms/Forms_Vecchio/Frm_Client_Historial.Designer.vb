<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Client_Historial
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
        Me.ButtonRefreshFiltre = New System.Windows.Forms.Button
        Me.ComboBoxArt = New System.Windows.Forms.ComboBox
        Me.ComboBoxStp = New System.Windows.Forms.ComboBox
        Me.ComboBoxTpa = New System.Windows.Forms.ComboBox
        Me.CheckBoxFiltre = New System.Windows.Forms.CheckBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonRefreshFiltre
        '
        Me.ButtonRefreshFiltre.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRefreshFiltre.Enabled = False
        Me.ButtonRefreshFiltre.Location = New System.Drawing.Point(564, 288)
        Me.ButtonRefreshFiltre.Name = "ButtonRefreshFiltre"
        Me.ButtonRefreshFiltre.Size = New System.Drawing.Size(80, 20)
        Me.ButtonRefreshFiltre.TabIndex = 11
        Me.ButtonRefreshFiltre.Text = "refresca"
        '
        'ComboBoxArt
        '
        Me.ComboBoxArt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxArt.Location = New System.Drawing.Point(388, 288)
        Me.ComboBoxArt.Name = "ComboBoxArt"
        Me.ComboBoxArt.Size = New System.Drawing.Size(168, 21)
        Me.ComboBoxArt.TabIndex = 10
        Me.ComboBoxArt.Visible = False
        '
        'ComboBoxStp
        '
        Me.ComboBoxStp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxStp.Location = New System.Drawing.Point(212, 288)
        Me.ComboBoxStp.Name = "ComboBoxStp"
        Me.ComboBoxStp.Size = New System.Drawing.Size(176, 21)
        Me.ComboBoxStp.TabIndex = 9
        Me.ComboBoxStp.Visible = False
        '
        'ComboBoxTpa
        '
        Me.ComboBoxTpa.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxTpa.Location = New System.Drawing.Point(52, 288)
        Me.ComboBoxTpa.Name = "ComboBoxTpa"
        Me.ComboBoxTpa.Size = New System.Drawing.Size(160, 21)
        Me.ComboBoxTpa.TabIndex = 8
        Me.ComboBoxTpa.Visible = False
        '
        'CheckBoxFiltre
        '
        Me.CheckBoxFiltre.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFiltre.Location = New System.Drawing.Point(4, 288)
        Me.CheckBoxFiltre.Name = "CheckBoxFiltre"
        Me.CheckBoxFiltre.Size = New System.Drawing.Size(48, 16)
        Me.CheckBoxFiltre.TabIndex = 7
        Me.CheckBoxFiltre.Text = "Filtre"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 28)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(656, 255)
        Me.DataGridView1.TabIndex = 12
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonExcel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(656, 25)
        Me.ToolStrip1.TabIndex = 13
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "ToolStripButton1"
        '
        'Frm_Client_Historial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(656, 311)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ButtonRefreshFiltre)
        Me.Controls.Add(Me.ComboBoxArt)
        Me.Controls.Add(Me.ComboBoxStp)
        Me.Controls.Add(Me.ComboBoxTpa)
        Me.Controls.Add(Me.CheckBoxFiltre)
        Me.Name = "Frm_Client_Historial"
        Me.Text = "HISTORIAL"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonRefreshFiltre As System.Windows.Forms.Button
    Friend WithEvents ComboBoxArt As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxStp As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxTpa As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxFiltre As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
End Class
