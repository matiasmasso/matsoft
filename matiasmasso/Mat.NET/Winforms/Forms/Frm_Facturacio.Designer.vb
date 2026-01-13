<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Facturacio
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
        Me.TabPageFactures = New System.Windows.Forms.TabPage()
        Me.TabPageExport = New System.Windows.Forms.TabPage()
        Me.TabPagePending = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_Facturacio2 = New Winforms.Xl_Facturacio()
        Me.Xl_ProgressBar2 = New Winforms.Xl_ProgressBar()
        Me.Xl_Deliveries2 = New Winforms.Xl_Deliveries()
        Me.Xl_Deliveries3 = New Winforms.Xl_Deliveries()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_Facturacio2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Deliveries2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Deliveries3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabPageFactures
        '
        Me.TabPageFactures.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFactures.Name = "TabPageFactures"
        Me.TabPageFactures.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageFactures.Size = New System.Drawing.Size(559, 210)
        Me.TabPageFactures.TabIndex = 0
        Me.TabPageFactures.Text = "Factures"
        Me.TabPageFactures.UseVisualStyleBackColor = True
        '
        'TabPageExport
        '
        Me.TabPageExport.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExport.Name = "TabPageExport"
        Me.TabPageExport.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageExport.Size = New System.Drawing.Size(559, 210)
        Me.TabPageExport.TabIndex = 2
        Me.TabPageExport.Text = "Export"
        Me.TabPageExport.UseVisualStyleBackColor = True
        '
        'TabPagePending
        '
        Me.TabPagePending.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePending.Name = "TabPagePending"
        Me.TabPagePending.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePending.Size = New System.Drawing.Size(559, 210)
        Me.TabPagePending.TabIndex = 1
        Me.TabPagePending.Text = "Pendent de facturar"
        Me.TabPagePending.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 472)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(568, 31)
        Me.Panel1.TabIndex = 43
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(350, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(460, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 30)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(568, 440)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Facturacio2)
        Me.TabPage1.Controls.Add(Me.Xl_ProgressBar2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(560, 414)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Factures"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_Deliveries2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(560, 414)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Export"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_Deliveries3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(560, 414)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Albarans per facturar"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_Facturacio2
        '
        Me.Xl_Facturacio2.AllowUserToAddRows = False
        Me.Xl_Facturacio2.AllowUserToDeleteRows = False
        Me.Xl_Facturacio2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Facturacio2.DisplayObsolets = False
        Me.Xl_Facturacio2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Facturacio2.Filter = Nothing
        Me.Xl_Facturacio2.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Facturacio2.MouseIsDown = False
        Me.Xl_Facturacio2.Name = "Xl_Facturacio2"
        Me.Xl_Facturacio2.ReadOnly = True
        Me.Xl_Facturacio2.Size = New System.Drawing.Size(554, 378)
        Me.Xl_Facturacio2.TabIndex = 0
        '
        'Xl_ProgressBar2
        '
        Me.Xl_ProgressBar2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_ProgressBar2.Location = New System.Drawing.Point(3, 381)
        Me.Xl_ProgressBar2.Name = "Xl_ProgressBar2"
        Me.Xl_ProgressBar2.Size = New System.Drawing.Size(554, 30)
        Me.Xl_ProgressBar2.TabIndex = 0
        '
        'Xl_Deliveries2
        '
        Me.Xl_Deliveries2.AllowUserToAddRows = False
        Me.Xl_Deliveries2.AllowUserToDeleteRows = False
        Me.Xl_Deliveries2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Deliveries2.DisplayObsolets = False
        Me.Xl_Deliveries2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Deliveries2.Filter = Nothing
        Me.Xl_Deliveries2.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Deliveries2.MouseIsDown = False
        Me.Xl_Deliveries2.Name = "Xl_Deliveries2"
        Me.Xl_Deliveries2.ReadOnly = True
        Me.Xl_Deliveries2.Size = New System.Drawing.Size(554, 408)
        Me.Xl_Deliveries2.TabIndex = 0
        '
        'Xl_Deliveries3
        '
        Me.Xl_Deliveries3.AllowUserToAddRows = False
        Me.Xl_Deliveries3.AllowUserToDeleteRows = False
        Me.Xl_Deliveries3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Deliveries3.DisplayObsolets = False
        Me.Xl_Deliveries3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Deliveries3.Filter = Nothing
        Me.Xl_Deliveries3.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Deliveries3.MouseIsDown = False
        Me.Xl_Deliveries3.Name = "Xl_Deliveries3"
        Me.Xl_Deliveries3.ReadOnly = True
        Me.Xl_Deliveries3.Size = New System.Drawing.Size(560, 414)
        Me.Xl_Deliveries3.TabIndex = 1
        '
        'Frm_Facturacio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 503)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Facturacio"
        Me.Text = "Facturacio"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_Facturacio2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Deliveries2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Deliveries3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabPageExport As TabPage
    Friend WithEvents TabPagePending As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents TabPageFactures As TabPage
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Xl_Facturacio2 As Xl_Facturacio
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_ProgressBar2 As Xl_ProgressBar
    Friend WithEvents Xl_Deliveries2 As Xl_Deliveries
    Friend WithEvents Xl_Deliveries3 As Xl_Deliveries
End Class
