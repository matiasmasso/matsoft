<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Atlas_Contacts
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
        Me.Xl_Countries_Grid1 = New Winforms.Xl_Countries()
        Me.Xl_Zonas_Grid1 = New Winforms.Xl_Zonas_Grid()
        Me.Xl_Locations_Grid1 = New Winforms.Xl_Locations_Grid()
        Me.Xl_Contacts_Grid1 = New Winforms.Xl_Contacts_Grid()
        Me.SuspendLayout()
        '
        'Xl_Countries_Grid1
        '
        Me.Xl_Countries_Grid1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Xl_Countries_Grid1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Countries_Grid1.Name = "Xl_Countries_Grid1"
        Me.Xl_Countries_Grid1.Size = New System.Drawing.Size(150, 150)
        Me.Xl_Countries_Grid1.TabIndex = 0
        '
        'Xl_Zonas_Grid1
        '
        Me.Xl_Zonas_Grid1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Xl_Zonas_Grid1.Location = New System.Drawing.Point(150, 0)
        Me.Xl_Zonas_Grid1.Name = "Xl_Zonas_Grid1"
        Me.Xl_Zonas_Grid1.Size = New System.Drawing.Size(150, 150)
        Me.Xl_Zonas_Grid1.TabIndex = 1
        '
        'Xl_Locations_Grid1
        '
        Me.Xl_Locations_Grid1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Xl_Locations_Grid1.Location = New System.Drawing.Point(300, 0)
        Me.Xl_Locations_Grid1.Name = "Xl_Locations_Grid1"
        Me.Xl_Locations_Grid1.Size = New System.Drawing.Size(150, 150)
        Me.Xl_Locations_Grid1.TabIndex = 2
        '
        'Xl_Contacts_Grid1
        '
        Me.Xl_Contacts_Grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contacts_Grid1.Location = New System.Drawing.Point(450, 0)
        Me.Xl_Contacts_Grid1.Name = "Xl_Contacts_Grid1"
        Me.Xl_Contacts_Grid1.Size = New System.Drawing.Size(312, 150)
        Me.Xl_Contacts_Grid1.TabIndex = 3
        '
        'Xl_Atlas_Contacts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Xl_Contacts_Grid1)
        Me.Controls.Add(Me.Xl_Locations_Grid1)
        Me.Controls.Add(Me.Xl_Zonas_Grid1)
        Me.Controls.Add(Me.Xl_Countries_Grid1)
        Me.Name = "Xl_Atlas_Contacts"
        Me.Size = New System.Drawing.Size(762, 150)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Countries_Grid1 As Winforms.Xl_Countries
    Friend WithEvents Xl_Zonas_Grid1 As Winforms.Xl_Zonas_Grid
    Friend WithEvents Xl_Locations_Grid1 As Winforms.Xl_Locations_Grid
    Friend WithEvents Xl_Contacts_Grid1 As Winforms.Xl_Contacts_Grid

End Class
