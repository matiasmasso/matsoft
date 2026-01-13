<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Tarifa
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxClient = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxCosts = New System.Windows.Forms.CheckBox()
        Me.ButtonPdf = New System.Windows.Forms.Button()
        Me.ButtonCopyLink = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Client"
        '
        'TextBoxClient
        '
        Me.TextBoxClient.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxClient.Location = New System.Drawing.Point(57, 23)
        Me.TextBoxClient.Name = "TextBoxClient"
        Me.TextBoxClient.ReadOnly = True
        Me.TextBoxClient.Size = New System.Drawing.Size(414, 20)
        Me.TextBoxClient.TabIndex = 1
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(57, 50)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(103, 20)
        Me.DateTimePicker1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Data"
        '
        'CheckBoxCosts
        '
        Me.CheckBoxCosts.AutoSize = True
        Me.CheckBoxCosts.Checked = True
        Me.CheckBoxCosts.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxCosts.Location = New System.Drawing.Point(57, 77)
        Me.CheckBoxCosts.Name = "CheckBoxCosts"
        Me.CheckBoxCosts.Size = New System.Drawing.Size(121, 17)
        Me.CheckBoxCosts.TabIndex = 4
        Me.CheckBoxCosts.Text = "inclou preus de cost"
        Me.CheckBoxCosts.UseVisualStyleBackColor = True
        '
        'ButtonPdf
        '
        Me.ButtonPdf.Location = New System.Drawing.Point(382, 131)
        Me.ButtonPdf.Name = "ButtonPdf"
        Me.ButtonPdf.Size = New System.Drawing.Size(89, 23)
        Me.ButtonPdf.TabIndex = 5
        Me.ButtonPdf.Text = "Pdf"
        Me.ButtonPdf.UseVisualStyleBackColor = True
        '
        'ButtonCopyLink
        '
        Me.ButtonCopyLink.Location = New System.Drawing.Point(382, 160)
        Me.ButtonCopyLink.Name = "ButtonCopyLink"
        Me.ButtonCopyLink.Size = New System.Drawing.Size(89, 23)
        Me.ButtonCopyLink.TabIndex = 6
        Me.ButtonCopyLink.Text = "Copiar enllaç"
        Me.ButtonCopyLink.UseVisualStyleBackColor = True
        '
        'Frm_Tarifa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(504, 198)
        Me.Controls.Add(Me.ButtonCopyLink)
        Me.Controls.Add(Me.ButtonPdf)
        Me.Controls.Add(Me.CheckBoxCosts)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxClient)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_Tarifa"
        Me.Text = "Tarifa de preus"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxClient As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxCosts As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonPdf As System.Windows.Forms.Button
    Friend WithEvents ButtonCopyLink As System.Windows.Forms.Button
End Class
