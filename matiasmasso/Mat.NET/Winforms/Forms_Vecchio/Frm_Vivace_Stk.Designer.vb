<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Vivace_Stk
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
        Me.TextBoxPath = New System.Windows.Forms.TextBox
        Me.ButtonBrowse = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown
        Me.NumericUpDownQ = New System.Windows.Forms.NumericUpDown
        Me.ButtonImport = New System.Windows.Forms.Button
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownQ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxPath
        '
        Me.TextBoxPath.Location = New System.Drawing.Point(12, 89)
        Me.TextBoxPath.Name = "TextBoxPath"
        Me.TextBoxPath.Size = New System.Drawing.Size(343, 20)
        Me.TextBoxPath.TabIndex = 0
        '
        'ButtonBrowse
        '
        Me.ButtonBrowse.Location = New System.Drawing.Point(365, 88)
        Me.ButtonBrowse.Name = "ButtonBrowse"
        Me.ButtonBrowse.Size = New System.Drawing.Size(44, 20)
        Me.ButtonBrowse.TabIndex = 1
        Me.ButtonBrowse.Text = "..."
        Me.ButtonBrowse.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "any"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(71, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "trimestre"
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Location = New System.Drawing.Point(15, 56)
        Me.NumericUpDownYea.Maximum = New Decimal(New Integer() {2050, 0, 0, 0})
        Me.NumericUpDownYea.Minimum = New Decimal(New Integer() {2006, 0, 0, 0})
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.NumericUpDownYea.Size = New System.Drawing.Size(59, 20)
        Me.NumericUpDownYea.TabIndex = 6
        Me.NumericUpDownYea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDownYea.Value = New Decimal(New Integer() {2006, 0, 0, 0})
        '
        'NumericUpDownQ
        '
        Me.NumericUpDownQ.Location = New System.Drawing.Point(80, 55)
        Me.NumericUpDownQ.Maximum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.NumericUpDownQ.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDownQ.Name = "NumericUpDownQ"
        Me.NumericUpDownQ.Size = New System.Drawing.Size(37, 20)
        Me.NumericUpDownQ.TabIndex = 7
        Me.NumericUpDownQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDownQ.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ButtonImport
        '
        Me.ButtonImport.Location = New System.Drawing.Point(364, 114)
        Me.ButtonImport.Name = "ButtonImport"
        Me.ButtonImport.Size = New System.Drawing.Size(44, 20)
        Me.ButtonImport.TabIndex = 8
        Me.ButtonImport.Text = "importar"
        Me.ButtonImport.UseVisualStyleBackColor = True
        '
        'Frm_Vivace_Stk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 264)
        Me.Controls.Add(Me.ButtonImport)
        Me.Controls.Add(Me.NumericUpDownQ)
        Me.Controls.Add(Me.NumericUpDownYea)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonBrowse)
        Me.Controls.Add(Me.TextBoxPath)
        Me.Name = "Frm_Vivace_Stk"
        Me.Text = "Frm_Vivace_Stk"
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownQ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxPath As System.Windows.Forms.TextBox
    Friend WithEvents ButtonBrowse As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownYea As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownQ As System.Windows.Forms.NumericUpDown
    Friend WithEvents ButtonImport As System.Windows.Forms.Button
End Class
