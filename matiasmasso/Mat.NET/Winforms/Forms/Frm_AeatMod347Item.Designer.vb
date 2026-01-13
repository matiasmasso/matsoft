<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AeatMod347Item
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
        Me.Xl_AeatMod347CcasCompras = New Winforms.Xl_AeatMod347Ccas()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageCompres = New System.Windows.Forms.TabPage()
        Me.TabPageVendes = New System.Windows.Forms.TabPage()
        Me.Xl_AeatMod347CcasVendes = New Winforms.Xl_AeatMod347Ccas()
        Me.TabControl1.SuspendLayout()
        Me.TabPageCompres.SuspendLayout()
        Me.TabPageVendes.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_AeatMod347CcasCompras
        '
        Me.Xl_AeatMod347CcasCompras.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_AeatMod347CcasCompras.Filter = Nothing
        Me.Xl_AeatMod347CcasCompras.Location = New System.Drawing.Point(3, 3)
        Me.Xl_AeatMod347CcasCompras.Name = "Xl_AeatMod347CcasCompras"
        Me.Xl_AeatMod347CcasCompras.Size = New System.Drawing.Size(876, 216)
        Me.Xl_AeatMod347CcasCompras.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageCompres)
        Me.TabControl1.Controls.Add(Me.TabPageVendes)
        Me.TabControl1.Location = New System.Drawing.Point(2, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(890, 248)
        Me.TabControl1.TabIndex = 1
        '
        'TabPageCompres
        '
        Me.TabPageCompres.Controls.Add(Me.Xl_AeatMod347CcasCompras)
        Me.TabPageCompres.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCompres.Name = "TabPageCompres"
        Me.TabPageCompres.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCompres.Size = New System.Drawing.Size(882, 222)
        Me.TabPageCompres.TabIndex = 0
        Me.TabPageCompres.Text = "Compres"
        Me.TabPageCompres.UseVisualStyleBackColor = True
        '
        'TabPageVendes
        '
        Me.TabPageVendes.Controls.Add(Me.Xl_AeatMod347CcasVendes)
        Me.TabPageVendes.Location = New System.Drawing.Point(4, 22)
        Me.TabPageVendes.Name = "TabPageVendes"
        Me.TabPageVendes.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageVendes.Size = New System.Drawing.Size(882, 222)
        Me.TabPageVendes.TabIndex = 1
        Me.TabPageVendes.Text = "Vendes"
        Me.TabPageVendes.UseVisualStyleBackColor = True
        '
        'Xl_AeatMod347CcasVendes
        '
        Me.Xl_AeatMod347CcasVendes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_AeatMod347CcasVendes.Filter = Nothing
        Me.Xl_AeatMod347CcasVendes.Location = New System.Drawing.Point(3, 3)
        Me.Xl_AeatMod347CcasVendes.Name = "Xl_AeatMod347CcasVendes"
        Me.Xl_AeatMod347CcasVendes.Size = New System.Drawing.Size(876, 216)
        Me.Xl_AeatMod347CcasVendes.TabIndex = 1
        '
        'Frm_AeatMod347Item
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 261)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_AeatMod347Item"
        Me.Text = "Frm_AeatMod347Item"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageCompres.ResumeLayout(False)
        Me.TabPageVendes.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_AeatMod347CcasCompras As Winforms.Xl_AeatMod347Ccas
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageCompres As TabPage
    Friend WithEvents TabPageVendes As TabPage
    Friend WithEvents Xl_AeatMod347CcasVendes As Xl_AeatMod347Ccas
End Class
