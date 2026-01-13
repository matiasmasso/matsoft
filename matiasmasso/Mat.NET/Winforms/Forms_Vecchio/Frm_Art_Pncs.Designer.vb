<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Art_Pncs
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.PictureBoxArt = New System.Windows.Forms.PictureBox
        Me.PictureBoxTpaLogo = New System.Windows.Forms.PictureBox
        Me.LabelTpa = New System.Windows.Forms.Label
        Me.LabelStp = New System.Windows.Forms.Label
        Me.LabelNomCurt = New System.Windows.Forms.Label
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxArt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxTpaLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 75)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(511, 208)
        Me.DataGridView1.TabIndex = 1
        '
        'PictureBoxArt
        '
        Me.PictureBoxArt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxArt.BackColor = System.Drawing.Color.White
        Me.PictureBoxArt.Location = New System.Drawing.Point(445, -1)
        Me.PictureBoxArt.Name = "PictureBoxArt"
        Me.PictureBoxArt.Size = New System.Drawing.Size(66, 76)
        Me.PictureBoxArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxArt.TabIndex = 5
        Me.PictureBoxArt.TabStop = False
        '
        'PictureBoxTpaLogo
        '
        Me.PictureBoxTpaLogo.BackColor = System.Drawing.Color.White
        Me.PictureBoxTpaLogo.Location = New System.Drawing.Point(0, -1)
        Me.PictureBoxTpaLogo.Name = "PictureBoxTpaLogo"
        Me.PictureBoxTpaLogo.Size = New System.Drawing.Size(170, 76)
        Me.PictureBoxTpaLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxTpaLogo.TabIndex = 7
        Me.PictureBoxTpaLogo.TabStop = False
        '
        'LabelTpa
        '
        Me.LabelTpa.AutoSize = True
        Me.LabelTpa.Location = New System.Drawing.Point(180, 13)
        Me.LabelTpa.Name = "LabelTpa"
        Me.LabelTpa.Size = New System.Drawing.Size(99, 13)
        Me.LabelTpa.TabIndex = 8
        Me.LabelTpa.Text = "[marca comercial...]"
        '
        'LabelStp
        '
        Me.LabelStp.AutoSize = True
        Me.LabelStp.Location = New System.Drawing.Point(180, 34)
        Me.LabelStp.Name = "LabelStp"
        Me.LabelStp.Size = New System.Drawing.Size(51, 13)
        Me.LabelStp.TabIndex = 9
        Me.LabelStp.Text = "[categoría...]"
        '
        'LabelNomCurt
        '
        Me.LabelNomCurt.AutoSize = True
        Me.LabelNomCurt.Location = New System.Drawing.Point(180, 55)
        Me.LabelNomCurt.Name = "LabelNomCurt"
        Me.LabelNomCurt.Size = New System.Drawing.Size(63, 13)
        Me.LabelNomCurt.TabIndex = 10
        Me.LabelNomCurt.Text = "[nom curt...]"
        '
        'Frm_Art_Pncs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(511, 284)
        Me.Controls.Add(Me.LabelNomCurt)
        Me.Controls.Add(Me.LabelStp)
        Me.Controls.Add(Me.LabelTpa)
        Me.Controls.Add(Me.PictureBoxTpaLogo)
        Me.Controls.Add(Me.PictureBoxArt)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Frm_Art_Pncs"
        Me.Text = "PENDENTS D'ENTREGA..."
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxArt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxTpaLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents PictureBoxArt As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxTpaLogo As System.Windows.Forms.PictureBox
    Friend WithEvents LabelTpa As System.Windows.Forms.Label
    Friend WithEvents LabelStp As System.Windows.Forms.Label
    Friend WithEvents LabelNomCurt As System.Windows.Forms.Label
End Class
