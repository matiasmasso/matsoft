<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Raffles
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
        Me.Xl_Raffles1 = New Xl_Raffles()
        Me.ComboBoxBrands = New System.Windows.Forms.ComboBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton()
        Me.Xl_Years1 = New Xl_Years()
        Me.Xl_Langs1 = New Xl_Langs()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Raffles1
        '
        Me.Xl_Raffles1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Raffles1.Filter = Nothing
        Me.Xl_Raffles1.Location = New System.Drawing.Point(0, 25)
        Me.Xl_Raffles1.Name = "Xl_Raffles1"
        Me.Xl_Raffles1.Size = New System.Drawing.Size(849, 213)
        Me.Xl_Raffles1.TabIndex = 0
        '
        'ComboBoxBrands
        '
        Me.ComboBoxBrands.FormattingEnabled = True
        Me.ComboBoxBrands.Location = New System.Drawing.Point(561, 2)
        Me.ComboBoxBrands.Name = "ComboBoxBrands"
        Me.ComboBoxBrands.Size = New System.Drawing.Size(288, 21)
        Me.ComboBoxBrands.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonExcel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(849, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = Global.Mat.Net.My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "ToolStripButton1"
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(336, 2)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 3
        Me.Xl_Years1.Value = 0
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(505, 2)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(50, 21)
        Me.Xl_Langs1.TabIndex = 4
        Me.Xl_Langs1.Value = Nothing
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 238)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(849, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 5
        Me.ProgressBar1.Visible = False
        '
        'Frm_Raffles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(849, 261)
        Me.Controls.Add(Me.Xl_Raffles1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Xl_Langs1)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.ComboBoxBrands)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Raffles"
        Me.Text = "Sortejos"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Raffles1 As Xl_Raffles
    Friend WithEvents ComboBoxBrands As ComboBox
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButtonExcel As ToolStripButton
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Xl_Langs1 As Xl_Langs
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
