<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Fiscal_Irpf2
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
        Me.Xl_Fiscal_Irpf1 = New Winforms.Xl_Fiscal_Irpf()
        Me.ComboBoxMonth = New System.Windows.Forms.ComboBox()
        Me.ComboBoxYea = New System.Windows.Forms.ComboBox()
        Me.Xl_BancsComboBox1 = New Winforms.Xl_BancsComboBox()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.Xl_Fiscal_Irpf1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Fiscal_Irpf1
        '
        Me.Xl_Fiscal_Irpf1.AllowUserToAddRows = False
        Me.Xl_Fiscal_Irpf1.AllowUserToDeleteRows = False
        Me.Xl_Fiscal_Irpf1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Fiscal_Irpf1.DisplayObsolets = False
        Me.Xl_Fiscal_Irpf1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Fiscal_Irpf1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Fiscal_Irpf1.MouseIsDown = False
        Me.Xl_Fiscal_Irpf1.Name = "Xl_Fiscal_Irpf1"
        Me.Xl_Fiscal_Irpf1.ReadOnly = True
        Me.Xl_Fiscal_Irpf1.Size = New System.Drawing.Size(547, 286)
        Me.Xl_Fiscal_Irpf1.TabIndex = 0
        '
        'ComboBoxMonth
        '
        Me.ComboBoxMonth.FormattingEnabled = True
        Me.ComboBoxMonth.Location = New System.Drawing.Point(367, 12)
        Me.ComboBoxMonth.Name = "ComboBoxMonth"
        Me.ComboBoxMonth.Size = New System.Drawing.Size(89, 21)
        Me.ComboBoxMonth.TabIndex = 1
        '
        'ComboBoxYea
        '
        Me.ComboBoxYea.FormattingEnabled = True
        Me.ComboBoxYea.Location = New System.Drawing.Point(462, 12)
        Me.ComboBoxYea.Name = "ComboBoxYea"
        Me.ComboBoxYea.Size = New System.Drawing.Size(85, 21)
        Me.ComboBoxYea.TabIndex = 2
        '
        'Xl_BancsComboBox1
        '
        Me.Xl_BancsComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Xl_BancsComboBox1.FormattingEnabled = True
        Me.Xl_BancsComboBox1.Location = New System.Drawing.Point(3, 12)
        Me.Xl_BancsComboBox1.Name = "Xl_BancsComboBox1"
        Me.Xl_BancsComboBox1.Size = New System.Drawing.Size(239, 21)
        Me.Xl_BancsComboBox1.TabIndex = 3
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Location = New System.Drawing.Point(402, 12)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(145, 24)
        Me.ButtonOk.TabIndex = 4
        Me.ButtonOk.Text = "Generar assentaments"
        Me.ButtonOk.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.Xl_BancsComboBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 354)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(548, 42)
        Me.Panel1.TabIndex = 5
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.Xl_Fiscal_Irpf1)
        Me.Panel2.Controls.Add(Me.ProgressBar1)
        Me.Panel2.Location = New System.Drawing.Point(0, 43)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(547, 309)
        Me.Panel2.TabIndex = 6
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 286)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(547, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'Frm_Fiscal_Irpf2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(548, 396)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ComboBoxYea)
        Me.Controls.Add(Me.ComboBoxMonth)
        Me.Name = "Frm_Fiscal_Irpf2"
        Me.Text = "Irpf"
        CType(Me.Xl_Fiscal_Irpf1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Fiscal_Irpf1 As Xl_Fiscal_Irpf
    Friend WithEvents ComboBoxMonth As ComboBox
    Friend WithEvents ComboBoxYea As ComboBox
    Friend WithEvents Xl_BancsComboBox1 As Xl_BancsComboBox
    Friend WithEvents ButtonOk As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
