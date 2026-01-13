<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_BancTransferBeneficiari
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DtoAmt1 As DTOAmt = New DTOAmt()
        Me.TextBoxConcept = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_Contact21 = New Xl_Contact2()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_Eur1 = New Xl_Eur()
        Me.Xl_IbanDigits1 = New Xl_IbanDigits()
        Me.Xl_LookupPgcCta1 = New Xl_LookupPgcCta()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxConcept
        '
        Me.TextBoxConcept.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxConcept.Location = New System.Drawing.Point(77, 101)
        Me.TextBoxConcept.MaxLength = 60
        Me.TextBoxConcept.Name = "TextBoxConcept"
        Me.TextBoxConcept.Size = New System.Drawing.Size(299, 20)
        Me.TextBoxConcept.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Concepte"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 281)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(388, 31)
        Me.Panel1.TabIndex = 44
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(169, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(280, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
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
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(77, 49)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(299, 20)
        Me.Xl_Contact21.TabIndex = 47
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Beneficiari"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Import"
        '
        'Xl_Eur1
        '
        DtoAmt1.Cur = Nothing
        DtoAmt1.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt1.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_Eur1.Amt = DtoAmt1
        Me.Xl_Eur1.Location = New System.Drawing.Point(77, 128)
        Me.Xl_Eur1.Name = "Xl_Eur1"
        Me.Xl_Eur1.Size = New System.Drawing.Size(100, 20)
        Me.Xl_Eur1.TabIndex = 50
        Me.Xl_Eur1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Xl_IbanDigits1
        '
        Me.Xl_IbanDigits1.Location = New System.Drawing.Point(77, 155)
        Me.Xl_IbanDigits1.Name = "Xl_IbanDigits1"
        Me.Xl_IbanDigits1.Size = New System.Drawing.Size(251, 71)
        Me.Xl_IbanDigits1.TabIndex = 51
        '
        'Xl_LookupPgcCta1
        '
        Me.Xl_LookupPgcCta1.IsDirty = False
        Me.Xl_LookupPgcCta1.Location = New System.Drawing.Point(77, 75)
        Me.Xl_LookupPgcCta1.Name = "Xl_LookupPgcCta1"
        Me.Xl_LookupPgcCta1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPgcCta1.PgcCta = Nothing
        Me.Xl_LookupPgcCta1.Size = New System.Drawing.Size(299, 20)
        Me.Xl_LookupPgcCta1.TabIndex = 52
        Me.Xl_LookupPgcCta1.Value = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Compte"
        '
        'Frm_BancTransferItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 312)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_LookupPgcCta1)
        Me.Controls.Add(Me.Xl_IbanDigits1)
        Me.Controls.Add(Me.Xl_Eur1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.TextBoxConcept)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_BancTransferItem"
        Me.Text = "Beneficiari de Transferencia"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxConcept As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_Eur1 As Xl_Eur
    Friend WithEvents Xl_IbanDigits1 As Xl_IbanDigits
    Friend WithEvents Xl_LookupPgcCta1 As Xl_LookupPgcCta
    Friend WithEvents Label4 As Label
End Class
