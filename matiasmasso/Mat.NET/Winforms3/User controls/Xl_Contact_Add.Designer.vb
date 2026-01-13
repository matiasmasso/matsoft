<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Contact_Add
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Xl_Contact21 = New Xl_Contact2()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(230, 20)
        Me.Xl_Contact21.TabIndex = 0
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Dock = System.Windows.Forms.DockStyle.Right
        Me.ButtonAdd.Enabled = False
        Me.ButtonAdd.Location = New System.Drawing.Point(234, 0)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(52, 20)
        Me.ButtonAdd.TabIndex = 1
        Me.ButtonAdd.Text = "afegir"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'Xl_Contact_Add
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Name = "Xl_Contact_Add"
        Me.Size = New System.Drawing.Size(286, 20)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button

End Class
