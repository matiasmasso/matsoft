<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PrTarifa
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox
        Me.LabelYea = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LabelPageSize = New System.Windows.Forms.Label
        Me.Xl_AmtTarifa = New Xl_Amount
        Me.Xl_PercentDto = New Xl_Percent
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Xl_PercentDt2 = New Xl_Percent
        Me.Xl_AmtNet = New Xl_Amount
        Me.Label4 = New System.Windows.Forms.Label
        Me.Xl_AmtLiquid = New Xl_Amount
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 240)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(283, 31)
        Me.Panel1.TabIndex = 46
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(64, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(175, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Location = New System.Drawing.Point(122, 3)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogo.TabIndex = 47
        Me.PictureBoxLogo.TabStop = False
        '
        'LabelYea
        '
        Me.LabelYea.AutoSize = True
        Me.LabelYea.Location = New System.Drawing.Point(12, 9)
        Me.LabelYea.Name = "LabelYea"
        Me.LabelYea.Size = New System.Drawing.Size(47, 13)
        Me.LabelYea.TabIndex = 48
        Me.LabelYea.Text = "tarifas ..."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(46, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "preu de tarifa"
        '
        'LabelPageSize
        '
        Me.LabelPageSize.AutoSize = True
        Me.LabelPageSize.Location = New System.Drawing.Point(12, 38)
        Me.LabelPageSize.Name = "LabelPageSize"
        Me.LabelPageSize.Size = New System.Drawing.Size(81, 13)
        Me.LabelPageSize.TabIndex = 50
        Me.LabelPageSize.Text = "mida de pagima"
        '
        'Xl_AmtTarifa
        '
        Me.Xl_AmtTarifa.Amt = Nothing
        Me.Xl_AmtTarifa.Location = New System.Drawing.Point(143, 83)
        Me.Xl_AmtTarifa.Name = "Xl_AmtTarifa"
        Me.Xl_AmtTarifa.Size = New System.Drawing.Size(129, 20)
        Me.Xl_AmtTarifa.TabIndex = 51
        '
        'Xl_PercentDto
        '
        Me.Xl_PercentDto.Location = New System.Drawing.Point(212, 110)
        Me.Xl_PercentDto.Name = "Xl_PercentDto"
        Me.Xl_PercentDto.Size = New System.Drawing.Size(60, 20)
        Me.Xl_PercentDto.TabIndex = 52
        Me.Xl_PercentDto.Value = 0.0!
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(46, 117)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "descompte de client"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(46, 170)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(108, 13)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "descompte d'agencia"
        '
        'Xl_PercentDt2
        '
        Me.Xl_PercentDt2.Location = New System.Drawing.Point(212, 163)
        Me.Xl_PercentDt2.Name = "Xl_PercentDt2"
        Me.Xl_PercentDt2.Size = New System.Drawing.Size(60, 20)
        Me.Xl_PercentDt2.TabIndex = 56
        Me.Xl_PercentDt2.Value = 0.0!
        '
        'Xl_AmtNet
        '
        Me.Xl_AmtNet.Amt = Nothing
        Me.Xl_AmtNet.Location = New System.Drawing.Point(143, 136)
        Me.Xl_AmtNet.Name = "Xl_AmtNet"
        Me.Xl_AmtNet.Size = New System.Drawing.Size(129, 20)
        Me.Xl_AmtNet.TabIndex = 55
        Me.Xl_AmtNet.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(46, 143)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "preu net"
        '
        'Xl_AmtLiquid
        '
        Me.Xl_AmtLiquid.Amt = Nothing
        Me.Xl_AmtLiquid.Location = New System.Drawing.Point(143, 189)
        Me.Xl_AmtLiquid.Name = "Xl_AmtLiquid"
        Me.Xl_AmtLiquid.Size = New System.Drawing.Size(129, 20)
        Me.Xl_AmtLiquid.TabIndex = 59
        Me.Xl_AmtLiquid.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(46, 196)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 58
        Me.Label5.Text = "preu a pagar"
        '
        'Frm_PrTarifa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(283, 271)
        Me.Controls.Add(Me.Xl_AmtLiquid)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_PercentDt2)
        Me.Controls.Add(Me.Xl_AmtNet)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_PercentDto)
        Me.Controls.Add(Me.Xl_AmtTarifa)
        Me.Controls.Add(Me.LabelPageSize)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelYea)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_PrTarifa"
        Me.Text = "TARIFA"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents PictureBoxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents LabelYea As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelPageSize As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtTarifa As Xl_Amount
    Friend WithEvents Xl_PercentDto As Xl_Percent
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_PercentDt2 As Xl_Percent
    Friend WithEvents Xl_AmtNet As Xl_Amount
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtLiquid As Xl_Amount
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
