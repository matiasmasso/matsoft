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
        Me.TextBoxEvento = New System.Windows.Forms.TextBox()
        Me.Xl_FairGuests1 = New Mat.NET.Xl_FairGuests()
        Me.TextBoxCount = New System.Windows.Forms.TextBox()
        Me.TextBoxSearch = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TextBoxEvento
        '
        Me.TextBoxEvento.Location = New System.Drawing.Point(4, 12)
        Me.TextBoxEvento.Name = "TextBoxEvento"
        Me.TextBoxEvento.ReadOnly = True
        Me.TextBoxEvento.Size = New System.Drawing.Size(286, 20)
        Me.TextBoxEvento.TabIndex = 0
        '
        'Xl_FairGuests1
        '
        Me.Xl_FairGuests1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_FairGuests1.Location = New System.Drawing.Point(4, 49)
        Me.Xl_FairGuests1.Name = "Xl_FairGuests1"
        Me.Xl_FairGuests1.Size = New System.Drawing.Size(657, 402)
        Me.Xl_FairGuests1.TabIndex = 1
        '
        'TextBoxCount
        '
        Me.TextBoxCount.Location = New System.Drawing.Point(296, 12)
        Me.TextBoxCount.Name = "TextBoxCount"
        Me.TextBoxCount.ReadOnly = True
        Me.TextBoxCount.Size = New System.Drawing.Size(71, 20)
        Me.TextBoxCount.TabIndex = 2
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.Location = New System.Drawing.Point(373, 12)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.Size = New System.Drawing.Size(288, 20)
        Me.TextBoxSearch.TabIndex = 3
        '
        'Frm_FairGuests
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 453)
        Me.Controls.Add(Me.TextBoxSearch)
        Me.Controls.Add(Me.TextBoxCount)
        Me.Controls.Add(Me.Xl_FairGuests1)
        Me.Controls.Add(Me.TextBoxEvento)
        Me.Name = "Frm_FairGuests"
        Me.Text = "Registrats Fira"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxEvento As System.Windows.Forms.TextBox
    Friend WithEvents Xl_FairGuests1 As Mat.NET.Xl_FairGuests
    Friend WithEvents TextBoxCount As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSearch As System.Windows.Forms.TextBox
End Class
