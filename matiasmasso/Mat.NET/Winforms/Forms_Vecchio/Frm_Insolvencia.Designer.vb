<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Insolvencia
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.TextBoxCli = New System.Windows.Forms.TextBox
        Me.DateTimePickerPresentacio = New System.Windows.Forms.DateTimePicker
        Me.CheckBoxPresentacio = New System.Windows.Forms.CheckBox
        Me.CheckBoxAdmisio = New System.Windows.Forms.CheckBox
        Me.DateTimePickerAdmisio = New System.Windows.Forms.DateTimePicker
        Me.CheckBoxLiquidacio = New System.Windows.Forms.CheckBox
        Me.DateTimePickerLiquidacio = New System.Windows.Forms.DateTimePicker
        Me.CheckBoxRehabilitacio = New System.Windows.Forms.CheckBox
        Me.DateTimePickerRehabilitacio = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Xl_AmtNominal = New Xl_Amount
        Me.Xl_AmtGastos = New Xl_Amount
        Me.Xl_AmtComisio = New Xl_Amount
        Me.Xl_AmtPagatACompte = New Xl_Amount
        Me.Label4 = New System.Windows.Forms.Label
        Me.Xl_AmtDeute = New Xl_Amount
        Me.Label5 = New System.Windows.Forms.Label
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 336)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(516, 119)
        Me.DataGridView1.TabIndex = 19
        Me.DataGridView1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 461)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(516, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(297, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 22
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(408, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 21
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
        Me.ButtonDel.TabIndex = 20
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxCli
        '
        Me.TextBoxCli.Location = New System.Drawing.Point(12, 12)
        Me.TextBoxCli.Name = "TextBoxCli"
        Me.TextBoxCli.Size = New System.Drawing.Size(492, 20)
        Me.TextBoxCli.TabIndex = 0
        Me.TextBoxCli.TabStop = False
        '
        'DateTimePickerPresentacio
        '
        Me.DateTimePickerPresentacio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerPresentacio.Location = New System.Drawing.Point(194, 204)
        Me.DateTimePickerPresentacio.Name = "DateTimePickerPresentacio"
        Me.DateTimePickerPresentacio.Size = New System.Drawing.Size(102, 20)
        Me.DateTimePickerPresentacio.TabIndex = 12
        '
        'CheckBoxPresentacio
        '
        Me.CheckBoxPresentacio.AutoSize = True
        Me.CheckBoxPresentacio.Checked = True
        Me.CheckBoxPresentacio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxPresentacio.Location = New System.Drawing.Point(72, 209)
        Me.CheckBoxPresentacio.Name = "CheckBoxPresentacio"
        Me.CheckBoxPresentacio.Size = New System.Drawing.Size(82, 17)
        Me.CheckBoxPresentacio.TabIndex = 11
        Me.CheckBoxPresentacio.Text = "Presentació"
        Me.CheckBoxPresentacio.UseVisualStyleBackColor = True
        '
        'CheckBoxAdmisio
        '
        Me.CheckBoxAdmisio.AutoSize = True
        Me.CheckBoxAdmisio.Location = New System.Drawing.Point(72, 232)
        Me.CheckBoxAdmisio.Name = "CheckBoxAdmisio"
        Me.CheckBoxAdmisio.Size = New System.Drawing.Size(62, 17)
        Me.CheckBoxAdmisio.TabIndex = 13
        Me.CheckBoxAdmisio.Text = "Admisió"
        Me.CheckBoxAdmisio.UseVisualStyleBackColor = True
        '
        'DateTimePickerAdmisio
        '
        Me.DateTimePickerAdmisio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAdmisio.Location = New System.Drawing.Point(194, 227)
        Me.DateTimePickerAdmisio.Name = "DateTimePickerAdmisio"
        Me.DateTimePickerAdmisio.Size = New System.Drawing.Size(102, 20)
        Me.DateTimePickerAdmisio.TabIndex = 14
        '
        'CheckBoxLiquidacio
        '
        Me.CheckBoxLiquidacio.AutoSize = True
        Me.CheckBoxLiquidacio.Location = New System.Drawing.Point(72, 255)
        Me.CheckBoxLiquidacio.Name = "CheckBoxLiquidacio"
        Me.CheckBoxLiquidacio.Size = New System.Drawing.Size(119, 17)
        Me.CheckBoxLiquidacio.TabIndex = 15
        Me.CheckBoxLiquidacio.Text = "Liquidació definitiva"
        Me.CheckBoxLiquidacio.UseVisualStyleBackColor = True
        '
        'DateTimePickerLiquidacio
        '
        Me.DateTimePickerLiquidacio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerLiquidacio.Location = New System.Drawing.Point(194, 250)
        Me.DateTimePickerLiquidacio.Name = "DateTimePickerLiquidacio"
        Me.DateTimePickerLiquidacio.Size = New System.Drawing.Size(102, 20)
        Me.DateTimePickerLiquidacio.TabIndex = 16
        '
        'CheckBoxRehabilitacio
        '
        Me.CheckBoxRehabilitacio.AutoSize = True
        Me.CheckBoxRehabilitacio.Location = New System.Drawing.Point(72, 278)
        Me.CheckBoxRehabilitacio.Name = "CheckBoxRehabilitacio"
        Me.CheckBoxRehabilitacio.Size = New System.Drawing.Size(87, 17)
        Me.CheckBoxRehabilitacio.TabIndex = 17
        Me.CheckBoxRehabilitacio.Text = "Rehabilitació"
        Me.CheckBoxRehabilitacio.UseVisualStyleBackColor = True
        '
        'DateTimePickerRehabilitacio
        '
        Me.DateTimePickerRehabilitacio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerRehabilitacio.Location = New System.Drawing.Point(194, 273)
        Me.DateTimePickerRehabilitacio.Name = "DateTimePickerRehabilitacio"
        Me.DateTimePickerRehabilitacio.Size = New System.Drawing.Size(102, 20)
        Me.DateTimePickerRehabilitacio.TabIndex = 18
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(67, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "nominal"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(64, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "despeses"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(64, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "comisió"
        '
        'Xl_AmtNominal
        '
        Me.Xl_AmtNominal.Amt = Nothing
        Me.Xl_AmtNominal.Location = New System.Drawing.Point(146, 39)
        Me.Xl_AmtNominal.Name = "Xl_AmtNominal"
        Me.Xl_AmtNominal.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmtNominal.TabIndex = 2
        '
        'Xl_AmtGastos
        '
        Me.Xl_AmtGastos.Amt = Nothing
        Me.Xl_AmtGastos.Location = New System.Drawing.Point(146, 65)
        Me.Xl_AmtGastos.Name = "Xl_AmtGastos"
        Me.Xl_AmtGastos.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmtGastos.TabIndex = 4
        '
        'Xl_AmtComisio
        '
        Me.Xl_AmtComisio.Amt = Nothing
        Me.Xl_AmtComisio.Location = New System.Drawing.Point(146, 117)
        Me.Xl_AmtComisio.Name = "Xl_AmtComisio"
        Me.Xl_AmtComisio.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmtComisio.TabIndex = 8
        '
        'Xl_AmtPagatACompte
        '
        Me.Xl_AmtPagatACompte.Amt = Nothing
        Me.Xl_AmtPagatACompte.Location = New System.Drawing.Point(146, 91)
        Me.Xl_AmtPagatACompte.Name = "Xl_AmtPagatACompte"
        Me.Xl_AmtPagatACompte.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmtPagatACompte.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(64, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "anticipades"
        '
        'Xl_AmtDeute
        '
        Me.Xl_AmtDeute.Amt = Nothing
        Me.Xl_AmtDeute.Location = New System.Drawing.Point(146, 143)
        Me.Xl_AmtDeute.Name = "Xl_AmtDeute"
        Me.Xl_AmtDeute.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmtDeute.TabIndex = 10
        Me.Xl_AmtDeute.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(64, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "deute"
        '
        'Frm_Insolvencia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(516, 492)
        Me.Controls.Add(Me.Xl_AmtDeute)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_AmtPagatACompte)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_AmtComisio)
        Me.Controls.Add(Me.Xl_AmtGastos)
        Me.Controls.Add(Me.Xl_AmtNominal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CheckBoxRehabilitacio)
        Me.Controls.Add(Me.DateTimePickerRehabilitacio)
        Me.Controls.Add(Me.CheckBoxLiquidacio)
        Me.Controls.Add(Me.DateTimePickerLiquidacio)
        Me.Controls.Add(Me.CheckBoxAdmisio)
        Me.Controls.Add(Me.DateTimePickerAdmisio)
        Me.Controls.Add(Me.CheckBoxPresentacio)
        Me.Controls.Add(Me.DateTimePickerPresentacio)
        Me.Controls.Add(Me.TextBoxCli)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Frm_Insolvencia"
        Me.Text = "INSOLVENCIA"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxCli As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePickerPresentacio As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxPresentacio As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxAdmisio As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerAdmisio As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxLiquidacio As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerLiquidacio As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxRehabilitacio As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerRehabilitacio As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtNominal As Xl_Amount
    Friend WithEvents Xl_AmtGastos As Xl_Amount
    Friend WithEvents Xl_AmtComisio As Xl_Amount
    Friend WithEvents Xl_AmtPagatACompte As Xl_Amount
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtDeute As Xl_Amount
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
