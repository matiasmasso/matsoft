<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_CreateContact_StepChannel
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
        Me.RadioButtonClient = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAltres = New System.Windows.Forms.RadioButton()
        Me.Xl_ContactClasses1 = New Xl_ContactClasses()
        CType(Me.Xl_ContactClasses1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadioButtonClient
        '
        Me.RadioButtonClient.AutoSize = True
        Me.RadioButtonClient.Checked = True
        Me.RadioButtonClient.Location = New System.Drawing.Point(24, 13)
        Me.RadioButtonClient.Name = "RadioButtonClient"
        Me.RadioButtonClient.Size = New System.Drawing.Size(51, 17)
        Me.RadioButtonClient.TabIndex = 0
        Me.RadioButtonClient.TabStop = True
        Me.RadioButtonClient.Text = "Client"
        Me.RadioButtonClient.UseVisualStyleBackColor = True
        '
        'RadioButtonAltres
        '
        Me.RadioButtonAltres.AutoSize = True
        Me.RadioButtonAltres.Location = New System.Drawing.Point(24, 37)
        Me.RadioButtonAltres.Name = "RadioButtonAltres"
        Me.RadioButtonAltres.Size = New System.Drawing.Size(51, 17)
        Me.RadioButtonAltres.TabIndex = 1
        Me.RadioButtonAltres.Text = "Altres"
        Me.RadioButtonAltres.UseVisualStyleBackColor = True
        '
        'Xl_ContactClasses1
        '
        Me.Xl_ContactClasses1.AllowUserToAddRows = False
        Me.Xl_ContactClasses1.AllowUserToDeleteRows = False
        Me.Xl_ContactClasses1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ContactClasses1.DisplayObsolets = False
        Me.Xl_ContactClasses1.Filter = Nothing
        Me.Xl_ContactClasses1.Location = New System.Drawing.Point(24, 73)
        Me.Xl_ContactClasses1.MouseIsDown = False
        Me.Xl_ContactClasses1.Name = "Xl_ContactClasses1"
        Me.Xl_ContactClasses1.ReadOnly = True
        Me.Xl_ContactClasses1.Size = New System.Drawing.Size(338, 242)
        Me.Xl_ContactClasses1.TabIndex = 2
        '
        'Xl_CreateContact_StepChannel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Xl_ContactClasses1)
        Me.Controls.Add(Me.RadioButtonAltres)
        Me.Controls.Add(Me.RadioButtonClient)
        Me.Name = "Xl_CreateContact_StepChannel"
        Me.Size = New System.Drawing.Size(389, 342)
        CType(Me.Xl_ContactClasses1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadioButtonClient As RadioButton
    Friend WithEvents RadioButtonAltres As RadioButton
    Friend WithEvents Xl_ContactClasses1 As Xl_ContactClasses
End Class
