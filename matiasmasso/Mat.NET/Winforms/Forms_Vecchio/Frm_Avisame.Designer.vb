<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Avisame
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
        Me.TextBoxArt = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ButtonAdd = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxEmail = New System.Windows.Forms.TextBox
        Me.CheckBoxHideOlds = New System.Windows.Forms.CheckBox
        Me.TextBoxObs = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.PictureBoxArt = New System.Windows.Forms.PictureBox
        Me.Label3 = New System.Windows.Forms.Label
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBoxArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxArt
        '
        Me.TextBoxArt.Location = New System.Drawing.Point(87, 31)
        Me.TextBoxArt.Name = "TextBoxArt"
        Me.TextBoxArt.Size = New System.Drawing.Size(363, 20)
        Me.TextBoxArt.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "producte:"
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Location = New System.Drawing.Point(12, 12)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(457, 34)
        Me.TextBox2.TabIndex = 0
        Me.TextBox2.TabStop = False
        Me.TextBox2.Text = "Avisar a la següent adreça de correu quan els següents productes estiguin disponi" & _
            "ble en stock per entrega inmediata:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 254)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(578, 251)
        Me.DataGridView1.TabIndex = 10
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Enabled = False
        Me.ButtonAdd.Image = My.Resources.Resources.clip
        Me.ButtonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonAdd.Location = New System.Drawing.Point(373, 119)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(77, 28)
        Me.ButtonAdd.TabIndex = 9
        Me.ButtonAdd.Text = "AFEGIR"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "e-mail:"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Location = New System.Drawing.Point(56, 52)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.ReadOnly = True
        Me.TextBoxEmail.Size = New System.Drawing.Size(406, 20)
        Me.TextBoxEmail.TabIndex = 2
        Me.TextBoxEmail.TabStop = False
        '
        'CheckBoxHideOlds
        '
        Me.CheckBoxHideOlds.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxHideOlds.AutoSize = True
        Me.CheckBoxHideOlds.Checked = True
        Me.CheckBoxHideOlds.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxHideOlds.Location = New System.Drawing.Point(12, 511)
        Me.CheckBoxHideOlds.Name = "CheckBoxHideOlds"
        Me.CheckBoxHideOlds.Size = New System.Drawing.Size(120, 17)
        Me.CheckBoxHideOlds.TabIndex = 11
        Me.CheckBoxHideOlds.Text = "exclou els ja avisats"
        Me.CheckBoxHideOlds.UseVisualStyleBackColor = True
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Location = New System.Drawing.Point(87, 57)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(363, 55)
        Me.TextBoxObs.TabIndex = 7
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PictureBoxArt)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBoxObs)
        Me.GroupBox1.Controls.Add(Me.TextBoxArt)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ButtonAdd)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 95)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(586, 153)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'PictureBoxArt
        '
        Me.PictureBoxArt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxArt.Location = New System.Drawing.Point(460, 16)
        Me.PictureBoxArt.Name = "PictureBoxArt"
        Me.PictureBoxArt.Size = New System.Drawing.Size(119, 135)
        Me.PictureBoxArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxArt.TabIndex = 11
        Me.PictureBoxArt.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "s/referencia:"
        '
        'Frm_Avisame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(602, 530)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CheckBoxHideOlds)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxEmail)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBox2)
        Me.Name = "Frm_Avisame"
        Me.Text = "AVISAME"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBoxArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxArt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxHideOlds As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxArt As System.Windows.Forms.PictureBox
End Class
