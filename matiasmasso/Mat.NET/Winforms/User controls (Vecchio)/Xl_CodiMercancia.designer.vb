<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_CodiMercancia
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
        Me.TextBoxId = New System.Windows.Forms.TextBox
        Me.ButtonSearch = New System.Windows.Forms.Button
        Me.TextBoxDsc = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxId.MaxLength = 8
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.ReadOnly = True
        Me.TextBoxId.Size = New System.Drawing.Size(58, 20)
        Me.TextBoxId.TabIndex = 0
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Location = New System.Drawing.Point(59, 0)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(30, 20)
        Me.ButtonSearch.TabIndex = 1
        Me.ButtonSearch.TabStop = False
        Me.ButtonSearch.Text = "..."
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDsc.Location = New System.Drawing.Point(91, 0)
        Me.TextBoxDsc.MaxLength = 8
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.ReadOnly = True
        Me.TextBoxDsc.Size = New System.Drawing.Size(348, 20)
        Me.TextBoxDsc.TabIndex = 2
        '
        'Xl_CodiMercancia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TextBoxDsc)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.TextBoxId)
        Me.Name = "Xl_CodiMercancia"
        Me.Size = New System.Drawing.Size(439, 20)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxId As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
    Friend WithEvents TextBoxDsc As System.Windows.Forms.TextBox

End Class
