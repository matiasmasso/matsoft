<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Product_Historial
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
        Me.ComboBoxMgz = New System.Windows.Forms.ComboBox()
        Me.CheckBoxInp = New System.Windows.Forms.CheckBox()
        Me.CheckBoxOut = New System.Windows.Forms.CheckBox()
        Me.Xl_TextboxSearch1 = New Xl_TextboxSearch()
        Me.Xl_Product_Historial1 = New Xl_ProductHistorial()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.Xl_Product_Historial1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBoxMgz
        '
        Me.ComboBoxMgz.FormattingEnabled = True
        Me.ComboBoxMgz.Location = New System.Drawing.Point(1, 12)
        Me.ComboBoxMgz.Name = "ComboBoxMgz"
        Me.ComboBoxMgz.Size = New System.Drawing.Size(229, 21)
        Me.ComboBoxMgz.TabIndex = 0
        '
        'CheckBoxInp
        '
        Me.CheckBoxInp.AutoSize = True
        Me.CheckBoxInp.Checked = True
        Me.CheckBoxInp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxInp.Location = New System.Drawing.Point(282, 16)
        Me.CheckBoxInp.Name = "CheckBoxInp"
        Me.CheckBoxInp.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxInp.TabIndex = 1
        Me.CheckBoxInp.Text = "Entrades"
        Me.CheckBoxInp.UseVisualStyleBackColor = True
        '
        'CheckBoxOut
        '
        Me.CheckBoxOut.AutoSize = True
        Me.CheckBoxOut.Checked = True
        Me.CheckBoxOut.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxOut.Location = New System.Drawing.Point(370, 16)
        Me.CheckBoxOut.Name = "CheckBoxOut"
        Me.CheckBoxOut.Size = New System.Drawing.Size(64, 17)
        Me.CheckBoxOut.TabIndex = 2
        Me.CheckBoxOut.Text = "Sortides"
        Me.CheckBoxOut.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(473, 13)
        Me.Xl_TextboxSearch1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(218, 20)
        Me.Xl_TextboxSearch1.TabIndex = 3
        '
        'Xl_Product_Historial1
        '
        Me.Xl_Product_Historial1.AllowUserToAddRows = False
        Me.Xl_Product_Historial1.AllowUserToDeleteRows = False
        Me.Xl_Product_Historial1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Product_Historial1.DisplayObsolets = False
        Me.Xl_Product_Historial1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Product_Historial1.Filter = Nothing
        Me.Xl_Product_Historial1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Product_Historial1.MouseIsDown = False
        Me.Xl_Product_Historial1.Name = "Xl_Product_Historial1"
        Me.Xl_Product_Historial1.ReadOnly = True
        Me.Xl_Product_Historial1.Size = New System.Drawing.Size(690, 231)
        Me.Xl_Product_Historial1.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_Product_Historial1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(1, 43)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(690, 254)
        Me.Panel1.TabIndex = 5
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 231)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(690, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_Product_Historial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 294)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.CheckBoxOut)
        Me.Controls.Add(Me.CheckBoxInp)
        Me.Controls.Add(Me.ComboBoxMgz)
        Me.Name = "Frm_Product_Historial"
        Me.Text = "Historial de producte"
        CType(Me.Xl_Product_Historial1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ComboBoxMgz As ComboBox
    Friend WithEvents CheckBoxInp As CheckBox
    Friend WithEvents CheckBoxOut As CheckBox
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_Product_Historial1 As Xl_ProductHistorial
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
