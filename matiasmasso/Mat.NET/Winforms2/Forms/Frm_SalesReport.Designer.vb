<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SalesReport
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
        Me.Xl_SalesReportItems1 = New Xl_SalesReportItems()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ComboBoxFormat = New System.Windows.Forms.ComboBox()
        Me.CheckBoxProduct = New System.Windows.Forms.CheckBox()
        Me.ComboBoxCentros = New System.Windows.Forms.ComboBox()
        Me.ComboBoxDepts = New System.Windows.Forms.ComboBox()
        Me.Xl_LookupProduct1 = New Xl_LookupProduct()
        Me.Xl_Years1 = New Xl_Years()
        Me.ButtonExcel = New System.Windows.Forms.Button()
        Me.ComboBoxConcept = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_SalesReportItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_SalesReportItems1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(-1, 50)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(684, 209)
        Me.Panel1.TabIndex = 0
        '
        'Xl_SalesReportItems1
        '
        Me.Xl_SalesReportItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SalesReportItems1.DisplayObsolets = False
        Me.Xl_SalesReportItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_SalesReportItems1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_SalesReportItems1.MouseIsDown = False
        Me.Xl_SalesReportItems1.Name = "Xl_SalesReportItems1"
        Me.Xl_SalesReportItems1.Size = New System.Drawing.Size(684, 186)
        Me.Xl_SalesReportItems1.TabIndex = 1
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 186)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(684, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'ComboBoxFormat
        '
        Me.ComboBoxFormat.FormattingEnabled = True
        Me.ComboBoxFormat.Items.AddRange(New Object() {"Unitats", "Imports"})
        Me.ComboBoxFormat.Location = New System.Drawing.Point(-1, 26)
        Me.ComboBoxFormat.Name = "ComboBoxFormat"
        Me.ComboBoxFormat.Size = New System.Drawing.Size(163, 21)
        Me.ComboBoxFormat.TabIndex = 2
        '
        'CheckBoxProduct
        '
        Me.CheckBoxProduct.AutoSize = True
        Me.CheckBoxProduct.Location = New System.Drawing.Point(178, 28)
        Me.CheckBoxProduct.Name = "CheckBoxProduct"
        Me.CheckBoxProduct.Size = New System.Drawing.Size(69, 17)
        Me.CheckBoxProduct.TabIndex = 4
        Me.CheckBoxProduct.Text = "Producte"
        Me.CheckBoxProduct.UseVisualStyleBackColor = True
        '
        'ComboBoxCentros
        '
        Me.ComboBoxCentros.FormattingEnabled = True
        Me.ComboBoxCentros.Location = New System.Drawing.Point(178, 1)
        Me.ComboBoxCentros.Name = "ComboBoxCentros"
        Me.ComboBoxCentros.Size = New System.Drawing.Size(275, 21)
        Me.ComboBoxCentros.TabIndex = 5
        '
        'ComboBoxDepts
        '
        Me.ComboBoxDepts.FormattingEnabled = True
        Me.ComboBoxDepts.Location = New System.Drawing.Point(459, 1)
        Me.ComboBoxDepts.Name = "ComboBoxDepts"
        Me.ComboBoxDepts.Size = New System.Drawing.Size(150, 21)
        Me.ComboBoxDepts.TabIndex = 6
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(253, 25)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(200, 20)
        Me.Xl_LookupProduct1.TabIndex = 3
        Me.Xl_LookupProduct1.Value = Nothing
        Me.Xl_LookupProduct1.Visible = False
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(-1, -1)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 1
        Me.Xl_Years1.Value = 0
        '
        'ButtonExcel
        '
        Me.ButtonExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExcel.Image = Global.Mat.Net.My.Resources.Resources.Excel
        Me.ButtonExcel.Location = New System.Drawing.Point(656, 1)
        Me.ButtonExcel.Name = "ButtonExcel"
        Me.ButtonExcel.Size = New System.Drawing.Size(27, 23)
        Me.ButtonExcel.TabIndex = 7
        Me.ButtonExcel.UseVisualStyleBackColor = True
        '
        'ComboBoxConcept
        '
        Me.ComboBoxConcept.FormattingEnabled = True
        Me.ComboBoxConcept.Location = New System.Drawing.Point(459, 24)
        Me.ComboBoxConcept.Name = "ComboBoxConcept"
        Me.ComboBoxConcept.Size = New System.Drawing.Size(150, 21)
        Me.ComboBoxConcept.TabIndex = 8
        '
        'Frm_SalesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 261)
        Me.Controls.Add(Me.ComboBoxConcept)
        Me.Controls.Add(Me.ButtonExcel)
        Me.Controls.Add(Me.ComboBoxDepts)
        Me.Controls.Add(Me.ComboBoxCentros)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.CheckBoxProduct)
        Me.Controls.Add(Me.ComboBoxFormat)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_SalesReport"
        Me.Text = "Sales Report"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_SalesReportItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Xl_SalesReportItems1 As Xl_SalesReportItems
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents ComboBoxFormat As ComboBox
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents CheckBoxProduct As CheckBox
    Friend WithEvents ComboBoxCentros As ComboBox
    Friend WithEvents ComboBoxDepts As ComboBox
    Friend WithEvents ButtonExcel As Button
    Friend WithEvents ComboBoxConcept As ComboBox
End Class
