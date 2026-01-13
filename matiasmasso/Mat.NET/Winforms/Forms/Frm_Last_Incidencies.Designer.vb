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
        Me.Xl_Incidencies1 = New Winforms.Xl_IncidenciesOld()
        Me.CheckBoxSrcProducte = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSrcTransport = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.CheckBoxCustomer = New System.Windows.Forms.CheckBox()
        Me.CheckBoxProduct = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIncludeClosed = New System.Windows.Forms.CheckBox()
        Me.Xl_Contact21 = New Winforms.Xl_Contact2()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReposicionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_Years1 = New Winforms.Xl_Years()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBoxClose
        '
        Me.ComboBoxClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxClose.Enabled = False
        Me.ComboBoxClose.FormattingEnabled = True
        Me.ComboBoxClose.Location = New System.Drawing.Point(633, 52)
        Me.ComboBoxClose.Name = "ComboBoxClose"
        Me.ComboBoxClose.Size = New System.Drawing.Size(437, 21)
        Me.ComboBoxClose.TabIndex = 3
        '
        'Xl_Incidencies1
        '
        Me.Xl_Incidencies1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Incidencies1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Incidencies1.Name = "Xl_Incidencies1"
        Me.Xl_Incidencies1.Size = New System.Drawing.Size(1070, 386)
        Me.Xl_Incidencies1.TabIndex = 5
        '
        'CheckBoxSrcProducte
        '
        Me.CheckBoxSrcProducte.AutoSize = True
        Me.CheckBoxSrcProducte.Location = New System.Drawing.Point(41, 33)
        Me.CheckBoxSrcProducte.Name = "CheckBoxSrcProducte"
        Me.CheckBoxSrcProducte.Size = New System.Drawing.Size(83, 17)
        Me.CheckBoxSrcProducte.TabIndex = 6
        Me.CheckBoxSrcProducte.Text = "de producte"
        Me.CheckBoxSrcProducte.UseVisualStyleBackColor = True
        '
        'CheckBoxSrcTransport
        '
        Me.CheckBoxSrcTransport.AutoSize = True
        Me.CheckBoxSrcTransport.Location = New System.Drawing.Point(41, 52)
        Me.CheckBoxSrcTransport.Name = "CheckBoxSrcTransport"
        Me.CheckBoxSrcTransport.Size = New System.Drawing.Size(82, 17)
        Me.CheckBoxSrcTransport.TabIndex = 7
        Me.CheckBoxSrcTransport.Text = "de transport"
        Me.CheckBoxSrcTransport.UseVisualStyleBackColor = True
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(260, 30)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(326, 20)
        Me.Xl_LookupProduct1.TabIndex = 8
        Me.Xl_LookupProduct1.Value = Nothing
        Me.Xl_LookupProduct1.Visible = False
        '
        'CheckBoxCustomer
        '
        Me.CheckBoxCustomer.AutoSize = True
        Me.CheckBoxCustomer.Location = New System.Drawing.Point(149, 53)
        Me.CheckBoxCustomer.Name = "CheckBoxCustomer"
        Me.CheckBoxCustomer.Size = New System.Drawing.Size(94, 17)
        Me.CheckBoxCustomer.TabIndex = 12
        Me.CheckBoxCustomer.Text = "filtrar per client"
        Me.CheckBoxCustomer.UseVisualStyleBackColor = True
        '
        'CheckBoxProduct
        '
        Me.CheckBoxProduct.AutoSize = True
        Me.CheckBoxProduct.Location = New System.Drawing.Point(149, 33)
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
        Me.CheckBoxIncludeClosed.Location = New System.Drawing.Point(886, 33)
        Me.CheckBoxIncludeClosed.Name = "CheckBoxIncludeClosed"
        Me.CheckBoxIncludeClosed.Size = New System.Drawing.Size(184, 17)
        Me.CheckBoxIncludeClosed.TabIndex = 14
        Me.CheckBoxIncludeClosed.Text = "inclou les incidencies ja tancades"
        Me.CheckBoxIncludeClosed.UseVisualStyleBackColor = True
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Emp = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(260, 52)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(326, 20)
        Me.Xl_Contact21.TabIndex = 15
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1070, 24)
        Me.MenuStrip1.TabIndex = 16
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.ReposicionsToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'ReposicionsToolStripMenuItem
        '
        Me.ReposicionsToolStripMenuItem.Name = "ReposicionsToolStripMenuItem"
        Me.ReposicionsToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.ReposicionsToolStripMenuItem.Text = "Reposicions"
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(633, 28)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 17
        Me.Xl_Years1.Value = 0
        Me.Xl_Years1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_Incidencies1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 77)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1070, 409)
        Me.Panel1.TabIndex = 18
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 386)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1070, 23)
        Me.ProgressBar1.TabIndex = 0
        Me.ProgressBar1.Style = ProgressBarStyle.Marquee
        '
        'Frm_Last_Incidencies
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1070, 487)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.CheckBoxIncludeClosed)
        Me.Controls.Add(Me.CheckBoxProduct)
        Me.Controls.Add(Me.CheckBoxCustomer)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.CheckBoxSrcTransport)
        Me.Controls.Add(Me.CheckBoxSrcProducte)
        Me.Controls.Add(Me.ComboBoxClose)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Last_Incidencies"
        Me.Text = "Ultimes Incidències"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxClose As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_Incidencies1 As Winforms.Xl_IncidenciesOld
    Friend WithEvents CheckBoxSrcProducte As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxSrcTransport As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_LookupProduct1 As Winforms.Xl_LookupProduct
    Friend WithEvents CheckBoxCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxProduct As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxIncludeClosed As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_Contact21 As Winforms.Xl_Contact2
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReposicionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
