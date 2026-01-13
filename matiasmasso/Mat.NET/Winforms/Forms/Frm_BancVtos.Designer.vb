<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BancVtos
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
        Me.Xl_Years1 = New Winforms.Xl_Years()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.Xl_CsbVtosxBanc1 = New Winforms.Xl_CsbVtosxBanc()
        CType(Me.Xl_CsbVtosxBanc1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(13, 13)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 0
        Me.Xl_Years1.Value = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(192, 16)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(224, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'Xl_CsbVtosxBanc1
        '
        Me.Xl_CsbVtosxBanc1.AllowUserToAddRows = False
        Me.Xl_CsbVtosxBanc1.AllowUserToDeleteRows = False
        Me.Xl_CsbVtosxBanc1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CsbVtosxBanc1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CsbVtosxBanc1.Location = New System.Drawing.Point(0, 55)
        Me.Xl_CsbVtosxBanc1.Name = "Xl_CsbVtosxBanc1"
        Me.Xl_CsbVtosxBanc1.ReadOnly = True
        Me.Xl_CsbVtosxBanc1.Size = New System.Drawing.Size(428, 458)
        Me.Xl_CsbVtosxBanc1.TabIndex = 2
        '
        'Frm_BancVtos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(428, 513)
        Me.Controls.Add(Me.Xl_CsbVtosxBanc1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Name = "Frm_BancVtos"
        Me.Text = "Efectes presentats a un banc"
        CType(Me.Xl_CsbVtosxBanc1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_CsbVtosxBanc1 As Xl_CsbVtosxBanc
End Class
