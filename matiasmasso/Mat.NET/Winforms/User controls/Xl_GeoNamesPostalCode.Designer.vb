<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_GeoNamesPostalCode
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
        Me.TextBoxZip = New System.Windows.Forms.TextBox()
        Me.ComboBoxPlaceNames = New System.Windows.Forms.ComboBox()
        Me.ComboBoxCountry = New System.Windows.Forms.ComboBox()
        Me.Xl_Lookup_Zip1 = New Winforms.Xl_LookupZip()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TextBoxZip
        '
        Me.TextBoxZip.Location = New System.Drawing.Point(78, 14)
        Me.TextBoxZip.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxZip.MaxLength = 50
        Me.TextBoxZip.Name = "TextBoxZip"
        Me.TextBoxZip.Size = New System.Drawing.Size(75, 20)
        Me.TextBoxZip.TabIndex = 88
        '
        'ComboBoxPlaceNames
        '
        Me.ComboBoxPlaceNames.FormattingEnabled = True
        Me.ComboBoxPlaceNames.Location = New System.Drawing.Point(157, 14)
        Me.ComboBoxPlaceNames.Name = "ComboBoxPlaceNames"
        Me.ComboBoxPlaceNames.Size = New System.Drawing.Size(74, 21)
        Me.ComboBoxPlaceNames.TabIndex = 87
        Me.ComboBoxPlaceNames.Visible = False
        '
        'ComboBoxCountry
        '
        Me.ComboBoxCountry.FormattingEnabled = True
        Me.ComboBoxCountry.Items.AddRange(New Object() {"(altres)", "Espanya", "Portugal", "Andorra"})
        Me.ComboBoxCountry.Location = New System.Drawing.Point(0, 14)
        Me.ComboBoxCountry.Name = "ComboBoxCountry"
        Me.ComboBoxCountry.Size = New System.Drawing.Size(74, 21)
        Me.ComboBoxCountry.TabIndex = 86
        '
        'Xl_Lookup_Zip1
        '
        Me.Xl_Lookup_Zip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Lookup_Zip1.IsDirty = False
        Me.Xl_Lookup_Zip1.Location = New System.Drawing.Point(157, 14)
        Me.Xl_Lookup_Zip1.Name = "Xl_Lookup_Zip1"
        Me.Xl_Lookup_Zip1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_Zip1.ReadOnlyLookup = False
        Me.Xl_Lookup_Zip1.Size = New System.Drawing.Size(533, 20)
        Me.Xl_Lookup_Zip1.TabIndex = 85
        Me.Xl_Lookup_Zip1.Value = Nothing
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(75, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 13)
        Me.Label9.TabIndex = 90
        Me.Label9.Text = "codi postal"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(-3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(26, 13)
        Me.Label8.TabIndex = 89
        Me.Label8.Text = "pais"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(154, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 91
        Me.Label1.Text = "població"
        '
        'Xl_GeoNamesPostalCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxZip)
        Me.Controls.Add(Me.ComboBoxPlaceNames)
        Me.Controls.Add(Me.ComboBoxCountry)
        Me.Controls.Add(Me.Xl_Lookup_Zip1)
        Me.Name = "Xl_GeoNamesPostalCode"
        Me.Size = New System.Drawing.Size(690, 37)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxZip As TextBox
    Friend WithEvents ComboBoxPlaceNames As ComboBox
    Friend WithEvents ComboBoxCountry As ComboBox
    Friend WithEvents Xl_Lookup_Zip1 As Xl_LookupZip
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label1 As Label
End Class
