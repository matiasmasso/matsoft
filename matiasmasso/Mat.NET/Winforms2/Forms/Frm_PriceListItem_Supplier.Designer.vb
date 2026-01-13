<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PriceListItem_Supplier
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxEAN = New System.Windows.Forms.TextBox()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_AmtRRPP = New Xl_Amount()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_AmtCost = New Xl_Amount()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxParent = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ButtonBrowseProduct = New System.Windows.Forms.Button()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
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
        Me.Panel1.Location = New System.Drawing.Point(0, 227)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(491, 31)
        Me.Panel1.TabIndex = 58
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(272, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 14
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(383, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 13
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
        Me.ButtonDel.TabIndex = 12
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "referencia"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "codi de barres"
        '
        'TextBoxEAN
        '
        Me.TextBoxEAN.Location = New System.Drawing.Point(113, 88)
        Me.TextBoxEAN.MaxLength = 13
        Me.TextBoxEAN.Name = "TextBoxEAN"
        Me.TextBoxEAN.Size = New System.Drawing.Size(109, 20)
        Me.TextBoxEAN.TabIndex = 5
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(113, 114)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(366, 20)
        Me.TextBoxNom.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(31, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "nom"
        '
        'Xl_AmtRRPP
        '
        Me.Xl_AmtRRPP.Amt = Nothing
        Me.Xl_AmtRRPP.Location = New System.Drawing.Point(113, 166)
        Me.Xl_AmtRRPP.Name = "Xl_AmtRRPP"
        Me.Xl_AmtRRPP.Size = New System.Drawing.Size(109, 20)
        Me.Xl_AmtRRPP.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 166)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "RRPP:"
        '
        'Xl_AmtCost
        '
        Me.Xl_AmtCost.Amt = Nothing
        Me.Xl_AmtCost.Location = New System.Drawing.Point(113, 140)
        Me.Xl_AmtCost.Name = "Xl_AmtCost"
        Me.Xl_AmtCost.Size = New System.Drawing.Size(109, 20)
        Me.Xl_AmtCost.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(31, 140)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(28, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Cost"
        '
        'TextBoxParent
        '
        Me.TextBoxParent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxParent.Enabled = False
        Me.TextBoxParent.Location = New System.Drawing.Point(113, 36)
        Me.TextBoxParent.Name = "TextBoxParent"
        Me.TextBoxParent.Size = New System.Drawing.Size(366, 20)
        Me.TextBoxParent.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(31, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "tarifa"
        '
        'ButtonBrowseProduct
        '
        Me.ButtonBrowseProduct.Location = New System.Drawing.Point(228, 62)
        Me.ButtonBrowseProduct.Name = "ButtonBrowseProduct"
        Me.ButtonBrowseProduct.Size = New System.Drawing.Size(26, 20)
        Me.ButtonBrowseProduct.TabIndex = 59
        Me.ButtonBrowseProduct.Text = "..."
        Me.ButtonBrowseProduct.UseVisualStyleBackColor = True
        '
        'TextBoxRef
        '
        Me.TextBoxRef.Location = New System.Drawing.Point(113, 62)
        Me.TextBoxRef.MaxLength = 20
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.TextBoxRef.Size = New System.Drawing.Size(109, 20)
        Me.TextBoxRef.TabIndex = 3
        '
        'Frm_PriceListItem_Supplier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(491, 258)
        Me.Controls.Add(Me.ButtonBrowseProduct)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxParent)
        Me.Controls.Add(Me.Xl_AmtCost)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_AmtRRPP)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxEAN)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxRef)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_PriceListItem_Supplier"
        Me.Text = "Tarifa de proveidor"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEAN As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtRRPP As Xl_Amount
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCost As Xl_Amount
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxParent As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ButtonBrowseProduct As Button
    Friend WithEvents TextBoxRef As TextBox
End Class
