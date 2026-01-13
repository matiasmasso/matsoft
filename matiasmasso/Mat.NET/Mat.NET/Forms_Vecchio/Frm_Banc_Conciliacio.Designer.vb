<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Banc_Conciliacio
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
        Me.Xl_BigFile1 = New Xl_Bigfile
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.Xl_AmtSSaldo = New Xl_Amount
        Me.LabelSaldo = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxNSdo = New System.Windows.Forms.TextBox
        Me.TextBoxCcaAnteriors = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.TextBoxCcaPosteriors = New System.Windows.Forms.TextBox
        Me.TextBoxResultat = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.PictureBoxResult = New System.Windows.Forms.PictureBox
        Me.Xl_IBAN1 = New Xl_Iban
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_BigFile1
        '
        Me.Xl_BigFile1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BigFile1.BigFile = Nothing
        Me.Xl_BigFile1.Location = New System.Drawing.Point(439, 12)
        Me.Xl_BigFile1.Name = "Xl_BigFile1"
        Me.Xl_BigFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_BigFile1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 440)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(793, 31)
        Me.Panel1.TabIndex = 45
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(574, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(685, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Xl_AmtSSaldo
        '
        Me.Xl_AmtSSaldo.Amt = Nothing
        Me.Xl_AmtSSaldo.Location = New System.Drawing.Point(333, 104)
        Me.Xl_AmtSSaldo.Name = "Xl_AmtSSaldo"
        Me.Xl_AmtSSaldo.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmtSSaldo.TabIndex = 46
        '
        'LabelSaldo
        '
        Me.LabelSaldo.AutoSize = True
        Me.LabelSaldo.Location = New System.Drawing.Point(12, 104)
        Me.LabelSaldo.Name = "LabelSaldo"
        Me.LabelSaldo.Size = New System.Drawing.Size(139, 13)
        Me.LabelSaldo.TabIndex = 47
        Me.LabelSaldo.Text = "Saldo del extracte del banc "
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 282)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(421, 150)
        Me.DataGridView1.TabIndex = 48
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Location = New System.Drawing.Point(15, 158)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(279, 34)
        Me.TextBox1.TabIndex = 50
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "mes apunts anteriors  que encara no figuren al extracte del banc en aquesta data " & _
            ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 130)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Saldo segons llibres"
        '
        'TextBoxNSdo
        '
        Me.TextBoxNSdo.Location = New System.Drawing.Point(333, 130)
        Me.TextBoxNSdo.Name = "TextBoxNSdo"
        Me.TextBoxNSdo.ReadOnly = True
        Me.TextBoxNSdo.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxNSdo.TabIndex = 52
        Me.TextBoxNSdo.TabStop = False
        Me.TextBoxNSdo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxCcaAnteriors
        '
        Me.TextBoxCcaAnteriors.Location = New System.Drawing.Point(333, 158)
        Me.TextBoxCcaAnteriors.Name = "TextBoxCcaAnteriors"
        Me.TextBoxCcaAnteriors.ReadOnly = True
        Me.TextBoxCcaAnteriors.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxCcaAnteriors.TabIndex = 54
        Me.TextBoxCcaAnteriors.TabStop = False
        Me.TextBoxCcaAnteriors.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.Location = New System.Drawing.Point(15, 198)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(279, 34)
        Me.TextBox3.TabIndex = 55
        Me.TextBox3.TabStop = False
        Me.TextBox3.Text = "menys apunts posteriors  que figuren al extracte del banc abans d'aquesta data "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "data de tancament"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(333, 79)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePicker1.TabIndex = 57
        '
        'TextBoxCcaPosteriors
        '
        Me.TextBoxCcaPosteriors.Location = New System.Drawing.Point(333, 195)
        Me.TextBoxCcaPosteriors.Name = "TextBoxCcaPosteriors"
        Me.TextBoxCcaPosteriors.ReadOnly = True
        Me.TextBoxCcaPosteriors.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxCcaPosteriors.TabIndex = 58
        Me.TextBoxCcaPosteriors.TabStop = False
        Me.TextBoxCcaPosteriors.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxResultat
        '
        Me.TextBoxResultat.Location = New System.Drawing.Point(333, 235)
        Me.TextBoxResultat.Name = "TextBoxResultat"
        Me.TextBoxResultat.ReadOnly = True
        Me.TextBoxResultat.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxResultat.TabIndex = 60
        Me.TextBoxResultat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(229, 239)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "diferencia"
        '
        'PictureBoxResult
        '
        Me.PictureBoxResult.ErrorImage = My.Resources.Resources.warn
        Me.PictureBoxResult.Image = My.Resources.Resources.warn
        Me.PictureBoxResult.Location = New System.Drawing.Point(311, 239)
        Me.PictureBoxResult.Name = "PictureBoxResult"
        Me.PictureBoxResult.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxResult.TabIndex = 61
        Me.PictureBoxResult.TabStop = False
        '
        'Xl_IBAN1
        '
        Me.Xl_IBAN1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_IBAN1.Location = New System.Drawing.Point(12, 13)
        Me.Xl_IBAN1.Name = "Xl_IBAN1"
        Me.Xl_IBAN1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_IBAN1.TabIndex = 62
        '
        'Frm_Banc_Conciliacio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(793, 471)
        Me.Controls.Add(Me.Xl_IBAN1)
        Me.Controls.Add(Me.PictureBoxResult)
        Me.Controls.Add(Me.TextBoxResultat)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxCcaPosteriors)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBoxCcaAnteriors)
        Me.Controls.Add(Me.TextBoxNSdo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelSaldo)
        Me.Controls.Add(Me.Xl_AmtSSaldo)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_BigFile1)
        Me.Name = "Frm_Banc_Conciliacio"
        Me.Text = "CONCILIACIO BANCARIA"
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_BigFile1 As Xl_Bigfile
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_AmtSSaldo As Xl_Amount
    Friend WithEvents LabelSaldo As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNSdo As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxCcaAnteriors As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxCcaPosteriors As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxResultat As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxResult As System.Windows.Forms.PictureBox
    Friend WithEvents Xl_IBAN1 As Xl_Iban
End Class
