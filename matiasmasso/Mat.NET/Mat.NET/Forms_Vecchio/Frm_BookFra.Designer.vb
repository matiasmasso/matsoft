<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BookFra
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxFranum = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Contact1 = New Mat.NET.Xl_Contact()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Cta1 = New Mat.NET.Xl_Cta()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxCca = New System.Windows.Forms.TextBox()
        Me.Xl_AmtBase = New Mat.Net.Xl_Amount()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_AmtIVA = New Mat.Net.Xl_Amount()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_AmtIRPF = New Mat.Net.Xl_Amount()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_AmtLiq = New Mat.Net.Xl_Amount()
        Me.LabelIVA = New System.Windows.Forms.Label()
        Me.LabelIrpf = New System.Windows.Forms.Label()
        Me.Xl_DocFile1 = New Mat.NET.Xl_DocFile()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 444)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(829, 31)
        Me.Panel1.TabIndex = 52
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(610, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(721, 4)
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
        'TextBoxFranum
        '
        Me.TextBoxFranum.Location = New System.Drawing.Point(129, 144)
        Me.TextBoxFranum.MaxLength = 15
        Me.TextBoxFranum.Name = "TextBoxFranum"
        Me.TextBoxFranum.Size = New System.Drawing.Size(134, 20)
        Me.TextBoxFranum.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 147)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "factura numero:"
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(129, 93)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.Size = New System.Drawing.Size(330, 20)
        Me.Xl_Contact1.TabIndex = 53
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "proveidor:"
        '
        'Xl_Cta1
        '
        Me.Xl_Cta1.Cta = Nothing
        Me.Xl_Cta1.Location = New System.Drawing.Point(129, 118)
        Me.Xl_Cta1.Name = "Xl_Cta1"
        Me.Xl_Cta1.Size = New System.Drawing.Size(330, 20)
        Me.Xl_Cta1.TabIndex = 55
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "compte:"
        '
        'TextBoxCca
        '
        Me.TextBoxCca.Location = New System.Drawing.Point(22, 12)
        Me.TextBoxCca.MaxLength = 15
        Me.TextBoxCca.Multiline = True
        Me.TextBoxCca.Name = "TextBoxCca"
        Me.TextBoxCca.ReadOnly = True
        Me.TextBoxCca.Size = New System.Drawing.Size(437, 75)
        Me.TextBoxCca.TabIndex = 57
        '
        'Xl_AmtBase
        '
        Me.Xl_AmtBase.Amt = Nothing
        Me.Xl_AmtBase.Location = New System.Drawing.Point(129, 171)
        Me.Xl_AmtBase.Name = "Xl_AmtBase"
        Me.Xl_AmtBase.Size = New System.Drawing.Size(134, 20)
        Me.Xl_AmtBase.TabIndex = 58
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 174)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "base imponible:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 200)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 13)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "IVA"
        '
        'Xl_AmtIVA
        '
        Me.Xl_AmtIVA.Amt = Nothing
        Me.Xl_AmtIVA.Location = New System.Drawing.Point(129, 197)
        Me.Xl_AmtIVA.Name = "Xl_AmtIVA"
        Me.Xl_AmtIVA.Size = New System.Drawing.Size(134, 20)
        Me.Xl_AmtIVA.TabIndex = 60
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 226)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 63
        Me.Label6.Text = "IRPF"
        '
        'Xl_AmtIRPF
        '
        Me.Xl_AmtIRPF.Amt = Nothing
        Me.Xl_AmtIRPF.Location = New System.Drawing.Point(129, 223)
        Me.Xl_AmtIRPF.Name = "Xl_AmtIRPF"
        Me.Xl_AmtIRPF.Size = New System.Drawing.Size(134, 20)
        Me.Xl_AmtIRPF.TabIndex = 62
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 252)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 13)
        Me.Label7.TabIndex = 65
        Me.Label7.Text = "liquid"
        '
        'Xl_AmtLiq
        '
        Me.Xl_AmtLiq.Amt = Nothing
        Me.Xl_AmtLiq.Enabled = False
        Me.Xl_AmtLiq.Location = New System.Drawing.Point(129, 249)
        Me.Xl_AmtLiq.Name = "Xl_AmtLiq"
        Me.Xl_AmtLiq.Size = New System.Drawing.Size(134, 20)
        Me.Xl_AmtLiq.TabIndex = 64
        '
        'LabelIVA
        '
        Me.LabelIVA.AutoSize = True
        Me.LabelIVA.Location = New System.Drawing.Point(280, 200)
        Me.LabelIVA.Name = "LabelIVA"
        Me.LabelIVA.Size = New System.Drawing.Size(15, 13)
        Me.LabelIVA.TabIndex = 66
        Me.LabelIVA.Text = "%"
        Me.LabelIVA.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelIrpf
        '
        Me.LabelIrpf.AutoSize = True
        Me.LabelIrpf.Location = New System.Drawing.Point(280, 226)
        Me.LabelIrpf.Name = "LabelIrpf"
        Me.LabelIrpf.Size = New System.Drawing.Size(15, 13)
        Me.LabelIrpf.TabIndex = 67
        Me.LabelIrpf.Text = "%"
        Me.LabelIrpf.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(475, 12)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 68
        '
        'Frm_BookFra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(829, 475)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.LabelIrpf)
        Me.Controls.Add(Me.LabelIVA)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Xl_AmtLiq)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_AmtIRPF)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_AmtIVA)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_AmtBase)
        Me.Controls.Add(Me.TextBoxCca)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_Cta1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxFranum)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_BookFra"
        Me.Text = "Registre de llibre de factures rebudes"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxFranum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact1 As Xl_Contact
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_Cta1 As Xl_Cta
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCca As System.Windows.Forms.TextBox
    Friend WithEvents Xl_AmtBase As Xl_Amount
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtIVA As Xl_Amount
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtIRPF As Xl_Amount
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtLiq As Xl_Amount
    Friend WithEvents LabelIVA As System.Windows.Forms.Label
    Friend WithEvents LabelIrpf As System.Windows.Forms.Label
    Friend WithEvents Xl_DocFile1 As Mat.NET.Xl_DocFile
End Class
