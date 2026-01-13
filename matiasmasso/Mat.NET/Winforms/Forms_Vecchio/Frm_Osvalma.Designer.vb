<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Osvalma
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
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TextBoxFra = New System.Windows.Forms.TextBox
        Me.TextBoxFch = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBoxAmt = New System.Windows.Forms.TextBox
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(14, 21)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "factura num"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxFra
        '
        Me.TextBoxFra.Location = New System.Drawing.Point(14, 47)
        Me.TextBoxFra.Name = "TextBoxFra"
        Me.TextBoxFra.ReadOnly = True
        Me.TextBoxFra.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFra.TabIndex = 1
        Me.TextBoxFra.TabStop = False
        Me.TextBoxFra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxFch
        '
        Me.TextBoxFch.Location = New System.Drawing.Point(120, 47)
        Me.TextBoxFch.Name = "TextBoxFch"
        Me.TextBoxFch.ReadOnly = True
        Me.TextBoxFch.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFch.TabIndex = 3
        Me.TextBoxFch.TabStop = False
        Me.TextBoxFch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(120, 21)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 2
        Me.TextBox3.TabStop = False
        Me.TextBox3.Text = "data"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxAmt
        '
        Me.TextBoxAmt.Location = New System.Drawing.Point(226, 47)
        Me.TextBoxAmt.Name = "TextBoxAmt"
        Me.TextBoxAmt.ReadOnly = True
        Me.TextBoxAmt.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxAmt.TabIndex = 5
        Me.TextBoxAmt.TabStop = False
        Me.TextBoxAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(226, 21)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(100, 20)
        Me.TextBox5.TabIndex = 4
        Me.TextBox5.TabStop = False
        Me.TextBox5.Text = "import"
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLogo.Location = New System.Drawing.Point(419, 21)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogo.TabIndex = 6
        Me.PictureBoxLogo.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(3, 95)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(570, 344)
        Me.DataGridView1.TabIndex = 7
        '
        'Frm_Osvalma
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 492)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.TextBoxAmt)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.TextBoxFch)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBoxFra)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "Frm_Osvalma"
        Me.Text = "FACTURA DE OSVALMA"
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxFra As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxFch As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAmt As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
