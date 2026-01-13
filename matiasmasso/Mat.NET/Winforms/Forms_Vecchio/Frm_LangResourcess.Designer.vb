<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_LangResourcess
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.CheckBoxFiltre = New System.Windows.Forms.CheckBox
        Me.TextboxClau = New System.Windows.Forms.TextBox
        Me.ButtonFiltre = New System.Windows.Forms.Button
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 36)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(739, 452)
        Me.DataGridView1.TabIndex = 3
        '
        'CheckBoxFiltre
        '
        Me.CheckBoxFiltre.AutoSize = True
        Me.CheckBoxFiltre.Location = New System.Drawing.Point(13, 13)
        Me.CheckBoxFiltre.Name = "CheckBoxFiltre"
        Me.CheckBoxFiltre.Size = New System.Drawing.Size(48, 17)
        Me.CheckBoxFiltre.TabIndex = 0
        Me.CheckBoxFiltre.Text = "filtrar"
        Me.CheckBoxFiltre.UseVisualStyleBackColor = True
        '
        'TextboxClau
        '
        Me.TextboxClau.Location = New System.Drawing.Point(79, 10)
        Me.TextboxClau.Name = "TextboxClau"
        Me.TextboxClau.Size = New System.Drawing.Size(209, 20)
        Me.TextboxClau.TabIndex = 1
        Me.TextboxClau.Visible = False
        '
        'ButtonFiltre
        '
        Me.ButtonFiltre.Location = New System.Drawing.Point(294, 9)
        Me.ButtonFiltre.Name = "ButtonFiltre"
        Me.ButtonFiltre.Size = New System.Drawing.Size(41, 23)
        Me.ButtonFiltre.TabIndex = 2
        Me.ButtonFiltre.Text = "..."
        Me.ButtonFiltre.UseVisualStyleBackColor = True
        Me.ButtonFiltre.Visible = False
        '
        'Frm_LangResourcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(752, 488)
        Me.Controls.Add(Me.ButtonFiltre)
        Me.Controls.Add(Me.TextboxClau)
        Me.Controls.Add(Me.CheckBoxFiltre)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Frm_LangResourcess"
        Me.Text = "RECURSOS DE TEXTE"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents CheckBoxFiltre As System.Windows.Forms.CheckBox
    Friend WithEvents TextboxClau As System.Windows.Forms.TextBox
    Friend WithEvents ButtonFiltre As System.Windows.Forms.Button
End Class
