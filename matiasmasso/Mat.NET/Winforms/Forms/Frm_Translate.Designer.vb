<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Translate
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
        Me.SplitContainerGlobal = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerSup = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerInf = New System.Windows.Forms.SplitContainer()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Xl_LangsToTranslate1 = New Winforms.Xl_LangsToTranslate()
        Me.Xl_LangsToTranslate2 = New Winforms.Xl_LangsToTranslate()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainerGlobal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerGlobal.Panel1.SuspendLayout()
        Me.SplitContainerGlobal.Panel2.SuspendLayout()
        Me.SplitContainerGlobal.SuspendLayout()
        CType(Me.SplitContainerSup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerSup.Panel1.SuspendLayout()
        Me.SplitContainerSup.Panel2.SuspendLayout()
        Me.SplitContainerSup.SuspendLayout()
        CType(Me.SplitContainerInf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerInf.Panel1.SuspendLayout()
        Me.SplitContainerInf.Panel2.SuspendLayout()
        Me.SplitContainerInf.SuspendLayout()
        CType(Me.Xl_LangsToTranslate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_LangsToTranslate2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 533)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(588, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(369, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(480, 4)
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
        'SplitContainerGlobal
        '
        Me.SplitContainerGlobal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerGlobal.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerGlobal.Name = "SplitContainerGlobal"
        Me.SplitContainerGlobal.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerGlobal.Panel1
        '
        Me.SplitContainerGlobal.Panel1.Controls.Add(Me.SplitContainerSup)
        '
        'SplitContainerGlobal.Panel2
        '
        Me.SplitContainerGlobal.Panel2.Controls.Add(Me.SplitContainerInf)
        Me.SplitContainerGlobal.Size = New System.Drawing.Size(588, 533)
        Me.SplitContainerGlobal.SplitterDistance = 251
        Me.SplitContainerGlobal.TabIndex = 43
        '
        'SplitContainerSup
        '
        Me.SplitContainerSup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerSup.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerSup.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerSup.Name = "SplitContainerSup"
        '
        'SplitContainerSup.Panel1
        '
        Me.SplitContainerSup.Panel1.Controls.Add(Me.Xl_LangsToTranslate1)
        '
        'SplitContainerSup.Panel2
        '
        Me.SplitContainerSup.Panel2.Controls.Add(Me.TextBox1)
        Me.SplitContainerSup.Size = New System.Drawing.Size(588, 251)
        Me.SplitContainerSup.SplitterDistance = 126
        Me.SplitContainerSup.TabIndex = 0
        '
        'SplitContainerInf
        '
        Me.SplitContainerInf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerInf.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerInf.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerInf.Name = "SplitContainerInf"
        '
        'SplitContainerInf.Panel1
        '
        Me.SplitContainerInf.Panel1.Controls.Add(Me.Xl_LangsToTranslate2)
        '
        'SplitContainerInf.Panel2
        '
        Me.SplitContainerInf.Panel2.Controls.Add(Me.TextBox2)
        Me.SplitContainerInf.Size = New System.Drawing.Size(588, 278)
        Me.SplitContainerInf.SplitterDistance = 126
        Me.SplitContainerInf.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(458, 251)
        Me.TextBox1.TabIndex = 0
        '
        'TextBox2
        '
        Me.TextBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox2.Location = New System.Drawing.Point(0, 0)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(458, 278)
        Me.TextBox2.TabIndex = 1
        '
        'Xl_LangsToTranslate1
        '
        Me.Xl_LangsToTranslate1.AllowUserToAddRows = False
        Me.Xl_LangsToTranslate1.AllowUserToDeleteRows = False
        Me.Xl_LangsToTranslate1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_LangsToTranslate1.DisplayObsolets = False
        Me.Xl_LangsToTranslate1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsToTranslate1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsToTranslate1.MouseIsDown = False
        Me.Xl_LangsToTranslate1.Name = "Xl_LangsToTranslate1"
        Me.Xl_LangsToTranslate1.ReadOnly = True
        Me.Xl_LangsToTranslate1.Size = New System.Drawing.Size(126, 251)
        Me.Xl_LangsToTranslate1.TabIndex = 1
        '
        'Xl_LangsToTranslate2
        '
        Me.Xl_LangsToTranslate2.AllowUserToAddRows = False
        Me.Xl_LangsToTranslate2.AllowUserToDeleteRows = False
        Me.Xl_LangsToTranslate2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_LangsToTranslate2.DisplayObsolets = False
        Me.Xl_LangsToTranslate2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsToTranslate2.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsToTranslate2.MouseIsDown = False
        Me.Xl_LangsToTranslate2.Name = "Xl_LangsToTranslate2"
        Me.Xl_LangsToTranslate2.ReadOnly = True
        Me.Xl_LangsToTranslate2.Size = New System.Drawing.Size(126, 278)
        Me.Xl_LangsToTranslate2.TabIndex = 2
        '
        'Frm_Translate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(588, 564)
        Me.Controls.Add(Me.SplitContainerGlobal)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Translate"
        Me.Text = "Traducció"
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainerGlobal.Panel1.ResumeLayout(False)
        Me.SplitContainerGlobal.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerGlobal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerGlobal.ResumeLayout(False)
        Me.SplitContainerSup.Panel1.ResumeLayout(False)
        Me.SplitContainerSup.Panel2.ResumeLayout(False)
        Me.SplitContainerSup.Panel2.PerformLayout()
        CType(Me.SplitContainerSup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerSup.ResumeLayout(False)
        Me.SplitContainerInf.Panel1.ResumeLayout(False)
        Me.SplitContainerInf.Panel2.ResumeLayout(False)
        Me.SplitContainerInf.Panel2.PerformLayout()
        CType(Me.SplitContainerInf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerInf.ResumeLayout(False)
        CType(Me.Xl_LangsToTranslate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_LangsToTranslate2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents SplitContainerGlobal As SplitContainer
    Friend WithEvents SplitContainerSup As SplitContainer
    Friend WithEvents SplitContainerInf As SplitContainer
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Xl_LangsToTranslate1 As Winforms.Xl_LangsToTranslate
    Friend WithEvents Xl_LangsToTranslate2 As Winforms.Xl_LangsToTranslate
End Class
