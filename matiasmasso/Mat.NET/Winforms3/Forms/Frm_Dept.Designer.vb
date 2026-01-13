<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Dept
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.NumericUpDownOrd = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxBrandNom = New System.Windows.Forms.TextBox()
        Me.TextBoxEsp = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_CnapsCheckTree1 = New Xl_CnapsCheckTree()
        Me.Xl_ProductCategories1 = New Xl_ProductCategories()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductCategories2 = New Xl_ProductCategories()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_Image1 = New Xl_Image()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductFilteredUrls1 = New Xl_ProductFilteredUrls()
        Me.ButtonShowLangTexts = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.NumericUpDownOrd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 294)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(451, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(232, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(343, 4)
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
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Location = New System.Drawing.Point(0, 29)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(447, 263)
        Me.TabControl1.TabIndex = 43
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ButtonShowLangTexts)
        Me.TabPage1.Controls.Add(Me.NumericUpDownOrd)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.TextBoxBrandNom)
        Me.TabPage1.Controls.Add(Me.TextBoxEsp)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(439, 237)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'NumericUpDownOrd
        '
        Me.NumericUpDownOrd.Location = New System.Drawing.Point(86, 190)
        Me.NumericUpDownOrd.Name = "NumericUpDownOrd"
        Me.NumericUpDownOrd.Size = New System.Drawing.Size(47, 20)
        Me.NumericUpDownOrd.TabIndex = 66
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 192)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 13)
        Me.Label6.TabIndex = 65
        Me.Label6.Text = "Ordre"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 64
        Me.Label5.Text = "Marca"
        '
        'TextBoxBrandNom
        '
        Me.TextBoxBrandNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBrandNom.Location = New System.Drawing.Point(86, 17)
        Me.TextBoxBrandNom.Name = "TextBoxBrandNom"
        Me.TextBoxBrandNom.ReadOnly = True
        Me.TextBoxBrandNom.Size = New System.Drawing.Size(347, 20)
        Me.TextBoxBrandNom.TabIndex = 63
        '
        'TextBoxEsp
        '
        Me.TextBoxEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEsp.Location = New System.Drawing.Point(86, 43)
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.ReadOnly = True
        Me.TextBoxEsp.Size = New System.Drawing.Size(311, 20)
        Me.TextBoxEsp.TabIndex = 56
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "Nom"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.SplitContainer1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(439, 237)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Cnaps"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_CnapsCheckTree1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_ProductCategories1)
        Me.SplitContainer1.Size = New System.Drawing.Size(433, 231)
        Me.SplitContainer1.SplitterDistance = 226
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_CnapsCheckTree1
        '
        Me.Xl_CnapsCheckTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CnapsCheckTree1.IgnoreClickAction = 0
        Me.Xl_CnapsCheckTree1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CnapsCheckTree1.Name = "Xl_CnapsCheckTree1"
        Me.Xl_CnapsCheckTree1.Size = New System.Drawing.Size(226, 231)
        Me.Xl_CnapsCheckTree1.TabIndex = 0
        '
        'Xl_ProductCategories1
        '
        Me.Xl_ProductCategories1.AllowRemoveOnContextMenu = False
        Me.Xl_ProductCategories1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductCategories1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductCategories1.Name = "Xl_ProductCategories1"
        Me.Xl_ProductCategories1.ShowObsolets = False
        Me.Xl_ProductCategories1.Size = New System.Drawing.Size(203, 231)
        Me.Xl_ProductCategories1.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_ProductCategories2)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(439, 237)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Categories"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_ProductCategories2
        '
        Me.Xl_ProductCategories2.AllowRemoveOnContextMenu = False
        Me.Xl_ProductCategories2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductCategories2.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductCategories2.Name = "Xl_ProductCategories2"
        Me.Xl_ProductCategories2.ShowObsolets = False
        Me.Xl_ProductCategories2.Size = New System.Drawing.Size(433, 231)
        Me.Xl_ProductCategories2.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_Image1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(439, 237)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Banner"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Image1.EmptyImageLabelText = ""
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(439, 237)
        Me.Xl_Image1.TabIndex = 0
        Me.Xl_Image1.ZipStream = Nothing
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Xl_ProductFilteredUrls1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(439, 237)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Filter Landing Pages"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Xl_ProductFilteredUrls1
        '
        Me.Xl_ProductFilteredUrls1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductFilteredUrls1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductFilteredUrls1.Name = "Xl_ProductFilteredUrls1"
        Me.Xl_ProductFilteredUrls1.Size = New System.Drawing.Size(433, 231)
        Me.Xl_ProductFilteredUrls1.TabIndex = 0
        '
        'ButtonShowLangTexts
        '
        Me.ButtonShowLangTexts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonShowLangTexts.Location = New System.Drawing.Point(403, 43)
        Me.ButtonShowLangTexts.Name = "ButtonShowLangTexts"
        Me.ButtonShowLangTexts.Size = New System.Drawing.Size(29, 20)
        Me.ButtonShowLangTexts.TabIndex = 141
        Me.ButtonShowLangTexts.Text = "..."
        Me.ButtonShowLangTexts.UseVisualStyleBackColor = True
        '
        'Frm_Dept
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 325)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Dept"
        Me.Text = "Departament"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.NumericUpDownOrd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TextBoxEsp As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxBrandNom As TextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_CnapsCheckTree1 As Xl_CnapsCheckTree
    Friend WithEvents Xl_ProductCategories1 As Xl_ProductCategories
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents NumericUpDownOrd As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Xl_ProductCategories2 As Xl_ProductCategories
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents Xl_ProductFilteredUrls1 As Xl_ProductFilteredUrls
    Friend WithEvents ButtonShowLangTexts As Button
End Class
