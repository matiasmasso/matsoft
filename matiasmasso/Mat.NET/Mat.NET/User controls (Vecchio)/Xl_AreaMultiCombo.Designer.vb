<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_AreaMultiCombo
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
        Me.Xl_CountriesCombo1 = New Mat.NET.Xl_CountriesCombo()
        Me.Xl_ZonasCombo1 = New Mat.NET.Xl_ZonasCombo()
        Me.Xl_LocationsCombo1 = New Mat.NET.Xl_LocationsCombo()
        Me.Xl_ContactsCombo1 = New Mat.NET.Xl_ContactsCombo()
        Me.SuspendLayout()
        '
        'Xl_CountriesCombo1
        '
        Me.Xl_CountriesCombo1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Xl_CountriesCombo1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CountriesCombo1.Name = "Xl_CountriesCombo1"
        Me.Xl_CountriesCombo1.Size = New System.Drawing.Size(501, 21)
        Me.Xl_CountriesCombo1.TabIndex = 0
        '
        'Xl_ZonasCombo1
        '
        Me.Xl_ZonasCombo1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Xl_ZonasCombo1.Location = New System.Drawing.Point(0, 21)
        Me.Xl_ZonasCombo1.Name = "Xl_ZonasCombo1"
        Me.Xl_ZonasCombo1.Size = New System.Drawing.Size(501, 21)
        Me.Xl_ZonasCombo1.TabIndex = 1
        '
        'Xl_LocationsCombo1
        '
        Me.Xl_LocationsCombo1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Xl_LocationsCombo1.Location = New System.Drawing.Point(0, 42)
        Me.Xl_LocationsCombo1.Name = "Xl_LocationsCombo1"
        Me.Xl_LocationsCombo1.Size = New System.Drawing.Size(501, 21)
        Me.Xl_LocationsCombo1.TabIndex = 2
        '
        'Xl_ContactsCombo1
        '
        Me.Xl_ContactsCombo1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Xl_ContactsCombo1.Location = New System.Drawing.Point(0, 63)
        Me.Xl_ContactsCombo1.Name = "Xl_ContactsCombo1"
        Me.Xl_ContactsCombo1.Size = New System.Drawing.Size(501, 21)
        Me.Xl_ContactsCombo1.TabIndex = 3
        '
        'Xl_AreaMultiCombo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Xl_ContactsCombo1)
        Me.Controls.Add(Me.Xl_LocationsCombo1)
        Me.Controls.Add(Me.Xl_ZonasCombo1)
        Me.Controls.Add(Me.Xl_CountriesCombo1)
        Me.Name = "Xl_AreaMultiCombo"
        Me.Size = New System.Drawing.Size(501, 86)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_CountriesCombo1 As Mat.NET.Xl_CountriesCombo
    Friend WithEvents Xl_ZonasCombo1 As Mat.NET.Xl_ZonasCombo
    Friend WithEvents Xl_LocationsCombo1 As Mat.NET.Xl_LocationsCombo
    Friend WithEvents Xl_ContactsCombo1 As Mat.NET.Xl_ContactsCombo

End Class
