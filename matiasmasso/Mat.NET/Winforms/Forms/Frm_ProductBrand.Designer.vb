<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ProductBrand
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
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ComboBoxDistribucio = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_ContactProveidor = New Winforms.Xl_Contact2()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductCategories1 = New Winforms.Xl_ProductCategories()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_BrandAreas1 = New Winforms.Xl_BrandAreas()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_RepProducts1 = New Winforms.Xl_RepProducts()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductChannels1 = New Winforms.Xl_ProductChannels()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.Xl_ProductChannels1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 231)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(453, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(234, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(345, 4)
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
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(103, 23)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(334, 20)
        Me.TextBoxNom.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Nom"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(0, 29)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(453, 200)
        Me.TabControl1.TabIndex = 52
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ComboBoxDistribucio)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Xl_ContactProveidor)
        Me.TabPage1.Controls.Add(Me.CheckBoxObsoleto)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(445, 174)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ComboBoxDistribucio
        '
        Me.ComboBoxDistribucio.FormattingEnabled = True
        Me.ComboBoxDistribucio.Items.AddRange(New Object() {"Oberta    - tothom pot comprar", "Tancada - nomes per distribuidors oficials"})
        Me.ComboBoxDistribucio.Location = New System.Drawing.Point(103, 77)
        Me.ComboBoxDistribucio.Name = "ComboBoxDistribucio"
        Me.ComboBoxDistribucio.Size = New System.Drawing.Size(334, 21)
        Me.ComboBoxDistribucio.TabIndex = 56
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Distribució"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Proveidor"
        '
        'Xl_ContactProveidor
        '
        Me.Xl_ContactProveidor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactProveidor.Contact = Nothing
        Me.Xl_ContactProveidor.Location = New System.Drawing.Point(103, 50)
        Me.Xl_ContactProveidor.Name = "Xl_ContactProveidor"
        Me.Xl_ContactProveidor.ReadOnly = False
        Me.Xl_ContactProveidor.Size = New System.Drawing.Size(334, 20)
        Me.Xl_ContactProveidor.TabIndex = 1
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.AutoSize = True
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(103, 104)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxObsoleto.TabIndex = 2
        Me.CheckBoxObsoleto.Text = "Obsoleto"
        Me.CheckBoxObsoleto.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_ProductCategories1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(445, 174)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Categories"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_ProductCategories1
        '
        Me.Xl_ProductCategories1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductCategories1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductCategories1.Name = "Xl_ProductCategories1"
        Me.Xl_ProductCategories1.ShowObsolets = False
        Me.Xl_ProductCategories1.Size = New System.Drawing.Size(439, 168)
        Me.Xl_ProductCategories1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_BrandAreas1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(445, 174)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Zones"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_BrandAreas1
        '
        Me.Xl_BrandAreas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BrandAreas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_BrandAreas1.Name = "Xl_BrandAreas1"
        Me.Xl_BrandAreas1.Size = New System.Drawing.Size(445, 174)
        Me.Xl_BrandAreas1.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_RepProducts1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(445, 174)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Reps"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_RepProducts1
        '
        Me.Xl_RepProducts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RepProducts1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_RepProducts1.Name = "Xl_RepProducts1"
        Me.Xl_RepProducts1.Size = New System.Drawing.Size(445, 174)
        Me.Xl_RepProducts1.TabIndex = 0
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Xl_ProductChannels1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(445, 174)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Canals"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Xl_ProductChannels1
        '
        Me.Xl_ProductChannels1.AllowUserToAddRows = False
        Me.Xl_ProductChannels1.AllowUserToDeleteRows = False
        Me.Xl_ProductChannels1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductChannels1.DisplayObsolets = False
        Me.Xl_ProductChannels1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductChannels1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductChannels1.MouseIsDown = False
        Me.Xl_ProductChannels1.Name = "Xl_ProductChannels1"
        Me.Xl_ProductChannels1.ReadOnly = True
        Me.Xl_ProductChannels1.Size = New System.Drawing.Size(439, 168)
        Me.Xl_ProductChannels1.TabIndex = 0
        '
        'Frm_ProductBrand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(453, 262)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_ProductBrand"
        Me.Text = "Marca Comercial"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        CType(Me.Xl_ProductChannels1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents CheckBoxObsoleto As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_BrandAreas1 As Winforms.Xl_BrandAreas
    Friend WithEvents Xl_ProductCategories1 As Winforms.Xl_ProductCategories
    Friend WithEvents Xl_RepProducts1 As Winforms.Xl_RepProducts
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactProveidor As Winforms.Xl_Contact2
    Friend WithEvents ComboBoxDistribucio As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents Xl_ProductChannels1 As Xl_ProductChannels
End Class
