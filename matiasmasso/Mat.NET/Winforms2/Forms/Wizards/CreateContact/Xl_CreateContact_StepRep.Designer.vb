<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_CreateContact_StepRep
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.LabelAlies = New System.Windows.Forms.Label()
        Me.TextBoxNickname = New System.Windows.Forms.TextBox()
        Me.LabelAlta = New System.Windows.Forms.Label()
        Me.DateTimePickerAlta = New System.Windows.Forms.DateTimePicker()
        Me.LabelComStd = New System.Windows.Forms.Label()
        Me.LabelComRed = New System.Windows.Forms.Label()
        Me.LabelProducts = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxRaoSocial = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_PercentComRed = New Xl_Percent()
        Me.Xl_PercentComStd = New Xl_Percent()
        Me.Xl_LookupAddress1 = New Xl_LookupAddress()
        Me.Xl_RepProductsTree1 = New Xl_RepProductsTree()
        Me.Xl_LookupNif1 = New Xl_LookupNif()
        Me.SuspendLayout()
        '
        'LabelAlies
        '
        Me.LabelAlies.AutoSize = True
        Me.LabelAlies.Location = New System.Drawing.Point(9, 43)
        Me.LabelAlies.Name = "LabelAlies"
        Me.LabelAlies.Size = New System.Drawing.Size(32, 13)
        Me.LabelAlies.TabIndex = 0
        Me.LabelAlies.Text = "Alies:"
        '
        'TextBoxNickname
        '
        Me.TextBoxNickname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNickname.Location = New System.Drawing.Point(110, 40)
        Me.TextBoxNickname.Name = "TextBoxNickname"
        Me.TextBoxNickname.Size = New System.Drawing.Size(266, 20)
        Me.TextBoxNickname.TabIndex = 1
        '
        'LabelAlta
        '
        Me.LabelAlta.AutoSize = True
        Me.LabelAlta.Location = New System.Drawing.Point(9, 121)
        Me.LabelAlta.Name = "LabelAlta"
        Me.LabelAlta.Size = New System.Drawing.Size(28, 13)
        Me.LabelAlta.TabIndex = 2
        Me.LabelAlta.Text = "Alta:"
        '
        'DateTimePickerAlta
        '
        Me.DateTimePickerAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAlta.Location = New System.Drawing.Point(110, 118)
        Me.DateTimePickerAlta.Name = "DateTimePickerAlta"
        Me.DateTimePickerAlta.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePickerAlta.TabIndex = 4
        '
        'LabelComStd
        '
        Me.LabelComStd.AutoSize = True
        Me.LabelComStd.Location = New System.Drawing.Point(9, 147)
        Me.LabelComStd.Name = "LabelComStd"
        Me.LabelComStd.Size = New System.Drawing.Size(87, 13)
        Me.LabelComStd.TabIndex = 4
        Me.LabelComStd.Text = "Comisió estandar"
        '
        'LabelComRed
        '
        Me.LabelComRed.AutoSize = True
        Me.LabelComRed.Location = New System.Drawing.Point(9, 173)
        Me.LabelComRed.Name = "LabelComRed"
        Me.LabelComRed.Size = New System.Drawing.Size(81, 13)
        Me.LabelComRed.TabIndex = 6
        Me.LabelComRed.Text = "Comisió reduida"
        '
        'LabelProducts
        '
        Me.LabelProducts.AutoSize = True
        Me.LabelProducts.Location = New System.Drawing.Point(9, 197)
        Me.LabelProducts.Name = "LabelProducts"
        Me.LabelProducts.Size = New System.Drawing.Size(55, 13)
        Me.LabelProducts.TabIndex = 9
        Me.LabelProducts.Text = "Productes"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Adreça"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Nif"
        '
        'TextBoxRaoSocial
        '
        Me.TextBoxRaoSocial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRaoSocial.Location = New System.Drawing.Point(110, 14)
        Me.TextBoxRaoSocial.Name = "TextBoxRaoSocial"
        Me.TextBoxRaoSocial.Size = New System.Drawing.Size(266, 20)
        Me.TextBoxRaoSocial.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Cognoms i nom"
        '
        'Xl_PercentComRed
        '
        Me.Xl_PercentComRed.Location = New System.Drawing.Point(110, 170)
        Me.Xl_PercentComRed.Name = "Xl_PercentComRed"
        Me.Xl_PercentComRed.Size = New System.Drawing.Size(53, 20)
        Me.Xl_PercentComRed.TabIndex = 6
        Me.Xl_PercentComRed.Text = "0 %"
        Me.Xl_PercentComRed.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_PercentComStd
        '
        Me.Xl_PercentComStd.Location = New System.Drawing.Point(110, 144)
        Me.Xl_PercentComStd.Name = "Xl_PercentComStd"
        Me.Xl_PercentComStd.Size = New System.Drawing.Size(53, 20)
        Me.Xl_PercentComStd.TabIndex = 5
        Me.Xl_PercentComStd.Text = "0 %"
        Me.Xl_PercentComStd.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_LookupAddress1
        '
        Me.Xl_LookupAddress1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupAddress1.IsDirty = False
        Me.Xl_LookupAddress1.Location = New System.Drawing.Point(110, 92)
        Me.Xl_LookupAddress1.Name = "Xl_LookupAddress1"
        Me.Xl_LookupAddress1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupAddress1.ReadOnlyLookup = False
        Me.Xl_LookupAddress1.Size = New System.Drawing.Size(266, 20)
        Me.Xl_LookupAddress1.TabIndex = 24
        Me.Xl_LookupAddress1.Value = Nothing
        '
        'Xl_RepProductsTree1
        '
        Me.Xl_RepProductsTree1.Location = New System.Drawing.Point(110, 197)
        Me.Xl_RepProductsTree1.Name = "Xl_RepProductsTree1"
        Me.Xl_RepProductsTree1.Size = New System.Drawing.Size(266, 130)
        Me.Xl_RepProductsTree1.TabIndex = 25
        '
        'Xl_LookupNif1
        '
        Me.Xl_LookupNif1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupNif1.IsDirty = False
        Me.Xl_LookupNif1.Location = New System.Drawing.Point(110, 66)
        Me.Xl_LookupNif1.Name = "Xl_LookupNif1"
        Me.Xl_LookupNif1.Nif = Nothing
        Me.Xl_LookupNif1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupNif1.ReadOnlyLookup = False
        Me.Xl_LookupNif1.Size = New System.Drawing.Size(266, 20)
        Me.Xl_LookupNif1.TabIndex = 26
        Me.Xl_LookupNif1.Value = Nothing
        '
        'Xl_CreateContact_StepRep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Xl_LookupNif1)
        Me.Controls.Add(Me.Xl_RepProductsTree1)
        Me.Controls.Add(Me.Xl_LookupAddress1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxRaoSocial)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelProducts)
        Me.Controls.Add(Me.Xl_PercentComRed)
        Me.Controls.Add(Me.LabelComRed)
        Me.Controls.Add(Me.Xl_PercentComStd)
        Me.Controls.Add(Me.LabelComStd)
        Me.Controls.Add(Me.DateTimePickerAlta)
        Me.Controls.Add(Me.LabelAlta)
        Me.Controls.Add(Me.TextBoxNickname)
        Me.Controls.Add(Me.LabelAlies)
        Me.Name = "Xl_CreateContact_StepRep"
        Me.Size = New System.Drawing.Size(389, 342)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelAlies As Label
    Friend WithEvents TextBoxNickname As TextBox
    Friend WithEvents LabelAlta As Label
    Friend WithEvents DateTimePickerAlta As DateTimePicker
    Friend WithEvents LabelComStd As Label
    Friend WithEvents Xl_PercentComStd As Xl_Percent
    Friend WithEvents Xl_PercentComRed As Xl_Percent
    Friend WithEvents LabelComRed As Label
    Friend WithEvents LabelProducts As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxRaoSocial As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_LookupAddress1 As Xl_LookupAddress
    Friend WithEvents Xl_RepProductsTree1 As Xl_RepProductsTree
    Friend WithEvents Xl_LookupNif1 As Xl_LookupNif
End Class
