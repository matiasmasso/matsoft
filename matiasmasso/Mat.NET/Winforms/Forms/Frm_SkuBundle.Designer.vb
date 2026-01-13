<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_SkuBundle
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.CheckBoxObsolet = New System.Windows.Forms.CheckBox()
        Me.ButtonNavigate = New System.Windows.Forms.Button()
        Me.Xl_Image1 = New Winforms.Xl_Image()
        Me.Xl_LookupProductCategory1 = New Winforms.Xl_LookupProductCategory()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Xl_BundleSkus1 = New Winforms.Xl_BundleSkus()
        Me.TextBoxEan = New System.Windows.Forms.TextBox()
        Me.TextBoxPvp = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_Percent1 = New Winforms.Xl_Percent()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxUrl = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxNomProveidor = New System.Windows.Forms.TextBox()
        Me.TextBoxRefProveidor = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductSkusExtendedAccessories = New Winforms.Xl_ProductSkusExtended()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductSkusExtendedSpares = New Winforms.Xl_ProductSkusExtended()
        Me.ButtonNomCurt = New System.Windows.Forms.Button()
        Me.TextBoxNomCurt = New System.Windows.Forms.TextBox()
        Me.ButtonNomLlarg = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNomLlarg = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_BundleSkus1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_ProductSkusExtendedAccessories, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_ProductSkusExtendedSpares, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(1, 32)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(776, 505)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ButtonNomCurt)
        Me.TabPage1.Controls.Add(Me.TextBoxNomCurt)
        Me.TabPage1.Controls.Add(Me.ButtonNomLlarg)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxNomLlarg)
        Me.TabPage1.Controls.Add(Me.CheckBoxObsolet)
        Me.TabPage1.Controls.Add(Me.ButtonNavigate)
        Me.TabPage1.Controls.Add(Me.Xl_Image1)
        Me.TabPage1.Controls.Add(Me.Xl_LookupProductCategory1)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Xl_BundleSkus1)
        Me.TabPage1.Controls.Add(Me.TextBoxEan)
        Me.TabPage1.Controls.Add(Me.TextBoxPvp)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.Xl_Percent1)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxUrl)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.TextBoxNomProveidor)
        Me.TabPage1.Controls.Add(Me.TextBoxRefProveidor)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(768, 479)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'CheckBoxObsolet
        '
        Me.CheckBoxObsolet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxObsolet.AutoSize = True
        Me.CheckBoxObsolet.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxObsolet.Location = New System.Drawing.Point(411, 222)
        Me.CheckBoxObsolet.Name = "CheckBoxObsolet"
        Me.CheckBoxObsolet.Size = New System.Drawing.Size(62, 17)
        Me.CheckBoxObsolet.TabIndex = 69
        Me.CheckBoxObsolet.Text = "Obsolet"
        Me.CheckBoxObsolet.UseVisualStyleBackColor = True
        '
        'ButtonNavigate
        '
        Me.ButtonNavigate.Location = New System.Drawing.Point(441, 96)
        Me.ButtonNavigate.Name = "ButtonNavigate"
        Me.ButtonNavigate.Size = New System.Drawing.Size(32, 20)
        Me.ButtonNavigate.TabIndex = 68
        Me.ButtonNavigate.Text = "..."
        Me.ButtonNavigate.UseVisualStyleBackColor = True
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image1.EmptyImageLabelText = ""
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(488, 3)
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(280, 320)
        Me.Xl_Image1.TabIndex = 67
        Me.Xl_Image1.ZipStream = Nothing
        '
        'Xl_LookupProductCategory1
        '
        Me.Xl_LookupProductCategory1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProductCategory1.IsDirty = False
        Me.Xl_LookupProductCategory1.Location = New System.Drawing.Point(82, 19)
        Me.Xl_LookupProductCategory1.Name = "Xl_LookupProductCategory1"
        Me.Xl_LookupProductCategory1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProductCategory1.Product = Nothing
        Me.Xl_LookupProductCategory1.ReadOnlyLookup = False
        Me.Xl_LookupProductCategory1.Size = New System.Drawing.Size(391, 20)
        Me.Xl_LookupProductCategory1.TabIndex = 1
        Me.Xl_LookupProductCategory1.Value = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(7, 245)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(186, 13)
        Me.Label10.TabIndex = 66
        Me.Label10.Text = "Es composa dels següents productes:"
        '
        'Xl_BundleSkus1
        '
        Me.Xl_BundleSkus1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BundleSkus1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BundleSkus1.Location = New System.Drawing.Point(9, 261)
        Me.Xl_BundleSkus1.Name = "Xl_BundleSkus1"
        Me.Xl_BundleSkus1.Size = New System.Drawing.Size(464, 178)
        Me.Xl_BundleSkus1.TabIndex = 9
        '
        'TextBoxEan
        '
        Me.TextBoxEan.Location = New System.Drawing.Point(82, 122)
        Me.TextBoxEan.Name = "TextBoxEan"
        Me.TextBoxEan.Size = New System.Drawing.Size(198, 20)
        Me.TextBoxEan.TabIndex = 4
        '
        'TextBoxPvp
        '
        Me.TextBoxPvp.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBoxPvp.Location = New System.Drawing.Point(366, 199)
        Me.TextBoxPvp.Name = "TextBoxPvp"
        Me.TextBoxPvp.ReadOnly = True
        Me.TextBoxPvp.Size = New System.Drawing.Size(107, 20)
        Me.TextBoxPvp.TabIndex = 63
        Me.TextBoxPvp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(331, 202)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(28, 13)
        Me.Label9.TabIndex = 62
        Me.Label9.Text = "PVP"
        '
        'Xl_Percent1
        '
        Me.Xl_Percent1.Location = New System.Drawing.Point(82, 199)
        Me.Xl_Percent1.Name = "Xl_Percent1"
        Me.Xl_Percent1.Size = New System.Drawing.Size(100, 20)
        Me.Xl_Percent1.TabIndex = 7
        Me.Xl_Percent1.Text = "0%"
        Me.Xl_Percent1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_Percent1.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 202)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 60
        Me.Label6.Text = "Descompte"
        '
        'TextBoxUrl
        '
        Me.TextBoxUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUrl.Location = New System.Drawing.Point(82, 96)
        Me.TextBoxUrl.Name = "TextBoxUrl"
        Me.TextBoxUrl.Size = New System.Drawing.Size(353, 20)
        Me.TextBoxUrl.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 125)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "EAN 13"
        '
        'TextBoxNomProveidor
        '
        Me.TextBoxNomProveidor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomProveidor.Location = New System.Drawing.Point(82, 175)
        Me.TextBoxNomProveidor.Name = "TextBoxNomProveidor"
        Me.TextBoxNomProveidor.Size = New System.Drawing.Size(391, 20)
        Me.TextBoxNomProveidor.TabIndex = 6
        '
        'TextBoxRefProveidor
        '
        Me.TextBoxRefProveidor.Location = New System.Drawing.Point(82, 148)
        Me.TextBoxRefProveidor.Name = "TextBoxRefProveidor"
        Me.TextBoxRefProveidor.Size = New System.Drawing.Size(198, 20)
        Me.TextBoxRefProveidor.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 99)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(20, 13)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "Url"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 178)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 13)
        Me.Label5.TabIndex = 49
        Me.Label5.Text = "Nom fabricant"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 151)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Ref fabricant"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Categoría"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(3, 445)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(762, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(543, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 11
        Me.ButtonCancel.Text = "Cancel·lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(654, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 10
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
        Me.ButtonDel.TabIndex = 12
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_ProductSkusExtendedAccessories)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(768, 479)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Accessoris"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_ProductSkusExtendedAccessories
        '
        Me.Xl_ProductSkusExtendedAccessories.AllowUserToAddRows = False
        Me.Xl_ProductSkusExtendedAccessories.AllowUserToDeleteRows = False
        Me.Xl_ProductSkusExtendedAccessories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductSkusExtendedAccessories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductSkusExtendedAccessories.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductSkusExtendedAccessories.Name = "Xl_ProductSkusExtendedAccessories"
        Me.Xl_ProductSkusExtendedAccessories.ReadOnly = True
        Me.Xl_ProductSkusExtendedAccessories.ShowObsolets = False
        Me.Xl_ProductSkusExtendedAccessories.Size = New System.Drawing.Size(762, 473)
        Me.Xl_ProductSkusExtendedAccessories.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_ProductSkusExtendedSpares)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(768, 479)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Recanvis"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_ProductSkusExtendedSpares
        '
        Me.Xl_ProductSkusExtendedSpares.AllowUserToAddRows = False
        Me.Xl_ProductSkusExtendedSpares.AllowUserToDeleteRows = False
        Me.Xl_ProductSkusExtendedSpares.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductSkusExtendedSpares.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductSkusExtendedSpares.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductSkusExtendedSpares.Name = "Xl_ProductSkusExtendedSpares"
        Me.Xl_ProductSkusExtendedSpares.ReadOnly = True
        Me.Xl_ProductSkusExtendedSpares.ShowObsolets = False
        Me.Xl_ProductSkusExtendedSpares.Size = New System.Drawing.Size(768, 479)
        Me.Xl_ProductSkusExtendedSpares.TabIndex = 1
        '
        'ButtonNomCurt
        '
        Me.ButtonNomCurt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNomCurt.Location = New System.Drawing.Point(443, 45)
        Me.ButtonNomCurt.Name = "ButtonNomCurt"
        Me.ButtonNomCurt.Size = New System.Drawing.Size(30, 20)
        Me.ButtonNomCurt.TabIndex = 151
        Me.ButtonNomCurt.Text = "..."
        Me.ButtonNomCurt.UseVisualStyleBackColor = True
        '
        'TextBoxNomCurt
        '
        Me.TextBoxNomCurt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomCurt.Location = New System.Drawing.Point(82, 44)
        Me.TextBoxNomCurt.MaxLength = 60
        Me.TextBoxNomCurt.Name = "TextBoxNomCurt"
        Me.TextBoxNomCurt.ReadOnly = True
        Me.TextBoxNomCurt.Size = New System.Drawing.Size(353, 20)
        Me.TextBoxNomCurt.TabIndex = 150
        '
        'ButtonNomLlarg
        '
        Me.ButtonNomLlarg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNomLlarg.Location = New System.Drawing.Point(443, 71)
        Me.ButtonNomLlarg.Name = "ButtonNomLlarg"
        Me.ButtonNomLlarg.Size = New System.Drawing.Size(30, 20)
        Me.ButtonNomLlarg.TabIndex = 149
        Me.ButtonNomLlarg.Text = "..."
        Me.ButtonNomLlarg.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(6, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 16)
        Me.Label3.TabIndex = 148
        Me.Label3.Text = "Nom:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 147
        Me.Label2.Text = "Nom curt:"
        '
        'TextBoxNomLlarg
        '
        Me.TextBoxNomLlarg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomLlarg.Location = New System.Drawing.Point(82, 70)
        Me.TextBoxNomLlarg.MaxLength = 60
        Me.TextBoxNomLlarg.Name = "TextBoxNomLlarg"
        Me.TextBoxNomLlarg.ReadOnly = True
        Me.TextBoxNomLlarg.Size = New System.Drawing.Size(353, 20)
        Me.TextBoxNomLlarg.TabIndex = 146
        '
        'Frm_SkuBundle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 538)
        Me.Controls.Add(Me.TabControl1)
        Me.MinimumSize = New System.Drawing.Size(16, 454)
        Me.Name = "Frm_SkuBundle"
        Me.Text = "Bundle"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.Xl_BundleSkus1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_ProductSkusExtendedAccessories, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_ProductSkusExtendedSpares, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents TextBoxUrl As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxNomProveidor As TextBox
    Friend WithEvents TextBoxRefProveidor As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxPvp As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Xl_Percent1 As Xl_Percent
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxEan As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Xl_BundleSkus1 As Xl_BundleSkus
    Friend WithEvents Xl_LookupProductCategory1 As Xl_LookupProductCategory
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents ButtonNavigate As Button
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_ProductSkusExtendedAccessories As Xl_ProductSkusExtended
    Friend WithEvents Xl_ProductSkusExtendedSpares As Xl_ProductSkusExtended
    Friend WithEvents CheckBoxObsolet As CheckBox
    Friend WithEvents ButtonNomCurt As Button
    Friend WithEvents TextBoxNomCurt As TextBox
    Friend WithEvents ButtonNomLlarg As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxNomLlarg As TextBox
End Class
