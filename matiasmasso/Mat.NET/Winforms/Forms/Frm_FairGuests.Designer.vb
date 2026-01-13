<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_FairGuests
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
        Me.Xl_FairGuests1 = New Winforms.Xl_FairGuests2()
        Me.Xl_LookupEvento1 = New Winforms.Xl_LookupEvento()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.LabelCount = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Xl_FairGuests1
        '
        Me.Xl_FairGuests1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_FairGuests1.Filter = Nothing
        Me.Xl_FairGuests1.Location = New System.Drawing.Point(4, 49)
        Me.Xl_FairGuests1.Name = "Xl_FairGuests1"
        Me.Xl_FairGuests1.Size = New System.Drawing.Size(657, 402)
        Me.Xl_FairGuests1.TabIndex = 1
        '
        'Xl_LookupEvento1
        '
        Me.Xl_LookupEvento1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupEvento1.EventoValue = Nothing
        Me.Xl_LookupEvento1.IsDirty = False
        Me.Xl_LookupEvento1.Location = New System.Drawing.Point(4, 23)
        Me.Xl_LookupEvento1.Name = "Xl_LookupEvento1"
        Me.Xl_LookupEvento1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupEvento1.Size = New System.Drawing.Size(273, 20)
        Me.Xl_LookupEvento1.TabIndex = 2
        Me.Xl_LookupEvento1.Value = Nothing
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(420, 23)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(244, 20)
        Me.Xl_TextboxSearch1.TabIndex = 3
        '
        'LabelCount
        '
        Me.LabelCount.AutoSize = True
        Me.LabelCount.Location = New System.Drawing.Point(287, 26)
        Me.LabelCount.Name = "LabelCount"
        Me.LabelCount.Size = New System.Drawing.Size(90, 13)
        Me.LabelCount.TabIndex = 4
        Me.LabelCount.Text = "visitants registrats"
        '
        'Frm_FairGuests
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 453)
        Me.Controls.Add(Me.LabelCount)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_LookupEvento1)
        Me.Controls.Add(Me.Xl_FairGuests1)
        Me.Name = "Frm_FairGuests"
        Me.Text = "Registrats Fira"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_FairGuests1 As Winforms.Xl_FairGuests2
    Friend WithEvents Xl_LookupEvento1 As Xl_LookupEvento
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents LabelCount As Label
End Class
