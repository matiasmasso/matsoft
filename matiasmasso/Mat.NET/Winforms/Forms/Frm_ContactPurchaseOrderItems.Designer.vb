<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ContactPurchaseOrderItems
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
        Me.Xl_ContactPurchaseOrderItems1 = New Winforms.Xl_ContactPurchaseOrderItems()
        Me.CheckBoxFilter = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        CType(Me.Xl_ContactPurchaseOrderItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_ContactPurchaseOrderItems1
        '
        Me.Xl_ContactPurchaseOrderItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ContactPurchaseOrderItems1.DisplayObsolets = False
        Me.Xl_ContactPurchaseOrderItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ContactPurchaseOrderItems1.ProductFilter = Nothing
        Me.Xl_ContactPurchaseOrderItems1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ContactPurchaseOrderItems1.MouseIsDown = False
        Me.Xl_ContactPurchaseOrderItems1.Name = "Xl_ContactPurchaseOrderItems1"
        Me.Xl_ContactPurchaseOrderItems1.Size = New System.Drawing.Size(692, 413)
        Me.Xl_ContactPurchaseOrderItems1.TabIndex = 0
        '
        'CheckBoxFilter
        '
        Me.CheckBoxFilter.AutoSize = True
        Me.CheckBoxFilter.Location = New System.Drawing.Point(13, 13)
        Me.CheckBoxFilter.Name = "CheckBoxFilter"
        Me.CheckBoxFilter.Size = New System.Drawing.Size(114, 17)
        Me.CheckBoxFilter.TabIndex = 1
        Me.CheckBoxFilter.Text = "Filtrar per producte"
        Me.CheckBoxFilter.UseVisualStyleBackColor = True
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(134, 10)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(322, 20)
        Me.Xl_LookupProduct1.TabIndex = 2
        Me.Xl_LookupProduct1.Value = Nothing
        Me.Xl_LookupProduct1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_ContactPurchaseOrderItems1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(692, 436)
        Me.Panel1.TabIndex = 3
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 413)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(692, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(542, 10)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 4
        '
        'Frm_ContactPurchaseOrderItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 472)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.CheckBoxFilter)
        Me.Name = "Frm_ContactPurchaseOrderItems"
        Me.Text = "Historial de client"
        CType(Me.Xl_ContactPurchaseOrderItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_ContactPurchaseOrderItems1 As Xl_ContactPurchaseOrderItems
    Friend WithEvents CheckBoxFilter As CheckBox
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
End Class
