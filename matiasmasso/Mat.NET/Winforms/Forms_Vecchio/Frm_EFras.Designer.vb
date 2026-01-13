<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EFras
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.LabelCount = New System.Windows.Forms.Label()
        Me.ButtonTels = New System.Windows.Forms.Button()
        Me.ButtonEmails = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(3, 61)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(612, 288)
        Me.DataGridView1.TabIndex = 0
        '
        'LabelCount
        '
        Me.LabelCount.AutoSize = True
        Me.LabelCount.Location = New System.Drawing.Point(0, 13)
        Me.LabelCount.Name = "LabelCount"
        Me.LabelCount.Size = New System.Drawing.Size(340, 13)
        Me.LabelCount.TabIndex = 1
        Me.LabelCount.Text = "Clients amb facturació electrónica (sobre el total dels que tenen crèdit):"
        '
        'ButtonTels
        '
        Me.ButtonTels.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTels.Location = New System.Drawing.Point(477, 3)
        Me.ButtonTels.Name = "ButtonTels"
        Me.ButtonTels.Size = New System.Drawing.Size(138, 23)
        Me.ButtonTels.TabIndex = 2
        Me.ButtonTels.Text = "telefs. clients que falten"
        Me.ButtonTels.UseVisualStyleBackColor = True
        '
        'ButtonEmails
        '
        Me.ButtonEmails.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEmails.Location = New System.Drawing.Point(477, 32)
        Me.ButtonEmails.Name = "ButtonEmails"
        Me.ButtonEmails.Size = New System.Drawing.Size(138, 23)
        Me.ButtonEmails.TabIndex = 3
        Me.ButtonEmails.Text = "email clients que falten"
        Me.ButtonEmails.UseVisualStyleBackColor = True
        '
        'Frm_EFras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(618, 351)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ButtonEmails)
        Me.Controls.Add(Me.ButtonTels)
        Me.Controls.Add(Me.LabelCount)
        Me.Name = "Frm_EFras"
        Me.Text = "CLIENTS AMB FACTURACIO ELECTRONICA"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents LabelCount As System.Windows.Forms.Label
    Friend WithEvents ButtonTels As System.Windows.Forms.Button
    Friend WithEvents ButtonEmails As System.Windows.Forms.Button
End Class
