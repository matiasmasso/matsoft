<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Last_Incidencies
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
        Me.ComboBoxClose = New System.Windows.Forms.ComboBox()
        Me.ButtonExcel = New System.Windows.Forms.Button()
        Me.Xl_Incidencies1 = New Mat.NET.Xl_Incidencies()
        Me.CheckBoxSrcProducte = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSrcTransport = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupProduct1 = New Mat.NET.Xl_LookupProduct()
        Me.CheckBoxCustomer = New System.Windows.Forms.CheckBox()
        Me.CheckBoxProduct = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIncludeClosed = New System.Windows.Forms.CheckBox()
        Me.Xl_Contact21 = New Mat.NET.Xl_Contact2()
        Me.SuspendLayout()
        '
        'ComboBoxClose
        '
        Me.ComboBoxClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxClose.Enabled = False
        Me.ComboBoxClose.FormattingEnabled = True
        Me.ComboBoxClose.Location = New System.Drawing.Point(633, 27)
        Me.ComboBoxClose.Name = "ComboBoxClose"
        Me.ComboBoxClose.Size = New System.Drawing.Size(437, 21)
        Me.ComboBoxClose.TabIndex = 3
        '
        'ButtonExcel
        '
        Me.ButtonExcel.Image = Global.Mat.NET.My.Resources.Resources.Excel
        Me.ButtonExcel.Location = New System.Drawing.Point(0, 5)
        Me.ButtonExcel.Name = "ButtonExcel"
        Me.ButtonExcel.Size = New System.Drawing.Size(24, 24)
        Me.ButtonExcel.TabIndex = 4
        Me.ButtonExcel.UseVisualStyleBackColor = True
        '
        'Xl_Incidencies1
        '
        Me.Xl_Incidencies1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Incidencies1.Location = New System.Drawing.Point(0, 50)
        Me.Xl_Incidencies1.Name = "Xl_Incidencies1"
        Me.Xl_Incidencies1.Size = New System.Drawing.Size(1070, 436)
        Me.Xl_Incidencies1.TabIndex = 5
        '
        'CheckBoxSrcProducte
        '
        Me.CheckBoxSrcProducte.AutoSize = True
        Me.CheckBoxSrcProducte.Location = New System.Drawing.Point(41, 8)
        Me.CheckBoxSrcProducte.Name = "CheckBoxSrcProducte"
        Me.CheckBoxSrcProducte.Size = New System.Drawing.Size(83, 17)
        Me.CheckBoxSrcProducte.TabIndex = 6
        Me.CheckBoxSrcProducte.Text = "de producte"
        Me.CheckBoxSrcProducte.UseVisualStyleBackColor = True
        '
        'CheckBoxSrcTransport
        '
        Me.CheckBoxSrcTransport.AutoSize = True
        Me.CheckBoxSrcTransport.Location = New System.Drawing.Point(41, 27)
        Me.CheckBoxSrcTransport.Name = "CheckBoxSrcTransport"
        Me.CheckBoxSrcTransport.Size = New System.Drawing.Size(82, 17)
        Me.CheckBoxSrcTransport.TabIndex = 7
        Me.CheckBoxSrcTransport.Text = "de transport"
        Me.CheckBoxSrcTransport.UseVisualStyleBackColor = True
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(260, 5)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(326, 20)
        Me.Xl_LookupProduct1.TabIndex = 8
        Me.Xl_LookupProduct1.Value = Nothing
        Me.Xl_LookupProduct1.Visible = False
        '
        'CheckBoxCustomer
        '
        Me.CheckBoxCustomer.AutoSize = True
        Me.CheckBoxCustomer.Location = New System.Drawing.Point(149, 28)
        Me.CheckBoxCustomer.Name = "CheckBoxCustomer"
        Me.CheckBoxCustomer.Size = New System.Drawing.Size(94, 17)
        Me.CheckBoxCustomer.TabIndex = 12
        Me.CheckBoxCustomer.Text = "filtrar per client"
        Me.CheckBoxCustomer.UseVisualStyleBackColor = True
        '
        'CheckBoxProduct
        '
        Me.CheckBoxProduct.AutoSize = True
        Me.CheckBoxProduct.Location = New System.Drawing.Point(149, 8)
        Me.CheckBoxProduct.Name = "CheckBoxProduct"
        Me.CheckBoxProduct.Size = New System.Drawing.Size(111, 17)
        Me.CheckBoxProduct.TabIndex = 13
        Me.CheckBoxProduct.Text = "filtrar per producte"
        Me.CheckBoxProduct.UseVisualStyleBackColor = True
        '
        'CheckBoxIncludeClosed
        '
        Me.CheckBoxIncludeClosed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxIncludeClosed.AutoSize = True
        Me.CheckBoxIncludeClosed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxIncludeClosed.Location = New System.Drawing.Point(886, 8)
        Me.CheckBoxIncludeClosed.Name = "CheckBoxIncludeClosed"
        Me.CheckBoxIncludeClosed.Size = New System.Drawing.Size(184, 17)
        Me.CheckBoxIncludeClosed.TabIndex = 14
        Me.CheckBoxIncludeClosed.Text = "inclou les incidencies ja tancades"
        Me.CheckBoxIncludeClosed.UseVisualStyleBackColor = True
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(260, 27)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(326, 20)
        Me.Xl_Contact21.TabIndex = 15
        '
        'Frm_Last_Incidencies
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1070, 487)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.CheckBoxIncludeClosed)
        Me.Controls.Add(Me.CheckBoxProduct)
        Me.Controls.Add(Me.CheckBoxCustomer)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.CheckBoxSrcTransport)
        Me.Controls.Add(Me.CheckBoxSrcProducte)
        Me.Controls.Add(Me.Xl_Incidencies1)
        Me.Controls.Add(Me.ButtonExcel)
        Me.Controls.Add(Me.ComboBoxClose)
        Me.Name = "Frm_Last_Incidencies"
        Me.Text = "ULTIMES INCIDENCIES"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxClose As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonExcel As System.Windows.Forms.Button
    Friend WithEvents Xl_Incidencies1 As Mat.NET.Xl_Incidencies
    Friend WithEvents CheckBoxSrcProducte As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxSrcTransport As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_LookupProduct1 As Mat.NET.Xl_LookupProduct
    Friend WithEvents CheckBoxCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxProduct As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxIncludeClosed As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_Contact21 As Mat.NET.Xl_Contact2
End Class
