<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Art_Trajectoria
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPageGrafica = New System.Windows.Forms.TabPage
        Me.PictureBoxGrafica = New System.Windows.Forms.PictureBox
        Me.TabPageData = New System.Windows.Forms.TabPage
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonRefresca = New System.Windows.Forms.ToolStripButton
        Me.ComboBoxYea = New System.Windows.Forms.ComboBox
        Me.TabControl1.SuspendLayout()
        Me.TabPageGrafica.SuspendLayout()
        CType(Me.PictureBoxGrafica, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageData.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGrafica)
        Me.TabControl1.Controls.Add(Me.TabPageData)
        Me.TabControl1.Location = New System.Drawing.Point(-1, 43)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(767, 438)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageGrafica
        '
        Me.TabPageGrafica.Controls.Add(Me.PictureBoxGrafica)
        Me.TabPageGrafica.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGrafica.Name = "TabPageGrafica"
        Me.TabPageGrafica.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGrafica.Size = New System.Drawing.Size(759, 412)
        Me.TabPageGrafica.TabIndex = 0
        Me.TabPageGrafica.Text = "GRAFICA"
        Me.TabPageGrafica.UseVisualStyleBackColor = True
        '
        'PictureBoxGrafica
        '
        Me.PictureBoxGrafica.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxGrafica.Location = New System.Drawing.Point(9, 6)
        Me.PictureBoxGrafica.Name = "PictureBoxGrafica"
        Me.PictureBoxGrafica.Size = New System.Drawing.Size(744, 403)
        Me.PictureBoxGrafica.TabIndex = 0
        Me.PictureBoxGrafica.TabStop = False
        '
        'TabPageData
        '
        Me.TabPageData.Controls.Add(Me.DataGridView1)
        Me.TabPageData.Location = New System.Drawing.Point(4, 22)
        Me.TabPageData.Name = "TabPageData"
        Me.TabPageData.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageData.Size = New System.Drawing.Size(759, 384)
        Me.TabPageData.TabIndex = 1
        Me.TabPageData.Text = "DADES"
        Me.TabPageData.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(753, 378)
        Me.DataGridView1.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonExcel, Me.ToolStripButtonRefresca})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(768, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "Excel"
        '
        'ToolStripButtonRefresca
        '
        Me.ToolStripButtonRefresca.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRefresca.Image = My.Resources.Resources.refresca
        Me.ToolStripButtonRefresca.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRefresca.Name = "ToolStripButtonRefresca"
        Me.ToolStripButtonRefresca.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonRefresca.Text = "refresca"
        '
        'ComboBoxYea
        '
        Me.ComboBoxYea.FormattingEnabled = True
        Me.ComboBoxYea.Location = New System.Drawing.Point(541, 4)
        Me.ComboBoxYea.Name = "ComboBoxYea"
        Me.ComboBoxYea.Size = New System.Drawing.Size(68, 21)
        Me.ComboBoxYea.TabIndex = 2
        '
        'Frm_Art_Trajectoria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(768, 479)
        Me.Controls.Add(Me.ComboBoxYea)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Art_Trajectoria"
        Me.Text = "Trajectoria"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGrafica.ResumeLayout(False)
        CType(Me.PictureBoxGrafica, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageData.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGrafica As System.Windows.Forms.TabPage
    Friend WithEvents TabPageData As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonRefresca As System.Windows.Forms.ToolStripButton
    Friend WithEvents ComboBoxYea As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBoxGrafica As System.Windows.Forms.PictureBox
End Class
