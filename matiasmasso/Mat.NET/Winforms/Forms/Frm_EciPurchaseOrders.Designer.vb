<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_EciPurchaseOrders
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
        Me.ComboBoxDepts = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.Xl_ECIPurchaseOrders1 = New Winforms.Xl_ECIPurchaseOrders()
        Me.Xl_Years1 = New Winforms.Xl_Years()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.Xl_ECIPurchaseOrders1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBoxDepts
        '
        Me.ComboBoxDepts.FormattingEnabled = True
        Me.ComboBoxDepts.Location = New System.Drawing.Point(253, 24)
        Me.ComboBoxDepts.Name = "ComboBoxDepts"
        Me.ComboBoxDepts.Size = New System.Drawing.Size(76, 21)
        Me.ComboBoxDepts.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(176, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Departament:"
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(335, 24)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(215, 20)
        Me.Xl_TextboxSearch1.TabIndex = 4
        '
        'Xl_ECIPurchaseOrders1
        '
        Me.Xl_ECIPurchaseOrders1.AllowUserToAddRows = False
        Me.Xl_ECIPurchaseOrders1.AllowUserToDeleteRows = False
        Me.Xl_ECIPurchaseOrders1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ECIPurchaseOrders1.DisplayObsolets = False
        Me.Xl_ECIPurchaseOrders1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ECIPurchaseOrders1.Filter = Nothing
        Me.Xl_ECIPurchaseOrders1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ECIPurchaseOrders1.MouseIsDown = False
        Me.Xl_ECIPurchaseOrders1.Name = "Xl_ECIPurchaseOrders1"
        Me.Xl_ECIPurchaseOrders1.ReadOnly = True
        Me.Xl_ECIPurchaseOrders1.Size = New System.Drawing.Size(549, 395)
        Me.Xl_ECIPurchaseOrders1.TabIndex = 0
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(1, 21)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 5
        Me.Xl_Years1.Value = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.Xl_ECIPurchaseOrders1)
        Me.Panel1.Location = New System.Drawing.Point(1, 50)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(549, 395)
        Me.Panel1.TabIndex = 6
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 372)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(549, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_EciPurchaseOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(551, 447)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxDepts)
        Me.Name = "Frm_EciPurchaseOrders"
        Me.Text = "Comandes El Corte Ingles"
        CType(Me.Xl_ECIPurchaseOrders1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_ECIPurchaseOrders1 As Xl_ECIPurchaseOrders
    Friend WithEvents ComboBoxDepts As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
