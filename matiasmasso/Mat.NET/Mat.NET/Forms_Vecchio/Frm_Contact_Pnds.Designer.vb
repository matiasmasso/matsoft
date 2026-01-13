<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Contact_Pnds
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
        Me.ExcelToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.PagarToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.TextBoxTotDeutor = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.AfegirToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.TextBoxTotCreditor = New System.Windows.Forms.TextBox
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ZoomToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ExtracteToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.CobrarToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExcelToolStripButton
        '
        Me.ExcelToolStripButton.Image = My.Resources.Resources.Excel
        Me.ExcelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ExcelToolStripButton.Name = "ExcelToolStripButton"
        Me.ExcelToolStripButton.Size = New System.Drawing.Size(53, 22)
        Me.ExcelToolStripButton.Text = "Excel"
        '
        'PagarToolStripButton
        '
        Me.PagarToolStripButton.Image = My.Resources.Resources.SquareArrowOrange
        Me.PagarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PagarToolStripButton.Name = "PagarToolStripButton"
        Me.PagarToolStripButton.Size = New System.Drawing.Size(57, 22)
        Me.PagarToolStripButton.Text = "Pagar"
        '
        'TextBoxTotDeutor
        '
        Me.TextBoxTotDeutor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTotDeutor.Location = New System.Drawing.Point(474, 274)
        Me.TextBoxTotDeutor.Name = "TextBoxTotDeutor"
        Me.TextBoxTotDeutor.ReadOnly = True
        Me.TextBoxTotDeutor.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxTotDeutor.TabIndex = 11
        Me.TextBoxTotDeutor.TabStop = False
        Me.TextBoxTotDeutor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(499, 258)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Saldo Deutor"
        '
        'AfegirToolStripButton
        '
        Me.AfegirToolStripButton.Image = My.Resources.Resources.clip
        Me.AfegirToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AfegirToolStripButton.Name = "AfegirToolStripButton"
        Me.AfegirToolStripButton.Size = New System.Drawing.Size(59, 22)
        Me.AfegirToolStripButton.Text = "Afegir"
        '
        'TextBoxTotCreditor
        '
        Me.TextBoxTotCreditor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTotCreditor.Location = New System.Drawing.Point(374, 274)
        Me.TextBoxTotCreditor.Name = "TextBoxTotCreditor"
        Me.TextBoxTotCreditor.ReadOnly = True
        Me.TextBoxTotCreditor.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxTotCreditor.TabIndex = 9
        Me.TextBoxTotCreditor.TabStop = False
        Me.TextBoxTotCreditor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZoomToolStripButton, Me.AfegirToolStripButton, Me.ExtracteToolStripButton, Me.CobrarToolStripButton, Me.PagarToolStripButton, Me.ExcelToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(572, 25)
        Me.ToolStrip1.TabIndex = 6
        '
        'ZoomToolStripButton
        '
        Me.ZoomToolStripButton.Image = My.Resources.Resources.prismatics
        Me.ZoomToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ZoomToolStripButton.Name = "ZoomToolStripButton"
        Me.ZoomToolStripButton.Size = New System.Drawing.Size(59, 22)
        Me.ZoomToolStripButton.Text = "Zoom"
        '
        'ExtracteToolStripButton
        '
        Me.ExtracteToolStripButton.Image = My.Resources.Resources.tabla_16
        Me.ExtracteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ExtracteToolStripButton.Name = "ExtracteToolStripButton"
        Me.ExtracteToolStripButton.Size = New System.Drawing.Size(68, 22)
        Me.ExtracteToolStripButton.Text = "Extracte"
        '
        'CobrarToolStripButton
        '
        Me.CobrarToolStripButton.Image = My.Resources.Resources.SquareArrowTurquesa
        Me.CobrarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CobrarToolStripButton.Name = "CobrarToolStripButton"
        Me.CobrarToolStripButton.Size = New System.Drawing.Size(63, 22)
        Me.CobrarToolStripButton.Text = "Cobrar"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(399, 258)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Saldo Creditor"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 25)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(572, 230)
        Me.DataGridView1.TabIndex = 12
        '
        'Frm_Contact_Pnds
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 296)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBoxTotDeutor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxTotCreditor)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_Contact_Pnds"
        Me.Text = "PARTIDES PENDENTS"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ExcelToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PagarToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents TextBoxTotDeutor As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents AfegirToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents TextBoxTotCreditor As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ExtracteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CobrarToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ZoomToolStripButton As System.Windows.Forms.ToolStripButton
End Class
